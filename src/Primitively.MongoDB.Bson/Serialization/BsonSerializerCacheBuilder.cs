namespace Primitively.MongoDB.Bson.Serialization;

/// <summary>
/// Fluent builder class to replace the default Primitively serializers with custom implementations
/// </summary>
public class BsonSerializerCacheBuilder
{
    internal BsonSerializerCacheBuilder() { }

    /// <summary>
    /// Replace the default Primitively Bson Serializer with a custom one.
    /// </summary>
    /// <param name="dataType"></param>
    /// <param name="serializerType"></param>
    /// <returns>BsonSerializerBuilder</returns>
    public BsonSerializerCacheBuilder SetSerializer(DataType dataType, Type serializerType)
    {
        BsonSerializerCache.Set(dataType, serializerType);

        return this;
    }
}
