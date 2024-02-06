using System.Globalization;
using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.DateOnlyTests;

public class InstantiationTests
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("0001-01-01")]
    [InlineData("9999-99-99")]
    [InlineData("2022-02-31")]
    [InlineData("2022-31-01")]
    [InlineData("2022/01/01")]
    [InlineData("01/01/2022")]
    [InlineData("31/01/2022")]
    [InlineData("01/31/2022")]
    [InlineData("2022-01-01", true)]
    public void ConvertFromThisToThatWithExpectedResults(string from, bool hasValue = default)
    {
#if NET6_0_OR_GREATER
        var expectedDateOnly = hasValue ? DateOnly.ParseExact(from, BirthDate.Format, CultureInfo.InvariantCulture) : default;
        var expectedDateTime = hasValue ? DateTime.ParseExact(from, BirthDate.Format, CultureInfo.InvariantCulture) : default;
        var expectedString = expectedDateOnly.ToString(BirthDate.Format);

        var @this = (BirthDate)from;
        string to = @this;
        var that = BirthDate.Parse(to);
        var and = new BirthDate(that);
        string back = and;

        @this.HasValue.Should().Be(hasValue);
        ((DateOnly)@this).Should().Be(expectedDateOnly);
        ((DateTime)@this).Should().Be(expectedDateTime);
        @this.ToString().Should().Be(expectedString);
        to.Should().Be(expectedString);
        that.HasValue.Should().Be(hasValue);
        ((DateOnly)that).Should().Be(expectedDateOnly);
        ((DateTime)that).Should().Be(expectedDateTime);
        that.ToString().Should().Be(expectedString);
        and.HasValue.Should().Be(hasValue);
        ((DateOnly)and).Should().Be(expectedDateOnly);
        ((DateTime)and).Should().Be(expectedDateTime);
        and.ToString().Should().Be(expectedString);
        back.Should().Be(expectedString);
#else
        var expectedDateTime = hasValue ? DateTime.ParseExact(from, BirthDate.Format, CultureInfo.InvariantCulture) : default;
        var expectedString = expectedDateTime.ToString(BirthDate.Format);

        var @this = (BirthDate)from;
        string to = @this;
        var that = BirthDate.Parse(to);
        var and = new BirthDate(that);
        string back = and;

        @this.HasValue.Should().Be(hasValue);
        ((DateTime)@this).Should().Be(expectedDateTime);
        @this.ToString().Should().Be(expectedString);
        to.Should().Be(expectedString);
        that.HasValue.Should().Be(hasValue);
        ((DateTime)that).Should().Be(expectedDateTime);
        that.ToString().Should().Be(expectedString);
        and.HasValue.Should().Be(hasValue);
        ((DateTime)and).Should().Be(expectedDateTime);
        and.ToString().Should().Be(expectedString);
        back.Should().Be(expectedString);
#endif
    }
}
