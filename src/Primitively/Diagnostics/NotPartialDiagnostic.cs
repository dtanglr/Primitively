using Microsoft.CodeAnalysis;

namespace Primitively;

internal static partial class Diagnostics
{
    internal static class NotPartialDiagnostic
    {
        internal const string Id = "PRIMITIVELY01";
        internal const string Message = "The target of the Primitive attribute must be declared as partial.";
        internal const string Title = "Must be partial";

        private static readonly DiagnosticDescriptor _descriptor = new(Id, Title, Message, category: Usage, defaultSeverity: DiagnosticSeverity.Warning, isEnabledByDefault: true);

        public static Diagnostic Create(SyntaxNode currentNode) => Diagnostic.Create(_descriptor, currentNode.GetLocation());
    }
}
