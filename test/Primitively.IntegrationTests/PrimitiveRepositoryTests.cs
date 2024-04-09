using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests;

public class PrimitiveRepositoryTests : PrimitiveTests
{
    public static TheoryData<Type, Type> PrimitiveTypes()
    {
        var testData = new TheoryData<Type, Type>();

        foreach (var type in Types)
        {
            testData.Add(type, GetInfoType(type));
        }

        return testData;

        static Type GetInfoType(Type type) => type switch
        {
            _ when type.IsAssignableTo(typeof(IByte)) => typeof(ByteInfo),
            _ when type.IsAssignableTo(typeof(IDateOnly)) => typeof(DateOnlyInfo),
            _ when type.IsAssignableTo(typeof(IDouble)) => typeof(DoubleInfo),
            _ when type.IsAssignableTo(typeof(IGuid)) => typeof(GuidInfo),
            _ when type.IsAssignableTo(typeof(IInt)) => typeof(IntInfo),
            _ when type.IsAssignableTo(typeof(ILong)) => typeof(LongInfo),
            _ when type.IsAssignableTo(typeof(ISByte)) => typeof(SByteInfo),
            _ when type.IsAssignableTo(typeof(IShort)) => typeof(ShortInfo),
            _ when type.IsAssignableTo(typeof(ISingle)) => typeof(SingleInfo),
            _ when type.IsAssignableTo(typeof(IString)) => typeof(StringInfo),
            _ when type.IsAssignableTo(typeof(IUInt)) => typeof(UIntInfo),
            _ when type.IsAssignableTo(typeof(IULong)) => typeof(ULongInfo),
            _ when type.IsAssignableTo(typeof(IUShort)) => typeof(UShortInfo),
            _ => throw new NotImplementedException(),
        };
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
        result.Should().HaveCount(Types.Count());
    }

    [Theory]
    [InlineData(typeof(IDateOnly))]
    [InlineData(typeof(IGuid))]
    [InlineData(typeof(IString))]
    [InlineData(typeof(INumeric))]
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
            nameof(INumeric) => repo.GetTypes<NumericInfo>(),
            _ => throw new NotImplementedException()
        };

        // Assert
        result.Should().HaveCount(Types.Count(t => t.IsAssignableTo(type)));
    }
}
