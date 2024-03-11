namespace PRIMITIVE_NAMESPACE;

public readonly record struct PrimitiveLibrary
{
    public static global::Primitively.IPrimitiveRepository Repository { get; } = new PrimitiveRepository();
    public static global::Primitively.IPrimitiveFactory Factory { get; } = new PrimitiveFactory();
    public static bool HasTypes => PRIMITIVE_LIBRARY_HAS_TYPES;
}
