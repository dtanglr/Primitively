using System;

namespace Primitively.IntegrationTests.Types.DateOnlyTests.BirthDateTests;

public class JsonConverterTests : JsonConverterTests<BirthDateJsonConverter, BirthDate, DateOnly>
{
    protected override BirthDate PrimitiveWithValue => BirthDate.Parse("2022-01-01");
}
