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
    internal class TaskCommandHandler : IRequestHandler<InsertTaskCommand, BaseResponseModel>
    {
        private readonly INotificationService _notifications;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public TaskCommandHandler(INotificationService notifications, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _notifications = notifications;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponseModel> Handle(InsertTaskCommand request, CancellationToken cancellationToken)
        {
            var task = TaskEntity.New(request);
            task.SetCreationDate();

            await _unitOfWork.TaskRepository.AddAsync(task);
            await _unitOfWork.CommitAsync();

            return new BaseResponseModel
            {
                Id = task.Id
            };
        }
    }
}
