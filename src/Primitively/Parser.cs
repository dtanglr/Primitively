//using System;
//using System.Collections.Generic;
//using System.Collections.Immutable;
//using System.Threading;
//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using Primitively.Diagnostics;

//namespace Primitively;

//internal static class Parser
//{
//    public const string PrimitivelyAttribute = "Primitively.PrimitivelyAttribute";
//    public const string PrimitivelyDefaultsAttribute = "Primitively.PrimitivelyDefaultsAttribute";

//    public static bool IsStructTargetForGeneration(SyntaxNode node)
//        => node is RecordDeclarationSyntax m && m.IsKind(SyntaxKind.RecordStructDeclaration) && m.AttributeLists.Count > 0;

//    public static bool IsAttributeTargetForGeneration(SyntaxNode node)
//        => node is AttributeListSyntax attributeList
//           && attributeList.Target is not null
//           && attributeList.Target.Identifier.IsKind(SyntaxKind.AssemblyKeyword);

//    public static StructDeclarationSyntax? GetStructSemanticTargetForGeneration(GeneratorSyntaxContext context)
//    {
//        // we know the node is a EnumDeclarationSyntax thanks to IsSyntaxTargetForGeneration
//        var structDeclarationSyntax = (StructDeclarationSyntax)context.Node;

//        // loop through all the attributes on the method
//        foreach (var attributeListSyntax in structDeclarationSyntax.AttributeLists)
//        {
//            foreach (var attributeSyntax in attributeListSyntax.Attributes)
//            {
//                if (ModelExtensions.GetSymbolInfo(context.SemanticModel, attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
//                {
//                    // weird, we couldn't get the symbol, ignore it
//                    continue;
//                }

//                var attributeContainingTypeSymbol = attributeSymbol.ContainingType;
//                var fullName = attributeContainingTypeSymbol.ToDisplayString();

//                // Is the attribute the [Primitively] attribute?
//                if (fullName == PrimitivelyAttribute)
//                {
//                    // return the enum
//                    return structDeclarationSyntax;
//                }
//            }
//        }

//        // we didn't find the attribute we were looking for
//        return null;
//    }

//    public static AttributeSyntax? GetAssemblyAttributeSemanticTargetForGeneration(GeneratorSyntaxContext context)
//    {
//        // we know the node is a AttributeListSyntax thanks to IsSyntaxTargetForGeneration
//        var attributeListSyntax = (AttributeListSyntax)context.Node;

//        // loop through all the attributes in the list
//        foreach (var attributeSyntax in attributeListSyntax.Attributes)
//        {
//            if (ModelExtensions.GetSymbolInfo(context.SemanticModel, attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
//            {
//                // weird, we couldn't get the symbol, ignore it
//                continue;
//            }

//            var attributeContainingTypeSymbol = attributeSymbol.ContainingType;
//            var fullName = attributeContainingTypeSymbol.ToDisplayString();

//            // Is the attribute the [PrimitivelyDefaultsAttribute] attribute?
//            if (fullName == PrimitivelyDefaultsAttribute)
//            {
//                // return the attribute
//                return attributeSyntax;
//            }
//        }

//        // we didn't find the attribute we were looking for
//        return null;
//    }

//    public static List<(string Name, string NameSpace, PrimitivelyConfiguration Config, ParentClass? Parent)> GetTypesToGenerate(
//        Compilation compilation,
//        ImmutableArray<StructDeclarationSyntax> targets,
//        Action<Diagnostic> reportDiagnostic,
//        CancellationToken ct)
//    {
//        var idsToGenerate = new List<(string Name, string NameSpace, PrimitivelyConfiguration Config, ParentClass? Parent)>();
//        var idAttribute = compilation.GetTypeByMetadataName(PrimitivelyAttribute);
//        if (idAttribute == null)
//        {
//            // nothing to do if this type isn't available
//            return idsToGenerate;
//        }

//        foreach (var structDeclarationSyntax in targets)
//        {
//            // stop if we're asked to
//            ct.ThrowIfCancellationRequested();

//            var semanticModel = compilation.GetSemanticModel(structDeclarationSyntax.SyntaxTree);
//            if (semanticModel.GetDeclaredSymbol(structDeclarationSyntax) is not INamedTypeSymbol structSymbol)
//            {
//                // something went wrong
//                continue;
//            }

//            PrimitivelyConfiguration? config = null;
//            var hasMisconfiguredInput = false;

//            foreach (var attribute in structSymbol.GetAttributes())
//            {
//                if (!idAttribute.Equals(attribute.AttributeClass, SymbolEqualityComparer.Default))
//                {
//                    continue;
//                }

//                PrimitivelyBackingType backingType = PrimitivelyBackingType.Default;
//                PrimitivelyConverter converter = PrimitivelyConverter.Default;
//                PrimitivelyImplementations implementations = PrimitivelyImplementations.Default;

//                if (!attribute.ConstructorArguments.IsEmpty)
//                {
//                    // make sure we don't have any errors
//                    var args = attribute.ConstructorArguments;

//                    foreach (var arg in args)
//                    {
//                        if (arg.Kind == TypedConstantKind.Error)
//                        {
//                            // have an error, so don't try and do any generation
//                            hasMisconfiguredInput = true;
//                        }
//                    }

//                    switch (args.Length)
//                    {
//                        case 3:
//                            implementations = (PrimitivelyImplementations)args[2].Value!;
//                            goto case 2;
//                        case 2:
//                            converter = (PrimitivelyConverter)args[1].Value!;
//                            goto case 1;
//                        case 1:
//                            backingType = (PrimitivelyBackingType)args[0].Value!;
//                            break;
//                    }
//                }

//                if (!attribute.NamedArguments.IsEmpty)
//                {
//                    foreach (var arg in attribute.NamedArguments)
//                    {
//                        var typedConstant = arg.Value;
//                        if (typedConstant.Kind == TypedConstantKind.Error)
//                        {
//                            hasMisconfiguredInput = true;
//                        }
//                        else
//                        {
//                            switch (arg.Key)
//                            {
//                                case "backingType":
//                                    backingType = (PrimitivelyBackingType)typedConstant.Value!;
//                                    break;
//                                case "converters":
//                                    converter = (PrimitivelyConverter)typedConstant.Value!;
//                                    break;
//                                case "implementations":
//                                    implementations = (PrimitivelyImplementations)typedConstant.Value!;
//                                    break;
//                            }
//                        }
//                    }

//                }

//                if (hasMisconfiguredInput)
//                {
//                    // skip further generator execution and let compiler generate the errors
//                    break;
//                }

//                if (!converter.IsValidFlags())
//                {
//                    reportDiagnostic(InvalidConverterDiagnostic.Create(structDeclarationSyntax));
//                }

//                if (!Enum.IsDefined(typeof(PrimitivelyBackingType), backingType))
//                {
//                    reportDiagnostic(InvalidBackingTypeDiagnostic.Create(structDeclarationSyntax));
//                }

//                if (!implementations.IsValidFlags())
//                {
//                    reportDiagnostic(InvalidImplementationsDiagnostic.Create(structDeclarationSyntax));
//                }

//                config = new PrimitivelyConfiguration(backingType, converter, implementations);
//                break;
//            }

//            if (config is null || hasMisconfiguredInput)
//            {
//                continue; // name clash, or error
//            }

//            var hasPartialModifier = false;
//            foreach (var modifier in structDeclarationSyntax.Modifiers)
//            {
//                if (modifier.IsKind(SyntaxKind.PartialKeyword))
//                {
//                    hasPartialModifier = true;
//                    break;
//                }
//            }

//            if (!hasPartialModifier)
//            {
//                reportDiagnostic(NotPartialDiagnostic.Create(structDeclarationSyntax));
//            }

//            var nameSpace = GetNameSpace(structDeclarationSyntax);
//            var parentClass = GetParentClasses(structDeclarationSyntax);
//            var name = structSymbol.Name;

//            idsToGenerate.Add((Name: name, NameSpace: nameSpace, Config: config.Value, Parent: parentClass));
//        }

//        return idsToGenerate;
//    }

//    public static PrimitivelyConfiguration? GetDefaults(
//        ImmutableArray<AttributeSyntax> defaults,
//        Compilation compilation,
//        Action<Diagnostic> reportDiagnostic)
//    {
//        if (defaults.IsDefaultOrEmpty)
//        {
//            // No global defaults
//            return null;
//        }

//        var assemblyAttributes = compilation.Assembly.GetAttributes();
//        if (assemblyAttributes.IsDefaultOrEmpty)
//        {
//            return null;
//        }

//        var defaultsAttribute = compilation.GetTypeByMetadataName(PrimitivelyDefaultsAttribute);
//        if (defaultsAttribute is null)
//        {
//            // The attribute isn't part of the compilation for some reason...
//            return null;
//        }

//        foreach (var attribute in assemblyAttributes)
//        {
//            if (!defaultsAttribute.Equals(attribute.AttributeClass, SymbolEqualityComparer.Default))
//            {
//                continue;
//            }

//            PrimitivelyBackingType backingType = PrimitivelyBackingType.Default;
//            PrimitivelyConverter converter = PrimitivelyConverter.Default;
//            PrimitivelyImplementations implementations = PrimitivelyImplementations.Default;
//            var hasMisconfiguredInput = false;

//            if (!attribute.ConstructorArguments.IsEmpty)
//            {
//                // make sure we don't have any errors
//                var args = attribute.ConstructorArguments;

//                foreach (var arg in args)
//                {
//                    if (arg.Kind == TypedConstantKind.Error)
//                    {
//                        // have an error, so don't try and do any generation
//                        hasMisconfiguredInput = true;
//                    }
//                }

//                switch (args.Length)
//                {
//                    case 3:
//                        implementations = (PrimitivelyImplementations)args[2].Value!;
//                        goto case 2;
//                    case 2:
//                        converter = (PrimitivelyConverter)args[1].Value!;
//                        goto case 1;
//                    case 1:
//                        backingType = (PrimitivelyBackingType)args[0].Value!;
//                        break;
//                }
//            }

//            if (!attribute.NamedArguments.IsEmpty)
//            {
//                foreach (var arg in attribute.NamedArguments)
//                {
//                    var typedConstant = arg.Value;
//                    if (typedConstant.Kind == TypedConstantKind.Error)
//                    {
//                        hasMisconfiguredInput = true;
//                    }
//                    else
//                    {
//                        switch (arg.Key)
//                        {
//                            case "backingType":
//                                backingType = (PrimitivelyBackingType)typedConstant.Value!;
//                                break;
//                            case "converters":
//                                converter = (PrimitivelyConverter)typedConstant.Value!;
//                                break;
//                            case "implementations":
//                                implementations = (PrimitivelyImplementations)typedConstant.Value!;
//                                break;
//                        }
//                    }
//                }
//            }

//            if (hasMisconfiguredInput)
//            {
//                // skip further generator execution and let compiler generate the errors
//                break;
//            }

//            SyntaxNode? syntax = null;
//            if (!converter.IsValidFlags())
//            {
//                syntax = attribute.ApplicationSyntaxReference?.GetSyntax();
//                if (syntax is not null)
//                {
//                    reportDiagnostic(InvalidConverterDiagnostic.Create(syntax));
//                }
//            }

//            if (!Enum.IsDefined(typeof(PrimitivelyBackingType), backingType))
//            {
//                syntax ??= attribute.ApplicationSyntaxReference?.GetSyntax();
//                if (syntax is not null)
//                {
//                    reportDiagnostic(InvalidBackingTypeDiagnostic.Create(syntax));
//                }
//            }

//            if (!implementations.IsValidFlags())
//            {
//                syntax ??= attribute.ApplicationSyntaxReference?.GetSyntax();
//                if (syntax is not null)
//                {
//                    reportDiagnostic(InvalidImplementationsDiagnostic.Create(syntax));
//                }
//            }

//            return new PrimitivelyConfiguration(backingType, converter, implementations);
//        }

//        return null;
//    }

//    private static string GetNameSpace(StructDeclarationSyntax structSymbol)
//    {
//        // determine the namespace the struct is declared in, if any
//        var potentialNamespaceParent = structSymbol.Parent;
//        while (potentialNamespaceParent != null &&
//               potentialNamespaceParent is not NamespaceDeclarationSyntax
//               && potentialNamespaceParent is not FileScopedNamespaceDeclarationSyntax)
//        {
//            potentialNamespaceParent = potentialNamespaceParent.Parent;
//        }

//        if (potentialNamespaceParent is BaseNamespaceDeclarationSyntax namespaceParent)
//        {
//            var nameSpace = namespaceParent.Name.ToString();
//            while (true)
//            {
//                if (namespaceParent.Parent is not NamespaceDeclarationSyntax namespaceParentParent)
//                {
//                    break;
//                }

//                namespaceParent = namespaceParentParent;
//                nameSpace = $"{namespaceParent.Name}.{nameSpace}";
//            }

//            return nameSpace;
//        }
//        return string.Empty;
//    }

//    private static ParentClass? GetParentClasses(StructDeclarationSyntax structSymbol)
//    {
//        var parentIdClass = structSymbol.Parent as TypeDeclarationSyntax;
//        ParentClass? parentClass = null;

//        while (parentIdClass != null && IsAllowedKind(parentIdClass.Kind()))
//        {
//            parentClass = new ParentClass(
//                keyword: parentIdClass.Keyword.ValueText,
//                name: parentIdClass.Identifier.ToString() + parentIdClass.TypeParameterList,
//                constraints: parentIdClass.ConstraintClauses.ToString(),
//                child: parentClass);

//            parentIdClass = parentIdClass.Parent as TypeDeclarationSyntax;
//        }

//        return parentClass;

//        static bool IsAllowedKind(SyntaxKind kind) =>
//            kind == SyntaxKind.ClassDeclaration ||
//            kind == SyntaxKind.StructDeclaration ||
//            kind == SyntaxKind.RecordDeclaration;
//    }
//}
