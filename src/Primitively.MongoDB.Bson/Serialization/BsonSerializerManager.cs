using MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson.Serialization;

public class BsonSerializerManager : IBsonSerializerManager
{
    public IBsonSerializer LookupSerializer(Type type) =>
        BsonSerializer.LookupSerializer(type);

    public bool TryRegisterSerializer(Type type, IBsonSerializer serializer) =>
        BsonSerializer.TryRegisterSerializer(type, serializer);
}
