using Uuid.Core;

namespace Uuid.Tests;

public sealed class LongestStrictlyIncreasingSubsequenceFinderTests
{
    private readonly LongestStrictlyIncreasingSubsequenceFinder _finder = new();

    [Fact]
    public void Find_LongestContiguousRun_ReturnsThatRun()
    {
        var result = _finder.Find([9, 20000, 1710, 2461, 9288, 10195, 10431, 12485, 12292]);

        Assert.Equal([1710, 2461, 9288, 10195, 10431, 12485], result.Values);
        Assert.Equal([2, 3, 4, 5, 6, 7], result.Indices);
    }

    [Fact]
    public void Find_LengthTie_ChoosesEarliestRun()
    {
        var result = _finder.Find([1, 3, 2, 4]);

        Assert.Equal([1, 3], result.Values);
        Assert.Equal([0, 1], result.Indices);
    }

    [Fact]
    public void Find_EmptyOrNonIncreasing_ReturnsFirstOrEmpty()
    {
        Assert.Empty(_finder.Find([]).Values);
        Assert.Equal([5], _finder.Find([5, 4, 4]).Values);
    }

    [Fact]
    public void Find_Null_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _finder.Find(null!));
    }
}
