using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data;

public class CatalogContext : ICatalogContext
{
    public CatalogContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
        var client = new MongoClient(connectionString);

#pragma warning disable CS0618 // Type or member is obsolete
        var settings = new MongoDatabaseSettings
        {
            // TODO: Avoid having to use this obsolete setting
            // Without this setting the example query (to get a product by Sku), does not work.
            GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard
        };
#pragma warning restore CS0618 // Type or member is obsolete

        var databaseName = configuration.GetValue<string>("DatabaseSettings:DatabaseName");
        var database = client.GetDatabase(databaseName, settings);

        var collectionName = configuration.GetValue<string>("DatabaseSettings:CollectionName");
        Products = database.GetCollection<Product>(collectionName);

        CatalogContextSeed.SeedData(Products);
    }

    public IMongoCollection<Product> Products { get; }
}
