using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Primitively.Parsers;

/// <summary>
/// Provides methods for parsing Guid attribute data.
/// </summary>
internal static class GuidParser
{
    /// <summary>
    /// Attempts to parse the specified attribute data into a record struct data.
    /// </summary>
    /// <param name="attributeData">The attribute data to parse.</param>
    /// <param name="name">The name of the record struct.</param>
    /// <param name="nameSpace">The namespace of the record struct.</param>
    /// <param name="parentData">The parent data of the record struct.</param>
    /// <param name="recordStructData">When this method returns, contains the parsed record struct data, if the conversion succeeded, or null if the conversion failed.</param>
    /// <returns>true if the attribute data was parsed successfully; otherwise, false.</returns>
    internal static bool TryParse(AttributeData attributeData, string name, string nameSpace, ParentData? parentData, out RecordStructData? recordStructData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
        }

        if (string.IsNullOrEmpty(nameSpace))
        {
            throw new ArgumentException($"'{nameof(nameSpace)}' cannot be null or empty.", nameof(nameSpace));
        }

        recordStructData = new RecordStructData(DataType.Guid, name, nameSpace, parentData)
        {
            Interface = MetaData.Guid.Interface,
            Type = MetaData.Guid.Type,
            InfoType = MetaData.Guid.InfoType
        };

        if (!TryParseConstructorArguments(attributeData, recordStructData))
        {
            return false;
        }

        return TryParseNamedArguments(attributeData, recordStructData);
    }

    /// <summary>
    /// Attempts to parse the constructor arguments of the specified attribute data into a record struct data.
    /// </summary>
    /// <param name="attributeData">The attribute data whose constructor arguments to parse.</param>
    /// <param name="recordStructData">The record struct data to populate with the parsed constructor arguments.</param>
    /// <returns>true if the constructor arguments were parsed successfully; otherwise, false.</returns>
    private static bool TryParseConstructorArguments(AttributeData attributeData, RecordStructData recordStructData)
    {
        if (attributeData.ConstructorArguments.IsEmpty)
        {
            recordStructData.Example = MetaData.Guid.D.Example;
            recordStructData.Format = MetaData.Guid.D.Format;
            recordStructData.Length = MetaData.Guid.D.Length;
            recordStructData.Specifier = Specifier.D;

            return true;
        }

        var args = attributeData.ConstructorArguments;

        if (args.Length > 1 || args.Any(a => a.Kind == TypedConstantKind.Error))
        {
            return false;
        }

        var specifier = (Specifier)args[0].Value!;

        switch (specifier)
        {
            case Specifier.N:
                recordStructData.Example = MetaData.Guid.N.Example;
                recordStructData.Format = MetaData.Guid.N.Format;
                recordStructData.Length = MetaData.Guid.N.Length;
                recordStructData.Specifier = Specifier.N;
                break;
            case Specifier.B:
                recordStructData.Example = MetaData.Guid.B.Example;
                recordStructData.Format = MetaData.Guid.B.Format;
                recordStructData.Length = MetaData.Guid.B.Length;
                recordStructData.Specifier = Specifier.B;
                break;
            case Specifier.P:
                recordStructData.Example = MetaData.Guid.P.Example;
                recordStructData.Format = MetaData.Guid.P.Format;
                recordStructData.Length = MetaData.Guid.P.Length;
                recordStructData.Specifier = Specifier.P;
                break;
            case Specifier.X:
                recordStructData.Example = MetaData.Guid.X.Example;
                recordStructData.Format = MetaData.Guid.X.Format;
                recordStructData.Length = MetaData.Guid.X.Length;
                recordStructData.Specifier = Specifier.X;
                break;
            default:
                recordStructData.Example = MetaData.Guid.D.Example;
                recordStructData.Format = MetaData.Guid.D.Format;
                recordStructData.Length = MetaData.Guid.D.Length;
                recordStructData.Specifier = Specifier.D;
                break;
        }

        return true;
    }

    /// <summary>
    /// Attempts to parse the named arguments of the specified attribute data into a record struct data.
    /// </summary>
    /// <param name="attributeData">The attribute data whose named arguments to parse.</param>
    /// <param name="recordStructData">The record struct data to populate with the parsed named arguments.</param>
    /// <returns>true if the named arguments were parsed successfully; otherwise, false.</returns>
    private static bool TryParseNamedArguments(AttributeData attributeData, RecordStructData recordStructData)
    {
        if (attributeData.NamedArguments.IsEmpty)
        {
            return true;
        }

        var args = attributeData.NamedArguments;

        if (args.Any(a => a.Value.Kind == TypedConstantKind.Error))
        {
            return false;
        }

        foreach (var arg in args)
        {
            var key = arg.Key;
            var value = arg.Value.Value;

            switch (key)
            {
                case nameof(GuidAttribute.ImplementIValidatableObject):
                    recordStructData.ImplementIValidatableObject = (bool?)value ?? false;
                    break;
                default:
                    break;
            }
        }

        return true;
    }
}
