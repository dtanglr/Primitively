using Primitively;

// https://en.wikipedia.org/wiki/Celsius
// https://en.wikipedia.org/wiki/Conversion_of_scales_of_temperature

namespace Acme.Temperature;

[Double(2, Minimum = -273.15d)]
public partial record struct Celsius : ITemperature<Celsius>
{
    public static Celsius AbsoluteZero => new(-273.15d);

    public static Celsius WaterMeltingPoint => new(0d);

    public static Celsius WaterBoilingPoint => new(99.9839d);

    public static explicit operator Kelvin(Celsius value) => new(value + 273.15d);
    public static explicit operator Fahrenheit(Celsius value) => new((value * (9d / 5d)) + 32d);
    public static explicit operator Rankine(Celsius value) => new((value + 273.15d) * (9d / 5d));
}
