using Cashbot.Application.Interfaces;
using Cashbot.Application.Notifications;
using Cashbot.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Cashbot.Application.Services
{
    public class BaseApplication
    {

        private readonly IUnitOfWork _uow;
        private readonly IMediatorHandler _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationNotificationHandler _notifications;


        public BaseApplication(IUnitOfWork uow, IMediatorHandler mediator, IHttpContextAccessor httpContextAccessor,
                                INotificationHandler<ApplicationNotification> notifications)
        {
            _uow = uow;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _notifications = (ApplicationNotificationHandler)notifications;
        }

        protected async Task<bool> Commit()
        {
            if (_notifications.HasNotifications()) return false;

            if (_uow.Commit())
                return true;

            await _mediator.PublishEvent(new ApplicationNotification("Ocorreu um erro ao salvar os dados"));
            return false;
        }
    }
}
