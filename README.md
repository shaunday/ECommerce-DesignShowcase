# ShoppingFramework Microservices Skeleton (Stage 1)

## Overview
This project is a modular, interface-driven skeleton for a distributed microservices system, designed for an online garments shop. It demonstrates key architectural patterns such as orchestration, load balancing, API gateway, service-to-service communication, and the saga pattern, all in C#.

## Architecture
- **Domain Services**: Each business domain (Catalog, Order, Payment, Inventory, Shipping, User) has its own interface and implementation in the `Services` directory.
- **Service Bus**: Abstracts communication between services (`Core/ServiceBus`).
- **Load Balancer**: Selects service instances in a round-robin, async, queue-based manner (`Core/Balancer`).
- **API Gateway**: Central entry point for all requests, routes to orchestrator or services, manages correlation IDs (`Core/ApiGateway`).
- **Saga Orchestrator**: Coordinates multi-step workflows with compensation logic (`Orchestrator/`).
- **Compensatable Steps**: Each step is an interface-driven, injectable unit (`Orchestrator/Steps/`).
- **Dependency Injection**: Simple DI container for wiring up dependencies (`Core/DependencyInjection`).
- **Workflow Context**: Carries correlation IDs and workflow data for traceability (`Core/WorkflowContext.cs`).

## System Diagram
See [`ARCHITECTURE.md`](./ARCHITECTURE.md) for a visual schematic of the system's main relationships and flow.

## Key Components
- `Core/ServiceBus/` — Service-to-service communication abstraction
- `Core/Balancer/` — Async, queue-based load balancer
- `Core/ApiGateway/` — API gateway/facade
- `Core/DependencyInjection/` — Simple DI container
- `Core/Auth/` — Authentication abstraction
- `Core/ProductRepository/` — Product persistence abstraction
- `Services/` — Per-domain service interfaces and implementations
- `Services/Order/Validation/` — Order validation logic
- `Orchestrator/` — Saga orchestrator and compensatable steps
- `App/Program.cs` — Wires up dependencies and runs a demo

## Demo Usage
The demo simulates:
- An order fulfillment workflow (via saga orchestrator)
- Compensation logic if a step fails
- All dependencies wired up via DI

To run the demo:
1. Open the solution in your C# IDE (e.g., Visual Studio, VS Code).
2. Build and run the project.
3. Observe the console output for orchestration, load balancing, and saga compensation.

## Design Principles
- **Interface-based**: All major components are defined by interfaces for extensibility.
- **Async and context-aware**: Simulates real-world distributed system behavior.
- **Separation of concerns**: Each component has a clear responsibility.
- **Traceability**: Correlation IDs are used throughout the system.
- **Saga Pattern**: Multi-step workflows with compensation logic for failure handling.

## What’s Next?
- Implement advanced saga features (see `TODO.md`)
- Add real networking, persistence, or error handling for deeper simulation.
- Expand API gateway and orchestrator logic as needed.

---
**Stage 1** focuses on architecture and simulation, not production-ready features. Perfect for learning, prototyping, or as a base for further development. 