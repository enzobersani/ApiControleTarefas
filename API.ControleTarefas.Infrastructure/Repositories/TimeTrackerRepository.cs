using API.ControleTarefas.Domain.Entities;
using API.ControleTarefas.Domain.Interfaces.Repositories;
using API.ControleTarefas.Domain.Notification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<bool> HasConflictingTimeAsync(DateTime startDate, DateTime endDate, Guid collaboratorId)
        {
            return await _context.TimeTrackers
                .AnyAsync(tt =>
                    tt.CollaboratorId == collaboratorId &&
                    tt.StartDate < endDate &&
                    tt.EndDate > startDate);
        }
    }
}
