using System.Collections.Generic;
using System.Threading.Tasks;
using Search.src.Models;

namespace Search.Services.Customers
{
    public interface IServiceCustomer
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetCustomerById(int id);
    }
}