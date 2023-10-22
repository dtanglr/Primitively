namespace PRIMITIVE_NAMESPACE;

public static class PrimitiveLibrary
{
    public static IPrimitiveRepository Respository { get; } = new PrimitiveRepository();
    public static IPrimitiveFactory Factory { get; } = new PrimitiveFactory();
    public static bool HasTypes => PRIMITIVE_LIBRARY_HAS_TYPES;
}
