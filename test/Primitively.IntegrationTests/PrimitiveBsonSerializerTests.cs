using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests;

public class PrimitiveBsonSerializerTests
{
    [Fact]
    public void Test()
    {
        // Act
        var result = Match((ByteId)null);

        // Assert
        result.Should().Be(nameof(IByte));
    }

    private static string Match<TPrimitive>(TPrimitive value)
        where TPrimitive : struct, IPrimitive
    {
        var type = typeof(TPrimitive);

        return type switch
        {
            _ when type.IsAssignableTo(typeof(IByte)) => nameof(IByte),
            _ when type.IsAssignableTo(typeof(ISByte)) => nameof(ISByte),
            _ => throw new NotImplementedException()
        };
    }
}
