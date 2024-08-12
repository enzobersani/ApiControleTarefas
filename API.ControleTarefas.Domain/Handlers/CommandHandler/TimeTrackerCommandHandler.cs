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
    public class TimeTrackerCommandHandler : IRequestHandler<InsertTimeTrackersCommand, BaseResponseModel>
    {
        private readonly INotificationService _notifications;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public TimeTrackerCommandHandler(INotificationService notifications, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _notifications = notifications;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponseModel> Handle(InsertTimeTrackersCommand request, CancellationToken cancellationToken)
        {
            var hasConflict = await _unitOfWork.TimeTrackerRepository.HasConflictingTimeAsync(request.StartDate, request.EndDate, request.CollaboratorId);
            if (hasConflict)
            {
                _notifications.AddNotification("Handle", "O intervalo de tempo conflita com um intervalo já existente.");
                return new BaseResponseModel();
            }

            if (await _unitOfWork.TaskRepository.GetById(request.TaskId) is null)
                _notifications.AddNotification("Handle", "Tarefa informada não cadastrada.");

            if (await _unitOfWork.CollaboratorRepository.GetById(request.CollaboratorId) is null)
                _notifications.AddNotification("Handle", "Colaborador informado não cadastrado.");

            await ValidTime(request);

            if (_notifications.HasNotifications())
                return new BaseResponseModel();

            var timeTracker = TimeTrackerEntity.New(request);
            timeTracker.SetCreationDate();

            await _unitOfWork.TimeTrackerRepository.AddAsync(timeTracker);
            await _unitOfWork.CommitAsync();

            return new BaseResponseModel
            {
                Id = timeTracker.Id
            };
        }

        public async Task ValidTime(InsertTimeTrackersCommand request)
        {
            var startOfDay = request.StartDate.Date;
            var endOfDay = request.StartDate.AddDays(1).AddTicks(-1);

            var totalHoursInDay = await _unitOfWork.TimeTrackerRepository.GetTotalHoursInDayAsync(startOfDay, endOfDay, request.CollaboratorId);
            var requestedHours = (request.EndDate - request.StartDate).TotalHours;

            if (totalHoursInDay + requestedHours > 24)
                _notifications.AddNotification("Handle", "O total de horas para este colaborador excede 24 horas em um único dia.");
        }
    }
}
