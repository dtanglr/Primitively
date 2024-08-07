﻿namespace Primitively;

/// <summary>
/// This enum represents each Primitively type variation.
/// </summary>
public enum DataType
{
    /// <summary>
    /// Represents an <see cref="IByte"/> type that encapsulates a <see cref="byte"/> value.
    /// </summary>
    Byte,

    /// <summary>
    /// Represents an <see cref="IDateOnly"/> type that encapsulates a date value in ISO 8601 YYYY-MM-DD format.
    /// </summary>
    DateOnly,

    /// <summary>
    /// Represents an <see cref="IDecimal"/> type that encapsulates a <see cref="decimal"/> value.
    /// </summary>
    Decimal,

    /// <summary>
    /// Represents an <see cref="IDouble"/> type that encapsulates a <see cref="double"/> value.
    /// </summary>
    Double,

    /// <summary>
    /// Represents an <see cref="IGuid"/> type that encapsulates a <see cref="System.Guid"/> value.
    /// </summary>
    Guid,

    /// <summary>
    /// Represents an <see cref="IInt"/> type that encapsulates an <see cref="int"/> value.
    /// </summary>
    Int,

    /// <summary>
    /// Represents an <see cref="ILong"/> type that encapsulates a <see cref="long"/> value.
    /// </summary>
    Long,

    /// <summary>
    /// Represents an <see cref="ISByte"/> type that encapsulates an <see cref="sbyte"/> value.
    /// </summary>
    SByte,

    /// <summary>
    /// Represents an <see cref="IShort"/> type that encapsulates a <see cref="short"/> value.
    /// </summary>
    Short,

    /// <summary>
    /// Represents an <see cref="ISingle"/> type that encapsulates a <see cref="float"/> value.
    /// </summary>
    Single,

    /// <summary>
    /// Represents an <see cref="IString"/> type that encapsulates a <see cref="string"/> value.
    /// </summary>
    String,

    /// <summary>
    /// Represents an <see cref="IUInt"/> type that encapsulates a <see cref="uint"/> value.
    /// </summary>
    UInt,

    /// <summary>
    /// Represents an <see cref="IULong"/> type that encapsulates a <see cref="ulong"/> value.
    /// </summary>
    ULong,

    /// <summary>
    /// Represents an <see cref="IUShort"/> type that encapsulates a <see cref="ushort"/> value.
    /// </summary>
    UShort
}
