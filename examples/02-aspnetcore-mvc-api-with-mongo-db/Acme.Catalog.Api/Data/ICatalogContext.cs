using Acme.Catalog.Api.Entities;
using MongoDB.Driver;

namespace Acme.Catalog.Api.Data;

public interface ICatalogContext
{
    IMongoCollection<Product> Products { get; }
}
