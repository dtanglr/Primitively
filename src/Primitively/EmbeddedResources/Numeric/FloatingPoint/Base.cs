readonly partial record struct PRIMITIVE_TYPE : global::PRIMITIVE_INTERFACE, global::System.IEquatable<PRIMITIVE_TYPE>, global::System.IComparable<PRIMITIVE_TYPE>PRIMITIVE_IVALIDATABLEOBJECT
{
    private readonly global::PRIMITIVE_VALUE_TYPE _value = default;

    public const string Example = "PRIMITIVE_EXAMPLE";
    public const global::PRIMITIVE_VALUE_TYPE Minimum = PRIMITIVE_MINIMUM;
    public const global::PRIMITIVE_VALUE_TYPE Maximum = PRIMITIVE_MAXIMUM;
    public const int Digits = PRIMITIVE_ROUNDINGDIGITS;
    public const global::System.MidpointRounding Mode = global::System.MidpointRounding.PRIMITIVE_MIDPOINTROUNDINGMODE;

    public PRIMITIVE_TYPE()
    {
        HasValue = IsMatch(_value);
    }

    public PRIMITIVE_TYPE(global::PRIMITIVE_VALUE_TYPE value)
    {
        PreMatchCheck(ref value);
        HasValue = IsMatch(value);
        _value = HasValue ? value : default;
    }

    private PRIMITIVE_TYPE(string value)
    {
        if (global::PRIMITIVE_VALUE_TYPE.TryParse(value, out var result))
        {
            PreMatchCheck(ref result);
            HasValue = IsMatch(result);
            _value = HasValue ? result : default;
        }
    }

    object global::Primitively.IPrimitive.Value => _value;

    global::PRIMITIVE_VALUE_TYPE global::Primitively.IPrimitive<global::PRIMITIVE_VALUE_TYPE>.Value => _value;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public bool HasValue { get; } = false;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public global::System.Type ValueType => typeof(global::PRIMITIVE_VALUE_TYPE);

    [global::System.Text.Json.Serialization.JsonIgnore]
    public global::Primitively.DataType DataType => global::Primitively.DataType.PRIMITIVE_DATA_TYPE;

    public bool Equals(PRIMITIVE_TYPE other) => _value == other._value;
    public int CompareTo(PRIMITIVE_TYPE other) => _value.CompareTo(other._value);
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => _value.ToString();

    public static implicit operator string(PRIMITIVE_TYPE value) => value.ToString();
    public static implicit operator global::PRIMITIVE_VALUE_TYPE(PRIMITIVE_TYPE value) => value._value;
    public static explicit operator PRIMITIVE_TYPE(global::PRIMITIVE_VALUE_TYPE value) => new(value);
    public static explicit operator PRIMITIVE_TYPE(string value) => new(value);

    public static PRIMITIVE_TYPE Parse(string value) => new(value);
    public static bool TryParse(string value, out PRIMITIVE_TYPE result) => (result = new(value)).HasValue;

    private static bool IsMatch(global::PRIMITIVE_VALUE_TYPE value) => value >= Minimum && value <= Maximum;
