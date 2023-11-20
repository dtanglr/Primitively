namespace Catalog.Api.Entities;

public readonly record struct Category
{
    public CategoryId CategoryId { get; init; }

    public string? Name { get; init; }
}
