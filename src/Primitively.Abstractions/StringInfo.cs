namespace Primitively;

/// <summary>
/// This class contains metadata about a Primitively <see cref="IString"/> type.
/// </summary>
/// <param name="Type">The Primitively type.</param>
/// <param name="Example">An optional example of the string.</param>
/// <param name="CreateFrom">A function that creates an instance of the Primitively type from a string.</param>
/// <param name="Format">An optional format of the string.</param>
/// <param name="Pattern">An optional regular expression pattern used to validate the encapsulated value.</param>
/// <param name="MinLength">The minimum length of the string representation of the encapsulated primitive value.</param>
/// <param name="MaxLength">The maximum length of the string representation of the encapsulated primitive value.</param>
public record StringInfo(
    Type Type,
    string? Example,
    Func<string?, IPrimitive> CreateFrom,
    string? Format,
    string? Pattern,
    int MinLength,
    int MaxLength)
    : PrimitiveInfo(DataType.String, Type, typeof(string), Example, CreateFrom);
