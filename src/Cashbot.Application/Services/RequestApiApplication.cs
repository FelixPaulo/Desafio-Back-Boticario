using AutoMapper;
using Cashbot.Application.Interfaces;
using Cashbot.Application.Notifications;
using Cashbot.Application.ViewModels.ResponseRequest;
using Cashbot.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cashbot.Application.Services
{
    public class RequestApiApplication : BaseApplication, IRequestApiApplication
    {
        private readonly IMediatorHandler _mediator;

        public RequestApiApplication(IMapper mapper,
                              IUnitOfWork uow,
                              IMediatorHandler mediator,
                              IHttpContextAccessor httpContextAccessor,
                              INotificationHandler<ApplicationNotification> notifications) : base(uow, mediator, httpContextAccessor, notifications)
        {
            _mediator = mediator;
        }


        public async Task<object> GetApiClient()
        {

            using (var client = new HttpClient())
            {
                var url = "https://mdaqk8ek5j.execute-api.us-east-1.amazonaws.com/v1/cashback?cpf=12312312323";
                client.DefaultRequestHeaders.Add("token", "ZXPURQOARHiMc6Y0flhRC1LVlZQVFRnm");
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer ZXPURQOARHiMc6Y0flhRC1LVlZQVFRnm");
                var response = await client.GetAsync(url);


                if (!response.IsSuccessStatusCode)
                {
                    await _mediator.PublishEvent(new ApplicationNotification($"Falha ao fazer a requisição status: {response.StatusCode}"));
                    return null;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var responseCashback = JsonConvert.DeserializeObject<ResponseCashbackViewModel>(json);

                    return new { responseCashback?.Body?.Credit };
                }
            }
        }
    }
}
