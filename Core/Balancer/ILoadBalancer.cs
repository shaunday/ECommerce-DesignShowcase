using System.Threading.Tasks;

namespace Core.Balancer
{
    public interface ILoadBalancer
    {
        Task<string> SelectInstanceAsync(string serviceName);
    }

    public interface ILoadBalancerFactory
    {
        ILoadBalancer CreateBalancer(string balancerType);
    }
} 