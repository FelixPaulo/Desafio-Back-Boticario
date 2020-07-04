using Cashbot.Application.ViewModels.Purchase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cashbot.Application.Interfaces
{
    public interface IPurchaseApplication
    {
        Task<PurchaseViewModel> Add(AddPurchaseViewModel addPurchaseViewModel);

        Task<IEnumerable<ListPurchaseViewModel>> ListPurchase();
    }
}
