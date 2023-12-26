using MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson.Serialization;

public interface IBsonSerializerManager
{
    IBsonSerializer LookupSerializer(Type type);
    bool TryRegisterSerializer(Type type, IBsonSerializer serializer);
}
