using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Domain.Interfaces;
using PruebaTecnica.Infrastructure.Context;

namespace PruebaTecnica.Infrastructure.Repositories;

public class ProductRepository : IProductRespository
{
    private readonly ApplicationDbContext _context;
    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<Product>> GetAllProductsAsync()
    {
        var res = await _context.Products.ToListAsync();
        return res;
    }

    public  async Task CreateProductAsync(Product product)
    {
       await _context.Products.AddAsync(product);
    }

    public Task UpdateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }
}