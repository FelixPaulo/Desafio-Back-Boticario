using Cashbot.Application.Interfaces;
using Cashbot.Application.Notifications;
using Cashbot.Application.ViewModels.Dealer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Cashbot.Services.Api.Controllers
{
    [Route("Dealers")]
    [Authorize(Policy = "Bearer")]
    [ApiController]
    public class DealerController : BaseController
    {

        private readonly IDealerApplication _dealerApplication;

        public DealerController(INotificationHandler<ApplicationNotification> notifications,
            IDealerApplication dealerApplication) : base(notifications)
        {
            _dealerApplication = dealerApplication;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addGroupViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] AddDealerViewModel addDealerViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return base.ResponseError(ModelState.Values);

                var dealer = await _dealerApplication.Add(addDealerViewModel);
                return base.ResponseResult(dealer);
            }
            catch (System.Exception ex)
            {
                return base.BadRequest(ex.Message);
            }
        }
    }
}