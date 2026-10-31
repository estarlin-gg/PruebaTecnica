using Mapster;
using PruebaTecnica.Application.Common.Exceptions;
using PruebaTecnica.Application.Dtos;
using PruebaTecnica.Application.Interfaces;
using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Domain.Interfaces;

namespace PruebaTecnica.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto productDto, CancellationToken cancellationToken = default)
    {
        var product = productDto.Adapt<Product>();
        product.Id = Guid.NewGuid();
        product.Name = productDto.Name.Trim();

        await _productRepository.CreateProductAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Adapt<ProductDto>();
    }

    public async Task<ProductDto> GetProductAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetProductAsync(id, cancellationToken)
            ?? throw new NotFoundException("No se encontró el producto indicado.");

        return product.Adapt<ProductDto>();
    }

    public async Task<IReadOnlyList<ProductDto>> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetProductsAsync(cancellationToken);

        return products.Adapt<List<ProductDto>>();
    }
}
