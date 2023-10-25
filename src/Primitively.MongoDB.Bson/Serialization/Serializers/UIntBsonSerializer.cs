using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDB.Bson.Serialization.Serializers;

public class UIntBsonSerializer<TPrimitive> : SerializerBase<TPrimitive>
    where TPrimitive : struct, IUInt
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TPrimitive value)
    {
        context.Writer.WriteInt64(value.Value);
    }

    public override TPrimitive Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();

            // Return default if null
            return new();
        }

        var value = Convert.ToUInt32(context.Reader.ReadInt64());

        return (TPrimitive)Activator.CreateInstance(typeof(TPrimitive), value)!;
    }
}
