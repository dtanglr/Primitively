namespace Primitively.Configuration;

public class PrimitivelyOptions
{
    public PrimitiveRegistry Registry { get; } = new();

    public void Register(IPrimitiveRepository repository) => Registry.Add(repository);
}
