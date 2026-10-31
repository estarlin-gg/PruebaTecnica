using Mapster;
using PruebaTecnica.Application.Dtos;
using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Application.Mapping;

public static class MappingConfig
{
    private static bool _isRegistered;

    public static void RegisterMappings()
    {
        if (_isRegistered)
        {
            return;
        }

        TypeAdapterConfig<Customer, CustomerDto>.NewConfig();
        TypeAdapterConfig<CreateCustomerDto, Customer>.NewConfig();
        TypeAdapterConfig<Product, ProductDto>.NewConfig();
        TypeAdapterConfig<CreateProductDto, Product>.NewConfig();

        TypeAdapterConfig<OrderItem, OrderItemDto>.NewConfig()
            .Map(destination => destination.ProductName, source => source.Product.Name)
            .Map(destination => destination.Subtotal, source => source.Quantity * source.UnitPrice);

        TypeAdapterConfig<Order, OrderDto>.NewConfig()
            .Map(destination => destination.OrderDate, source => DateTime.SpecifyKind(source.OrderDate, DateTimeKind.Utc))
            .Map(destination => destination.CustomerName, source => source.Customer.Name + " " + source.Customer.LastName)
            .Map(destination => destination.Total, source => source.Items.Sum(item => item.Quantity * item.UnitPrice));

        TypeAdapterConfig<OrderItem, InvoiceItemDto>.NewConfig()
            .Map(destination => destination.ProductName, source => source.Product.Name)
            .Map(destination => destination.Subtotal, source => source.Quantity * source.UnitPrice);

        TypeAdapterConfig<Order, InvoiceDto>.NewConfig()
            .Map(destination => destination.InvoiceDate, source => DateTime.SpecifyKind(source.OrderDate, DateTimeKind.Utc))
            .Map(destination => destination.CustomerName, source => source.Customer.Name + " " + source.Customer.LastName)
            .Map(destination => destination.CustomerEmail, source => source.Customer.Email)
            .Map(destination => destination.Subtotal, source => source.Items.Sum(item => item.Quantity * item.UnitPrice));

        _isRegistered = true;
    }
}
