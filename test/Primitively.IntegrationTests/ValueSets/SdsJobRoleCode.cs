namespace Primitively.IntegrationTests.ValueSets;

[StringPrimitive(5, Pattern = @"^R\d{4}$", Example = "R8000")]
public readonly partial record struct SdsJobRoleCode;
