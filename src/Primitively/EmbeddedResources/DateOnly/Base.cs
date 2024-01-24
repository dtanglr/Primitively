readonly partial record struct PRIMITIVE_TYPE : Primitively.IDateOnly, System.IEquatable<PRIMITIVE_TYPE>, System.IComparable<PRIMITIVE_TYPE>PRIMITIVE_IVALIDATABLEOBJECT
{
#if NET6_0_OR_GREATER
    private readonly System.DateTime _value;

    public const string Example = @"PRIMITIVE_EXAMPLE";
    public const string Format = @"PRIMITIVE_FORMAT";
    public const int Length = PRIMITIVE_LENGTH;

    public PRIMITIVE_TYPE(System.DateOnly value)
    {
        _value = new System.DateTime(value.Year, value.Month, value.Day);
    }

    public PRIMITIVE_TYPE(System.DateTime value)
    {
        _value = new System.DateTime(value.Year, value.Month, value.Day);
    }

    private PRIMITIVE_TYPE(string value)
    {
        System.DateTime.TryParseExact(value, Format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var result);

        _value = result;
    }

    object Primitively.IPrimitive.Value => _value;

    System.DateTime Primitively.IPrimitive<System.DateTime>.Value => _value;

    [System.Text.Json.Serialization.JsonIgnore]
    public bool HasValue => _value != default;

    [System.Text.Json.Serialization.JsonIgnore]
    public System.Type ValueType => typeof(System.DateTime);

    [System.Text.Json.Serialization.JsonIgnore]
    public Primitively.DataType DataType => Primitively.DataType.PRIMITIVE_DATA_TYPE;

    public bool Equals(PRIMITIVE_TYPE other) => _value == other._value;
    public int CompareTo(PRIMITIVE_TYPE other) => _value.CompareTo(other._value);
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => _value.ToString(Format);

    public static implicit operator string(PRIMITIVE_TYPE value) => value.ToString();
    public static implicit operator System.DateOnly(PRIMITIVE_TYPE value) => DateOnly.FromDateTime(value._value);
    public static explicit operator PRIMITIVE_TYPE(System.DateOnly value) => new(value);
    public static explicit operator System.DateTime(PRIMITIVE_TYPE value) => value._value;
    public static explicit operator PRIMITIVE_TYPE(System.DateTime value) => new(value);
    public static explicit operator PRIMITIVE_TYPE(string value) => new(value);

    public static PRIMITIVE_TYPE Parse(string value) => new(value);
    public static bool TryParse(string value, out PRIMITIVE_TYPE result) => (result = new(value)).HasValue;
#else
    private readonly System.DateTime _value;

    public const string Example = @"PRIMITIVE_EXAMPLE";
    public const string Format = @"PRIMITIVE_FORMAT";
    public const int Length = PRIMITIVE_LENGTH;

    public PRIMITIVE_TYPE(System.DateTime value)
    {
        _value = new System.DateTime(value.Year, value.Month, value.Day);
    }

    private PRIMITIVE_TYPE(string value)
    {
        System.DateTime.TryParseExact(value, Format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var result);

        _value = result;
    }

    object Primitively.IPrimitive.Value => _value;

    System.DateTime Primitively.IPrimitive<System.DateTime>.Value => _value;

    [System.Text.Json.Serialization.JsonIgnore]
    public bool HasValue => _value != default;

    [System.Text.Json.Serialization.JsonIgnore]
    public System.Type ValueType => typeof(System.DateTime);

    [System.Text.Json.Serialization.JsonIgnore]
    public Primitively.DataType DataType => Primitively.DataType.PRIMITIVE_DATA_TYPE;

    public bool Equals(PRIMITIVE_TYPE other) => _value == other._value;
    public int CompareTo(PRIMITIVE_TYPE other) => _value.CompareTo(other._value);
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => _value.ToString(Format);

    public static implicit operator string(PRIMITIVE_TYPE value) => value.ToString();
    public static implicit operator System.DateTime(PRIMITIVE_TYPE value) => value._value;
    public static explicit operator PRIMITIVE_TYPE(System.DateTime value) => new(value);
    public static explicit operator PRIMITIVE_TYPE(string value) => new(value);

    public static PRIMITIVE_TYPE Parse(string value) => new(value);
    public static bool TryParse(string value, out PRIMITIVE_TYPE result) => (result = new(value)).HasValue;
#endif
