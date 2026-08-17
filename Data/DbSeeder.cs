using Microsoft.AspNetCore.Identity;
using Round_OP.Models;

namespace Round_OP.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(
            IServiceProvider services,
            IConfiguration configuration)
        {
            var roleManager =
                services.GetRequiredService<RoleManager<IdentityRole>>();

            var userManager =
                services.GetRequiredService<UserManager<ApplicationUser>>();

            const string roleName = "Admin";

            var adminEmail =
                configuration["AdminSeed:Email"];

            var adminPassword =
                configuration["AdminSeed:Password"];

            // Do not create an administrator if credentials
            // have not been explicitly configured.
            if (string.IsNullOrWhiteSpace(adminEmail) ||
                string.IsNullOrWhiteSpace(adminPassword))
            {
                return;
            }

            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var roleResult =
                    await roleManager.CreateAsync(
                        new IdentityRole(roleName));

                if (!roleResult.Succeeded)
                {
                    throw new Exception(
                        $"Failed to create Admin role: " +
                        $"{string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                }
            }

            var adminUser =
                await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FullName = "System Administrator"
                };

                var userResult =
                    await userManager.CreateAsync(
                        adminUser,
                        adminPassword);

                if (!userResult.Succeeded)
                {
                    throw new Exception(
                        $"Failed to create admin user: " +
                        $"{string.Join(", ", userResult.Errors.Select(e => e.Description))}");
                }
            }

            if (!await userManager.IsInRoleAsync(
                    adminUser,
                    roleName))
            {
                var roleResult =
                    await userManager.AddToRoleAsync(
                        adminUser,
                        roleName);

                if (!roleResult.Succeeded)
                {
                    throw new Exception(
                        $"Failed to assign Admin role to user: " +
                        $"{string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}