namespace Primitively;

/// <summary>
/// The enum representation of each <see cref="Guid"/> type format variation supported by Primitively
/// </summary>
public enum Specifier
{
    /// <summary>
    /// 32 characters
    /// e.g. 2c48c1527cb74f518f01704454f36e60
    /// </summary>
    N,
    /// <summary>
    /// 36 characters separated by hyphens (DEFAULT)
    /// e.g. 2c48c152-7cb7-4f51-8f01-704454f36e60
    /// </summary>
    D,
    /// <summary>
    /// 38 characters separated by hyphens, enclosed in braces
    /// e.g. {2c48c152-7cb7-4f51-8f01-704454f36e60}
    /// </summary>
    B,
    /// <summary>
    /// 38 characters separated by hyphens, enclosed in parentheses
    /// e.g. (2c48c152-7cb7-4f51-8f01-704454f36e60)
    /// </summary>
    P,
    /// <summary>
    /// 68 characters comprised of four hexadecimal values enclosed in braces, where the fourth value is a subset of eight hexadecimal values that is also enclosed in braces
    /// e.g. {0x2c48c152,0x7cb7,0x4f51,{0x8f,0x01,0x70,0x44,0x54,0xf3,0x6e,0x60}}
    /// </summary>
    X
}
