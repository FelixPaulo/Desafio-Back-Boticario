using Cashbot.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cashbot.Domain.Interfaces
{
    public interface IPurchaseRepository : IRepository<Purchase>
    {
        Task<IEnumerable<Purchase>> GetAll();
    }
}
