using MongoDB.Bson.Serialization;
using Primitively.MongoDb.Bson.Serialization.Serializers;

namespace Primitively.MongoDb.Bson.Serialization;

public class PrimitiveBsonSerializationProvider : IBsonSerializationProvider
{
    public IBsonSerializer? GetSerializer(Type type)
    {
        if (type.IsAssignableTo(typeof(IPrimitive)))
        {
            // Construct a Primitively serializer of the Primitively type
            var serializerType = GetSerializerType(type);

            // Create a Primitively serializer instance
            var serializerInstance = Activator.CreateInstance(serializerType)
                ?? throw new Exception($"Unable to create serializer instance for type: {serializerType.FullName}");

            return (IBsonSerializer)serializerInstance;
        }

        return null;
    }

    private static Type GetSerializerType(Type type) => type switch
    {
        _ when type.IsAssignableTo(typeof(IGuid)) => typeof(GuidBsonSerializer<>).MakeGenericType(type),
        _ when type.IsAssignableTo(typeof(IInteger)) => GetIntegerSerializerType(type),
        _ when type.IsAssignableTo(typeof(IString)) => typeof(StringBsonSerializer<>).MakeGenericType(type),
        _ when type.IsAssignableTo(typeof(IDateOnly)) => typeof(DateOnlyBsonSerializer<>).MakeGenericType(type),
        _ => throw new NotImplementedException()
    };

    private static Type GetIntegerSerializerType(Type type) => type switch
    {
        _ when type.IsAssignableTo(typeof(IInt)) => typeof(IntBsonSerializer<>).MakeGenericType(type),
        _ when type.IsAssignableTo(typeof(IUInt)) => typeof(UIntBsonSerializer<>).MakeGenericType(type),
        _ when type.IsAssignableTo(typeof(ILong)) => typeof(LongBsonSerializer<>).MakeGenericType(type),
        _ when type.IsAssignableTo(typeof(IULong)) => typeof(ULongBsonSerializer<>).MakeGenericType(type),
        _ when type.IsAssignableTo(typeof(IShort)) => typeof(ShortBsonSerializer<>).MakeGenericType(type),
        _ when type.IsAssignableTo(typeof(IUShort)) => typeof(UShortBsonSerializer<>).MakeGenericType(type),
        _ when type.IsAssignableTo(typeof(IByte)) => typeof(ByteBsonSerializer<>).MakeGenericType(type),
        _ when type.IsAssignableTo(typeof(ISByte)) => typeof(SByteBsonSerializer<>).MakeGenericType(type),
        _ => throw new NotImplementedException()
    };
}
