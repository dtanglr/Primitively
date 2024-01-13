using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson.Serialization;

/// <summary>
/// An interface which defines all the commmon properties to be implemented on Primitively Bson serializer options classes.
/// </summary>
public interface IBsonSerializerOptions
{
    /// <summary>
    /// Gets the <see cref="Primitively.DataType"/> of the Primitively type that the serializer targets
    /// </summary>
    DataType DataType { get; }

    /// <summary>
    /// Gets or sets the <see cref="BsonType"/> used to create a serializer instance
    /// </summary>
    BsonType Representation { get; set; }

    /// <summary>
    /// Gets or sets the serializer type used to serialize/deserialize Primitively types that match the given <see cref="Primitively.DataType"/>
    /// </summary>
    Type SerializerType { get; set; }

    /// <summary>
    /// Gets the function used to create an instance of the serializer
    /// </summary>
    Func<Type, IBsonSerializer> CreateInstance { get; }
}
