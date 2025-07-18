using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.User
{
    public interface IUserService : IService
    {
        Task<IEnumerable<User>> ListUsersAsync();
        Task<User?> GetUserByIdAsync(string id);
    }

    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
} 