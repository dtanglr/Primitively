using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Primitively.AspNetCore;
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

        services.AddPrimitively(options => options.Repositories.Add<PrimitiveRepository>())
            .UseAspNet();

        var s = new TimeSpan(0, 10, 0).ToString();

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
            .UseAspNet(builder =>
            {
                // Register types to be used by Model Binders
                builder.AddModelBindersFor<PrimitiveFactory>();
            });

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
    public void SwaggerSchemaFilter_Types_Are_Registered_Correctly_Using_IPrimitive_In_Params()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddPrimitively()
            .UseAspNet(builder =>
            {
                // Register types to be used for Swagger schema filters
                builder
                    .AddOpenApiSchemaFor<BirthDate>()
                    .AddOpenApiSchemaFor<DeathDate>();
            });

        // Act
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var options = serviceProvider.GetService<IOptions<SwaggerGenOptions>>();
        options.Should().NotBeNull();
        options!.Value.SchemaFilterDescriptors.Exists(f => f.Type == typeof(PrimitiveSchemaFilter)).Should().BeTrue();

        var filter = options.Value.SchemaFilterDescriptors.SingleOrDefault(f => f.Type == typeof(PrimitiveSchemaFilter));
        filter.Should().NotBeNull();
        filter!.Arguments.Length.Should().Be(1);

        var arg = filter.Arguments[0] as Func<IEnumerable<PrimitiveInfo>>;
        arg.Should().NotBeNull();

        var types = arg!.Invoke();
        types.SingleOrDefault(i => i.Type == typeof(BirthDate)).Should().NotBeNull();
        types.SingleOrDefault(i => i.Type == typeof(DeathDate)).Should().NotBeNull();

        var schemaFilter = Activator.CreateInstance(filter.Type, filter.Arguments) as PrimitiveSchemaFilter;
        schemaFilter.Should().NotBeNull();
    }

    [Fact]
    public void SwaggerSchemaFilter_Types_Are_Registered_Correctly_Using_IPrimitiveRepository_Instances_In_Params()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddPrimitively()
            .UseAspNet(builder =>
            {
                // Register types to be used for Swagger schema filters
                builder.AddOpenApiSchemasFor<PrimitiveRepository>();
            });

        // Act
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var options = serviceProvider.GetService<IOptions<SwaggerGenOptions>>();
        options.Should().NotBeNull();
        options!.Value.SchemaFilterDescriptors.Exists(f => f.Type == typeof(PrimitiveSchemaFilter)).Should().BeTrue();

        var filter = options.Value.SchemaFilterDescriptors.SingleOrDefault(f => f.Type == typeof(PrimitiveSchemaFilter));
        filter.Should().NotBeNull();
        filter!.Arguments.Length.Should().Be(1);

        var arg = filter.Arguments[0] as Func<IEnumerable<PrimitiveInfo>>;
        arg.Should().NotBeNull();

        var repository = new PrimitiveRepository();

        foreach (var type in arg!.Invoke())
        {
            repository.GetTypes().SingleOrDefault(p => p == type).Should().NotBeNull();
        }

        var schemaFilter = Activator.CreateInstance(filter.Type, filter.Arguments) as PrimitiveSchemaFilter;
        schemaFilter.Should().NotBeNull();
    }
}
