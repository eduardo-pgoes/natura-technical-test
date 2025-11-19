using Microsoft.EntityFrameworkCore;
using Natura.TechnicalTest.Core.Entities;
using Natura.TechnicalTest.Infrastructure.Data;

namespace Natura.TechnicalTest.Features.Users;

public static class GetUsers
{
    public record Response(int Id, string Name, string Email, string Role, DateTime CreatedAt);

    public static async Task<IResult> HandleAsync(AppDbContext db)
    {
        var users = await db.Users
            .Select(u => new Response(u.Id, u.Name, u.Email, u.Role, u.CreatedAt))
            .ToListAsync();

        return Results.Ok(users);
    }
}
