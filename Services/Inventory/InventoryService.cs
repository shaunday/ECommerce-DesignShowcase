using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;

namespace Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        public string Name => "InventoryService";

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

        public Task<IEnumerable<InventoryItem>> ListInventoryAsync()
        {
            Console.WriteLine($"{Name}: Listing inventory.");
            return Task.FromResult<IEnumerable<InventoryItem>>(new List<InventoryItem>());
        }

        public Task<InventoryItem?> GetInventoryItemByIdAsync(string id)
        {
            Console.WriteLine($"{Name}: Getting inventory item by id: {id}");
            return Task.FromResult<InventoryItem?>(null);
        }
    }
} 