using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PruebaTecnica.Application.Interfaces;
using PruebaTecnica.Application.Mapping;
using PruebaTecnica.Application.Options;
using PruebaTecnica.Application.Services;

namespace PruebaTecnica.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        MappingConfig.RegisterMappings();

        services.Configure<InvoiceOptions>(configuration.GetSection(InvoiceOptions.SectionName));

        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IInvoiceService, InvoiceService>();

        return services;
    }
}
