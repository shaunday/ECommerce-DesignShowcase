# ECommerce-DesignShowcase: Microservices Architecture Skeleton

## Overview
This project is a modular, interface-driven skeleton for a distributed microservices system, designed to showcase modern architectural patterns for workflow orchestration, service abstraction, and extensibility. It is intended as a learning guide and design reference, not a production-ready implementation.

## Architecture Highlights
- **Domain Services**: Each business domain (Catalog, Order, Payment, Inventory, Shipping, User) is encapsulated behind interfaces and implementations in the `Services` directory.
- **Load Balancer**: Pluggable, async, queue-based load balancer abstraction (`Core/Balancer`).
- **API Gateway**: Central entry point for all requests, routes to orchestrator or services, manages correlation IDs (`Adapters/WebApi/ApiGateway`).
- **Saga Orchestrator**: Coordinates multi-step workflows with advanced compensation logic (`Workflows/Orchestrator`).
- **Compensatable Steps**: Each step is an interface-driven, injectable unit with support for validation, transformation, timeout, and compensation policies (`Workflows/Orchestrator/Steps`).
- **Dependency Injection**: Simple DI container for wiring up dependencies (`Core/DependencyInjection`).
- **Workflow Context**: Strongly-typed, extensible context object for passing data, correlation IDs, and state through the workflow (`Workflows/WorkflowContext.cs`).
- **Factory Patterns**: Factories for load balancers, workflow steps, and protocol-agnostic API adapters enable dynamic, configurable orchestration and service-to-service communication.
- **Protocol-Agnostic API Abstraction**: All service-to-service and workflow step communication is handled via a generic API interface (e.g., `IServiceApi<TRequest, TResponse>`) and injected via factories, decoupling business logic from transport/protocol.

## Advanced Saga & Workflow Patterns
- Context propagation and correlation/tracking IDs throughout the workflow
- Per-step validation and transformation (pre and post)
- Per-step timeout and compensation trigger policies
- Context-aware compensation: each step can compensate differently based on the failure reason and failed step
- Centralized, extensible orchestrator with interface-driven design
- Support for future patterns: parallel/conditional execution, event-driven progression, idempotency, and custom failure policies

## Production-Grade System Design Goals
- Durable database abstraction layers for all services (e.g., SQL, NoSQL in production)
- HTTP/gRPC API abstraction layers for all services (define interfaces and adapters to decouple service logic from transport/protocol)
- Authentication and authorization abstractions (OAuth2, JWT, RBAC/ABAC)
- Centralized logging, tracing, and metrics abstraction layers (e.g., ELK stack, OpenTelemetry, Prometheus in production)
- Auto-scaling, circuit breaker, and fallback logic abstractions
- Dead-letter queue and retry policy abstractions
- Disaster recovery and multi-region support patterns
- Caching and service discovery abstraction layers

## System Diagram
See [`ARCHITECTURE.md`](./ARCHITECTURE.md) for a visual schematic of the system's main relationships and flow.

## Key Components
- `Core/Balancer/` — Async, queue-based load balancer
- `Adapters/WebApi/ApiGateway/` — API gateway/facade
- `Core/DependencyInjection/` — Simple DI container
- `Core/Auth/` — Authentication abstraction
- `Core/ProductRepository/` — Product persistence abstraction
- `Services/` — Per-domain service interfaces and implementations
- `Services/Order/Validation/` — Order validation logic
- `Workflows/Orchestrator/` — Saga orchestrator and compensatable steps
- `Workflows/WorkflowContext.cs` — Workflow context and related abstractions
- `App/Program.cs` — Wires up dependencies and runs a demo
- **Protocol-Agnostic API Abstraction** — All service-to-service and workflow step communication is handled via generic API interfaces and injected via factories (see `IServiceApi<TRequest, TResponse>` and `IServiceApiFactory`).

## Design Principles
- **Interface-based**: All major components are defined by interfaces for extensibility.
- **Async and context-aware**: Simulates real-world distributed system behavior.
- **Separation of concerns**: Each component has a clear responsibility.
- **Traceability**: Correlation IDs are used throughout the system.
- **Saga Pattern**: Multi-step workflows with advanced, context-aware compensation logic.
- **Protocol-Agnostic Communication**: All service-to-service and workflow step calls are decoupled from transport/protocol via API abstractions and factories.

## What’s Next?
See [`TODO.md`](./TODO.md) for a detailed roadmap of advanced saga/workflow patterns and production-grade design abstractions to implement or extend.

---
**Note:** This project is for design and learning purposes. It focuses on abstraction, documentation, and demonstration of patterns—not real infrastructure or production implementation. 