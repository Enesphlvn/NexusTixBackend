using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NexusTix.Domain.Entities;

namespace NexusTix.Persistence.Seed
{
    public static class IdentitySeeder
    {
        private const string SuperAdminEmail = "admin@admin.com";
        private const string SuperAdminPassword = "AdminPassword.123";
        private const string SuperAdminRole = "Admin";

        public static async Task SeedRolesAndSuperAdminAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            // Rol oluşturma
            string[] roleNames = { SuperAdminRole, "Manager", "User" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(roleName));
                }
            }

            // Süper yönetici oluşturma
            var superAdmin = await userManager.FindByEmailAsync(SuperAdminEmail);

            if (superAdmin == null)
            {
                var newAdmin = new User
                {
                    UserName = "SuperAdmin",
                    Email = SuperAdminEmail,
                    FirstName = "Super",
                    LastName = "Admin",
                    EmailConfirmed = true,
                };

                var createResult = await userManager.CreateAsync(newAdmin, SuperAdminPassword);

                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, SuperAdminRole);
                }
            }
        }
    }
}
