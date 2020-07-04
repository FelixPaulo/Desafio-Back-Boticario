using Cashbot.Application.Interfaces;
using Cashbot.Application.Notifications;
using Cashbot.Application.ViewModels.Dealer;
using Cashbot.Application.ViewModels.Purchase;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Cashbot.Services.Api.Controllers
{
    [Route("Purchases")]
    [Authorize(Policy = "Bearer")]
    [ApiController]
    public class PurchaseController : BaseController
    {

        private readonly IPurchaseApplication _purchaseApplication;

        public PurchaseController(INotificationHandler<ApplicationNotification> notifications,
            IPurchaseApplication purchaseApplication) : base(notifications)
        {
            _purchaseApplication = purchaseApplication;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addGroupViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] AddPurchaseViewModel addPurchaseViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return base.ResponseError(ModelState.Values);

                var purchase = await _purchaseApplication.Add(addPurchaseViewModel);
                return base.ResponseResult(purchase);
            }
            catch (System.Exception ex)
            {
                return base.BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return base.ResponseResult(await _purchaseApplication.ListPurchase());
            }
            catch (System.Exception ex)
            {
                return base.BadRequest(ex.Message);
            }
        }
    }
}