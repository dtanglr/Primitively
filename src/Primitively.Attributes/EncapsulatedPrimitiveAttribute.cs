using System;

namespace Primitively
{
    [AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    [System.Diagnostics.Conditional("PRIMITIVELY_USAGES")]
    public sealed class EncapsulatedPrimitiveAttribute : Attribute
    {
        /// <summary>
        ///     Make a readonly record struct that encapsulates a primitive
        /// </summary>
        public EncapsulatedPrimitiveAttribute()
        {
        }
    }
}
