using Microsoft.CodeAnalysis;

namespace Primitively;

internal static partial class Diagnostics
{
    internal static class StaticFieldsMustBeReadonlyDiagnostic
    {
        internal const string Id = "PRIMITIVELY005";
        internal const string Message = "Primitively types must declare static fields as readonly: '{0}'";
        internal const string Title = "Static fields must be readonly";

        private static readonly DiagnosticDescriptor _descriptor = new(Id, Title, Message, category: Usage, defaultSeverity: DiagnosticSeverity.Error, isEnabledByDefault: true);

        public static Diagnostic Create(SyntaxNode currentNode, string fieldNames) => Diagnostic.Create(_descriptor, currentNode.GetLocation(), fieldNames);
    }
}
