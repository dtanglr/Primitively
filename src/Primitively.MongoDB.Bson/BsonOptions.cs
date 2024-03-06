using MongoDB.Bson.Serialization.Serializers;
using Primitively.Configuration;
using Primitively.MongoDB.Bson.Serialization;
using Primitively.MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDB.Bson;

/// <summary>
/// The <see cref="BsonOptions"/> class provides options for configuring BSON serialization for Primitively types.
/// </summary>
public class BsonOptions
{
    private readonly IBsonSerializerManager _manager;
    private readonly Dictionary<DataType, IBsonSerializerOptions> _options = new(GetAll().ToDictionary(o => o.DataType, o => o));
    private readonly Dictionary<Type, DataType> _primitiveTypes = [];
    private readonly PrimitiveRegistry _registry;

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonOptions"/> class.
    /// </summary>
    /// <param name="registry">The registry of Primitively types.</param>
    /// <param name="manager">The BSON serializer manager.</param>
    internal BsonOptions(PrimitiveRegistry registry, IBsonSerializerManager manager)
    {
        _registry = registry;
        _manager = manager;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to register BSON serializers for each type in the Primitively registry.
    /// </summary>
    public bool RegisterSerializersForEachTypeInRegistry { get; set; } = true;

    /// <summary>
    /// Configures the specified BSON serializer options.
    /// </summary>
    /// <typeparam name="TBsonSerializerOptions">The type of the BSON serializer options.</typeparam>
    /// <param name="options">A delegate to configure the BSON serializer options.</param>
    /// <returns>The same instance of the <see cref="BsonOptions"/> for chaining calls.</returns>
    public BsonOptions Configure<TBsonSerializerOptions>(Action<TBsonSerializerOptions> options)
        where TBsonSerializerOptions : class, IBsonSerializerOptions
    {
        var option = GetSerializerOptions<TBsonSerializerOptions>()!;
        options.Invoke(option);

        return this;
    }

    /// <summary>
    /// Registers the specified Primitively type.
    /// </summary>
    /// <typeparam name="TPrimitive">The type of the Primitively type.</typeparam>
    /// <returns>The same instance of the <see cref="BsonOptions"/> for chaining calls.</returns>
    public BsonOptions Register<TPrimitive>() where TPrimitive : struct, IPrimitive
    {
        var primitive = new TPrimitive();
        AddPrimitiveType(typeof(TPrimitive), primitive.DataType);

        return this;
    }

    /// <summary>
    /// Registers the specified Primitively repository.
    /// </summary>
    /// <param name="repository">The Primitively repository.</param>
    /// <returns>The same instance of the <see cref="BsonOptions"/> for chaining calls.</returns>
    public BsonOptions Register(IPrimitiveRepository repository)
    {
        if (repository is null)
        {
            throw new ArgumentNullException(nameof(repository));
        }

        foreach (var primitiveInfo in repository.GetTypes())
        {
            AddPrimitiveType(primitiveInfo.Type, primitiveInfo.DataType);
        }

        return this;
    }

    /// <summary>
    /// Builds the BSON options.
    /// </summary>
    internal void Build()
    {
        // If configured, add all the primitive types from the registry
        if (RegisterSerializersForEachTypeInRegistry && !_registry.IsEmpty)
        {
            foreach (var primitiveInfo in _registry.ToList())
            {
                AddPrimitiveType(primitiveInfo.Type, primitiveInfo.DataType);
            }
        }

        // Now generate and register a Bson serializer for each type in the collection
        foreach (var primitiveType in _primitiveTypes)
        {
            RegisterSerializer(primitiveType.Key, primitiveType.Value);
        }
    }

    /// <summary>
    /// Gets the serializer options for the specified data type.
    /// </summary>
    /// <param name="dataType">The data type.</param>
    /// <returns>The serializer options for the specified data type.</returns>
    internal IBsonSerializerOptions GetSerializerOptions(DataType dataType) => _options[dataType];

    /// <summary>
    /// Gets the serializer options of the specified type.
    /// </summary>
    /// <typeparam name="TOptions">The type of the serializer options.</typeparam>
    /// <returns>The serializer options of the specified type.</returns>
    internal TOptions GetSerializerOptions<TOptions>() where TOptions : class, IBsonSerializerOptions =>
        (TOptions)_options.Single(o => o.Value is TOptions).Value;

    /// <summary>
    /// Gets all serializer options.
    /// </summary>
    /// <returns>A collection of all serializer options.</returns>
    private static IEnumerable<IBsonSerializerOptions> GetAll()
    {
        // Initialises the default instance for each option
        yield return new BsonIByteSerializerOptions();
        yield return new BsonIDateOnlySerializerOptions();
        yield return new BsonIGuidSerializerOptions();
        yield return new BsonIIntSerializerOptions();
        yield return new BsonILongSerializerOptions();
        yield return new BsonISByteSerializerOptions();
        yield return new BsonIShortSerializerOptions();
        yield return new BsonIStringSerializerOptions();
        yield return new BsonIUIntSerializerOptions();
        yield return new BsonIULongSerializerOptions();
        yield return new BsonIUShortSerializerOptions();
    }

    /// <summary>
    /// Adds a Primitively type to the collection of primitive types.
    /// </summary>
    /// <param name="type">The type of the Primitively type.</param>
    /// <param name="dataType">The data type of the Primitively type.</param>
    private void AddPrimitiveType(Type type, DataType dataType)
    {
#if NET6_0_OR_GREATER
        _primitiveTypes.TryAdd(type, dataType);
#else
        if (!_primitiveTypes.ContainsKey(type))
        {
            _primitiveTypes.Add(type, dataType);
        }
#endif
    }

    /// <summary>
    /// Registers a serializer for the specified Primitively type.
    /// </summary>
    /// <param name="primitiveType">The type of the Primitively type.</param>
    /// <param name="dataType">The data type of the Primitively type.</param>
    private void RegisterSerializer(Type primitiveType, DataType dataType)
    {
        // Retrieve the serializer options for the given dataType
        var serializerOptions = GetSerializerOptions(dataType);

        // Create a Primitively serializer instance
        var serializerInstance = serializerOptions.CreateInstance(primitiveType);

        // Construct a nullable version of the Primitively type
        var nullablePrimitiveType = typeof(Nullable<>).MakeGenericType(primitiveType);

        // Create a nullable Primitively serializer instance
        var nullableSerializerInstance = NullableSerializer.Create(serializerInstance);

        // Register a Serializer for the Primitively type
        _manager.TryRegisterSerializer(primitiveType, serializerInstance);

        // Register a NullableSerializer for a nullable version of the Primitively type
        _manager.TryRegisterSerializer(nullablePrimitiveType, nullableSerializerInstance);
    }
}
