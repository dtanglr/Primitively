namespace Primitively.MongoDb.Bson.Serialization;

/// <summary>
/// Fluent builder class to register MongoDB Bson serializers for Primitively source generated types
/// </summary>
public class BsonSerializerBuilder
{
    private readonly BsonSerializerCacheBuilder _cacheBuilder = new();
    private readonly BsonSerializerRegisterBuilder _registerBuilder = new();

    /// <summary>
    /// Override any of the default Primitively serializers with custom ones
    /// </summary>
    /// <param name="cache">Cache builder</param>
    /// <returns>The current instance of the builder</returns>
    /// <remarks>
    /// There is a default Primitively Bson Serializer for each type of Primitively type. These are held in a cache.
    /// This method allows the default for any given Primitively DataType to be replaced with a custom implementation.
    /// </remarks>
    public BsonSerializerBuilder DefaultSerializers(Action<BsonSerializerCacheBuilder> cache)
    {
        cache.Invoke(_cacheBuilder);

        return this;
    }

    /// <summary>
    /// Registers non-nullable and nullable serializer instances for each of the given Primitively types. 
    /// </summary>
    /// <param name="register">Register builder</param>
    /// <returns>The current instance of the builder</returns>
    /// <remarks>
    /// For a Primitively type to be serialized correctly in MongoDB they must have an associated serializer.
    /// This method provides a mechanism which generates and registers an instance of a serializer for the given type.
    /// Each Primitively DataType has a matching serializer held in cache. The defaults can be changed using 
    /// the DefaultSerializers method.
    /// </remarks>
    public BsonSerializerBuilder RegisterSerializers(Action<BsonSerializerRegisterBuilder> register)
    {
        register.Invoke(_registerBuilder);

        return this;
    }
}
