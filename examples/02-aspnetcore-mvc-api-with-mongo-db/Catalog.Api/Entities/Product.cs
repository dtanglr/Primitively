using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Primitively.MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Api.Entities;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonRepresentation(BsonType.String)]
    public Guid Guid { get; set; } = Guid.NewGuid();

    // This property is a Primitively IGuid type. It will use the Primitively GuidBsonSerializer for Bson compatibility.
    // By default, this property value will be stored as: BsonType.Binary + GuidRepresentation.CSharpLegacy (Base64)
    // In this example, the default has been overidden at collection level to use GuidRepresentation.Standard (UUID)
    // Override options: [BsonRepresentation(BsonType.String)] to store as a string
    //[BsonIGuidRepresentation(GuidRepresentation.Standard)]
    public Sku Sku { get; set; } = Sku.New();

    [BsonIGuidRepresentation(GuidRepresentation.PythonLegacy)]
    public ProductId ProductId { get; set; } = ProductId.New();

    public string? Name { get; set; }

    public Category Category { get; set; }

    public string? Summary { get; set; }

    public string? Description { get; set; }

    public string? ImageFile { get; set; }

    public decimal Price { get; set; }
}
