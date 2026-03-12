using System.Collections.Generic;
using Sales.Models;

namespace Sales.Data
{
    public interface ISalesRepository
    {
        IEnumerable<Sale> GetAll();
        Sale GetById(int id);
        void Add(Sale sale);
        void Update(Sale sale);
        void Delete(int id);
    }
}