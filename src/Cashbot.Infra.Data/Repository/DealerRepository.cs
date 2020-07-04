using Cashbot.Domain.Interfaces;
using Cashbot.Domain.Models;
using Cashbot.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cashbot.Infra.Data.Repository
{
    public class DealerRepository : Repository<Dealer>, IDealerRepository
    {
        public DealerRepository(CachbotContext context) : base(context)
        {

        }

        public async Task<Dealer> GetByCpf(string cpf)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Cpf == cpf);
        }

        public async Task<Dealer> GetByEmailAndPassword(string email, string password)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }
    }
}
