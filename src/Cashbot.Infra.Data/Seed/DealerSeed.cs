using Cashbot.Domain.Models;
using Cashbot.Infra.Data.Context;
using System.Linq;

namespace Cashbot.Infra.Data.Seed
{
    public class DealerSeed
    {
        private CachbotContext _context;
        public DealerSeed(CachbotContext context)
        {
            _context = context;
        }

        public void SeedDealer()
        {
            var currentDealer = _context.Dealers.FirstOrDefault(x => x.Id == 1);

            if (currentDealer == null)
            {
                var dealer = new Dealer(1, "master", "000.000.000-00", "master@grupoboticario.com.br", "teste@123");
                _context.Dealers.AddAsync(dealer);
                _context.SaveChangesAsync();
            }
        }
    }
}
