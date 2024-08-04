using API.ControleTarefas.Controllers.Base;
using API.ControleTarefas.Domain.Commands;
using API.ControleTarefas.Domain.Models.Response;
using API.ControleTarefas.Domain.Notification;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.ControleTarefas.Controllers
{
    [Route("api/auth")]
    public class AuthController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator ,INotificationService notifications) : base(notifications)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(RegisterUserResponseModel), 201)]
        [ProducesResponseType(typeof(Notification), 400)]
        [Produces("application/json")]
        public async Task<IActionResult> Auth([FromBody] RegisterUserCommand request)
            => Response(await _mediator.Send(request), 201);

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(RegisterUserResponseModel), 200)]
        [ProducesResponseType(typeof(Notification), 401)]
        [Produces("application/json")]
        public async Task<IActionResult> Login([FromBody] LoginCommand request)
            => Response(await _mediator.Send(request), 200);
    }
}
