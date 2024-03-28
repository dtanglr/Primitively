namespace Acme.Temperature.UnitTests;

public class CelsiusTests
{
    [Fact]
    public void AbsoluteZero_In_Sut_Scale_Converts_To_AbsoluteZero_In_Other_Scales()
    {
        // Assign
        var celsius = Celsius.AbsoluteZero;

        // Act
        var fahrenheit = (Fahrenheit)celsius;
        var kelvin = (Kelvin)celsius;
        var rankine = (Rankine)celsius;

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
        var celsius = Celsius.WaterMeltingPoint;

        // Act
        var fahrenheit = (Fahrenheit)celsius;
        var kelvin = (Kelvin)celsius;
        var rankine = (Rankine)celsius;

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
        var celsius = Celsius.WaterBoilingPoint;

        // Act
        var fahrenheit = (Fahrenheit)celsius;
        var kelvin = (Kelvin)celsius;
        var rankine = (Rankine)celsius;

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
        var celsius = new Celsius(Celsius.Minimum);

        // Act
        double kelvin = (Kelvin)celsius;
        double fahrenheit = (Fahrenheit)celsius;
        double rankine = (Rankine)celsius;

        // Assert
        celsius.Should().BeEquivalentTo((Celsius)Celsius.Minimum);
        fahrenheit.Should().Be(Fahrenheit.Minimum);
        kelvin.Should().Be(Kelvin.Minimum);
        rankine.Should().Be(Rankine.Minimum);
    }
}
