using API.ControleTarefas.Domain.Models.Response;
using MediatR;

namespace API.ControleTarefas.Domain.Commands
{
    public class InsertTaskCommand : IRequest<BaseResponseModel>
    {
        public string? Name { get; set;}
        public string? Description { get; set; }
        public string? ProjectId { get; set; }
        public string? CollaboratorId { get; set; }
    }
}
