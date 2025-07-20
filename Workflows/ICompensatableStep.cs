using System.Threading.Tasks;

namespace Orchestrator.Steps
{
    public interface ICompensatableStep
    {
        Task ExecuteAsync(Core.Workflow.WorkflowContext context);
        Task CompensateAsync(Core.Workflow.WorkflowContext context);
    }
} 