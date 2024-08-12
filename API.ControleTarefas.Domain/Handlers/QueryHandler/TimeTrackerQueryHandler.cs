using API.ControleTarefas.Domain.Interfaces.UnitOfWork;
using API.ControleTarefas.Domain.Models.Response;
using API.ControleTarefas.Domain.Notification;
using API.ControleTarefas.Domain.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Handlers.QueryHandler
{
    public class TimeTrackerQueryHandler : IRequestHandler<SearchTimeTrackerQuery, List<SearchTimeTrackerResponseModel>>,
                                           IRequestHandler<SearchHoursQuery, SearchHoursResponseModel>
    {
        private readonly INotificationService _notifications;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public TimeTrackerQueryHandler(INotificationService notifications, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _notifications = notifications;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SearchTimeTrackerResponseModel>> Handle(SearchTimeTrackerQuery request, CancellationToken cancellationToken)
        {
            var timeTrackers = await _unitOfWork.TimeTrackerRepository.getByTaskId(request.TaskId);
            if (timeTrackers == null || timeTrackers.Count == 0)
                return new List<SearchTimeTrackerResponseModel>();

            var timeTrackerResultModels = new List<SearchTimeTrackerResponseModel>();

            foreach (var timeTracker in timeTrackers)
            {
                var collaborator = await _unitOfWork.CollaboratorRepository.GetById(timeTracker.CollaboratorId);
                var duration = timeTracker.EndDate - timeTracker.StartDate;
                var hoursWorked = duration.TotalHours;

                int hours = (int)hoursWorked;
                int minutes = (int)((hoursWorked - hours) * 60);
                var formattedTime = $"{hours:D2}:{minutes:D2}";

                timeTrackerResultModels.Add(new SearchTimeTrackerResponseModel
                {
                    CollaboratorId = timeTracker.CollaboratorId,
                    CollaboratorName = collaborator.Name,
                    StartTime = timeTracker.StartDate,
                    EndTime = timeTracker.EndDate,
                    Hours = formattedTime
                });
            }

            return timeTrackerResultModels;
        }

        public async Task<SearchHoursResponseModel> Handle(SearchHoursQuery request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.CollaboratorRepository.GetById(request.CollaboratorId) is null)
            {
                _notifications.AddNotification("Handle", "Colaborador informado não cadastrado!");
                return new SearchHoursResponseModel();
            }

            var startDay = request.Date.Date;
            var endDay = startDay.AddDays(1);

            var startMonth = new DateTime(request.Date.Year, request.Date.Month, 1);
            var endOfMonth = startMonth.AddMonths(1);

            if (_notifications.HasNotifications())
                return new SearchHoursResponseModel();

            var hoursToday = await _unitOfWork.TimeTrackerRepository.GetHoursAsync(request.CollaboratorId, startDay, endDay, cancellationToken);
            var hoursMonth = await _unitOfWork.TimeTrackerRepository.GetHoursAsync(request.CollaboratorId, startMonth, endOfMonth, cancellationToken);

            string FormatHours(double totalHours)
            {
                int hours = (int)totalHours;
                int minutes = (int)((totalHours - hours) * 60);
                return $"{hours:D2}:{minutes:D2}";
            }

            return new SearchHoursResponseModel
            {
                HoursToday = FormatHours(hoursToday),
                HoursMonth = FormatHours(hoursMonth)
            };
        }
    }
}
