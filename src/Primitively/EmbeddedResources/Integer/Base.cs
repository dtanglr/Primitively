readonly partial record struct PRIMITIVE_TYPE : PRIMITIVE_INTERFACE, global::System.IEquatable<PRIMITIVE_TYPE>, global::System.IComparable<PRIMITIVE_TYPE>PRIMITIVE_IVALIDATABLEOBJECT
{
    private readonly global::PRIMITIVE_VALUE_TYPE _value = default;

    public const string Example = @"PRIMITIVE_EXAMPLE";
    public const global::PRIMITIVE_VALUE_TYPE Minimum = PRIMITIVE_MINIMUM;
    public const global::PRIMITIVE_VALUE_TYPE Maximum = PRIMITIVE_MAXIMUM;

    public PRIMITIVE_TYPE(global::PRIMITIVE_VALUE_TYPE value)
    {
        if (value >= PRIMITIVE_MINIMUM && value <= PRIMITIVE_MAXIMUM)
        {
            _value = value;
        }
    }

    private PRIMITIVE_TYPE(string value)
    {
        if (global::PRIMITIVE_VALUE_TYPE.TryParse(value, out var result) && result >= PRIMITIVE_MINIMUM && result <= PRIMITIVE_MAXIMUM)
        {
            _value = result;
        }
    }

    object Primitively.IPrimitive.Value => _value;

    global::PRIMITIVE_VALUE_TYPE Primitively.IPrimitive<global::PRIMITIVE_VALUE_TYPE>.Value => _value;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public bool HasValue => _value != default;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public global::System.Type ValueType => typeof(global::PRIMITIVE_VALUE_TYPE);

    [global::System.Text.Json.Serialization.JsonIgnore]
    public Primitively.DataType DataType => Primitively.DataType.PRIMITIVE_DATA_TYPE;

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
