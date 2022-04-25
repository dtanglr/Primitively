readonly partial record struct PRIMITIVE_TYPE : Primitively.IPrimitive<System.DateOnly>, System.IEquatable<PRIMITIVE_TYPE>
{
    public const string Example = @"PRIMITIVE_EXAMPLE";
    public const string Format = @"PRIMITIVE_FORMAT";
    public const int MinLength = PRIMITIVE_MINLENGTH;
    public const int MaxLength = PRIMITIVE_MAXLENGTH;

    public PRIMITIVE_TYPE(System.DateOnly value)
    {
        Value = value;
    }

    private PRIMITIVE_TYPE(string value)
    {
        if (!System.DateOnly.TryParseExact(value, Format, out var result)) return;

        Value = result;
    }

    public bool HasValue => Value != default;

    public System.DateOnly Value { get; } = default;

    public bool Equals(PRIMITIVE_TYPE other) => Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value.ToString(Format);

    public static implicit operator string(PRIMITIVE_TYPE value) => value.ToString();
    public static implicit operator System.DateOnly(PRIMITIVE_TYPE value) => value.Value;
    public static explicit operator PRIMITIVE_TYPE(System.DateOnly value) => new(value);
    public static explicit operator PRIMITIVE_TYPE(string value) => new(value);

    public static PRIMITIVE_TYPE Parse(string value) => new(value);
    public static bool TryParse(string value, out PRIMITIVE_TYPE result) => (result = new(value)).HasValue;
