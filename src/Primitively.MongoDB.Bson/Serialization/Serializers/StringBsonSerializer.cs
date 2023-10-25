using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDB.Bson.Serialization.Serializers;

public class StringBsonSerializer<TPrimitive> : SerializerBase<TPrimitive>
    where TPrimitive : struct, IString
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TPrimitive value)
    {
        context.Writer.WriteString(value.Value);
    }

    public override TPrimitive Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();

            // Return default if null
            return new();
        }

        var value = context.Reader.ReadString();

        return (TPrimitive)Activator.CreateInstance(typeof(TPrimitive), value)!;
    }
}
