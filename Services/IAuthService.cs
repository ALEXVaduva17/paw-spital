using System.Security.Claims;
using PawSpital.Models;

namespace PawSpital.Services;

public interface IAuthService
{
    Task<(bool Success, string Error)> RegisterAsync(string fullName, string email, string password);
    Task<(bool Success, string Error)> LoginAsync(string email, string password);
    Task LogoutAsync();
    Task<ApplicationUser?> GetCurrentUserAsync(ClaimsPrincipal principal);
    Task<IList<string>> GetUserRolesAsync(ClaimsPrincipal principal);
    Task<(bool Success, string Error)> UpdateProfileImageAsync(ClaimsPrincipal principal, IFormFile image);
}
