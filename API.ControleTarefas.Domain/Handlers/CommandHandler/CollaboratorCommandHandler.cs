using API.ControleTarefas.Domain.Commands;
using API.ControleTarefas.Domain.Entities;
using API.ControleTarefas.Domain.Interfaces.UnitOfWork;
using API.ControleTarefas.Domain.Models.Response;
using API.ControleTarefas.Domain.Notification;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Handlers.CommandHandler
{
    public class CollaboratorCommandHandler : IRequestHandler<InsertCollaboratorCommand, BaseResponseModel>
    {
        private readonly INotificationService _notifications;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public CollaboratorCommandHandler(INotificationService notifications, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _notifications = notifications;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponseModel> Handle(InsertCollaboratorCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetById(request.UserId);
            if(user is null)
            {
                _notifications.AddNotification("Handler", $"Usuário não encontrado para o código {request.UserId}");
                return new BaseResponseModel();
            }

            var collaborator = CollaboratorEntity.New(request);
            collaborator.SetCreationDate();

            await _unitOfWork.CollaboratorRepository.AddAsync(collaborator);
            await _unitOfWork.CommitAsync();

            return new BaseResponseModel
            {
                Id = collaborator.Id
            };
        }
    }
}
