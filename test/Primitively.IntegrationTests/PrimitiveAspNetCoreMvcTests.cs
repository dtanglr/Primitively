using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Primitively.AspNetCore.Mvc;
using Primitively.AspNetCore.Mvc.ModelBinding;
using Primitively.Configuration;
using Xunit;

namespace Primitively.IntegrationTests;

public class PrimitiveAspNetCoreMvcTests
{
    [Fact]
    public void PrimitiveModelBinderProvider_IsRegistered()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddPrimitively(options => options.Register(PrimitiveLibrary.Respository))
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
