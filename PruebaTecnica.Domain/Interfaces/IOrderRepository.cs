using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Domain.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetOrderAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Order>> GetOrdersAsync(CancellationToken cancellationToken = default);

    Task CreateOrderAsync(Order order, CancellationToken cancellationToken = default);

    Task RemoveOrderItemAsync(OrderItem item);
}
