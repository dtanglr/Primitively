using Microsoft.Extensions.DependencyInjection;

namespace Primitively.Configuration;

/// <summary>
/// The PrimitivelyConfigurator class is used to configure the services and options for Primitively.
/// </summary>
public sealed class PrimitivelyConfigurator
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PrimitivelyConfigurator"/> class.
    /// </summary>
    /// <param name="services">The collection of services to add Primitively to.</param>
    /// <param name="options">The options to configure Primitively with.</param>
    public PrimitivelyConfigurator(IServiceCollection services, PrimitivelyOptions options) =>
        (Services, Options) = (services, options);

    /// <summary>
    /// Gets the collection of services that Primitively has been added to.
    /// </summary>
    public IServiceCollection Services { get; }

    /// <summary>
    /// Gets the options that Primitively has been configured with.
    /// </summary>
    public PrimitivelyOptions Options { get; }
}
