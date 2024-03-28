using Primitively;

// https://en.wikipedia.org/wiki/Celsius
// https://en.wikipedia.org/wiki/Conversion_of_scales_of_temperature

namespace Acme.TestLib;

[Double(2, Minimum = -273.15d)]
public partial record struct Celsius : ITemperature<Celsius>
{
    public static Celsius AbsoluteZero => new(-273.15d);

    public static Celsius WaterMeltingPoint => new(0d);

    public static Celsius WaterBoilingPoint => new(99.9839d);

    public static explicit operator Kelvin(Celsius value) => new(value + 273.15d);
    public static explicit operator Fahrenheit(Celsius value) => new((9d / 5d * value) + 32d);
    public static explicit operator Rankine(Celsius value) => new((value + 273.15d) * (9d / 5d));
}

[Double(Minimum = -459.67d)]
public partial record struct Fahrenheit : ITemperature<Fahrenheit>
{
    public static Fahrenheit AbsoluteZero => new(-459.67d);

    public static Fahrenheit WaterMeltingPoint => new(32d);

    public static Fahrenheit WaterBoilingPoint => new(211.97102d);

    public static explicit operator Celsius(Fahrenheit value) => new((value - 32d) * (5d / 9d));
    public static explicit operator Kelvin(Fahrenheit value) => new((value + 459.67d) * (5d / 9d));
    public static explicit operator Rankine(Fahrenheit value) => new(value + 459.67d);
}

[Double(Minimum = 0d)]
public partial record struct Kelvin : ITemperature<Kelvin>
{
    public static Kelvin AbsoluteZero => new(0d);

    public static Kelvin WaterMeltingPoint => new(273.15d);

    public static Kelvin WaterBoilingPoint => new(373.1339d);

    public static explicit operator Celsius(Kelvin value) => new(value - 273.15d);
    public static explicit operator Fahrenheit(Kelvin value) => new((9d / 5d * value) - 459.67d);
    public static explicit operator Rankine(Kelvin value) => new(9d / 5d * value);
}

[Double(Minimum = 0d)]
public partial record struct Rankine : ITemperature<Rankine>
{
    public static Rankine AbsoluteZero => new(0d);

    public static Rankine WaterMeltingPoint => new(491.67d);

    public static Rankine WaterBoilingPoint => new(671.64102d);

    public static explicit operator Celsius(Rankine value) => new((5d / 9d * value) - 273.15d);
    public static explicit operator Fahrenheit(Rankine value) => new(value - 459.67d);
    public static explicit operator Kelvin(Rankine value) => new(5d / 9d * value);
}

public interface ITemperature<out T> where T : ITemperature<T>
{
#if NET7_0_OR_GREATER
    static abstract T AbsoluteZero { get; }
    static abstract T WaterMeltingPoint { get; }
    static abstract T WaterBoilingPoint { get; }
#endif
}
