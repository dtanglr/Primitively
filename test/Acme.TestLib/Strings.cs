using Primitively;

namespace Acme.TestLib;

[String(7, Pattern = "^[0-9]{7}$", Example = "1234567")]
public partial record struct SevenDigits;

[String(8, Pattern = "^[0-9]{8}$", Example = "12345678")]
public partial record struct EightDigits;

[String(2, 8, Pattern = "^[0-9A-Za-z]{2,8}$", Example = "Qs")]
public partial record struct TwoAndEightAnyCharacter;

[String(10, ImplementIValidatableObject = true, Example = "0123456789")]
public partial record struct ValidatableString;

[String(minLength: 6, maxLength: 99, Pattern = @"^[a-zA-Z0-9._%+-]{1,64}@[a-zA-Z0-9.-]{1,255}\.[a-zA-Z]{2,}$", Example = "test@example.com", ImplementIValidatableObject = true)]
public readonly partial record struct Email;
