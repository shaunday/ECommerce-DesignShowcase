using Services.Order;

namespace Services.Order.Validation
{
    public interface IOrderValidator
    {
        bool Validate(Order order);
    }
} 