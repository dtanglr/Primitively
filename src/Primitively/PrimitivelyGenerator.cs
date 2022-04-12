using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Primitively;

/// <inheritdoc />
[Generator]
public class PrimitivelyGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Register the abstractions sources
        context.RegisterPostInitializationOutput(i =>
        {
            foreach (var resource in EmbeddedResources.Abstractions.GetEmbeddedResources())
            {
                i.AddSource($"{resource.Key}.g.cs", resource.Value);
            }
        });
    }
}
