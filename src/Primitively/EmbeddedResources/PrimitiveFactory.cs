public partial class PrimitiveFactory : Primitively.IPrimitiveFactory
{
    public Primitively.IPrimitive Create(System.Type type, string value)
    {
        var created = TryCreate(type, value, out var result);

        if (!created)
        {
            throw new System.ArgumentOutOfRangeException(nameof(type), $"There is no match for '{type.Name}'");
        }

        return result;
    }

    public bool TryCreate(System.Type type, string value, out Primitively.IPrimitive result)
    {
        result = type.Name switch
        {
PRIMITIVE_FACTORY_CASE_STATEMENTS
            _ => null
        };

        return result is not null;
    }
}
