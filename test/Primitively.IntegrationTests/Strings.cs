using System;

namespace Primitively.IntegrationTests;

[String(7, Pattern = "^[0-9]{7}$", Example = "1234567")]
public partial record struct SevenDigits;

[String(8, Pattern = "^[0-9]{8}$", Example = "12345678")]
public partial record struct EightDigits;

[String(2, 8, Pattern = "^[0-9A-Za-z]{2,8}$", Example = "Qs")]
public partial record struct TwoAndEightAnyCharacter;
