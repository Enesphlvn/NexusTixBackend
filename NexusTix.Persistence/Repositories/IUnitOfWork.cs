using NexusTix.Persistence.Repositories.Cities;
using NexusTix.Persistence.Repositories.Districts;
using NexusTix.Persistence.Repositories.Events;
using NexusTix.Persistence.Repositories.EventTypes;
using NexusTix.Persistence.Repositories.Tickets;
using NexusTix.Persistence.Repositories.Users;
using NexusTix.Persistence.Repositories.Venues;

namespace NexusTix.Persistence.Repositories
{
    public interface IUnitOfWork
    {
        ICityRepository Cities { get; }
        IDistrictRepository Districts { get; }
        IEventRepository Events { get; }
        IEventTypeRepository EventTypes { get; }
        ITicketRepository Tickets { get; }
        IUserRepository Users { get; }
        IVenueRepository Venues { get; }

        Task<int> SaveChangesAsync();
    }
}
