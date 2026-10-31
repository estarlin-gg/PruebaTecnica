using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Domain.Interfaces;

public interface ICustomerRepository
{
    Task<Customer?> GetCustomerAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Customer?> GetCustomerByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Customer>> GetCustomersAsync(CancellationToken cancellationToken = default);

    Task CreateCustomerAsync(Customer customer, CancellationToken cancellationToken = default);
}
