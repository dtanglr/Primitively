using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDb.Bson.Serialization;

/// <summary>
/// Fluent builder class to register MongoDB Bson serializers for Primitively source generated types
/// </summary>
public class BsonSerializerBuilder
{
    private static readonly List<Type> _primitiveTypes = new();

    private readonly BsonSerializerOptions _options;

    public BsonSerializerBuilder()
    {
        _options = new BsonSerializerOptions();
    }

    public BsonSerializerBuilder(BsonSerializerOptions options)
    {
        _options = options;
    }

    /// <summary>
    /// Automatically register a nullable and a non-nullable Bson serializer for the provided Primitively type
    /// </summary>
    /// <typeparam name="T">IPrimitive</typeparam>
    /// <returns>PrimitiveBsonSerializerBuilder</returns>
    public BsonSerializerBuilder RegisterBsonSerializerForType<T>()
        where T : struct, IPrimitive
    {
        // Get a default instance of the provided Primitively struct type
        var primitive = new T();

        // Generate an nullable and non-nullable Bson serializer for the Primitively struct
        var serializerType = GetSerializerType(_options, primitive.DataType);

        RegisterBsonSerializer(primitive.GetType(), serializerType);

        return this;
    }

    /// <summary>
    /// Automatically register nullable and non-nullable Bson serializers for all the Primitively types contained in the source generated repository
    /// </summary>
    /// <typeparam name="T">IPrimitive</typeparam>
    /// <returns>PrimitiveBsonSerializerBuilder</returns>
    public BsonSerializerBuilder RegisterBsonSerializerForEachTypeIn<T>()
        where T : class, IPrimitiveRepository, new()
    {
        // Get a list of the source generated Primitively types
        var primitiveRepository = new T();

        foreach (var primitiveInfo in primitiveRepository.GetTypes())
        {
            var serializerType = GetSerializerType(_options, primitiveInfo.DataType);

            RegisterBsonSerializer(primitiveInfo.Type, serializerType);
        }

        return this;
    }

    private static Type GetSerializerType(BsonSerializerOptions options, DataType dataType)
    {
        return dataType switch
        {
            DataType.Byte => options.ByteBsonSerializer,
            DataType.DateOnly => options.DateOnlyBsonSerializer,
            DataType.Guid => options.GuidBsonSerializer,
            DataType.Int => options.IntBsonSerializer,
            DataType.Long => options.LongBsonSerializer,
            DataType.SByte => options.SByteBsonSerializer,
            DataType.Short => options.ShortBsonSerializer,
            DataType.String => options.StringBsonSerializer,
            DataType.UInt => options.UIntBsonSerializer,
            DataType.ULong => options.ULongBsonSerializer,
            DataType.UShort => options.UShortBsonSerializer,
            _ => throw new NotImplementedException(),
        };
    }

    private static void RegisterBsonSerializer(Type primitiveType, Type serializerType)
    {
        // Check that Primitive types has not been handled already
        if (_primitiveTypes.Contains(primitiveType))
        {
            return;
        }

        // Add the type to a collection to provide a data source for the above check
        _primitiveTypes.Add(primitiveType);

        // Construct a Primitively serializer of the Primitively type
        var primitiveSerializerType = serializerType.MakeGenericType(primitiveType);

        // Create a Primitively serializer instance
        var primitiveSerializerInstance = CreateSerializerInstance(primitiveSerializerType);

        // Register a Serializer for the Primitively type
        BsonSerializer.TryRegisterSerializer(primitiveType, primitiveSerializerInstance);

        // Construct a nullable version of the Primitively type
        var nullablePrimitiveType = typeof(Nullable<>).MakeGenericType(primitiveType);

        // Create a Nullable Primitively serializer instance
        var nullablePrimitiveSerializerInstance = NullableSerializer.Create(primitiveSerializerInstance);

        // Register a NullableSerializer for a nullable version of the Primitively type
        BsonSerializer.TryRegisterSerializer(nullablePrimitiveType, nullablePrimitiveSerializerInstance);
    }

    private static IBsonSerializer CreateSerializerInstance(Type serializerType)
    {
        var instance = Activator.CreateInstance(serializerType)
            ?? throw new Exception($"Unable to create serializer instance for type: {serializerType.FullName}");

        return (IBsonSerializer)instance;
    }
}
