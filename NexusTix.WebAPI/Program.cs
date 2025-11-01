using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NexusTix.Application.Extensions;
using NexusTix.Domain.Entities;
using NexusTix.Persistence.Context;
using NexusTix.Persistence.Extensions;
using NexusTix.Persistence.Seed;

namespace NexusTix.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // -- Persistence katmaný kayýtlarý --
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddRepositoryServices();

            // -- Application katmaný kayýtlarý --
            builder.Services.AddApplicationServices();

            // -- API katmaný kayýtlarý --
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

            app.Run();
        }
    }
}
