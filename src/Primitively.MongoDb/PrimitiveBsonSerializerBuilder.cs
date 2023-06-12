using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDb;

/// <summary>
/// Fluent builder class to register MongoDB Bson serializers for Primitively source generated types
/// </summary>
public class PrimitiveBsonSerializerBuilder : IPrimitiveBsonSerializerBuilder
{
    private static readonly List<Type> _primitiveTypes = new();

    /// <summary>
    /// Automatically register a nullable and a non-nullable Bson serializer for the provided Primitively type
    /// </summary>
    public IPrimitiveBsonSerializerBuilder AddBsonSerializerFor<T>() where T : struct, IPrimitive
    {
        // Generate an nullable and non-nullable Bson serializer for the Primitively type
        RegisterBsonSerializer(typeof(T));

        return this;
    }

    /// <summary>
    /// Automatically register nullable and non-nullable Bson serializers for all the Primitively types contained in the source generated repository
    /// </summary>
    public IPrimitiveBsonSerializerBuilder AddBsonSerializersFor<T>() where T : class, IPrimitiveRepository, new()
    {
        // Get a list of the source generated Primitively types
        var primitiveRepository = new T();
        var primitiveTypes = primitiveRepository
            .GetTypes()
            .Select(p => p.Type)
            .ToArray();

        foreach (var primitiveType in primitiveTypes)
        {
            RegisterBsonSerializer(primitiveType);
        }

        return this;
    }

    private static void RegisterBsonSerializer(Type primitiveType)
    {
        // Check that Primitive types has not been handled already
        if (_primitiveTypes.Contains(primitiveType))
        {
            return;
        }

        // Add the type to a collection to provide a data source for the above check
        _primitiveTypes.Add(primitiveType);

        // Construct a Primitively serializer of the Primitively type
        var serializerType = typeof(PrimitiveBsonSerializer<>).MakeGenericType(primitiveType);

        // Create a Primitively serializer instance
        var serializerInstance = CreateInstance(serializerType);

        // Register a Serializer for the Primitively type
        BsonSerializer.TryRegisterSerializer(primitiveType, serializerInstance);

        // Construct a nullable version of the Primitively type
        var nullablePrimitiveType = typeof(Nullable<>).MakeGenericType(primitiveType);

        // Create a Nullable Primitively serializer instance
        var nullableSerializerInstance = NullableSerializer.Create(serializerInstance);

        // Register a NullableSerializer for a nullable version of the Primitively type
        BsonSerializer.TryRegisterSerializer(nullablePrimitiveType, nullableSerializerInstance);
    }

    private static IBsonSerializer CreateInstance(Type serializerType)
    {
        var instance = Activator.CreateInstance(serializerType)
            ?? throw new Exception($"Unable to create serializer instance for type: {serializerType.FullName}");

        return (IBsonSerializer)instance;
    }
}
