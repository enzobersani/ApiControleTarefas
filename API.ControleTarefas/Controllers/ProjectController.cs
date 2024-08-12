using API.ControleTarefas.Controllers.Base;
using API.ControleTarefas.Domain.Commands;
using API.ControleTarefas.Domain.Models.Response;
using API.ControleTarefas.Domain.Notification;
using API.ControleTarefas.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.ControleTarefas.Controllers
{
    [Route("api/project")]
    public class ProjectController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public ProjectController(IMediator mediator, INotificationService notifications) : base(notifications)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Inclusão de projetos.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        //[Authorize]
        [ProducesResponseType(typeof(BaseResponseModel), 201)]
        [ProducesResponseType(typeof(Notification), 400)]
        [Produces("application/json")]
        public async Task<IActionResult> Create([FromBody] CreateProjectCommand request)
            => Response(await _mediator.Send(request), 201);

        /// <summary>
        /// Atualização de projetos existentes.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(UpdateProjectResponseModel), 200)]
        [ProducesResponseType(typeof(Notification), 400)]
        [Produces("application/json")]
        public async Task<IActionResult> Update([FromBody] UpdateProjectCommand request)
            => Response(await _mediator.Send(request), 200);

        /// <summary>
        /// Consulta de projetos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        //[Authorize]
        [ProducesResponseType(typeof(SearchProjectResponseModel), 200)]
        [ProducesResponseType(typeof(Notification), 400)]
        [Produces("application/json")]
        public async Task<IActionResult> Search([FromQuery] SearchProjectQuery request)
            => Response(await _mediator.Send(request), 200);

        /// <summary>
        /// Consulta por código de projeto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        //[Authorize]
        [Route("id")]
        [ProducesResponseType(typeof(ProjectResultModel), 200)]
        [ProducesResponseType(typeof(Notification), 400)]
        [Produces("application/json")]
        public async Task<IActionResult> SearchById([FromQuery] SearchProjectByIdQuery request)
            => Response(await _mediator.Send(request));

        /// <summary>
        /// Exclusão de projeto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeleteProjectResponseModel), 200)]
        [ProducesResponseType(typeof(Notification), 400)]
        [Produces("application/json")]
        public async Task<IActionResult> Delete(Guid id)
            => Response(await _mediator.Send(new DeleteProjectCommand(id)));
    }
}
