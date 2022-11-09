readonly partial record struct PRIMITIVE_TYPE : Primitively.IDateOnly, System.IEquatable<PRIMITIVE_TYPE>, System.IComparable<PRIMITIVE_TYPE>PRIMITIVE_IVALIDATABLEOBJECT
{
    private readonly System.DateOnly _value;

    public const string Example = @"PRIMITIVE_EXAMPLE";
    public const string Format = @"PRIMITIVE_FORMAT";
    public const int Length = PRIMITIVE_LENGTH;

    public PRIMITIVE_TYPE(System.DateOnly value)
    {
        _value = value;
    }

    private PRIMITIVE_TYPE(string value)
    {
        System.DateOnly.TryParseExact(value, Format, out var result);

        _value = result;
    }

    [System.Text.Json.Serialization.JsonIgnore]
    public bool HasValue => _value != default;

    [System.Text.Json.Serialization.JsonIgnore]
    public System.Type ValueType => typeof(System.DateOnly);

    public bool Equals(PRIMITIVE_TYPE other) => _value == other._value;
    public int CompareTo(PRIMITIVE_TYPE other) => _value.CompareTo(other._value);
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => _value.ToString(Format);

    public static implicit operator string(PRIMITIVE_TYPE value) => value.ToString();
    public static implicit operator System.DateOnly(PRIMITIVE_TYPE value) => value._value;
    public static explicit operator PRIMITIVE_TYPE(System.DateOnly value) => new(value);
    public static explicit operator PRIMITIVE_TYPE(string value) => new(value);

    public static PRIMITIVE_TYPE Parse(string value) => new(value);
    public static bool TryParse(string value, out PRIMITIVE_TYPE result) => (result = new(value)).HasValue;
