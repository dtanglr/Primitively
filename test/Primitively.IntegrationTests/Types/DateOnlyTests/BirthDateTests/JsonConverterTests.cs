using System;

namespace Primitively.IntegrationTests.Types.DateOnlyTests.BirthDateTests;

public class JsonConverterTests : JsonConverterTests<BirthDateJsonConverter, BirthDate>
{
    protected override BirthDate PrimitiveWithValue => (BirthDate)BirthDate.Example;
}
