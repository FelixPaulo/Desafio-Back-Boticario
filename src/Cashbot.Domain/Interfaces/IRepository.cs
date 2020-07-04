using Cashbot.Domain.Core.Models;
using System;
using System.Threading.Tasks;

namespace Cashbot.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity<TEntity>
    {
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        int SaveChanges();
    }
}
