using Primitively.Configuration;
using Primitively.MongoDB.Bson.Serialization;
using Primitively.MongoDB.Bson.Serialization.Options;

namespace Primitively.MongoDB.Bson;

/// <summary>
/// Primitively Configurator extensions to register MongoDB Bson serializers 
/// for Primitively source generated types
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Register MongoDB nullable and non-nullable Bson serializers
    /// </summary>
    /// <param name="configurator">Configurator</param>
    /// <param name="options">BsonOptions</param>
    /// <returns>Configurator</returns>
    public static PrimitivelyConfigurator AddBson(this PrimitivelyConfigurator configurator, BsonOptions? options = null)
    {
        return configurator.AddBson(configure => { }, options ?? new BsonOptions());
    }

    /// <summary>
    /// Register MongoDB nullable and non-nullable Bson serializers
    /// </summary>
    /// <param name="configurator">Configurator</param>
    /// <param name="options">BsonOptions</param>
    /// <param name="configure">BsonSerializerBuilder</param>
    /// <returns>Configurator</returns>
    public static PrimitivelyConfigurator AddBson(this PrimitivelyConfigurator configurator, Action<BsonSerializerBuilder> configure, BsonOptions? options = null)
    {
        BsonSerializerBuilder builder = new(options ??= new BsonOptions());
        builder.SetDefaultGuidRepresentation();

        if (options.RegisterSerializersForEachTypeInRegistry)
        {
            builder.RegisterSerializers(register =>
                register.AddSerializerForEachTypeIn(configurator.Options.Registry));
        }

        configure.Invoke(builder);

        return configurator;
    }
}
