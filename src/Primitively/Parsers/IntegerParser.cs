using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Primitively.Parsers;

internal class IntegerParser
{
    internal static bool TryParse(AttributeData attributeData, string name, string nameSpace, ParentData? parentData, DataType dataType, out RecordStructData? recordStructData)
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

        recordStructData = InitRecordStructData(dataType, new RecordStructData(dataType, name, nameSpace, parentData));

        return TryParseNamedArguments(attributeData, recordStructData);
    }

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
            }
        }

        return true;
    }

    private static RecordStructData InitRecordStructData(DataType dataType, RecordStructData recordStructData)
    {
        switch (dataType)
        {
            case DataType.Byte:
                recordStructData.Interface = MetaData.Integer.Byte.Interface;
                recordStructData.Type = MetaData.Integer.Byte.Type;
                recordStructData.Example = MetaData.Integer.Byte.Example;
                recordStructData.JsonReaderMethod = MetaData.Integer.Byte.JsonReaderMethod;
                break;
            case DataType.SByte:
                recordStructData.Interface = MetaData.Integer.SByte.Interface;
                recordStructData.Type = MetaData.Integer.SByte.Type;
                recordStructData.Example = MetaData.Integer.SByte.Example;
                recordStructData.JsonReaderMethod = MetaData.Integer.SByte.JsonReaderMethod;
                break;
            case DataType.Short:
                recordStructData.Interface = MetaData.Integer.Short.Interface;
                recordStructData.Type = MetaData.Integer.Short.Type;
                recordStructData.Example = MetaData.Integer.Short.Example;
                recordStructData.JsonReaderMethod = MetaData.Integer.Short.JsonReaderMethod;
                break;
            case DataType.UShort:
                recordStructData.Interface = MetaData.Integer.UShort.Interface;
                recordStructData.Type = MetaData.Integer.UShort.Type;
                recordStructData.Example = MetaData.Integer.UShort.Example;
                recordStructData.JsonReaderMethod = MetaData.Integer.UShort.JsonReaderMethod;
                break;
            case DataType.Int:
                recordStructData.Interface = MetaData.Integer.Int.Interface;
                recordStructData.Type = MetaData.Integer.Int.Type;
                recordStructData.Example = MetaData.Integer.Int.Example;
                recordStructData.JsonReaderMethod = MetaData.Integer.Int.JsonReaderMethod;
                break;
            case DataType.UInt:
                recordStructData.Interface = MetaData.Integer.UInt.Interface;
                recordStructData.Type = MetaData.Integer.UInt.Type;
                recordStructData.Example = MetaData.Integer.UInt.Example;
                recordStructData.JsonReaderMethod = MetaData.Integer.UInt.JsonReaderMethod;
                break;
            case DataType.Long:
                recordStructData.Interface = MetaData.Integer.Long.Interface;
                recordStructData.Type = MetaData.Integer.Long.Type;
                recordStructData.Example = MetaData.Integer.Long.Example;
                recordStructData.JsonReaderMethod = MetaData.Integer.Long.JsonReaderMethod;
                break;
            case DataType.ULong:
                recordStructData.Interface = MetaData.Integer.ULong.Interface;
                recordStructData.Type = MetaData.Integer.ULong.Type;
                recordStructData.Example = MetaData.Integer.ULong.Example;
                recordStructData.JsonReaderMethod = MetaData.Integer.ULong.JsonReaderMethod;
                break;
            default:
                throw new NotSupportedException($"{dataType} is not supported");
        }

        return recordStructData;
    }
}
