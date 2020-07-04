using Cashbot.Application.Handlers;
using Cashbot.Application.Interfaces;
using Cashbot.Application.Notifications;
using Cashbot.Application.Services;
using Cashbot.Domain.Interfaces;
using Cashbot.Infra.Data.Context;
using Cashbot.Infra.Data.Repository;
using Cashbot.Infra.Data.UoW;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Cashbot.Infra.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Application
            services.AddScoped<INotificationHandler<ApplicationNotification>, ApplicationNotificationHandler>();
            services.AddScoped<IDealerApplication, DealerApplication>();
            services.AddScoped<IPurchaseApplication, PurchaseApplication>();
            services.AddScoped<IRequestApiApplication, RequestApiApplication>();

            //Infra - Data
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDealerRepository, DealerRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();

            // Context
            services.AddScoped<CachbotContext>();

        }
    }
}
