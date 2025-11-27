using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NexusTix.Domain.Entities;
using NexusTix.Domain.Entities.Common;
using System.Reflection;

namespace NexusTix.Persistence.Context
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Artist> Artists { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(x =>
                x.Entity is IEntity<int> && (x.State == EntityState.Added || x.State == EntityState.Modified)
            );

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("Created").CurrentValue = DateTimeOffset.UtcNow;
                }

                else if (entry.State == EntityState.Modified)
                {
                    entry.Property("Updated").CurrentValue = DateTimeOffset.UtcNow;
                    entry.Property("Created").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<User>().HasQueryFilter(e => e.IsActive);
            builder.Entity<City>().HasQueryFilter(e => e.IsActive);
            builder.Entity<District>().HasQueryFilter(e => e.IsActive);
            builder.Entity<EventType>().HasQueryFilter(e => e.IsActive);
            builder.Entity<Venue>().HasQueryFilter(e => e.IsActive);
            builder.Entity<Event>().HasQueryFilter(e => e.IsActive);
            builder.Entity<Ticket>().HasQueryFilter(e => e.IsActive);
            builder.Entity<Artist>().HasQueryFilter(e => e.IsActive);
        }
    }
}
