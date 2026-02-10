using Microsoft.CodeAnalysis;

namespace Primitively;

internal static partial class Diagnostics
{
    internal static class AdditionalConstructorsDiagnostic
    {
        internal const string Id = "PRIMITIVELY004";
        internal const string Message = "Primitively types must not declare additional constructors";
        internal const string Title = "Must not have additional constructors";

        private static readonly DiagnosticDescriptor _descriptor = new(Id, Title, Message, category: Usage, defaultSeverity: DiagnosticSeverity.Error, isEnabledByDefault: true);

        public static Diagnostic Create(SyntaxNode currentNode) => Diagnostic.Create(_descriptor, currentNode.GetLocation());
    }
}
