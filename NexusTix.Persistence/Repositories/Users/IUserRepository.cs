using NexusTix.Domain.Entities;

namespace NexusTix.Persistence.Repositories.Users
{
    public interface IUserRepository : IGenericRepository<User, int>
    {
        Task<User?> GetUserWithTicketsAsync(int id);
        Task<IEnumerable<User>> GetUsersWithTicketsAsync();
        Task<User?> GetUserAggregateAsync(int id);
        Task<IEnumerable<User>> GetUsersAggregateAsync();
    }
}
