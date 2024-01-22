namespace PRIMITIVE_NAMESPACE;

public readonly record struct PrimitiveLibrary
{
    public static Primitively.IPrimitiveRepository Respository { get; } = new PrimitiveRepository();
    public static Primitively.IPrimitiveFactory Factory { get; } = new PrimitiveFactory();
    public static bool HasTypes => PRIMITIVE_LIBRARY_HAS_TYPES;
}
