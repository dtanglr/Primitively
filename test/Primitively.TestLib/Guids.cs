namespace Primitively.TestLib;

[Guid] // Default: Specifier.D
public partial record struct DefaultThirtySixDigitsWithHyphens;

[Guid(Specifier.B)]
public partial record struct ThirtyEightDigitsWithHyphensAndBraces;

[Guid(Specifier.D)]
public partial record struct ThirtySixDigitsWithHyphens;

[Guid(Specifier.N)]
public partial record struct ThirtyTwoDigits;

[Guid(Specifier.P)]
public partial record struct ThirtyEightDigitsWithHyphensAndParentheses;

[Guid(Specifier.X)]
public partial record struct SixtyEightHexadecimalsWithHyphensAndBraces;

[Guid(ImplementIValidatableObject = true)]
public partial record struct ValidatableGuid;
