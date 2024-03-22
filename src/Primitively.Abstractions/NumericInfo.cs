namespace Primitively;

/// <summary>
/// This abstract class represents metadata properties common to all source generated Primitively integer types.
/// </summary>
/// <param name="DataType">The <see cref="DataType"/> enum representation of the Primitively type.</param>
/// <param name="Type">The .NET type of the Primitively type.</param>
/// <param name="ValueType">The .NET type of the encapsulated value.</param>
/// <param name="Example">An optional example of the integer.</param>
/// <param name="CreateFrom">A function that creates an instance of the Primitively type from a string.</param>
public abstract record NumericInfo(DataType DataType, Type Type, Type ValueType, string? Example, Func<string?, IPrimitive> CreateFrom)
    : PrimitiveInfo(DataType, Type, ValueType, Example, CreateFrom);
