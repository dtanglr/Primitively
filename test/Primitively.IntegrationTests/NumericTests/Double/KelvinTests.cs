using Acme.TestLib2.Double;
using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.NumericTests.Double;

public class KelvinTests
{
    [Fact]
    public void AbsoluteZero_In_Sut_Scale_Converts_To_AbsoluteZero_In_Other_Scales()
    {
        // Assign
        var kelvin = Kelvin.AbsoluteZero;

        // Act
        var celsius = (Celsius)kelvin;
        var fahrenheit = (Fahrenheit)kelvin;
        var rankine = (Rankine)kelvin;

        // Assert
        celsius.Should().BeEquivalentTo(Celsius.AbsoluteZero);
        fahrenheit.Should().BeEquivalentTo(Fahrenheit.AbsoluteZero);
        kelvin.Should().BeEquivalentTo(Kelvin.AbsoluteZero);
        rankine.Should().BeEquivalentTo(Rankine.AbsoluteZero);
    }

    [Fact]
    public void WaterMeltingPoint_In_Sut_Scale_Converts_To_WaterMeltingPoint_In_Other_Scales()
    {
        // Assign
        var kelvin = Kelvin.WaterMeltingPoint;

        // Act
        var celsius = (Celsius)kelvin;
        var fahrenheit = (Fahrenheit)kelvin;
        var rankine = (Rankine)kelvin;

        // Assert
        celsius.Should().BeEquivalentTo(Celsius.WaterMeltingPoint);
        fahrenheit.Should().BeEquivalentTo(Fahrenheit.WaterMeltingPoint);
        kelvin.Should().BeEquivalentTo(Kelvin.WaterMeltingPoint);
        rankine.Should().BeEquivalentTo(Rankine.WaterMeltingPoint);
    }

    [Fact]
    public void WaterBoilingPoint_In_Sut_Scale_Converts_To_WaterBoilingPoint_In_Other_Scales()
    {
        // Assign
        var kelvin = Kelvin.WaterBoilingPoint;

        // Act
        var celsius = (Celsius)kelvin;
        var fahrenheit = (Fahrenheit)kelvin;
        var rankine = (Rankine)kelvin;

        // Assert
        celsius.Should().BeEquivalentTo(Celsius.WaterBoilingPoint);
        fahrenheit.Should().BeEquivalentTo(Fahrenheit.WaterBoilingPoint);
        kelvin.Should().BeEquivalentTo(Kelvin.WaterBoilingPoint);
        rankine.Should().BeEquivalentTo(Rankine.WaterBoilingPoint);
    }

    [Fact]
    public void Minimum_Sut_Scale_Converts_To_Minimum_In_Other_Scales()
    {
        // Assign
        var kelvin = new Kelvin(Kelvin.Minimum);

        // Act
        double celsius = (Celsius)kelvin;
        double fahrenheit = (Fahrenheit)kelvin;
        double rankine = (Rankine)kelvin;

        // Assert
        celsius.Should().Be(Celsius.Minimum);
        fahrenheit.Should().Be(Fahrenheit.Minimum);
        kelvin.Should().BeEquivalentTo((Kelvin)Kelvin.Minimum);
        rankine.Should().Be(Rankine.Minimum);
    }
}
