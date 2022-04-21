using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Primitively.Diagnostics;

namespace Primitively;

internal static class Parser
{
    public const string DatePrimitiveAttribute = $"{nameof(Primitively)}.{nameof(Primitively.DatePrimitiveAttribute)}";
    public const string GuidPrimitiveAttribute = $"{nameof(Primitively)}.{nameof(Primitively.GuidPrimitiveAttribute)}";
    public const string StringPrimitiveAttribute = $"{nameof(Primitively)}.{nameof(Primitively.StringPrimitiveAttribute)}";

    private static readonly List<string> _attributeFullNames = new()
    {
        DatePrimitiveAttribute,
        GuidPrimitiveAttribute,
        StringPrimitiveAttribute
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

    public static List<PrimitiveRecordStruct> GetPrimitiveRecordStructsToGenerate(
        Compilation compilation,
        ImmutableArray<RecordDeclarationSyntax> recordStructs,
        Action<Diagnostic> reportDiagnostic,
        CancellationToken cancellationToken)
    {
        var typesToGenerate = new List<PrimitiveRecordStruct>();
        var attributeSymbols = GetPrimitiveAttributeSymbols(compilation);

        if (!attributeSymbols.Any())
        {
            return typesToGenerate;
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

            foreach (var attribute in recordStructSymbol.GetAttributes())
            {
                if (!attributeSymbols.Any(a => a.Equals(attribute.AttributeClass, SymbolEqualityComparer.Default)))
                {
                    continue;
                }

                var isMisconfigured = false;
                var name = recordStructSymbol.Name;
                var nameSpace = GetNameSpace(recordDeclarationSyntax);
                var parentClass = GetParentClasses(recordDeclarationSyntax);
                var type = new PrimitiveRecordStruct(name, nameSpace, parentClass);
                var attributeName = attribute.AttributeClass?.Name;

                switch (attributeName)
                {
                    case nameof(Primitively.DatePrimitiveAttribute):
                        type.PrimitiveType = PrimitiveType.Date;
                        break;
                    case nameof(Primitively.GuidPrimitiveAttribute):
                        type.PrimitiveType = PrimitiveType.Guid;
                        break;
                    case nameof(Primitively.StringPrimitiveAttribute):
                        type.PrimitiveType = PrimitiveType.String;
                        break;
                }

                if (!attribute.ConstructorArguments.IsEmpty)
                {
                    // make sure we don't have any errors
                    var args = attribute.ConstructorArguments;

                    foreach (var arg in args)
                    {
                        if (arg.Kind == TypedConstantKind.Error)
                        {
                            // There's an error, so don't try and do any generation
                            isMisconfigured = true;
                        }
                    }

                    switch (args.Length)
                    {
                        case 5:
                            type.MaxLength = (int)args[4].Value!;
                            type.MinLength = (int)args[3].Value!;
                            goto case 3;
                        case 4:
                            type.MaxLength = (int)args[3].Value!;
                            type.MinLength = (int)args[3].Value!;
                            goto case 3;
                        case 3:
                            type.Format = (string?)args[2].Value;
                            goto case 2;
                        case 2:
                            type.Example = (string?)args[1].Value;
                            goto case 1;
                        case 1:
                            type.Pattern = (string?)args[0].Value;
                            break;
                    }
                }

                if (!attribute.NamedArguments.IsEmpty)
                {
                    var d = 2;
                }

                typesToGenerate.Add(type);

                break;
            }
        }

        return typesToGenerate;
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

    private static ParentClass? GetParentClasses(RecordDeclarationSyntax recordStructSymbol)
    {
        static bool IsAllowedKind(SyntaxKind kind) =>
            kind == SyntaxKind.ClassDeclaration ||
            kind == SyntaxKind.StructDeclaration ||
            kind == SyntaxKind.RecordDeclaration;

        TypeDeclarationSyntax? parentIdClass = recordStructSymbol.Parent as TypeDeclarationSyntax;
        ParentClass? parentClass = null;

        while (parentIdClass != null && IsAllowedKind(parentIdClass.Kind()))
        {
            parentClass = new ParentClass(
                keyword: parentIdClass.Keyword.ValueText,
                name: parentIdClass.Identifier.ToString() + parentIdClass.TypeParameterList,
                constraints: parentIdClass.ConstraintClauses.ToString(),
                child: parentClass);

            parentIdClass = parentIdClass.Parent as TypeDeclarationSyntax;
        }

        return parentClass;
    }
}
