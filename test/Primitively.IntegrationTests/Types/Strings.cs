namespace Primitively.IntegrationTests.Types;

[StringPrimitive("^[0-9]{10}$", "2323435649", length: 10)]
public partial record struct NhsNumber;

[StringPrimitive("^[0-9]{7}$", "37263546", length: 7)]
public partial record struct GphcNumber;

[StringPrimitive("^[A-Za-z0-9]{3,10}$", "Y12345", minLength: 3, maxLength: 10)]
public partial record struct NominatedPharmacyCode;
