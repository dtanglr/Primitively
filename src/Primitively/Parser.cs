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
    public const string DateOnlyAttribute = $"{nameof(Primitively)}.{nameof(Primitively.DateOnlyAttribute)}";
    public const string GuidAttribute = $"{nameof(Primitively)}.{nameof(Primitively.GuidAttribute)}";
    public const string NhsNumberPrimitiveAttribute = $"{nameof(Primitively)}.{nameof(Primitively.NhsNumberPrimitiveAttribute)}";
    public const string OdsCodePrimitiveAttribute = $"{nameof(Primitively)}.{nameof(Primitively.OdsCodePrimitiveAttribute)}";
    public const string PostcodePrimitiveAttribute = $"{nameof(Primitively)}.{nameof(Primitively.PostcodePrimitiveAttribute)}";
    public const string StringPrimitiveAttribute = $"{nameof(Primitively)}.{nameof(Primitively.StringPrimitiveAttribute)}";

    private static readonly List<string> _attributeFullNames = new()
    {
        DateOnlyAttribute,
        GuidAttribute,
        NhsNumberPrimitiveAttribute,
        OdsCodePrimitiveAttribute,
        PostcodePrimitiveAttribute,
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

        if (attributeSymbols == null)
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

            var isMisconfigured = false;
            PrimitiveRecordStruct? typeToGenerate = null;

            foreach (var attribute in recordStructSymbol.GetAttributes())
            {
                if (!attributeSymbols.Any(a => a.Equals(attribute.AttributeClass, SymbolEqualityComparer.Default)))
                {
                    continue;
                }

                var name = recordStructSymbol.Name;
                var nameSpace = GetNameSpace(recordDeclarationSyntax);
                var parentClass = GetParentClasses(recordDeclarationSyntax);
                var attributeName = attribute.AttributeClass?.Name;

                switch (attributeName)
                {
                    case nameof(Primitively.DateOnlyAttribute):
                        typeToGenerate = new PrimitiveRecordStruct(PrimitiveType.DateOnly, name, nameSpace, parentClass)
                        {
                            Length = Constants.DateOnly.Iso8601.Length,
                            Example = Constants.DateOnly.Iso8601.Example,
                            Format = Constants.DateOnly.Iso8601.Format
                        };
                        break;
                    case nameof(Primitively.GuidAttribute):
                        typeToGenerate = new PrimitiveRecordStruct(PrimitiveType.Guid, name, nameSpace, parentClass)
                        {
                            Length = Constants.Guid.Default.Length,
                            Example = Constants.Guid.Default.Example,
                            Format = Constants.Guid.Default.Format
                        };
                        break;
                    case nameof(Primitively.NhsNumberPrimitiveAttribute):
                        typeToGenerate = new PrimitiveRecordStruct(PrimitiveType.NhsNumber, name, nameSpace, parentClass)
                        {
                            MinLength = Constants.StringPrimitive.NhsNumber.Length,
                            MaxLength = Constants.StringPrimitive.NhsNumber.Length,
                            Example = Constants.StringPrimitive.NhsNumber.Example,
                            Pattern = Constants.StringPrimitive.NhsNumber.Pattern
                        };
                        break;
                    case nameof(Primitively.OdsCodePrimitiveAttribute):
                        typeToGenerate = new PrimitiveRecordStruct(PrimitiveType.String, name, nameSpace, parentClass)
                        {
                            MinLength = Constants.StringPrimitive.OdsCode.MinLength,
                            MaxLength = Constants.StringPrimitive.OdsCode.MaxLength,
                            Example = Constants.StringPrimitive.OdsCode.Example,
                            Pattern = Constants.StringPrimitive.OdsCode.Pattern
                        };
                        break;
                    case nameof(Primitively.PostcodePrimitiveAttribute):
                        typeToGenerate = new PrimitiveRecordStruct(PrimitiveType.String, name, nameSpace, parentClass)
                        {
                            MinLength = Constants.StringPrimitive.Postcode.MinLength,
                            MaxLength = Constants.StringPrimitive.Postcode.MaxLength,
                            Example = Constants.StringPrimitive.Postcode.Example,
                            Pattern = Constants.StringPrimitive.Postcode.Pattern
                        };
                        break;
                    case nameof(Primitively.StringPrimitiveAttribute):
                        typeToGenerate = new PrimitiveRecordStruct(PrimitiveType.String, name, nameSpace, parentClass);
                        isMisconfigured = !TrySetFromPrimitiveAttributeArguments(attribute, typeToGenerate);
                        break;
                }

                break;
            }

            if (typeToGenerate is null || isMisconfigured)
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
            typesToGenerate.Add(typeToGenerate);
        }

        return typesToGenerate;
    }

    private static bool TrySetFromPrimitiveAttributeArguments(AttributeData attribute, PrimitiveRecordStruct type)
    {
        if (!TrySetFromPrimitiveAttributeConstructorArguments(attribute, type))
        {
            return false;
        }

        return TrySetFromPrimitiveAttributeNamedArguments(attribute, type);
    }

    private static bool TrySetFromPrimitiveAttributeConstructorArguments(AttributeData attribute, PrimitiveRecordStruct type)
    {
        if (attribute.ConstructorArguments.IsEmpty)
        {
            // 1 or 2 arguments are expected
            return false;
        }

        var args = attribute.ConstructorArguments;
        if (args.Any(a => a.Kind == TypedConstantKind.Error))
        {
            return false;
        }

        switch (args.Length)
        {
            case 2:
                type.MaxLength = args[1].IsNull ? 0 : (int)args[1].Value!;
                type.MinLength = args[0].IsNull ? 0 : (int)args[0].Value!;
                break;
            case 1:
                var length = args[0].IsNull ? 0 : (int)args[0].Value!;
                type.MaxLength = length;
                type.MinLength = length;
                break;
            default:
                return false; // just in case
        }

        return true;
    }

    private static bool TrySetFromPrimitiveAttributeNamedArguments(AttributeData attribute, PrimitiveRecordStruct type)
    {
        if (attribute.NamedArguments.IsEmpty)
        {
            return true;
        }

        var args = attribute.NamedArguments;
        if (args.Any(a => a.Value.Kind == TypedConstantKind.Error))
        {
            return false;
        }

        foreach (var arg in args)
        {
            var key = arg.Key;
            var value = (string?)arg.Value.Value;

            switch (key)
            {
                case nameof(Primitively.StringPrimitiveAttribute.Example):
                    type.Example = value;
                    break;
                case nameof(Primitively.StringPrimitiveAttribute.Format):
                    type.Format = value;
                    break;
                case nameof(Primitively.StringPrimitiveAttribute.Pattern):
                    type.Pattern = value;
                    break;
            }
        }

        return true;
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
