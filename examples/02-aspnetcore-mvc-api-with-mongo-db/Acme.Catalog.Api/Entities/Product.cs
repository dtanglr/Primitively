﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Primitively.MongoDB.Bson.Serialization.Attributes;

namespace Acme.Catalog.Api.Entities;

public class Product
{
    [BsonId]
    // Primitively IGuid: Will use the default GuidRepresentation of CSharpLegacy unless configured differently in the DI
    public ProductId Id { get; set; } = ProductId.New();

    // Primitively IGuid: Will use the default GuidRepresentation of CSharpLegacy unless configured differently in the DI
    public Sku Sku { get; set; } = Sku.New();

    // Primitively IGuid: Will use Standard (UUID) format
    [BsonIGuidRepresentation(GuidRepresentation.Standard)]
    public ProductId ProductId { get; set; } = ProductId.New();

    // .NET Guid: Requires configuration of either GuidSerializer (via DI), BsonType or GuidRepresentation (via attribute) or property map to work correctly
    [BsonGuidRepresentation(GuidRepresentation.CSharpLegacy)]
    public Guid Guid { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public Category Category { get; set; }

    public string? Summary { get; set; }

    public string? Description { get; set; }

    public string? ImageFile { get; set; }

    public decimal Price { get; set; }
}
