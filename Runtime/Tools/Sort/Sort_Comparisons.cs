/**************************************************************************************************/
/*!
\file   Sort_Comparisons.cs
\author Craig Williams
\par    Last Updated
        2021-06-05
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A partner file for the Sort class, containing various Comparison functions.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System;

namespace CodeParadox.Tenor.Tools
{
  /************************************************************************************************/
  public static partial class Sort
  {
    /// <summary>
    /// A <see cref="Comparison{T}"/> function for determining if something is sorted least
    /// to greatest.
    /// </summary>
    /// <typeparam name="T">The <see cref="IComparable{T}"/> type.</typeparam>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the comparison result. 0 means the values are equal. Less than 0 means
    /// that <paramref name="a"/> is less than <paramref name="b"/>. Greater than 0 means that
    /// <paramref name="a"/> is greater than <paramref name="b"/>.</returns>
    public static int CompareMinToMax<T>(T a, T b) where T : IComparable<T>
    {
      return a.CompareTo(b); // Return a's comparison to b.
    }

    /// <summary>
    /// A <see cref="Comparison{T}"/> function for determining if something is sorted greatest
    /// to least.
    /// </summary>
    /// <typeparam name="T">The <see cref="IComparable{T}"/> type.</typeparam>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the comparison result. 0 means the values are equal. Less than 0 means
    /// that <paramref name="b"/> is less than <paramref name="a"/>. Greater than 0 means that
    /// <paramref name="b"/> is greater than <paramref name="a"/>.</returns>
    public static int CompareMaxToMin<T>(T a, T b) where T : IComparable<T>
    {
      return b.CompareTo(a); // Return b's comparison to a.
    }
  }
  /************************************************************************************************/
}