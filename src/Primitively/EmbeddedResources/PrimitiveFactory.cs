namespace PRIMITIVE_NAMESPACE;

public partial class PrimitiveFactory : global::Primitively.IPrimitiveFactory
{
#nullable enable
    public global::Primitively.IPrimitive? Create(global::System.Type type, string? value)
    {
        var created = TryCreate(type, value, out var result);

        if (!created)
        {
            throw new global::System.ArgumentOutOfRangeException(nameof(type), $"There is no match for '{type.Name}'");
        }

        return result;
    }

    public bool TryCreate(global::System.Type type, string? value, out global::Primitively.IPrimitive? result)
    {
        result = type.FullName switch
        {
PRIMITIVE_FACTORY_CASE_STATEMENTS
            _ => null
        };

        return result is not null;
    }
#nullable disable
}
