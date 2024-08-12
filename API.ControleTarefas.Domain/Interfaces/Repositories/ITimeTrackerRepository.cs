using API.ControleTarefas.Domain.Entities;
using API.ControleTarefas.Domain.Handlers.CommandHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Interfaces.Repositories
{
    public interface ITimeTrackerRepository
    {
        Task AddAsync(TimeTrackerEntity timeTracker);
        Task<bool> HasConflictingTimeAsync(DateTime startDate, DateTime endDate, Guid collaboratorId);
        Task<double> GetTotalHoursInDayAsync(DateTime startOfDay, DateTime endOfDay, Guid collaboratorId);
        Task<List<TimeTrackerEntity>> getByTaskId(Guid taskId);
        Task<double> GetHoursAsync(Guid collaboratorId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
    }
}
