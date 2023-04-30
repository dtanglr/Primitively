using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDb;

public class NullablePrimitiveSerializer<TPrimitive> : SerializerBase<TPrimitive?>, IPrimitiveSerializer<TPrimitive>
    where TPrimitive : struct, IPrimitive
{
    public override TPrimitive? Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();

            return null;
        }

        return ((IPrimitiveSerializer<TPrimitive>)this).Deserialize(context);
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TPrimitive? value)
    {
        if (value == null)
        {
            context.Writer.WriteNull();

            return;
        }

        ((IPrimitiveSerializer<TPrimitive>)this).Serialize(context, (TPrimitive)value);
    }
}
