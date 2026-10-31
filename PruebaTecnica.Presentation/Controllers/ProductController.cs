using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Application.Dtos;
using PruebaTecnica.Application.Interfaces;

namespace PruebaTecnica.Presentation.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDto>> CreateAsync([FromBody] CreateProductDto productDto, CancellationToken cancellationToken)
    {
        var product = await _productService.CreateProductAsync(productDto, cancellationToken);

        return CreatedAtRoute("GetProductById", new { id = product.Id }, product);
    }

    [HttpGet("{id:guid}", Name = "GetProductById")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await _productService.GetProductAsync(id, cancellationToken);

        return Ok(product);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var products = await _productService.GetProductsAsync(cancellationToken);

        return Ok(products);
    }
}
