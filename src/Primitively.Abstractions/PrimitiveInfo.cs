namespace Primitively;

/// <summary>
/// This represents a class that contains metadata properties common to all source generated Primitively types.
/// </summary>
/// <param name="DataType">The Primitively type's data type enum value</param>
/// <param name="Type">The Primitively type</param>
/// <param name="ValueType">The Primitively type's value type</param>
/// <param name="Example">An example value of this Primitively type</param>
/// <param name="CreateFrom">A factory method to instantiate an instance of this Primitively type</param>
public abstract record PrimitiveInfo(
    DataType DataType,
    Type Type,
    Type ValueType,
    string? Example,
    Func<string?, IPrimitive> CreateFrom);
