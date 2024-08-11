using API.ControleTarefas.Domain.Models.Response;
using MediatR;

namespace API.ControleTarefas.Domain.Commands
{
    public class DeleteProjectCommand : IRequest<DeleteProjectResponseModel>
    {
        public Guid Id { get; set; }

        public DeleteProjectCommand(Guid id)
        {
            Id = id;
        }
    }
}
