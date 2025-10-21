using NexusTix.Domain.Entities.Common;

namespace NexusTix.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<T, int> GetRepository<T>() where T : BaseEntity<int>
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this); // Garbage Collector'a temizlendiğini bildiriyoruz
        }
    }
}
