namespace Primitively;

public static class TypeExtensions
{
    public static bool IsAssignableTo(this Type type, Type baseType)
    {
        return baseType.IsAssignableFrom(type);
    }
}
