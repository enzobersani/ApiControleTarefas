using API.ControleTarefas.Domain.Interfaces.UnitOfWork;
using API.ControleTarefas.Domain.Models;
using API.ControleTarefas.Domain.Models.Response;
using API.ControleTarefas.Domain.Notification;
using API.ControleTarefas.Domain.Queries;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Handlers.QueryHandler
{
    public class CollaboratorQueryHandler : IRequestHandler<SearchCollaboratorQuery, CollaboratorKeyResultModel>
    {
        private readonly INotificationService _notifications;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public CollaboratorQueryHandler(INotificationService notifications, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _notifications = notifications;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<CollaboratorKeyResultModel> Handle(SearchCollaboratorQuery request, CancellationToken cancellationToken)
        {
            var collaborators = await _unitOfWork.CollaboratorRepository.GetAllCollaborators();

            if (_notifications.HasNotifications())
                return new CollaboratorKeyResultModel();

            if (collaborators is null || !collaborators.Any())
                return new CollaboratorKeyResultModel();

            var collaboratorResultModels = new List<CollaboratorKeyModel>();

            foreach (var collaborator in collaborators)
            {
                collaboratorResultModels.Add(new CollaboratorKeyModel
                {
                    Id = collaborator.Id,
                    Name = collaborator.Name,
                });
            }

            var response = new CollaboratorKeyResultModel
            {
                Items = collaboratorResultModels
            };

            if (!response.TryPage(request.PageSize, request.Page))
                _notifications.AddNotification("Handle", $"Página {request.Page.ToString()} não encontrada!");

            return response;
        }
    }
}
