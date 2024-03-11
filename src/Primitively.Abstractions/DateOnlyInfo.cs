namespace Primitively;

/// <summary>
/// This class contains metadata about a Primitively <see cref="IDateOnly"/> type.
/// </summary>
/// <param name="Type">The .NET type of the Primitively type.</param>
/// <param name="ValueType">The .NET type of the encapsulated value.</param>
/// <param name="Example">An optional example of the date.</param>
/// <param name="CreateFrom">A function that creates an instance of the Primitively type from a string.</param>
/// <param name="Format">The format of the date.</param>
/// <param name="Length">The length of the date.</param>
public record DateOnlyInfo(Type Type, Type ValueType, string? Example, Func<string?, IPrimitive> CreateFrom, string Format, int Length)
    : PrimitiveInfo(DataType.DateOnly, Type, ValueType, Example, CreateFrom);
