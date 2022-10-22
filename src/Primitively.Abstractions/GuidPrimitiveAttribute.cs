using System;
using System.Diagnostics;

namespace Primitively;

/// <summary>
///     Make a readonly record struct that encapsulates a 
///     GUID primitive value with a fixed length of 36
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
[Conditional(Constants.ConditionalCompilationSymbol)]
public sealed class GuidPrimitiveAttribute : Attribute
{
}

/// <summary>
///     Make a readonly record struct that encapsulates a 
///     GUID primitive value with a fixed length of 36
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
[Conditional(Constants.ConditionalCompilationSymbol)]
public sealed class GuidAttribute : Attribute
{
}
