using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PawSpital.Models;
using PawSpital.Security;

namespace PawSpital.Services;

public sealed class IdentityAuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IWebHostEnvironment _env;

    public IdentityAuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IWebHostEnvironment env)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _env = env;
    }

    public async Task<(bool Success, string Error)> RegisterAsync(string fullName, string email, string password)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();
        var existingUser = await _userManager.FindByEmailAsync(normalizedEmail);
        if (existingUser != null)
            return (false, "Există deja un cont cu acest email.");

        var user = new ApplicationUser
        {
            UserName = normalizedEmail,
            Email = normalizedEmail,
            FullName = fullName.Trim()
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            return (false, string.Join(" ", result.Errors.Select(e => e.Description)));

        await _userManager.AddToRoleAsync(user, AppRoles.Pacient);
        return (true, string.Empty);
    }

    public async Task<(bool Success, string Error)> LoginAsync(string email, string password)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();
        var user = await _userManager.FindByEmailAsync(normalizedEmail);
        if (user == null)
            return (false, "Email sau parolă invalidă.");

        var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);
        if (!result.Succeeded)
            return (false, "Email sau parolă invalidă.");

        return (true, string.Empty);
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<ApplicationUser?> GetCurrentUserAsync(ClaimsPrincipal principal)
    {
        return await _userManager.GetUserAsync(principal);
    }

    public async Task<IList<string>> GetUserRolesAsync(ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal);
        if (user == null) return new List<string>();
        return await _userManager.GetRolesAsync(user);
    }

    public async Task<(bool Success, string Error)> UpdateProfileImageAsync(ClaimsPrincipal principal, IFormFile image)
    {
        var user = await _userManager.GetUserAsync(principal);
        if (user == null)
            return (false, "Utilizatorul nu a fost găsit.");

        if (image == null || image.Length == 0)
            return (false, "Selectați o imagine.");

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        var extension = Path.GetExtension(image.FileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(extension))
            return (false, "Format de imagine neacceptat. Folosiți JPG, PNG, GIF sau WebP.");

        if (image.Length > 5 * 1024 * 1024)
            return (false, "Imaginea nu poate depăși 5MB.");

        var uploadsDir = Path.Combine(_env.WebRootPath, "uploads", "profiles");
        Directory.CreateDirectory(uploadsDir);

        // Delete old image if exists
        if (!string.IsNullOrEmpty(user.ProfileImagePath))
        {
            var oldPath = Path.Combine(_env.WebRootPath, user.ProfileImagePath.TrimStart('/'));
            if (File.Exists(oldPath))
                File.Delete(oldPath);
        }

        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(uploadsDir, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }

        user.ProfileImagePath = $"/uploads/profiles/{fileName}";
        await _userManager.UpdateAsync(user);
        await _signInManager.RefreshSignInAsync(user);

        return (true, string.Empty);
    }
}
