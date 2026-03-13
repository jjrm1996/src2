using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Search.Services.Customers;
using Search.src.Models;

namespace Search.src.Services.Customers;

public class ServiceCustomer(IHttpClientFactory _httpClientFactory) : IServiceCustomer
{
    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        var client = _httpClientFactory.CreateClient("customerApi");
        var response = await client.GetAsync(string.Empty);
        if (response.IsSuccessStatusCode)
        {
            var customerData = await response.Content.ReadFromJsonAsync<IEnumerable<Customer>>();
            return customerData ?? [];
        }
        
        return [];
    }

    public async Task<Customer> GetCustomerById(int id)
    {
        var client = _httpClientFactory.CreateClient("customerApi");
        var response = await client.GetAsync($"{id}");
        if (response.IsSuccessStatusCode)
        {
            var customerData = await response.Content.ReadFromJsonAsync<Customer>();
            return customerData ?? null;
        }

        return null;
    }
}
