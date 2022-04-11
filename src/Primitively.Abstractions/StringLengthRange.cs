namespace Primitively;

public class StringLengthRange : IStringLength
{
    public StringLengthRange(int minLength, int maxLength)
    {
        MinLength = minLength;
        MaxLength = maxLength;
    }

    public int MinLength { get; }
    public int MaxLength { get; }

    public static implicit operator StringLengthRange((int MinLength, int MaxLength) value) => new(value.MinLength, value.MaxLength);
    public static implicit operator (int MinLength, int MaxLength)(StringLengthRange value) => new(value.MinLength, value.MaxLength);
}
