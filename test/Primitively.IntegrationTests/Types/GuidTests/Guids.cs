namespace Primitively.IntegrationTests.Types;

[Guid]
public partial record struct CorrelationId;

[Guid(Specifier.D)]
public partial record struct RequestId;

[Guid(Specifier.N)]
public partial record struct SiteId;

[Guid(Specifier.B)]
public partial record struct SiteCollectionId;

[Guid(Specifier.N)]
public partial record struct ListId;

[Guid(Specifier.X)]
public partial record struct QueryId;
