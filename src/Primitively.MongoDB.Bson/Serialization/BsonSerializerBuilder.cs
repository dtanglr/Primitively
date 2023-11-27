using Primitively.MongoDB.Bson.Serialization.Options;

namespace Primitively.MongoDB.Bson.Serialization;

/// <summary>
/// Fluent builder class to register MongoDB Bson serializers for Primitively source generated types
/// </summary>
public class BsonSerializerBuilder
{
    private readonly BsonSerializerRegisterBuilder _registerBuilder = new();
    private readonly BsonOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonSerializerBuilder"/> class.
    /// </summary>
    internal BsonSerializerBuilder(BsonOptions options)
    {
        _options = options;
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
    /// </remarks>
    public BsonSerializerBuilder RegisterSerializers(Action<BsonSerializerRegisterBuilder> register)
    {
        register.Invoke(_registerBuilder);

        return this;
    }
}
