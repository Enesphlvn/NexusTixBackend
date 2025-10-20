using Microsoft.AspNetCore.Identity;

namespace NexusTix.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public List<Ticket> Tickets { get; set; } = [];
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
