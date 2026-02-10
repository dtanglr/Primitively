using Microsoft.CodeAnalysis;

namespace Primitively;

internal static partial class Diagnostics
{
    internal static class AdditionalNonStaticPropertiesDiagnostic
    {
        internal const string Id = "PRIMITIVELY002";
        internal const string Message = "Primitively types must not declare additional non-static properties: '{0}'";
        internal const string Title = "Must not have additional non-static properties";

        private static readonly DiagnosticDescriptor _descriptor = new(Id, Title, Message, category: Usage, defaultSeverity: DiagnosticSeverity.Error, isEnabledByDefault: true);

        public static Diagnostic Create(SyntaxNode currentNode, string propertyNames) => Diagnostic.Create(_descriptor, currentNode.GetLocation(), propertyNames);
    }
}
