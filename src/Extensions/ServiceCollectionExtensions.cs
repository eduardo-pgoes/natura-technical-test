using Microsoft.EntityFrameworkCore;
using Natura.TechnicalTest.Core.Services;
using Natura.TechnicalTest.Infrastructure.Data;
using Natura.TechnicalTest.Infrastructure.Services;

namespace Natura.TechnicalTest.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = BuildConnectionString(configuration);

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }

    private static string BuildConnectionString(IConfiguration configuration)
    {
        // Try to get full connection string from config first (for backwards compatibility)
        var existingConnectionString = configuration.GetConnectionString("DefaultConnection");
        if (!string.IsNullOrWhiteSpace(existingConnectionString))
        {
            return existingConnectionString;
        }

        // Build from environment variables (matches docker-compose.yml/.env)
        var host = configuration["POSTGRES_HOST"] ?? "localhost";
        var port = configuration["POSTGRES_PORT"] ?? "5432";
        var database = configuration["POSTGRES_DB"]
            ?? throw new InvalidOperationException("POSTGRES_DB environment variable not found.");
        var username = configuration["POSTGRES_USER"]
            ?? throw new InvalidOperationException("POSTGRES_USER environment variable not found.");
        var password = configuration["POSTGRES_PASSWORD"]
            ?? throw new InvalidOperationException("POSTGRES_PASSWORD environment variable not found.");

        return $"Host={host};Port={port};Database={database};Username={username};Password={password}";
    }
}
