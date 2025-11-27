using NexusTix.Domain.Entities;
using NexusTix.Persistence.Context;
using System.Linq.Expressions;

namespace NexusTix.Persistence.Repositories.Artists
{
    public class ArtistRepository : GenericRepository<Artist, int>, IArtistRepository
    {
        public ArtistRepository(AppDbContext context) : base(context)
        {
            
        }

        public Task AddAsync(Artist entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<Artist, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Delete(Artist entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Artist>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Artist>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Artist?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task PassiveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Artist entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Artist> Where(Expression<Func<Artist, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Artist> WhereTracked(Expression<Func<Artist, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
