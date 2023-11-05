using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Catalog.Api.Models;

namespace Catalog.Api.Entities;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    // This Sku property type will use the Primitively GuidBsonSerializer.
    // This stores the value as a BsonType.Binary.
    public Sku Sku { get; set; }

    // This other Sku property type override the default and store the
    // value as a BsonType.String instead.
    [BsonRepresentation(BsonType.String)]
    public Sku Sku2 { get; set; }

    [BsonElement("Name")]
    public string? Name { get; set; }
    public string? Category { get; set; }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string? ImageFile { get; set; }
    public decimal Price { get; set; }
}
