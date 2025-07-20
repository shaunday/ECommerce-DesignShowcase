using System;
using System.Threading.Tasks;
using Core.Balancer;
using Core.ServiceBus;
using Core.Workflow;
using Orchestrator;

namespace Orchestrator.Steps
{
    public class ReserveInventoryStep : Orchestrator.IInfrastructureAwareStep
    {
        private ILoadBalancer _balancer;
        private IServiceApi<InventoryRequest, InventoryResponse> _inventoryApi;
        public void SetInfrastructure(ILoadBalancer balancer, IServiceBus serviceBus)
        {
            _balancer = balancer;
            _serviceBus = serviceBus;
        }
        public void SetApi(IServiceApi<InventoryRequest, InventoryResponse> inventoryApi)
        {
            _inventoryApi = inventoryApi;
        }
        public async Task ExecuteAsync(Core.Workflow.IWorkflowContext context)
        {
            Console.WriteLine("ReserveInventoryStep: Executing");
            var data = context.Data;
            if (_inventoryApi != null)
            {
                var request = new InventoryRequest { /* map from data */ };
                var response = await _inventoryApi.HandleAsync(request);
                Console.WriteLine($"ReserveInventoryStep: Reserved inventory, response: {response}");
            }
            else
            {
                Console.WriteLine("ReserveInventoryStep: No API, simulated execution");
            }
        }
        public async Task CompensateAsync(Core.Workflow.IWorkflowContext context, Exception sagaFailure, ICompensatableStep failedStep)
        {
            Console.WriteLine($"ReserveInventoryStep: Compensating (releasing inventory) due to failure in {failedStep?.GetType().Name}: {sagaFailure.Message}");
            var data = context.Data;
            if (_inventoryApi != null)
            {
                var request = new InventoryRequest { /* map from data, indicate release */ };
                var response = await _inventoryApi.HandleAsync(request);
                Console.WriteLine($"ReserveInventoryStep: Released inventory, response: {response}");
            }
            else
            {
                Console.WriteLine("ReserveInventoryStep: No API, simulated compensation");
            }
        }
    }
} 