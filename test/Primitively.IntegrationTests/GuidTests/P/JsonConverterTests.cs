﻿namespace Primitively.IntegrationTests.GuidTests.P;

public class JsonConverterTests : PrimitiveJsonConverterTests<ThirtyEightDigitsWithHyphensAndParentheses.JsonConverter, ThirtyEightDigitsWithHyphensAndParentheses>
{
    protected override ThirtyEightDigitsWithHyphensAndParentheses PrimitiveWithValue => (ThirtyEightDigitsWithHyphensAndParentheses)ThirtyEightDigitsWithHyphensAndParentheses.Example;
}
