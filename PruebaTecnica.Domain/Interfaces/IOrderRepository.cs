using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Domain.Interfaces;

public interface IOrderRepository
{
    Task UpdateOrderAsync(Order order);
    Task CreateOrderAsync(Order order);
    Task<Order> GetOrderAsync(Guid id);
}