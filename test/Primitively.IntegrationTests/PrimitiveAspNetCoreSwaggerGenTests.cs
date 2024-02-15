using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Primitively.AspNetCore.SwaggerGen;
using Primitively.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;
using Xunit;
using PrimitiveLibrary1 = Acme.TestLib.PrimitiveLibrary;
using PrimitiveLibrary2 = Acme.TestLib2.PrimitiveLibrary;

namespace Primitively.IntegrationTests;

public class PrimitiveAspNetCoreSwaggerGenTests
{
    [Fact]
    public void PrimitiveSchemaFilter_IsRegistered()
    {
        // Arrange
        var types = new List<PrimitiveInfo>();
        types.AddRange(PrimitiveLibrary1.Respository.GetTypes());
        types.AddRange(PrimitiveLibrary2.Respository.GetTypes());

        var services = new ServiceCollection();
        services.AddPrimitively(options => options
            .Register(PrimitiveLibrary1.Respository)
            .Register(PrimitiveLibrary2.Respository))
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

        var registry = filter.Arguments[0] as PrimitiveRegistry;
        registry.Should().NotBeNull();

        var registryTypes = registry!.ToList();
        registryTypes.Should().NotBeNullOrEmpty();

        foreach (var type in types)
        {
            registryTypes.Should().ContainSingle(r => r.Equals(type));
        }

        var schemaFilter = Activator.CreateInstance(filter.Type, filter.Arguments) as PrimitiveSchemaFilter;
        schemaFilter.Should().NotBeNull();
    }
}
