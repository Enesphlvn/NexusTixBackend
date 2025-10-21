using NexusTix.Domain.Entities.Common;

namespace NexusTix.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T, int> GetRepository<T>() where T : BaseEntity<int>;
        Task<int> SaveChangesAsync();
    }
}
