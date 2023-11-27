using System.Collections.Concurrent;

namespace Primitively.MongoDB.Bson.Serialization.Options;

internal static class BsonSerializerOptionsCache
{
    private static readonly ConcurrentDictionary<DataType, IBsonSerializerOptions> _items = new(GetAll().ToDictionary(o => o.DataType, o => o));

    public static IBsonSerializerOptions Get(DataType dataType) => _items[dataType];

    private static IEnumerable<IBsonSerializerOptions> GetAll()
    {
        yield return new BsonIByteSerializerOptions();
        yield return new BsonIDateOnlySerializerOptions();
        yield return new BsonIGuidSerializerOptions();
        yield return new BsonIIntSerializerOptions();
        yield return new BsonILongSerializerOptions();
        yield return new BsonISByteSerializerOptions();
        yield return new BsonIShortSerializerOptions();
        yield return new BsonIStringSerializerOptions();
        yield return new BsonIUIntSerializerOptions();
        yield return new BsonIULongSerializerOptions();
        yield return new BsonIUShortSerializerOptions();
    }
}
