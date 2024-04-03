using Primitively;

// https://en.wikipedia.org/wiki/Celsius
// https://en.wikipedia.org/wiki/Conversion_of_scales_of_temperature

namespace Acme.TestLib2.Double;

[Double(2, Minimum = 0d)]
public partial record struct Kelvin : ITemperature<Kelvin>
{
    public static Kelvin AbsoluteZero => new(0d);

    public static Kelvin WaterMeltingPoint => new(273.15d);

    public static Kelvin WaterBoilingPoint => new(373.1339d);

    public static explicit operator Celsius(Kelvin value) => new(value - 273.15d);
    public static explicit operator Fahrenheit(Kelvin value) => new((9d / 5d * value) - 459.67d);
    public static explicit operator Rankine(Kelvin value) => new(9d / 5d * value);
}
