namespace Acme.TestLib2;

public interface ITemperature<out T> where T : ITemperature<T>
{
#if NET7_0_OR_GREATER
    static abstract T AbsoluteZero { get; }
    static abstract T WaterMeltingPoint { get; }
    static abstract T WaterBoilingPoint { get; }
#endif
}
