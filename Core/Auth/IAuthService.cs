namespace Core.Auth
{
    public interface IAuthService
    {
        bool Authenticate(string token);
        bool Authorize(string userId, string action);
    }
} 