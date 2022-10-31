using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using Moq;
using Primitively.AspNetCore;
using Xunit;

namespace Primitively.IntegrationTests;

public class PrimitiveModelBinderTests
{
    private static readonly IEnumerable<Type> _types = Assembly
        .GetExecutingAssembly()
        .GetTypes()
        .Where(t => t.IsValueType && t.IsAssignableTo(typeof(IPrimitive)));

    public static IEnumerable<object[]> PrimitiveTypes()
    {
        foreach (var type in _types)
        {
            yield return new object[] { type };
        }
    }

    [Fact]
    public void BindModelAsync_TerminatesBeforeSettingModelValue_WhenValueProviderResultIsNone()
    {
        var valueProvider = new Mock<IValueProvider>();
        valueProvider.Setup(provider => provider.GetValue(It.IsAny<string>())).Returns(ValueProviderResult.None);

        var bindingContext = new Mock<ModelBindingContext>();
        bindingContext.Setup(context => context.ModelName).Returns("A");
        bindingContext.Setup(context => context.ValueProvider).Returns(valueProvider.Object);
        bindingContext.Setup(context => context.ModelState).Returns(new ModelStateDictionary());

        var factory = new PrimitiveFactory();
        var binder = new PrimitiveModelBinder(factory);
        var result = binder.BindModelAsync(bindingContext.Object);
        result.Should().Be(Task.CompletedTask);

        bindingContext.VerifySet(context => context.Result = It.IsAny<ModelBindingResult>(), Times.Never);
    }

    [Theory]
    [MemberData(nameof(PrimitiveTypes))]
    public void BindModelAsync_ReportsSuccessfulBinding_WhenValueProviderResultIsEmptyString(Type primitiveType)
    {
        var valueProvider = new Mock<IValueProvider>();
        valueProvider.Setup(provider => provider.GetValue(It.IsAny<string>())).Returns(new ValueProviderResult(new StringValues("")));

        var bindingContext = new Mock<ModelBindingContext>();
        bindingContext.Setup(context => context.ModelName).Returns("A");
        bindingContext.Setup(context => context.ModelType).Returns(primitiveType);
        bindingContext.Setup(context => context.ValueProvider).Returns(valueProvider.Object);
        bindingContext.Setup(context => context.ModelState).Returns(new ModelStateDictionary());

        var factory = new PrimitiveFactory();
        var binder = new PrimitiveModelBinder(factory);
        var result = binder.BindModelAsync(bindingContext.Object);
        result.Should().Be(Task.CompletedTask);

        bindingContext.VerifySet(context => context.Result = It.IsAny<ModelBindingResult>(), Times.Once);
    }

    [Theory]
    [MemberData(nameof(PrimitiveTypes))]
    public void BindModelAsync_ReportsSuccessfulBinding_WhenValueProviderResultIsValueString(Type primitiveType)
    {
        var valueProvider = new Mock<IValueProvider>();
        valueProvider.Setup(provider => provider.GetValue(It.IsAny<string>())).Returns(new ValueProviderResult(new StringValues("value")));

        var bindingContext = new Mock<ModelBindingContext>();
        bindingContext.Setup(context => context.ModelName).Returns("A");
        bindingContext.Setup(context => context.ModelType).Returns(primitiveType);
        bindingContext.Setup(context => context.ValueProvider).Returns(valueProvider.Object);
        bindingContext.Setup(context => context.ModelState).Returns(new ModelStateDictionary());

        var factory = new PrimitiveFactory();
        var binder = new PrimitiveModelBinder(factory);
        var result = binder.BindModelAsync(bindingContext.Object);
        result.Should().Be(Task.CompletedTask);

        bindingContext.VerifySet(context => context.Result = It.IsAny<ModelBindingResult>(), Times.Once);
    }

    [Theory]
    [MemberData(nameof(PrimitiveTypes))]
    public void GetBinder_ReturnsBinder_WhenOfCorrectType(Type primitiveType)
    {
        var factory = new PrimitiveFactory();
        var provider = new PrimitiveModelBinderProvider(factory);
        var context = new Mock<ModelBinderProviderContext>();
        var metadataProvider = new EmptyModelMetadataProvider();
        var metadata = metadataProvider.GetMetadataForType(primitiveType);

        context.Setup(item => item.Metadata).Returns(metadata);

        var binder = provider.GetBinder(context.Object);
        binder.Should().NotBeNull();
        binder.Should().BeAssignableTo<PrimitiveModelBinder>();
    }

    [Fact]
    public void GetBinder_ReturnsNull_WhenOfIncorrectType()
    {
        var factory = new PrimitiveFactory();
        var provider = new PrimitiveModelBinderProvider(factory);
        var context = new Mock<ModelBinderProviderContext>();
        var metadataProvider = new EmptyModelMetadataProvider();
        var metadata = metadataProvider.GetMetadataForType(typeof(DateTime));

        context.Setup(item => item.Metadata).Returns(metadata);

        var binder = provider.GetBinder(context.Object);

        binder.Should().BeNull();
    }
}
