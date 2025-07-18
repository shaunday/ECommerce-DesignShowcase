using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Balancer
{
    public class QueuedBalancer : ILoadBalancer
    {
        private readonly ConcurrentDictionary<string, Queue<string>> _serviceQueues = new();
        private readonly int _instanceCount;

        public QueuedBalancer(int instanceCount = 3)
        {
            _instanceCount = instanceCount;
        }

        public async Task<string> SelectInstanceAsync(string serviceName)
        {
            var queue = _serviceQueues.GetOrAdd(serviceName, _ =>
            {
                var q = new Queue<string>();
                for (int i = 0; i < _instanceCount; i++)
                    q.Enqueue($"{serviceName}_instance_{i}");
                return q;
            });

            // Simulate waiting if all instances are busy (queue empty)
            while (queue.Count == 0)
            {
                await Task.Delay(50); // Wait for an instance to become available
            }

            var instance = queue.Dequeue();
            queue.Enqueue(instance); // Put it back at the end
            return instance;
        }
    }
} 