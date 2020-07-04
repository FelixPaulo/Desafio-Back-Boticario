using AutoMapper;
using Cashbot.Application.ViewModels.Dealer;
using Cashbot.Application.ViewModels.Purchase;
using Cashbot.Domain.Models;

namespace Cashbot.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Dealer, DealerViewModel>();
            CreateMap<Purchase, PurchaseViewModel>();
        }
    }
}
