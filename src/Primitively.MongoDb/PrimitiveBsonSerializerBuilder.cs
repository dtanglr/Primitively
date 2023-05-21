using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDb;

/// <summary>
/// Fluent builder class to register MongoDB Bson serializers for Primitively source generated types
/// </summary>
public class PrimitiveBsonSerializerBuilder : IPrimitiveBsonSerializerBuilder
{
    /// <summary>
    /// Automatically register MongoDB nullable and non-nullable Bson serializers for the provided Primitively types contained in the source generated repositories
    /// </summary>
    /// <param name="primitiveRepositories">One or more source generated Primitive Repository classes</param>
    public IPrimitiveBsonSerializerBuilder AddBsonSerializers(params IPrimitiveRepository[] primitiveRepositories)
    {
        if (!primitiveRepositories.Any())
        {
            return this;
        }

        // Get a list of the source generated Primitively types
        var primitiveTypes = primitiveRepositories
            .SelectMany(r => r.GetTypes())
            .Select(p => p.Type)
            .ToArray();

        return AddBsonSerializers(primitiveTypes);
    }

    /// <summary>
    /// Automatically register MongoDB nullable and non-nullable Bson serializers for the provided Primitively types contained in the source generated repository
    /// </summary>
    /// <param name="primitiveRepository">A source generated Primitive Repository classes</param>
    public IPrimitiveBsonSerializerBuilder AddBsonSerializers(IPrimitiveRepository primitiveRepository)
    {
        if (primitiveRepository is null)
        {
            throw new ArgumentNullException(nameof(primitiveRepository));
        }

        // Get a list of the source generated Primitively types
        var primitiveTypes = primitiveRepository
            .GetTypes()
            .Select(p => p.Type)
            .ToArray();

        return AddBsonSerializers(primitiveTypes);
    }

    /// <summary>
    /// Automatically register MongoDB nullable and non-nullable Bson serializers for the provided Primitively types
    /// </summary>
    /// <param name="primitiveTypes">One or more Primitively types</param>
    public IPrimitiveBsonSerializerBuilder AddBsonSerializers(params Type[] primitiveTypes)
    {
        static bool Filter(Type t) => t.IsValueType && t.IsAssignableTo(typeof(IPrimitive));

        if (!primitiveTypes.Any(Filter))
        {
            return this;
        }

        // Generate an nullable and non-nullable Bson serializer for each Primitively type
        var types = primitiveTypes.Where(Filter).Distinct();

        foreach (var primitiveType in types)
        {
            RegisterBsonSerializer(primitiveType);
        }

        return this;
    }

    /// <summary>
    /// Automatically register MongoDB a nullable and a non-nullable Bson serializer for the provided Primitively type
    /// </summary>
    /// <param name="primitiveType">A Primitively type</param>
    public IPrimitiveBsonSerializerBuilder AddBsonSerializer(Type primitiveType)
    {
        if (primitiveType is null)
        {
            throw new ArgumentNullException(nameof(primitiveType));
        }

        if (!primitiveType.IsAssignableTo(typeof(IPrimitive)))
        {
            throw new ArgumentException($"The provided type does not implement: {nameof(IPrimitive)}", nameof(primitiveType));
        }

        // Generate an nullable and non-nullable Bson serializer for the Primitively type
        RegisterBsonSerializer(primitiveType);

        return this;
    }

    private static void RegisterBsonSerializer(Type primitiveType)
    {
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
