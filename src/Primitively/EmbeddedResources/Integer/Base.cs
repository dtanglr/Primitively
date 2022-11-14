readonly partial record struct PRIMITIVE_TYPE : PRIMITIVE_INTERFACE, System.IEquatable<PRIMITIVE_TYPE>, System.IComparable<PRIMITIVE_TYPE>PRIMITIVE_IVALIDATABLEOBJECT
{
    private readonly PRIMITIVE_VALUE_TYPE _value = default;

    public const string Example = @"PRIMITIVE_EXAMPLE";
    public const PRIMITIVE_VALUE_TYPE Minimum = PRIMITIVE_MINIMUM;
    public const PRIMITIVE_VALUE_TYPE Maximum = PRIMITIVE_MAXIMUM;

    public PRIMITIVE_TYPE(PRIMITIVE_VALUE_TYPE value)
    {
        if (value >= PRIMITIVE_MINIMUM && value <= PRIMITIVE_MAXIMUM)
        {
            _value = value;
        }
    }

    private PRIMITIVE_TYPE(string value)
    {
        if (PRIMITIVE_VALUE_TYPE.TryParse(value, out var result) && result >= PRIMITIVE_MINIMUM && result <= PRIMITIVE_MAXIMUM)
        {
            _value = result;
        }
    }

    [System.Text.Json.Serialization.JsonIgnore]
    public bool HasValue => _value != default;

    [System.Text.Json.Serialization.JsonIgnore]
    public System.Type ValueType => typeof(PRIMITIVE_VALUE_TYPE);

    public bool Equals(PRIMITIVE_TYPE other) => _value == other._value;
    public int CompareTo(PRIMITIVE_TYPE other) => _value.CompareTo(other._value);
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => _value.ToString();

    public static implicit operator string(PRIMITIVE_TYPE value) => value.ToString();
    public static implicit operator PRIMITIVE_VALUE_TYPE(PRIMITIVE_TYPE value) => value._value;
    public static explicit operator PRIMITIVE_TYPE(PRIMITIVE_VALUE_TYPE value) => new(value);
    public static explicit operator PRIMITIVE_TYPE(string value) => new(value);

    public static PRIMITIVE_TYPE Parse(string value) => new(value);
    public static bool TryParse(string value, out PRIMITIVE_TYPE result) => (result = new(value)).HasValue;
