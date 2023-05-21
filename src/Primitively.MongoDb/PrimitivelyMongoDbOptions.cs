namespace Primitively.MongoDb;

public record PrimitivelyMongoDbOptions
{
    public IPrimitiveBsonSerializerBuilder BsonSerializerBuilder { get; init; } = new PrimitiveBsonSerializerBuilder();
}
