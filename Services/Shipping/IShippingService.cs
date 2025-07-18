using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Shipping
{
    public interface IShippingService : IService
    {
        Task<IEnumerable<Shipment>> ListShipmentsAsync();
        Task<Shipment?> GetShipmentByIdAsync(string id);
    }

    public class Shipment
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string Status { get; set; }
    }
} 