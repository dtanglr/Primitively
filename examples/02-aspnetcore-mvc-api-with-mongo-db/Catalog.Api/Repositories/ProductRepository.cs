using Catalog.Api.Data;
using Catalog.Api.Entities;
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
            .Find(Builders<Product>.Filter.Empty)
            .ToListAsync();
    }

    public async Task<Product> GetProductById(ProductId id)
    {
        return await _context
            .Products
            .Find(p => p.Id == id)
            .SingleOrDefaultAsync();
    }

    public async Task<Product> GetProductByGuid(Guid guid)
    {
        return await _context
            .Products
            .Find(p => p.Guid == guid)
            .SingleOrDefaultAsync();
    }

    public async Task<Product> GetProductBySku(Sku sku)
    {
        return await _context
            .Products
            .Find(Builders<Product>.Filter.Eq(p => p.Sku, sku))
            .SingleOrDefaultAsync();
    }

    public async Task<Product> GetProductByProductId(ProductId productId)
    {
        return await _context
            .Products
            .Find(p => p.ProductId == productId)
            .SingleOrDefaultAsync();
    }

    public async Task<List<Product>> GetProducts(CategoryId categoryId)
    {
        return await _context
            .Products
            .Find(Builders<Product>.Filter.Eq(p => p.Category.CategoryId, categoryId))
            .ToListAsync();
    }
}
