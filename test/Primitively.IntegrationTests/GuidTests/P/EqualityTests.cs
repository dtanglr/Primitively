using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.GuidTests.P;

public class EqualityTests
{
    private const string Value = "(2c48c152-7cb7-4f51-8f01-704454f36e60)";
    private const string OtherValue = "(3c48c152-7cb7-4f51-8f01-704454f36e66)";

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("invalid")]
    [InlineData(Value)]
    public void WhenTheSameType_ThisEqualsThat(string? value)
    {
        var @this = ThirtyEightDigitsWithHyphensAndParentheses.Parse(value);
        var that = ThirtyEightDigitsWithHyphensAndParentheses.Parse(value);

        // This == That
        @this.Equals(that).Should().BeTrue();
        (@this == that).Should().BeTrue();
        (@this != that).Should().BeFalse();
        @this.CompareTo(that).Should().Be(0);

        // That == This
        that.Equals(@this).Should().BeTrue();
        (that == @this).Should().BeTrue();
        (that != @this).Should().BeFalse();
        that.CompareTo(@this).Should().Be(0);
    }

    [Fact]
    public void WhenTheSameType_ThisDoesNotEqualThat()
    {
        var @this = ThirtyEightDigitsWithHyphensAndParentheses.Parse(Value);
        var that = ThirtyEightDigitsWithHyphensAndParentheses.Parse(OtherValue);

        // This == That
        @this.Equals(that).Should().BeFalse();
        (@this == that).Should().BeFalse();
        (@this != that).Should().BeTrue();
        @this.CompareTo(that).Should().NotBe(0);

        // That == This
        that.Equals(@this).Should().BeFalse();
        (that == @this).Should().BeFalse();
        (that != @this).Should().BeTrue();
        that.CompareTo(@this).Should().NotBe(0);
    }

    [Fact]
    public void WhenTheOtherTypeWithSameValueType_ThisNotEqualsThat()
    {
        var @this = ThirtyEightDigitsWithHyphensAndParentheses.Parse(Value);
        var that = DefaultThirtySixDigitsWithHyphens.Parse(Value);

        // This != That
        @this.Equals(that).Should().BeFalse();

        // That != This
        that.Equals(@this).Should().BeFalse();
    }
}
