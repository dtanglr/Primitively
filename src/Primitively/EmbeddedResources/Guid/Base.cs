readonly record struct ENCAPSULATED_PRIMITIVE_TYPE : INTERFACES
{
    public ENCAPSULATED_PRIMITIVE_TYPE(System.Guid value)
    {
        Value = value;
    }

    private ENCAPSULATED_PRIMITIVE_TYPE(string value)
    {
        if (NhsGuid.IsMatch(value) && Guid.TryParse(value, out var guid))
        {
            Value = guid;
        }
    }

    public bool HasValue => Value != default;

    public System.Guid Value { get; } = default;

    public bool Equals(ENCAPSULATED_PRIMITIVE_TYPE other) => Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value.ToString("D");

    public static implicit operator string(ENCAPSULATED_PRIMITIVE_TYPE value) => value.ToString();
    public static implicit operator Guid(ENCAPSULATED_PRIMITIVE_TYPE value) => value.Value;
    public static explicit operator ENCAPSULATED_PRIMITIVE_TYPE(Guid value) => new(value);
    public static explicit operator ENCAPSULATED_PRIMITIVE_TYPE(string value) => new(value);

    public static ENCAPSULATED_PRIMITIVE_TYPE New() => new ENCAPSULATED_PRIMITIVE_TYPE(System.Guid.NewGuid());
    public static readonly ENCAPSULATED_PRIMITIVE_TYPE Empty = new ENCAPSULATED_PRIMITIVE_TYPE(System.Guid.Empty);

    public static ENCAPSULATED_PRIMITIVE_TYPE Parse(string value) => new(value);

