namespace Uuid.Core;

/// <summary>
/// Finds a longest strictly increasing subsequence, breaking ties by earliest indices.
/// </summary>
public sealed class LongestStrictlyIncreasingSubsequenceFinder
{
    /// <summary>
    /// Returns a longest strictly increasing subsequence of <paramref name="values"/>.
    /// </summary>
    /// <param name="values">The source sequence.</param>
    /// <returns>
    /// A subsequence of maximum length. If several exist, the one whose index tuple is
    /// lexicographically smallest is returned.
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

        var lengthStartingAt = ComputeLengthsStartingAtEachIndex(values);
        var startIndex = IndexOfFirstMaximum(lengthStartingAt);
        var indices = ReconstructEarliestSequence(values, lengthStartingAt, startIndex);

        var subsequenceValues = new int[indices.Count];
        for (var i = 0; i < indices.Count; i++)
        {
            subsequenceValues[i] = values[indices[i]];
        }

        return new LongestIncreasingSubsequenceResult(subsequenceValues, indices);
    }

    private static int[] ComputeLengthsStartingAtEachIndex(IReadOnlyList<int> values)
    {
        var count = values.Count;
        var lengthStartingAt = new int[count];

        for (var i = count - 1; i >= 0; i--)
        {
            var bestLength = 1;
            for (var j = i + 1; j < count; j++)
            {
                if (values[j] > values[i] && 1 + lengthStartingAt[j] > bestLength)
                {
                    bestLength = 1 + lengthStartingAt[j];
                }
            }

            lengthStartingAt[i] = bestLength;
        }

        return lengthStartingAt;
    }

    private static int IndexOfFirstMaximum(IReadOnlyList<int> lengthStartingAt)
    {
        var bestIndex = 0;
        var bestLength = lengthStartingAt[0];

        for (var i = 1; i < lengthStartingAt.Count; i++)
        {
            if (lengthStartingAt[i] > bestLength)
            {
                bestLength = lengthStartingAt[i];
                bestIndex = i;
            }
        }

        return bestIndex;
    }

    private static IReadOnlyList<int> ReconstructEarliestSequence(
        IReadOnlyList<int> values,
        IReadOnlyList<int> lengthStartingAt,
        int startIndex)
    {
        var remainingLength = lengthStartingAt[startIndex];
        var indices = new int[remainingLength];
        var currentIndex = startIndex;
        indices[0] = currentIndex;
        remainingLength--;

        var written = 1;
        while (remainingLength > 0)
        {
            var nextIndex = FindEarliestValidNext(values, lengthStartingAt, currentIndex, remainingLength);
            indices[written] = nextIndex;
            written++;
            currentIndex = nextIndex;
            remainingLength--;
        }

        return indices;
    }

    private static int FindEarliestValidNext(
        IReadOnlyList<int> values,
        IReadOnlyList<int> lengthStartingAt,
        int currentIndex,
        int remainingLength)
    {
        for (var j = currentIndex + 1; j < values.Count; j++)
        {
            if (values[j] > values[currentIndex] && lengthStartingAt[j] == remainingLength)
            {
                return j;
            }
        }

        throw new InvalidOperationException(
            "Failed to reconstruct the subsequence; length table and values are inconsistent.");
    }
}
