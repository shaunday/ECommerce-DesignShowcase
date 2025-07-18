namespace Core.Auth
{
    public class StubAuthService : IAuthService
    {
        public bool Authenticate(string token) => true;
        public bool Authorize(string userId, string action) => true;
    }
} 