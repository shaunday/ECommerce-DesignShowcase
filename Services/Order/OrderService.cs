using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;

namespace Services.Order
{
    public class OrderService : IOrderService
    {
        public string Name => "OrderService";

        public Task StartAsync()
        {
            Console.WriteLine($"{Name} started.");
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            Console.WriteLine($"{Name} stopped.");
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Order>> ListOrdersAsync()
        {
            Console.WriteLine($"{Name}: Listing orders.");
            return Task.FromResult<IEnumerable<Order>>(new List<Order>());
        }

        public Task<Order?> GetOrderByIdAsync(string id)
        {
            Console.WriteLine($"{Name}: Getting order by id: {id}");
            return Task.FromResult<Order?>(null);
        }
    }
} 