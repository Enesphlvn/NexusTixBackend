using NexusTix.Domain.Entities.Common;

namespace NexusTix.Domain.Entities
{
        public class District : BaseEntity<int>
        {
            public string Name { get; set; } = null!;
            public int CityId { get; set; }
            public City City { get; set; } = default!;
            public List<Venue> Venues { get; set; } = [];
        }
}
