using API.ControleTarefas.Controllers.Base;
using API.ControleTarefas.Domain.Commands;
using API.ControleTarefas.Domain.Models.Response;
using API.ControleTarefas.Domain.Notification;
using API.ControleTarefas.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.ControleTarefas.Controllers
{
    [Route("api/task")]
    public class TaskController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public TaskController(IMediator mediator, INotificationService notifications) : base(notifications)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Inclusão de tarefas.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(BaseResponseModel), 201)]
        [ProducesResponseType(typeof(Notification), 400)]
        [Produces("application/json")]
        public async Task<IActionResult> Create([FromBody] InsertTaskCommand request)
            => Response(await _mediator.Send(request), 201);

        /// <summary>
        /// Atualização de tarefas
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(typeof(UpdateTaskResponseModel), 200)]
        [ProducesResponseType(typeof(Notification), 400)]
        [Produces("application/json")]
        public async Task<IActionResult> Update([FromBody] UpdateTaskCommand request)
            => Response(await _mediator.Send(request), 200);
        
        /// <summary>
        /// Exclusão de tarefas
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(DeleteTaskResponseModel), 200)]
        [ProducesResponseType(typeof(Notification), 400)]
        [Produces("application/json")]
        public async Task<IActionResult> Delete(Guid id)
            => Response(await _mediator.Send(new DeleteTaskCommand(id)));

        /// <summary>
        /// Consulta de tarefas
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(SearchTaskResponseModel), 200)]
        [ProducesResponseType(typeof(Notification), 400)]
        [Produces("application/json")]
        public async Task<IActionResult> Search([FromQuery] SearchTaskQuery request)
            => Response(await _mediator.Send(request));
    }
}
