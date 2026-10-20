using Uuid.Core;

namespace Uuid.Tests;

public sealed class LongestIncreasingSubsequenceServiceTests
{
    private readonly LongestIncreasingSubsequenceService _service = new(
        new IntegerSequenceParser(),
        new LongestStrictlyIncreasingSubsequenceFinder(),
        new SubsequenceFormatter());

    [Fact]
    public void Solve_ValidInput_ReturnsFormattedRun()
    {
        Assert.Equal("", _service.Solve(""));
        Assert.Equal("1 3", _service.Solve("1 3 2 4"));
        Assert.Equal(
            "1710 2461 9288 10195 10431 12485",
            _service.Solve("9 20000 1710 2461 9288 10195 10431 12485 12292"));
    }

    [Fact]
    public void Solve_InvalidInput_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => _service.Solve(null!));
        Assert.Throws<FormatException>(() => _service.Solve("1 x 3"));
        Assert.Throws<OverflowException>(() => _service.Solve("2147483648"));
    }
}
