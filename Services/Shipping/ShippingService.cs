using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;

namespace Services.Shipping
{
    public class ShippingService : IShippingService
    {
        public string Name => "ShippingService";

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

        public Task<IEnumerable<Shipment>> ListShipmentsAsync()
        {
            Console.WriteLine($"{Name}: Listing shipments.");
            return Task.FromResult<IEnumerable<Shipment>>(new List<Shipment>());
        }

        public Task<Shipment?> GetShipmentByIdAsync(string id)
        {
            Console.WriteLine($"{Name}: Getting shipment by id: {id}");
            return Task.FromResult<Shipment?>(null);
        }
    }
} 