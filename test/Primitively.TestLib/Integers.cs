namespace Primitively.TestLib;

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

[Int(ImplementIValidatableObject = true)]
public partial record struct ValidatableInteger;

[Int(Minimum = 100)]
public partial record struct MinimumOf100;

[Int(Minimum = 100, Maximum = 200)]
public partial record struct MinimumOf100AndMaximumOf200;

[Byte(Maximum = 10)]
public partial record struct MaximumOf10;
