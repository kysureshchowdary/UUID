namespace Uuid.Core;

/// <summary>
/// Parses input, finds a longest strictly increasing subsequence, and formats the result.
/// </summary>
public sealed class LongestIncreasingSubsequenceService
{
    private readonly IntegerSequenceParser _parser;
    private readonly LongestStrictlyIncreasingSubsequenceFinder _finder;
    private readonly SubsequenceFormatter _formatter;

    /// <summary>
    /// Initializes a new service from its parse, find, and format collaborators.
    /// </summary>
    /// <param name="parser">Parses the input string.</param>
    /// <param name="finder">Finds the subsequence.</param>
    /// <param name="formatter">Formats subsequence values for output.</param>
    /// <exception cref="ArgumentNullException">Any collaborator is <see langword="null"/>.</exception>
    public LongestIncreasingSubsequenceService(
        IntegerSequenceParser parser,
        LongestStrictlyIncreasingSubsequenceFinder finder,
        SubsequenceFormatter formatter)
    {
        _parser = parser ?? throw new ArgumentNullException(nameof(parser));
        _finder = finder ?? throw new ArgumentNullException(nameof(finder));
        _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
    }

    /// <summary>
    /// Returns the longest strictly increasing subsequence of <paramref name="input"/> as a space-separated string.
    /// </summary>
    /// <param name="input">A string of integers separated by a single space, or an empty string.</param>
    /// <returns>The formatted subsequence, or an empty string when <paramref name="input"/> is empty.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/>.</exception>
    /// <exception cref="FormatException"><paramref name="input"/> is not a valid integer sequence.</exception>
    /// <exception cref="OverflowException">A token is outside the range of a 32-bit signed integer.</exception>
    public string Solve(string input)
    {
        var values = _parser.Parse(input);
        var result = _finder.Find(values);
        return _formatter.Format(result.Values);
    }
}
