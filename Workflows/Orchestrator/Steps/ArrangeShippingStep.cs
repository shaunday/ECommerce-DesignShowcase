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
        public async Task ExecuteAsync(Core.Workflow.WorkflowContext context)
        {
            Console.WriteLine("ArrangeShippingStep: Executing");
            // Simulate success
            await Task.CompletedTask;
        }

        public async Task CompensateAsync(Core.Workflow.IWorkflowContext context, Exception sagaFailure, ICompensatableStep failedStep)
        {
            Console.WriteLine($"ArrangeShippingStep: Compensating (cancel shipping) due to failure in {failedStep?.GetType().Name}: {sagaFailure.Message}");
            await Task.CompletedTask;
        }
    }
} 