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
        private readonly ILoadBalancerFactory _balancerFactory;
        private readonly IServiceBus _serviceBus;
        private readonly Core.Workflow.IWorkflowStepFactory? _stepFactory;
        private readonly Core.Workflow.IWorkflowValidator? _validator;
        private readonly Core.Workflow.IWorkflowTransformer? _transformer;

        public OrderSagaOrchestrator(List<ICompensatableStep> steps, ILoadBalancerFactory balancerFactory, IServiceBus serviceBus, Core.Workflow.IWorkflowStepFactory? stepFactory = null, Core.Workflow.IWorkflowValidator? validator = null, Core.Workflow.IWorkflowTransformer? transformer = null)
        {
            _steps = steps;
            _balancerFactory = balancerFactory;
            _serviceBus = serviceBus;
            _stepFactory = stepFactory;
            _validator = validator;
            _transformer = transformer;
        }

        public async Task<object?> ExecuteSagaAsync(string sagaName, object data)
        {
            Console.WriteLine($"SagaOrchestrator: Starting saga '{sagaName}'");
            var executedSteps = new Stack<ICompensatableStep>();
            var context = new Core.Workflow.WorkflowContext(sagaName, data);

            // Global validation
            _validator?.Validate(context);
            // Global transformation
            if (_transformer != null)
            {
                context = (Core.Workflow.WorkflowContext)_transformer.Transform(context);
            }
            try
            {
                foreach (var step in _steps)
                {
                    // Per-step validation and pre-transformation
                    if (step is Core.Workflow.IValidatableStep validatableStep)
                    {
                        validatableStep.Validator?.Validate(context);
                        if (validatableStep.PreTransformer != null)
                        {
                            context = (Core.Workflow.WorkflowContext)validatableStep.PreTransformer.PreTransform(context);
                        }
                    }
                    if (step is IInfrastructureAwareStep infraStep)
                    {
                        var balancer = _balancerFactory.CreateBalancer("default");
                        infraStep.SetInfrastructure(balancer, _serviceBus);
                    }
                    await step.ExecuteAsync(context);
                    // Per-step post-transformation
                    if (step is Core.Workflow.IValidatableStep validatableStepPost)
                    {
                        if (validatableStepPost.PostTransformer != null)
                        {
                            context = (Core.Workflow.WorkflowContext)validatableStepPost.PostTransformer.PostTransform(context);
                        }
                    }
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
                    await step.CompensateAsync(context);
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