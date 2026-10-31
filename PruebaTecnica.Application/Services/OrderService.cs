using Mapster;
using PruebaTecnica.Application.Common.Exceptions;
using PruebaTecnica.Application.Dtos;
using PruebaTecnica.Application.Interfaces;
using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Domain.Interfaces;

namespace PruebaTecnica.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderDto> CreateOrderAsync(CreateOrderDto orderDto, CancellationToken cancellationToken = default)
    {
        if (orderDto.CustomerId == Guid.Empty)
        {
            throw new BusinessRuleException("El cliente indicado no es válido.");
        }

        if (orderDto.Items.Count < 2)
        {
            throw new BusinessRuleException("La orden debe incluir al menos dos productos.");
        }

        if (orderDto.Items.Any(item => item.ProductId == Guid.Empty))
        {
            throw new BusinessRuleException("Uno o más productos no son válidos.");
        }

        if (orderDto.Items.GroupBy(item => item.ProductId).Any(group => group.Count() > 1))
        {
            throw new BusinessRuleException("No se puede repetir un producto en la orden.");
        }

        var customer = await _customerRepository.GetCustomerAsync(orderDto.CustomerId, cancellationToken)
            ?? throw new NotFoundException("No se encontró el cliente indicado.");

        var productIds = orderDto.Items.Select(item => item.ProductId).ToList();
        var products = await _productRepository.GetProductsByIdsAsync(productIds, cancellationToken);

        if (products.Count != productIds.Count)
        {
            throw new NotFoundException("Uno o más productos no existen.");
        }

        var order = new Order
        {
            Id = Guid.NewGuid(),
            OrderDate = DateTime.UtcNow,
            CustomerId = customer.Id,
            Customer = customer
        };

        foreach (var itemDto in orderDto.Items)
        {
            var product = products.First(product => product.Id == itemDto.ProductId);

            if (product.Stock < itemDto.Quantity)
            {
                throw new ConflictException($"No hay stock suficiente para el producto '{product.Name}'.");
            }

            product.Stock -= itemDto.Quantity;

            order.Items.Add(new OrderItem
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                Product = product,
                Quantity = itemDto.Quantity,
                UnitPrice = product.Price
            });
        }

        await _orderRepository.CreateOrderAsync(order, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return order.Adapt<OrderDto>();
    }

    public async Task<OrderDto> GetOrderAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetOrderAsync(id, cancellationToken)
            ?? throw new NotFoundException("No se encontró la orden indicada.");

        return order.Adapt<OrderDto>();
    }

    public async Task<IReadOnlyList<OrderDto>> GetOrdersAsync(CancellationToken cancellationToken = default)
    {
        var orders = await _orderRepository.GetOrdersAsync(cancellationToken);

        return orders.Adapt<List<OrderDto>>();
    }

    public async Task<OrderDto> UpdateOrderItemQuantityAsync(Guid orderId, Guid itemId, UpdateOrderItemDto orderItemDto, CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetOrderAsync(orderId, cancellationToken)
            ?? throw new NotFoundException("No se encontró la orden indicada.");

        var item = order.Items.FirstOrDefault(orderItem => orderItem.Id == itemId)
            ?? throw new NotFoundException("No se encontró el ítem indicado dentro de la orden.");

        if (orderItemDto.Quantity <= 0)
        {
            throw new BusinessRuleException("La cantidad debe ser mayor que cero.");
        }

        var difference = orderItemDto.Quantity - item.Quantity;

        if (difference > 0)
        {
            if (item.Product.Stock < difference)
            {
                throw new ConflictException($"No hay stock suficiente para el producto '{item.Product.Name}'.");
            }

            item.Product.Stock -= difference;
        }
        else if (difference < 0)
        {
            item.Product.Stock += Math.Abs(difference);
        }

        item.Quantity = orderItemDto.Quantity;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return order.Adapt<OrderDto>();
    }

    public async Task RemoveOrderItemAsync(Guid orderId, Guid itemId, CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetOrderAsync(orderId, cancellationToken)
            ?? throw new NotFoundException("No se encontró la orden indicada.");

        var item = order.Items.FirstOrDefault(orderItem => orderItem.Id == itemId)
            ?? throw new NotFoundException("No se encontró el ítem indicado dentro de la orden.");

        item.Product.Stock += item.Quantity;

        await _orderRepository.RemoveOrderItemAsync(item);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
