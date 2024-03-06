namespace Primitively;

/// <summary>
/// The <see cref="DateOnlyAttribute"/> should be used on a <c>partial record struct</c> to source generate
/// a Primitively <see cref="IDateOnly"/> type that encapsulates a date value in ISO 8601 YYYY-MM-DD format.
/// </summary>
/// <example>
/// This example shows how to use the DateOnly attribute to source generate a Primitively <see cref="IDateOnly"/> type.
/// <code>
/// [DateOnly]
/// public partial record struct Example;
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class DateOnlyAttribute : PrimitiveAttribute
{
}
