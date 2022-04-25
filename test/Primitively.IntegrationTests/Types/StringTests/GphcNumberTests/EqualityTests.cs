using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.Types.StringTests.GphcNumberTests;

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
        var @this = GphcNumber.Parse(value);
        var that = GphcNumber.Parse(value);

        // This == That
        @this.Equals(that).Should().BeTrue();
        @this.Equals((GphcNumber)that.Value).Should().BeTrue();
        @this.Value?.Equals(that).Should().BeTrue();
        @this.Value?.Equals(that.Value).Should().BeTrue();
        (@this == that).Should().BeTrue();
        (@this.Value == that.Value).Should().BeTrue();
        (@this != that).Should().BeFalse();
        (@this.Value != that.Value).Should().BeFalse();

        // That == This
        that.Equals(@this).Should().BeTrue();
        that.Equals((GphcNumber)@this.Value).Should().BeTrue();
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
        var @this = GphcNumber.Parse(Value);
        var that = GphcNumber.Parse(OtherValue);

        // This == That
        @this.Equals(that).Should().BeFalse();
        (@this == that).Should().BeFalse();
        (@this.Value == that).Should().BeFalse();
        (@this != that).Should().BeTrue();
        (@this.Value != that).Should().BeTrue();

        // That == This
        that.Equals(@this).Should().BeFalse();
        (that == @this).Should().BeFalse();
        (that == (GphcNumber)@this.Value).Should().BeFalse();
        (that != @this).Should().BeTrue();
        (that != (GphcNumber)@this.Value).Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(Value)]
    public void WhenTheOtherTypeWithSameValueType_ThisNotEqualsThat(string value)
    {
        var @this = GphcNumber.Parse(value);
        var that = Postcode.Parse(value);

        // This != That
        @this.Equals(that).Should().BeFalse();

        // That != This
        that.Equals(@this).Should().BeFalse();
    }
}
