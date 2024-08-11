using API.ControleTarefas.Domain.Commands;
using API.ControleTarefas.Domain.Entities;
using API.ControleTarefas.Domain.Interfaces.UnitOfWork;
using API.ControleTarefas.Domain.Models.Response;
using API.ControleTarefas.Domain.Notification;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace API.ControleTarefas.Domain.Handlers.CommandHandler
{
    internal class TaskCommandHandler : IRequestHandler<InsertTaskCommand, BaseResponseModel>,
                                        IRequestHandler<UpdateTaskCommand, UpdateTaskResponseModel>,
                                        IRequestHandler<DeleteTaskCommand, DeleteTaskResponseModel>
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

        public async Task<UpdateTaskResponseModel> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _unitOfWork.TaskRepository.GetById(request.Id);
            if (task is null)
            {
                _notifications.AddNotification("Handle", "Tarefa informada não cadastrada!");
                return new UpdateTaskResponseModel();
            }

            task.Update(request);
            task.SetUpdateDate();

            _unitOfWork.TaskRepository.Update(task);
            await _unitOfWork.CommitAsync();
            return new UpdateTaskResponseModel
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                CollaboratorId = task.CollaboratorId,
                ProjectId = task.ProjectId,
                IsInactive = task.IsInactive
            };
        }

        public async Task<DeleteTaskResponseModel> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _unitOfWork.TaskRepository.GetById(request.Id);

            if (task is null)
            {
                _notifications.AddNotification("Handle", "Projeto não encontrado.");
                return new DeleteTaskResponseModel();
            }

            task.SetDeleteDate();
            task.SetInactive();

            _unitOfWork.TaskRepository.Update(task);

            await _unitOfWork.CommitAsync();

            return new DeleteTaskResponseModel
            {
                Id = task.Id,
                IsInactive = task.IsInactive
            };
        }
    }
}
