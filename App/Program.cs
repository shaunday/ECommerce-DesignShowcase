using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core;
using Core.Balancer;
using Core.ServiceBus;
using Orchestrator;
using Orchestrator.Steps;
using Services.User;
using Services.Order;
using Services.Catalog;

namespace App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Set up DI container
            var resolver = new Core.DependencyInjection.SimpleDependencyResolver();

            // Register core services
            resolver.Register<IServiceBus>(new ConsoleServiceBus());
            resolver.Register<ILoadBalancer>(new QueuedBalancer());
            resolver.Register<IInventoryService>(new StubInventoryService());
            resolver.Register<IPaymentService>(new StubPaymentService());
            resolver.Register<IShippingService>(new StubShippingService());

            // Register domain services
            resolver.Register<IUserService>(new UserService());

            // Register saga orchestrator with compensatable steps using DI
            var balancer = resolver.Resolve<ILoadBalancer>();
            var serviceBus = resolver.Resolve<IServiceBus>();
            var steps = new List<ICompensatableStep>
            {
                new ReserveInventoryStep(),
                new ProcessPaymentStep(),
                new ArrangeShippingStep()
            };
            var sagaOrchestrator = new OrderSagaOrchestrator(steps, balancer, serviceBus);
            resolver.Register<ISagaOrchestrator>(sagaOrchestrator);

            // Register API gateway with saga orchestrator
            resolver.Register<Core.ApiGateway.IApiGateway>(() => new Core.ApiGateway.SimpleApiGateway(
                sagaOrchestrator,
                resolver.Resolve<IUserService>()
            ));

            // Run the demo via the API gateway
            var apiGateway = resolver.Resolve<Core.ApiGateway.IApiGateway>();
            await apiGateway.RouteRequestAsync("order/fulfill", new WorkflowContext(null, new { OrderId = "123", ProductId = "456", Quantity = 2 }));

            // Simulate a physical input (e.g., barcode scan)
            var physicalInputHandler = new Core.PhysicalInput.PhysicalInputHandler(sagaOrchestrator);
            await physicalInputHandler.OnPhysicalInputAsync("BarcodeScan", new { Barcode = "1234567890", Timestamp = DateTime.UtcNow });

            Console.WriteLine("ShoppingFramework saga orchestration demo complete.");
        }
    }
} 