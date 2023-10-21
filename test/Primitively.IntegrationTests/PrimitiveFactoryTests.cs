using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests;

public class PrimitiveFactoryTests
{
    private static readonly IEnumerable<Type> _types = Assembly
        .GetExecutingAssembly()
        .GetTypes()
        .Where(t => t.IsValueType && t.IsAssignableTo(typeof(IPrimitive)));

    public static IEnumerable<object[]> PrimitiveTypes()
    {
        if (!_types.Any())
        {
            yield return Array.Empty<object[]>();
        }

        foreach (var type in _types)
        {
            yield return new object[] { type, GetExample(type) };
        }

        static string GetExample(Type type)
        {
            var repo = new PrimitiveRepository();
            repo.TryGetType(type, out var result);

            return result?.Example ?? string.Empty;
        }
    }

    [Theory]
    [MemberData(nameof(PrimitiveTypes))]
    public void CreateMethod_ReturnsCorrectType_WhenMatchFound(Type modelType, string value)
    {
        // Arrange
        var factory = new PrimitiveFactory();

        // Act
        var result = factory.Create(modelType, value);

        // Assert
        result.Should().BeOfType(modelType);
        result!.HasValue.Should().BeTrue();
    }

    [Fact]
    public void CreateMethod_ReturnsError_WhenNoMatchFound()
    {
        // Arrange
        var factory = new PrimitiveFactory();

        // Act
        var result = () => factory.Create(typeof(Guid), Guid.NewGuid().ToString());

        // Assert
        result.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [MemberData(nameof(PrimitiveTypes))]
    public void TryCreateMethod_ReturnsCorrectType_WhenMatchFound(Type modelType, string value)
    {
        // Arrange
        var factory = new PrimitiveFactory();

        // Act
        var created = factory.TryCreate(modelType, value, out var result);

        // Assert
        result.Should().BeOfType(modelType);
        result!.HasValue.Should().BeTrue();
        created.Should().BeTrue();
    }

    [Fact]
    public void TryCreateMethod_ReturnsFalse_WhenNoMatchFound()
    {
        // Arrange
        var factory = new PrimitiveFactory();

        // Act
        var created = factory.TryCreate(typeof(Guid), Guid.NewGuid().ToString(), out var result);

        // Assert
        result.Should().BeNull();
        created.Should().BeFalse();
    }
}
