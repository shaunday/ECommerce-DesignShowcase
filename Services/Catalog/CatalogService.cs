using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;

namespace Services.Catalog
{
    public class CatalogService : ICatalogService
    {
        public string Name => "CatalogService";

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

        public Task<IEnumerable<Product>> ListProductsAsync()
        {
            Console.WriteLine($"{Name}: Listing products.");
            return Task.FromResult<IEnumerable<Product>>(new List<Product>());
        }

        public Task<Product?> GetProductByIdAsync(string id)
        {
            Console.WriteLine($"{Name}: Getting product by id: {id}");
            return Task.FromResult<Product?>(null);
        }
    }
} 