using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Primitively.Parsers;
using static Primitively.Diagnostics;

namespace Primitively;

internal static class Parser
{
    public const string DateOnlyAttribute = $"{nameof(Primitively)}.{nameof(Primitively.DateOnlyAttribute)}";
    public const string GuidAttribute = $"{nameof(Primitively)}.{nameof(Primitively.GuidAttribute)}";
    public const string StringAttribute = $"{nameof(Primitively)}.{nameof(Primitively.StringAttribute)}";

    private static readonly List<string> _attributeFullNames = new()
    {
        DateOnlyAttribute,
        GuidAttribute,
        StringAttribute
    };

    public static bool IsRecordStructTargetForGeneration(SyntaxNode node) =>
        node is RecordDeclarationSyntax record &&
        record.IsKind(SyntaxKind.RecordStructDeclaration) &&
        record.AttributeLists.Count > 0;

    public static RecordDeclarationSyntax? GetRecordStructSemanticTargetForGeneration(GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        // We know the node is a RecordDeclarationSyntax thanks to IsRecordStructTargetForGeneration
        var syntaxDeclaration = (RecordDeclarationSyntax)context.Node;

        // Loop through all the attributes on the Record Struct
        foreach (var attributeListSyntax in syntaxDeclaration.AttributeLists)
        {
            foreach (var attributeSyntax in attributeListSyntax.Attributes)
            {
                var symbolInfo = ModelExtensions.GetSymbolInfo(context.SemanticModel, attributeSyntax, cancellationToken);

                if (symbolInfo.Symbol is not IMethodSymbol attributeSymbol)
                {
                    // Weird, we couldn't get the symbol, so ignore it
                    continue;
                }

                // Get the containing type name
                var fullName = attributeSymbol.ContainingType.ToDisplayString();

                // Is the Record Struct decorated with a matching Primitively attribute
                if (_attributeFullNames.Any(a => a.Equals(fullName, StringComparison.Ordinal)))
                {
                    return syntaxDeclaration;
                }
            }
        }

        // We didn't find the attribute we were looking for
        return null;
    }

    public static List<RecordStructData> GetRecordStructDataToGenerate(SourceProductionContext context, Compilation compilation, ImmutableArray<RecordDeclarationSyntax> recordStructs)
    {
        var recordStructDataToGenerate = new List<RecordStructData>();
        var reportDiagnostic = context.ReportDiagnostic;
        var cancellationToken = context.CancellationToken;
        var attributeSymbols = GetPrimitiveAttributeSymbols(compilation);

        if (attributeSymbols == null)
        {
            return recordStructDataToGenerate;
        }

        foreach (var recordDeclarationSyntax in recordStructs)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!recordDeclarationSyntax.IsKind(SyntaxKind.RecordStructDeclaration))
            {
                // Something went wrong because the Record is not a Struct
                continue;
            }

            if (GetPrimitiveRecordStructSymbol(compilation, recordDeclarationSyntax) is not INamedTypeSymbol recordStructSymbol)
            {
                // Something went wrong
                continue;
            }

            var isMisconfigured = false;
            RecordStructData? recordStructData = null;

            foreach (var attributeData in recordStructSymbol.GetAttributes())
            {
                if (!attributeSymbols.Any(a => a.Equals(attributeData.AttributeClass, SymbolEqualityComparer.Default)))
                {
                    continue;
                }

                var name = recordStructSymbol.Name;
                var nameSpace = GetNameSpace(recordDeclarationSyntax);
                var parentData = GetParentData(recordDeclarationSyntax);
                var attributeName = attributeData.AttributeClass?.Name;

                switch (attributeName)
                {
                    case nameof(Primitively.DateOnlyAttribute):
                        isMisconfigured = !DateOnlyParser.TryParse(attributeData, name, nameSpace, parentData, out recordStructData);
                        break;
                    case nameof(Primitively.GuidAttribute):
                        isMisconfigured = !GuidParser.TryParse(attributeData, name, nameSpace, parentData, out recordStructData);
                        break;
                    case nameof(Primitively.StringAttribute):
                        isMisconfigured = !StringParser.TryParse(attributeData, name, nameSpace, parentData, out recordStructData);
                        break;
                }

                break;
            }

            if (recordStructData is null || isMisconfigured)
            {
                continue; // name clash, or error
            }

            // Check there is a partial keyword. If not, report it.
            var hasPartialModifier = false;
            foreach (var modifier in recordDeclarationSyntax.Modifiers)
            {
                if (modifier.IsKind(SyntaxKind.PartialKeyword))
                {
                    hasPartialModifier = true;
                    break;
                }
            }

            if (!hasPartialModifier)
            {
                reportDiagnostic(NotPartialDiagnostic.Create(recordDeclarationSyntax));
            }

            // Add type to the collection
            recordStructDataToGenerate.Add(recordStructData);
        }

        return recordStructDataToGenerate;
    }

    private static IEnumerable<INamedTypeSymbol> GetPrimitiveAttributeSymbols(Compilation compilation)
    {
        foreach (var attributeFullName in _attributeFullNames)
        {
            var attributeSymbol = compilation.GetTypeByMetadataName(attributeFullName);

            if (attributeSymbol is not null)
            {
                yield return attributeSymbol;
            }
        }
    }

    private static INamedTypeSymbol? GetPrimitiveRecordStructSymbol(Compilation compilation, RecordDeclarationSyntax recordDeclarationSyntax)
    {
        var semanticModel = compilation.GetSemanticModel(recordDeclarationSyntax.SyntaxTree);

        return semanticModel.GetDeclaredSymbol(recordDeclarationSyntax);
    }

    private static string GetNameSpace(RecordDeclarationSyntax recordStructSymbol)
    {
        // Determine the namespace the struct is declared in, if any
        var potentialNamespaceParent = recordStructSymbol.Parent;

        while (potentialNamespaceParent != null &&
               potentialNamespaceParent is not NamespaceDeclarationSyntax &&
               potentialNamespaceParent is not FileScopedNamespaceDeclarationSyntax)
        {
            potentialNamespaceParent = potentialNamespaceParent.Parent;
        }

        if (potentialNamespaceParent is BaseNamespaceDeclarationSyntax namespaceParent)
        {
            var nameSpace = namespaceParent.Name.ToString();

            while (true)
            {
                if (namespaceParent.Parent is not NamespaceDeclarationSyntax namespaceParentParent)
                {
                    break;
                }

                namespaceParent = namespaceParentParent;
                nameSpace = $"{namespaceParent.Name}.{nameSpace}";
            }

            return nameSpace;
        }

        return string.Empty;
    }

    private static ParentData? GetParentData(RecordDeclarationSyntax recordStructSymbol)
    {
        static bool IsAllowedKind(SyntaxKind kind) =>
            kind == SyntaxKind.ClassDeclaration ||
            kind == SyntaxKind.StructDeclaration ||
            kind == SyntaxKind.RecordDeclaration;

        TypeDeclarationSyntax? parent = recordStructSymbol.Parent as TypeDeclarationSyntax;
        ParentData? parentData = null;

        while (parent != null && IsAllowedKind(parent.Kind()))
        {
            parentData = new ParentData(
                parent.Keyword.ValueText,
                parent.Identifier.ToString() + parent.TypeParameterList,
                parent.ConstraintClauses.ToString(),
                parentData);

            parent = parent.Parent as TypeDeclarationSyntax;
        }

        return parentData;
    }
}
