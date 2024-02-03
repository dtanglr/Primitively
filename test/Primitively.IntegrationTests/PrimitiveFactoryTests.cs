using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests;

public class PrimitiveFactoryTests : PrimitiveTests
{
    public static TheoryData<Type, string> PrimitiveTypes()
    {
        var testData = new TheoryData<Type, string>();

        foreach (var type in Types)
        {
            testData.Add(type, GetExample(type));
        }

        return testData;

        static string GetExample(Type type)
        {
            var repo = PrimitiveLibrary.Respository;
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
