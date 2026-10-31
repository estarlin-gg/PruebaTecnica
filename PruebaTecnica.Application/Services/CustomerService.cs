using Mapster;
using PruebaTecnica.Application.Common.Exceptions;
using PruebaTecnica.Application.Dtos;
using PruebaTecnica.Application.Interfaces;
using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Domain.Interfaces;

namespace PruebaTecnica.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto customerDto, CancellationToken cancellationToken = default)
    {
        var email = customerDto.Email.Trim().ToLowerInvariant();

        var existingCustomer = await _customerRepository.GetCustomerByEmailAsync(email, cancellationToken);
        if (existingCustomer is not null)
        {
            throw new ConflictException("Ya existe un cliente registrado con ese correo electrónico.");
        }

        var customer = customerDto.Adapt<Customer>();
        customer.Id = Guid.NewGuid();
        customer.Name = customerDto.Name.Trim();
        customer.LastName = customerDto.LastName.Trim();
        customer.Email = email;

        await _customerRepository.CreateCustomerAsync(customer, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return customer.Adapt<CustomerDto>();
    }

    public async Task<CustomerDto> GetCustomerAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.GetCustomerAsync(id, cancellationToken)
            ?? throw new NotFoundException("No se encontró el cliente indicado.");

        return customer.Adapt<CustomerDto>();
    }

    public async Task<IReadOnlyList<CustomerDto>> GetCustomersAsync(CancellationToken cancellationToken = default)
    {
        var customers = await _customerRepository.GetCustomersAsync(cancellationToken);

        return customers.Adapt<List<CustomerDto>>();
    }
}
