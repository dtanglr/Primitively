using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Primitively.Parsers;

/// <summary>
/// Provides methods for parsing DateOnly attribute data.
/// </summary>
internal static class DateOnlyParser
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

        recordStructData = new RecordStructData(DataType.DateOnly, name, nameSpace, parentData)
        {
            Length = MetaData.DateOnly.Iso8601.Length,
            Example = MetaData.DateOnly.Iso8601.Example,
            Format = MetaData.DateOnly.Iso8601.Format
        };

        return TryParseNamedArguments(attributeData, recordStructData);
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
                case nameof(DateOnlyAttribute.ImplementIValidatableObject):
                    recordStructData.ImplementIValidatableObject = (bool?)value ?? false;
                    break;
                default:
                    break;
            }
        }

        return true;
    }
}
