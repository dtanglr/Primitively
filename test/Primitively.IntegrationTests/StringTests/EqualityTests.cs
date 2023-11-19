using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.StringTests;

public class EqualityTests
{
    private const string Value = "1234567";
    private const string OtherValue = "2345678";

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("invalid")]
    [InlineData(Value)]
    public void WhenTheSameType_ThisEqualsThat(string? value)
    {
        var @this = SevenDigits.Parse(value);
        var that = SevenDigits.Parse(value);

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
        var @this = SevenDigits.Parse(Value);
        var that = SevenDigits.Parse(OtherValue);

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

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(Value)]
    public void WhenTheOtherTypeWithSameValueType_ThisNotEqualsThat(string? value)
    {
        var @this = SevenDigits.Parse(value);
        var that = EightDigits.Parse(value);

        // This != That
        @this.Equals(that).Should().BeFalse();

        // That != This
        that.Equals(@this).Should().BeFalse();
    }
}
