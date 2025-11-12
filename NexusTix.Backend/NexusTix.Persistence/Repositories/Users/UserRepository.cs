using Microsoft.EntityFrameworkCore;
using NexusTix.Domain.Entities;
using NexusTix.Persistence.Context;

namespace NexusTix.Persistence.Repositories.Users
{
    public class UserRepository : GenericRepository<User, int>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User?> GetUserAggregateAsync(int id)
        {
            return await _context.Users
                .Include(x => x.Tickets).ThenInclude(x => x.Event).ThenInclude(x => x.EventType)
                .Include(x => x.Tickets).ThenInclude(x => x.Event).ThenInclude(x => x.Venue).ThenInclude(x => x.District).ThenInclude(x => x.City)
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsersAggregateAsync()
        {
            return await _context.Users
                .Include(x => x.Tickets).ThenInclude(x => x.Event).ThenInclude(x => x.EventType)
                .Include(x => x.Tickets).ThenInclude(x => x.Event).ThenInclude(x => x.Venue).ThenInclude(x => x.District).ThenInclude(x => x.City)
                .AsNoTracking().ToListAsync();
        }
    }
}
