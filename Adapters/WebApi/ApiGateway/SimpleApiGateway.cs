using System;
using System.Threading.Tasks;
using Orchestrator;
using Core;

namespace Adapters.WebApi.ApiGateway
{
    public class SimpleApiGateway : IApiGateway
    {
        private readonly ISagaOrchestrator _sagaOrchestrator;
        private readonly Services.User.IUserService _userService;

        public SimpleApiGateway(ISagaOrchestrator sagaOrchestrator, Services.User.IUserService userService = null)
        {
            _sagaOrchestrator = sagaOrchestrator;
            _userService = userService;
        }

        public async Task<object?> RouteRequestAsync(string route, WorkflowContext context)
        {
            // Ensure correlationId exists
            if (string.IsNullOrEmpty(context.CorrelationId))
                context.CorrelationId = Guid.NewGuid().ToString();

            if (route == "order/fulfill")
            {
                Console.WriteLine($"[ApiGateway] Routing to saga orchestrator with CorrelationId: {context.CorrelationId}");
                return await _sagaOrchestrator.ExecuteSagaAsync("OrderSaga", context);
            }
            else if (route == "user/get")
            {
                Console.WriteLine($"[ApiGateway] Routing to user service with CorrelationId: {context.CorrelationId}");
                if (_userService != null)
                {
                    var userId = (context.Data as dynamic)?.UserId as string;
                    return await _userService.GetUserByIdAsync(userId);
                }
                else
                {
                    Console.WriteLine("[ApiGateway] No user service available.");
                    return null;
                }
            }
            else
            {
                Console.WriteLine($"[ApiGateway] Direct service call to '{route}' with CorrelationId: {context.CorrelationId}");
                return null;
            }
        }
    }
} 