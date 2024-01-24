using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests;

public class PrimitiveRepositoryTests
{
    private static readonly IEnumerable<Type> _types = typeof(PrimitiveLibrary)
        .Assembly
        .GetTypes()
        .Where(t => t.IsValueType && t.IsAssignableTo(typeof(IPrimitive)));

    public static IEnumerable<object[]> PrimitiveTypes()
    {
        static Type GetInfoType(Type type) => type switch
        {
            _ when type.IsAssignableTo(typeof(IDateOnly)) => typeof(DateOnlyInfo),
            _ when type.IsAssignableTo(typeof(IGuid)) => typeof(GuidInfo),
            _ when type.IsAssignableTo(typeof(IInteger)) => typeof(IntegerInfo),
            _ when type.IsAssignableTo(typeof(IString)) => typeof(StringInfo),
            _ => throw new NotImplementedException(),
        };

        foreach (var type in _types)
        {
            yield return new object[] { type, GetInfoType(type) };
        }
    }

    [Theory]
    [MemberData(nameof(PrimitiveTypes))]
    public void GetTypeMethod_ReturnsCorrectType_WhenMatchFound(Type type, Type resultType)
    {
        // Arrange
        var repo = new PrimitiveRepository();

        // Act
        var result = repo.GetType(type);

        // Assert
        result.Should().BeOfType(resultType);
    }

    [Fact]
    public void GetTypeMethod_ReturnsError_WhenNoMatchFound()
    {
        // Arrange
        var repo = new PrimitiveRepository();

        // Act
        var result = () => repo.GetType(typeof(Guid));

        // Assert
        result.Should().Throw<InvalidOperationException>();
    }

    [Theory]
    [MemberData(nameof(PrimitiveTypes))]
    public void TryGetTypeMethod_ReturnsCorrectType_WhenMatchFound(Type type, Type? resultType)
    {
        // Arrange
        var repo = new PrimitiveRepository();

        // Act
        var outcome = repo.TryGetType(type, out var result);

        // Assert
        outcome.Should().BeTrue();
        result.Should().BeOfType(resultType);
    }

    [Fact]
    public void TryGetTypeMethod_ReturnsFalse_WhenNoMatchFound()
    {
        // Arrange
        var repo = new PrimitiveRepository();

        // Act
        var outcome = repo.TryGetType(typeof(Guid), out var result);

        // Assert
        outcome.Should().BeFalse();
        result.Should().BeNull();
    }

    [Fact]
    public void GetTypesMethod_ReturnsCollectionOfPrimitiveInfoType()
    {
        // Arrange
        var repo = new PrimitiveRepository();

        // Act
        var result = repo.GetTypes();

        // Assert
        result.Should().HaveCount(_types.Count());
    }

    [Theory]
    [InlineData(typeof(IDateOnly))]
    [InlineData(typeof(IGuid))]
    [InlineData(typeof(IString))]
    public void GetTypesOfTMethod_ReturnsCollectionOfPrimitiveInfoType(Type type)
    {
        // Arrange
        var repo = new PrimitiveRepository();

        // Act
        IReadOnlyCollection<PrimitiveInfo> result = type.Name switch
        {
            nameof(IDateOnly) => repo.GetTypes<DateOnlyInfo>(),
            nameof(IGuid) => repo.GetTypes<GuidInfo>(),
            nameof(IString) => repo.GetTypes<StringInfo>(),
            _ => throw new NotImplementedException()
        };

        // Assert
        result.Should().HaveCount(_types.Count(t => t.IsAssignableTo(type)));
    }
}
