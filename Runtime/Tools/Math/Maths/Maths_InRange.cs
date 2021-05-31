/**************************************************************************************************/
/*!
\file   Maths_InRange.cs
\author Craig Williams
\par    Last Updated
        2021-05-24
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for tool functions related to checking if a value is within range of two other values.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System;
using System.Numerics;

namespace CodeParadox.Tenor.Tools
{
  /************************************************************************************************/
  public static partial class Maths
  {
    /// <summary>
    /// A function that determines if a value is within a range of [min, max].
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="IComparable"/> value.</typeparam>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeII<T>(T value, T min, T max) where T : IComparable<T>
    {
      return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max).
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="IComparable"/> value.</typeparam>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEE<T>(T value, T min, T max) where T : IComparable<T>
    {
      return value.CompareTo(min) > 0 && value.CompareTo(max) < 0;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max).
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="IComparable"/> value.</typeparam>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeIE<T>(T value, T min, T max) where T : IComparable<T>
    {
      return value.CompareTo(min) >= 0 && value.CompareTo(max) < 0;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max].
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="IComparable"/> value.</typeparam>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEI<T>(T value, T min, T max) where T : IComparable<T>
    {
      return value.CompareTo(min) > 0 && value.CompareTo(max) <= 0;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max].
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="IComparable"/> value.</typeparam>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeIING<T>(T value, T min, T max) where T : IComparable
    {
      return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max).
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="IComparable"/> value.</typeparam>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEENG<T>(T value, T min, T max) where T : IComparable
    {
      return value.CompareTo(min) > 0 && value.CompareTo(max) < 0;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max).
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="IComparable"/> value.</typeparam>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeIENG<T>(T value, T min, T max) where T : IComparable
    {
      return value.CompareTo(min) >= 0 && value.CompareTo(max) < 0;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max].
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="IComparable"/> value.</typeparam>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEING<T>(T value, T min, T max) where T : IComparable
    {
      return value.CompareTo(min) > 0 && value.CompareTo(max) <= 0;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeII(sbyte value, sbyte min, sbyte max)
    {
      return value >= min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEE(sbyte value, sbyte min, sbyte max)
    {
      return value > min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeIE(sbyte value, sbyte min, sbyte max)
    {
      return value >= min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEI(sbyte value, sbyte min, sbyte max)
    {
      return value > min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeII(byte value, byte min, byte max)
    {
      return value >= min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEE(byte value, byte min, byte max)
    {
      return value > min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeIE(byte value, byte min, byte max)
    {
      return value >= min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEI(byte value, byte min, byte max)
    {
      return value > min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeII(short value, short min, short max)
    {
      return value >= min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEE(short value, short min, short max)
    {
      return value > min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeIE(short value, short min, short max)
    {
      return value >= min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEI(short value, short min, short max)
    {
      return value > min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeII(ushort value, ushort min, ushort max)
    {
      return value >= min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEE(ushort value, ushort min, ushort max)
    {
      return value > min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeIE(ushort value, ushort min, ushort max)
    {
      return value >= min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEI(ushort value, ushort min, ushort max)
    {
      return value > min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeII(int value, int min, int max)
    {
      return value >= min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEE(int value, int min, int max)
    {
      return value > min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeIE(int value, int min, int max)
    {
      return value >= min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEI(int value, int min, int max)
    {
      return value > min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeII(uint value, uint min, uint max)
    {
      return value >= min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEE(uint value, uint min, uint max)
    {
      return value > min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeIE(uint value, uint min, uint max)
    {
      return value >= min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEI(uint value, uint min, uint max)
    {
      return value > min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeII(long value, long min, long max)
    {
      return value >= min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEE(long value, long min, long max)
    {
      return value > min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeIE(long value, long min, long max)
    {
      return value >= min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEI(long value, long min, long max)
    {
      return value > min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeII(ulong value, ulong min, ulong max)
    {
      return value >= min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEE(ulong value, ulong min, ulong max)
    {
      return value > min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeIE(ulong value, ulong min, ulong max)
    {
      return value >= min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEI(ulong value, ulong min, ulong max)
    {
      return value > min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeII(float value, float min, float max)
    {
      return value >= min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEE(float value, float min, float max)
    {
      return value > min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeIE(float value, float min, float max)
    {
      return value >= min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEI(float value, float min, float max)
    {
      return value > min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeII(double value, double min, double max)
    {
      return value >= min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEE(double value, double min, double max)
    {
      return value > min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeIE(double value, double min, double max)
    {
      return value >= min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEI(double value, double min, double max)
    {
      return value > min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeII(decimal value, decimal min, decimal max)
    {
      return value >= min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEE(decimal value, decimal min, decimal max)
    {
      return value > min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeIE(decimal value, decimal min, decimal max)
    {
      return value >= min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEI(decimal value, decimal min, decimal max)
    {
      return value > min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeII(char value, char min, char max)
    {
      return value >= min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEE(char value, char min, char max)
    {
      return value > min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeIE(char value, char min, char max)
    {
      return value >= min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEI(char value, char min, char max)
    {
      return value > min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeII(BigInteger value, BigInteger min, BigInteger max)
    {
      return value >= min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEE(BigInteger value, BigInteger min, BigInteger max)
    {
      return value > min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeIE(BigInteger value, BigInteger min, BigInteger max)
    {
      return value >= min && value < max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEI(BigInteger value, BigInteger min, BigInteger max)
    {
      return value > min && value <= max;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeII(Matrix3x2 value, Matrix3x2 min, Matrix3x2 max)
    {
      return value.M11 >= min.M11 && value.M11 <= max.M11 &&
             value.M12 >= min.M12 && value.M12 <= max.M12 &&
             value.M21 >= min.M21 && value.M21 <= max.M21 &&
             value.M22 >= min.M22 && value.M22 <= max.M22 &&
             value.M31 >= min.M31 && value.M31 <= max.M31 &&
             value.M32 >= min.M32 && value.M32 <= max.M32;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEE(Matrix3x2 value, Matrix3x2 min, Matrix3x2 max)
    {
      return value.M11 > min.M11 && value.M11 < max.M11 &&
             value.M12 > min.M12 && value.M12 < max.M12 &&
             value.M21 > min.M21 && value.M21 < max.M21 &&
             value.M22 > min.M22 && value.M22 < max.M22 &&
             value.M31 > min.M31 && value.M31 < max.M31 &&
             value.M32 > min.M32 && value.M32 < max.M32;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeIE(Matrix3x2 value, Matrix3x2 min, Matrix3x2 max)
    {
      return value.M11 >= min.M11 && value.M11 < max.M11 &&
             value.M12 >= min.M12 && value.M12 < max.M12 &&
             value.M21 >= min.M21 && value.M21 < max.M21 &&
             value.M22 >= min.M22 && value.M22 < max.M22 &&
             value.M31 >= min.M31 && value.M31 < max.M31 &&
             value.M32 >= min.M32 && value.M32 < max.M32;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEI(Matrix3x2 value, Matrix3x2 min, Matrix3x2 max)
    {
      return value.M11 > min.M11 && value.M11 <= max.M11 &&
             value.M12 > min.M12 && value.M12 <= max.M12 &&
             value.M21 > min.M21 && value.M21 <= max.M21 &&
             value.M22 > min.M22 && value.M22 <= max.M22 &&
             value.M31 > min.M31 && value.M31 <= max.M31 &&
             value.M32 > min.M32 && value.M32 <= max.M32;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeII(Matrix4x4 value, Matrix4x4 min, Matrix4x4 max)
    {
      return value.M11 >= min.M11 && value.M11 <= max.M11 &&
             value.M12 >= min.M12 && value.M12 <= max.M12 &&
             value.M13 >= min.M13 && value.M13 <= max.M13 &&
             value.M14 >= min.M14 && value.M14 <= max.M14 &&
             value.M21 >= min.M21 && value.M21 <= max.M21 &&
             value.M22 >= min.M22 && value.M22 <= max.M22 &&
             value.M23 >= min.M23 && value.M23 <= max.M23 &&
             value.M24 >= min.M24 && value.M24 <= max.M24 &&
             value.M31 >= min.M31 && value.M31 <= max.M31 &&
             value.M32 >= min.M32 && value.M32 <= max.M32 &&
             value.M33 >= min.M33 && value.M33 <= max.M33 &&
             value.M34 >= min.M34 && value.M34 <= max.M34 &&
             value.M41 >= min.M41 && value.M41 <= max.M41 &&
             value.M42 >= min.M42 && value.M42 <= max.M42 &&
             value.M43 >= min.M43 && value.M43 <= max.M43 &&
             value.M44 >= min.M44 && value.M44 <= max.M44;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEE(Matrix4x4 value, Matrix4x4 min, Matrix4x4 max)
    {
      return value.M11 > min.M11 && value.M11 < max.M11 &&
             value.M12 > min.M12 && value.M12 < max.M12 &&
             value.M13 > min.M13 && value.M13 < max.M13 &&
             value.M14 > min.M14 && value.M14 < max.M14 &&
             value.M21 > min.M21 && value.M21 < max.M21 &&
             value.M22 > min.M22 && value.M22 < max.M22 &&
             value.M23 > min.M23 && value.M23 < max.M23 &&
             value.M24 > min.M24 && value.M24 < max.M24 &&
             value.M31 > min.M31 && value.M31 < max.M31 &&
             value.M32 > min.M32 && value.M32 < max.M32 &&
             value.M33 > min.M33 && value.M33 < max.M33 &&
             value.M34 > min.M34 && value.M34 < max.M34 &&
             value.M41 > min.M41 && value.M41 < max.M41 &&
             value.M42 > min.M42 && value.M42 < max.M42 &&
             value.M43 > min.M43 && value.M43 < max.M43 &&
             value.M44 > min.M44 && value.M44 < max.M44;
    }

    /// <summary>
    /// A function that determines if a value is within a range of [min, max).
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeIE(Matrix4x4 value, Matrix4x4 min, Matrix4x4 max)
    {
      return value.M11 >= min.M11 && value.M11 < max.M11 &&
             value.M12 >= min.M12 && value.M12 < max.M12 &&
             value.M13 >= min.M13 && value.M13 < max.M13 &&
             value.M14 >= min.M14 && value.M14 < max.M14 &&
             value.M21 >= min.M21 && value.M21 < max.M21 &&
             value.M22 >= min.M22 && value.M22 < max.M22 &&
             value.M23 >= min.M23 && value.M23 < max.M23 &&
             value.M24 >= min.M24 && value.M24 < max.M24 &&
             value.M31 >= min.M31 && value.M31 < max.M31 &&
             value.M32 >= min.M32 && value.M32 < max.M32 &&
             value.M33 >= min.M33 && value.M33 < max.M33 &&
             value.M34 >= min.M34 && value.M34 < max.M34 &&
             value.M41 >= min.M41 && value.M41 < max.M41 &&
             value.M42 >= min.M42 && value.M42 < max.M42 &&
             value.M43 >= min.M43 && value.M43 < max.M43 &&
             value.M44 >= min.M44 && value.M44 < max.M44;
    }

    /// <summary>
    /// A function that determines if a value is within a range of (min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns if <paramref name="value"/> is within the wanted range.</returns>
    public static bool InRangeEI(Matrix4x4 value, Matrix4x4 min, Matrix4x4 max)
    {
      return value.M11 > min.M11 && value.M11 <= max.M11 &&
             value.M12 > min.M12 && value.M12 <= max.M12 &&
             value.M13 > min.M13 && value.M13 <= max.M13 &&
             value.M14 > min.M14 && value.M14 <= max.M14 &&
             value.M21 > min.M21 && value.M21 <= max.M21 &&
             value.M22 > min.M22 && value.M22 <= max.M22 &&
             value.M23 > min.M23 && value.M23 <= max.M23 &&
             value.M24 > min.M24 && value.M24 <= max.M24 &&
             value.M31 > min.M31 && value.M31 <= max.M31 &&
             value.M32 > min.M32 && value.M32 <= max.M32 &&
             value.M33 > min.M33 && value.M33 <= max.M33 &&
             value.M34 > min.M34 && value.M34 <= max.M34 &&
             value.M41 > min.M41 && value.M41 <= max.M41 &&
             value.M42 > min.M42 && value.M42 <= max.M42 &&
             value.M43 > min.M43 && value.M43 <= max.M43 &&
             value.M44 > min.M44 && value.M44 <= max.M44;
    }
  }
  /************************************************************************************************/
}