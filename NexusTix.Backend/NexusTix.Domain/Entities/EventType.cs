using NexusTix.Domain.Entities.Common;

namespace NexusTix.Domain.Entities
{
    public class EventType : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public List<Event> Events { get; set; } = [];
    }
}
