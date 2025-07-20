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

                    // Timeout policy
                    TimeSpan? timeout = null;
                    if (step is Core.Workflow.ITimeoutPolicyProvider timeoutProvider && timeoutProvider.TimeoutPolicy != null)
                    {
                        timeout = timeoutProvider.TimeoutPolicy.GetTimeout(context);
                    }

                    Task execTask = step.ExecuteAsync(context);
                    if (timeout.HasValue)
                    {
                        if (await Task.WhenAny(execTask, Task.Delay(timeout.Value)) != execTask)
                        {
                            // Throw timeout exception to be caught by the catch block below
                            throw new TimeoutException($"Step timed out after {timeout.Value.TotalSeconds} seconds");
                        }
                        // Await the task to propagate any exceptions
                        await execTask;
                    }
                    else
                    {
                        await execTask;
                    }
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
                var failedStep = executedSteps.Count > 0 ? executedSteps.Peek() : null;
                while (executedSteps.Count > 0)
                {
                    var prevStep = executedSteps.Pop();
                    bool shouldCompensateStep = true;
                    if (prevStep is Core.Workflow.ICompensationTriggerProvider compProvider && compProvider.CompensationTriggerPolicy != null)
                    {
                        shouldCompensateStep = compProvider.CompensationTriggerPolicy.ShouldTriggerCompensation(ex, context);
                    }
                    if (shouldCompensateStep)
                    {
                        await prevStep.CompensateAsync(context, ex, failedStep);
                    }
                }
                Console.WriteLine($"SagaOrchestrator: Saga '{sagaName}' compensated");
                Console.WriteLine($"SagaOrchestrator: Error in saga '{sagaName}': {ex.Message}");
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