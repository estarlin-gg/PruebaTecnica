using PruebaTecnica.Application.Dtos;

namespace PruebaTecnica.Infrastructure.Interfaces;

public interface ICustomerService
{
    Task CreateCustomerAsync(CreateCustomerDto customerDto);
     Task<CustomerDto>  GetCustomerAsync(Guid id);
     
}