using System;

namespace Primitively.IntegrationTests.Types.DateTests.BirthDateTests;

public class JsonConverterTests : JsonConverterTests<BirthDate.BirthDateJsonConverter, BirthDate, DateOnly>
{
    protected override BirthDate PrimitiveWithValue => BirthDate.Parse("2022-01-01");
}
