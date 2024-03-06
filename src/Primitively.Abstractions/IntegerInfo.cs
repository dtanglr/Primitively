namespace Primitively;

/// <summary>
/// The <see cref="IntegerInfo"/> abstract class represents metadata properties common to all source generated Primitively integer types.
/// </summary>
/// <param name="DataType">The <see cref="DataType"/> enum representation of the Primitively type.</param>
/// <param name="Type">The .NET type of the Primitively type.</param>
/// <param name="ValueType">The .NET type of the encapsulated value.</param>
/// <param name="Example">An optional example of the integer.</param>
/// <param name="CreateFrom">A function that creates an instance of the Primitively type from a string.</param>
/// <param name="Minimum">The minimum value that can be set on the source generated Primitively type.</param>
/// <param name="Maximum">The maximum value that can be set on the source generated Primitively type.</param>
public record IntegerInfo(DataType DataType, Type Type, Type ValueType, string? Example, Func<string?, IPrimitive> CreateFrom, decimal Minimum, decimal Maximum)
    : PrimitiveInfo(DataType, Type, ValueType, Example, CreateFrom);
