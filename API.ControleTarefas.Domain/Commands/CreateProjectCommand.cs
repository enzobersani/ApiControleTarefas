using API.ControleTarefas.Domain.Models.Response;
using MediatR;

namespace API.ControleTarefas.Domain.Commands
{
    public class CreateProjectCommand : IRequest<BaseResponseModel>
    {
        public string Name { get; set; }
    }
}
