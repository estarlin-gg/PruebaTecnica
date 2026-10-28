using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Domain.Interfaces;

public interface ICustomerRepository
{
    Task<Customer> GetCustomerAsync(Guid id);
    
    Task CreateCustomerAsync(Customer customer);
    
 
}