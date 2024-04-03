using Acme.TestLib2.Double;

namespace Primitively.IntegrationTests.NumericTests.Double;

public class RankineTests
{
    [Fact]
    public void AbsoluteZero_In_Sut_Scale_Converts_To_AbsoluteZero_In_Other_Scales()
    {
        // Assign
        var rankine = Rankine.AbsoluteZero;

        // Act
        var celsius = (Celsius)rankine;
        var fahrenheit = (Fahrenheit)rankine;
        var kelvin = (Kelvin)rankine;

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
        var rankine = Rankine.WaterMeltingPoint;

        // Act
        var celsius = (Celsius)rankine;
        var fahrenheit = (Fahrenheit)rankine;
        var kelvin = (Kelvin)rankine;

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
        var rankine = Rankine.WaterBoilingPoint;

        // Act
        var celsius = (Celsius)rankine;
        var fahrenheit = (Fahrenheit)rankine;
        var kelvin = (Kelvin)rankine;

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
        var rankine = new Rankine(Rankine.Minimum);

        // Act
        double celsius = (Celsius)rankine;
        double fahrenheit = (Fahrenheit)rankine;
        double kelvin = (Kelvin)rankine;

        // Assert
        celsius.Should().Be(Celsius.Minimum);
        fahrenheit.Should().Be(Fahrenheit.Minimum);
        kelvin.Should().Be(Kelvin.Minimum);
        rankine.Should().BeEquivalentTo((Rankine)Rankine.Minimum);
    }
}
