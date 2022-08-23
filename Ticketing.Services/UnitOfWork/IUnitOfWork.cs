using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Common;
using Ticketing.Data.Persistence;
using Ticketing.Services.Repository.Contracts;

namespace Ticketing.Services.UnitOfWork
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : TicketingDbContext
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;

        Task<int> Commit();
    }
}
