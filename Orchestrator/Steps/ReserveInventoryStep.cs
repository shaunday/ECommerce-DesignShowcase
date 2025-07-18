using System;
using System.Threading.Tasks;
using Core.Balancer;
using Core.ServiceBus;
using Core.Workflow;
using Orchestrator;

namespace Orchestrator.Steps
{
    public class ReserveInventoryStep : Orchestrator.IInfrastructureAwareStep
    {
        private ILoadBalancer _balancer;
        private IServiceBus _serviceBus;

        public void SetInfrastructure(ILoadBalancer balancer, IServiceBus serviceBus)
        {
            _balancer = balancer;
            _serviceBus = serviceBus;
        }

        public async Task ExecuteAsync(object data)
        {
            Console.WriteLine("ReserveInventoryStep: Executing");
            if (_balancer != null && _serviceBus != null)
            {
                var instance = await _balancer.SelectInstanceAsync("InventoryService");
                await _serviceBus.SendAsync<object, object>(instance, new { Action = "Reserve", Data = data });
                Console.WriteLine($"ReserveInventoryStep: Reserved inventory via {instance}");
            }
            else
            {
                Console.WriteLine("ReserveInventoryStep: No infrastructure, simulated execution");
            }
        }

        public async Task CompensateAsync(object data)
        {
            Console.WriteLine("ReserveInventoryStep: Compensating (releasing inventory)");
            if (_balancer != null && _serviceBus != null)
            {
                var instance = await _balancer.SelectInstanceAsync("InventoryService");
                await _serviceBus.SendAsync<object, object>(instance, new { Action = "Release", Data = data });
                Console.WriteLine($"ReserveInventoryStep: Released inventory via {instance}");
            }
            else
            {
                Console.WriteLine("ReserveInventoryStep: No infrastructure, simulated compensation");
            }
        }
    }
} 