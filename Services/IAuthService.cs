namespace PawSpital.Services;

public interface IAuthService
{
    (bool Success, string Error) Register(string fullName, string email, string password);
    bool ValidateCredentials(string email, string password, out string fullName, out string role);
}

