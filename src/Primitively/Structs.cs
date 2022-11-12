﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Primitively;

/// <inheritdoc />
[Generator]
public class Structs : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Register the record struct and factory class sources
        var source = GetTargetSyntax(context);
        context.RegisterSourceOutput(source, (ctx, src) =>
        {
            // Parse the c# and collect all the partial record structs decorated with a Primitively attribute
            var recordStructs = Parser.GetRecordStructDataToGenerate(ctx, src.Compilation, src.RecordStructs);

            // Create a c# file for each struct
            GenerateRecordStructFiles(ctx, recordStructs);

            // Create a c# file for a factory class
            GenerateFactoryFile(ctx, recordStructs, src.Compilation);

            // Create a c# file for a repository class
            GenerateRepositoryFile(ctx, recordStructs, src.Compilation);
        });
    }

    private static IncrementalValueProvider<(Compilation Compilation, ImmutableArray<RecordDeclarationSyntax?> RecordStructs)> GetTargetSyntax(IncrementalGeneratorInitializationContext context)
    {
        // Create SyntaxProvider which sniffs out Record Structs decorated with a Primitively attribute
        var recordDeclarationSyntaxProvider = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (node, _) => Parser.IsRecordStructTargetForGeneration(node),
                transform: static (ctx, cancellationToken) => Parser.GetRecordStructSemanticTargetForGeneration(ctx, cancellationToken));

        var targets = recordDeclarationSyntaxProvider.Collect();
        var compilationAndValues = context.CompilationProvider.Combine(targets);

        return compilationAndValues;
    }

    private static void GenerateRecordStructFiles(SourceProductionContext context, List<RecordStructData> recordStructs)
    {
        var sb = new StringBuilder();

        foreach (var recordStruct in recordStructs)
        {
            sb.Clear();
            sb.Append(EmbeddedResources.AutoGeneratedHeader);
            sb.AppendLine();
            sb.AppendLine($"namespace {recordStruct.NameSpace};");
            sb.AppendLine();
            sb.Append(EmbeddedResources.JsonConverterAttribute);
            sb.Append(EmbeddedResources.TypeConverterAttribute);

            switch (recordStruct.DataType)
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
                    throw new NotSupportedException($"{recordStruct.DataType} is not supported");
            }

            // Add Validate Method
            sb.Append(EmbeddedResources.ValidateMethod);

            // Add closing brace
            sb.AppendLine("}");

            // Replace variable names with values
            sb.Replace("PRIMITIVE_TYPE", recordStruct.Name);
            sb.Replace("PRIMITIVE_IVALIDATABLEOBJECT", recordStruct.ImplementIValidatableObject ? ", System.ComponentModel.DataAnnotations.IValidatableObject" : string.Empty);
            sb.Replace("PRIMITIVE_PATTERN", recordStruct.Pattern);
            sb.Replace("PRIMITIVE_EXAMPLE", recordStruct.Example);
            sb.Replace("PRIMITIVE_FORMAT", recordStruct.Format);
            sb.Replace("PRIMITIVE_LENGTH", recordStruct.Length.ToString());
            sb.Replace("PRIMITIVE_MINLENGTH", recordStruct.MinLength.ToString());
            sb.Replace("PRIMITIVE_MAXLENGTH", recordStruct.MaxLength.ToString());

            // Construct source file text from string
            context.AddSource($"{recordStruct.NameSpace}.{recordStruct.Name}.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
        }
    }

    private static void GenerateFactoryFile(SourceProductionContext context, List<RecordStructData> recordStructs, Compilation compilation)
    {
        const string Padding = "            ";

        var dataTypes = recordStructs.Select(rs => rs.DataType)?.Distinct() ?? Enumerable.Empty<DataType>();
        var caseStatements = new List<string>();

        // Generate a list of switch case statements
        foreach (var dataType in dataTypes)
        {
            var items = recordStructs
                .Where(rs => rs.DataType == dataType)
                .OrderBy(rs => rs.Name)
                .Select(rs => $"{Padding}\"{rs.NameSpace}.{rs.Name}\" => ({rs.NameSpace}.{rs.Name})value,");

            caseStatements.Add($"{Padding}// {dataType}");
            caseStatements.AddRange(items);
        }

        // Create a c# file for the factory class
        var sb = new StringBuilder();
        sb.Append(EmbeddedResources.AutoGeneratedHeader);
        sb.AppendLine();
        sb.AppendLine($"namespace {compilation.AssemblyName};"); // TODO: Get Root Namespace from the Compilation class
        sb.AppendLine();
        sb.Append(EmbeddedResources.PrimitiveFactory);

        // Replace variable names with values
        sb.Replace("PRIMITIVE_FACTORY_CASE_STATEMENTS", string.Join(Environment.NewLine, caseStatements));

        // Construct source file text from string
        context.AddSource("PrimitiveFactory.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
    }

    private static void GenerateRepositoryFile(SourceProductionContext context, List<RecordStructData> recordStructs, Compilation compilation)
    {
        const string Padding = "        ";

        var dataTypes = recordStructs.Select(rs => rs.DataType)?.Distinct() ?? Enumerable.Empty<DataType>();
        var yieldStatements = new List<string>();

        if (!dataTypes.Any())
        {
            yieldStatements.Add($"{Padding}return Enumerable.Empty<PrimitiveInfo>();");
        }
        else
        {
            // Generate a list of yield return statements
            foreach (var dataType in dataTypes)
            {
                var items = recordStructs
                    .Where(rs => rs.DataType == dataType)
                    .OrderBy(rs => rs.Name)
                    .Select(rs => rs.DataType switch
                    {
                        DataType.DateOnly => $"{Padding}yield return new DateOnlyInfo(typeof({rs.NameSpace}.{rs.Name}), typeof(DateOnly), \"{rs.Example}\", \"{rs.Format}\", {rs.Length});",
                        DataType.Guid => $"{Padding}yield return new GuidInfo(typeof({rs.NameSpace}.{rs.Name}), typeof(Guid), \"{rs.Example}\", {nameof(rs.Specifier)}.{rs.Specifier}, {rs.Length});",
                        DataType.String => $"{Padding}yield return new StringInfo(typeof({rs.NameSpace}.{rs.Name}), typeof(string), \"{rs.Example}\", \"{rs.Format}\", \"{rs.Pattern}\", {rs.MinLength}, {rs.MaxLength});",
                        _ => throw new NotImplementedException()
                    });

                yieldStatements.Add($"{Padding}// {dataType}");
                yieldStatements.AddRange(items);
            }
        }

        // Create a c# file for the factory class
        var sb = new StringBuilder();
        sb.Append(EmbeddedResources.AutoGeneratedHeader);
        sb.AppendLine();
        sb.Append(EmbeddedResources.PrimitiveRepository);

        // Replace variable names with values
        sb.Replace("PRIMITIVE_NAMESPACE", compilation.AssemblyName); // TODO: Get Root Namespace from the Compilation class
        sb.Replace("PRIMITIVE_REPOSITORY_YIELD_STATEMENTS", string.Join(Environment.NewLine, yieldStatements));

        // Construct source file text from string
        context.AddSource("PrimitiveRepository.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
    }
}