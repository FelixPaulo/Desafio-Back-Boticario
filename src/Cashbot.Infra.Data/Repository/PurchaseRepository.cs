using Cashbot.Domain.Interfaces;
using Cashbot.Domain.Models;
using Cashbot.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cashbot.Infra.Data.Repository
{
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(CachbotContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Purchase>> GetAll()
        {
            //return await base._context.Purchases.ToListAsync();
            //return await _dbSet.AllAsync.ToListAsynca();
            return await _dbSet.ToListAsync();
        }
    }
}
