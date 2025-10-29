using Microsoft.EntityFrameworkCore;
using NexusTix.Domain.Entities;
using NexusTix.Persistence.Context;

namespace NexusTix.Persistence.Repositories.Users
{
    public class UserRepository : GenericRepository<User, int>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetUserAggregateAsync(int id)
        {
            return await _context.Users
                .Include(x => x.Tickets).ThenInclude(x => x.Event).ThenInclude(x => x.EventType)
                .Include(x => x.Tickets).ThenInclude(x => x.Event).ThenInclude(x => x.Venue).ThenInclude(x => x.District).ThenInclude(x => x.City)
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetuserbyEmailAsync(string email)
        {
            return (await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email))!;
        }

        public async Task<IEnumerable<User>> GetUsersAggregateAsync()
        {
            return await _context.Users
                .Include(x => x.Tickets).ThenInclude(x => x.Event).ThenInclude(x => x.EventType)
                .Include(x => x.Tickets).ThenInclude(x => x.Event).ThenInclude(x => x.Venue).ThenInclude(x => x.District).ThenInclude(x => x.City)
                .AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersWithTicketsAsync()
        {
            return await _context.Users.Include(x => x.Tickets).AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetUserWithTicketsAsync(int id)
        {
            return await _context.Users.Include(x => x.Tickets).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
