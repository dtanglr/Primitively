using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Primitively.AspNetCore.SwaggerGen;
using Primitively.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;
using Xunit;

namespace Primitively.IntegrationTests;

public class PrimitiveAspNetCoreSwaggerGenTests
{
    [Fact]
    public void PrimitiveSchemaFilter_IsRegistered()
    {
        // Arrange
        var repository = PrimitiveLibrary.Respository;
        var services = new ServiceCollection();

        services.AddPrimitively(options => options.Register(repository))
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
