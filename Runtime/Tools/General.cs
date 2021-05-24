/**************************************************************************************************/
/*!
\file   General.cs
\author Craig Williams
\par    Last Updated
        2021-05-21
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A toolkit of functions for general situations.

\par Bug List

\par References
*/
/**************************************************************************************************/

namespace CodeParadox.Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// A series of useful extension and helper functions for general situations.
  /// </summary>
  public static partial class General
  {
    /// <summary>
    /// A function for swapping two values. <paramref name="a"/> becomes <paramref name="b"/>,
    /// and <paramref name="b"/> becomes <paramref name="a"/>.
    /// </summary>
    /// <typeparam name="T">The type that both values are.</typeparam>
    /// <param name="a">The value to swap to <paramref name="b"/>.</param>
    /// <param name="b">The value to swap to <paramref name="a"/>.</param>
    public static void SwapValues<T>(ref T a, ref T b)
    {
      T temp = a; // Make a temp copy of a.
      a = b; // Swap a to b.
      b = temp; // Swap b to the stored value of a.
    }
  }
  /************************************************************************************************/
}