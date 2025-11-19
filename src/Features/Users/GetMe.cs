using Microsoft.AspNetCore.Http.HttpResults;
using Natura.TechnicalTest.Core.Entities;
using Natura.TechnicalTest.Core.Services;

namespace Natura.TechnicalTest.Features.Users;

public static class GetMe
{
    public static async Task<Results<Ok<User>, UnauthorizedHttpResult>> HandleAsync(
        ICurrentUserService currentUserService)
    {
        var currentUser = await currentUserService.GetCurrentUserAsync();

        if (currentUser == null)
        {
            return TypedResults.Unauthorized();
        }

        return TypedResults.Ok(currentUser);
    }
}
