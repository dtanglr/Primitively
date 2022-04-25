namespace Primitively.IntegrationTests.Types.StringTests.GphcNumberTests;

public class JsonConverterTests : JsonConverterTests<GphcNumberJsonConverter, GphcNumber, string>
{
    protected override GphcNumber PrimitiveWithValue => GphcNumber.Parse("1234567");
}
