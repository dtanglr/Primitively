using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.IntegerTests.Long;

public class InstantiationTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("-1", true)]
    [InlineData("0", true)]
    [InlineData("00", true)]
    [InlineData("001", true)]
    [InlineData(LongId.Example, true)]
    public void ConvertFromThisToThatWithExpectedResults(string? from, bool hasValue = false)
    {
        var @this = LongId.Parse(from);

        @this.HasValue.Should().Be(hasValue);
    }
}
