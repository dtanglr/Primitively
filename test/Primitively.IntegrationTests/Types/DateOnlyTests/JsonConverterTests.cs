﻿namespace Primitively.IntegrationTests.Types.DateOnlyTests;

public class JsonConverterTests : PrimitiveJsonConverterTests<BirthDate.JsonConverter, BirthDate>
{
    protected override BirthDate PrimitiveWithValue => (BirthDate)BirthDate.Example;
}
