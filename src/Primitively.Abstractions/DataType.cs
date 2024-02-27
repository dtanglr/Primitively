namespace Primitively;

/// <summary>
/// The enum representation of each .net type supported by Primitively
/// </summary>
public enum DataType
{
    /// <summary>
    /// Represents a Primitively <see cref="IByte"/> type that encapsulates a <see cref="byte"/> value
    /// </summary>
    Byte,

    /// <summary>
    /// Represents a Primitively <see cref="IDateOnly"/> type that encapsulates a <see cref="DateTime"/> value
    /// in default Iso8601 format of yyyy-MM-dd
    /// </summary>
    DateOnly,

    /// <summary>
    /// Represents a Primitively <see cref="IGuid"/> type that encapsulates a <see cref="System.Guid"/> value
    /// </summary>
    Guid,

    /// <summary>
    /// Represents a Primitively <see cref="IInt"/> type that encapsulates an <see cref="int"/> value
    /// </summary>
    Int,

    /// <summary>
    /// Represents a Primitively <see cref="ILong"/> type that encapsulates a <see cref="long"/> value
    /// </summary>
    Long,

    /// <summary>
    /// Represents a Primitively <see cref="ISByte"/> type that encapsulates an <see cref="sbyte"/> value
    /// </summary>
    SByte,

    /// <summary>
    /// Represents a Primitively <see cref="IShort"/> type that encapsulates a <see cref="short"/> value
    /// </summary>
    Short,

    /// <summary>
    /// Represents a Primitively <see cref="IString"/> type that encapsulates a <see cref="string"/> value
    /// </summary>
    String,

    /// <summary>
    /// Represents a Primitively <see cref="IUInt"/> type that encapsulates a <see cref="uint"/> value
    /// </summary>
    UInt,

    /// <summary>
    /// Represents a Primitively <see cref="IULong"/> type that encapsulates a <see cref="ulong"/> value
    /// </summary>
    ULong,

    /// <summary>
    /// Represents a Primitively <see cref="IUShort"/> type that encapsulates a <see cref="ushort"/> value
    /// </summary>
    UShort
}
