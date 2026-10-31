using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Domain.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetProductAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Product>> GetProductsAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Product>> GetProductsByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    Task CreateProductAsync(Product product, CancellationToken cancellationToken = default);
}
