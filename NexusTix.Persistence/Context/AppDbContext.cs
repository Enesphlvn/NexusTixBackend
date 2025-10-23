using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NexusTix.Domain.Entities;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);

            builder.Entity<City>().HasQueryFilter(e => e.IsActive);
            builder.Entity<District>().HasQueryFilter(e => e.IsActive);
            builder.Entity<EventType>().HasQueryFilter(e => e.IsActive);
            builder.Entity<Venue>().HasQueryFilter(e => e.IsActive);
            builder.Entity<Event>().HasQueryFilter(e => e.IsActive);
            builder.Entity<Ticket>().HasQueryFilter(e => e.IsActive);
        }
    }
}
