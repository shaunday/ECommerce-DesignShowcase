using System;
using System.Threading.Tasks;
using Core.Balancer;
using Core.ServiceBus;
using Core.Workflow;
using Orchestrator;

namespace Orchestrator.Steps
{
    public class ProcessPaymentStep : Orchestrator.IInfrastructureAwareStep
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
            Console.WriteLine("ProcessPaymentStep: Executing");
            if (_balancer != null && _serviceBus != null)
            {
                var instance = await _balancer.SelectInstanceAsync("PaymentService");
                await _serviceBus.SendAsync<object, object>(instance, new { Action = "Pay", Data = data });
                Console.WriteLine($"ProcessPaymentStep: Payment processed via {instance}");
            }
            else
            {
                Console.WriteLine("ProcessPaymentStep: No infrastructure, simulated execution");
            }
        }

        public async Task CompensateAsync(object data)
        {
            Console.WriteLine("ProcessPaymentStep: Compensating (refund payment)");
            if (_balancer != null && _serviceBus != null)
            {
                var instance = await _balancer.SelectInstanceAsync("PaymentService");
                await _serviceBus.SendAsync<object, object>(instance, new { Action = "Refund", Data = data });
                Console.WriteLine($"ProcessPaymentStep: Payment refunded via {instance}");
            }
            else
            {
                Console.WriteLine("ProcessPaymentStep: No infrastructure, simulated compensation");
            }
        }
    }
} 