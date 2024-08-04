using API.ControleTarefas.Domain.Commands;
using API.ControleTarefas.Domain.Entities;
using API.ControleTarefas.Domain.Interfaces.UnitOfWork;
using API.ControleTarefas.Domain.Models.Response;
using API.ControleTarefas.Domain.Notification;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace API.ControleTarefas.Domain.Handlers.CommandHandler
{
    public class ProjectCommandHandler : IRequestHandler<InsertProjectCommand, InsertProjectResponseModel>
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

        public async Task<InsertProjectResponseModel> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project(request.Name);
            project.SetCreationDate();

            await _unitOfWork.ProjectRepository.AddAsync(project);
            await _unitOfWork.CommitAsync();
            return new InsertProjectResponseModel
            {
                Code = project.Id
            };
        }
    }
}
