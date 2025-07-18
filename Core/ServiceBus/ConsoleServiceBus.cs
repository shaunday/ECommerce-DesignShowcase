using System;
using System.Threading.Tasks;

namespace Core.ServiceBus
{
    public class ConsoleServiceBus : IServiceBus
    {
        public Task<TResponse?> SendAsync<TRequest, TResponse>(string serviceName, TRequest request)
        {
            Console.WriteLine($"[ServiceBus] Sending request to '{serviceName}': {request}");
            // Return default dummy data
            return Task.FromResult<TResponse?>(default);
        }
    }
} 