using Primitively;

// https://en.wikipedia.org/wiki/Celsius
// https://en.wikipedia.org/wiki/Conversion_of_scales_of_temperature

namespace Acme.Temperature;

[Double(2, Minimum = 0d)]
public partial record struct Rankine : ITemperature<Rankine>
{
    public static Rankine AbsoluteZero => new(0d);

    public static Rankine WaterMeltingPoint => new(491.67d);

    public static Rankine WaterBoilingPoint => new(671.64102d);

    public static explicit operator Celsius(Rankine value) => new((5d / 9d * value) - 273.15d);
    public static explicit operator Fahrenheit(Rankine value) => new(value - 459.67d);
    public static explicit operator Kelvin(Rankine value) => new(5d / 9d * value);
}
