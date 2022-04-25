    static void PreMatchCheck(ref string value) => value = value?.Replace(" ", string.Empty);

    static void PostMatchCheck(ref string value)
    {
        if (value is null || PassesCheckSumTest(value.ToCharArray()))
        {
            return;
        }

        value = default;
    }

    public string FormattedValue => HasValue ? FormatValue(Value) : default;

    // Format the number correctly '123 123 1234' if valid
    private static string FormatValue(string value) => $"{value[..3]} {value.Substring(3, 3)} {value[6..]}";

    // Check that they are not all the same numbers because
    // this may pass the checksum test but they are not valid
    private static bool PassesCheckSumTest(System.Collections.Generic.IReadOnlyList<char> digits) => !AreConstantNumbers(digits) && CheckSumTest(digits);

    private static bool AreConstantNumbers(System.Collections.Generic.IReadOnlyList<char> digits)
    {
        // Are they constant numbers.
        var firstNo = digits[0];

        for (var i = 1; i < digits.Count; i++)
        {
            if (firstNo != digits[i])
            {
                return false;
            }
        }

        return true;
    }

    private static bool CheckSumTest(System.Collections.Generic.IReadOnlyList<char> digits)
    {
        const short CheckSumBase = 11;
        const short InvalidCheckSum = 10;

        // Only use the first 9 values for checksum calculation
        var checksum = System.Convert.ToInt32(digits[9].ToString());
        var digitTotals = new int[9];
        var factors = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        for (var i = 0; i < digits.Count - 1; i++)
        {
            digitTotals[i] = System.Convert.ToInt32(digits[i].ToString()) * factors[i];
        }

        var computedCheckSum = CheckSumBase - (System.Linq.Enumerable.Sum(digitTotals) % 11);
        computedCheckSum = computedCheckSum == CheckSumBase ? 0 : computedCheckSum;

        if (computedCheckSum == InvalidCheckSum)
        {
            //?? What do we do??? Guidance is not clear. for now we'll throw them out.
            return false;
        }

        return checksum == computedCheckSum;
    }
