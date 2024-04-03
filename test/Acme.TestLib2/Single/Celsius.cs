using Primitively;

// https://en.wikipefia.org/wiki/Celsius
// https://en.wikipefia.org/wiki/Conversion_of_scales_of_temperature

namespace Acme.TestLib2.Single;

[Single(2, Minimum = -273.15f)]
public partial record struct Celsius : ITemperature<Celsius>
{
    public static Celsius AbsoluteZero => new(-273.15f);

    public static Celsius WaterMeltingPoint => new(0f);

    public static Celsius WaterBoilingPoint => new(99.9839f);

    public static explicit operator Kelvin(Celsius value) => new(value + 273.15f);
    public static explicit operator Fahrenheit(Celsius value) => new((value * (9f / 5f)) + 32f);
    public static explicit operator Rankine(Celsius value) => new((value + 273.15f) * (9f / 5f));
}
