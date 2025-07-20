using System;
using System.Threading.Tasks;
using Core.Balancer;
using Core.ServiceBus;
using Core.Workflow;
using Orchestrator;

namespace Orchestrator.Steps
{
    public class ProcessPaymentStep : Orchestrator.IInfrastructureAwareStep
    {
        private ILoadBalancer _balancer;
        private IServiceApi<PaymentRequest, PaymentResponse> _paymentApi;

        public void SetInfrastructure(ILoadBalancer balancer, IServiceBus serviceBus)
        {
            _balancer = balancer;
            _serviceBus = serviceBus;
        }

        public void SetApi(IServiceApi<PaymentRequest, PaymentResponse> paymentApi)
        {
            _paymentApi = paymentApi;
        }

        public async Task ExecuteAsync(Core.Workflow.IWorkflowContext context)
        {
            Console.WriteLine("ProcessPaymentStep: Executing");
            var data = context.Data;
            if (_paymentApi != null)
            {
                var request = new PaymentRequest { /* map from data */ };
                var response = await _paymentApi.HandleAsync(request);
                Console.WriteLine($"ProcessPaymentStep: Payment processed, response: {response}");
            }
            else
            {
                Console.WriteLine("ProcessPaymentStep: No API, simulated execution");
            }
        }

        public async Task CompensateAsync(Core.Workflow.IWorkflowContext context, Exception sagaFailure, ICompensatableStep failedStep)
        {
            Console.WriteLine($"ProcessPaymentStep: Compensating (refund payment) due to failure in {failedStep?.GetType().Name}: {sagaFailure.Message}");
            var data = context.Data;
            if (_paymentApi != null)
            {
                var request = new PaymentRequest { /* map from data, indicate refund */ };
                var response = await _paymentApi.HandleAsync(request);
                Console.WriteLine($"ProcessPaymentStep: Payment refunded, response: {response}");
            }
            else
            {
                Console.WriteLine("ProcessPaymentStep: No API, simulated compensation");
            }
        }
    }
} 