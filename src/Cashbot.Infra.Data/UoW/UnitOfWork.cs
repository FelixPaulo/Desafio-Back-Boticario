using Cashbot.Domain.Interfaces;
using Cashbot.Infra.Data.Context;

namespace Cashbot.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CachbotContext _context;

        public UnitOfWork(CachbotContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
