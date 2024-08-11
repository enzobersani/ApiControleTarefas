using API.ControleTarefas.Domain.Commands;
using API.ControleTarefas.Domain.Entities;
using API.ControleTarefas.Domain.Interfaces.UnitOfWork;
using API.ControleTarefas.Domain.Models.Response;
using API.ControleTarefas.Domain.Notification;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace API.ControleTarefas.Domain.Handlers.CommandHandler
{
    public class ProjectCommandHandler : IRequestHandler<CreateProjectCommand, BaseResponseModel>,
                                         IRequestHandler<UpdateProjectCommand, UpdateProjectResponseModel>,
                                         IRequestHandler<DeleteProjectCommand, DeleteProjectResponseModel>
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

        public async Task<BaseResponseModel> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new ProjectEntity(request.Name);
            project.SetCreationDate();

            await _unitOfWork.ProjectRepository.AddAsync(project);

            await _unitOfWork.CommitAsync();
            return new BaseResponseModel
            {
                Id = project.Id
            };
        }

        public async Task<UpdateProjectResponseModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.ProjectRepository.GetById(request.Id);
            if (project is null)
            {
                _notifications.AddNotification("Handle", "Projeto informado não cadastrado!");
                return new UpdateProjectResponseModel();
            }

            project.Update(request.Name);
            project.SetUpdateDate();

            _unitOfWork.ProjectRepository.Update(project);
            await _unitOfWork.CommitAsync();
            return new UpdateProjectResponseModel
            {
                Id = project.Id,
                Name = project.Name,
                IsInactive = project.IsInactive
            };
        }

        public async Task<DeleteProjectResponseModel> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.ProjectRepository.GetById(request.Id);

            if (project is null)
            {
                _notifications.AddNotification("Handle", "Projeto não encontrado.");
                return new DeleteProjectResponseModel();
            }

            project.SetDeleteDate();
            project.SetInactive();

            _unitOfWork.ProjectRepository.Update(project);

            await _unitOfWork.CommitAsync();

            return new DeleteProjectResponseModel
            {
                Id = project.Id,
                IsInactive = project.IsInactive
            };
        }
    }
}
