using CRM.Domain.Entities;
using CRM.Domain.Enums;
using BCrypt.Net;

namespace CRM.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // ===== STATUSES =====
            if (!context.Statuses.Any())
            {
                context.Statuses.AddRange(
                    new Status { Title = "New", OrderIndex = 1 },
                    new Status { Title = "In Progress", OrderIndex = 2 },
                    new Status { Title = "Closed", OrderIndex = 3 }
                );

                await context.SaveChangesAsync();
            }

            // ===== ADMIN =====
            if (!context.Users.Any(u => u.Role == Role.Admin))
            {
                var admin = new User
                {
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    Role = Role.Admin
                };

                context.Users.Add(admin);
                await context.SaveChangesAsync();
            }
        }
    }
}