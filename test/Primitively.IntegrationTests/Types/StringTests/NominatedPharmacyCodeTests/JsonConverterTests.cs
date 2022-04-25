﻿namespace Primitively.IntegrationTests.Types.StringTests.NominatedPharmacyCodeTests;

public class JsonConverterTests : JsonConverterTests<NominatedPharmacyCodeJsonConverter, NominatedPharmacyCode, string>
{
    protected override NominatedPharmacyCode PrimitiveWithValue => NominatedPharmacyCode.Parse("Y12345");
}
