using MongoDB.Bson;
using Primitively.MongoDB.Bson.Serialization.Attributes;

namespace Acme.Catalog.Api.Entities;

public readonly record struct Category
{
    // Primitively IGuid: Will use PythonLegacy (base64 string) format
    [BsonIGuidRepresentation(GuidRepresentation.PythonLegacy)]
    public CategoryId CategoryId { get; init; }

    public string? Name { get; init; }
}
