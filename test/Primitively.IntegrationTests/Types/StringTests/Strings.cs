namespace Primitively.IntegrationTests.Types;

[NhsNumberPrimitive]
public partial record struct NhsNumber;

[OdsCodePrimitive]
public partial record struct NominatedPharmacyCode;

[PostcodePrimitive]
public partial record struct Postcode;

[StringPrimitive(7, Pattern = "^[0-9]{7}$", Example = "37263546")]
public partial record struct GphcNumber;
