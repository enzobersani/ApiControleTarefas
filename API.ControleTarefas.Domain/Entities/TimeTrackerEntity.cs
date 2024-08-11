using API.ControleTarefas.Domain.Commands;
using API.ControleTarefas.Domain.Entities.Base;
using Mapster;

namespace API.ControleTarefas.Domain.Entities
{
    public class TimeTrackerEntity : BaseEntity
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string TimeZoneId { get; private set; }
        public Guid TaskId { get; private set; }
        public Guid CollaboratorId { get; private set; }

        public static TimeTrackerEntity New(InsertTimeTrackersCommand request)
        {
            var timeTracker = new TimeTrackerEntity();
            request.Adapt(timeTracker);

            return timeTracker;
        }
    }
}
