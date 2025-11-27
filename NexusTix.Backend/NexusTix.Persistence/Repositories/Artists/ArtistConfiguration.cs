using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusTix.Domain.Entities;

namespace NexusTix.Persistence.Repositories.Artists
{
    public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Bio).HasMaxLength(1000);
            builder.Property(a => a.ImageUrl).HasMaxLength(500);

            builder.HasMany(a => a.Events)
                .WithMany(e => e.Artists)
                .UsingEntity(j => j.ToTable("EventArtists"));
        }
    }
}
