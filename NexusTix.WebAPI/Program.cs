using NexusTix.Application.Extensions;
using NexusTix.Persistence.Extensions;
using NexusTix.Persistence.Seed;
using NexusTix.WebAPI.Extensions;

namespace NexusTix.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Identity Kaydý
            builder.Services.AddIdentityServices(builder.Configuration);

            // Jwt Kaydý
            builder.Services.AddJwtAuthentication(builder.Configuration);

            // -- Persistence katmaný kayýtlarý --
            builder.Services.AddRepositoryServices();

            // -- Application katmaný kayýtlarý --
            builder.Services.AddApplicationServices();

            // -- API katmaný kayýtlarý --
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            await IdentitySeeder.SeedRolesAndSuperAdminAsync(app.Services);
            await DataSeeder.SeedDataAsync(app.Services);

            app.Run();
        }
    }
}