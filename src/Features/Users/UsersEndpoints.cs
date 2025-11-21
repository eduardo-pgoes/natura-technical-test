using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Natura.TechnicalTest.Core.Services;
using Natura.TechnicalTest.Core.Entities;
using Natura.TechnicalTest.Infrastructure.Data;
using Natura.TechnicalTest.Features.Users;
namespace Natura.TechnicalTest.Features.Users;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users")
            .WithTags("Users");

            //Existing Endpoints

        group.MapGet("", GetUsers.HandleAsync)
            .WithName("GetUsers")
            .WithOpenApi();

        group.MapGet("/me", GetMe.HandleAsync)
            .WithName("GetCurrentUser")
            .WithOpenApi();

        // New Endpoint
        group.MapPost("{id}/elevate", Elevate)
            .WithName("ElevateUserRole")
            .WithOpenApi();

        return app;
    }

    // Elevate logic
  public static async Task<IResult> Elevate(
        int id, 
        AppDbContext context, 
        ICurrentUserService currentUserService)
    {
        // Authorization
        var executingUser = await currentUserService.GetCurrentUserAsync();

        if (executingUser == null)
        {
            // Return 401 Unauthorized manually to avoid issues if auth scheme is missing

            return Results.StatusCode(401); 
        }

        if (executingUser.Role != "admin")
        {
            // Return 403 Forbidden manually
            return Results.StatusCode(403); 
        }


        if (executingUser.Id == id)
        {
            return Results.BadRequest(new { Message = "You cannot promote yourself." });
        }

        // FETCH TARGET USER
        var targetUser = await context.Users.FindAsync(id);

        if (targetUser is null)
        {
            return Results.NotFound(new { Message = $"User with ID {id} not found." });
        }

        if (targetUser.Role == "admin")
        {
            return Results.BadRequest(new { Message = "Target user is already an admin." });
        }
        
        // UPDATE AND SAVE
        targetUser.Role = "admin";
        await context.SaveChangesAsync();

        return Results.Ok(targetUser);
    }
}