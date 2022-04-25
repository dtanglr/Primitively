using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.Types.StringTests.NhsNumberTests;

public class ImplicitOperatorAndParserTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("A")]
    [InlineData("ABCDEFGHIJK")]
    [InlineData("012345678")]
    [InlineData("1234567890")]
    [InlineData("01234567890")]
    [InlineData("0123456789", true, "012 345 6789")]
    [InlineData("012 345 6789", true, "012 345 6789")]
    public void ConvertFromThisToThatWithExpectedResults(string from, bool hasValue = default, string? formatted = default)
    {
        var expected = hasValue ? from?.Replace(" ", string.Empty) : default;

        var @this = (NhsNumber)from;
        string to = @this;
        var that = NhsNumber.Parse(to);

        @this.HasValue.Should().Be(hasValue);
        @this.Value.Should().Be(expected);
        @this.FormattedValue.Should().Be(formatted);
        @this.ToString().Should().Be(expected);
        to.Should().Be(expected);
        that.HasValue.Should().Be(hasValue);
        that.Value.Should().Be(expected);
        that.FormattedValue.Should().Be(formatted);
        that.ToString().Should().Be(expected);
    }
}
