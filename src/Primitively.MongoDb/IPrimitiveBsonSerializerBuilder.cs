namespace Primitively.MongoDb;

public interface IPrimitiveBsonSerializerBuilder
{
    IPrimitiveBsonSerializerBuilder AddBsonSerializer(Type primitiveType);
    IPrimitiveBsonSerializerBuilder AddBsonSerializers(IPrimitiveRepository primitiveRepository);
    IPrimitiveBsonSerializerBuilder AddBsonSerializers(params IPrimitiveRepository[] primitiveReposities);
    IPrimitiveBsonSerializerBuilder AddBsonSerializers(params Type[] primitiveTypes);
}
