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
    /// A test for <see cref="Maths.Lerp(sbyte, sbyte, float)"/>,
    /// <see cref="<see cref="Maths.Lerp(sbyte, sbyte, double)"/>"/>,
    /// <see cref="<see cref="Maths.LerpUnclamped(sbyte, sbyte, float)"/>"/>, and
    /// <see cref="<see cref="Maths.LerpUnclamped(sbyte, sbyte, double)"/>"/>.
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

      Assert.AreEqual(expectedClamped, Maths.Lerp(a, b, t));
      Assert.AreEqual(expectedClamped, Maths.Lerp(a, b, (double)t));
      Assert.AreEqual(expectedUnclamped, Maths.LerpUnclamped(a, b, t));
      Assert.AreEqual(expectedUnclamped, Maths.LerpUnclamped(a, b, (double)t));
    }

    /// <summary>
    /// A test for <see cref="Maths.Lerp(byte, byte, float)"/>,
    /// <see cref="<see cref="Maths.Lerp(byte, byte, double)"/>"/>,
    /// <see cref="<see cref="Maths.LerpUnclamped(byte, byte, float)"/>"/>, and
    /// <see cref="<see cref="Maths.LerpUnclamped(byte, byte, double)"/>"/>.
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

      Assert.AreEqual(expectedClamped, Maths.Lerp(a, b, t));
      Assert.AreEqual(expectedClamped, Maths.Lerp(a, b, (double)t));
      Assert.AreEqual(expectedUnclamped, Maths.LerpUnclamped(a, b, t));
      Assert.AreEqual(expectedUnclamped, Maths.LerpUnclamped(a, b, (double)t));
    }

    /// <summary>
    /// A test for <see cref="Maths.Lerp(short, short, float)"/>,
    /// <see cref="<see cref="Maths.Lerp(short, short, double)"/>"/>,
    /// <see cref="<see cref="Maths.LerpUnclamped(short, short, float)"/>"/>, and
    /// <see cref="<see cref="Maths.LerpUnclamped(short, short, double)"/>"/>.
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

      Assert.AreEqual(expectedClamped, Maths.Lerp(a, b, t));
      Assert.AreEqual(expectedClamped, Maths.Lerp(a, b, (double)t));
      Assert.AreEqual(expectedUnclamped, Maths.LerpUnclamped(a, b, t));
      Assert.AreEqual(expectedUnclamped, Maths.LerpUnclamped(a, b, (double)t));
    }

    /// <summary>
    /// A test for <see cref="Maths.Lerp(ushort, ushort, float)"/>,
    /// <see cref="<see cref="Maths.Lerp(ushort, ushort, double)"/>"/>,
    /// <see cref="<see cref="Maths.LerpUnclamped(ushort, ushort, float)"/>"/>, and
    /// <see cref="<see cref="Maths.LerpUnclamped(ushort, ushort, double)"/>"/>.
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

      Assert.AreEqual(expectedClamped, Maths.Lerp(a, b, t));
      Assert.AreEqual(expectedClamped, Maths.Lerp(a, b, (double)t));
      Assert.AreEqual(expectedUnclamped, Maths.LerpUnclamped(a, b, t));
      Assert.AreEqual(expectedUnclamped, Maths.LerpUnclamped(a, b, (double)t));
    }

    /// <summary>
    /// A test for <see cref="Maths.Lerp(int, int, float)"/>,
    /// <see cref="<see cref="Maths.Lerp(int, int, double)"/>"/>,
    /// <see cref="<see cref="Maths.LerpUnclamped(int, int, float)"/>"/>, and
    /// <see cref="<see cref="Maths.LerpUnclamped(int, int, double)"/>"/>.
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

      Assert.AreEqual(expectedClamped, Maths.Lerp(a, b, t));
      Assert.AreEqual(expectedClamped, Maths.Lerp(a, b, (double)t));
      Assert.AreEqual(expectedUnclamped, Maths.LerpUnclamped(a, b, t));
      Assert.AreEqual(expectedUnclamped, Maths.LerpUnclamped(a, b, (double)t));
    }

    /// <summary>
    /// A test for <see cref="Maths.Lerp(uint, uint, float)"/>,
    /// <see cref="<see cref="Maths.Lerp(uint, uint, double)"/>"/>,
    /// <see cref="<see cref="Maths.LerpUnclamped(uint, uint, float)"/>"/>, and
    /// <see cref="<see cref="Maths.LerpUnclamped(uint, uint, double)"/>"/>.
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

      Assert.AreEqual(expectedClamped, Maths.Lerp(a, b, t));
      Assert.AreEqual(expectedClamped, Maths.Lerp(a, b, (double)t));
      Assert.AreEqual(expectedUnclamped, Maths.LerpUnclamped(a, b, t));
      Assert.AreEqual(expectedUnclamped, Maths.LerpUnclamped(a, b, (double)t));
    }

    /// <summary>
    /// A test for <see cref="Maths.Lerp(long, long, float)"/>,
    /// <see cref="<see cref="Maths.Lerp(long, long, double)"/>"/>,
    /// <see cref="<see cref="Maths.LerpUnclamped(long, long, float)"/>"/>, and
    /// <see cref="<see cref="Maths.LerpUnclamped(long, long, double)"/>"/>.
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

      Assert.AreEqual(expectedClamped, Maths.Lerp(a, b, t));
      Assert.AreEqual(expectedClamped, Maths.Lerp(a, b, (double)t));
      Assert.AreEqual(expectedUnclamped, Maths.LerpUnclamped(a, b, t));
      Assert.AreEqual(expectedUnclamped, Maths.LerpUnclamped(a, b, (double)t));
    }

    /// <summary>
    /// A test for <see cref="Maths.Lerp(ulong, ulong, float)"/>,
    /// <see cref="<see cref="Maths.Lerp(ulong, ulong, double)"/>"/>,
    /// <see cref="<see cref="Maths.LerpUnclamped(ulong, ulong, float)"/>"/>, and
    /// <see cref="<see cref="Maths.LerpUnclamped(ulong, ulong, double)"/>"/>.
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

      Assert.AreEqual(expectedClamped, Maths.Lerp(a, b, t));
      Assert.AreEqual(expectedClamped, Maths.Lerp(a, b, (double)t));
      Assert.AreEqual(expectedUnclamped, Maths.LerpUnclamped(a, b, t));
      Assert.AreEqual(expectedUnclamped, Maths.LerpUnclamped(a, b, (double)t));
    }

    /// <summary>
    /// A test for <see cref="Maths.Lerp(float, float, float)"/>,
    /// <see cref="<see cref="Maths.Lerp(float, float, double)"/>"/>,
    /// <see cref="<see cref="Maths.LerpUnclamped(float, float, float)"/>"/>, and
    /// <see cref="<see cref="Maths.LerpUnclamped(float, float, double)"/>"/>.
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

      Assert.AreEqual(expectedClamped, Maths.Lerp(a, b, t));
      Assert.AreEqual(expectedClamped, Maths.Lerp(a, b, (double)t));
      Assert.AreEqual(expectedUnclamped, Maths.LerpUnclamped(a, b, t));
      Assert.AreEqual(expectedUnclamped, Maths.LerpUnclamped(a, b, (double)t));
    }

    /// <summary>
    /// A test for <see cref="Maths.Lerp(double, double, float)"/>,
    /// <see cref="<see cref="Maths.Lerp(double, double, double)"/>"/>,
    /// <see cref="<see cref="Maths.LerpUnclamped(double, double, float)"/>"/>, and
    /// <see cref="<see cref="Maths.LerpUnclamped(double, double, double)"/>"/>.
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

      Assert.AreEqual(expectedClamped, Maths.Lerp(a, b, t));
      Assert.AreEqual(expectedClamped, Maths.Lerp(a, b, (double)t));
      Assert.AreEqual(expectedUnclamped, Maths.LerpUnclamped(a, b, t));
      Assert.AreEqual(expectedUnclamped, Maths.LerpUnclamped(a, b, (double)t));
    }

    /// <summary>
    /// A test for <see cref="Maths.Lerp(decimal, decimal, float)"/>,
    /// <see cref="<see cref="Maths.Lerp(decimal, decimal, double)"/>"/>,
    /// <see cref="<see cref="Maths.LerpUnclamped(decimal, decimal, float)"/>"/>, and
    /// <see cref="<see cref="Maths.LerpUnclamped(decimal, decimal, double)"/>"/>.
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

      Assert.AreEqual(expectedClamped, Maths.Lerp(da, db, t));
      Assert.AreEqual(expectedClampedD, Maths.Lerp(da, db, (double)t));
      Assert.AreEqual(expectedUnclamped, Maths.LerpUnclamped(da, db, t));
      Assert.AreEqual(expectedUnclampedD, Maths.LerpUnclamped(da, db, (double)t));
    }
  }
  /************************************************************************************************/
}