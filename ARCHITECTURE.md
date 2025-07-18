# Architecture Diagram

```mermaid
flowchart TD
    PHYS["Physical Device/Input"]
    APIGW["API Gateway"]
    SAGA["OrderSagaOrchestrator"]
    STEP1["ReserveInventoryStep"]
    STEP2["ProcessPaymentStep"]
    STEP3["ArrangeShippingStep"]
    BAL["Load Balancer"]
    BUS["Service Bus"]
    INV["Inventory Service"]
    PAY["Payment Service"]
    SHIP["Shipping Service"]
    USER["User Service"]
    VALID["Order Validation"]

    PHYS -->|input| SAGA
    APIGW -->|order/fulfill| SAGA
    SAGA --> VALID
    SAGA --> STEP1
    SAGA --> STEP2
    SAGA --> STEP3
    STEP1 --> BAL
    STEP2 --> BAL
    STEP3 --> BAL
    BAL --> BUS
    BUS --> INV
    BUS --> PAY
    BUS --> SHIP
    APIGW -->|user/get| USER
```

**Legend:**
- **Physical Device/Input**: Represents a real-world event (e.g., barcode scan, IoT sensor) that can trigger a workflow.
- **API Gateway** routes requests to the saga orchestrator or user service.
- **OrderSagaOrchestrator** coordinates the workflow, validation, and steps.
- **Steps** use the balancer and service bus to interact with domain services.
- **Balancer** and **Service Bus** are infrastructure, routing requests to the correct service instance.
- **Domain services** (Inventory, Payment, Shipping, User) are the business logic endpoints. 