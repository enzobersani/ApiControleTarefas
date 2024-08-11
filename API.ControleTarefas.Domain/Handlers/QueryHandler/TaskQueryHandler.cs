using API.ControleTarefas.Domain.Interfaces.Repositories;
using API.ControleTarefas.Domain.Interfaces.UnitOfWork;
using API.ControleTarefas.Domain.Models.Response;
using API.ControleTarefas.Domain.Notification;
using API.ControleTarefas.Domain.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.ControleTarefas.Domain.Handlers.QueryHandler
{
    public class TaskQueryHandler : IRequestHandler<SearchTaskQuery, SearchTaskResponseModel>
    {
        private readonly INotificationService _notifications;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public TaskQueryHandler(INotificationService notifications, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _notifications = notifications;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }
        public async Task<SearchTaskResponseModel> Handle(SearchTaskQuery request, CancellationToken cancellationToken)
        {
            var taskQuery = _unitOfWork.TaskRepository.Query();
            var collaboratorQuery = _unitOfWork.CollaboratorRepository.Query();

            taskQuery = taskQuery.Where(t => t.IsInactive == false);

            if (request.Id.HasValue)
            {
                taskQuery = taskQuery.Where(t => t.Id == request.Id.Value);
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                taskQuery = taskQuery.Where(t => t.Name.Contains(request.Name));
            }

            if (request.ProjectId.HasValue)
            {
                taskQuery = taskQuery.Where(t => t.ProjectId == request.ProjectId.Value);
            }

            if (request.CollaboratorId.HasValue)
            {
                taskQuery = taskQuery.Where(t => t.CollaboratorId == request.CollaboratorId.Value);
            }

            var tasks = await taskQuery
                .GroupJoin(
                    collaboratorQuery,
                    task => task.CollaboratorId,
                    collaborator => collaborator.Id,
                    (task, collaborators) => new { task, collaborators }
                )
                .SelectMany(
                    x => x.collaborators.DefaultIfEmpty(),
                    (x, collaborator) => new SearchTaskResultModel
                    {
                        Id = x.task.Id,
                        Name = x.task.Name,
                        Description = x.task.Description,
                        ProjectId = (Guid)x.task.ProjectId,
                        CollaboratorId = collaborator.Id,
                        CollaboratorName = collaborator.Name
                    }
                )
                .ToListAsync(cancellationToken);

            var response = new SearchTaskResponseModel
            {
                Items = tasks
            };

            if (!response.TryPage(request.PageSize, request.Page))
                _notifications.AddNotification("Handle", $"Página {request.Page.ToString()} não encontrada!");

            return response;
        }
    }
}
