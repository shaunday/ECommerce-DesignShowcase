using System.Threading.Tasks;

namespace Orchestrator.Steps
{
    public interface ICompensatableStep
    {
        Task ExecuteAsync(Core.Workflow.IWorkflowContext context);
        Task CompensateAsync(Core.Workflow.IWorkflowContext context, Exception sagaFailure, ICompensatableStep failedStep);
    }
} 