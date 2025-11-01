using Microsoft.EntityFrameworkCore;
using NexusTix.Domain.Entities.Common;
using NexusTix.Persistence.Context;
using System.Linq.Expressions;

namespace NexusTix.Persistence.Repositories
{
    public class GenericRepository<T, TId> : IGenericRepository<T, TId> where T : class, IEntity<TId>
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<bool> AnyAsync(TId id)
        {
            return await _dbSet.AnyAsync(e => e.Id!.Equals(id));
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            return await _dbSet.AsNoTracking().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(TId id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task PassiveAsync(TId id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
            {
                return;
            }

            entity.IsActive = false;

            _dbSet.Update(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsNoTracking();
        }

        public IQueryable<T> WhereTracked(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
    }
}
