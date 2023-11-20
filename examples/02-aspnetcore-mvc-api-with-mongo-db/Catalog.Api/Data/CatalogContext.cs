using Catalog.Api.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Api.Data;

public class CatalogContext : ICatalogContext
{
    public CatalogContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
        var client = new MongoClient(connectionString);

        var databaseName = configuration.GetValue<string>("DatabaseSettings:DatabaseName");
        var database = client.GetDatabase(databaseName);

#pragma warning disable CS0618 // Type or member is obsolete
        var mongoCollectionSettings = new MongoCollectionSettings
        {
            // This stores all GUIDs and Primitively IGuid types as UUID rather than the default Base64 (CharpLegacy)
            // Nb. This is flagged as obsolete because GuidRepresentation.Standard will become the default in version 3
            GuidRepresentation = GuidRepresentation.Standard
        };
#pragma warning restore CS0618 // Type or member is obsolete

        var collectionName = configuration.GetValue<string>("DatabaseSettings:CollectionName");
        Products = database.GetCollection<Product>(collectionName, mongoCollectionSettings);
    }

    public IMongoCollection<Product> Products { get; }
}
