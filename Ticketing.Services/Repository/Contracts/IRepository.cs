using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Common;

namespace Ticketing.Services.Repository.Contracts
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> Query();

        Task<ICollection<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> AddAsync(TEntity entity);
        Task AddRangeAsync(ICollection<TEntity> entities);
        Task<TEntity> UpdateAsync(TEntity entity);

        Task<int> DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(ICollection<TEntity> entities);
        Task<int> DeleteAsync(int id);
    }
}
