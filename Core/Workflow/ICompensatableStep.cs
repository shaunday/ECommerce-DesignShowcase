using System.Threading.Tasks;

namespace Orchestrator.Steps
{
    public interface ICompensatableStep
    {
        Task ExecuteAsync(object data);
        Task CompensateAsync(object data);
    }
} 