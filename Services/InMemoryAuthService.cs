using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using PawSpital.Security;

namespace PawSpital.Services;

public sealed class InMemoryAuthService : IAuthService
{
    private static readonly ConcurrentDictionary<string, AuthUser> Users = new(StringComparer.OrdinalIgnoreCase);
    private static bool _seeded;

    public (bool Success, string Error) Register(string fullName, string email, string password)
    {
        EnsureSeedData();
        var normalizedEmail = email.Trim().ToLowerInvariant();
        if (Users.ContainsKey(normalizedEmail))
            return (false, "Există deja un cont cu acest email.");

        var user = new AuthUser(fullName.Trim(), normalizedEmail, Hash(password), AppRoles.Pacient);
        if (!Users.TryAdd(normalizedEmail, user))
            return (false, "Înregistrarea a eșuat. Încearcă din nou.");

        return (true, string.Empty);
    }

    public bool ValidateCredentials(string email, string password, out string fullName, out string role)
    {
        EnsureSeedData();
        var normalizedEmail = email.Trim().ToLowerInvariant();
        fullName = string.Empty;
        role = string.Empty;

        if (!Users.TryGetValue(normalizedEmail, out var user))
            return false;

        if (!string.Equals(user.PasswordHash, Hash(password), StringComparison.Ordinal))
            return false;

        fullName = user.FullName;
        role = user.Role;
        return true;
    }

    private static void EnsureSeedData()
    {
        if (_seeded)
            return;

        var admin = new AuthUser("Administrator SanaMed", "admin@sanamed.ro", Hash("Admin123!"), AppRoles.Admin);
        Users.TryAdd(admin.Email, admin);
        _seeded = true;
    }

    private static string Hash(string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = SHA256.HashData(bytes);
        return Convert.ToHexString(hashBytes);
    }

    private sealed record AuthUser(string FullName, string Email, string PasswordHash, string Role);
}

