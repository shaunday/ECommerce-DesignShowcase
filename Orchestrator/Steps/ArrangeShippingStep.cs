using System;
using System.Threading.Tasks;
using Core.Balancer;
using Core.ServiceBus;
using Core.Workflow;
using Orchestrator;

namespace Orchestrator.Steps
{
    public class ArrangeShippingStep : Core.Workflow.ICompensatableStep
    {
        public async Task ExecuteAsync(object data)
        {
            Console.WriteLine("ArrangeShippingStep: Executing");
            // Simulate success
            await Task.CompletedTask;
        }

        public async Task CompensateAsync(object data)
        {
            Console.WriteLine("ArrangeShippingStep: Compensating (cancel shipping)");
            await Task.CompletedTask;
        }
    }
} 