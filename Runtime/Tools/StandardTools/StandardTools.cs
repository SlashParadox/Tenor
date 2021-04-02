/**************************************************************************************************/
/*!
\file   StandardTools.cs
\author Craig Williams
\par    Last Updated
        2021-04-01
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for tool function that are not related to any particular topic.

\par Bug List

\par References
*/
/**************************************************************************************************/

namespace Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// A series of tool functions that can be used in any general context. They fall under no
  /// specific type.
  /// </summary>
  public static partial class StandardTools
  {
    /// <summary>
    /// A function for swapping two values. <paramref name="A"/> becomes <paramref name="B"/>, and
    /// <paramref name="B"/> becomes <paramref name="A"/>.
    /// </summary>
    /// <typeparam name="T">The type of both values.</typeparam>
    /// <param name="A">The value to swap to <paramref name="B"/>.</param>
    /// <param name="B">The value to swap to <paramref name="A"/>.</param>
    public static void SwapValues<T>(ref T A, ref T B)
    {
      T temp = A; // Make a temp value of A.
      A = B; // Swap A to B.
      B = temp; // Swap B to the stored A value.
    }
  }
  /************************************************************************************************/
}