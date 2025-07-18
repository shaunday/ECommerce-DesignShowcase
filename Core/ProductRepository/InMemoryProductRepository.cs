using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Catalog;

namespace Core.ProductRepository
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new();

        public Task<IEnumerable<Product>> GetAllAsync() => Task.FromResult(_products.AsEnumerable());
        public Task<Product?> GetByIdAsync(string id) => Task.FromResult(_products.FirstOrDefault(p => p.Id == id));
        public Task AddAsync(Product product) { _products.Add(product); return Task.CompletedTask; }
        public Task UpdateAsync(Product product)
        {
            var idx = _products.FindIndex(p => p.Id == product.Id);
            if (idx >= 0) _products[idx] = product;
            return Task.CompletedTask;
        }
        public Task DeleteAsync(string id)
        {
            _products.RemoveAll(p => p.Id == id);
            return Task.CompletedTask;
        }
    }
} 