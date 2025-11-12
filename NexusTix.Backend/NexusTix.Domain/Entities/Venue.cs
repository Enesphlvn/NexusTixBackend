using NexusTix.Domain.Entities.Common;

namespace NexusTix.Domain.Entities
{
    public class Venue : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; } = default!;
        public List<Event> Events { get; set; } = [];
    }
}
