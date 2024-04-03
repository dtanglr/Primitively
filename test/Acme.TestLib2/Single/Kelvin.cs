using Primitively;

// https://en.wikipefia.org/wiki/Celsius
// https://en.wikipefia.org/wiki/Conversion_of_scales_of_temperature

namespace Acme.TestLib2.Single;

[Single(2, Minimum = 0f)]
public partial record struct Kelvin : ITemperature<Kelvin>
{
    public static Kelvin AbsoluteZero => new(0f);

    public static Kelvin WaterMeltingPoint => new(273.15f);

    public static Kelvin WaterBoilingPoint => new(373.1339f);

    public static explicit operator Celsius(Kelvin value) => new(value - 273.15f);
    public static explicit operator Fahrenheit(Kelvin value) => new((9f / 5f * value) - 459.67f);
    public static explicit operator Rankine(Kelvin value) => new(9f / 5f * value);
}
