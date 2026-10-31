using Mapster;
using Microsoft.Extensions.Options;
using PruebaTecnica.Application.Common.Exceptions;
using PruebaTecnica.Application.Dtos;
using PruebaTecnica.Application.Interfaces;
using PruebaTecnica.Application.Options;
using PruebaTecnica.Domain.Interfaces;

namespace PruebaTecnica.Application.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IOrderRepository _orderRepository;
    private readonly InvoiceOptions _invoiceOptions;

    public InvoiceService(IOrderRepository orderRepository, IOptions<InvoiceOptions> invoiceOptions)
    {
        _orderRepository = orderRepository;
        _invoiceOptions = invoiceOptions.Value;
    }

    public async Task<InvoiceDto> GetInvoiceAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetOrderAsync(orderId, cancellationToken)
            ?? throw new NotFoundException("No se encontró la orden indicada.");

        var invoice = order.Adapt<InvoiceDto>();
        invoice.InvoiceNumber = $"INV-{order.OrderDate:yyyyMMdd}-{order.Id.ToString("N")[..8].ToUpperInvariant()}";
        invoice.Tax = Math.Round(invoice.Subtotal * _invoiceOptions.TaxRate, 2);
        invoice.Total = invoice.Subtotal + invoice.Tax;

        return invoice;
    }
}
