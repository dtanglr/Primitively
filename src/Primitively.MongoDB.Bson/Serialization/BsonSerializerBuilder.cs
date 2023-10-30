﻿using Primitively.Configuration;

namespace Primitively.MongoDB.Bson.Serialization;

/// <summary>
/// Fluent builder class to register MongoDB Bson serializers for Primitively source generated types
/// </summary>
public class BsonSerializerBuilder
{
    private readonly BsonSerializerCacheBuilder _cacheBuilder = new();
    private readonly BsonSerializerRegisterBuilder _registerBuilder = new();
    private readonly PrimitiveRegistry _registry;

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonSerializerBuilder"/> class.
    /// </summary>
    /// <param name="registry">The registry of Primitively types</param>
    public BsonSerializerBuilder(PrimitiveRegistry registry)
    {
        _registry = registry;
    }

    /// <summary>
    /// Register serializers for each of the Primitively types in the registry
    /// </summary>
    internal void RegisterSerializers()
    {
        foreach (var primitiveInfo in _registry.ToList())
        {
            _registerBuilder.AddSerializerForType(primitiveInfo);
        }
    }

    /// <summary>
    /// Override any of the default Primitively serializers with custom ones
    /// </summary>
    /// <param name="cache">Cache builder. See <see cref="BsonSerializerCacheBuilder"/></param>
    /// <returns>The current instance</returns>
    /// <remarks>
    /// There is a default serializer for each Primitively <see cref="DataType"/>. These are held in cache.
    /// This method allows the default serializer to be replaced with a custom implementation.
    /// </remarks>
    public BsonSerializerBuilder DefaultSerializers(Action<BsonSerializerCacheBuilder> cache)
    {
        cache.Invoke(_cacheBuilder);

        return this;
    }

    /// <summary>
    /// Registers non-nullable and nullable serializer instances for each of the given Primitively types. 
    /// </summary>
    /// <param name="register">Register builder. See <see cref="BsonSerializerRegisterBuilder"/></param>
    /// <returns>The current instance</returns>
    /// <remarks>
    /// For a Primitively type to be serialized correctly in MongoDB they must have an associated serializer.
    /// This method provides a mechanism which generates and registers an instance of a serializer for the given type.
    /// Each Primitively <see cref="DataType"/> has a matching serializer held in cache. The defaults can be changed using 
    /// the <see cref="DefaultSerializers(Action{BsonSerializerCacheBuilder})"/> method.
    /// </remarks>
    public BsonSerializerBuilder RegisterSerializers(Action<BsonSerializerRegisterBuilder> register)
    {
        register.Invoke(_registerBuilder);

        return this;
    }
}
