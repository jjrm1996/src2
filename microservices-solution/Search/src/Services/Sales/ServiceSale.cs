using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Search.Models;

namespace Search.Services.Sales
{
    public class ServiceSale(IHttpClientFactory _httpClientFactory)  : IServiceSale
    {
        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            var client = _httpClientFactory.CreateClient("salesApi");
            var response = await client.GetAsync("/api/sales");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Sale>>(content);
            }

            return [];
        }

        public async Task<IEnumerable<Sale>> GetByCustomerIdAsync(int customerId)
        {
            var client = _httpClientFactory.CreateClient("salesApi");
            var response = await client.GetAsync($"customer/{customerId}");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<IEnumerable<Sale>>();
                return content;
            }

            return [];
        }
    }
}