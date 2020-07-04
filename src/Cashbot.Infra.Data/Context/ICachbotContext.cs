using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Cashbot.Infra.Data.Context
{
    public interface ICachbotContext : IDisposable
    {
        DatabaseFacade Database { get; }
        int SaveChanges();
    }
}
