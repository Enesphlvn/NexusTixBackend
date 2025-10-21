using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusTix.Domain.Entities;

namespace NexusTix.Persistence.Repositories.Districts
{
    public class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(x => x.Venues)
                .WithOne(v => v.District)
                .HasForeignKey(v => v.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
