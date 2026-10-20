using Uuid.Core;

namespace Uuid.Tests;

public sealed class LongestIncreasingSubsequenceResultTests
{
    [Fact]
    public void Constructor_MatchingLists_Succeeds()
    {
        var result = new LongestIncreasingSubsequenceResult([10, 20], [1, 4]);

        Assert.Equal([10, 20], result.Values);
        Assert.Equal([1, 4], result.Indices);
    }

    [Fact]
    public void Constructor_InvalidArguments_Throw()
    {
        Assert.Throws<ArgumentNullException>(() => new LongestIncreasingSubsequenceResult(null!, [0]));
        Assert.Throws<ArgumentException>(() => new LongestIncreasingSubsequenceResult([1, 2], [0]));
    }
}
