using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.Types.StringTests.SevenDigitsTests;

public class EqualityTests
{
    private const string Value = "1234567";
    private const string OtherValue = "2345678";

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("invalid")]
    [InlineData(Value)]
    public void WhenTheSameType_ThisEqualsThat(string value)
    {
        var @this = SevenDigits.Parse(value);
        var that = SevenDigits.Parse(value);

        // This == That
        @this.Equals(that).Should().BeTrue();
        @this.Equals((SevenDigits)that.Value).Should().BeTrue();
        @this.Value?.Equals(that).Should().BeTrue();
        @this.Value?.Equals(that.Value).Should().BeTrue();
        (@this == that).Should().BeTrue();
        (@this.Value == that.Value).Should().BeTrue();
        (@this != that).Should().BeFalse();
        (@this.Value != that.Value).Should().BeFalse();

        // That == This
        that.Equals(@this).Should().BeTrue();
        that.Equals((SevenDigits)@this.Value).Should().BeTrue();
        that.Value?.Equals(@this).Should().BeTrue();
        that.Value?.Equals(@this.Value).Should().BeTrue();
        (that == @this).Should().BeTrue();
        (that.Value == @this.Value).Should().BeTrue();
        (that != @this).Should().BeFalse();
        (that.Value != @this.Value).Should().BeFalse();
    }

    [Fact]
    public void WhenTheSameType_ThisDoesNotEqualThat()
    {
        var @this = SevenDigits.Parse(Value);
        var that = SevenDigits.Parse(OtherValue);

        // This == That
        @this.Equals(that).Should().BeFalse();
        (@this == that).Should().BeFalse();
        (@this.Value == that).Should().BeFalse();
        (@this != that).Should().BeTrue();
        (@this.Value != that).Should().BeTrue();

        // That == This
        that.Equals(@this).Should().BeFalse();
        (that == @this).Should().BeFalse();
        (that == (SevenDigits)@this.Value).Should().BeFalse();
        (that != @this).Should().BeTrue();
        (that != (SevenDigits)@this.Value).Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(Value)]
    public void WhenTheOtherTypeWithSameValueType_ThisNotEqualsThat(string value)
    {
        var @this = SevenDigits.Parse(value);
        var that = EightDigits.Parse(value);

        // This != That
        @this.Equals(that).Should().BeFalse();

        // That != This
        that.Equals(@this).Should().BeFalse();
    }
}
