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
            if (!_context.Dealers.Any())
            {
                var dealer = new Dealer("master", "000.000.000-00", "master@grupoboticario.com.br", "teste@123");
                _context.Dealers.Add(dealer);
                _context.SaveChanges();

            }
        }
    }
}
