using MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson.Serialization;

/// <summary>
/// An interface which defines all the common properties to be implemented on Primitively Bson serializer options classes.
/// </summary>
/// <typeparam name="TOptions">The type of Primitively Bson serializer options class.</typeparam>
public interface IBsonSerializerOptions<TOptions> : IBsonSerializerOptions
    where TOptions : class, IBsonSerializerOptions
{
    /// <summary>
    /// Gets or sets the function used to create an instance of the serializer
    /// </summary>
    new Func<TOptions, Type, IBsonSerializer> CreateInstance { get; set; }
}
