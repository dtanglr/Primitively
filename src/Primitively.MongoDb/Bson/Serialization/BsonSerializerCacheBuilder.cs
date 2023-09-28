namespace Primitively.MongoDb.Bson.Serialization;

public class BsonSerializerCacheBuilder
{
    internal BsonSerializerCacheBuilder() { }

    /// <summary>
    /// Replace the default Primitively Bson Serializer with a custom one.
    /// </summary>
    /// <param name="dataType"></param>
    /// <param name="serializerType"></param>
    /// <returns>BsonSerializerBuilder</returns>
    public BsonSerializerCacheBuilder ForDataType(DataType dataType, Type serializerType)
    {
        BsonSerializerCache.Set(dataType, serializerType);

        return this;
    }
}
