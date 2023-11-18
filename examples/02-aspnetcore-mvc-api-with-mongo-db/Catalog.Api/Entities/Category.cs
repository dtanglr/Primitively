namespace Catalog.Api.Entities;

public record struct Category
{
    public CategoryId CategoryId { get; init; }

    public string? Name { get; init; }
}
