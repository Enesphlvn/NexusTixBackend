using NexusTix.Domain.Entities.Common;

namespace NexusTix.Domain.Entities
{
    public class City : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public List<District> Districts { get; set; } = [];
    }
}
