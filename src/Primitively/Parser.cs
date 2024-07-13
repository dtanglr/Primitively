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

/// <summary>
/// Provides methods for parsing record struct data.
/// </summary>
internal static class Parser
{
    private static readonly List<string> _attributeFullNames =
    [
        typeof(ByteAttribute).FullName,
        typeof(DateOnlyAttribute).FullName,
        typeof(DecimalAttribute).FullName,
        typeof(DoubleAttribute).FullName,
        typeof(GuidAttribute).FullName,
        typeof(IntAttribute).FullName,
        typeof(LongAttribute).FullName,
        typeof(SByteAttribute).FullName,
        typeof(ShortAttribute).FullName,
        typeof(SingleAttribute).FullName,
        typeof(StringAttribute).FullName,
        typeof(UIntAttribute).FullName,
        typeof(ULongAttribute).FullName,
        typeof(UShortAttribute).FullName
    ];

    /// <summary>
    /// Gets the record struct data to generate.
    /// </summary>
    /// <param name="context">The source production context.</param>
    /// <param name="compilation">The compilation.</param>
    /// <param name="recordStructs">The record structs.</param>
    /// <returns>A list of record struct data to generate.</returns>
    public static List<RecordStructData> GetRecordStructDataToGenerate(SourceProductionContext context, Compilation compilation, ImmutableArray<RecordDeclarationSyntax?> recordStructs)
    {
        var recordStructDataToGenerate = new List<RecordStructData>();

        if (recordStructs.IsDefaultOrEmpty)
        {
            return recordStructDataToGenerate;
        }

        var attributeSymbols = GetPrimitiveAttributeSymbols(compilation);

        if (attributeSymbols == null)
        {
            return recordStructDataToGenerate;
        }

        var reportDiagnostic = context.ReportDiagnostic;
        var cancellationToken = context.CancellationToken;

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

                isMisconfigured = attributeName switch
                {
                    // DateOnly
                    nameof(DateOnlyAttribute) => !DateOnlyParser.TryParse(attributeData, name, nameSpace, parentData, out recordStructData),
                    // Guid
                    nameof(GuidAttribute) => !GuidParser.TryParse(attributeData, name, nameSpace, parentData, out recordStructData),
                    // Numeric
                    nameof(ByteAttribute) => !NumericParser.TryParse(attributeData, name, nameSpace, parentData, DataType.Byte, out recordStructData),
                    nameof(DecimalAttribute) => !NumericParser.TryParse(attributeData, name, nameSpace, parentData, DataType.Decimal, out recordStructData),
                    nameof(DoubleAttribute) => !NumericParser.TryParse(attributeData, name, nameSpace, parentData, DataType.Double, out recordStructData),
                    nameof(IntAttribute) => !NumericParser.TryParse(attributeData, name, nameSpace, parentData, DataType.Int, out recordStructData),
                    nameof(LongAttribute) => !NumericParser.TryParse(attributeData, name, nameSpace, parentData, DataType.Long, out recordStructData),
                    nameof(SByteAttribute) => !NumericParser.TryParse(attributeData, name, nameSpace, parentData, DataType.SByte, out recordStructData),
                    nameof(ShortAttribute) => !NumericParser.TryParse(attributeData, name, nameSpace, parentData, DataType.Short, out recordStructData),
                    nameof(SingleAttribute) => !NumericParser.TryParse(attributeData, name, nameSpace, parentData, DataType.Single, out recordStructData),
                    nameof(UIntAttribute) => !NumericParser.TryParse(attributeData, name, nameSpace, parentData, DataType.UInt, out recordStructData),
                    nameof(ULongAttribute) => !NumericParser.TryParse(attributeData, name, nameSpace, parentData, DataType.ULong, out recordStructData),
                    nameof(UShortAttribute) => !NumericParser.TryParse(attributeData, name, nameSpace, parentData, DataType.UShort, out recordStructData),
                    // String
                    nameof(StringAttribute) => !StringParser.TryParse(attributeData, name, nameSpace, parentData, out recordStructData),
                    _ => throw new NotImplementedException(),
                };

                break;
            }

            if (recordStructData is null || isMisconfigured)
            {
                continue; // name clash, or error
            }

            // Check there is a partial keyword. If not, report it.
            var hasPartialModifier = recordDeclarationSyntax.Modifiers
                .ToList()
                .Exists(p => p.IsKind(SyntaxKind.PartialKeyword));

            if (!hasPartialModifier)
            {
                reportDiagnostic(NotPartialDiagnostic.Create(recordDeclarationSyntax));
            }

            // Add type to the collection
            recordStructDataToGenerate.Add(recordStructData);
        }

        return recordStructDataToGenerate;
    }

    /// <summary>
    /// Gets the record struct semantic target for generation.
    /// </summary>
    /// <param name="context">The generator syntax context.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The record struct semantic target for generation, if any; otherwise, null.</returns>
    public static RecordDeclarationSyntax? GetRecordStructSemanticTargetForGeneration(GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        // We know the node is a RecordDeclarationSyntax thanks to IsRecordStructTargetForGeneration
        if (context.Node is not RecordDeclarationSyntax syntaxDeclaration)
        {
            return null;
        }

        // Loop through all the attributes on the Record Struct
        foreach (var attributeListSyntax in syntaxDeclaration.AttributeLists)
        {
            if (attributeListSyntax is null)
            {
                continue;
            }

            foreach (var attributeSyntax in attributeListSyntax.Attributes)
            {
                if (attributeSyntax is null)
                {
                    continue;
                }

                var symbolInfo = context.SemanticModel.GetSymbolInfo(attributeSyntax, cancellationToken);

                if (symbolInfo.Symbol is not IMethodSymbol attributeSymbol)
                {
                    continue;
                }

                // Get the containing type name
                var fullName = attributeSymbol.ContainingType.ToDisplayString();

                // Is the Record Struct decorated with a matching Primitively attribute
                if (_attributeFullNames.Exists(a => a.Equals(fullName, StringComparison.Ordinal)))
                {
                    return syntaxDeclaration;
                }
            }
        }

        // We didn't find the attribute we were looking for
        return null;
    }

    /// <summary>
    /// Determines whether the specified node is a record struct target for generation.
    /// </summary>
    /// <param name="node">The syntax node to check.</param>
    /// <returns>true if the specified node is a record struct target for generation; otherwise, false.</returns>
    public static bool IsRecordStructTargetForGeneration(SyntaxNode node) =>
        node is RecordDeclarationSyntax record &&
        record.IsKind(SyntaxKind.RecordStructDeclaration) &&
        record.AttributeLists.Count > 0;

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

        var parent = recordStructSymbol.Parent as TypeDeclarationSyntax;
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
}
