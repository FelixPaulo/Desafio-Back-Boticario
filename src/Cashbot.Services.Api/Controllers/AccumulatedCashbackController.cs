using Cashbot.Application.Interfaces;
using Cashbot.Application.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Cashbot.Services.Api.Controllers
{
    [Route("AccumulatedCashbacks")]
    [Authorize(Policy = "Bearer")]
    [ApiController]
    public class AccumulatedCashbackController : BaseController
    {

        private readonly IRequestApiApplication _requestApiApplication;

        public AccumulatedCashbackController(INotificationHandler<ApplicationNotification> notifications,
            IRequestApiApplication requestApiApplication) : base(notifications)
        {
            _requestApiApplication = requestApiApplication;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _requestApiApplication.GetApiClient();
                return base.ResponseResult(result);
            }
            catch (System.Exception ex)
            {
                return base.BadRequest(ex.Message);
            }
        }
    }
}