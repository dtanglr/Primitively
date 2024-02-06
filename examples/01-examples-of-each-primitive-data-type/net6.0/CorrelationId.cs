using Primitively;

namespace Acme.Examples;

[Guid]
public partial record struct CorrelationId
{
    public const string HttpHeaderKey = "X-Correlation-ID";
}
