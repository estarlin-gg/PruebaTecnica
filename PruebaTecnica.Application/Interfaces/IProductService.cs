using PruebaTecnica.Application.Dtos;

namespace PruebaTecnica.Application.Interfaces;

public interface IProductService
{
    Task<ProductDto> CreateProductAsync(CreateProductDto productDto, CancellationToken cancellationToken = default);

    Task<ProductDto> GetProductAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProductDto>> GetProductsAsync(CancellationToken cancellationToken = default);
}
