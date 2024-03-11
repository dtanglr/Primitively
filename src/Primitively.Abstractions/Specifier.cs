namespace Primitively;

/// <summary>
/// This enum represents each .NET <see cref="Guid"/> format variation.
/// </summary>
public enum Specifier
{
    /// <summary>
    /// Represents a GUID format that consists of 38 characters separated by hyphens, enclosed in braces.
    /// For example: {2c48c152-7cb7-4f51-8f01-704454f36e60}
    /// </summary>
    B,
    /// <summary>
    /// Represents a GUID format that consists of 36 characters separated by hyphens. This is the default format.
    /// For example: 2c48c152-7cb7-4f51-8f01-704454f36e60
    /// </summary>
    D,
    /// <summary>
    /// Represents a GUID format that consists of 32 characters.
    /// For example: 2c48c1527cb74f518f01704454f36e60
    /// </summary>
    N,
    /// <summary>
    /// Represents a GUID format that consists of 38 characters separated by hyphens, enclosed in parentheses.
    /// For example: (2c48c152-7cb7-4f51-8f01-704454f36e60)
    /// </summary>
    P,
    /// <summary>
    /// Represents a GUID format that consists of 68 characters comprised of four hexadecimal values enclosed in braces, 
    /// where the fourth value is a subset of eight hexadecimal values that is also enclosed in braces.
    /// For example: {0x2c48c152,0x7cb7,0x4f51,{0x8f,0x01,0x70,0x44,0x54,0xf3,0x6e,0x60}}
    /// </summary>
    X
}
