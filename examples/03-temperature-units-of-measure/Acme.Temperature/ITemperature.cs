namespace Acme.Temperature;

public interface ITemperature<out T> where T : ITemperature<T>
{
    static abstract T AbsoluteZero { get; }
    static abstract T WaterMeltingPoint { get; }
    static abstract T WaterBoilingPoint { get; }
}
