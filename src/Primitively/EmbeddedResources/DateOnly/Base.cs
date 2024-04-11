﻿readonly partial record struct PRIMITIVE_TYPE :
    global::Primitively.IDateOnly,
    global::Primitively.IPrimitiveInfo<global::PRIMITIVE_INFO_TYPE>,
    global::System.IEquatable<PRIMITIVE_TYPE>,
    global::System.IComparable<PRIMITIVE_TYPE>PRIMITIVE_IVALIDATABLEOBJECT
{
#if NET6_0_OR_GREATER
    public const string Example = "PRIMITIVE_EXAMPLE";
    public const string Format = "PRIMITIVE_FORMAT";
    public const int Length = PRIMITIVE_LENGTH;

    private static readonly global::PRIMITIVE_INFO_TYPE _info = new
    (
        Type: typeof(PRIMITIVE_TYPE),
        Example: Example,
        CreateFrom: (value) => (PRIMITIVE_TYPE)value,
        Format: Format,
        Length: Length
    );

    private readonly global::System.DateTime _value;

    public PRIMITIVE_TYPE(global::System.DateOnly value)
    {
        _value = new global::System.DateTime(value.Year, value.Month, value.Day);
    }

    public PRIMITIVE_TYPE(global::System.DateTime value)
    {
        _value = value;
    }

    private PRIMITIVE_TYPE(string value)
    {
        global::System.DateTime.TryParseExact(value, Format, global::System.Globalization.CultureInfo.InvariantCulture, global::System.Globalization.DateTimeStyles.None, out var result);

        _value = result;
    }

    object global::Primitively.IPrimitive.Value => _value;

    global::System.DateTime global::Primitively.IPrimitive<global::System.DateTime>.Value => _value;

    global::Primitively.PrimitiveInfo global::Primitively.IPrimitiveInfo.Info => _info;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public global::Primitively.DataType DataType => global::Primitively.DataType.PRIMITIVE_DATA_TYPE;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public bool HasValue => _value != default;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public global::PRIMITIVE_INFO_TYPE Info => _info;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public global::System.Type ValueType => typeof(global::System.DateTime);

    public bool Equals(PRIMITIVE_TYPE other) => _value == other._value;
    public int CompareTo(PRIMITIVE_TYPE other) => _value.CompareTo(other._value);
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => _value.ToString(Format);

    public static implicit operator string(PRIMITIVE_TYPE value) => value.ToString();
    public static implicit operator global::System.DateOnly(PRIMITIVE_TYPE value) => DateOnly.FromDateTime(value._value);
    public static explicit operator PRIMITIVE_TYPE(global::System.DateOnly value) => new(value);
    public static explicit operator global::System.DateTime(PRIMITIVE_TYPE value) => value._value;
    public static explicit operator PRIMITIVE_TYPE(global::System.DateTime value) => new(value);
    public static explicit operator PRIMITIVE_TYPE(string value) => new(value);

    public static global::PRIMITIVE_INFO_TYPE TypeInfo => _info;

    public static PRIMITIVE_TYPE Parse(string value) => new(value);
    public static bool TryParse(string value, out PRIMITIVE_TYPE result) => (result = new(value)).HasValue;
#else
    public const string Example = "PRIMITIVE_EXAMPLE";
    public const string Format = "PRIMITIVE_FORMAT";
    public const int Length = PRIMITIVE_LENGTH;

    private static readonly global::PRIMITIVE_INFO_TYPE _info = new
    (
        Type: typeof(PRIMITIVE_TYPE),
        Example: Example,
        CreateFrom: (value) => (PRIMITIVE_TYPE)value,
        Format: Format,
        Length: Length
    );

    private readonly global::System.DateTime _value;

    public PRIMITIVE_TYPE(global::System.DateTime value)
    {
        _value = new global::System.DateTime(value.Year, value.Month, value.Day);
    }

    private PRIMITIVE_TYPE(string value)
    {
        global::System.DateTime.TryParseExact(value, Format, global::System.Globalization.CultureInfo.InvariantCulture, global::System.Globalization.DateTimeStyles.None, out var result);

        _value = result;
    }

    object global::Primitively.IPrimitive.Value => _value;

    global::System.DateTime global::Primitively.IPrimitive<global::System.DateTime>.Value => _value;

    global::Primitively.PrimitiveInfo global::Primitively.IPrimitiveInfo.Info => _info;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public global::Primitively.DataType DataType => global::Primitively.DataType.PRIMITIVE_DATA_TYPE;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public bool HasValue => _value != default;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public global::PRIMITIVE_INFO_TYPE Info => _info;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public global::System.Type ValueType => typeof(global::System.DateTime);

    public bool Equals(PRIMITIVE_TYPE other) => _value == other._value;
    public int CompareTo(PRIMITIVE_TYPE other) => _value.CompareTo(other._value);
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => _value.ToString(Format);

    public static implicit operator string(PRIMITIVE_TYPE value) => value.ToString();
    public static implicit operator global::System.DateTime(PRIMITIVE_TYPE value) => value._value;
    public static explicit operator PRIMITIVE_TYPE(global::System.DateTime value) => new(value);
    public static explicit operator PRIMITIVE_TYPE(string value) => new(value);

    public static global::PRIMITIVE_INFO_TYPE TypeInfo => _info;

    public static PRIMITIVE_TYPE Parse(string value) => new(value);
    public static bool TryParse(string value, out PRIMITIVE_TYPE result) => (result = new(value)).HasValue;
#endif
