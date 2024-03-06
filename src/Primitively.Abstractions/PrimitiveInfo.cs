namespace Primitively;

/// <summary>
/// The <see cref="PrimitiveInfo"/> abstract class represents metadata properties common to all source generated Primitively types.
/// </summary>
/// <param name="DataType">The <see cref="Primitively.DataType"/> enum representation of the Primitively type.</param>
/// <param name="Type">The .NET type of the Primitively type.</param>
/// <param name="ValueType">The .NET type of the encapsulated value.</param>
/// <param name="Example">An optional example of the Primitively type in string format.</param>
/// <param name="CreateFrom">A function that creates an instance of the Primitively type from a string.</param>
public abstract record PrimitiveInfo(
    DataType DataType,
    Type Type,
    Type ValueType,
    string? Example,
    Func<string?, IPrimitive> CreateFrom);
