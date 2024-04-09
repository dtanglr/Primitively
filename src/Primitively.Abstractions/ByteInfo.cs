namespace Primitively;

/// <summary>
/// This class represents metadata properties common to all source generated Primitively numeric types.
/// </summary>
/// <param name="Type">The .NET type of the Primitively type.</param>
/// <param name="Example">An optional example of the integer.</param>
/// <param name="CreateFrom">A function that creates an instance of the Primitively type from a string.</param>
/// <param name="Minimum">The minimum value that can be set on the source generated Primitively type.</param>
/// <param name="Maximum">The maximum value that can be set on the source generated Primitively type.</param>
public sealed record ByteInfo(
    Type Type,
    string? Example,
    Func<string?, IPrimitive> CreateFrom,
    byte Minimum,
    byte Maximum)
    : NumericInfo<byte>(DataType.Byte, Type, Example, CreateFrom, Minimum, Maximum);
