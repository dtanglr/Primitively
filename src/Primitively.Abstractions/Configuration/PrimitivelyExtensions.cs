namespace Primitively.Configuration;

public static class PrimitivelyExtensions
{
    public static List<IPrimitiveRepository> Add<TPrimitiveRepository>(this List<IPrimitiveRepository> repositories)
        where TPrimitiveRepository : class, IPrimitiveRepository, new()
    {
        if (!repositories.Any(r => r.GetType() == typeof(TPrimitiveRepository)))
        {
            repositories.Add(new TPrimitiveRepository());
        }

        return repositories;
    }
}
