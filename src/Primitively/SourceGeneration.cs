﻿using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Primitively;

/// <inheritdoc />
[Generator]
public class SourceGeneration : IIncrementalGenerator
{
    private const string EmbedAbstractionsSymbol = "EMBED_PRIMITIVELY_ABSTRACTIONS";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Register the abstractions sources
        context.RegisterPostInitializationOutput(ctx => GenerateSource(ctx));

        // Register the record struct sources
        var source = GetTargetSyntax(context);
        context.RegisterSourceOutput(source, (ctx, src) => GenerateSource(ctx, src.Compilation, src.RecordStructs));
    }

    private static void GenerateSource(IncrementalGeneratorPostInitializationContext context)
    {
        var sb = new StringBuilder();

        foreach (var resource in EmbeddedResources.Abstractions.GetEmbeddedResources())
        {
            sb.Clear();
            sb.Append(EmbeddedResources.AutoGeneratedHeader);
            sb.AppendLine();
            sb.AppendLine($"#if {EmbedAbstractionsSymbol}");
            sb.AppendLine();
            sb.Append(resource.Value);
            sb.AppendLine();
            sb.AppendLine("#endif");

            var sourceText = SourceText.From(sb.ToString(), Encoding.UTF8);
            context.AddSource($"{resource.Key}.g.cs", sourceText);
        }
    }

    private static void GenerateSource(SourceProductionContext context, Compilation compilation, ImmutableArray<RecordDeclarationSyntax> recordStructs)
    {
        if (recordStructs.IsDefaultOrEmpty)
        {
            return;
        }

        var structDataToGenerate = Parser.GetRecordStructDataToGenerate(compilation, recordStructs, context.ReportDiagnostic, context.CancellationToken);

        if (!structDataToGenerate.Any())
        {
            return;
        }

        var sb = new StringBuilder();

        foreach (var type in structDataToGenerate)
        {
            sb.Clear();
            sb.Append(EmbeddedResources.AutoGeneratedHeader);
            sb.AppendLine();
            sb.AppendLine($"namespace {type.NameSpace};");
            sb.AppendLine();
            sb.Append(EmbeddedResources.JsonConverterAttribute);
            sb.Append(EmbeddedResources.TypeConverterAttribute);

            switch (type.DataType)
            {
                case DataType.DateOnly:
                    sb.Append(EmbeddedResources.DateOnly.Base);
                    sb.Append(EmbeddedResources.DateOnly.JsonConverter);
                    sb.Append(EmbeddedResources.DateOnly.TypeConverter);
                    break;
                case DataType.Guid:
                    sb.Append(EmbeddedResources.Guid.Base);
                    sb.Append(EmbeddedResources.Guid.JsonConverter);
                    sb.Append(EmbeddedResources.Guid.TypeConverter);
                    break;
                case DataType.String:
                    sb.Append(EmbeddedResources.String.Base);
                    sb.Append(EmbeddedResources.String.DefaultPartialMethods);
                    sb.Append(EmbeddedResources.String.JsonConverter);
                    sb.Append(EmbeddedResources.String.TypeConverter);
                    break;
                default:
                    throw new NotSupportedException($"{type.DataType} is not supported");
            }

            // Add Validate Method
            sb.Append(EmbeddedResources.ValidateMethod);

            // Add closing brace
            sb.AppendLine("}");

            // Replece variable names with values
            sb.Replace("PRIMITIVE_TYPE", type.Name);
            sb.Replace("PRIMITIVE_PATTERN", type.Pattern);
            sb.Replace("PRIMITIVE_EXAMPLE", type.Example);
            sb.Replace("PRIMITIVE_FORMAT", type.Format);
            sb.Replace("PRIMITIVE_LENGTH", type.Length.ToString());
            sb.Replace("PRIMITIVE_MINLENGTH", type.MinLength.ToString());
            sb.Replace("PRIMITIVE_MAXLENGTH", type.MaxLength.ToString());

            // Construct source file text from string
            var sourceText = SourceText.From(sb.ToString(), Encoding.UTF8);
            context.AddSource($"{type.Name}.g.cs", sourceText);
        }
    }

    private static IncrementalValueProvider<(Compilation Compilation, ImmutableArray<RecordDeclarationSyntax> RecordStructs)> GetTargetSyntax(IncrementalGeneratorInitializationContext context)
    {
        // Create SyntaxProvider which sniffs out Record Structs decorated with a Primitively attribute
        IncrementalValuesProvider<RecordDeclarationSyntax> recordDeclarationSyntaxProvider = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (node, _) => Parser.IsRecordStructTargetForGeneration(node),
                transform: static (context, cancellationToken) => Parser.GetRecordStructSemanticTargetForGeneration(context, cancellationToken))
            .Where(static m => m is not null)!;

        var targets = recordDeclarationSyntaxProvider.Collect();
        var compilationAndValues = context.CompilationProvider.Combine(targets);

        return compilationAndValues;
    }
}
