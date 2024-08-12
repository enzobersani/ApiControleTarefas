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
    [Route("api/timeTracker")]
    public class TimeTrackerController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public TimeTrackerController(IMediator mediator, INotificationService notifications) : base(notifications)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Inclusão de TimeTrackers
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(BaseResponseModel), 201)]
        [ProducesResponseType(typeof(Notification), 400)]
        [Produces("application/json")]
        public async Task<IActionResult> Create([FromBody] InsertTimeTrackersCommand request)
            => Response(await _mediator.Send(request), 201);

        /// <summary>
        /// Consulta de TimeTrackers
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("task")]
        [ProducesResponseType(typeof(SearchTimeTrackerResponseModel), 200)]
        [ProducesResponseType(typeof(Notification), 400)]
        [Produces("application/json")]
        public async Task<IActionResult> Search([FromQuery] SearchTimeTrackerQuery request)
            => Response(await _mediator.Send(request), 200);

        /// <summary>
        /// Consulta de horas gastadas
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("hours")]
        [ProducesResponseType(typeof(SearchHoursResponseModel), 200)]
        [ProducesResponseType(typeof(Notification), 400)]
        [Produces("application/json")]
        public async Task<IActionResult> SearchHours([FromQuery] SearchHoursQuery request)
            => Response(await _mediator.Send(request), 200);
    }
}
