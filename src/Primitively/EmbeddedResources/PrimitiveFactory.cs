public static class PrimitiveFactory
{
    public static Primitively.IPrimitive Create(System.Type modelType, string value) => modelType.Name switch
    {
PRIMITIVE_FACTORY_CASE_STATEMENTS
        _ => throw new System.ArgumentOutOfRangeException(nameof(modelType), $"There is no match in the switch expression for '{modelType.Name}'")
    };
}
