using System.Collections.Generic;
using System.Threading.Tasks;

namespace Products.src.Data;

public interface IProductRepository
{
    public Task<IEnumerable<Product>> GetAll();
    public Task<Product> GetById(int id);
    public Task Add(Product product);
    public Task Update(Product product);
    public Task Delete(int id);
    public Task<IEnumerable<Product>> GetProductsByIdAsync(IEnumerable<int> ids);
}
