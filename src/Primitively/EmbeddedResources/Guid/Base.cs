readonly partial record struct PRIMITIVE_TYPE : Primitively.IPrimitive<System.Guid>, System.IEquatable<PRIMITIVE_TYPE>
{
    public const string Example = @"PRIMITIVE_EXAMPLE";
    public const string Format = @"PRIMITIVE_FORMAT"; // "N", "D", "B", "P", or "X"
    public const int MinLength = PRIMITIVE_MINLENGTH;
    public const int MaxLength = PRIMITIVE_MAXLENGTH;

    public PRIMITIVE_TYPE(System.Guid value)
    {
        Value = value;
    }

    private PRIMITIVE_TYPE(string value)
    {
        if (!System.Guid.TryParseExact(value, Format, out var guid)) return;

        Value = guid;
    }

    public bool HasValue => Value != default;

    public System.Guid Value { get; } = default;

    public bool Equals(PRIMITIVE_TYPE other) => Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value.ToString(Format);

    public static implicit operator string(PRIMITIVE_TYPE value) => value.ToString();
    public static implicit operator System.Guid(PRIMITIVE_TYPE value) => value.Value;
    public static explicit operator PRIMITIVE_TYPE(System.Guid value) => new(value);
    public static explicit operator PRIMITIVE_TYPE(string value) => new(value);

    public static PRIMITIVE_TYPE New() => new PRIMITIVE_TYPE(System.Guid.NewGuid());
    public static readonly PRIMITIVE_TYPE Empty = new PRIMITIVE_TYPE(System.Guid.Empty);

    public static PRIMITIVE_TYPE Parse(string value) => new(value);
    public static bool TryParse(string value, out PRIMITIVE_TYPE result) => (result = new(value)).HasValue;
