using System.Globalization;

namespace Uuid.Core;

/// <summary>
/// Parses a string of integers separated by single spaces into a sequence of 32-bit values.
/// </summary>
public sealed class IntegerSequenceParser
{
    /// <summary>
    /// Parses <paramref name="input"/> into integers in left-to-right order.
    /// </summary>
    /// <param name="input">A string of integers separated by a single ASCII space, or an empty string.</param>
    /// <returns>The parsed integers. An empty string yields an empty list.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/>.</exception>
    /// <exception cref="FormatException">
    /// <paramref name="input"/> contains an empty token or a token that is not a valid integer.
    /// </exception>
    /// <exception cref="OverflowException">A token is outside the range of a 32-bit signed integer.</exception>
    public IReadOnlyList<int> Parse(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        input = input.Trim();
        if (input.Length == 0)
        {
            return [];
        }

        var tokens = input.Split(' ');
        var values = new int[tokens.Length];

        for (var i = 0; i < tokens.Length; i++)
        {
            var token = tokens[i];
            if (token.Length == 0)
            {
                throw new FormatException(
                    "Input contains an empty token; values must be separated by a single space.");
            }

            try
            {
                values[i] = int.Parse(token, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
            }
            catch (FormatException ex)
            {
                throw new FormatException($"Token '{token}' is not a valid 32-bit integer.", ex);
            }
            catch (OverflowException ex)
            {
                throw new OverflowException($"Token '{token}' is outside the range of a 32-bit integer.", ex);
            }
        }

        return values;
    }
}
