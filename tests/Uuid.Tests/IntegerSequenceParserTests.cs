using Uuid.Core;

namespace Uuid.Tests;

public sealed class IntegerSequenceParserTests
{
    private readonly IntegerSequenceParser _parser = new();

    [Fact]
    public void Parse_ValidNumbers_ReturnsIntegers()
    {
        Assert.Equal([-2, 0, 3], _parser.Parse(" -2 0 3 "));
    }

    [Fact]
    public void Parse_Empty_ReturnsEmptyList()
    {
        Assert.Empty(_parser.Parse(""));
    }

    [Fact]
    public void Parse_Null_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _parser.Parse(null!));
    }

    [Fact]
    public void Parse_InvalidToken_ThrowsFormatException()
    {
        Assert.Throws<FormatException>(() => _parser.Parse("1 x 3"));
    }

    [Fact]
    public void Parse_Overflow_ThrowsOverflowException()
    {
        Assert.Throws<OverflowException>(() => _parser.Parse("2147483648"));
    }
}
