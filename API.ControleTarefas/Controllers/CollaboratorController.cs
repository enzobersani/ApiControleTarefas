using API.ControleTarefas.Controllers.Base;
using API.ControleTarefas.Domain.Commands;
using API.ControleTarefas.Domain.Models;
using API.ControleTarefas.Domain.Models.Response;
using API.ControleTarefas.Domain.Notification;
using API.ControleTarefas.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.ControleTarefas.Controllers
{
    [Route("api/collaborator")]
    public class CollaboratorController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public CollaboratorController(IMediator mediator, INotificationService notifications) : base(notifications)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponseModel), 201)]
        [ProducesResponseType(typeof(Notification), 400)]
        [Produces("application/json")]
        public async Task<IActionResult> Create([FromBody] InsertCollaboratorCommand request)
            => Response(await _mediator.Send(request), 201);

        [HttpGet]
        [ProducesResponseType(typeof(CollaboratorKeyResultModel), 200)]
        [ProducesResponseType(typeof(Notification), 400)]
        [Produces("application/json")]
        public async Task<IActionResult> Search([FromQuery] SearchCollaboratorQuery request)
            => Response(await _mediator.Send(request), 200);
    }
}
