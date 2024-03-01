namespace Primitively;

/// <summary>
/// A static class to provide extension methods on the <see cref="Type"/> class.
/// </summary>
/// <remarks>
/// This provides the method <see cref="IsAssignableTo(Type, Type)"/> which is missing from .NET Standard.
/// </remarks>
public static class TypeExtensions
{
    /// <summary>
    /// Determines whether the current type can be assigned to a variable of the specified targetType.
    /// </summary>
    /// <param name="type">The current type.</param>
    /// <param name="baseType">The type to compare with the current type.</param>
    /// <returns>
    /// true if any of the following conditions is true: 
    /// - The current instance and targetType represent the same type. 
    /// - The current type is derived either directly or indirectly from targetType. 
    /// - targetType is an interface that the current type implements. 
    /// - The current type is a generic type parameter, and targetType represents one of the constraints of the current type. 
    /// - The current type represents a value type, and targetType represents <see cref="Nullable{T}"/>. 
    /// false if none of these conditions are true, or if targetType is null.
    /// </returns>
    public static bool IsAssignableTo(this Type type, Type baseType)
    {
        return baseType.IsAssignableFrom(type);
    }
}
