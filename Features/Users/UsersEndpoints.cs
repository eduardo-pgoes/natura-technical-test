namespace Natura.TechnicalTest.Features.Users;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users")
            .WithTags("Users");

        group.MapGet("", GetUsers.HandleAsync)
            .WithName("GetUsers")
            .WithOpenApi();

        return app;
    }
}
