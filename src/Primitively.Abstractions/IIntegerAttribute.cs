namespace Primitively;

/// <summary>
/// An interface used to provide a default contract to Primitively attributes that are used to source generate integer data types.
/// </summary>
public interface IIntegerAttribute : IPimitivelyAttribute
{
    /// <summary>
    /// The minimum value of the Primitively integer type
    /// </summary>
    object Minimum { get; }

    /// <summary>
    /// The maximum value of the Primitively integer type
    /// </summary>
    object Maximum { get; }
}
