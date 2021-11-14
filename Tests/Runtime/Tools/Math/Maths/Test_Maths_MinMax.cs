/**************************************************************************************************/
/*!
\file   Test_Maths_MinMax.cs
\author Craig Williams
\par    Last Updated
        2021-05-24
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A unit test file for the Math Min/Max tools.

\par Bug List

\par References
*/
/**************************************************************************************************/

using SlashParadox.Tenor.Tools;
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SlashParadox.Tenor.Tests.Runtime.EditMode
{
  /************************************************************************************************/
  /// <summary>
  /// A test class for <see cref="Tenor.Tools.Maths"/>, for testing min-max functions.
  /// </summary>
  [Explicit] // Comment this to allow this to be run automatically.
  public class Test_Maths_MinMax : Test_Maths
  {
    /// <summary>
    /// A test for <see cref="Maths.Min{T}(T, T)"/> and <see cref="Maths.Max{T}(T, T)"/>.
    /// </summary>
    /// <param name="valueA">The first value to compare.</param>
    /// <param name="valueB">The second value to compare.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMaxSolo_IComparable_ReturnsAccurate([Random(-100, 100, 5)] int valueA,
                                                       [Random(-100, 100, 5)] int valueB)
    {
      // Get the expected values.
      int expectedMin = valueA < valueB ? valueA : valueB;
      int expectedMax = valueA > valueB ? valueA : valueB;

      // Create the comparable tests.
      ComparableTest compA = new ComparableTest(valueA);
      ComparableTest compB = new ComparableTest(valueB);

      // Get the min and maxes.
      ComparableTest actualMin = Maths.Min(compA, compB);
      ComparableTest actualMax = Maths.Max(compA, compB);

      // Test the min and max.
      Assert.AreEqual(expectedMin, actualMin.Value);
      Assert.AreEqual(expectedMax, actualMax.Value);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min{T}(T[])"/>, <see cref="Maths.Min{T}(IList{T})"/>,
    /// <see cref="Maths.Max{T}(T[])"/>, and <see cref="Maths.Max{T}(IList{T})"/>.
    /// </summary>
    /// <param name="generatedValues">The number of random values to test.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMax_IComparable_ReturnsAccurate([Random(2, 50, 50)] int generatedValues)
    {
      int expectedMin = int.MaxValue; // Hold the expected min.
      int expectedMax = int.MinValue; // Hold the expected max.

      // Fill a randomly sized list with random values.
      Random random = new System.Random();
      List<ComparableTest> tests = new List<ComparableTest>(generatedValues);
      for (int i = 0; i < generatedValues; i++)
      {
        int value = random.Next(-100, 100);
        tests.Add(new ComparableTest(value));

        // Calculate the current expected min.
        if (value < expectedMin)
          expectedMin = value;

        // Calculate the current expected max.
        if (value > expectedMax)
          expectedMax = value;
      }

      // Get the tested mins.
      ComparableTest actualMin = Maths.Min(tests);
      ComparableTest actualMinNG = Maths.MinNG(tests.ToArray());

      // Get the tested maxes.
      ComparableTest actualMax = Maths.Max(tests);
      ComparableTest actualMaxNG = Maths.MaxNG(tests.ToArray());

      // Assert that all values match.
      Assert.AreEqual(expectedMin, actualMin.Value);
      Assert.AreEqual(expectedMin, actualMinNG.Value);
      Assert.AreEqual(expectedMax, actualMax.Value);
      Assert.AreEqual(expectedMax, actualMaxNG.Value);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(sbyte, sbyte)"/> and <see cref="Maths.Max(sbyte, sbyte)"/>.
    /// </summary>
    /// <param name="valueA">The first value to compare.</param>
    /// <param name="valueB">The second value to compare.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMaxSolo_SByte_ReturnsAccurate([Random(-100, 100, 5)] int valueA,
                                                 [Random(-100, 100, 5)] int valueB)
    {
      // Get the expected values.
      sbyte expectedMin = (sbyte)(valueA < valueB ? valueA : valueB);
      sbyte expectedMax = (sbyte)(valueA > valueB ? valueA : valueB);

      // Get the min and maxes.
      sbyte actualMin = Maths.Min((sbyte)valueA, (sbyte)valueB);
      sbyte actualMax = Maths.Max((sbyte)valueA, (sbyte)valueB);

      // Test the min and max.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMax, actualMax);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(sbyte[])"/>, <see cref="Maths.Min(IList{sbyte})"/>,
    /// <see cref="Maths.Max(sbyte[])"/>, and <see cref="Maths.Max(IList{sbyte})"/>.
    /// </summary>
    /// <param name="generatedValues">The number of random values to test.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMax_SByte_ReturnsAccurate([Random(2, 50, 50)] int generatedValues)
    {
      sbyte expectedMin = sbyte.MaxValue; // Hold the expected min.
      sbyte expectedMax = sbyte.MinValue; // Hold the expected max.

      // Fill a randomly sized list with random values.
      Random random = new System.Random();
      List<sbyte> tests = new List<sbyte>(generatedValues);
      for (int i = 0; i < generatedValues; i++)
      {
        sbyte value = (sbyte)random.Next(-100, 100);
        tests.Add(value);

        // Calculate the current expected min.
        if (value < expectedMin)
          expectedMin = value;

        // Calculate the current expected max.
        if (value > expectedMax)
          expectedMax = value;
      }

      // Get the tested mins.
      sbyte actualMin = Maths.Min(tests);
      sbyte actualMinNG = Maths.MinNG(tests.ToArray());

      // Get the tested maxes.
      sbyte actualMax = Maths.Max(tests);
      sbyte actualMaxNG = Maths.MaxNG(tests.ToArray());

      // Assert that all values match.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMin, actualMinNG);
      Assert.AreEqual(expectedMax, actualMax);
      Assert.AreEqual(expectedMax, actualMaxNG);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(byte, byte)"/> and <see cref="Maths.Max(byte, byte)"/>.
    /// </summary>
    /// <param name="valueA">The first value to compare.</param>
    /// <param name="valueB">The second value to compare.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMaxSolo_Byte_ReturnsAccurate([Random(0, 100, 5)] int valueA,
                                                [Random(0, 100, 5)] int valueB)
    {
      // Get the expected values.
      byte expectedMin = (byte)(valueA < valueB ? valueA : valueB);
      byte expectedMax = (byte)(valueA > valueB ? valueA : valueB);

      // Get the min and maxes.
      byte actualMin = Maths.Min((byte)valueA, (byte)valueB);
      byte actualMax = Maths.Max((byte)valueA, (byte)valueB);

      // Test the min and max.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMax, actualMax);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(byte[])"/>, <see cref="Maths.Min(IList{byte})"/>,
    /// <see cref="Maths.Max(byte[])"/>, and <see cref="Maths.Max(IList{byte})"/>.
    /// </summary>
    /// <param name="generatedValues">The number of random values to test.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMax_Byte_ReturnsAccurate([Random(2, 50, 50)] int generatedValues)
    {
      byte expectedMin = byte.MaxValue; // Hold the expected min.
      byte expectedMax = byte.MinValue; // Hold the expected max.

      // Fill a randomly sized list with random values.
      Random random = new System.Random();
      List<byte> tests = new List<byte>(generatedValues);
      for (int i = 0; i < generatedValues; i++)
      {
        byte value = (byte)random.Next(0, 100);
        tests.Add(value);

        // Calculate the current expected min.
        if (value < expectedMin)
          expectedMin = value;

        // Calculate the current expected max.
        if (value > expectedMax)
          expectedMax = value;
      }

      // Get the tested mins.
      byte actualMin = Maths.Min(tests);
      byte actualMinNG = Maths.MinNG(tests.ToArray());

      // Get the tested maxes.
      byte actualMax = Maths.Max(tests);
      byte actualMaxNG = Maths.MaxNG(tests.ToArray());

      // Assert that all values match.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMin, actualMinNG);
      Assert.AreEqual(expectedMax, actualMax);
      Assert.AreEqual(expectedMax, actualMaxNG);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(short, short)"/> and <see cref="Maths.Max(short, short)"/>.
    /// </summary>
    /// <param name="valueA">The first value to compare.</param>
    /// <param name="valueB">The second value to compare.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMaxSolo_Short_ReturnsAccurate([Random(-100, 100, 5)] int valueA,
                                                 [Random(-100, 100, 5)] int valueB)
    {
      // Get the expected values.
      short expectedMin = (short)(valueA < valueB ? valueA : valueB);
      short expectedMax = (short)(valueA > valueB ? valueA : valueB);

      // Get the min and maxes.
      short actualMin = Maths.Min((short)valueA, (short)valueB);
      short actualMax = Maths.Max((short)valueA, (short)valueB);

      // Test the min and max.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMax, actualMax);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(short[])"/>, <see cref="Maths.Min(IList{short})"/>,
    /// <see cref="Maths.Max(short[])"/>, and <see cref="Maths.Max(IList{short})"/>.
    /// </summary>
    /// <param name="generatedValues">The number of random values to test.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMax_Short_ReturnsAccurate([Random(2, 50, 50)] int generatedValues)
    {
      short expectedMin = short.MaxValue; // Hold the expected min.
      short expectedMax = short.MinValue; // Hold the expected max.

      // Fill a randomly sized list with random values.
      Random random = new System.Random();
      List<short> tests = new List<short>(generatedValues);
      for (int i = 0; i < generatedValues; i++)
      {
        short value = (short)random.Next(-100, 100);
        tests.Add(value);

        // Calculate the current expected min.
        if (value < expectedMin)
          expectedMin = value;

        // Calculate the current expected max.
        if (value > expectedMax)
          expectedMax = value;
      }

      // Get the tested mins.
      short actualMin = Maths.Min(tests);
      short actualMinNG = Maths.MinNG(tests.ToArray());

      // Get the tested maxes.
      short actualMax = Maths.Max(tests);
      short actualMaxNG = Maths.MaxNG(tests.ToArray());

      // Assert that all values match.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMin, actualMinNG);
      Assert.AreEqual(expectedMax, actualMax);
      Assert.AreEqual(expectedMax, actualMaxNG);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(ushort, ushort)"/> and <see cref="Maths.Max(ushort, ushort)"/>.
    /// </summary>
    /// <param name="valueA">The first value to compare.</param>
    /// <param name="valueB">The second value to compare.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMaxSolo_UShort_ReturnsAccurate([Random(0, 100, 5)] int valueA,
                                                  [Random(0, 100, 5)] int valueB)
    {
      // Get the expected values.
      ushort expectedMin = (ushort)(valueA < valueB ? valueA : valueB);
      ushort expectedMax = (ushort)(valueA > valueB ? valueA : valueB);

      // Get the min and maxes.
      ushort actualMin = Maths.Min((ushort)valueA, (ushort)valueB);
      ushort actualMax = Maths.Max((ushort)valueA, (ushort)valueB);

      // Test the min and max.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMax, actualMax);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(ushort[])"/>, <see cref="Maths.Min(IList{ushort})"/>,
    /// <see cref="Maths.Max(ushort[])"/>, and <see cref="Maths.Max(IList{ushort})"/>.
    /// </summary>
    /// <param name="generatedValues">The number of random values to test.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMax_UShort_ReturnsAccurate([Random(2, 50, 50)] int generatedValues)
    {
      ushort expectedMin = ushort.MaxValue; // Hold the expected min.
      ushort expectedMax = ushort.MinValue; // Hold the expected max.

      // Fill a randomly sized list with random values.
      Random random = new System.Random();
      List<ushort> tests = new List<ushort>(generatedValues);
      for (int i = 0; i < generatedValues; i++)
      {
        ushort value = (ushort)random.Next(0, 100);
        tests.Add(value);

        // Calculate the current expected min.
        if (value < expectedMin)
          expectedMin = value;

        // Calculate the current expected max.
        if (value > expectedMax)
          expectedMax = value;
      }

      // Get the tested mins.
      ushort actualMin = Maths.Min(tests);
      ushort actualMinNG = Maths.MinNG(tests.ToArray());

      // Get the tested maxes.
      ushort actualMax = Maths.Max(tests);
      ushort actualMaxNG = Maths.MaxNG(tests.ToArray());

      // Assert that all values match.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMin, actualMinNG);
      Assert.AreEqual(expectedMax, actualMax);
      Assert.AreEqual(expectedMax, actualMaxNG);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(int, int)"/> and <see cref="Maths.Max(int, int)"/>.
    /// </summary>
    /// <param name="valueA">The first value to compare.</param>
    /// <param name="valueB">The second value to compare.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMaxSolo_Int_ReturnsAccurate([Random(-100, 100, 5)] int valueA,
                                               [Random(-100, 100, 5)] int valueB)
    {
      // Get the expected values.
      int expectedMin = valueA < valueB ? valueA : valueB;
      int expectedMax = valueA > valueB ? valueA : valueB;

      // Get the min and maxes.
      int actualMin = Maths.Min(valueA, valueB);
      int actualMax = Maths.Max(valueA, valueB);

      // Test the min and max.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMax, actualMax);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(int[])"/>, <see cref="Maths.Min(IList{int})"/>,
    /// <see cref="Maths.Max(int[])"/>, and <see cref="Maths.Max(IList{int})"/>.
    /// </summary>
    /// <param name="generatedValues">The number of random values to test.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMax_Int_ReturnsAccurate([Random(2, 50, 50)] int generatedValues)
    {
      int expectedMin = int.MaxValue; // Hold the expected min.
      int expectedMax = int.MinValue; // Hold the expected max.

      // Fill a randomly sized list with random values.
      Random random = new System.Random();
      List<int> tests = new List<int>(generatedValues);
      for (int i = 0; i < generatedValues; i++)
      {
        int value = random.Next(-100, 100);
        tests.Add(value);

        // Calculate the current expected min.
        if (value < expectedMin)
          expectedMin = value;

        // Calculate the current expected max.
        if (value > expectedMax)
          expectedMax = value;
      }

      // Get the tested mins.
      int actualMin = Maths.Min(tests);
      int actualMinNG = Maths.MinNG(tests.ToArray());

      // Get the tested maxes.
      int actualMax = Maths.Max(tests);
      int actualMaxNG = Maths.MaxNG(tests.ToArray());

      // Assert that all values match.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMin, actualMinNG);
      Assert.AreEqual(expectedMax, actualMax);
      Assert.AreEqual(expectedMax, actualMaxNG);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(uint, uint)"/> and <see cref="Maths.Max(uint, uint)"/>.
    /// </summary>
    /// <param name="valueA">The first value to compare.</param>
    /// <param name="valueB">The second value to compare.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMaxSolo_UInt_ReturnsAccurate([Random(0, 100, 5)] int valueA,
                                                [Random(0, 100, 5)] int valueB)
    {
      // Get the expected values.
      uint expectedMin = (uint)(valueA < valueB ? valueA : valueB);
      uint expectedMax = (uint)(valueA > valueB ? valueA : valueB);

      // Get the min and maxes.
      uint actualMin = Maths.Min((uint)valueA, (uint)valueB);
      uint actualMax = Maths.Max((uint)valueA, (uint)valueB);

      // Test the min and max.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMax, actualMax);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(uint[])"/>, <see cref="Maths.Min(IList{uint})"/>,
    /// <see cref="Maths.Max(uint[])"/>, and <see cref="Maths.Max(IList{uint})"/>.
    /// </summary>
    /// <param name="generatedValues">The number of random values to test.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMax_UInt_ReturnsAccurate([Random(2, 50, 50)] int generatedValues)
    {
      uint expectedMin = uint.MaxValue; // Hold the expected min.
      uint expectedMax = uint.MinValue; // Hold the expected max.

      // Fill a randomly sized list with random values.
      Random random = new System.Random();
      List<uint> tests = new List<uint>(generatedValues);
      for (int i = 0; i < generatedValues; i++)
      {
        uint value = (uint)random.Next(0, 100);
        tests.Add(value);

        // Calculate the current expected min.
        if (value < expectedMin)
          expectedMin = value;

        // Calculate the current expected max.
        if (value > expectedMax)
          expectedMax = value;
      }

      // Get the tested mins.
      uint actualMin = Maths.Min(tests);
      uint actualMinNG = Maths.MinNG(tests.ToArray());

      // Get the tested maxes.
      uint actualMax = Maths.Max(tests);
      uint actualMaxNG = Maths.MaxNG(tests.ToArray());

      // Assert that all values match.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMin, actualMinNG);
      Assert.AreEqual(expectedMax, actualMax);
      Assert.AreEqual(expectedMax, actualMaxNG);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(long, long)"/> and <see cref="Maths.Max(long, long)"/>.
    /// </summary>
    /// <param name="valueA">The first value to compare.</param>
    /// <param name="valueB">The second value to compare.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMaxSolo_Long_ReturnsAccurate([Random(-100, 100, 5)] int valueA,
                                                [Random(-100, 100, 5)] int valueB)
    {
      // Get the expected values.
      long expectedMin = valueA < valueB ? valueA : valueB;
      long expectedMax = valueA > valueB ? valueA : valueB;

      // Get the min and maxes.
      long actualMin = Maths.Min(valueA, valueB);
      long actualMax = Maths.Max(valueA, valueB);

      // Test the min and max.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMax, actualMax);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(long[])"/>, <see cref="Maths.Min(IList{long})"/>,
    /// <see cref="Maths.Max(long[])"/>, and <see cref="Maths.Max(IList{long})"/>.
    /// </summary>
    /// <param name="generatedValues">The number of random values to test.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMax_Long_ReturnsAccurate([Random(2, 50, 50)] int generatedValues)
    {
      long expectedMin = long.MaxValue; // Hold the expected min.
      long expectedMax = long.MinValue; // Hold the expected max.

      // Fill a randomly sized list with random values.
      Random random = new System.Random();
      List<long> tests = new List<long>(generatedValues);
      for (int i = 0; i < generatedValues; i++)
      {
        long value = random.Next(-100, 100);
        tests.Add(value);

        // Calculate the current expected min.
        if (value < expectedMin)
          expectedMin = value;

        // Calculate the current expected max.
        if (value > expectedMax)
          expectedMax = value;
      }

      // Get the tested mins.
      long actualMin = Maths.Min(tests);
      long actualMinNG = Maths.MinNG(tests.ToArray());

      // Get the tested maxes.
      long actualMax = Maths.Max(tests);
      long actualMaxNG = Maths.MaxNG(tests.ToArray());

      // Assert that all values match.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMin, actualMinNG);
      Assert.AreEqual(expectedMax, actualMax);
      Assert.AreEqual(expectedMax, actualMaxNG);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(ulong, ulong)"/> and <see cref="Maths.Max(ulong, ulong)"/>.
    /// </summary>
    /// <param name="valueA">The first value to compare.</param>
    /// <param name="valueB">The second value to compare.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMaxSolo_ULong_ReturnsAccurate([Random(0, 100, 5)] int valueA,
                                                 [Random(0, 100, 5)] int valueB)
    {
      // Get the expected values.
      ulong expectedMin = (ulong)(valueA < valueB ? valueA : valueB);
      ulong expectedMax = (ulong)(valueA > valueB ? valueA : valueB);

      // Get the min and maxes.
      ulong actualMin = Maths.Min((ulong)valueA, (ulong)valueB);
      ulong actualMax = Maths.Max((ulong)valueA, (ulong)valueB);

      // Test the min and max.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMax, actualMax);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(ulong[])"/>, <see cref="Maths.Min(IList{ulong})"/>,
    /// <see cref="Maths.Max(ulong[])"/>, and <see cref="Maths.Max(IList{ulong})"/>.
    /// </summary>
    /// <param name="generatedValues">The number of random values to test.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMax_ULong_ReturnsAccurate([Random(2, 50, 50)] int generatedValues)
    {
      ulong expectedMin = ulong.MaxValue; // Hold the expected min.
      ulong expectedMax = ulong.MinValue; // Hold the expected max.

      // Fill a randomly sized list with random values.
      Random random = new System.Random();
      List<ulong> tests = new List<ulong>(generatedValues);
      for (int i = 0; i < generatedValues; i++)
      {
        ulong value = (ulong)random.Next(0, 100);
        tests.Add(value);

        // Calculate the current expected min.
        if (value < expectedMin)
          expectedMin = value;

        // Calculate the current expected max.
        if (value > expectedMax)
          expectedMax = value;
      }

      // Get the tested mins.
      ulong actualMin = Maths.Min(tests);
      ulong actualMinNG = Maths.MinNG(tests.ToArray());

      // Get the tested maxes.
      ulong actualMax = Maths.Max(tests);
      ulong actualMaxNG = Maths.MaxNG(tests.ToArray());

      // Assert that all values match.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMin, actualMinNG);
      Assert.AreEqual(expectedMax, actualMax);
      Assert.AreEqual(expectedMax, actualMaxNG);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(float, float)"/> and <see cref="Maths.Max(float, float)"/>.
    /// </summary>
    /// <param name="valueA">The first value to compare.</param>
    /// <param name="valueB">The second value to compare.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMaxSolo_Float_ReturnsAccurate([Random(-100.0f, 100.0f, 5)] float valueA,
                                                 [Random(-100.0f, 100.0f, 5)] float valueB)
    {
      // Get the expected values.
      float expectedMin = valueA < valueB ? valueA : valueB;
      float expectedMax = valueA > valueB ? valueA : valueB;

      // Get the min and maxes.
      float actualMin = Maths.Min(valueA, valueB);
      float actualMax = Maths.Max(valueA, valueB);

      // Test the min and max.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMax, actualMax);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(float[])"/>, <see cref="Maths.Min(IList{float})"/>,
    /// <see cref="Maths.Max(float[])"/>, and <see cref="Maths.Max(IList{float})"/>.
    /// </summary>
    /// <param name="generatedValues">The number of random values to test.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMax_Float_ReturnsAccurate([Random(2, 50, 50)] int generatedValues)
    {
      float expectedMin = float.MaxValue; // Hold the expected min.
      float expectedMax = float.MinValue; // Hold the expected max.

      // Fill a randomly sized list with random values.
      Random random = new System.Random();
      List<float> tests = new List<float>(generatedValues);
      for (float i = 0; i < generatedValues; i++)
      {
        float value = (float)random.NextDouble();
        tests.Add(value);

        // Calculate the current expected min.
        if (value < expectedMin)
          expectedMin = value;

        // Calculate the current expected max.
        if (value > expectedMax)
          expectedMax = value;
      }

      // Get the tested mins.
      float actualMin = Maths.Min(tests);
      float actualMinNG = Maths.MinNG(tests.ToArray());

      // Get the tested maxes.
      float actualMax = Maths.Max(tests);
      float actualMaxNG = Maths.MaxNG(tests.ToArray());

      // Assert that all values match.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMin, actualMinNG);
      Assert.AreEqual(expectedMax, actualMax);
      Assert.AreEqual(expectedMax, actualMaxNG);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(double, double)"/> and <see cref="Maths.Max(double, double)"/>.
    /// </summary>
    /// <param name="valueA">The first value to compare.</param>
    /// <param name="valueB">The second value to compare.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMaxSolo_Double_ReturnsAccurate([Random(-100.0f, 100.0f, 5)] double valueA,
                                                  [Random(-100.0f, 100.0f, 5)] double valueB)
    {
      // Get the expected values.
      double expectedMin = valueA < valueB ? valueA : valueB;
      double expectedMax = valueA > valueB ? valueA : valueB;

      // Get the min and maxes.
      double actualMin = Maths.Min(valueA, valueB);
      double actualMax = Maths.Max(valueA, valueB);

      // Test the min and max.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMax, actualMax);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(double[])"/>, <see cref="Maths.Min(IList{double})"/>,
    /// <see cref="Maths.Max(double[])"/>, and <see cref="Maths.Max(IList{double})"/>.
    /// </summary>
    /// <param name="generatedValues">The number of random values to test.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMax_Double_ReturnsAccurate([Random(2, 50, 50)] int generatedValues)
    {
      double expectedMin = double.MaxValue; // Hold the expected min.
      double expectedMax = double.MinValue; // Hold the expected max.

      // Fill a randomly sized list with random values.
      Random random = new System.Random();
      List<double> tests = new List<double>(generatedValues);
      for (double i = 0; i < generatedValues; i++)
      {
        double value = random.NextDouble();
        tests.Add(value);

        // Calculate the current expected min.
        if (value < expectedMin)
          expectedMin = value;

        // Calculate the current expected max.
        if (value > expectedMax)
          expectedMax = value;
      }

      // Get the tested mins.
      double actualMin = Maths.Min(tests);
      double actualMinNG = Maths.MinNG(tests.ToArray());

      // Get the tested maxes.
      double actualMax = Maths.Max(tests);
      double actualMaxNG = Maths.MaxNG(tests.ToArray());

      // Assert that all values match.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMin, actualMinNG);
      Assert.AreEqual(expectedMax, actualMax);
      Assert.AreEqual(expectedMax, actualMaxNG);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(decimal, decimal)"/> and <see cref="Maths.Max(decimal, decimal)"/>.
    /// </summary>
    /// <param name="valueA">The first value to compare.</param>
    /// <param name="valueB">The second value to compare.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMaxSolo_Decimal_ReturnsAccurate([Random(-100.0f, 100.0f, 5)] float valueA,
                                                   [Random(-100.0f, 100.0f, 5)] float valueB)
    {
      decimal dvalueA = Convert.ToDecimal(valueA);
      decimal dvalueB = Convert.ToDecimal(valueB);

      // Get the expected values.
      decimal expectedMin = dvalueA < dvalueB ? dvalueA : dvalueB;
      decimal expectedMax = dvalueA > dvalueB ? dvalueA : dvalueB;

      // Get the min and maxes.
      decimal actualMin = Maths.Min(dvalueA, dvalueB);
      decimal actualMax = Maths.Max(dvalueA, dvalueB);

      // Test the min and max.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMax, actualMax);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(decimal[])"/>, <see cref="Maths.Min(IList{decimal})"/>,
    /// <see cref="Maths.Max(decimal[])"/>, and <see cref="Maths.Max(IList{decimal})"/>.
    /// </summary>
    /// <param name="generatedValues">The number of random values to test.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMax_Decimal_ReturnsAccurate([Random(2, 50, 50)] int generatedValues)
    {
      decimal expectedMin = decimal.MaxValue; // Hold the expected min.
      decimal expectedMax = decimal.MinValue; // Hold the expected max.

      // Fill a randomly sized list with random values.
      Random random = new System.Random();
      List<decimal> tests = new List<decimal>(generatedValues);
      for (decimal i = 0; i < generatedValues; i++)
      {
        decimal value = Convert.ToDecimal(random.NextDouble());
        tests.Add(value);

        // Calculate the current expected min.
        if (value < expectedMin)
          expectedMin = value;

        // Calculate the current expected max.
        if (value > expectedMax)
          expectedMax = value;
      }

      // Get the tested mins.
      decimal actualMin = Maths.Min(tests);
      decimal actualMinNG = Maths.MinNG(tests.ToArray());

      // Get the tested maxes.
      decimal actualMax = Maths.Max(tests);
      decimal actualMaxNG = Maths.MaxNG(tests.ToArray());

      // Assert that all values match.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMin, actualMinNG);
      Assert.AreEqual(expectedMax, actualMax);
      Assert.AreEqual(expectedMax, actualMaxNG);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(char, char)"/> and <see cref="Maths.Max(char, char)"/>.
    /// </summary>
    /// <param name="valueA">The first value to compare.</param>
    /// <param name="valueB">The second value to compare.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMaxSolo_Char_ReturnsAccurate([Random(-100, 100, 5)] int valueA,
                                                [Random(-100, 100, 5)] int valueB)
    {
      // Get the expected values.
      char expectedMin = (char)valueA < (char)valueB ? (char)valueA : (char)valueB;
      char expectedMax = (char)valueA > (char)valueB ? (char)valueA : (char)valueB;

      // Get the min and maxes.
      char actualMin = Maths.Min((char)valueA, (char)valueB);
      char actualMax = Maths.Max((char)valueA, (char)valueB);

      // Test the min and max.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMax, actualMax);
    }

    /// <summary>
    /// A test for <see cref="Maths.Min(char[])"/>, <see cref="Maths.Min(IList{char})"/>,
    /// <see cref="Maths.Max(char[])"/>, and <see cref="Maths.Max(IList{char})"/>.
    /// </summary>
    /// <param name="generatedValues">The number of random values to test.</param>
    [Test(TestOf = typeof(Maths))]
    public void MinMax_Char_ReturnsAccurate([Random(2, 50, 50)] int generatedValues)
    {
      char expectedMin = char.MaxValue; // Hold the expected min.
      char expectedMax = char.MinValue; // Hold the expected max.

      // Fill a randomly sized list with random values.
      Random random = new System.Random();
      List<char> tests = new List<char>(generatedValues);
      for (int i = 0; i < generatedValues; i++)
      {
        char value = (char)random.Next(-100, 100);
        tests.Add(value);

        // Calculate the current expected min.
        if (value < expectedMin)
          expectedMin = value;

        // Calculate the current expected max.
        if (value > expectedMax)
          expectedMax = value;
      }

      // Get the tested mins.
      char actualMin = Maths.Min(tests);
      char actualMinNG = Maths.MinNG(tests.ToArray());

      // Get the tested maxes.
      char actualMax = Maths.Max(tests);
      char actualMaxNG = Maths.MaxNG(tests.ToArray());

      // Assert that all values match.
      Assert.AreEqual(expectedMin, actualMin);
      Assert.AreEqual(expectedMin, actualMinNG);
      Assert.AreEqual(expectedMax, actualMax);
      Assert.AreEqual(expectedMax, actualMaxNG);
    }
  }
  /************************************************************************************************/
}