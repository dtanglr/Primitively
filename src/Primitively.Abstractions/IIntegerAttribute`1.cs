namespace Primitively;

/// <summary>
/// An interface used to provide a default contract to Primitively attributes that are used to source generate integer data types.
/// </summary>
/// <typeparam name="T">The .net integer type that the Primitively type encapsulates</typeparam>
public interface IIntegerAttribute<T> : IIntegerAttribute where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
{
    /// <summary>
    /// The minimum value of the Primitively type
    /// </summary>
    /// <value>The minimum value</value>
    new T Minimum { get; set; }

    /// <summary>
    /// The maximum value of the Primitively type
    /// </summary>
    /// <value>The maximum value</value>
    new T Maximum { get; set; }
}
