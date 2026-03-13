using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Search.Models;

namespace Search.Services.Products
{
    public class ServiceProduct(IHttpClientFactory _httpClientFactory)  : IServiceProduct
    {
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var client = _httpClientFactory.CreateClient("productApi");
            var response = await client.GetAsync("");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Product>>(content);
            }

            return [];
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("productApi");
            var response = await client.GetAsync($"{id}");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return System.Text.Json.JsonSerializer.Deserialize<Product>(content);
            }

            return null;
        }

        public async Task<IEnumerable<Product>> GetProductsByIdAsync(IEnumerable<int> ids)
        {
            var client = _httpClientFactory.CreateClient("productApi");
            var response = await client.PostAsync($"filters", JsonContent.Create(ids));
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
                return content;
            }

            return null;
        }
    }
}