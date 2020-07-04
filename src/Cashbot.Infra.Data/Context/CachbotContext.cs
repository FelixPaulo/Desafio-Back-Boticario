using Cashbot.Domain.Models;
using Cashbot.Infra.Data.Mappings;
using Cashbot.Infra.Data.Seed;
using Microsoft.EntityFrameworkCore;

namespace Cashbot.Infra.Data.Context
{
    public class CachbotContext : DbContext, ICachbotContext
    {
        public CachbotContext(DbContextOptions<CachbotContext> options) : base(options)
        {

        }

        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DealerMapping());
            modelBuilder.ApplyConfiguration(new PurchaseMapping());
        }

        public virtual int SaveChanges(string userName, int companyId)
        {
            var result = SaveChanges();
            return result;
        }
    }
}
