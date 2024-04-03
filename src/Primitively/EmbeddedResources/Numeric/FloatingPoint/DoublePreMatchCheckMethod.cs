
    private static void PreMatchCheck(ref global::PRIMITIVE_VALUE_TYPE value)
    {
        if (Digits >= 0)
        {
            value = global::System.Math.Round(value, Digits, Mode);
        }
    }
