using NexusTix.Domain.Entities;

namespace NexusTix.Persistence.Repositories.Users
{
    public interface IUserRepository : IGenericRepository<User, int>
    {
        Task<User?> GetUserAggregateAsync(int id);
        Task<IEnumerable<User>> GetUsersAggregateAsync();
    }
}
