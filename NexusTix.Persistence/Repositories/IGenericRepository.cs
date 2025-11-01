using NexusTix.Domain.Entities.Common;
using System.Linq.Expressions;

namespace NexusTix.Persistence.Repositories
{
    public interface IGenericRepository<T, TId> where T : class, IEntity<TId>
    {
        Task<bool> AnyAsync(TId id);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllPagedAsync(int pageNumber, int pageSize);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        IQueryable<T> WhereTracked(Expression<Func<T, bool>> predicate);
        Task<T?> GetByIdAsync(TId id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task PassiveAsync(TId id);
    }
}
