/**************************************************************************************************/
/*!
\file   Test_Maths_InRange.cs
\author Craig Williams
\par    Last Updated
        2021-05-24
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A unit test file for the Math InRange checking tools.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System;
using CodeParadox.Tenor.Tools;
using NUnit.Framework;

namespace CodeParadox.Tenor.Tests.Runtime.EditMode
{
  /************************************************************************************************/
  /// <summary>
  /// A test class for <see cref="Tenor.Tools.Maths"/>, for checking in-range functions.
  /// </summary>
  public sealed class Test_Maths_InRange : Test_Maths
  {
    /// <summary>
    /// A test for <see cref="Maths.InRangeII{T}(T, T, T)"/>,
    /// <see cref="Maths.InRangeIING{T}(T, T, T)"/>, <see cref="Maths.InRangeEE{T}(T, T, T)"/>,
    /// <see cref="Maths.InRangeEENG{T}(T, T, T)"/>, <see cref="Maths.InRangeIE{T}(T, T, T)"/>,
    /// <see cref="Maths.InRangeIENG{T}(T, T, T)"/>, <see cref="Maths.InRangeEI{T}(T, T, T)"/>, and
    /// <see cref="Maths.InRangeEING{T}(T, T, T)"/>,
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to test.</param>
    [Test(TestOf = typeof(Maths))]
    public void InRange_IComparable_ReturnsAccuracy([Random(0, 10, 5)] int min,
                                                    [Random(10, 20, 5)] int max,
                                                    [Random(1, 20, 5)] int value)
    {
      // Get the expected values from clamping.
      bool expectedII = value >= min && value <= max ? true : false;
      bool expectedEE = value > min && value < max ? true : false;
      bool expectedIE = value >= min && value < max ? true : false;
      bool expectedEI = value > min && value <= max ? true : false;

      // Create the comparable objects.
      ComparableTest ctMin = new ComparableTest(min);
      ComparableTest ctMax = new ComparableTest(max);
      ComparableTest ctValue = new ComparableTest(value);

      // Compare the values and test them against the expected output.
      Assert.AreEqual(expectedII, Maths.InRangeII(ctValue, ctMin, ctMax));
      Assert.AreEqual(expectedII, Maths.InRangeIING(ctValue, ctMin, ctMax));
      Assert.AreEqual(expectedEE, Maths.InRangeEE(ctValue, ctMin, ctMax));
      Assert.AreEqual(expectedEE, Maths.InRangeEENG(ctValue, ctMin, ctMax));
      Assert.AreEqual(expectedIE, Maths.InRangeIE(ctValue, ctMin, ctMax));
      Assert.AreEqual(expectedIE, Maths.InRangeIENG(ctValue, ctMin, ctMax));
      Assert.AreEqual(expectedEI, Maths.InRangeEI(ctValue, ctMin, ctMax));
      Assert.AreEqual(expectedEI, Maths.InRangeEING(ctValue, ctMin, ctMax));
    }

    /// <summary>
    /// A test for <see cref="Maths.InRangeII(sbyte, sbyte, sbyte)"/>,
    /// <see cref="Maths.InRangeEE(sbyte, sbyte, sbyte)"/>,
    /// <see cref="Maths.InRangeIE(sbyte, sbyte, sbyte)"/>, and
    /// <see cref="Maths.InRangeEI(sbyte, sbyte, sbyte)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void InRange_SByte_ReturnsAccuracy([Random(0, 10, 5)] sbyte min,
                                              [Random(10, 20, 5)] sbyte max,
                                              [Random(1, 20, 5)] sbyte value)
    {
      // Get the expected values from clamping.
      sbyte expectedII = value < min ? min : (value > max ? max : value);
      sbyte expectedEE = value <= min ? (sbyte)(min + 1) : (value >= max ? (sbyte)(max - 1) : value);
      sbyte expectedIE = value < min ? min : (value >= max ? (sbyte)(max - 1) : value);
      sbyte expectedEI = value <= min ? (sbyte)(min + 1) : (value > max ? max : value);

      // InRange the given value and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.InRangeII(value, min, max));
      Assert.AreEqual(expectedEE, Maths.InRangeEE(value, min, max));
      Assert.AreEqual(expectedIE, Maths.InRangeIE(value, min, max));
      Assert.AreEqual(expectedEI, Maths.InRangeEI(value, min, max));
    }

    /// <summary>
    /// A test for <see cref="Maths.InRangeII(byte, byte, byte)"/>,
    /// <see cref="Maths.InRangeEE(byte, byte, byte)"/>,
    /// <see cref="Maths.InRangeIE(byte, byte, byte)"/>, and
    /// <see cref="Maths.InRangeEI(byte, byte, byte)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void InRange_Byte_ReturnsAccuracy([Random(1, 10, 5)] byte min,
                                             [Random(10, 20, 5)] byte max,
                                             [Random(1, 20, 5)] byte value)
    {
      // Get the expected values from clamping.
      byte expectedII = value < min ? min : (value > max ? max : value);
      byte expectedEE = value <= min ? (byte)(min + 1) : (value >= max ? (byte)(max - 1) : value);
      byte expectedIE = value < min ? min : (value >= max ? (byte)(max - 1) : value);
      byte expectedEI = value <= min ? (byte)(min + 1) : (value > max ? max : value);

      // InRange the given value and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.InRangeII(value, min, max));
      Assert.AreEqual(expectedEE, Maths.InRangeEE(value, min, max));
      Assert.AreEqual(expectedIE, Maths.InRangeIE(value, min, max));
      Assert.AreEqual(expectedEI, Maths.InRangeEI(value, min, max));
    }

    /// <summary>
    /// A test for <see cref="Maths.InRangeII(short, short, short)"/>,
    /// <see cref="Maths.InRangeEE(short, short, short)"/>,
    /// <see cref="Maths.InRangeIE(short, short, short)"/>, and
    /// <see cref="Maths.InRangeEI(short, short, short)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void InRange_Short_ReturnsAccuracy([Random(0, 10, 5)] short min,
                                           [Random(10, 20, 5)] short max,
                                           [Random(1, 20, 5)] short value)
    {
      // Get the expected values from clamping.
      short expectedII = value < min ? min : (value > max ? max : value);
      short expectedEE = value <= min ? (short)(min + 1) : (value >= max ? (short)(max - 1) : value);
      short expectedIE = value < min ? min : (value >= max ? (short)(max - 1) : value);
      short expectedEI = value <= min ? (short)(min + 1) : (value > max ? max : value);

      // InRange the given value and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.InRangeII(value, min, max));
      Assert.AreEqual(expectedEE, Maths.InRangeEE(value, min, max));
      Assert.AreEqual(expectedIE, Maths.InRangeIE(value, min, max));
      Assert.AreEqual(expectedEI, Maths.InRangeEI(value, min, max));
    }

    /// <summary>
    /// A test for <see cref="Maths.InRangeII(ushort, ushort, ushort)"/>,
    /// <see cref="Maths.InRangeEE(ushort, ushort, ushort)"/>,
    /// <see cref="Maths.InRangeIE(ushort, ushort, ushort)"/>, and
    /// <see cref="Maths.InRangeEI(ushort, ushort, ushort)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void InRange_UShort_ReturnsAccuracy([Random(1, 10, 5)] ushort min,
                                               [Random(10, 20, 5)] ushort max,
                                               [Random(1, 20, 5)] ushort value)
    {
      // Get the expected values from clamping.
      ushort expectedII = value < min ? min : (value > max ? max : value);
      ushort expectedEE = value <= min ? (ushort)(min + 1) : (value >= max ? (ushort)(max - 1) : value);
      ushort expectedIE = value < min ? min : (value >= max ? (ushort)(max - 1) : value);
      ushort expectedEI = value <= min ? (ushort)(min + 1) : (value > max ? max : value);

      // InRange the given value and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.InRangeII(value, min, max));
      Assert.AreEqual(expectedEE, Maths.InRangeEE(value, min, max));
      Assert.AreEqual(expectedIE, Maths.InRangeIE(value, min, max));
      Assert.AreEqual(expectedEI, Maths.InRangeEI(value, min, max));
    }

    /// <summary>
    /// A test for <see cref="Maths.InRangeII(int, int, int)"/>,
    /// <see cref="Maths.InRangeEE(int, int, int)"/>,
    /// <see cref="Maths.InRangeIE(int, int, int)"/>, and
    /// <see cref="Maths.InRangeEI(int, int, int)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void InRange_Int_ReturnsAccuracy([Random(0, 10, 5)] int min,
                                            [Random(10, 20, 5)] int max,
                                            [Random(1, 20, 5)] int value)
    {
      // Get the expected values from clamping.
      int expectedII = value < min ? min : (value > max ? max : value);
      int expectedEE = value <= min ? min + 1 : (value >= max ? max - 1 : value);
      int expectedIE = value < min ? min : (value >= max ? max - 1 : value);
      int expectedEI = value <= min ? min + 1 : (value > max ? max : value);

      // InRange the given value and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.InRangeII(value, min, max));
      Assert.AreEqual(expectedEE, Maths.InRangeEE(value, min, max));
      Assert.AreEqual(expectedIE, Maths.InRangeIE(value, min, max));
      Assert.AreEqual(expectedEI, Maths.InRangeEI(value, min, max));
    }

    /// <summary>
    /// A test for <see cref="Maths.InRangeII(uint, uint, uint)"/>,
    /// <see cref="Maths.InRangeEE(uint, uint, uint)"/>,
    /// <see cref="Maths.InRangeIE(uint, uint, uint)"/>, and
    /// <see cref="Maths.InRangeEI(uint, uint, uint)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void InRange_UInt_ReturnsAccuracy([Random(1, 10, 5)] int min,
                                             [Random(10, 20, 5)] int max,
                                             [Random(1, 20, 5)] int value)
    {
      uint umin = Convert.ToUInt32(min);
      uint umax = Convert.ToUInt32(max);
      uint uvalue = Convert.ToUInt32(value);

      // Get the expected values from clamping.
      uint expectedII = uvalue < umin ? umin : (uvalue > umax ? umax : uvalue);
      uint expectedEE = uvalue <= umin ? umin + 1 : (uvalue >= umax ? umax - 1 : uvalue);
      uint expectedIE = uvalue < umin ? umin : (uvalue >= umax ? umax - 1 : uvalue);
      uint expectedEI = uvalue <= umin ? umin + 1 : (uvalue > umax ? umax : uvalue);

      // InRange the given value and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.InRangeII(uvalue, umin, umax));
      Assert.AreEqual(expectedEE, Maths.InRangeEE(uvalue, umin, umax));
      Assert.AreEqual(expectedIE, Maths.InRangeIE(uvalue, umin, umax));
      Assert.AreEqual(expectedEI, Maths.InRangeEI(uvalue, umin, umax));
    }

    /// <summary>
    /// A test for <see cref="Maths.InRangeII(long, long, long)"/>,
    /// <see cref="Maths.InRangeEE(long, long, long)"/>,
    /// <see cref="Maths.InRangeIE(long, long, long)"/>, and
    /// <see cref="Maths.InRangeEI(long, long, long)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void InRange_Long_ReturnsAccuracy([Random(0, 10, 5)] long min,
                                             [Random(10, 20, 5)] long max,
                                             [Random(1, 20, 5)] long value)
    {
      // Get the expected values from clamping.
      long expectedII = value < min ? min : (value > max ? max : value);
      long expectedEE = value <= min ? min + 1 : (value >= max ? max - 1 : value);
      long expectedIE = value < min ? min : (value >= max ? max - 1 : value);
      long expectedEI = value <= min ? min + 1 : (value > max ? max : value);

      // InRange the given value and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.InRangeII(value, min, max));
      Assert.AreEqual(expectedEE, Maths.InRangeEE(value, min, max));
      Assert.AreEqual(expectedIE, Maths.InRangeIE(value, min, max));
      Assert.AreEqual(expectedEI, Maths.InRangeEI(value, min, max));
    }

    /// <summary>
    /// A test for <see cref="Maths.InRangeII(ulong, ulong, ulong)"/>,
    /// <see cref="Maths.InRangeEE(ulong, ulong, ulong)"/>,
    /// <see cref="Maths.InRangeIE(ulong, ulong, ulong)"/>, and
    /// <see cref="Maths.InRangeEI(ulong, ulong, ulong)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void InRange_ULong_ReturnsAccuracy([Random(1, 10, 5)] long min,
                                              [Random(10, 20, 5)] long max,
                                              [Random(1, 20, 5)] long value)
    {
      ulong umin = Convert.ToUInt64(min);
      ulong umax = Convert.ToUInt64(max);
      ulong uvalue = Convert.ToUInt64(value);

      // Get the expected values from clamping.
      ulong expectedII = uvalue < umin ? umin : (uvalue > umax ? umax : uvalue);
      ulong expectedEE = uvalue <= umin ? umin + 1 : (uvalue >= umax ? umax - 1 : uvalue);
      ulong expectedIE = uvalue < umin ? umin : (uvalue >= umax ? umax - 1 : uvalue);
      ulong expectedEI = uvalue <= umin ? umin + 1 : (uvalue > umax ? umax : uvalue);

      // InRange the given uvalue and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.InRangeII(uvalue, umin, umax));
      Assert.AreEqual(expectedEE, Maths.InRangeEE(uvalue, umin, umax));
      Assert.AreEqual(expectedIE, Maths.InRangeIE(uvalue, umin, umax));
      Assert.AreEqual(expectedEI, Maths.InRangeEI(uvalue, umin, umax));
    }

    /// <summary>
    /// A test for <see cref="Maths.InRangeII(float, float, float)"/>,
    /// <see cref="Maths.InRangeEE(float, float, float)"/>,
    /// <see cref="Maths.InRangeIE(float, float, float)"/>, and
    /// <see cref="Maths.InRangeEI(float, float, float)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void InRange_Float_ReturnsAccuracy([Random(0, 10, 5)] float min,
                                              [Random(10, 20, 5)] float max,
                                              [Random(1, 20, 5)] float value)
    {
      // Get the expected values from clamping.
      float epsilon = float.Epsilon;
      float expectedII = value < min ? min : (value > max ? max : value);
      float expectedEE = value <= min ? min + epsilon : (value >= max ? max - epsilon : value);
      float expectedIE = value < min ? min : (value >= max ? max - epsilon : value);
      float expectedEI = value <= min ? min + epsilon : (value > max ? max : value);

      // InRange the given value and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.InRangeII(value, min, max));
      Assert.AreEqual(expectedEE, Maths.InRangeEE(value, min, max));
      Assert.AreEqual(expectedIE, Maths.InRangeIE(value, min, max));
      Assert.AreEqual(expectedEI, Maths.InRangeEI(value, min, max));
    }

    /// <summary>
    /// A test for <see cref="Maths.InRangeII(double, double, double)"/>,
    /// <see cref="Maths.InRangeEE(double, double, double)"/>,
    /// <see cref="Maths.InRangeIE(double, double, double)"/>, and
    /// <see cref="Maths.InRangeEI(double, double, double)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void InRange_Double_ReturnsAccuracy([Random(0, 10, 5)] double min,
                                               [Random(10, 20, 5)] double max,
                                               [Random(1, 20, 5)] double value)
    {
      // Get the expected values from clamping.
      double epsilon = double.Epsilon;
      double expectedII = value < min ? min : (value > max ? max : value);
      double expectedEE = value <= min ? min + epsilon : (value >= max ? max - epsilon : value);
      double expectedIE = value < min ? min : (value >= max ? max - epsilon : value);
      double expectedEI = value <= min ? min + epsilon : (value > max ? max : value);

      // InRange the given value and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.InRangeII(value, min, max));
      Assert.AreEqual(expectedEE, Maths.InRangeEE(value, min, max));
      Assert.AreEqual(expectedIE, Maths.InRangeIE(value, min, max));
      Assert.AreEqual(expectedEI, Maths.InRangeEI(value, min, max));
    }

    /// <summary>
    /// A test for <see cref="Maths.InRangeII(decimal, decimal, decimal)"/>,
    /// <see cref="Maths.InRangeEE(decimal, decimal, decimal)"/>,
    /// <see cref="Maths.InRangeIE(decimal, decimal, decimal)"/>, and
    /// <see cref="Maths.InRangeEI(decimal, decimal, decimal)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void InRange_Decimal_ReturnsAccuracy([Random(0, 10, 5)] decimal min,
                                                [Random(10, 20, 5)] decimal max,
                                                [Random(1, 20, 5)] decimal value)
    {
      // Get the expected values from clamping.
      decimal epsilon = (decimal)double.Epsilon;
      decimal expectedII = value < min ? min : (value > max ? max : value);
      decimal expectedEE = value <= min ? min + epsilon : (value >= max ? max - epsilon : value);
      decimal expectedIE = value < min ? min : (value >= max ? max - epsilon : value);
      decimal expectedEI = value <= min ? min + epsilon : (value > max ? max : value);

      // InRange the given value and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.InRangeII(value, min, max));
      Assert.AreEqual(expectedEE, Maths.InRangeEE(value, min, max));
      Assert.AreEqual(expectedIE, Maths.InRangeIE(value, min, max));
      Assert.AreEqual(expectedEI, Maths.InRangeEI(value, min, max));
    }

    /// <summary>
    /// A test for <see cref="Maths.InRangeII(char, char, char)"/>,
    /// <see cref="Maths.InRangeEE(char, char, char)"/>,
    /// <see cref="Maths.InRangeIE(char, char, char)"/>, and
    /// <see cref="Maths.InRangeEI(char, char, char)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void InRange_Char_ReturnsAccuracy([Random(0, 10, 5)] int min,
                                             [Random(10, 20, 5)] int max,
                                             [Random(1, 20, 5)] int value)
    {
      char cmin = (char)min;
      char cmax = (char)max;
      char cvalue = (char)value;

      // Get the expected cvalues from clamping.
      char expectedII = cvalue < cmin ? cmin : (cvalue > cmax ? cmax : cvalue);
      char expectedEE = cvalue <= cmin ? (char)(cmin + 1) : (cvalue >= cmax ? (char)(cmax - 1) : cvalue);
      char expectedIE = cvalue < cmin ? cmin : (cvalue >= cmax ? (char)(cmax - 1) : cvalue);
      char expectedEI = cvalue <= cmin ? (char)(cmin + 1) : (cvalue > cmax ? cmax : cvalue);

      // InRange the given cvalue and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.InRangeII(cvalue, cmin, cmax));
      Assert.AreEqual(expectedEE, Maths.InRangeEE(cvalue, cmin, cmax));
      Assert.AreEqual(expectedIE, Maths.InRangeIE(cvalue, cmin, cmax));
      Assert.AreEqual(expectedEI, Maths.InRangeEI(cvalue, cmin, cmax));
    }
  }
  /************************************************************************************************/
}