namespace Primitively.IntegrationTests.Types;

[String(7, Pattern = "^[0-9]{7}$", Example = "1234567")]
public partial record struct SevenDigits;

[String(8, Pattern = "^[0-9]{8}$", Example = "12345678")]
public partial record struct EightDigits;

[String(9, Pattern = "^[0-9]{9}$", Example = "123456789")]
public partial record struct NineDigits;
