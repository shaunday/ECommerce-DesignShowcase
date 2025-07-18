using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Inventory
{
    public interface IInventoryService : IService
    {
        Task<IEnumerable<InventoryItem>> ListInventoryAsync();
        Task<InventoryItem?> GetInventoryItemByIdAsync(string id);
    }

    public class InventoryItem
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
} 