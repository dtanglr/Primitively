using System;
using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.Types.StringTests.SevenDigitsTests;

public class InstantiationTests
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

        var @this = (SevenDigits)from;
        string to = @this;
        var that = SevenDigits.Parse(to);

        @this.HasValue.Should().Be(hasValue);
        @this.ToString().Should().Be(expected);
        to.Should().Be(expected);
        that.HasValue.Should().Be(hasValue);
        that.ToString().Should().Be(expected);
    }
}
