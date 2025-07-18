using System;
using System.Threading.Tasks;

namespace Adapters.Physical
{
    public class PhysicalInputHandler : IPhysicalInputHandler
    {
        private readonly ISagaOrchestrator _sagaOrchestrator;

        public PhysicalInputHandler(ISagaOrchestrator sagaOrchestrator)
        {
            _sagaOrchestrator = sagaOrchestrator;
        }

        public async Task OnPhysicalInputAsync(string inputType, object data)
        {
            Console.WriteLine($"[PhysicalInput] Received input: {inputType}");
            // Example: Start a saga based on the physical input
            await _sagaOrchestrator.ExecuteSagaAsync($"{inputType}Saga", data);
        }
    }
} 