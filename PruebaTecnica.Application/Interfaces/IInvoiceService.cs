using PruebaTecnica.Application.Dtos;

namespace PruebaTecnica.Application.Interfaces;

public interface IInvoiceService
{
    Task<InvoiceDto> GetInvoiceAsync(Guid orderId, CancellationToken cancellationToken = default);
}
