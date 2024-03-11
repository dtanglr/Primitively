namespace Primitively.Configuration;

/// <summary>
/// This class provides options for configuring Primitively.
/// </summary>
public sealed class PrimitivelyOptions
{
    /// <summary>
    /// Registers a repository of Primitively types.
    /// </summary>
    /// <param name="repository">The repository of Primitively types to register.</param>
    /// <returns>The same instance of the <see cref="PrimitivelyOptions"/> class for chaining calls.</returns>
    public PrimitivelyOptions Register(IPrimitiveRepository repository)
    {
        Registry.Add(repository);

        return this;
    }

    /// <summary>
    /// Gets the registry of Primitively types.
    /// </summary>
    /// <value>
    /// The registry of Primitively types.
    /// </value>
    public PrimitiveRegistry Registry { get; } = new();
}
