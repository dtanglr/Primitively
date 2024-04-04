using Acme.TestLib2.Double;
using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.NumericTests.Double;

public class FahrenheitTests
{
    [Fact]
    public void AbsoluteZero_In_Sut_Scale_Converts_To_AbsoluteZero_In_Other_Scales()
    {
        // Assign
        var fahrenheit = Fahrenheit.AbsoluteZero;

        // Act
        var celsius = (Celsius)fahrenheit;
        var kelvin = (Kelvin)fahrenheit;
        var rankine = (Rankine)fahrenheit;

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
        var fahrenheit = Fahrenheit.WaterMeltingPoint;

        // Act
        var celsius = (Celsius)fahrenheit;
        var kelvin = (Kelvin)fahrenheit;
        var rankine = (Rankine)fahrenheit;

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
        var fahrenheit = Fahrenheit.WaterBoilingPoint;

        // Act
        var celsius = (Celsius)fahrenheit;
        var kelvin = (Kelvin)fahrenheit;
        var rankine = (Rankine)fahrenheit;

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
        var fahrenheit = new Fahrenheit(Fahrenheit.Minimum);

        // Act
        double celsius = (Celsius)fahrenheit;
        double kelvin = (Kelvin)fahrenheit;
        double rankine = (Rankine)fahrenheit;

        // Assert
        celsius.Should().Be(Celsius.Minimum);
        fahrenheit.Should().BeEquivalentTo((Fahrenheit)Fahrenheit.Minimum);
        kelvin.Should().Be(Kelvin.Minimum);
        rankine.Should().Be(Rankine.Minimum);
    }
}
