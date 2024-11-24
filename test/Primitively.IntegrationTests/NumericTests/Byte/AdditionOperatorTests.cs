using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.NumericTests.Byte;

public class AdditionOperatorTests
{
    [Fact]
    public void Should_Add_Two_Values()
    {
        // Assign
        var value1 = new ByteId(1);
        var value2 = new ByteId(2);

        // Act
        var result = value1 + value2;

        // Assert
        result.Should().BeEquivalentTo(new ByteId(3));
        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public void Should_Add_Value_To_Zero()
    {
        // Assign
        var value1 = new ByteId(0);
        var value2 = new ByteId(2);

        // Act
        var result = value1 + value2;

        // Assert
        result.Should().BeEquivalentTo(new ByteId(2));
        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public void Should_Add_Zero_To_Value()
    {
        // Assign
        var value1 = new ByteId(2);
        var value2 = new ByteId(0);

        // Act
        var result = value1 + value2;

        // Assert
        result.Should().BeEquivalentTo(new ByteId(2));
        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public void Should_Add_Zero_To_Zero()
    {
        // Assign
        var value1 = new ByteId(0);
        var value2 = new ByteId(0);

        // Act
        var result = value1 + value2;

        // Assert
        result.Should().BeEquivalentTo(new ByteId(0));
        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public void Should_Not_Add_More_Than_Maximum()
    {
        // Assign
        var value1 = new ByteId(ByteId.Maximum);
        var value2 = new ByteId(1);

        // Act
        var result = value1 + value2;

        // Assert
        result.Should().BeEquivalentTo(new ByteId(0));
        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public void Should_Not_Add_More_Than_Maximum2()
    {
        // Assign
        byte value1 = byte.MaxValue;
        byte value2 = 1;

        // Act
        var result = (byte)(value1 + value2);

        // Assert
        result.Should().Be(byte.MinValue); // Max exceeded
    }

    [Fact]
    public void Should_Not_Add_More_Than_Maximum21()
    {
        // Assign
        byte value1 = byte.MinValue;
        byte value2 = 1;

        // Act
        var result = (byte)(value1 - value2);

        // Assert
        result.Should().Be(byte.MaxValue); // Min exceeded
    }

    [Fact]
    public void Should_Not_Add_More_Than_Maximum22()
    {
        // Assign
        byte value1 = byte.MaxValue;
        byte value2 = 2;

        // Act
        var result = (byte)(value1 * value2);

        // Assert
        result.Should().Be(byte.MaxValue - 1); // Max exceeded
    }

    [Fact]
    public void Should_Not_Add_More_Than_Maximum23()
    {
        // Assign
        byte value1 = 1;
        byte value2 = 200;

        // Act
        var result = (value1 / value2);

        // Assert
        result.Should().Be(byte.MinValue); // Min
    }

    [Fact]
    public void Should_Not_Add_More_Than_Maximum3()
    {
        // Assign
        int value1 = int.MaxValue;
        int value2 = 1;

        // Act
        var result = value1 + value2;

        // Assert
        result.Should().Be(int.MinValue);
    }

    [Fact]
    public void Should_Not_Add_More_Than_Maximum4()
    {
        // Assign
        int value1 = int.MinValue;
        int value2 = 1;

        // Act
        var result = value1 - value2;

        // Assert
        result.Should().Be(int.MaxValue);
    }

    [Fact]
    public void Should_Not_Add_More_Than_Maximum5()
    {
        // Assign
        long value1 = long.MaxValue;
        long value2 = 1;

        // Act
        var result = value1 + value2;

        // Assert
        result.Should().Be(long.MinValue);
    }

    [Fact]
    public void Should_Not_Add_More_Than_Maximum6()
    {
        // Assign
        long value1 = long.MinValue;
        long value2 = 1;

        // Act
        var result = value1 - value2;

        // Assert
        result.Should().Be(long.MaxValue);
    }

    [Fact]
    public void Should_Not_Add_More_Than_Maximum7()
    {
        // Assign
        ulong value1 = ulong.MaxValue;
        ulong value2 = 1;

        // Act
        var result = value1 + value2;

        // Assert
        result.Should().Be(ulong.MinValue);
    }
}
