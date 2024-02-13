readonly partial record struct PRIMITIVE_TYPE : Primitively.IGuid, global::System.IEquatable<PRIMITIVE_TYPE>, global::System.IComparable<PRIMITIVE_TYPE>PRIMITIVE_IVALIDATABLEOBJECT
{
    private readonly global::System.Guid _value;

    public const string Example = @"PRIMITIVE_EXAMPLE";
    public const string Format = @"PRIMITIVE_FORMAT"; // "N", "D", "B", "P", or "X"
    public const int Length = PRIMITIVE_LENGTH;

    public PRIMITIVE_TYPE(global::System.Guid value)
    {
        _value = value;
    }

    private PRIMITIVE_TYPE(string value)
    {
        global::System.Guid.TryParseExact(value, Format, out var guid);

        _value = guid;
    }

    object Primitively.IPrimitive.Value => _value;

    global::System.Guid Primitively.IPrimitive<global::System.Guid>.Value => _value;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public bool HasValue => _value != default;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public global::System.Type ValueType => typeof(global::System.Guid);

    [global::System.Text.Json.Serialization.JsonIgnore]
    public Primitively.DataType DataType => Primitively.DataType.PRIMITIVE_DATA_TYPE;

    public bool Equals(PRIMITIVE_TYPE other) => _value == other._value;
    public int CompareTo(PRIMITIVE_TYPE other) => _value.CompareTo(other._value);
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => _value.ToString(Format);

    public static implicit operator string(PRIMITIVE_TYPE value) => value.ToString();
    public static implicit operator global::System.Guid(PRIMITIVE_TYPE value) => value._value;
    public static explicit operator PRIMITIVE_TYPE(global::System.Guid value) => new(value);
    public static explicit operator PRIMITIVE_TYPE(string value) => new(value);

    public static PRIMITIVE_TYPE New() => new PRIMITIVE_TYPE(global::System.Guid.NewGuid());
    public static readonly PRIMITIVE_TYPE Empty = new PRIMITIVE_TYPE(global::System.Guid.Empty);

    public static PRIMITIVE_TYPE Parse(string value) => new(value);
    public static bool TryParse(string value, out PRIMITIVE_TYPE result) => (result = new(value)).HasValue;
