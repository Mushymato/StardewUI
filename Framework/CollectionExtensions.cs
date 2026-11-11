using System.Collections;

namespace StardewUI.Framework;

/// <summary>
/// Collection extensions/helpers used in various code.
/// </summary>
internal static class CollectionExtensions
{
    /// <summary>
    /// Determines if a sequence contains any items
    /// </summary>
    public static bool Any(this IEnumerable items)
    {
        if (items is ICollection c)
            return c.Count != 0;

        return items.GetEnumerator().MoveNext();
    }
}
