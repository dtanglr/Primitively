using Acme.Catalog.Api.Entities;
using MongoDB.Driver;

namespace Acme.Catalog.Api.Data;

public class CatalogContext : ICatalogContext
{
    public CatalogContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
        var client = new MongoClient(connectionString);

        var databaseName = configuration.GetValue<string>("DatabaseSettings:DatabaseName");
        var database = client.GetDatabase(databaseName);

        var collectionName = configuration.GetValue<string>("DatabaseSettings:CollectionName");
        Products = database.GetCollection<Product>(collectionName);
    }

    public IMongoCollection<Product> Products { get; }
}
