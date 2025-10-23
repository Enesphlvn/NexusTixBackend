using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusTix.Domain.Entities;

namespace NexusTix.Persistence.Repositories.Tickets
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => t.QRCodeGuid).IsUnique();
            builder.Property(t => t.PurchaseDate).IsRequired();
            builder.Property(t => t.IsUsed).IsRequired();

            builder.Property(t => t.EventId).IsRequired();

            builder.HasOne(t => t.User)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
