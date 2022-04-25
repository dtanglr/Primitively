using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.Types.StringTests.PostcodeTests;

public class EqualityTests
{
    private const string Value = "DN551PT";
    private const string OtherValue = "KN551PK";

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("invalid")]
    [InlineData(Value)]
    public void WhenTheSameType_ThisEqualsThat(string value)
    {
        var @this = Postcode.Parse(value);
        var that = Postcode.Parse(value);

        // This == That
        @this.Equals(that).Should().BeTrue();
        @this.Equals((Postcode)that.Value).Should().BeTrue();
        @this.Value?.Equals(that).Should().BeTrue();
        @this.Value?.Equals(that.Value).Should().BeTrue();
        (@this == that).Should().BeTrue();
        (@this.Value == that.Value).Should().BeTrue();
        (@this != that).Should().BeFalse();
        (@this.Value != that.Value).Should().BeFalse();

        // That == This
        that.Equals(@this).Should().BeTrue();
        that.Equals((Postcode)@this.Value).Should().BeTrue();
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
        var @this = Postcode.Parse(Value);
        var that = Postcode.Parse(OtherValue);

        // This == That
        @this.Equals(that).Should().BeFalse();
        (@this == that).Should().BeFalse();
        (@this.Value == that).Should().BeFalse();
        (@this != that).Should().BeTrue();
        (@this.Value != that).Should().BeTrue();

        // That == This
        that.Equals(@this).Should().BeFalse();
        (that == @this).Should().BeFalse();
        (that == (Postcode)@this.Value).Should().BeFalse();
        (that != @this).Should().BeTrue();
        (that != (Postcode)@this.Value).Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(Value)]
    public void WhenTheOtherTypeWithSameValueType_ThisNotEqualsThat(string value)
    {
        var @this = Postcode.Parse(value);
        var that = NhsNumber.Parse(value);

        // This != That
        @this.Equals(that).Should().BeFalse();

        // That != This
        that.Equals(@this).Should().BeFalse();
    }
}
