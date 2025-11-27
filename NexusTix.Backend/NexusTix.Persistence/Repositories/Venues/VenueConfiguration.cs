using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusTix.Domain.Entities;

namespace NexusTix.Persistence.Repositories.Venues
{
    public class VenueConfiguration : IEntityTypeConfiguration<Venue>
    {
        public void Configure(EntityTypeBuilder<Venue> builder)
        {
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Name).IsRequired().HasMaxLength(150);
            builder.Property(v => v.Capacity).IsRequired();
            builder.Property(v => v.Latitude).IsRequired();
            builder.Property(v => v.Longitude).IsRequired();

            builder.HasOne(v => v.District)
                .WithMany(d => d.Venues)
                .HasForeignKey(v => v.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(v => v.Events)
                .WithOne(e => e.Venue)
                .HasForeignKey(e => e.VenueId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
