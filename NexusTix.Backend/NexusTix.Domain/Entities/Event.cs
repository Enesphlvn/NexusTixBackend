using NexusTix.Domain.Entities.Common;

namespace NexusTix.Domain.Entities
{
    public class Event : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public DateTimeOffset Date { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public int EventTypeId { get; set; }
        public EventType EventType { get; set; } = default!;
        public int VenueId { get; set; }
        public Venue Venue { get; set; } = default!;
        public List<Ticket> Tickets { get; set; } = [];
        public List<Artist> Artists { get; set; } = [];
    }
}
