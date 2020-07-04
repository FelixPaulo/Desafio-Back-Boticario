using AutoMapper;
using Cashbot.Application.Interfaces;
using Cashbot.Application.Notifications;
using Cashbot.Application.ViewModels.Dealer;
using Cashbot.Domain.Interfaces;
using Cashbot.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Cashbot.Application.Services
{
    public class DealerApplication : BaseApplication, IDealerApplication
    {

        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;
        private readonly IDealerRepository _dealerRepository;

        public DealerApplication(IMapper mapper,
                                IUnitOfWork uow,
                                IMediatorHandler mediator,
                                IHttpContextAccessor httpContextAccessor,
                                IDealerRepository dealerRepository,
                                INotificationHandler<ApplicationNotification> notifications) : base(uow, mediator, httpContextAccessor, notifications)
        {
            _mapper = mapper;
            _mediator = mediator;
            _dealerRepository = dealerRepository;
        }


        public async Task<DealerViewModel> Add(AddDealerViewModel addDealerViewModel)
        {
            var dealer = new Dealer(addDealerViewModel.Name, addDealerViewModel.Cpf, addDealerViewModel.Email, addDealerViewModel.Password);

            if (!dealer.IsValid())
            {
                foreach (var error in dealer.ValidationResult.Errors)
                    await _mediator.PublishEvent(new ApplicationNotification(error.ErrorMessage));

                return null;
            }

            await _dealerRepository.AddAsync(dealer);
            await base.Commit();

            return _mapper.Map<DealerViewModel>(dealer);
        }

        public Task<Dealer> GetByCpf(string cpf) => _dealerRepository.GetByCpf(cpf);

        public Task<Dealer> GetByEmailAndPassword(string email, string password) => _dealerRepository.GetByEmailAndPassword(email, password);
    }
}
