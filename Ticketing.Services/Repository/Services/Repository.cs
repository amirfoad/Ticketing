using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Common;
using Ticketing.Data.Persistence;
using Ticketing.Services.Repository.Contracts;
using Ticketing.Services.UnitOfWork;

namespace Ticketing.Services.Repository.Services
{
    public class Repository<TEntity, TContext> : IRepository<TEntity> where TEntity : class, IEntity where TContext : TicketingDbContext
    {
        private readonly TContext _context;
        private readonly IUnitOfWork<TContext> _unitOfWork;

        public Repository(TContext context, IUnitOfWork<TContext> unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);

            await _unitOfWork.Commit();

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            await _unitOfWork.Commit();

            return entity;
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);

            return await _unitOfWork.Commit();
        }

        public async Task AddRangeAsync(ICollection<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            await _unitOfWork.Commit();


        }

        public async Task<int> DeleteAsync(int id)
        {
            var entityToDelete = await _context.Set<TEntity>().FindAsync(id);
            await DeleteAsync(entityToDelete);

            return await _unitOfWork.Commit();

        }

        public async Task DeleteRangeAsync(ICollection<TEntity> entities)
        {
             _context.Set<TEntity>().RemoveRange(entities);
            await _unitOfWork.Commit();
        }
    }
}
