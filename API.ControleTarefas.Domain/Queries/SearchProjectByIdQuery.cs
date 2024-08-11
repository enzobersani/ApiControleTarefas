using API.ControleTarefas.Domain.Models.Response;
using MediatR;

namespace API.ControleTarefas.Domain.Queries
{
    public class SearchProjectByIdQuery : IRequest<ProjectResultModel>
    {
        public Guid Id { get; set; }
    }
}
