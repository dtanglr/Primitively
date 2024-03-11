namespace Primitively;

/// <summary>
/// This static class that provides extension methods for the <see cref="Type"/> class.
/// </summary>
/// <remarks>
/// This class provides the method <see cref="IsAssignableTo(Type, Type)"/> which is missing from .NET Standard.
/// </remarks>
public static class TypeExtensions
{
    /// <summary>
    /// Determines whether an instance of the current <see cref="Type"/> can be assigned to a variable of the specified targetType.
    /// </summary>
    /// <param name="type">The current type.</param>
    /// <param name="targetType">The type to compare with the current type.</param>
    /// <returns>
    /// <c>true</c> if any of the following conditions is true: 
    /// - The current instance and targetType represent the same type. 
    /// - The current type is derived either directly or indirectly from targetType. 
    /// - targetType is an interface that the current type implements. 
    /// - The current type is a generic type parameter, and targetType represents one of the constraints of the current type. 
    /// - The current type represents a value type, and targetType represents <see cref="Nullable{T}"/>. 
    /// <c>false</c> if none of these conditions are true, or if targetType is null.
    /// </returns>
    public static bool IsAssignableTo(this Type type, Type targetType)
    {
        return targetType.IsAssignableFrom(type);
    }
}
