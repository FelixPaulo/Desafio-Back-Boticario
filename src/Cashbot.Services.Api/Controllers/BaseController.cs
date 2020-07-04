using Cashbot.Application.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Cashbot.Services.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        private readonly ApplicationNotificationHandler _notifications;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notifications"></param>
        public BaseController(INotificationHandler<ApplicationNotification> notifications)
        {
            _notifications = (ApplicationNotificationHandler)notifications;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected bool HasNotifications()
        {
            return (!_notifications.HasNotifications());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected IActionResult ResponseOk(object result = null)
        {
            return base.Ok(new { data = result });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected IActionResult ResponseError(object result = null)
        {
            return base.BadRequest(new { errors = result });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected IActionResult ResponseResult(object result = null)
        {
            if (HasNotifications())
                return base.Ok(new { data = result });

            return base.BadRequest(new { errors = _notifications.GetNotifications().Select(n => n.Value) });
        }
    }
}