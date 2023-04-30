using MongoDB.Bson.Serialization;

namespace Primitively.MongoDb;

internal static class PrimitivelyBson
{
    public static TPrimitive Deserialize<TPrimitive>(BsonDeserializationContext context)
        where TPrimitive : struct, IPrimitive
    {
        object value = typeof(TPrimitive) switch
        {
            IByte => Convert.ToByte(context.Reader.ReadInt32()),
            ISByte => Convert.ToSByte(context.Reader.ReadInt32()),
            IShort => Convert.ToInt16(context.Reader.ReadInt32()),
            IUShort => Convert.ToUInt16(context.Reader.ReadInt32()),
            IInt => context.Reader.ReadInt32(),
            IUInt => Convert.ToUInt32(context.Reader.ReadInt32()),
            ILong => context.Reader.ReadInt64(),
            IULong => Convert.ToUInt64(context.Reader.ReadInt64()),
            IDateOnly => DateOnly.FromDateTime(Convert.ToDateTime(context.Reader.ReadDateTime())),
            IGuid => Guid.Parse(context.Reader.ReadString()),
            IString => context.Reader.ReadString(),
            _ => new NotImplementedException()
        };

        return (TPrimitive)Activator.CreateInstance(typeof(TPrimitive), value)!;
    }

    public static void Serialize<TPrimitive>(BsonSerializationContext context, TPrimitive value)
        where TPrimitive : struct, IPrimitive
    {
        switch (value)
        {
            case ILong:
            case IULong:
                {
                    _ = long.TryParse(value.ToString(), out var num);
                    context.Writer.WriteInt64(num);
                    break;
                }
            case IInteger: // Store all other numbers as int32 e.g. IByte, ISByte, IInt, IUInt
                {
                    _ = int.TryParse(value.ToString(), out var num);
                    context.Writer.WriteInt32(num);
                    break;
                }

            case IDateOnly: // Store DataOnly as ticks
                {
                    _ = DateTime.TryParse(value.ToString(), out var dt);
                    context.Writer.WriteDateTime(dt.Ticks);
                    break;
                }

            case IGuid:
            case IString:
                {
                    context.Writer.WriteString(value.ToString());
                    break;
                }

            default:
                throw new NotImplementedException();
        }
    }
}
