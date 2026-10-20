namespace Uuid.Core;

/// <summary>
/// Formats a sequence of integers as a single-space-separated string.
/// </summary>
public sealed class SubsequenceFormatter
{
    /// <summary>
    /// Joins <paramref name="values"/> with a single ASCII space.
    /// </summary>
    /// <param name="values">The values to format.</param>
    /// <returns>The joined string, or an empty string when <paramref name="values"/> is empty.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
    public string Format(IReadOnlyList<int> values)
    {
        ArgumentNullException.ThrowIfNull(values);
        return string.Join(' ', values);
    }
}
