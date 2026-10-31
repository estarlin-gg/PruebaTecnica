using PruebaTecnica.Application.Dtos;

namespace PruebaTecnica.Application.Interfaces;

public interface ICustomerService
{
    Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto customerDto, CancellationToken cancellationToken = default);

    Task<CustomerDto> GetCustomerAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<CustomerDto>> GetCustomersAsync(CancellationToken cancellationToken = default);
}
