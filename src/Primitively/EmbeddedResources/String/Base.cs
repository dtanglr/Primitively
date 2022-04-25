readonly partial record struct PRIMITIVE_TYPE : Primitively.IPrimitive<string>, System.IEquatable<PRIMITIVE_TYPE>
{
    public const string Pattern = @"PRIMITIVE_PATTERN";
    public const string Example = @"PRIMITIVE_EXAMPLE";
    public const string Format = @"PRIMITIVE_FORMAT";
    public const int MinLength = PRIMITIVE_MINLENGTH;
    public const int MaxLength = PRIMITIVE_MAXLENGTH;

    private PRIMITIVE_TYPE(string value)
    {
        PreMatchCheck(ref value);

        if (!IsMatch(value)) return;

        PostMatchCheck(ref value);

        Value = value;
    }

    public bool HasValue => Value != default;

    public string Value { get; } = default;

    public bool Equals(PRIMITIVE_TYPE other) => Value == other.Value;

    public override int GetHashCode() => Value?.GetHashCode() ?? 0;

    public override string ToString() => Value;

    public static implicit operator string(PRIMITIVE_TYPE value) => value.ToString();
    public static explicit operator PRIMITIVE_TYPE(string value) => new(value);

    public static PRIMITIVE_TYPE Parse(string value) => new(value);
    public static bool TryParse(string value, out PRIMITIVE_TYPE result) => (result = new(value)).HasValue;

    static bool IsMatch(string value) =>
        !string.IsNullOrWhiteSpace(value) &&
        !(value.Length < MinLength) &&
        !(value.Length > MaxLength) &&
        (Pattern.Length == 0 || System.Text.RegularExpressions.Regex.IsMatch(value, Pattern, System.Text.RegularExpressions.RegexOptions.None, System.TimeSpan.FromSeconds(1)));
