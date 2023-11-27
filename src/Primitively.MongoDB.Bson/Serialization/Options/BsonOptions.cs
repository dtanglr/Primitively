namespace Primitively.MongoDB.Bson.Serialization.Options;

public class BsonOptions
{
    public bool RegisterSerializersForEachTypeInRegistry { get; set; } = true;
    public BsonIByteSerializerOptions BsonIByteSerializerOptions { get; set; } = new BsonIByteSerializerOptions();
    public BsonIDateOnlySerializerOptions BsonIDateOnlySerializerOptions { get; set; } = new BsonIDateOnlySerializerOptions();
    public BsonIGuidSerializerOptions BsonIGuidSerializerOptions { get; set; } = new BsonIGuidSerializerOptions();
    public BsonIIntSerializerOptions BsonIIntSerializerOptions { get; set; } = new BsonIIntSerializerOptions();
    public BsonILongSerializerOptions BsonILongSerializerOptions { get; set; } = new BsonILongSerializerOptions();
    public BsonISByteSerializerOptions BsonISByteSerializerOptions { get; set; } = new BsonISByteSerializerOptions();
    public BsonIShortSerializerOptions BsonIShortSerializerOptions { get; set; } = new BsonIShortSerializerOptions();
    public BsonIStringSerializerOptions BsonIStringSerializerOptions { get; set; } = new BsonIStringSerializerOptions();
    public BsonIUIntSerializerOptions BsonIUIntSerializerOptions { get; set; } = new BsonIUIntSerializerOptions();
    public BsonIULongSerializerOptions BsonIULongSerializerOptions { get; set; } = new BsonIULongSerializerOptions();
    public BsonIUShortSerializerOptions BsonIUShortSerializerOptions { get; set; } = new BsonIUShortSerializerOptions();

    internal Dictionary<DataType, IBsonSerializerOptions> GetBsonSerializerOptions() => GetAll().ToDictionary(o => o.DataType, o => o);

    public static Type GetSerializerType<TPrimitive>(Type serializerType) where TPrimitive : struct, IPrimitive
    {
        // Construct a Primitively serializer of the Primitively type
        return serializerType.IsGenericTypeDefinition ? serializerType.MakeGenericType(typeof(TPrimitive)) : serializerType;
    }

    public static Type GetSerializerType(Type primitiveType, Type serializerType)
    {
        // Construct a Primitively serializer of the Primitively type
        return serializerType.IsGenericTypeDefinition ? serializerType.MakeGenericType(primitiveType) : serializerType;
    }

    private IEnumerable<IBsonSerializerOptions> GetAll()
    {
        yield return BsonIByteSerializerOptions;
        yield return BsonIDateOnlySerializerOptions;
        yield return BsonIGuidSerializerOptions;
        yield return BsonIIntSerializerOptions;
        yield return BsonILongSerializerOptions;
        yield return BsonISByteSerializerOptions;
        yield return BsonIShortSerializerOptions;
        yield return BsonIStringSerializerOptions;
        yield return BsonIUIntSerializerOptions;
        yield return BsonIULongSerializerOptions;
        yield return BsonIUShortSerializerOptions;
    }
}
