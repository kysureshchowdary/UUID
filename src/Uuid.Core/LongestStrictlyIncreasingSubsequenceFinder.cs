namespace Uuid.Core;

/// <summary>
/// Finds a longest strictly increasing contiguous run, breaking ties by earliest start index.
/// </summary>
public sealed class LongestStrictlyIncreasingSubsequenceFinder
{
    /// <summary>
    /// Returns a longest strictly increasing contiguous subsequence of <paramref name="values"/>.
    /// </summary>
    /// <param name="values">The source sequence.</param>
    /// <returns>
    /// A contiguous run of maximum length. If several exist, the earliest one is returned.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
    public LongestIncreasingSubsequenceResult Find(IReadOnlyList<int> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        var count = values.Count;
        if (count == 0)
        {
            return new LongestIncreasingSubsequenceResult([], []);
        }

        var bestStart = 0;
        var bestLength = 1;
        var runStart = 0;

        for (var i = 1; i < count; i++)
        {
            if (values[i] > values[i - 1])
            {
                var runLength = i - runStart + 1;
                if (runLength > bestLength)
                {
                    bestLength = runLength;
                    bestStart = runStart;
                }
            }
            else
            {
                runStart = i;
            }
        }

        var subsequenceValues = new int[bestLength];
        var indices = new int[bestLength];
        for (var offset = 0; offset < bestLength; offset++)
        {
            var index = bestStart + offset;
            indices[offset] = index;
            subsequenceValues[offset] = values[index];
        }

        return new LongestIncreasingSubsequenceResult(subsequenceValues, indices);
    }
}
