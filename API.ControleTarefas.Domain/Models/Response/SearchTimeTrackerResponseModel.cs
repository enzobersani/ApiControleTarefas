namespace API.ControleTarefas.Domain.Models.Response
{
    public class SearchTimeTrackerResponseModel
    {
        public Guid CollaboratorId { get; set; }
        public string CollaboratorName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Hours { get; set; }
    }
}
