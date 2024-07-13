using Primitively;

namespace Acme.TestLib;

[Byte]
public partial record struct ByteId;

[SByte]
public partial record struct SByteId;

[Short]
public partial record struct ShortId;

[UShort]
public partial record struct UShortId;

[Int]
public partial record struct IntId;

[UInt]
public partial record struct UIntId;

[Long]
public partial record struct LongId;

[ULong]
public partial record struct ULongId;

[Single]
public partial record struct SingleId;

[Double]
public partial record struct DoubleId;

[Decimal]
public partial record struct DecimalId;

[Int(ImplementIValidatableObject = true)]
public partial record struct ValidatableInteger;

[Int(Minimum = 100)]
public partial record struct MinimumOf100;

[Int(Minimum = 100, Maximum = 200)]
public partial record struct MinimumOf100AndMaximumOf200;

[Byte(Maximum = 10)]
public partial record struct MaximumOf10;

[Single(4, Minimum = -10.12345f, Maximum = 100.54321f)]
public partial record struct MinimumOfMinus10FAndMaximumOf100F;

[Double(4, Minimum = -10.12345d, Maximum = 100.54321d)]
public partial record struct MinimumOfMinus10DAndMaximumOf100D;
