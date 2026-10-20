using Uuid.Core;

namespace Uuid.Tests;

public sealed class SubsequenceFormatterTests
{
    private readonly SubsequenceFormatter _formatter = new();

    [Fact]
    public void Format_Values_JoinsWithSingleSpaces()
    {
        Assert.Equal("", _formatter.Format([]));
        Assert.Equal("-2 0 5", _formatter.Format([-2, 0, 5]));
    }

    [Fact]
    public void Format_Null_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _formatter.Format(null!));
    }
}
