using Primitively;

// https://en.wikipedia.org/wiki/Celsius
// https://en.wikipedia.org/wiki/Conversion_of_scales_of_temperature

namespace Acme.TestLib2.Double;

[Double(2, Minimum = -459.67d)]
public partial record struct Fahrenheit : ITemperature<Fahrenheit>
{
    public static Fahrenheit AbsoluteZero => new(-459.67d);

    public static Fahrenheit WaterMeltingPoint => new(32d);

    public static Fahrenheit WaterBoilingPoint => new(211.97102d);

    public static explicit operator Celsius(Fahrenheit value) => new((value - 32d) * (5d / 9d));
    public static explicit operator Kelvin(Fahrenheit value) => new((value + 459.67d) * (5d / 9d));
    public static explicit operator Rankine(Fahrenheit value) => new(value + 459.67d);
}
