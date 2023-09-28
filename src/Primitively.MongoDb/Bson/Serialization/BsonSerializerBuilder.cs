namespace Primitively.MongoDb.Bson.Serialization;

/// <summary>
/// Fluent builder class to register MongoDB Bson serializers for Primitively source generated types
/// </summary>
public class BsonSerializerBuilder
{
    private readonly BsonSerializerCacheBuilder _cacheBuilder = new();
    private readonly BsonSerializerRegisterBuilder _registerBuilder = new();

    public BsonSerializerRegisterBuilder RegisterBsonSerializer() => _registerBuilder;

    public BsonSerializerCacheBuilder UseBsonSerializer() => _cacheBuilder;
}
