using System.Threading.Tasks;

namespace Core.ServiceBus
{
    public interface IServiceBus
    {
        Task<TResponse?> SendAsync<TRequest, TResponse>(string serviceName, TRequest request);
    }
} 