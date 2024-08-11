using API.ControleTarefas.Controllers.Base;
using API.ControleTarefas.Domain.Commands;
using API.ControleTarefas.Domain.Models.Response;
using API.ControleTarefas.Domain.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.ControleTarefas.Controllers
{
    [Route("api/timeTracker")]
    public class TimeTrackerController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public TimeTrackerController(IMediator mediator, INotificationService notifications) : base(notifications)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponseModel), 201)]
        [ProducesResponseType(typeof(Notification), 400)]
        [Produces("application/json")]
        public async Task<IActionResult> Create([FromBody] InsertTimeTrackersCommand request)
            => Response(await _mediator.Send(request), 201);
    }
}
