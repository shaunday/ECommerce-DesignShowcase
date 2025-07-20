namespace Core.Workflow
{
    public interface IWorkflowContext
    {
        string CorrelationId { get; set; }
        object Data { get; set; }
    }

    public class WorkflowContext : IWorkflowContext
    {
        public string CorrelationId { get; set; }
        public object Data { get; set; }

        public WorkflowContext(string correlationId, object data)
        {
            CorrelationId = correlationId;
            Data = data;
        }
    }

    public interface IWorkflowValidator
    {
        void Validate(IWorkflowContext context);
    }

    public interface IWorkflowPreTransformer
    {
        IWorkflowContext PreTransform(IWorkflowContext context);
    }

    public interface IWorkflowPostTransformer
    {
        IWorkflowContext PostTransform(IWorkflowContext context);
    }

    public interface IWorkflowStepFactory
    {
        ICompensatableStep CreateStep(string stepType, IWorkflowContext context);
    }

    public interface IValidatableStep : ICompensatableStep
    {
        IWorkflowValidator? Validator { get; }
        IWorkflowPreTransformer? PreTransformer { get; }
        IWorkflowPostTransformer? PostTransformer { get; }
    }

    public interface IStepTimeoutPolicy
    {
        TimeSpan GetTimeout(IWorkflowContext context);
    }

    public interface ICompensationTriggerPolicy
    {
        bool ShouldTriggerCompensation(Exception ex, IWorkflowContext context);
    }

    public interface ITimeoutPolicyProvider
    {
        IStepTimeoutPolicy? TimeoutPolicy { get; }
    }

    public interface ICompensationTriggerProvider
    {
        ICompensationTriggerPolicy? CompensationTriggerPolicy { get; }
    }
} 