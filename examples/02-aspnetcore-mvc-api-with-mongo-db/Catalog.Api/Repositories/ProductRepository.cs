using Catalog.Api.Data;
using Catalog.Api.Entities;
using Catalog.Api.Models;
using MongoDB.Driver;

namespace Catalog.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext _context;

    public ProductRepository(ICatalogContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _context
            .Products
            .Find(p => true)
            .ToListAsync();
    }

    public async Task<Product> GetProduct(string id)
    {
        return await _context
            .Products
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Product> GetProduct(Sku sku)
    {
        return await _context
            .Products
            .Find(p => p.Sku == sku)
            .FirstOrDefaultAsync();
    }
}
