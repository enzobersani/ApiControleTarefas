using API.ControleTarefas.Domain.Entities;
using API.ControleTarefas.Domain.Interfaces.Repositories;
using API.ControleTarefas.Domain.Notification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Infrastructure.Repositories
{
    public class TimeTrackerRepository : ITimeTrackerRepository
    {
        private readonly ApiControleTarefasDbContext _context;
        private readonly INotificationService _notifications;

        public TimeTrackerRepository(ApiControleTarefasDbContext context, INotificationService notification)
        {
            _context = context;
            _notifications = notification;
        }
        public async Task AddAsync(TimeTrackerEntity timeTracker)
        {
            await _context.TimeTrackers.AddAsync(timeTracker);
        }

        public async Task<List<TimeTrackerEntity>> getByTaskId(Guid taskId)
        {
            try
            {
                return await _context.TimeTrackers.AsNoTracking().Where(x => x.TaskId == taskId).ToListAsync();
            }
            catch (Exception ex)
            {
                _notifications.AddNotification("getByTaskId", $"Ocorreu um erro: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> HasConflictingTimeAsync(DateTime startDate, DateTime endDate, Guid collaboratorId)
        {
            return await _context.TimeTrackers
                .AnyAsync(tt =>
                    tt.CollaboratorId == collaboratorId &&
                    tt.StartDate < endDate &&
                    tt.EndDate > startDate);
        }

        public async Task<double> GetTotalHoursInDayAsync(DateTime startOfDay, DateTime endOfDay, Guid collaboratorId)
        {
            var timeTrackers = await _context.TimeTrackers
                .Where(tt => tt.CollaboratorId == collaboratorId &&
                             tt.StartDate >= startOfDay &&
                             tt.EndDate <= endOfDay)
                .ToListAsync();

            return timeTrackers.Sum(tt => (tt.EndDate - tt.StartDate).TotalHours);
        }

        public async Task<double> GetHoursAsync(Guid collaboratorId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            return await _context.TimeTrackers
                .Where(t => t.CollaboratorId == collaboratorId && t.StartDate >= startDate && t.EndDate < endDate)
                .SumAsync(t => EF.Functions.DateDiffMinute(t.StartDate, t.EndDate) / 60.0, cancellationToken);
        }
    }
}
