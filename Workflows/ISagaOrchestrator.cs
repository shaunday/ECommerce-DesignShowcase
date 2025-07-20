using System.Threading.Tasks;

namespace Orchestrator
{
    public interface ISagaOrchestrator
    {
        Task<object?> ExecuteSagaAsync(string sagaName, object data);
    }
} 