using System;
using Microsoft.CodeAnalysis;

namespace Primitively.Parsers;

internal class DateOnlyParser
{
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

        return true;
    }
}
