namespace Primitively.Examples;

[String(7, Pattern = "^[0-9]{7}$", Example = "1234567")]
public partial record struct SevenDigits;

[String(8, Pattern = "^[0-9]{8}$", Example = "12345678")]
public partial record struct EightDigits;

[String(2, 8, Pattern = "^[0-9A-Za-z]{2,8}$", Example = "Qs")]
public partial record struct TwoAndEightAnyCharacter;

[String(10, ImplementIValidatableObject = true, Example = "0123456789")]
public partial record struct ValidatableString;

[String(4, 8, Example = "N20 1LP", Pattern = "^[A-Z]{1,2}[0-9][A-Z0-9]? ?[0-9][A-Z]{2}$")]
public partial record struct Postcode;
