﻿using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDb.Bson.Serialization.Serializers;

public class ByteBsonSerializer<TPrimitive> : SerializerBase<TPrimitive>
    where TPrimitive : struct, IPrimitive, IByte
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TPrimitive value)
    {
        _ = int.TryParse(value.ToString(), out var num);
        context.Writer.WriteInt32(num);
    }

    public override TPrimitive Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();

            // Return default if null
            return new();
        }

        var value = Convert.ToByte(context.Reader.ReadInt32());

        return (TPrimitive)Activator.CreateInstance(typeof(TPrimitive), value)!;
    }
}
