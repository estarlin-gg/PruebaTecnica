using PruebaTecnica.Application.Dtos;

namespace PruebaTecnica.Application.Interfaces;

public interface IOrderService
{
    Task<OrderDto> CreateOrderAsync(CreateOrderDto orderDto, CancellationToken cancellationToken = default);

    Task<OrderDto> GetOrderAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<OrderDto>> GetOrdersAsync(CancellationToken cancellationToken = default);

    Task<OrderDto> UpdateOrderItemQuantityAsync(Guid orderId, Guid itemId, UpdateOrderItemDto orderItemDto, CancellationToken cancellationToken = default);

    Task RemoveOrderItemAsync(Guid orderId, Guid itemId, CancellationToken cancellationToken = default);
}
