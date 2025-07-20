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

        public async Task ExecuteAsync(Core.Workflow.WorkflowContext context)
        {
            Console.WriteLine("ProcessPaymentStep: Executing");
            var data = context.Data;
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

        public async Task CompensateAsync(Core.Workflow.WorkflowContext context)
        {
            Console.WriteLine("ProcessPaymentStep: Compensating (refund payment)");
            var data = context.Data;
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