using Cashbot.Domain.Models;
using System.Threading.Tasks;

namespace Cashbot.Domain.Interfaces
{
    public interface IDealerRepository : IRepository<Dealer>
    {
        Task<Dealer> GetByCpf(string cpf);
        Task<Dealer> GetByEmailAndPassword(string email, string password);
    }
}
