using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Catalog.Api.Entities;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    // This Sku property type will use the Primitively GuidBsonSerializer.
    // This stores the value as a BsonType.Binary.
    // Override option: [BsonRepresentation(BsonType.String)]
    [BsonRepresentation(BsonType.String)]
    public Sku Sku { get; set; }

    public string? Name { get; set; }

    public string? Category { get; set; }

    public string? Summary { get; set; }

    public string? Description { get; set; }

    public string? ImageFile { get; set; }

    public decimal Price { get; set; }
}
