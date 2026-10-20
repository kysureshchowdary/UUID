namespace Uuid.Core;

/// <summary>
/// The values and original indices of a longest strictly increasing subsequence.
/// </summary>
public sealed class LongestIncreasingSubsequenceResult
{
    /// <summary>
    /// Initializes a new result with matching value and index sequences.
    /// </summary>
    /// <param name="values">Subsequence values in the order they appear in the source.</param>
    /// <param name="indices">Zero-based source indices of those values.</param>
    /// <exception cref="ArgumentNullException"><paramref name="values"/> or <paramref name="indices"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">The two sequences have different lengths.</exception>
    public LongestIncreasingSubsequenceResult(IReadOnlyList<int> values, IReadOnlyList<int> indices)
    {
        ArgumentNullException.ThrowIfNull(values);
        ArgumentNullException.ThrowIfNull(indices);

        if (values.Count != indices.Count)
        {
            throw new ArgumentException("Values and indices must have the same length.");
        }

        Values = values;
        Indices = indices;
    }

    /// <summary>
    /// Gets the subsequence values in source order.
    /// </summary>
    public IReadOnlyList<int> Values { get; }

    /// <summary>
    /// Gets the zero-based source indices of <see cref="Values"/>.
    /// </summary>
    public IReadOnlyList<int> Indices { get; }
}
