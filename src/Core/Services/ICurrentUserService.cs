using Natura.TechnicalTest.Core.Entities;

namespace Natura.TechnicalTest.Core.Services;

public interface ICurrentUserService
{
    /// <summary>
    /// Retrieves the current user from the X-User-Id header.
    /// Returns null if the header is missing, invalid, or user not found.
    /// </summary>
    Task<User?> GetCurrentUserAsync();
}
