using System.Text.RegularExpressions;

namespace Plex.NaturalStringOrdering;
/// <summary>
/// Defines a method that a string implements to compare two values according to their natural value (text and number).
/// This class implements the <c>IComparer&lt;T&gt;</c> interface with type string.
/// </summary>
/// <remarks>
/// Creates an instance of <c>NaturalStringComparer</c> for case comparison according to the value specified in input.
/// </remarks>
/// <param name="ignoreCase">true to ignore case during the comparison; otherwise, false.</param>
/// Ref: https://github.com/stefanotempesta/Space/tree/985c1c9d886e51719b119364cb4feb6d7c0cc97c/NaturalStringOrdering
public class NaturalStringComparer(bool ignoreCase) : IComparer<string?>
{
    private readonly bool _ignoreCase = ignoreCase;

    /// <summary>
    /// Creates an instance of <c>NaturalStringComparer</c> for case-insensitive string comparison.
    /// </summary>
    public NaturalStringComparer()
        : this(true)
    {
    }

    /// <summary>
    /// Compares two strings and returns a value indicating whether one is less than, equal to, or greater than the other.
    /// </summary>
    /// <param name="x">The first string to compare.</param>
    /// <param name="y">The second string to compare.</param>
    /// <returns>A signed integer that indicates the relative values of x and y, as in the Compare method in the <c>IComparer&lt;T&gt;</c> interface.</returns>
    public int Compare(string? x, string? y)
    {
        // check for null values first: a null reference is considered to be less than any reference that is not null
        if (x == null && y == null)
        {
            return 0;
        }
        if (x == null)
        {
            return -1;
        }
        if (y == null)
        {
            return 1;
        }

        string[] splitX = Regex.Split(x.Replace(" ", ""), "([0-9]+)");
        string[] splitY = Regex.Split(y.Replace(" ", ""), "([0-9]+)");

        int comparer = 0;

        for (int i = 0; comparer == 0 && i < splitX.Length; i++)
        {
            if (splitY.Length <= i)
            {
                comparer = 1; // x > y
            }

            int numericX = -1;
            int numericY = -1;
            if (int.TryParse(splitX[i], out numericX))
            {
                if (int.TryParse(splitY[i], out numericY))
                {
                    comparer = numericX - numericY;
                }
                else
                {
                    comparer = 1; // x > y
                }
            }
            else
            {
                comparer = string.Compare(splitX[i], splitY[i], _ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
            }
        }

        return comparer;
    }
}
