namespace Primitively.Configuration;

public sealed class PrimitivelyOptions
{
    public PrimitiveRegistry Registry { get; } = new();

    public PrimitivelyOptions Register(IPrimitiveRepository repository)
    {
        Registry.Add(repository);

        return this;
    }
}
