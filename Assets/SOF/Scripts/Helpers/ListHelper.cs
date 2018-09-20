using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Extension methods for Lists.
/// </summary>
static class ListHelper
{
    /// <summary>Pops the data from the list at the given index.</summary>
    /// <typeparam name="T">The type of data in the list.</typeparam>
    /// <param name="list">The list to pop from.</param>
    /// <param name="index">The index of the data to pop.</param>
    /// <returns></returns>
    public static T Pop<T>(this List<T> list, int index=0)
    {
        T r = list[index];
        list.RemoveAt(index);
        return r;
    }

    /// <summary>
    /// Create a new list instance with a clone of each item in the supplied list.
    /// </summary>
    /// <typeparam name="T">The type of data in the list. T must be a cloneable type.</typeparam>
    /// <param name="listToClone">The list to clone.</param>
    /// <returns>A list filled with clones of the data in the original list.</returns>
    public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
    {
        return listToClone.Select(item => (T)item.Clone()).ToList();
    }

    /// <summary>
    /// Create a new list instance with a clone of each item in the supplied list.
    /// </summary>
    /// <typeparam name="T">The type of data in the list. T must be a cloneable type.</typeparam>
    /// <param name="listToClone">The list to clone.</param>
    /// <returns>A list filled with clones of the data in the original list.</returns>
    public static List<T> Clone<T>(this List<T> listToClone) where T : ICloneable
    {
        return listToClone.Select(item => (T)item.Clone()).ToList();
    }
}
