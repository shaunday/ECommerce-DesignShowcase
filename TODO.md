# Microservices Architecture Design TODO

> **Note:** This project is a design showcase and learning guide for modern microservices architecture. All items focus on abstraction, documentation, and demonstration of patterns—not real infrastructure or production implementation.

---

## Advanced Saga & Workflow Patterns
- [ ] Design saga state persistence abstraction (ISagaStateStore) to illustrate how sagas survive restarts and resume workflows.
- [ ] Showcase parallel and conditional execution patterns for saga steps within the orchestrator.
- [ ] Illustrate event-driven saga progression (steps waiting for external events to proceed).
- [ ] Ensure idempotency and safe re-execution patterns for saga steps (track completed steps, allow retries in design).
- [ ] Document custom failure policy patterns for saga steps (retry, skip, escalate, etc.).

---

## Production-Grade System Design TODO
- [ ] Design durable database abstraction layers for all services (e.g., SQL, NoSQL in production).
- [ ] Design HTTP/gRPC API abstraction layers for all services.
- [ ] Design authentication and authorization abstractions (OAuth2, JWT, RBAC/ABAC).
- [ ] Design centralized logging, tracing, and metrics abstraction layers (e.g., ELK stack, OpenTelemetry, Prometheus in production).
- [ ] Design auto-scaling, circuit breaker, and fallback logic abstractions.
- [ ] Design dead-letter queue and retry policy abstractions.
- [ ] Design disaster recovery and multi-region support patterns.
- [ ] Design caching and service discovery abstraction layers.

---
**Note:** The main system architecture diagram is now maintained in [`ARCHITECTURE.md`](./ARCHITECTURE.md). 