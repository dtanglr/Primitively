using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDb;

public class PrimitiveSerializer<TPrimitive> : SerializerBase<TPrimitive>, IPrimitiveSerializer<TPrimitive>
    where TPrimitive : struct, IPrimitive
{
    public override TPrimitive Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();

            return new();
        }

        return ((IPrimitiveSerializer<TPrimitive>)this).Deserialize(context);
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TPrimitive value)
    {
        ((IPrimitiveSerializer<TPrimitive>)this).Serialize(context, value);
    }
}
