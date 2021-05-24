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
  }
  /************************************************************************************************/
}