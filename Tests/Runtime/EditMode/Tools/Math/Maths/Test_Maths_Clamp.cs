/**************************************************************************************************/
/*!
\file   Test_Maths_Clamp.cs
\author Craig Williams
\par    Last Updated
        2021-05-24
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A unit test file for the Math Clamp tools.

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
  /// A test class for <see cref="Tenor.Tools.Maths"/>, for checking clamp functions.
  /// </summary>
  [Explicit] // Comment this to allow this to be run automatically.
  public sealed class Test_Maths_Clamp : Test_Maths
  {
    /// <summary>
    /// A test for <see cref="Maths.ClampII{T}(T, T, T)"/> and
    /// <see cref="Maths.ClampIING{T}(T, T, T)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void Clamp_IComparable_ReturnsEquality([Random(0, 10, 5)] int min,
                                                  [Random(10, 20, 5)] int max,
                                                  [Random(1, 20, 5)] int value)
    {
      // Get the expected value from clamping.
      int expected = value < min ? min : (value > max ? max : value);

      // Create the comparable objects.
      ComparableTest ctMin = new ComparableTest(min);
      ComparableTest ctMax = new ComparableTest(max);
      ComparableTest ctValue = new ComparableTest(value);

      // Compare the values and test them against the expected output.
      Assert.AreEqual(expected, Maths.ComparableClampII(ctValue, ctMin, ctMax).Value);
      Assert.AreEqual(expected, Maths.ComparableClampIING(ctValue, ctMin, ctMax).Value);
    }

    /// <summary>
    /// A test for <see cref="Maths.ClampII(sbyte, sbyte, sbyte)"/>,
    /// <see cref="Maths.ClampEE(sbyte, sbyte, sbyte)"/>,
    /// <see cref="Maths.ClampIE(sbyte, sbyte, sbyte)"/>, and
    /// <see cref="Maths.ClampEI(sbyte, sbyte, sbyte)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void Clamp_SByte_ReturnsEquality([Random(0, 10, 5)] sbyte min,
                                            [Random(10, 20, 5)] sbyte max,
                                            [Random(1, 20, 5)] sbyte value)
    {
      // Get the expected values from clamping.
      sbyte expectedII = value < min ? min : (value > max ? max : value);
      sbyte expectedEE = value <= min ? (sbyte)(min + 1) : (value >= max ? (sbyte)(max - 1) : value);
      sbyte expectedIE = value < min ? min : (value >= max ? (sbyte)(max - 1) : value);
      sbyte expectedEI = value <= min ? (sbyte)(min + 1) : (value > max ? max : value);

      // Clamp the given value and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.ClampII(value, min, max));
      Assert.AreEqual(expectedEE, Maths.ClampEE(value, min, max));
      Assert.AreEqual(expectedIE, Maths.ClampIE(value, min, max));
      Assert.AreEqual(expectedEI, Maths.ClampEI(value, min, max));
    }

    /// <summary>
    /// A test for <see cref="Maths.ClampII(byte, byte, byte)"/>,
    /// <see cref="Maths.ClampEE(byte, byte, byte)"/>,
    /// <see cref="Maths.ClampIE(byte, byte, byte)"/>, and
    /// <see cref="Maths.ClampEI(byte, byte, byte)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void Clamp_Byte_ReturnsEquality([Random(1, 10, 5)] byte min,
                                           [Random(10, 20, 5)] byte max,
                                           [Random(1, 20, 5)] byte value)
    {
      // Get the expected values from clamping.
      byte expectedII = value < min ? min : (value > max ? max : value);
      byte expectedEE = value <= min ? (byte)(min + 1) : (value >= max ? (byte)(max - 1) : value);
      byte expectedIE = value < min ? min : (value >= max ? (byte)(max - 1) : value);
      byte expectedEI = value <= min ? (byte)(min + 1) : (value > max ? max : value);

      // Clamp the given value and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.ClampII(value, min, max));
      Assert.AreEqual(expectedEE, Maths.ClampEE(value, min, max));
      Assert.AreEqual(expectedIE, Maths.ClampIE(value, min, max));
      Assert.AreEqual(expectedEI, Maths.ClampEI(value, min, max));
    }

    /// <summary>
    /// A test for <see cref="Maths.ClampII(short, short, short)"/>,
    /// <see cref="Maths.ClampEE(short, short, short)"/>,
    /// <see cref="Maths.ClampIE(short, short, short)"/>, and
    /// <see cref="Maths.ClampEI(short, short, short)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void Clamp_Short_ReturnsEquality([Random(0, 10, 5)] short min,
                                            [Random(10, 20, 5)] short max,
                                            [Random(1, 20, 5)] short value)
    {
      // Get the expected values from clamping.
      short expectedII = value < min ? min : (value > max ? max : value);
      short expectedEE = value <= min ? (short)(min + 1) : (value >= max ? (short)(max - 1) : value);
      short expectedIE = value < min ? min : (value >= max ? (short)(max - 1) : value);
      short expectedEI = value <= min ? (short)(min + 1) : (value > max ? max : value);

      // Clamp the given value and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.ClampII(value, min, max));
      Assert.AreEqual(expectedEE, Maths.ClampEE(value, min, max));
      Assert.AreEqual(expectedIE, Maths.ClampIE(value, min, max));
      Assert.AreEqual(expectedEI, Maths.ClampEI(value, min, max));
    }

    /// <summary>
    /// A test for <see cref="Maths.ClampII(ushort, ushort, ushort)"/>,
    /// <see cref="Maths.ClampEE(ushort, ushort, ushort)"/>,
    /// <see cref="Maths.ClampIE(ushort, ushort, ushort)"/>, and
    /// <see cref="Maths.ClampEI(ushort, ushort, ushort)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void Clamp_UShort_ReturnsEquality([Random(1, 10, 5)] ushort min,
                                             [Random(10, 20, 5)] ushort max,
                                             [Random(1, 20, 5)] ushort value)
    {
      // Get the expected values from clamping.
      ushort expectedII = value < min ? min : (value > max ? max : value);
      ushort expectedEE = value <= min ? (ushort)(min + 1) : (value >= max ? (ushort)(max - 1) : value);
      ushort expectedIE = value < min ? min : (value >= max ? (ushort)(max - 1) : value);
      ushort expectedEI = value <= min ? (ushort)(min + 1) : (value > max ? max : value);

      // Clamp the given value and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.ClampII(value, min, max));
      Assert.AreEqual(expectedEE, Maths.ClampEE(value, min, max));
      Assert.AreEqual(expectedIE, Maths.ClampIE(value, min, max));
      Assert.AreEqual(expectedEI, Maths.ClampEI(value, min, max));
    }

    /// <summary>
    /// A test for <see cref="Maths.ClampII(int, int, int)"/>,
    /// <see cref="Maths.ClampEE(int, int, int)"/>,
    /// <see cref="Maths.ClampIE(int, int, int)"/>, and
    /// <see cref="Maths.ClampEI(int, int, int)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void Clamp_Int_ReturnsEquality([Random(0, 10, 5)] int min,
                                          [Random(10, 20, 5)] int max,
                                          [Random(1, 20, 5)] int value)
    {
      // Get the expected values from clamping.
      int expectedII = value < min ? min : (value > max ? max : value);
      int expectedEE = value <= min ? min + 1 : (value >= max ? max - 1 : value);
      int expectedIE = value < min ? min : (value >= max ? max - 1 : value);
      int expectedEI = value <= min ? min + 1 : (value > max ? max : value);

      // Clamp the given value and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.ClampII(value, min, max));
      Assert.AreEqual(expectedEE, Maths.ClampEE(value, min, max));
      Assert.AreEqual(expectedIE, Maths.ClampIE(value, min, max));
      Assert.AreEqual(expectedEI, Maths.ClampEI(value, min, max));
    }

    /// <summary>
    /// A test for <see cref="Maths.ClampII(uint, uint, uint)"/>,
    /// <see cref="Maths.ClampEE(uint, uint, uint)"/>,
    /// <see cref="Maths.ClampIE(uint, uint, uint)"/>, and
    /// <see cref="Maths.ClampEI(uint, uint, uint)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void Clamp_UInt_ReturnsEquality([Random(1, 10, 5)] int min,
                                           [Random(10, 20, 5)] int max,
                                           [Random(1, 20, 5)] int value)
    {
      uint umin = Convert.ToUInt32(min);
      uint umax = Convert.ToUInt32(max);
      uint uvalue = Convert.ToUInt32(value);

      // Get the expected values from clamping.
      uint expectedII = uvalue < umin ? umin : (uvalue > umax ? umax : uvalue);
      uint expectedEE = uvalue <= umin ? umin + 1: (uvalue >= umax ? umax - 1 : uvalue);
      uint expectedIE = uvalue < umin ? umin : (uvalue >= umax ? umax - 1 : uvalue);
      uint expectedEI = uvalue <= umin ? umin + 1 : (uvalue > umax ? umax : uvalue);

      // Clamp the given value and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.ClampII(uvalue, umin, umax));
      Assert.AreEqual(expectedEE, Maths.ClampEE(uvalue, umin, umax));
      Assert.AreEqual(expectedIE, Maths.ClampIE(uvalue, umin, umax));
      Assert.AreEqual(expectedEI, Maths.ClampEI(uvalue, umin, umax));
    }

    /// <summary>
    /// A test for <see cref="Maths.ClampII(long, long, long)"/>,
    /// <see cref="Maths.ClampEE(long, long, long)"/>,
    /// <see cref="Maths.ClampIE(long, long, long)"/>, and
    /// <see cref="Maths.ClampEI(long, long, long)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void Clamp_Long_ReturnsEquality([Random(0, 10, 5)] long min,
                                           [Random(10, 20, 5)] long max,
                                           [Random(1, 20, 5)] long value)
    {
      // Get the expected values from clamping.
      long expectedII = value < min ? min : (value > max ? max : value);
      long expectedEE = value <= min ? min + 1 : (value >= max ? max - 1 : value);
      long expectedIE = value < min ? min : (value >= max ? max - 1 : value);
      long expectedEI = value <= min ? min + 1 : (value > max ? max : value);

      // Clamp the given value and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.ClampII(value, min, max));
      Assert.AreEqual(expectedEE, Maths.ClampEE(value, min, max));
      Assert.AreEqual(expectedIE, Maths.ClampIE(value, min, max));
      Assert.AreEqual(expectedEI, Maths.ClampEI(value, min, max));
    }

    /// <summary>
    /// A test for <see cref="Maths.ClampII(ulong, ulong, ulong)"/>,
    /// <see cref="Maths.ClampEE(ulong, ulong, ulong)"/>,
    /// <see cref="Maths.ClampIE(ulong, ulong, ulong)"/>, and
    /// <see cref="Maths.ClampEI(ulong, ulong, ulong)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void Clamp_ULong_ReturnsEquality([Random(1, 10, 5)] long min,
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

      // Clamp the given uvalue and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.ClampII(uvalue, umin, umax));
      Assert.AreEqual(expectedEE, Maths.ClampEE(uvalue, umin, umax));
      Assert.AreEqual(expectedIE, Maths.ClampIE(uvalue, umin, umax));
      Assert.AreEqual(expectedEI, Maths.ClampEI(uvalue, umin, umax));
    }

    /// <summary>
    /// A test for <see cref="Maths.ClampII(float, float, float)"/>,
    /// <see cref="Maths.ClampEE(float, float, float)"/>,
    /// <see cref="Maths.ClampIE(float, float, float)"/>, and
    /// <see cref="Maths.ClampEI(float, float, float)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void Clamp_Float_ReturnsEquality([Random(0, 10, 5)] float min,
                                            [Random(10, 20, 5)] float max,
                                            [Random(1, 20, 5)] float value)
    {
      // Get the expected values from clamping.
      float epsilon = float.Epsilon;
      float expectedII = value < min ? min : (value > max ? max : value);
      float expectedEE = value <= min ? min + epsilon : (value >= max ? max - epsilon : value);
      float expectedIE = value < min ? min : (value >= max ? max - epsilon : value);
      float expectedEI = value <= min ? min + epsilon : (value > max ? max : value);

      // Clamp the given value and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.ClampII(value, min, max));
      Assert.AreEqual(expectedEE, Maths.ClampEE(value, min, max));
      Assert.AreEqual(expectedIE, Maths.ClampIE(value, min, max));
      Assert.AreEqual(expectedEI, Maths.ClampEI(value, min, max));
    }

    /// <summary>
    /// A test for <see cref="Maths.ClampII(double, double, double)"/>,
    /// <see cref="Maths.ClampEE(double, double, double)"/>,
    /// <see cref="Maths.ClampIE(double, double, double)"/>, and
    /// <see cref="Maths.ClampEI(double, double, double)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void Clamp_Double_ReturnsEquality([Random(0, 10, 5)] double min,
                                             [Random(10, 20, 5)] double max,
                                             [Random(1, 20, 5)] double value)
    {
      // Get the expected values from clamping.
      double epsilon = double.Epsilon;
      double expectedII = value < min ? min : (value > max ? max : value);
      double expectedEE = value <= min ? min + epsilon : (value >= max ? max - epsilon : value);
      double expectedIE = value < min ? min : (value >= max ? max - epsilon : value);
      double expectedEI = value <= min ? min + epsilon : (value > max ? max : value);

      // Clamp the given value and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.ClampII(value, min, max));
      Assert.AreEqual(expectedEE, Maths.ClampEE(value, min, max));
      Assert.AreEqual(expectedIE, Maths.ClampIE(value, min, max));
      Assert.AreEqual(expectedEI, Maths.ClampEI(value, min, max));
    }

    /// <summary>
    /// A test for <see cref="Maths.ClampII(decimal, decimal, decimal)"/>,
    /// <see cref="Maths.ClampEE(decimal, decimal, decimal)"/>,
    /// <see cref="Maths.ClampIE(decimal, decimal, decimal)"/>, and
    /// <see cref="Maths.ClampEI(decimal, decimal, decimal)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void Clamp_Decimal_ReturnsEquality([Random(0, 10, 5)] decimal min,
                                              [Random(10, 20, 5)] decimal max,
                                              [Random(1, 20, 5)] decimal value)
    {
      // Get the expected values from clamping.
      decimal epsilon = (decimal)double.Epsilon;
      decimal expectedII = value < min ? min : (value > max ? max : value);
      decimal expectedEE = value <= min ? min + epsilon : (value >= max ? max - epsilon : value);
      decimal expectedIE = value < min ? min : (value >= max ? max - epsilon : value);
      decimal expectedEI = value <= min ? min + epsilon : (value > max ? max : value);

      // Clamp the given value and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.ClampII(value, min, max));
      Assert.AreEqual(expectedEE, Maths.ClampEE(value, min, max));
      Assert.AreEqual(expectedIE, Maths.ClampIE(value, min, max));
      Assert.AreEqual(expectedEI, Maths.ClampEI(value, min, max));
    }

    /// <summary>
    /// A test for <see cref="Maths.ClampII(char, char, char)"/>,
    /// <see cref="Maths.ClampEE(char, char, char)"/>,
    /// <see cref="Maths.ClampIE(char, char, char)"/>, and
    /// <see cref="Maths.ClampEI(char, char, char)"/>.
    /// </summary>
    /// <param name="min">The minimum value allowed.</param>
    /// <param name="max">The maximum value allowed.</param>
    /// <param name="value">The value to clamp.</param>
    [Test(TestOf = typeof(Maths))]
    public void Clamp_Char_ReturnsEquality([Random(0, 10, 5)] int min,
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

      // Clamp the given cvalue and test in each clamp case.
      Assert.AreEqual(expectedII, Maths.ClampII(cvalue, cmin, cmax));
      Assert.AreEqual(expectedEE, Maths.ClampEE(cvalue, cmin, cmax));
      Assert.AreEqual(expectedIE, Maths.ClampIE(cvalue, cmin, cmax));
      Assert.AreEqual(expectedEI, Maths.ClampEI(cvalue, cmin, cmax));
    }
  }
  /************************************************************************************************/
}