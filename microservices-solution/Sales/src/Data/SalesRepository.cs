using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sales.Models;

namespace Sales.Data;

public class SalesRepository : ISalesRepository
{
    private readonly List<Sale> _sales = [
        new Sale { Id = 1, CustomerId = 1, ProductId = 1, Quantity = 2  },
            new Sale { Id = 2, CustomerId = 2, ProductId = 1, Quantity = 5  },
            new Sale { Id = 3, CustomerId = 3, ProductId = 2, Quantity = 10  },
            new Sale { Id = 4, CustomerId = 4, ProductId = 3, Quantity = 1  },
            new Sale { Id = 5, CustomerId = 5, ProductId = 4, Quantity = 3  },
        ];

    public async Task<IEnumerable<Sale>> GetAllAsync() => _sales;

    public async Task<Sale> GetByIdAsync(int id) => _sales.FirstOrDefault(s => s.Id == id);

    public async Task<IEnumerable<Sale>> GetByCustomerIdAsync(int customerId) => _sales.Where(s => s.CustomerId == customerId);

    public async Task AddAsync(Sale sale)
    {
        _sales.Add(sale);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Sale sale)
    {
        var existingSale = await GetByIdAsync(sale.Id);
        if (existingSale != null)
        {
            existingSale.ProductId = sale.ProductId;
            existingSale.CustomerId = sale.CustomerId;
            existingSale.Quantity = sale.Quantity;
        }
    }

    public async Task DeleteAsync(int id)
    {
        var sale = await GetByIdAsync(id);
        if (sale != null)
        {
            _sales.Remove(sale);
        }
    }
}

