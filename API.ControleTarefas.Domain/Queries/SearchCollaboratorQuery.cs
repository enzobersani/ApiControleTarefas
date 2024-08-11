using API.ControleTarefas.Domain.Models;
using MediatR;

namespace API.ControleTarefas.Domain.Queries
{
    public class SearchCollaboratorQuery : IRequest<CollaboratorKeyResultModel>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 100;
    }
}
