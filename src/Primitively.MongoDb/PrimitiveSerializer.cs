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

            // TODO: Revisit this and decide whether to throw an error instead, rather than return a
            // struct in it's default state. 
            return new();
        }

        return ((IPrimitiveSerializer<TPrimitive>)this).Deserialize(context);
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TPrimitive value)
    {
        ((IPrimitiveSerializer<TPrimitive>)this).Serialize(context, value);
    }
}
