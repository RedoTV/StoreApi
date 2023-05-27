using Microsoft.AspNetCore.Mvc;
using ProductsApi.Models;

namespace ProductsApi.Services
{
    public interface IRepository
    {
        public Task<IEnumerable<Product>> GetAllProductsAsync();
        public Task<Product> GetProductAsync(int id);
        public Task<Product> AddProductAsync(AddProductModel product);
        public Task<Product> DeleteProductAsync(int id);
        public Task<Product?> UpdateProductInfoAsync(UpdateProductModel updatedInfo);
    }
}