using Microsoft.EntityFrameworkCore;
using Natura.TechnicalTest.Core.Entities;

namespace Natura.TechnicalTest.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(AppDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Users.AnyAsync())
        {
            return;
        }

        var users = new[]
        {
            new User
            {
                Name = "Admin",
                Email = "admin@naturalabs.io",
                Role = "admin",
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Name = "Joao das Neves",
                Email = "joao.neves@naturalabs.io",
                Role = "user",
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Name = "Maria das Couves",
                Email = "maria.couves@naturalabs.io",
                Role = "user",
                CreatedAt = DateTime.UtcNow
            },
        };

        context.Users.AddRange(users);
        await context.SaveChangesAsync();
    }
}
