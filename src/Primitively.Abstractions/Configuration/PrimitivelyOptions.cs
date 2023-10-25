namespace Primitively.Configuration;

public class PrimitivelyOptions
{
    public PrimitiveRegistry Registry { get; } = new();

    public PrimitivelyOptions Register(IPrimitiveRepository repository)
    {
        Registry.Add(repository);

        return this;
    }
}
