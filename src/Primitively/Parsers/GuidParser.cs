using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Primitively.Parsers;

internal class GuidParser
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

        recordStructData = new RecordStructData(DataType.Guid, name, nameSpace, parentData);

        return TryParseConstructorArguments(attributeData, recordStructData);
    }

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
}
