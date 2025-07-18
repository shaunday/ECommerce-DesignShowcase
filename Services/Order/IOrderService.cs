using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Order
{
    public interface IOrderService : IService
    {
        Task<IEnumerable<Order>> ListOrdersAsync();
        Task<Order?> GetOrderByIdAsync(string id);
    }

    public class Order
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
} 