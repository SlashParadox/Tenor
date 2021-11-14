/**************************************************************************************************/
/*!
\file   Test_General.cs
\author Craig Williams
\par    Last Updated
        2021-05-22
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A unit test file for the General tools.

\par Bug List

\par References
*/
/**************************************************************************************************/

using SlashParadox.Tenor.Tools;
using NUnit.Framework;

namespace SlashParadox.Tenor.Tests.Runtime.EditMode
{
  /************************************************************************************************/
  /// <summary>
  /// A test class for <see cref="Tenor.Tools.General"/>.
  /// </summary>
  [Explicit] // Comment this to allow this to be run automatically.
  public sealed class Test_General
  {
    /// <summary>
    /// A test for <see cref="General.SwapValues{T}(ref T, ref T)"/>.
    /// </summary>
    [Test(TestOf = typeof(General))]
    public void SwapValues_Int_ReturnsSuccess([Random(1, 100, 5)] int a, [Random(1, 100, 5)] int b)
    {
      // Create copies of the passed-in values.
      int copyA = a;
      int copyB = b;

      // Swap the values.
      General.SwapValues(ref copyA, ref copyB);

      // Make sure the values were properly swapped.
      Assert.AreEqual(copyA, b);
      Assert.AreEqual(copyB, a);
    }
  }
  /************************************************************************************************/
}