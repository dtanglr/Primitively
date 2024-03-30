using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.NumericTests.ULong;

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
    [InlineData(ULongId.Example, true)]
    public void ConvertFromThisToThatWithExpectedResults(string? from, bool hasValue = false)
    {
        var @this = ULongId.Parse(from);

        @this.HasValue.Should().Be(hasValue);
    }
}
