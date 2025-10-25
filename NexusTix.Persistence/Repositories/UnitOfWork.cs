using NexusTix.Persistence.Context;
using NexusTix.Persistence.Repositories.Cities;
using NexusTix.Persistence.Repositories.Districts;
using NexusTix.Persistence.Repositories.Events;
using NexusTix.Persistence.Repositories.EventTypes;
using NexusTix.Persistence.Repositories.Tickets;
using NexusTix.Persistence.Repositories.Users;
using NexusTix.Persistence.Repositories.Venues;

namespace NexusTix.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ICityRepository? _cityRepository;
        private IDistrictRepository? _districtRepository;
        private IEventRepository? _eventRepository;
        private IEventTypeRepository? _eventTypeRepository;
        private ITicketRepository? _ticketRepository;
        private IUserRepository? _userRepository;
        private IVenueRepository? _venueRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICityRepository Cities
        {
            get
            {
                return _cityRepository ??= new CityRepository(_context);
            }
        }

        public IDistrictRepository Districts
        {
            get
            {
                return _districtRepository ??= new DistrictRepository(_context);
            }
        }

        public IEventRepository Events
        {
            get
            {
                return _eventRepository ??= new EventRepository(_context);
            }
        }

        public IEventTypeRepository EventTypes
        {
            get
            {
                return _eventTypeRepository ??= new EventTypeRepository(_context);
            }
        }

        public ITicketRepository Tickets
        {
            get
            {
                return _ticketRepository ??= new TicketRepository(_context);
            }
        }

        public IUserRepository Users
        {
            get
            {
                return _userRepository ??= new UserRepository(_context);
            }
        }

        public IVenueRepository Venues
        {
            get
            {
                return _venueRepository ??= new VenueRepository(_context);
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
