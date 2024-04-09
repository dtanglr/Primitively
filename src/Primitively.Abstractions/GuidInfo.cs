namespace Primitively;

/// <summary>
/// This class contains metadata about a Primitively <see cref="IGuid"/> type.
/// </summary>
/// <param name="Type">The .NET type of the Primitively type.</param>
/// <param name="Example">An optional example of the GUID in string format.</param>
/// <param name="CreateFrom">A function that creates an instance of the Primitively type from a string.</param>
/// <param name="Specifier">The specifier used to generate the GUID.</param>
/// <param name="Length">The length of the GUID in string format.</param>
public sealed record GuidInfo(
    Type Type,
    string? Example,
    Func<string?, IPrimitive> CreateFrom,
    Specifier Specifier,
    int Length)
    : PrimitiveInfo(DataType.Guid, Type, typeof(Guid), Example, CreateFrom)
{
    /// <summary>
    /// The format of the GUID.
    /// </summary>
    /// <value>
    /// The <see cref="Specifier"/> as a string.
    /// </value>
    public string Format => Specifier.ToString();
}
