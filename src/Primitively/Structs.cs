﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Primitively;

/// <summary>
/// A source generator for creating Primitively struct types.
/// </summary>
[Generator]
public class Structs : IIncrementalGenerator
{
    /// <summary>
    /// Initializes the generator.
    /// </summary>
    /// <param name="context">The initialization context.</param>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
#if DEBUGGERENABLED
        if (!System.Diagnostics.Debugger.IsAttached)
        {
            System.Diagnostics.Debugger.Launch();
        }
#endif 
        // Register the record struct and factory class sources
        var source = GetTargetSyntax(context);
        context.RegisterSourceOutput(source, (ctx, src) =>
        {
            // Parse the c# and collect all the partial record structs decorated with a Primitively attribute
            var recordStructs = Parser.GetRecordStructDataToGenerate(ctx, src.Compilation, src.RecordStructs);

            // TODO: Validate Integers ensuring Minimum and Maximum range is valid. If not, add Diagnostics

            // Create a c# file for each struct
            GenerateRecordStructFiles(ctx, recordStructs);

            // Create a c# file for a factory class
            GenerateFactoryFile(ctx, recordStructs, src.Compilation);

            // Create a c# file for a repository class
            GenerateRepositoryFile(ctx, recordStructs, src.Compilation);

            // Create a c# file for a library class
            GenerateLibraryFile(ctx, recordStructs, src.Compilation);
        });
    }

    /// <summary>
    /// Generates a factory file.
    /// </summary>
    /// <param name="context">The source production context.</param>
    /// <param name="recordStructs">The record structs.</param>
    /// <param name="compilation">The compilation.</param>
    private static void GenerateFactoryFile(SourceProductionContext context, List<RecordStructData> recordStructs, Compilation compilation)
    {
        const string Padding = "            ";

        var dataTypes = recordStructs.Select(rs => rs.DataType).Distinct();
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
        sb.Append(EmbeddedResources.PrimitiveFactory);

        // Replace variable names with values
        sb.Replace("PRIMITIVE_NAMESPACE", compilation.AssemblyName); // TODO: Get Root Namespace from the Compilation class
        sb.Replace("PRIMITIVE_FACTORY_CASE_STATEMENTS", string.Join("\r\n", caseStatements));

        // Construct source file text from string
        context.AddSource("PrimitiveFactory.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
    }

    /// <summary>
    /// Generates a library file.
    /// </summary>
    /// <param name="context">The source production context.</param>
    /// <param name="recordStructs">The record structs.</param>
    /// <param name="compilation">The compilation.</param>
    private static void GenerateLibraryFile(SourceProductionContext context, List<RecordStructData> recordStructs, Compilation compilation)
    {
        // Create a c# file for the library class
        var sb = new StringBuilder();
        sb.Append(EmbeddedResources.AutoGeneratedHeader);
        sb.AppendLine();
        sb.Append(EmbeddedResources.PrimitiveLibrary);

        // Replace variable names with values
        sb.Replace("PRIMITIVE_NAMESPACE", compilation.AssemblyName); // TODO: Get Root Namespace from the Compilation class
        sb.Replace("PRIMITIVE_LIBRARY_HAS_TYPES", recordStructs.Any().ToString().ToLower());

        // Construct source file text from string
        context.AddSource("PrimitiveLibrary.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
    }

    /// <summary>
    /// Generates record struct files.
    /// </summary>
    /// <param name="context">The source production context.</param>
    /// <param name="recordStructs">The record structs.</param>
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
                case DataType.Byte:
                case DataType.SByte:
                case DataType.Short:
                case DataType.UShort:
                case DataType.Int:
                case DataType.UInt:
                case DataType.Long:
                case DataType.ULong:
                    sb.Append(EmbeddedResources.Numeric.Integer.Base);
                    sb.Append(EmbeddedResources.Numeric.JsonConverter);
                    sb.Append(EmbeddedResources.Numeric.TypeConverter);
                    break;
                case DataType.Single:
                    sb.Append(EmbeddedResources.Numeric.FloatingPoint.Base);
                    sb.Append(EmbeddedResources.Numeric.FloatingPoint.SinglePreMatchCheckMethod);
                    sb.Append(EmbeddedResources.Numeric.JsonConverter);
                    sb.Append(EmbeddedResources.Numeric.TypeConverter);
                    break;
                case DataType.Double:
                    sb.Append(EmbeddedResources.Numeric.FloatingPoint.Base);
                    sb.Append(EmbeddedResources.Numeric.FloatingPoint.DoublePreMatchCheckMethod);
                    sb.Append(EmbeddedResources.Numeric.JsonConverter);
                    sb.Append(EmbeddedResources.Numeric.TypeConverter);
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
            if (recordStruct.ImplementIValidatableObject)
            {
                sb.Append(EmbeddedResources.ValidateMethod);
            }

            // Add closing brace
            sb.AppendLine("}");

            // Replace variable names with values
            sb.Replace("PRIMITIVE_TYPE", recordStruct.Name);
            sb.Replace("PRIMITIVE_INTERFACE", recordStruct.Interface);
            sb.Replace("PRIMITIVE_VALUE_TYPE", recordStruct.Type);
            sb.Replace("PRIMITIVE_INFO_TYPE", recordStruct.InfoType);
            sb.Replace("PRIMITIVE_DATA_TYPE", recordStruct.DataType.ToString());
            sb.Replace("PRIMITIVE_IVALIDATABLEOBJECT", recordStruct.ImplementIValidatableObject ? $", global::System.ComponentModel.DataAnnotations.IValidatableObject" : string.Empty);
            sb.Replace("PRIMITIVE_PATTERN", recordStruct.Pattern);
            sb.Replace("PRIMITIVE_EXAMPLE", recordStruct.Example);
            sb.Replace("PRIMITIVE_FORMAT", recordStruct.Format);
            sb.Replace("PRIMITIVE_LENGTH", recordStruct.Length.ToString());
            sb.Replace("PRIMITIVE_MINLENGTH", recordStruct.MinLength.ToString());
            sb.Replace("PRIMITIVE_MAXLENGTH", recordStruct.MaxLength.ToString());
            sb.Replace("PRIMITIVE_JSON_READER_METHOD", recordStruct.JsonReaderMethod);

            switch (recordStruct.DataType)
            {
                case DataType.Byte:
                case DataType.SByte:
                case DataType.Short:
                case DataType.UShort:
                case DataType.Int:
                case DataType.UInt:
                case DataType.Long:
                case DataType.ULong:
                    {
                        sb.Replace("PRIMITIVE_MINIMUM", recordStruct.Minimum.ToString());
                        sb.Replace("PRIMITIVE_MAXIMUM", recordStruct.Maximum.ToString());
                        break;
                    }
                case DataType.Single:
                    {
                        sb.Replace("PRIMITIVE_MINIMUM", GetMinimum(Convert.ToSingle(recordStruct.Minimum)));
                        sb.Replace("PRIMITIVE_MAXIMUM", GetMaximum(Convert.ToSingle(recordStruct.Maximum)));
                        sb.Replace("PRIMITIVE_ROUNDINGDIGITS", recordStruct.Digits?.ToString() ?? MetaData.Numeric.Single.Digits.ToString());
                        sb.Replace("PRIMITIVE_MIDPOINTROUNDINGMODE", recordStruct.Mode?.ToString() ?? MetaData.Numeric.Single.Mode.ToString());
                        break;
                    }
                case DataType.Double:
                    {
                        sb.Replace("PRIMITIVE_MINIMUM", GetMinimum(Convert.ToDouble(recordStruct.Minimum)));
                        sb.Replace("PRIMITIVE_MAXIMUM", GetMaximum(Convert.ToDouble(recordStruct.Maximum)));
                        sb.Replace("PRIMITIVE_ROUNDINGDIGITS", recordStruct.Digits?.ToString() ?? MetaData.Numeric.Double.Digits.ToString());
                        sb.Replace("PRIMITIVE_MIDPOINTROUNDINGMODE", recordStruct.Mode?.ToString() ?? MetaData.Numeric.Double.Mode.ToString());
                        break;
                    }
                default:
                    break;
            }

            // Construct source file text from string
            context.AddSource($"{recordStruct.NameSpace}.{recordStruct.Name}.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
        }
    }

    /// <summary>
    /// Generates a repository file.
    /// </summary>
    /// <param name="context">The source production context.</param>
    /// <param name="recordStructs">The record structs.</param>
    /// <param name="compilation">The compilation.</param>
    private static void GenerateRepositoryFile(SourceProductionContext context, List<RecordStructData> recordStructs, Compilation compilation)
    {
        const string Padding = "        ";

        var dataTypes = recordStructs.Select(rs => rs.DataType).Distinct();
        var yieldStatements = new List<string>();

        if (!dataTypes.Any())
        {
            yieldStatements.Add($"{Padding}return global::System.Linq.Enumerable.Empty<global::Primitively.PrimitiveInfo>();");
        }
        else
        {
            // Generate a list of yield return statements
            foreach (var dataType in dataTypes.OrderBy(d => d))
            {
                var items = recordStructs
                    .Where(rs => rs.DataType == dataType)
                    .OrderBy(rs => rs.Name)
                    .Select(rs => $"{Padding}yield return {rs.NameSpace}.{rs.Name}.TypeInfo;");

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
        sb.Replace("PRIMITIVE_REPOSITORY_YIELD_STATEMENTS", string.Join("\r\n", yieldStatements));

        // Construct source file text from string
        context.AddSource("PrimitiveRepository.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
    }

    private static string GetMaximum(double value) => value == double.MaxValue ? "double.MaxValue" : $"{value}d";

    private static string GetMaximum(float value) => value == float.MaxValue ? "float.MaxValue" : $"{value}f";

    private static string GetMinimum(double value) => value == double.MinValue ? "double.MinValue" : $"{value}d";

    private static string GetMinimum(float value) => value == float.MinValue ? "float.MinValue" : $"{value}f";

    /// <summary>
    /// Gets the target syntax.
    /// </summary>
    /// <param name="context">The initialization context.</param>
    /// <returns>The target syntax.</returns>
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
}
