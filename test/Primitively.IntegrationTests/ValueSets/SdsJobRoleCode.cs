namespace Primitively.IntegrationTests.ValueSets;

[StringPrimitive(@"^R\d{4}$", "R8000", length: 5)]
public readonly partial record struct SdsJobRoleCode;
