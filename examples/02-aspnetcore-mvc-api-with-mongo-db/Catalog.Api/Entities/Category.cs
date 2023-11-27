using MongoDB.Bson;
using Primitively.MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Api.Entities;

public readonly record struct Category
{
    [BsonIGuidRepresentation(GuidRepresentation.CSharpLegacy)]
    public CategoryId CategoryId { get; init; }

    public string? Name { get; init; }
}
