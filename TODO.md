# Advanced Saga Features TODO

- [ ] Implement saga state persistence (ISagaStateStore) to allow sagas to survive restarts and resume incomplete workflows.
- [ ] Add timeouts and compensation triggers for saga steps (e.g., compensate if a step takes too long).
- [ ] Support parallel and conditional execution of saga steps within the orchestrator.
- [ ] Enable event-driven saga progression (steps can wait for external events to proceed).
- [ ] Ensure idempotency and safe re-execution of saga steps (track completed steps, allow retries).
- [ ] Add correlation/tracking for sagas (unique sagaId/correlationId, pass through all steps and logs).
- [ ] Support passing and handling step results between saga steps.
- [ ] Implement custom failure policies for saga steps (retry, skip, escalate, etc.).

---
**Note:** The main system architecture diagram is now maintained in [`ARCHITECTURE.md`](./ARCHITECTURE.md). 