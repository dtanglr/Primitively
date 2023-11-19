using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.IntegerTests.UInt;

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
    [InlineData(UIntId.Example, true)]
    public void ConvertFromThisToThatWithExpectedResults(string? from, bool hasValue = default)
    {
        var expectedInteger = hasValue ? UIntId.Parse(from) : default;
        var expectedString = expectedInteger.ToString();

        var @this = (UIntId)from;
        string to = @this;
        var that = UIntId.Parse(to);
        var and = new UIntId(that);
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
