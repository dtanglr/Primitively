using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.NumericTests.Single;

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
    [InlineData(SingleId.Example, true)]
    public void ConvertFromThisToThatWithExpectedResults(string? from, bool hasValue = false)
    {
        var @this = SingleId.Parse(from);

        @this.HasValue.Should().Be(hasValue);
    }
}
