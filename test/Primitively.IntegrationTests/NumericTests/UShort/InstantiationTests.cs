using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.NumericTests.UShort;

public class InstantiationTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("-1")]
    [InlineData("0", true)]
    [InlineData("00", true)]
    [InlineData("001", true)]
    [InlineData(UShortId.Example, true)]
    public void ConvertFromThisToThatWithExpectedResults(string? from, bool hasValue = false)
    {
        var @this = UShortId.Parse(from);

        @this.HasValue.Should().Be(hasValue);
    }
}
