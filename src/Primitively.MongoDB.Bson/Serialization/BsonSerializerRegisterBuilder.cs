using System.Collections.Concurrent;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Primitively.Configuration;
using Primitively.MongoDB.Bson.Serialization.Options;

namespace Primitively.MongoDB.Bson.Serialization;

/// <summary>
/// Fluent builder used to create and register instances of serializers for Primitively types
/// </summary>
public class BsonSerializerRegisterBuilder
{
    private static readonly ConcurrentBag<Type> _primitiveTypes = new();

    internal BsonSerializerRegisterBuilder() { }

    /// <summary>
    /// Register nullable and non-nullable Bson serializers for all the Primitively types contained 
    /// in the source generated repository
    /// </summary>
    /// <typeparam name="TPrimitiveRepository">IPrimitiveRepository</typeparam>
    /// <returns>BsonSerializerRegisterBuilder</returns>
    /// <remarks>
    /// The type of serializer used is governed by the BsonSerializerOptions. 
    /// If a serializer for the given type is already registered, subsequent attempts for the same type will be ignored.
    /// To use a custom serializer for one or more types in a repository. Simply register them individually 
    /// first before calling this method.
    /// </remarks>
    public BsonSerializerRegisterBuilder ForEachTypeIn<TPrimitiveRepository>() where TPrimitiveRepository : class, IPrimitiveRepository, new()
    {
        // Get a list of the source generated Primitively types
        var repository = new TPrimitiveRepository();

        return ForEachTypeIn(repository);
    }

    public BsonSerializerRegisterBuilder ForEachTypeIn(IPrimitiveRepository repository)
    {
        // Get a list of the source generated Primitively types
        var primitives = repository.GetTypes();

        if (primitives.Count == 0)
        {
            return this;
        }

        RegisterBsonSerializers(primitives);

        return this;
    }

    /// <summary>
    /// Automatically register a nullable and a non-nullable Bson serializer for the provided Primitively type
    /// </summary>
    /// <typeparam name="TPrimitive">IPrimitive</typeparam>
    /// <returns>BsonSerializerRegisterBuilder</returns>
    public BsonSerializerRegisterBuilder ForType<TPrimitive>() where TPrimitive : struct, IPrimitive
    {
        // Get a default instance of the provided Primitively type to obtain the DataType enum property value
        var primitive = new TPrimitive();
        var dataType = primitive.DataType;

        RegisterBsonSerializer(typeof(TPrimitive), dataType);

        return this;
    }

    /// <summary>
    /// Automatically register a nullable and a non-nullable Bson serializer for the provided Primitively type
    /// </summary>
    /// <typeparam name="TPrimitive">IPrimitive</typeparam>
    /// <returns>BsonSerializerRegisterBuilder</returns>
    public BsonSerializerRegisterBuilder ForType<TPrimitive>(IBsonSerializer<TPrimitive> serializerInstance) where TPrimitive : struct, IPrimitive
    {
        // Check that Primitive types has not been handled already
        var primitiveType = typeof(TPrimitive);

        // Check that Primitive types has not been handled already
        if (!_primitiveTypes.Contains(primitiveType))
        {
            // Add the type to a collection to provide a data source for the above check
            _primitiveTypes.Add(primitiveType);

            // Register a Serializer for the Primitively type
            BsonSerializer.TryRegisterSerializer(primitiveType, serializerInstance);
        }

        // Construct a nullable version of the Primitively type
        var nullablePrimitiveType = typeof(Nullable<>).MakeGenericType(primitiveType);

        // Check that nullable Primitive types has not been handled already
        if (!_primitiveTypes.Contains(nullablePrimitiveType))
        {
            // Add the type to a collection to provide a data source for the above check
            _primitiveTypes.Add(nullablePrimitiveType);

            // Register a NullableSerializer for a nullable version of the Primitively type
            BsonSerializer.TryRegisterSerializer(nullablePrimitiveType, serializerInstance);
        }

        return this;
    }

    /// <summary>
    /// Register a nullable and a non-nullable Bson serializer for the provided Primitively type
    /// </summary>
    /// <typeparam name="TPrimitive">IPrimitive</typeparam>
    /// <typeparam name="TBsonSerializer">IBsonSerializer</typeparam>
    /// <returns>BsonSerializerRegisterBuilder</returns>
    /// <remarks>
    /// The type of serializer used is governed by the TBsonSerializer type parameter.  This allows for
    /// individual primitive types to have their own serialiser, rather than the one defined in BsonSerializerOptions.
    /// If a serializer for the given type is already registered, subsequent attempts for the same type will be ignored.
    /// </remarks>
    public BsonSerializerRegisterBuilder ForType<TPrimitive, TBsonSerializer>()
        where TPrimitive : struct, IPrimitive
        where TBsonSerializer : class, IBsonSerializer<TPrimitive>, new()
    {
        // Create an instance of the serializer
        var serializerInstance = (TBsonSerializer)Activator.CreateInstance(typeof(TBsonSerializer))!;

        return ForType(serializerInstance);
    }

    /// <summary>
    /// Register nullable and non-nullable Bson serializers for each of the Primitively types in the registry
    /// </summary>
    /// <param name="registry">PrimitiveRegistry</param>
    /// <returns>BsonSerializerRegisterBuilder</returns>
    internal BsonSerializerRegisterBuilder ForEachTypeIn(PrimitiveRegistry registry)
    {
        if (registry.IsEmpty)
        {
            return this;
        }

        RegisterBsonSerializers(registry.ToList());

        return this;
    }

    private static void RegisterBsonSerializer(Type primitiveType, DataType dataType)
    {
        // Check that Primitive types has not been handled already
        if (!_primitiveTypes.Contains(primitiveType))
        {
            // Add the type to a collection to provide a data source for the above check
            _primitiveTypes.Add(primitiveType);

            // Create a Primitively serializer instance
            var serializerType = BsonSerializerOptionsCache.Get(dataType); // TODO: Update Get method/call to ensure failure handled
            var serializerInstance = serializerType.CreateInstance(primitiveType);

            // Register a Serializer for the Primitively type
            BsonSerializer.TryRegisterSerializer(primitiveType, serializerInstance);
        }

        // Construct a nullable version of the Primitively type
        var nullablePrimitiveType = typeof(Nullable<>).MakeGenericType(primitiveType);

        // Check that nullable Primitive types has not been handled already
        if (!_primitiveTypes.Contains(nullablePrimitiveType))
        {
            // Add the type to a collection to provide a data source for the above check
            _primitiveTypes.Add(nullablePrimitiveType);

            // Create a nullable Primitively serializer instance
            var serializerType = BsonSerializerOptionsCache.Get(dataType); // TODO: Update Get method/call to ensure failure handled
            var serializerInstance = NullableSerializer.Create(serializerType.CreateInstance(primitiveType));

            // Register a NullableSerializer for a nullable version of the Primitively type
            BsonSerializer.TryRegisterSerializer(nullablePrimitiveType, serializerInstance);
        }
    }

    private static void RegisterBsonSerializers(IEnumerable<PrimitiveInfo> primitives)
    {
        foreach (var primitiveInfo in primitives)
        {
            RegisterBsonSerializer(primitiveInfo.Type, primitiveInfo.DataType);
        }
    }
}
