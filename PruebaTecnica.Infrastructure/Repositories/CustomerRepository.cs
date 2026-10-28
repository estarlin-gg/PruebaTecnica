using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Domain.Interfaces;
using PruebaTecnica.Infrastructure.Context;

namespace PruebaTecnica.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext  _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task CreateCustomerAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
    }

    public async Task<Customer> GetCustomerAsync(Guid id)
    {
       var res = await   _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
       return res;
    }
    
     

}