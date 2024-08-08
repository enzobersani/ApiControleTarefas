using API.ControleTarefas.Domain.Models.Response;
using MediatR;

namespace API.ControleTarefas.Domain.Commands
{
    public class UpsertProjectCommand : IRequest<BaseResponseModel>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
