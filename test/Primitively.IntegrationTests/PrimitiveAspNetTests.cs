using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Primitively.AspNetCore.Mvc;
using Primitively.AspNetCore.Mvc.ModelBinding;
using Primitively.AspNetCore.SwaggerGen;
using Primitively.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;
using Xunit;

namespace Primitively.IntegrationTests;

public class PrimitiveAspNetTests
{
    [Fact]
    public void ModelBinder_Types_Are_Registered_Correctly_Not_Using_IPrimitiveFactory_Instances_In_Params()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddPrimitively(PrimitiveLibrary.Respository)
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
    public void ModelBinder_Types_Are_Registered_Correctly_Using_IPrimitiveFactory_Instances_In_Params()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddPrimitively()
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
    public void SwaggerSchemaFilter_Types_Are_Registered_Correctly_Using_IPrimitiveRepository_Instances_In_Params()
    {
        // Arrange
        var repository = PrimitiveLibrary.Respository;
        var services = new ServiceCollection();

        services.AddPrimitively(repository)
            .AddSwaggerGen();

        // Act
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var options = serviceProvider.GetService<IOptions<SwaggerGenOptions>>();
        options.Should().NotBeNull();
        options!.Value.SchemaFilterDescriptors.Exists(f => f.Type == typeof(PrimitiveSchemaFilter)).Should().BeTrue();

        var filter = options.Value.SchemaFilterDescriptors.SingleOrDefault(f => f.Type == typeof(PrimitiveSchemaFilter));
        filter.Should().NotBeNull();
        filter!.Arguments.Length.Should().Be(1);

        var arg = filter.Arguments[0] as PrimitiveRegistry;
        arg.Should().NotBeNull();

        foreach (var type in repository.GetTypes())
        {
            type.Should().NotBeNull();
        }

        var schemaFilter = Activator.CreateInstance(filter.Type, filter.Arguments) as PrimitiveSchemaFilter;
        schemaFilter.Should().NotBeNull();
    }
}
