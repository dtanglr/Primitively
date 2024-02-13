readonly partial record struct PRIMITIVE_TYPE : Primitively.IDateOnly, global::System.IEquatable<PRIMITIVE_TYPE>, global::System.IComparable<PRIMITIVE_TYPE>PRIMITIVE_IVALIDATABLEOBJECT
{
#if NET6_0_OR_GREATER
    private readonly global::System.DateTime _value;

    public const string Example = @"PRIMITIVE_EXAMPLE";
    public const string Format = @"PRIMITIVE_FORMAT";
    public const int Length = PRIMITIVE_LENGTH;

    public PRIMITIVE_TYPE(global::System.DateOnly value)
    {
        _value = new global::System.DateTime(value.Year, value.Month, value.Day);
    }

    public PRIMITIVE_TYPE(global::System.DateTime value)
    {
        _value = new global::System.DateTime(value.Year, value.Month, value.Day);
    }

    private PRIMITIVE_TYPE(string value)
    {
        global::System.DateTime.TryParseExact(value, Format, global::System.Globalization.CultureInfo.InvariantCulture, global::System.Globalization.DateTimeStyles.None, out var result);

        _value = result;
    }

    object Primitively.IPrimitive.Value => _value;

    global::System.DateTime Primitively.IPrimitive<global::System.DateTime>.Value => _value;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public bool HasValue => _value != default;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public global::System.Type ValueType => typeof(global::System.DateTime);

    [global::System.Text.Json.Serialization.JsonIgnore]
    public Primitively.DataType DataType => Primitively.DataType.PRIMITIVE_DATA_TYPE;

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

    public static PRIMITIVE_TYPE Parse(string value) => new(value);
    public static bool TryParse(string value, out PRIMITIVE_TYPE result) => (result = new(value)).HasValue;
#else
    private readonly global::System.DateTime _value;

    public const string Example = @"PRIMITIVE_EXAMPLE";
    public const string Format = @"PRIMITIVE_FORMAT";
    public const int Length = PRIMITIVE_LENGTH;

    public PRIMITIVE_TYPE(global::System.DateTime value)
    {
        _value = new global::System.DateTime(value.Year, value.Month, value.Day);
    }

    private PRIMITIVE_TYPE(string value)
    {
        global::System.DateTime.TryParseExact(value, Format, global::System.Globalization.CultureInfo.InvariantCulture, global::System.Globalization.DateTimeStyles.None, out var result);

        _value = result;
    }

    object Primitively.IPrimitive.Value => _value;

    global::System.DateTime Primitively.IPrimitive<global::System.DateTime>.Value => _value;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public bool HasValue => _value != default;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public global::System.Type ValueType => typeof(global::System.DateTime);

    [global::System.Text.Json.Serialization.JsonIgnore]
    public Primitively.DataType DataType => Primitively.DataType.PRIMITIVE_DATA_TYPE;

    public bool Equals(PRIMITIVE_TYPE other) => _value == other._value;
    public int CompareTo(PRIMITIVE_TYPE other) => _value.CompareTo(other._value);
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => _value.ToString(Format);

    public static implicit operator string(PRIMITIVE_TYPE value) => value.ToString();
    public static implicit operator global::System.DateTime(PRIMITIVE_TYPE value) => value._value;
    public static explicit operator PRIMITIVE_TYPE(global::System.DateTime value) => new(value);
    public static explicit operator PRIMITIVE_TYPE(string value) => new(value);

    public static PRIMITIVE_TYPE Parse(string value) => new(value);
    public static bool TryParse(string value, out PRIMITIVE_TYPE result) => (result = new(value)).HasValue;
#endif
