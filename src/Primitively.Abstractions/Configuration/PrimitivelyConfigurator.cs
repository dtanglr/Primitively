using Microsoft.Extensions.DependencyInjection;

namespace Primitively.Configuration;

/// <summary>
/// The <see cref="PrimitivelyConfigurator"/> class is used to configure the services and options for Primitively.
/// </summary>
public sealed class PrimitivelyConfigurator
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PrimitivelyConfigurator"/> class.
    /// </summary>
    /// <param name="services">The collection of services to which Primitively will be added.</param>
    /// <param name="options">The options with which Primitively will be configured.</param>
    public PrimitivelyConfigurator(IServiceCollection services, PrimitivelyOptions options) =>
        (Services, Options) = (services, options);

    /// <summary>
    /// Gets the collection of services to which Primitively has been added.
    /// </summary>
    /// <value>
    /// The collection of services to which Primitively has been added.
    /// </value>
    public IServiceCollection Services { get; }

    /// <summary>
    /// Gets the options with which Primitively has been configured.
    /// </summary>
    /// <value>
    /// The options with which Primitively has been configured.
    /// </value>
    public PrimitivelyOptions Options { get; }
}
