using NexusTix.Domain.Entities.Common;

namespace NexusTix.Domain.Entities
{
    public class Ticket : BaseEntity<int>
    {
        public int EventId { get; set; }
        public Event Event { get; set; } = default!;
        public int UserId { get; set; }
        public User User { get; set; } = default!;
        public Guid QRCodeGuid { get; set; }
        public DateTimeOffset PurchaseDate { get; set; }
        public bool IsUsed { get; set; }
        public bool IsCancelled { get; set; } = false;
    }
}