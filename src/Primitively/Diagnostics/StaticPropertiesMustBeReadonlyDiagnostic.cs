using Microsoft.CodeAnalysis;

namespace Primitively;

internal static partial class Diagnostics
{
    internal static class StaticPropertiesMustBeReadonlyDiagnostic
    {
        internal const string Id = "PRIMITIVELY006";
        internal const string Message = "Primitively types must declare static properties as readonly: '{0}'";
        internal const string Title = "Static properties must be readonly";

        private static readonly DiagnosticDescriptor _descriptor = new(Id, Title, Message, category: Usage, defaultSeverity: DiagnosticSeverity.Error, isEnabledByDefault: true);

        public static Diagnostic Create(SyntaxNode currentNode, string propertyNames) => Diagnostic.Create(_descriptor, currentNode.GetLocation(), propertyNames);
    }
}
