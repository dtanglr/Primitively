﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDb.Bson.Serialization.Serializers;

public class ULongBsonSerializer<TPrimitive> : SerializerBase<TPrimitive>
    where TPrimitive : struct, IULong
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TPrimitive value)
    {
        // Store as decimal128 because an unsigned long can exceed the Mongo int64 maximum
        context.Writer.WriteDecimal128(new Decimal128(value.Value));
    }

    public override TPrimitive Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();

            // Return default if null
            return new();
        }

        var value = Decimal128.ToUInt64(context.Reader.ReadDecimal128());

        return (TPrimitive)Activator.CreateInstance(typeof(TPrimitive), value)!;
    }
}