﻿namespace Primitively.IntegrationTests.GuidTests.B;

public class JsonConverterTests : PrimitiveJsonConverterTests<ThirtyEightDigitsWithHyphensAndBraces.JsonConverter, ThirtyEightDigitsWithHyphensAndBraces>
{
    protected override ThirtyEightDigitsWithHyphensAndBraces PrimitiveWithValue => (ThirtyEightDigitsWithHyphensAndBraces)ThirtyEightDigitsWithHyphensAndBraces.Example;
}
