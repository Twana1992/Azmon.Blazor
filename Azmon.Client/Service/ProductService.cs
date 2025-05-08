using Azmon.Core;
using System.Net.Http.Json;

namespace Azmon.Client.Service
{
    public class ProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        // Get All Products
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _http.GetFromJsonAsync<List<Product>>("api/Products")
                   ?? new List<Product>();
        }

        // Get Single Product by Id
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<Product>($"api/Products/{id}");
        }

        // Add New Product
        public async Task<HttpResponseMessage> AddProductAsync(Product product)
        {
            return await _http.PostAsJsonAsync("api/Products", product);
        }

        // Update Existing Product
        public async Task<HttpResponseMessage> UpdateProductAsync(int id, Product product)
        {
            return await _http.PutAsJsonAsync($"api/Products/{id}", product);
        }

        // Delete Product
        public async Task<HttpResponseMessage> DeleteProductAsync(int id)
        {
            return await _http.DeleteAsync($"api/Products/{id}");
        }
    }
}
