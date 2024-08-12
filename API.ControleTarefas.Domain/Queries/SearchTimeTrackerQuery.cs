using API.ControleTarefas.Domain.Models.Response;
using MediatR;

namespace API.ControleTarefas.Domain.Queries
{
    public class SearchTimeTrackerQuery : IRequest<List<SearchTimeTrackerResponseModel>>
    {
        public Guid TaskId { get; set; }
    }
}
