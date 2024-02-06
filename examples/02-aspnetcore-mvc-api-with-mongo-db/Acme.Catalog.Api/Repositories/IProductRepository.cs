using Acme.Catalog.Api.Entities;

namespace Acme.Catalog.Api.Repositories;

public interface IProductRepository
{
    Task<Product> GetProduct(ProductId id);
    Task<Product> GetProduct(Sku sku);
    Task<IEnumerable<Product>> GetProducts();
    Task<IEnumerable<Product>> GetProducts(CategoryId categoryId);
}
