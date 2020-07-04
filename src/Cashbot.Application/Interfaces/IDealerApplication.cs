using Cashbot.Application.ViewModels.Dealer;
using Cashbot.Domain.Models;
using System.Threading.Tasks;

namespace Cashbot.Application.Interfaces
{
    public interface IDealerApplication
    {
        Task<DealerViewModel> Add(AddDealerViewModel addDealerViewModel);
        Task<Dealer> GetByCpf(string cpf);
        Task<Dealer> GetByEmailAndPassword(string email, string password);
    }
}
