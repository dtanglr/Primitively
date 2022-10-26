using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Moq;
using Xunit;

namespace Primitively.IntegrationTests.Types;

public abstract class TypeConverterTests<TTypeConverter, TPrimitive>
   where TTypeConverter : TypeConverter, new()
   where TPrimitive : struct, IPrimitive
{
    private static readonly List<Type> _types = Assembly
       .GetExecutingAssembly()
       .GetTypes()
       .Where(type => type.GetConstructor(Type.EmptyTypes) != null && !type.IsValueType)
       .ToList();

    [Fact]
    public void TypeConverter_CanConvertFrom()
    {
        var converter = new TTypeConverter();
        converter.Should().NotBeNull();

        // Should convert from string
        converter.CanConvertFrom(new Mock<ITypeDescriptorContext>().Object, typeof(string)).Should().BeTrue();
        converter.ConvertFrom(string.Empty).Should().BeAssignableTo(typeof(TPrimitive));

        if (typeof(TPrimitive) is IDateOnly)
        {
            // Should convert from DateOnly
            converter.CanConvertFrom(new Mock<ITypeDescriptorContext>().Object, typeof(DateOnly)).Should().BeTrue();
            converter.ConvertFrom(default(DateOnly)).Should().BeAssignableTo(typeof(TPrimitive));
            converter.ConvertFrom(DateOnly.FromDateTime(DateTime.Now)).Should().BeAssignableTo(typeof(TPrimitive));

            // Should convert from DateOnly?
            converter.CanConvertFrom(new Mock<ITypeDescriptorContext>().Object, typeof(DateOnly?)).Should().BeTrue();
            converter.ConvertFrom((DateOnly?)DateOnly.FromDateTime(DateTime.Now)).Should().BeAssignableTo(typeof(TPrimitive));

            // Should convert from DateTime
            converter.CanConvertFrom(new Mock<ITypeDescriptorContext>().Object, typeof(DateTime)).Should().BeTrue();
            converter.ConvertFrom(default(DateTime)).Should().BeAssignableTo(typeof(TPrimitive));
            converter.ConvertFrom(DateTime.Now).Should().BeAssignableTo(typeof(TPrimitive));

            // Should convert from DateTime?
            converter.CanConvertFrom(new Mock<ITypeDescriptorContext>().Object, typeof(DateTime?)).Should().BeTrue();
            converter.ConvertFrom((DateTime?)DateTime.Now).Should().BeAssignableTo(typeof(TPrimitive));
        }
        else if (typeof(TPrimitive) is IGuid)
        {
            // Should convert from Guid
            converter.CanConvertFrom(new Mock<ITypeDescriptorContext>().Object, typeof(Guid)).Should().BeTrue();
            converter.ConvertFrom(default(Guid)).Should().BeAssignableTo(typeof(TPrimitive));
            converter.ConvertFrom(Guid.NewGuid()).Should().BeAssignableTo(typeof(TPrimitive));

            // Should convert from Guid?
            converter.CanConvertFrom(new Mock<ITypeDescriptorContext>().Object, typeof(Guid?)).Should().BeTrue();
            converter.ConvertFrom((Guid?)Guid.NewGuid()).Should().BeAssignableTo(typeof(TPrimitive));
        }
    }

    [Fact]
    public void TypeConverter_CannotConvertFrom()
    {
        var converter = new TTypeConverter();
        converter.Should().NotBeNull();

        foreach (var type in _types)
        {
            converter.CanConvertFrom(new Mock<ITypeDescriptorContext>().Object, type).Should().BeFalse();
        }
    }
}
