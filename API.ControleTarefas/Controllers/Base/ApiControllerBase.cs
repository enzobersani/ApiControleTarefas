using API.ControleTarefas.Domain.Notification;
using Microsoft.AspNetCore.Mvc;

namespace API.ControleTarefas.Controllers.Base
{
    public class ApiControllerBase : ControllerBase
    {
        private readonly INotificationService _notifications;
        public ApiControllerBase(INotificationService notifications)
        {
            _notifications = notifications;
        }
        protected new IActionResult Response(object result = null, int okCode = 200)
        {
            if (_notifications.HasNotifications())
            {
                var notifications = _notifications.GetNotifications();

                //if (notifications.Any(e => e.Code == "PathParamNotFound"))
                //{
                //    return NotFound(new { notifications });
                //}

                if (notifications.Any(e => e.Code == "Unauthorized"))
                {
                    return Unauthorized(new { notifications });
                }

                //if (notifications.Any(e => e.Code == "Forbidden"))
                //{
                //    return Forbid();
                //}

                return BadRequest(new { notifications });
            }

            return new ObjectResult(result)
            {
                StatusCode = okCode
            };
        }
    }

}
