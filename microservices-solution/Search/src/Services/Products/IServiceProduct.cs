using System.Collections.Generic;
using System.Threading.Tasks;
using Search.Models;

namespace Search.Services.Products
{
    public interface IServiceProduct
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetProductsByIdAsync(IEnumerable<int> ids);
    }
}