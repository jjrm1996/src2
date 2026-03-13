using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.src.Data;

public class CustomerRepository : ICustomerRepository
{
    private readonly List<Customer> _customers = [
        new Customer { Id = 1, FirstName = "Alejandro", LastName = "García", Email = "alejandro.garcia@email.com" },
            new Customer { Id = 2, FirstName = "Lucía", LastName = "Fernández", Email = "lucia.f@provider.net" },
            new Customer { Id = 3, FirstName = "Mateo", LastName = "Rodríguez", Email = "m.rodriguez@company.org" },
            new Customer { Id = 4, FirstName = "Elena", LastName = "Sánchez", Email = "elena.sanchez88@gmail.com" },
            new Customer { Id = 5, FirstName = "Julián", LastName = "Torres", Email = "jtorres_dev@outlook.com" }
    ];

    public async Task<IEnumerable<Customer>> GetAllAsync() => await Task.FromResult(_customers);

    public async Task<Customer> GetByIdAsync(int id) => await Task.FromResult(_customers.FirstOrDefault(c => c.Id == id));

    public async Task AddAsync(Customer customer)
    {
        customer.Id = _customers.Count > 0 ? _customers.Max(c => c.Id) + 1 : 1;
        _customers.Add(customer);
    }

    public async Task UpdateAsync(Customer customer)
    {
        var existingCustomer = await GetByIdAsync(customer.Id);
        if (existingCustomer != null)
        {
            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.Email = customer.Email;
        }
    }

    public async Task DeleteAsync(int id)
    {
        var customer = await GetByIdAsync(id);
        if (customer != null)
        {
            _customers.Remove(customer);
        }
    }
}
