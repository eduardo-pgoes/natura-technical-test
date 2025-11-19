using Microsoft.EntityFrameworkCore;
using Natura.TechnicalTest.Core.Entities;
using Natura.TechnicalTest.Core.Services;
using Natura.TechnicalTest.Infrastructure.Data;

namespace Natura.TechnicalTest.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AppDbContext _context;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor, AppDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public async Task<User?> GetCurrentUserAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            return null;
        }

        if (!httpContext.Request.Headers.TryGetValue("X-User-Id", out var userIdHeader))
        {
            return null;
        }

        if (!int.TryParse(userIdHeader.ToString(), out var userId))
        {
            return null;
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        return user;
    }
}
