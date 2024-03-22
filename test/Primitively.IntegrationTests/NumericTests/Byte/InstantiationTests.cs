using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.NumericTests.Byte;

public class InstantiationTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("0")]
    [InlineData("00")]
    [InlineData("001", true)]
    [InlineData(ByteId.Example, true)]
    public void ConvertFromThisToThatWithExpectedResults(string? from, bool hasValue = default)
    {
        var expectedInteger = hasValue ? ByteId.Parse(from) : default;
        var expectedString = expectedInteger.ToString();

        var @this = (ByteId)from;
        string to = @this;
        var that = ByteId.Parse(to);
        var and = new ByteId(that);
        string back = and;

        @this.HasValue.Should().Be(hasValue);
        @this.Should().Be(expectedInteger);
        @this.ToString().Should().Be(expectedString);
        to.Should().Be(expectedString);
        that.HasValue.Should().Be(hasValue);
        that.Should().Be(expectedInteger);
        that.ToString().Should().Be(expectedString);
        and.HasValue.Should().Be(hasValue);
        and.Should().Be(expectedInteger);
        and.ToString().Should().Be(expectedString);
        back.Should().Be(expectedString);
    }
}
