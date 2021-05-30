/**************************************************************************************************/
/*!
\file   Test_Maths_Lerp.cs
\author Craig Williams
\par    Last Updated
        2021-05-25
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A unit test file for the Math Lerp tools.

\par Bug List

\par References
*/
/**************************************************************************************************/

using CodeParadox.Tenor.Tools;
using System;
using NUnit.Framework;

namespace CodeParadox.Tenor.Tests.Runtime.EditMode
{
  /************************************************************************************************/
  /// <summary>
  /// A test class for <see cref="Tenor.Tools.Maths"/>, for checking lerp functions.
  /// </summary>
  [Explicit] // Comment this to allow this to be run automatically.
  public class Test_Maths_Lerp : Test_Maths
  {
    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(sbyte, sbyte, float)"/>,
    /// <see cref="<see cref="Lerp.LerpValue(sbyte, sbyte, double)"/>"/>,
    /// <see cref="<see cref="Lerp.LerpUnclamped(sbyte, sbyte, float)"/>"/>, and
    /// <see cref="<see cref="Lerp.LerpUnclamped(sbyte, sbyte, double)"/>"/>.
    /// </summary>
    /// <param name="a">The minimum value allowed.</param>
    /// <param name="b">The maximum value allowed.</param>
    /// <param name="t">The interpolation between the two values.</param>
    [Test(TestOf = typeof(Maths))]
    public void Lerp_SByte_ReturnsEquality([Random(0, 10, 5)] sbyte a,
                                           [Random(10, 20, 5)] sbyte b,
                                           [Random(-1.0f, 1.0f, 5)] float t)
    {
      sbyte expectedUnclamped = (sbyte)(a + (b - a) * t);
      sbyte expectedClamped = Maths.ClampII(expectedUnclamped, a, b);

      Assert.AreEqual(expectedClamped, Lerp.LerpValue(a, b, t));
      Assert.AreEqual(expectedClamped, Lerp.LerpValue(a, b, (double)t));
      Assert.AreEqual(expectedUnclamped, Lerp.LerpUnclamped(a, b, t));
      Assert.AreEqual(expectedUnclamped, Lerp.LerpUnclamped(a, b, (double)t));
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(byte, byte, float)"/>,
    /// <see cref="<see cref="Lerp.LerpValue(byte, byte, double)"/>"/>,
    /// <see cref="<see cref="Lerp.LerpUnclamped(byte, byte, float)"/>"/>, and
    /// <see cref="<see cref="Lerp.LerpUnclamped(byte, byte, double)"/>"/>.
    /// </summary>
    /// <param name="a">The minimum value allowed.</param>
    /// <param name="b">The maximum value allowed.</param>
    /// <param name="t">The interpolation between the two values.</param>
    [Test(TestOf = typeof(Maths))]
    public void Lerp_Byte_ReturnsEquality([Random(0, 10, 5)] byte a,
                                          [Random(10, 20, 5)] byte b,
                                          [Random(0.0f, 2.0f, 5)] float t)
    {
      byte expectedUnclamped = (byte)(a + (b - a) * t);
      byte expectedClamped = Maths.ClampII(expectedUnclamped, a, b);

      Assert.AreEqual(expectedClamped, Lerp.LerpValue(a, b, t));
      Assert.AreEqual(expectedClamped, Lerp.LerpValue(a, b, (double)t));
      Assert.AreEqual(expectedUnclamped, Lerp.LerpUnclamped(a, b, t));
      Assert.AreEqual(expectedUnclamped, Lerp.LerpUnclamped(a, b, (double)t));
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(short, short, float)"/>,
    /// <see cref="<see cref="Lerp.LerpValue(short, short, double)"/>"/>,
    /// <see cref="<see cref="Lerp.LerpUnclamped(short, short, float)"/>"/>, and
    /// <see cref="<see cref="Lerp.LerpUnclamped(short, short, double)"/>"/>.
    /// </summary>
    /// <param name="a">The minimum value allowed.</param>
    /// <param name="b">The maximum value allowed.</param>
    /// <param name="t">The interpolation between the two values.</param>
    [Test(TestOf = typeof(Maths))]
    public void Lerp_Short_ReturnsEquality([Random(0, 10, 5)] short a,
                                           [Random(10, 20, 5)] short b,
                                           [Random(-1.0f, 1.0f, 5)] float t)
    {
      short expectedUnclamped = (short)(a + (b - a) * t);
      short expectedClamped = Maths.ClampII(expectedUnclamped, a, b);

      Assert.AreEqual(expectedClamped, Lerp.LerpValue(a, b, t));
      Assert.AreEqual(expectedClamped, Lerp.LerpValue(a, b, (double)t));
      Assert.AreEqual(expectedUnclamped, Lerp.LerpUnclamped(a, b, t));
      Assert.AreEqual(expectedUnclamped, Lerp.LerpUnclamped(a, b, (double)t));
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(ushort, ushort, float)"/>,
    /// <see cref="<see cref="Lerp.LerpValue(ushort, ushort, double)"/>"/>,
    /// <see cref="<see cref="Lerp.LerpUnclamped(ushort, ushort, float)"/>"/>, and
    /// <see cref="<see cref="Lerp.LerpUnclamped(ushort, ushort, double)"/>"/>.
    /// </summary>
    /// <param name="a">The minimum value allowed.</param>
    /// <param name="b">The maximum value allowed.</param>
    /// <param name="t">The interpolation between the two values.</param>
    [Test(TestOf = typeof(Maths))]
    public void Lerp_UShort_ReturnsEquality([Random(0, 10, 5)] ushort a,
                                            [Random(10, 20, 5)] ushort b,
                                            [Random(0.0f, 2.0f, 5)] float t)
    {
      ushort expectedUnclamped = (ushort)(a + (b - a) * t);
      ushort expectedClamped = Maths.ClampII(expectedUnclamped, a, b);

      Assert.AreEqual(expectedClamped, Lerp.LerpValue(a, b, t));
      Assert.AreEqual(expectedClamped, Lerp.LerpValue(a, b, (double)t));
      Assert.AreEqual(expectedUnclamped, Lerp.LerpUnclamped(a, b, t));
      Assert.AreEqual(expectedUnclamped, Lerp.LerpUnclamped(a, b, (double)t));
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(int, int, float)"/>,
    /// <see cref="<see cref="Lerp.LerpValue(int, int, double)"/>"/>,
    /// <see cref="<see cref="Lerp.LerpUnclamped(int, int, float)"/>"/>, and
    /// <see cref="<see cref="Lerp.LerpUnclamped(int, int, double)"/>"/>.
    /// </summary>
    /// <param name="a">The minimum value allowed.</param>
    /// <param name="b">The maximum value allowed.</param>
    /// <param name="t">The interpolation between the two values.</param>
    [Test(TestOf = typeof(Maths))]
    public void Lerp_Int_ReturnsEquality([Random(0, 10, 5)] int a,
                                         [Random(10, 20, 5)] int b,
                                         [Random(-1.0f, 1.0f, 5)] float t)
    {
      int expectedUnclamped = (int)(a + (b - a) * t);
      int expectedClamped = Maths.ClampII(expectedUnclamped, a, b);

      Assert.AreEqual(expectedClamped, Lerp.LerpValue(a, b, t));
      Assert.AreEqual(expectedClamped, Lerp.LerpValue(a, b, (double)t));
      Assert.AreEqual(expectedUnclamped, Lerp.LerpUnclamped(a, b, t));
      Assert.AreEqual(expectedUnclamped, Lerp.LerpUnclamped(a, b, (double)t));
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(uint, uint, float)"/>,
    /// <see cref="<see cref="Lerp.LerpValue(uint, uint, double)"/>"/>,
    /// <see cref="<see cref="Lerp.LerpUnclamped(uint, uint, float)"/>"/>, and
    /// <see cref="<see cref="Lerp.LerpUnclamped(uint, uint, double)"/>"/>.
    /// </summary>
    /// <param name="a">The minimum value allowed.</param>
    /// <param name="b">The maximum value allowed.</param>
    /// <param name="t">The interpolation between the two values.</param>
    [Test(TestOf = typeof(Maths))]
    public void Lerp_UInt_ReturnsEquality([Random(0u, 10u, 5)] uint a,
                                          [Random(10u, 20u, 5)] uint b,
                                          [Random(0.0f, 2.0f, 5)] float t)
    {
      uint expectedUnclamped = (uint)(a + (b - a) * t);
      uint expectedClamped = Maths.ClampII(expectedUnclamped, a, b);

      Assert.AreEqual(expectedClamped, Lerp.LerpValue(a, b, t));
      Assert.AreEqual(expectedClamped, Lerp.LerpValue(a, b, (double)t));
      Assert.AreEqual(expectedUnclamped, Lerp.LerpUnclamped(a, b, t));
      Assert.AreEqual(expectedUnclamped, Lerp.LerpUnclamped(a, b, (double)t));
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(long, long, float)"/>,
    /// <see cref="<see cref="Lerp.LerpValue(long, long, double)"/>"/>,
    /// <see cref="<see cref="Lerp.LerpUnclamped(long, long, float)"/>"/>, and
    /// <see cref="<see cref="Lerp.LerpUnclamped(long, long, double)"/>"/>.
    /// </summary>
    /// <param name="a">The minimum value allowed.</param>
    /// <param name="b">The maximum value allowed.</param>
    /// <param name="t">The interpolation between the two values.</param>
    [Test(TestOf = typeof(Maths))]
    public void Lerp_Long_ReturnsEquality([Random(0, 10, 5)] long a,
                                          [Random(10, 20, 5)] long b,
                                          [Random(-1.0f, 1.0f, 5)] float t)
    {
      long expectedUnclamped = (long)(a + (b - a) * t);
      long expectedClamped = Maths.ClampII(expectedUnclamped, a, b);

      Assert.AreEqual(expectedClamped, Lerp.LerpValue(a, b, t));
      Assert.AreEqual(expectedClamped, Lerp.LerpValue(a, b, (double)t));
      Assert.AreEqual(expectedUnclamped, Lerp.LerpUnclamped(a, b, t));
      Assert.AreEqual(expectedUnclamped, Lerp.LerpUnclamped(a, b, (double)t));
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(ulong, ulong, float)"/>,
    /// <see cref="<see cref="Lerp.LerpValue(ulong, ulong, double)"/>"/>,
    /// <see cref="<see cref="Lerp.LerpUnclamped(ulong, ulong, float)"/>"/>, and
    /// <see cref="<see cref="Lerp.LerpUnclamped(ulong, ulong, double)"/>"/>.
    /// </summary>
    /// <param name="a">The minimum value allowed.</param>
    /// <param name="b">The maximum value allowed.</param>
    /// <param name="t">The interpolation between the two values.</param>
    [Test(TestOf = typeof(Maths))]
    public void Lerp_ULong_ReturnsEquality([Random(0ul, 10ul, 5)] ulong a,
                                           [Random(10ul, 20ul, 5)] ulong b,
                                           [Random(0.0f, 2.0f, 5)] float t)
    {
      ulong expectedUnclamped = (ulong)(a + (b - a) * t);
      ulong expectedClamped = Maths.ClampII(expectedUnclamped, a, b);

      Assert.AreEqual(expectedClamped, Lerp.LerpValue(a, b, t));
      Assert.AreEqual(expectedClamped, Lerp.LerpValue(a, b, (double)t));
      Assert.AreEqual(expectedUnclamped, Lerp.LerpUnclamped(a, b, t));
      Assert.AreEqual(expectedUnclamped, Lerp.LerpUnclamped(a, b, (double)t));
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(float, float, float)"/>,
    /// <see cref="<see cref="Lerp.LerpValue(float, float, double)"/>"/>,
    /// <see cref="<see cref="Lerp.LerpUnclamped(float, float, float)"/>"/>, and
    /// <see cref="<see cref="Lerp.LerpUnclamped(float, float, double)"/>"/>.
    /// </summary>
    /// <param name="a">The minimum value allowed.</param>
    /// <param name="b">The maximum value allowed.</param>
    /// <param name="t">The interpolation between the two values.</param>
    [Test(TestOf = typeof(Maths))]
    public void Lerp_Float_ReturnsEquality([Random(0.0f, 10.0f, 5)] float a,
                                           [Random(10.0f, 20.0f, 5)] float b,
                                           [Random(-1.0f, 1.0f, 5)] float t)
    {
      float expectedUnclamped = a + (b - a) * t;
      float expectedClamped = Maths.ClampII(expectedUnclamped, a, b);

      Assert.AreEqual(expectedClamped, Lerp.LerpValue(a, b, t));
      Assert.AreEqual(expectedClamped, Lerp.LerpValue(a, b, (double)t));
      Assert.AreEqual(expectedUnclamped, Lerp.LerpUnclamped(a, b, t));
      Assert.AreEqual(expectedUnclamped, Lerp.LerpUnclamped(a, b, (double)t));
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(double, double, float)"/>,
    /// <see cref="<see cref="Lerp.LerpValue(double, double, double)"/>"/>,
    /// <see cref="<see cref="Lerp.LerpUnclamped(double, double, float)"/>"/>, and
    /// <see cref="<see cref="Lerp.LerpUnclamped(double, double, double)"/>"/>.
    /// </summary>
    /// <param name="a">The minimum value allowed.</param>
    /// <param name="b">The maximum value allowed.</param>
    /// <param name="t">The interpolation between the two values.</param>
    [Test(TestOf = typeof(Maths))]
    public void Lerp_Double_ReturnsEquality([Random(0.0f, 10.0f, 5)] double a,
                                            [Random(10.0f, 20.0f, 5)] double b,
                                            [Random(-1.0f, 1.0f, 5)] float t)
    {
      double expectedUnclamped = a + (b - a) * t;
      double expectedClamped = Maths.ClampII(expectedUnclamped, a, b);

      Assert.AreEqual(expectedClamped, Lerp.LerpValue(a, b, t));
      Assert.AreEqual(expectedClamped, Lerp.LerpValue(a, b, (double)t));
      Assert.AreEqual(expectedUnclamped, Lerp.LerpUnclamped(a, b, t));
      Assert.AreEqual(expectedUnclamped, Lerp.LerpUnclamped(a, b, (double)t));
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(decimal, decimal, float)"/>,
    /// <see cref="<see cref="Lerp.LerpValue(decimal, decimal, double)"/>"/>,
    /// <see cref="<see cref="Lerp.LerpUnclamped(decimal, decimal, float)"/>"/>, and
    /// <see cref="<see cref="Lerp.LerpUnclamped(decimal, decimal, double)"/>"/>.
    /// </summary>
    /// <param name="a">The minimum value allowed.</param>
    /// <param name="b">The maximum value allowed.</param>
    /// <param name="t">The interpolation between the two values.</param>
    [Test(TestOf = typeof(Maths))]
    public void Lerp_Decimal_ReturnsEquality([Random(0.0f, 10.0f, 5)] float a,
                                             [Random(10.0f, 20.0f, 5)] float b,
                                             [Random(-1.0f, 1.0f, 5)] float t)
    {
      decimal da = Convert.ToDecimal(a);
      decimal db = Convert.ToDecimal(b);

      decimal expectedUnclamped = da + (db - da) * Convert.ToDecimal(t);
      decimal expectedUnclampedD = da + (db - da) * Convert.ToDecimal((double)t);
      decimal expectedClamped = Maths.ClampII(expectedUnclamped, da, db);
      decimal expectedClampedD = Maths.ClampII(expectedUnclampedD, da, db);

      Assert.AreEqual(expectedClamped, Lerp.LerpValue(da, db, t));
      Assert.AreEqual(expectedClampedD, Lerp.LerpValue(da, db, (double)t));
      Assert.AreEqual(expectedUnclamped, Lerp.LerpUnclamped(da, db, t));
      Assert.AreEqual(expectedUnclampedD, Lerp.LerpUnclamped(da, db, (double)t));
    }
  }
  /************************************************************************************************/
}