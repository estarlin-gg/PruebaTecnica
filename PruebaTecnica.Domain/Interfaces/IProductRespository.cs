using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Domain.Interfaces;

public interface IProductRespository
{
    Task<List<Product>> GetAllProductsAsync();
    
    Task CreateProductAsync(Product product);
    
    Task UpdateProductAsync(Product product);
    
    
    
    
}