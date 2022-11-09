﻿using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Primitively.Parsers;

internal class StringParser
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

        recordStructData = new RecordStructData(DataType.String, name, nameSpace, parentData);

        if (!TryParseConstructorArguments(attributeData, recordStructData))
        {
            return false;
        }

        return TryParseNamedArguments(attributeData, recordStructData);
    }

    private static bool TryParseConstructorArguments(AttributeData attributeData, RecordStructData recordStructData)
    {
        if (attributeData.ConstructorArguments.IsEmpty)
        {
            // 1 or 2 arguments are expected
            return false;
        }

        var args = attributeData.ConstructorArguments;

        if (args.Any(a => a.Kind == TypedConstantKind.Error))
        {
            return false;
        }

        switch (args.Length)
        {
            case 2:
                recordStructData.MaxLength = args[1].IsNull ? 0 : (int)args[1].Value!;
                recordStructData.MinLength = args[0].IsNull ? 0 : (int)args[0].Value!;
                return true;
            case 1:
                var length = args[0].IsNull ? 0 : (int)args[0].Value!;
                recordStructData.MaxLength = length;
                recordStructData.MinLength = length;
                return true;
            default:
                return false;
        }
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
                case nameof(StringAttribute.Example):
                    recordStructData.Example = (string?)value;
                    break;
                case nameof(StringAttribute.Format):
                    recordStructData.Format = (string?)value;
                    break;
                case nameof(StringAttribute.Pattern):
                    recordStructData.Pattern = (string?)value;
                    break;
                case nameof(StringAttribute.ImplementIValidatableObject):
                    recordStructData.ImplementIValidatableObject = (bool?)value ?? false;
                    break;
            }
        }

        return true;
    }
}
