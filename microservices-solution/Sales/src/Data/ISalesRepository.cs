using System.Collections.Generic;
using System.Threading.Tasks;
using Sales.Models;

namespace Sales.Data;

public interface ISalesRepository
{
    Task<IEnumerable<Sale>> GetAllAsync();
    Task<Sale> GetByIdAsync(int id);
    Task<IEnumerable<Sale>> GetByCustomerIdAsync(int customerId);
    Task AddAsync(Sale sale);
    Task UpdateAsync(Sale sale);
    Task DeleteAsync(int id);
}
