
    private static void PreMatchCheck(ref global::PRIMITIVE_VALUE_TYPE value)
    {
        if (Digits >= 0)
        {
#if NETCOREAPP2_0_OR_GREATER
            value = global::System.MathF.Round(value, Digits, Mode);
#else
            value = (float)global::System.Math.Round(value, Digits, Mode);
#endif
        }
    }
