using System;
using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.Types.StringTests.GphcNumberTests;

public class ImplicitOperatorAndParserTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("G7123456")]
    [InlineData("123456")]
    [InlineData("1234567", true)]
    public void ConvertFromThisToThatWithExpectedResults(string from, bool hasValue = default)
    {
        var expected = hasValue ? from.Replace(" ", string.Empty) : default;

        var @this = (GphcNumber)from;
        string to = @this;
        var that = GphcNumber.Parse(to);

        @this.HasValue.Should().Be(hasValue);
        @this.Value.Should().Be(expected);
        @this.ToString().Should().Be(expected);
        to.Should().Be(expected);
        that.HasValue.Should().Be(hasValue);
        that.Value.Should().Be(expected);
        that.ToString().Should().Be(expected);
    }
}
