﻿using FluentAssertions;
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
    public void SwaggerSchemaFilter_Types_Are_Registered_Correctly_Using_IPrimitive_In_Params()
    {
        // Arrange
        var services = new ServiceCollection();
        var repository = new PrimitiveRepository();
        var birthDateInfo = repository.GetType(typeof(BirthDate));
        var deathDateInfo = repository.GetType(typeof(DeathDate));

        services.AddPrimitively(configure =>
        {
            // Add AspNet support
            configure.UseAspNet(builder =>
            {
                // Register types to be used for Swagger schema filters
                builder
                    .AddSwaggerSchemaFilterFor(birthDateInfo)
                    .AddSwaggerSchemaFilterFor(deathDateInfo)
                    .Build();
            });
        });

        // Act
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var options = serviceProvider.GetService<IOptions<SwaggerGenOptions>>();
        options.Should().NotBeNull();
        options!.Value.SchemaFilterDescriptors.Exists(f => f.Type == typeof(PrimitiveSchemaFilter)).Should().BeTrue();

        var filter = options.Value.SchemaFilterDescriptors.SingleOrDefault(f => f.Type == typeof(PrimitiveSchemaFilter));
        filter.Should().NotBeNull();
        filter!.Arguments.Count().Should().Be(1);

        var arg = filter.Arguments[0] as Func<IEnumerable<PrimitiveInfo>>;
        arg.Should().NotBeNull();

        var types = arg!.Invoke();
        types.Contains(birthDateInfo).Should().BeTrue();
        types.Contains(deathDateInfo).Should().BeTrue();

        var schemaFilter = Activator.CreateInstance(filter.Type, filter.Arguments) as PrimitiveSchemaFilter;
        schemaFilter.Should().NotBeNull();
    }

    [Fact]
    public void SwaggerSchemaFilter_Types_Are_Registered_Correctly_Using_IPrimitiveRepository_Instances_In_Params()
    {
        // Arrange
        var services = new ServiceCollection();
        var repository = new PrimitiveRepository();

        services.AddPrimitively(configure =>
        {
            // Add AspNet support
            configure.UseAspNet(builder =>
            {
                // Register types to be used for Swagger schema filters
                builder.AddSwaggerSchemaFiltersFor(repository);
                builder.Build();
            });
        });

        // Act
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var options = serviceProvider.GetService<IOptions<SwaggerGenOptions>>();
        options.Should().NotBeNull();
        options!.Value.SchemaFilterDescriptors.Exists(f => f.Type == typeof(PrimitiveSchemaFilter)).Should().BeTrue();

        var filter = options.Value.SchemaFilterDescriptors.SingleOrDefault(f => f.Type == typeof(PrimitiveSchemaFilter));
        filter.Should().NotBeNull();
        filter!.Arguments.Count().Should().Be(1);

        var arg = filter.Arguments[0] as Func<IEnumerable<PrimitiveInfo>>;
        arg.Should().NotBeNull();

        foreach (var type in arg!.Invoke())
        {
            repository.GetTypes().SingleOrDefault(p => p == type).Should().NotBeNull();
        }

        var schemaFilter = Activator.CreateInstance(filter.Type, filter.Arguments) as PrimitiveSchemaFilter;
        schemaFilter.Should().NotBeNull();
    }
}