using Microsoft.CodeAnalysis;

namespace Primitively;

internal static partial class Diagnostics
{
    internal static class AdditionalNonStaticFieldsDiagnostic
    {
        internal const string Id = "PRIMITIVELY003";
        internal const string Message = "Primitively types must not declare additional non-static fields: '{0}'";
        internal const string Title = "Must not have additional non-static fields";

        private static readonly DiagnosticDescriptor _descriptor = new(Id, Title, Message, category: Usage, defaultSeverity: DiagnosticSeverity.Error, isEnabledByDefault: true);

        public static Diagnostic Create(SyntaxNode currentNode, string fieldNames) => Diagnostic.Create(_descriptor, currentNode.GetLocation(), fieldNames);
    }
}
