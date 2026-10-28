using PruebaTecnica.Application.Dtos;
using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Domain.Interfaces;
using PruebaTecnica.Infrastructure.Interfaces;

namespace PruebaTecnica.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    public async Task CreateCustomerAsync(CreateCustomerDto customerDto)
    {
        var customer = new Customer(
            
            );
    }

    public async Task<CustomerDto> GetCustomerAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}