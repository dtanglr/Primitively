using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDb.Bson.Serialization;

public class BsonSerializerRegisterBuilder
{
    private static readonly List<Type> _primitiveTypes = new();

    internal BsonSerializerRegisterBuilder() { }

    /// <summary>
    /// Automatically register a nullable and a non-nullable Bson serializer for the provided Primitively type
    /// </summary>
    /// <typeparam name="TPrimitive">IPrimitive</typeparam>
    /// <returns>BsonSerializerBuilder</returns>
    /// <remarks>
    /// The type of serializer used is governed by the BsonSerializerOptions. If a serializer for the given type 
    /// is already registered, subsequent attempts for the same type will be ignored.
    /// </remarks>
    public BsonSerializerRegisterBuilder ForType<TPrimitive>()
        where TPrimitive : struct, IPrimitive
    {
        // Get a default instance of the provided Primitively struct type
        var primitive = new TPrimitive();

        // Generate an nullable and non-nullable Bson serializer for the Primitively struct
        var serializerType = BsonSerializerCache.Get(primitive.DataType);

        RegisterBsonSerializer(typeof(TPrimitive), serializerType);

        return this;
    }

    /// <summary>
    /// Automatically register a nullable and a non-nullable Bson serializer for the provided Primitively type
    /// </summary>
    /// <typeparam name="TPrimitive">IPrimitive</typeparam>
    /// <typeparam name="TBsonSerializer">IBsonSerializer</typeparam>
    /// <returns>BsonSerializerBuilder</returns>
    /// <remarks>
    /// The type of serializer used is governed by the TBsonSerializer type parameter.  This allows for
    /// individual primitive types to have their own serialiser, rather than the one defined in BsonSerializerOptions.
    /// If a serializer for the given type is already registered, subsequent attempts for the same type will be ignored.
    /// </remarks>
    public BsonSerializerRegisterBuilder ForType<TPrimitive, TBsonSerializer>()
        where TPrimitive : struct, IPrimitive
        where TBsonSerializer : class, IBsonSerializer<TPrimitive>
    {
        RegisterBsonSerializer(typeof(TPrimitive), typeof(TBsonSerializer));

        return this;
    }

    /// <summary>
    /// Automatically register nullable and non-nullable Bson serializers for all the Primitively types contained 
    /// in the source generated repository
    /// </summary>
    /// <typeparam name="TPrimitiveRepository">IPrimitiveRepository</typeparam>
    /// <returns>BsonSerializerBuilder</returns>
    /// <remarks>
    /// The type of serializer used is governed by the BsonSerializerOptions. 
    /// If a serializer for the given type is already registered, subsequent attempts for the same type will be ignored.
    /// To use a custom serializer for one or more types in a repository. Simply register them individually 
    /// first before calling this method.
    /// </remarks>
    public BsonSerializerRegisterBuilder ForEachTypeIn<TPrimitiveRepository>()
        where TPrimitiveRepository : class, IPrimitiveRepository, new()
    {
        // Get a list of the source generated Primitively types
        var primitiveRepository = new TPrimitiveRepository();

        foreach (var primitiveInfo in primitiveRepository.GetTypes())
        {
            var serializerType = BsonSerializerCache.Get(primitiveInfo.DataType);

            RegisterBsonSerializer(primitiveInfo.Type, serializerType);
        }

        return this;
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
        var primitiveSerializerType = serializerType.IsGenericTypeDefinition ? serializerType.MakeGenericType(primitiveType) : serializerType;

        // Create a Primitively serializer instance
        var primitiveSerializerInstance = (IBsonSerializer)Activator.CreateInstance(primitiveSerializerType)!;

        // Register a Serializer for the Primitively type
        BsonSerializer.TryRegisterSerializer(primitiveType, primitiveSerializerInstance);

        // Construct a nullable version of the Primitively type
        var nullablePrimitiveType = typeof(Nullable<>).MakeGenericType(primitiveType);

        // Create a Nullable Primitively serializer instance
        var nullablePrimitiveSerializerInstance = NullableSerializer.Create(primitiveSerializerInstance);

        // Register a NullableSerializer for a nullable version of the Primitively type
        BsonSerializer.TryRegisterSerializer(nullablePrimitiveType, nullablePrimitiveSerializerInstance);
    }
}
