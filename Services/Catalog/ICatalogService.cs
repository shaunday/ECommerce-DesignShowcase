using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Catalog
{
    public interface ICatalogService : IService
    {
        Task<IEnumerable<Product>> ListProductsAsync();
        Task<Product?> GetProductByIdAsync(string id);
    }

    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
} 