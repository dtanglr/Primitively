readonly partial record struct PRIMITIVE_TYPE :
    global::Primitively.IString,
    global::Primitively.IPrimitiveInfo<global::PRIMITIVE_INFO_TYPE>,
    global::System.IEquatable<PRIMITIVE_TYPE>,
    global::System.IComparable<PRIMITIVE_TYPE>PRIMITIVE_IVALIDATABLEOBJECT
{
    public const int MaxLength = PRIMITIVE_MAXLENGTH;
    public const int MinLength = PRIMITIVE_MINLENGTH;
    public const string Example = @"PRIMITIVE_EXAMPLE";
    public const string Format = @"PRIMITIVE_FORMAT";
    public const string Pattern = @"PRIMITIVE_PATTERN";

    private static readonly global::PRIMITIVE_INFO_TYPE _info = new
    (
        Type: typeof(PRIMITIVE_TYPE),
        Example: Example,
        CreateFrom: (value) => (PRIMITIVE_TYPE)value,
        Format: Format,
        Pattern: Pattern,
        MinLength: MinLength,
        MaxLength: MaxLength
    );

    private readonly string _value;

    public PRIMITIVE_TYPE(string value)
    {
        PreMatchCheck(ref value);

        if (!IsMatch(value))
        {
            _value = default;

            return;
        }

        PostMatchCheck(ref value);

        _value = value;
    }

    object global::Primitively.IPrimitive.Value => _value;

    string global::Primitively.IPrimitive<string>.Value => _value;

    global::Primitively.PrimitiveInfo global::Primitively.IPrimitiveInfo.Info => _info;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public global::Primitively.DataType DataType => global::Primitively.DataType.PRIMITIVE_DATA_TYPE;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public bool HasValue => _value != default;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public global::PRIMITIVE_INFO_TYPE Info => _info;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public global::System.Type ValueType => typeof(string);

    public bool Equals(PRIMITIVE_TYPE other) => _value == other._value;
    public int CompareTo(PRIMITIVE_TYPE other) => global::System.String.Compare(_value, other._value, comparisonType: global::System.StringComparison.OrdinalIgnoreCase);
    public override int GetHashCode() => _value?.GetHashCode() ?? 0;
    public override string ToString() => _value;

    public static implicit operator string(PRIMITIVE_TYPE value) => value.ToString();
    public static explicit operator PRIMITIVE_TYPE(string value) => new(value);

    public static global::PRIMITIVE_INFO_TYPE TypeInfo => _info;

    public static PRIMITIVE_TYPE Parse(string value) => new(value);
    public static bool TryParse(string value, out PRIMITIVE_TYPE result) => (result = new(value)).HasValue;

    static bool IsMatch(string value) =>
        !string.IsNullOrWhiteSpace(value) &&
        !(value.Length < MinLength) &&
        !(value.Length > MaxLength) &&
        (Pattern.Length == 0 || global::System.Text.RegularExpressions.Regex.IsMatch(value, Pattern, global::System.Text.RegularExpressions.RegexOptions.IgnoreCase, global::System.TimeSpan.FromSeconds(1)));
