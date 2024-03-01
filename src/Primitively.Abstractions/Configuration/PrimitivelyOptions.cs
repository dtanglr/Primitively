namespace Primitively.Configuration;

/// <summary>
/// The PrimitivelyOptions class provides options for configuring Primitively.
/// </summary>
public sealed class PrimitivelyOptions
{
    /// <summary>
    /// Gets the registry of Primitively types.
    /// </summary>
    public PrimitiveRegistry Registry { get; } = new();

    /// <summary>
    /// Registers a repository of Primitively types.
    /// </summary>
    /// <param name="repository">The repository to register.</param>
    /// <returns>The same instance of the PrimitivelyOptions class for chaining calls.</returns>
    public PrimitivelyOptions Register(IPrimitiveRepository repository)
    {
        Registry.Add(repository);

        return this;
    }
}
