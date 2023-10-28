using Catalog.Api.Entities;
using Catalog.Api.Models;

namespace Catalog.Api.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> GetProduct(string id);
    Task<Product> GetProduct(Sku sku);
}
