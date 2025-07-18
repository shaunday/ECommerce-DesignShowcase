using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Balancer;
using Core.ServiceBus;
using Core.Workflow;

namespace Orchestrator
{
    public class OrderSagaOrchestrator : ISagaOrchestrator
    {
        private readonly List<ICompensatableStep> _steps;
        private readonly ILoadBalancer _balancer;
        private readonly IServiceBus _serviceBus;

        public OrderSagaOrchestrator(List<ICompensatableStep> steps, ILoadBalancer balancer, IServiceBus serviceBus)
        {
            _steps = steps;
            _balancer = balancer;
            _serviceBus = serviceBus;
        }

        public async Task<object?> ExecuteSagaAsync(string sagaName, object data)
        {
            Console.WriteLine($"SagaOrchestrator: Starting saga '{sagaName}'");
            var executedSteps = new Stack<ICompensatableStep>();
            try
            {
                foreach (var step in _steps)
                {
                    // If the step needs balancer/serviceBus, you can cast and set them here
                    if (step is IInfrastructureAwareStep infraStep)
                    {
                        infraStep.SetInfrastructure(_balancer, _serviceBus);
                    }
                    await step.ExecuteAsync(data);
                    executedSteps.Push(step);
                }
                Console.WriteLine($"SagaOrchestrator: Saga '{sagaName}' completed successfully");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SagaOrchestrator: Error in saga '{sagaName}': {ex.Message}. Compensating...");
                while (executedSteps.Count > 0)
                {
                    var step = executedSteps.Pop();
                    await step.CompensateAsync(data);
                }
                Console.WriteLine($"SagaOrchestrator: Saga '{sagaName}' compensated");
                return false;
            }
        }
    }

    // Optional: Interface for steps that need infrastructure
    public interface IInfrastructureAwareStep : ICompensatableStep
    {
        void SetInfrastructure(ILoadBalancer balancer, IServiceBus serviceBus);
    }
} 