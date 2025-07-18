using Services.Order;

namespace Services.Order.Validation
{
    public class StubOrderValidator : IOrderValidator
    {
        public bool Validate(Order order) => true;
    }
} 