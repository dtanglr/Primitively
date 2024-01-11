using Catalog.Api.Entities;

namespace Catalog.Api.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> GetProductById(ProductId id);
    Task<Product> GetProductByGuid(Guid guid);
    Task<Product> GetProductBySku(Sku sku);
    Task<Product> GetProductByProductId(ProductId productId);
    Task<List<Product>> GetProducts(CategoryId categoryId);
}
