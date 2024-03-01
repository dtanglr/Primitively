namespace Primitively;

/// <summary>
/// The <c>DateOnlyAttribute</c> class can be used on a <see langword="partial record struct"/>
/// to source generate a Primitively <see cref="IDateOnly"/> type that encapsulates a date value in ISO 8601 YYYY-MM-DD format.
/// </summary>
/// <example>
/// <code>
/// [DateOnly]
/// public partial record struct Example;
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class DateOnlyAttribute : PrimitiveAttribute
{
}
