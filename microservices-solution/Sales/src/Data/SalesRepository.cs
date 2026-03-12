using System.Collections.Generic;
using System.Linq;
using Sales.Models;

namespace Sales.Data
{
    public class SalesRepository : ISalesRepository
    {
        private readonly List<Sale> _sales = [
            new Sale { Id = 1, CustomerId = 1, ProductId = 1, Quantity = 2  },
            new Sale { Id = 2, CustomerId = 2, ProductId = 1, Quantity = 5  },
            new Sale { Id = 3, CustomerId = 3, ProductId = 2, Quantity = 10  },
            new Sale { Id = 4, CustomerId = 4, ProductId = 3, Quantity = 1  },
            new Sale { Id = 5, CustomerId = 5, ProductId = 4, Quantity = 3  },
        ];

        public IEnumerable<Sale> GetAll() => _sales;

        public Sale GetById(int id) => _sales.FirstOrDefault(s => s.Id == id);

        public void Add(Sale sale) => _sales.Add(sale);

        public void Update(Sale sale)
        {
            var existingSale = GetById(sale.Id);
            if (existingSale != null)
            {
                existingSale.ProductId = sale.ProductId;
                existingSale.CustomerId = sale.CustomerId;
                existingSale.Quantity = sale.Quantity;
            }
        }

        public void Delete(int id)
        {
            var sale = GetById(id);
            if (sale != null)
            {
                _sales.Remove(sale);
            }
        }
    }

}