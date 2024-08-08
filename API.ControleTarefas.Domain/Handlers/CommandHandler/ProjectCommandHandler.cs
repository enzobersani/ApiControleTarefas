using API.ControleTarefas.Domain.Commands;
using API.ControleTarefas.Domain.Entities;
using API.ControleTarefas.Domain.Interfaces.UnitOfWork;
using API.ControleTarefas.Domain.Models.Response;
using API.ControleTarefas.Domain.Notification;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace API.ControleTarefas.Domain.Handlers.CommandHandler
{
    public class ProjectCommandHandler : IRequestHandler<UpsertProjectCommand, BaseResponseModel>
    {

        private readonly INotificationService _notifications;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectCommandHandler(INotificationService notifications, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _notifications = notifications;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponseModel> Handle(UpsertProjectCommand request, CancellationToken cancellationToken)
        {
            ProjectEntity project;

            var existingProject = await _unitOfWork.ProjectRepository.GetById(request.Id);
            if (existingProject != null) 
            {
                existingProject.Update(request.Name);
                existingProject.SetUpdateDate();
                project = existingProject;
            }
            else
            {
                project = new ProjectEntity(request.Name);
                project.SetCreationDate();

                await _unitOfWork.ProjectRepository.AddAsync(project);
            }

            await _unitOfWork.CommitAsync();
            return new BaseResponseModel
            {
                Id = project.Id
            };
        }
    }
}
