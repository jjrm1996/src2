using System.Collections.Generic;
using System.Threading.Tasks;
using Search.Models;

namespace Search.Services.Sales
{
    public interface IServiceSale
    {
        Task<IEnumerable<Sale>> GetAllAsync();
        Task<IEnumerable<Sale>> GetByCustomerIdAsync(int customerId);
    }
}