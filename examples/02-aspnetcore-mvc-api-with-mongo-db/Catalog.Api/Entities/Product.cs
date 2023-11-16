using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Api.Entities;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    // This Sku property type will use the Primitively GuidBsonSerializer.
    // By default, this property value will be stored as: BsonType.Binary + GuidRepresentation.CSharpLegacy (Base64)
    // Override options
    //  1: [BsonRepresentation(BsonType.String)] to store as a string
    //  2: [BsonIGuidRepresentation(GuidRepresentation.Standard)] to store as a UUID
    public Sku Sku { get; set; } = Sku.New();

    public string? Name { get; set; }

    public string? Category { get; set; }

    public string? Summary { get; set; }

    public string? Description { get; set; }

    public string? ImageFile { get; set; }

    public decimal Price { get; set; }
}
