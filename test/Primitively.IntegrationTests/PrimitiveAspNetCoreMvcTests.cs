using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Primitively.AspNetCore.Mvc.ModelBinding;
using Xunit;
using PrimitiveLibrary1 = Acme.TestLib.PrimitiveLibrary;
using PrimitiveLibrary2 = Acme.TestLib2.PrimitiveLibrary;

namespace Primitively.IntegrationTests;

public class PrimitiveAspNetCoreMvcTests
{
    [Fact]
    public void PrimitiveModelBinderProvider_IsRegistered()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddPrimitively(options => options
            .Register(PrimitiveLibrary1.Respository)
            .Register(PrimitiveLibrary2.Respository))
            .AddMvc();

        // Act
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var options = serviceProvider.GetService<IOptions<MvcOptions>>();
        options.Should().NotBeNull();
        options!.Value.ModelBinderProviders.Count.Should().Be(1);

        var provider = options.Value.ModelBinderProviders[0] as PrimitiveModelBinderProvider;
        provider.Should().NotBeNull();
    }

    [Fact]
    public void PrimitiveModelBinderProvider_IsNotRegistered()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddPrimitively()
            .AddMvc();

        // Act
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var options = serviceProvider.GetService<IOptions<MvcOptions>>();
        options.Should().BeNull();
    }
}
