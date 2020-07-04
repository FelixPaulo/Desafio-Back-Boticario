using AutoMapper;
using Cashbot.Application.Interfaces;
using Cashbot.Application.Notifications;
using Cashbot.Application.ViewModels.Dealer;
using Cashbot.Application.ViewModels.Purchase;
using Cashbot.Domain.Interfaces;
using Cashbot.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cashbot.Application.Services
{
    public class PurchaseApplication : BaseApplication, IPurchaseApplication
    {

        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;
        private readonly IDealerApplication _dealerApplication;
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseApplication(IMapper mapper,
                                IUnitOfWork uow,
                                IMediatorHandler mediator,
                                IHttpContextAccessor httpContextAccessor,
                                IDealerApplication dealerApplication,
                                IPurchaseRepository purchaseRepository,
                                INotificationHandler<ApplicationNotification> notifications) : base(uow, mediator, httpContextAccessor, notifications)
        {
            _mapper = mapper;
            _mediator = mediator;
            _dealerApplication = dealerApplication;
            _purchaseRepository = purchaseRepository;
        }


        public async Task<PurchaseViewModel> Add(AddPurchaseViewModel addPurchaseViewModel)
        {
            var currentDealer = await _dealerApplication.GetByCpf(addPurchaseViewModel.Cpf);

            if(currentDealer == null)
            {
                await _mediator.PublishEvent(new ApplicationNotification($"Não foi encontrado nenhum revendedor com o cpf: {addPurchaseViewModel.Cpf}"));
                return null;
            }

            var purchase = currentDealer.IsAproved() ? new Purchase(currentDealer.Id, addPurchaseViewModel.Code, addPurchaseViewModel.Value, "Aprovado", addPurchaseViewModel.Date)
                                              : new Purchase(currentDealer.Id, addPurchaseViewModel.Code, addPurchaseViewModel.Value, "Em validação", addPurchaseViewModel.Date);

            if (!purchase.IsValid())
            {
                foreach (var error in purchase.ValidationResult.Errors)
                    await _mediator.PublishEvent(new ApplicationNotification(error.ErrorMessage));

                return null;
            }

            await _purchaseRepository.AddAsync(purchase);
            await base.Commit();

            return _mapper.Map<PurchaseViewModel>(purchase);
        }

        public async Task<IEnumerable<ListPurchaseViewModel>> ListPurchase()
        {
            var lstPurchase = await _purchaseRepository.GetAll();
            int percentageCashback;
            double valueCashback;

            var lstPurchaseViewModel = new List<ListPurchaseViewModel>();

            foreach (var item in lstPurchase)
            {
                (percentageCashback, valueCashback) = item.CalcCashback();

                var purchase = new ListPurchaseViewModel
                {
                    Code = item.Code,
                    Value = item.Value,
                    Date = item.Date,
                    PercentCashback = $"{percentageCashback}%",
                    ValueCashback = valueCashback,
                    Status = item.Status
                };
                lstPurchaseViewModel.Add(purchase);
            }
            return lstPurchaseViewModel;
        }
    }
}
