using System;
using System.Threading.Tasks;
using Core.Balancer;
using Core.ServiceBus;
using Core.Workflow;
using Orchestrator;

namespace Orchestrator.Steps
{
    public class ArrangeShippingStep : Core.Workflow.ICompensatableStep
    {
        private IServiceApi<ShippingRequest, ShippingResponse> _shippingApi;
        public void SetApi(IServiceApi<ShippingRequest, ShippingResponse> shippingApi)
        {
            _shippingApi = shippingApi;
        }
        public async Task ExecuteAsync(Core.Workflow.IWorkflowContext context)
        {
            Console.WriteLine("ArrangeShippingStep: Executing");
            var data = context.Data;
            if (_shippingApi != null)
            {
                var request = new ShippingRequest { /* map from data */ };
                var response = await _shippingApi.HandleAsync(request);
                Console.WriteLine($"ArrangeShippingStep: Arranged shipping, response: {response}");
            }
            else
            {
                Console.WriteLine("ArrangeShippingStep: No API, simulated execution");
            }
        }
        public async Task CompensateAsync(Core.Workflow.IWorkflowContext context, Exception sagaFailure, ICompensatableStep failedStep)
        {
            Console.WriteLine($"ArrangeShippingStep: Compensating (cancel shipping) due to failure in {failedStep?.GetType().Name}: {sagaFailure.Message}");
            var data = context.Data;
            if (_shippingApi != null)
            {
                var request = new ShippingRequest { /* map from data, indicate cancel */ };
                var response = await _shippingApi.HandleAsync(request);
                Console.WriteLine($"ArrangeShippingStep: Shipping cancelled, response: {response}");
            }
            else
            {
                Console.WriteLine("ArrangeShippingStep: No API, simulated compensation");
            }
        }
    }
} 