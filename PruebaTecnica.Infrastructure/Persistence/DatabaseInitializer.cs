using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Infrastructure.Context;

namespace PruebaTecnica.Infrastructure.Persistence;

public static class DatabaseInitializer
{
    public static async Task InitializeDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();

        if (await context.Products.AnyAsync())
        {
            return;
        }

        context.Products.AddRange(
            new Product { Id = Guid.NewGuid(), Name = "Laptop Pro 14", Price = 1250.00m, Stock = 10 },
            new Product { Id = Guid.NewGuid(), Name = "Monitor UltraWide", Price = 420.00m, Stock = 15 },
            new Product { Id = Guid.NewGuid(), Name = "Teclado Mecánico", Price = 85.50m, Stock = 30 },
            new Product { Id = Guid.NewGuid(), Name = "Mouse Inalámbrico", Price = 35.00m, Stock = 40 },
            new Product { Id = Guid.NewGuid(), Name = "Audífonos Bluetooth", Price = 95.00m, Stock = 25 });

        await context.SaveChangesAsync();
    }
}
