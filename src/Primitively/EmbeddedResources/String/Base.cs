﻿readonly partial record struct PRIMITIVE_TYPE : Primitively.IString, global::System.IEquatable<PRIMITIVE_TYPE>, global::System.IComparable<PRIMITIVE_TYPE>PRIMITIVE_IVALIDATABLEOBJECT
{
    private readonly string _value;

    public const string Pattern = @"PRIMITIVE_PATTERN";
    public const string Example = @"PRIMITIVE_EXAMPLE";
    public const string Format = @"PRIMITIVE_FORMAT";
    public const int MinLength = PRIMITIVE_MINLENGTH;
    public const int MaxLength = PRIMITIVE_MAXLENGTH;

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

    object Primitively.IPrimitive.Value => _value;

    string Primitively.IPrimitive<string>.Value => _value;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public bool HasValue => _value != default;

    [global::System.Text.Json.Serialization.JsonIgnore]
    public global::System.Type ValueType => typeof(string);

    [global::System.Text.Json.Serialization.JsonIgnore]
    public Primitively.DataType DataType => Primitively.DataType.PRIMITIVE_DATA_TYPE;

    public bool Equals(PRIMITIVE_TYPE other) => _value == other._value;
    public int CompareTo(PRIMITIVE_TYPE other) => global::System.String.Compare(_value, other._value, comparisonType: global::System.StringComparison.OrdinalIgnoreCase);
    public override int GetHashCode() => _value?.GetHashCode() ?? 0;
    public override string ToString() => _value;

    public static implicit operator string(PRIMITIVE_TYPE value) => value.ToString();
    public static explicit operator PRIMITIVE_TYPE(string value) => new(value);

    public static PRIMITIVE_TYPE Parse(string value) => new(value);
    public static bool TryParse(string value, out PRIMITIVE_TYPE result) => (result = new(value)).HasValue;

    static bool IsMatch(string value) =>
        !string.IsNullOrWhiteSpace(value) &&
        !(value.Length < MinLength) &&
        !(value.Length > MaxLength) &&
        (Pattern.Length == 0 || global::System.Text.RegularExpressions.Regex.IsMatch(value, Pattern, global::System.Text.RegularExpressions.RegexOptions.IgnoreCase, global::System.TimeSpan.FromSeconds(1)));
