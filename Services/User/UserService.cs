using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;

namespace Services.User
{
    public class UserService : IUserService
    {
        public string Name => "UserService";

        public Task StartAsync()
        {
            Console.WriteLine($"{Name} started.");
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            Console.WriteLine($"{Name} stopped.");
            return Task.CompletedTask;
        }

        public Task<IEnumerable<User>> ListUsersAsync()
        {
            Console.WriteLine($"{Name}: Listing users.");
            return Task.FromResult<IEnumerable<User>>(new List<User>());
        }

        public Task<User?> GetUserByIdAsync(string id)
        {
            Console.WriteLine($"{Name}: Getting user by id: {id}");
            return Task.FromResult<User?>(null);
        }
    }
} 