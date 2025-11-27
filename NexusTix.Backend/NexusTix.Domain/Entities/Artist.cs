using NexusTix.Domain.Entities.Common;

namespace NexusTix.Domain.Entities
{
    public class Artist : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string? Bio { get; set; }
        public string? ImageUrl { get; set; }
        public List<Event> Events { get; set; } = [];
    }
}
