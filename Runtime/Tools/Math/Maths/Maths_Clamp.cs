/**************************************************************************************************/
/*!
\file   Maths_Clamp.cs
\author Craig Williams
\par    Last Updated
        2021-05-22
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for tool functions related to clamping a value between two extremes. This is a part
  of the Maths toolkit.

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
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="IComparable"/> value.</typeparam>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static T ComparableClampII<T>(T value, T min, T max) where T : IComparable<T>
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        T v when v.CompareTo(min) < 0 => min,
        // If greater than max, return max.
        T v when v.CompareTo(max) > 0 => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="IComparable"/> value.</typeparam>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static T ComparableClampIING<T>(T value, T min, T max) where T : IComparable
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        T v when v.CompareTo(min) < 0 => min,
        // If greater than max, return max.
        T v when v.CompareTo(max) > 0 => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static sbyte ClampII(sbyte value, sbyte min, sbyte max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        sbyte v when v < min => min,
        // If greater than max, return max.
        sbyte v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static sbyte ClampEE(sbyte value, sbyte min, sbyte max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        sbyte v when v <= min => (sbyte)(min + 1),
        // If greater than or equal to max, return max - the smallest decrement.
        sbyte v when v >= max => (sbyte)(max - 1),
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static sbyte ClampIE(sbyte value, sbyte min, sbyte max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        sbyte v when v < min => min,
        // If greater than or equal to max, return max - the smallest decrement.
        sbyte v when v >= max => (sbyte)(max - 1),
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static sbyte ClampEI(sbyte value, sbyte min, sbyte max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        sbyte v when v <= min => (sbyte)(min + 1),
        // If greater than max, return max.
        sbyte v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static byte ClampII(byte value, byte min, byte max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        byte v when v < min => min,
        // If greater than max, return max.
        byte v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static byte ClampEE(byte value, byte min, byte max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        byte v when v <= min => (byte)(min + 1),
        // If greater than or equal to max, return max - the smallest decrement.
        byte v when v >= max => (byte)(max - 1),
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static byte ClampIE(byte value, byte min, byte max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        byte v when v < min => min,
        // If greater than or equal to max, return max - the smallest decrement.
        byte v when v >= max => (byte)(max - 1),
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static byte ClampEI(byte value, byte min, byte max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        byte v when v <= min => (byte)(min + 1),
        // If greater than max, return max.
        byte v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static short ClampII(short value, short min, short max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        short v when v < min => min,
        // If greater than max, return max.
        short v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static short ClampEE(short value, short min, short max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        short v when v <= min => (short)(min + 1),
        // If greater than or equal to max, return max - the smallest decrement.
        short v when v >= max => (short)(max - 1),
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static short ClampIE(short value, short min, short max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        short v when v < min => min,
        // If greater than or equal to max, return max - the smallest decrement.
        short v when v >= max => (short)(max - 1),
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static short ClampEI(short value, short min, short max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        short v when v <= min => (short)(min + 1),
        // If greater than max, return max.
        short v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static ushort ClampII(ushort value, ushort min, ushort max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        ushort v when v < min => min,
        // If greater than max, return max.
        ushort v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static ushort ClampEE(ushort value, ushort min, ushort max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        ushort v when v <= min => (ushort)(min + 1),
        // If greater than or equal to max, return max - the smallest decrement.
        ushort v when v >= max => (ushort)(max - 1),
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static ushort ClampIE(ushort value, ushort min, ushort max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        ushort v when v < min => min,
        // If greater than or equal to max, return max - the smallest decrement.
        ushort v when v >= max => (ushort)(max - 1),
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static ushort ClampEI(ushort value, ushort min, ushort max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        ushort v when v <= min => (ushort)(min + 1),
        // If greater than max, return max.
        ushort v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static int ClampII(int value, int min, int max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        int v when v < min => min,
        // If greater than max, return max.
        int v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static int ClampEE(int value, int min, int max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        int v when v <= min => min + 1,
        // If greater than or equal to max, return max - the smallest decrement.
        int v when v >= max => max - 1,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static int ClampIE(int value, int min, int max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        int v when v < min => min,
        // If greater than or equal to max, return max - the smallest decrement.
        int v when v >= max => max - 1,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static int ClampEI(int value, int min, int max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        int v when v <= min => min + 1,
        // If greater than max, return max.
        int v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static uint ClampII(uint value, uint min, uint max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        uint v when v < min => min,
        // If greater than max, return max.
        uint v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static uint ClampEE(uint value, uint min, uint max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        uint v when v <= min => min + 1,
        // If greater than or equal to max, return max - the smallest decrement.
        uint v when v >= max => max - 1,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static uint ClampIE(uint value, uint min, uint max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        uint v when v < min => min,
        // If greater than or equal to max, return max - the smallest decrement.
        uint v when v >= max => max - 1,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static uint ClampEI(uint value, uint min, uint max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        uint v when v <= min => min + 1,
        // If greater than max, return max.
        uint v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static long ClampII(long value, long min, long max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        long v when v < min => min,
        // If greater than max, return max.
        long v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static long ClampEE(long value, long min, long max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        long v when v <= min => min + 1,
        // If greater than or equal to max, return max - the smallest decrement.
        long v when v >= max => max - 1,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static long ClampIE(long value, long min, long max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        long v when v < min => min,
        // If greater than or equal to max, return max - the smallest decrement.
        long v when v >= max => max - 1,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static long ClampEI(long value, long min, long max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        long v when v <= min => min + 1,
        // If greater than max, return max.
        long v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static ulong ClampII(ulong value, ulong min, ulong max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        ulong v when v < min => min,
        // If greater than max, return max.
        ulong v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static ulong ClampEE(ulong value, ulong min, ulong max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        ulong v when v <= min => min + 1,
        // If greater than or equal to max, return max - the smallest decrement.
        ulong v when v >= max => max - 1,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static ulong ClampIE(ulong value, ulong min, ulong max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        ulong v when v < min => min,
        // If greater than or equal to max, return max - the smallest decrement.
        ulong v when v >= max => max - 1,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static ulong ClampEI(ulong value, ulong min, ulong max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        ulong v when v <= min => min + 1,
        // If greater than max, return max.
        ulong v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static float ClampII(float value, float min, float max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        float v when v < min => min,
        // If greater than max, return max.
        float v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static float ClampEE(float value, float min, float max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        float v when v <= min => min + float.Epsilon,
        // If greater than or equal to max, return max - the smallest decrement.
        float v when v >= max => max - float.Epsilon,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static float ClampIE(float value, float min, float max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        float v when v < min => min,
        // If greater than or equal to max, return max - the smallest decrement.
        float v when v >= max => max - float.Epsilon,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static float ClampEI(float value, float min, float max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        float v when v <= min => min + float.Epsilon,
        // If greater than max, return max.
        float v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static double ClampII(double value, double min, double max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        double v when v < min => min,
        // If greater than max, return max.
        double v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static double ClampEE(double value, double min, double max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        double v when v <= min => min + double.Epsilon,
        // If greater than or equal to max, return max - the smallest decrement.
        double v when v >= max => max - double.Epsilon,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static double ClampIE(double value, double min, double max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        double v when v < min => min,
        // If greater than or equal to max, return max - the smallest decrement.
        double v when v >= max => max - double.Epsilon,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static double ClampEI(double value, double min, double max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        double v when v <= min => min + double.Epsilon,
        // If greater than max, return max.
        double v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static decimal ClampII(decimal value, decimal min, decimal max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        decimal v when v < min => min,
        // If greater than max, return max.
        decimal v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static decimal ClampEE(decimal value, decimal min, decimal max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        decimal v when v <= min => min + (decimal)double.Epsilon,
        // If greater than or equal to max, return max - the smallest decrement.
        decimal v when v >= max => max - (decimal)double.Epsilon,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static decimal ClampIE(decimal value, decimal min, decimal max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        decimal v when v < min => min,
        // If greater than or equal to max, return max - the smallest decrement.
        decimal v when v >= max => max - (decimal)double.Epsilon,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static decimal ClampEI(decimal value, decimal min, decimal max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        decimal v when v <= min => min + (decimal)double.Epsilon,
        // If greater than max, return max.
        decimal v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static char ClampII(char value, char min, char max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        char v when v < min => min,
        // If greater than max, return max.
        char v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static char ClampEE(char value, char min, char max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        char v when v <= min => (char)(min + 1),
        // If greater than or equal to max, return max - the smallest decrement.
        char v when v >= max => (char)(max - 1),
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static char ClampIE(char value, char min, char max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        char v when v < min => min,
        // If greater than or equal to max, return max - the smallest decrement.
        char v when v >= max => (char)(max - 1),
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static char ClampEI(char value, char min, char max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        char v when v <= min => (char)(min + 1),
        // If greater than max, return max.
        char v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static BigInteger ClampII(BigInteger value, BigInteger min, BigInteger max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        BigInteger v when v < min => min,
        // If greater than max, return max.
        BigInteger v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static BigInteger ClampEE(BigInteger value, BigInteger min, BigInteger max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        BigInteger v when v <= min => (min + 1),
        // If greater than or equal to max, return max - the smallest decrement.
        BigInteger v when v >= max => (max - 1),
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static BigInteger ClampIE(BigInteger value, BigInteger min, BigInteger max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than min, return min.
        BigInteger v when v < min => min,
        // If greater than or equal to max, return max - the smallest decrement.
        BigInteger v when v >= max => (max - 1),
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static BigInteger ClampEI(BigInteger value, BigInteger min, BigInteger max)
    {
      // Switch on the value.
      return value switch
      {
        // If less than or equal to min, return min + the smallest increment.
        BigInteger v when v <= min => (min + 1),
        // If greater than max, return max.
        BigInteger v when v > max => max,
        // Default to the value.
        _ => value,
      };
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Complex ClampII(Complex value, Complex min, Complex max)
    {
      double real = ClampII(value.Real, min.Real, max.Real);
      double imaginary = ClampII(value.Imaginary, min.Imaginary, max.Imaginary);
      return new Complex(real, imaginary);
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Complex ClampEE(Complex value, Complex min, Complex max)
    {
      double real = ClampEE(value.Real, min.Real, max.Real);
      double imaginary = ClampEE(value.Imaginary, min.Imaginary, max.Imaginary);
      return new Complex(real, imaginary);
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Complex ClampIE(Complex value, Complex min, Complex max)
    {
      double real = ClampIE(value.Real, min.Real, max.Real);
      double imaginary = ClampIE(value.Imaginary, min.Imaginary, max.Imaginary);
      return new Complex(real, imaginary);
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Complex ClampEI(Complex value, Complex min, Complex max)
    {
      double real = ClampEI(value.Real, min.Real, max.Real);
      double imaginary = ClampEI(value.Imaginary, min.Imaginary, max.Imaginary);
      return new Complex(real, imaginary);
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Matrix3x2 ClampII(Matrix3x2 value, Matrix3x2 min, Matrix3x2 max)
    {
      value.M11 = ClampII(value.M11, min.M11, max.M11);
      value.M12 = ClampII(value.M12, min.M12, max.M12);
      value.M21 = ClampII(value.M21, min.M21, max.M21);
      value.M22 = ClampII(value.M22, min.M22, max.M22);
      value.M31 = ClampII(value.M31, min.M31, max.M31);
      value.M32 = ClampII(value.M32, min.M32, max.M32);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Matrix3x2 ClampEE(Matrix3x2 value, Matrix3x2 min, Matrix3x2 max)
    {
      value.M11 = ClampEE(value.M11, min.M11, max.M11);
      value.M12 = ClampEE(value.M12, min.M12, max.M12);
      value.M21 = ClampEE(value.M21, min.M21, max.M21);
      value.M22 = ClampEE(value.M22, min.M22, max.M22);
      value.M31 = ClampEE(value.M31, min.M31, max.M31);
      value.M32 = ClampEE(value.M32, min.M32, max.M32);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Matrix3x2 ClampIE(Matrix3x2 value, Matrix3x2 min, Matrix3x2 max)
    {
      value.M11 = ClampIE(value.M11, min.M11, max.M11);
      value.M12 = ClampIE(value.M12, min.M12, max.M12);
      value.M21 = ClampIE(value.M21, min.M21, max.M21);
      value.M22 = ClampIE(value.M22, min.M22, max.M22);
      value.M31 = ClampIE(value.M31, min.M31, max.M31);
      value.M32 = ClampIE(value.M32, min.M32, max.M32);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Matrix3x2 ClampEI(Matrix3x2 value, Matrix3x2 min, Matrix3x2 max)
    {
      value.M11 = ClampEI(value.M11, min.M11, max.M11);
      value.M12 = ClampEI(value.M12, min.M12, max.M12);
      value.M21 = ClampEI(value.M21, min.M21, max.M21);
      value.M22 = ClampEI(value.M22, min.M22, max.M22);
      value.M31 = ClampEI(value.M31, min.M31, max.M31);
      value.M32 = ClampEI(value.M32, min.M32, max.M32);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Matrix4x4 ClampII(Matrix4x4 value, Matrix4x4 min, Matrix4x4 max)
    {
      value.M11 = ClampII(value.M11, min.M11, max.M11);
      value.M12 = ClampII(value.M12, min.M12, max.M12);
      value.M13 = ClampII(value.M13, min.M13, max.M13);
      value.M14 = ClampII(value.M14, min.M14, max.M14);
      value.M21 = ClampII(value.M21, min.M21, max.M21);
      value.M22 = ClampII(value.M22, min.M22, max.M22);
      value.M23 = ClampII(value.M23, min.M23, max.M23);
      value.M24 = ClampII(value.M24, min.M24, max.M24);
      value.M31 = ClampII(value.M31, min.M31, max.M31);
      value.M32 = ClampII(value.M32, min.M32, max.M32);
      value.M33 = ClampII(value.M33, min.M33, max.M33);
      value.M34 = ClampII(value.M34, min.M34, max.M34);
      value.M41 = ClampII(value.M41, min.M41, max.M41);
      value.M42 = ClampII(value.M42, min.M42, max.M42);
      value.M43 = ClampII(value.M43, min.M43, max.M43);
      value.M44 = ClampII(value.M44, min.M44, max.M44);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Matrix4x4 ClampEE(Matrix4x4 value, Matrix4x4 min, Matrix4x4 max)
    {
      value.M11 = ClampEE(value.M11, min.M11, max.M11);
      value.M12 = ClampEE(value.M12, min.M12, max.M12);
      value.M13 = ClampEE(value.M13, min.M13, max.M13);
      value.M14 = ClampEE(value.M14, min.M14, max.M14);
      value.M21 = ClampEE(value.M21, min.M21, max.M21);
      value.M22 = ClampEE(value.M22, min.M22, max.M22);
      value.M23 = ClampEE(value.M23, min.M23, max.M23);
      value.M24 = ClampEE(value.M24, min.M24, max.M24);
      value.M31 = ClampEE(value.M31, min.M31, max.M31);
      value.M32 = ClampEE(value.M32, min.M32, max.M32);
      value.M33 = ClampEE(value.M33, min.M33, max.M33);
      value.M34 = ClampEE(value.M34, min.M34, max.M34);
      value.M41 = ClampEE(value.M41, min.M41, max.M41);
      value.M42 = ClampEE(value.M42, min.M42, max.M42);
      value.M43 = ClampEE(value.M43, min.M43, max.M43);
      value.M44 = ClampEE(value.M44, min.M44, max.M44);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Matrix4x4 ClampIE(Matrix4x4 value, Matrix4x4 min, Matrix4x4 max)
    {
      value.M11 = ClampIE(value.M11, min.M11, max.M11);
      value.M12 = ClampIE(value.M12, min.M12, max.M12);
      value.M13 = ClampIE(value.M13, min.M13, max.M13);
      value.M14 = ClampIE(value.M14, min.M14, max.M14);
      value.M21 = ClampIE(value.M21, min.M21, max.M21);
      value.M22 = ClampIE(value.M22, min.M22, max.M22);
      value.M23 = ClampIE(value.M23, min.M23, max.M23);
      value.M24 = ClampIE(value.M24, min.M24, max.M24);
      value.M31 = ClampIE(value.M31, min.M31, max.M31);
      value.M32 = ClampIE(value.M32, min.M32, max.M32);
      value.M33 = ClampIE(value.M33, min.M33, max.M33);
      value.M34 = ClampIE(value.M34, min.M34, max.M34);
      value.M41 = ClampIE(value.M41, min.M41, max.M41);
      value.M42 = ClampIE(value.M42, min.M42, max.M42);
      value.M43 = ClampIE(value.M43, min.M43, max.M43);
      value.M44 = ClampIE(value.M44, min.M44, max.M44);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Matrix4x4 ClampEI(Matrix4x4 value, Matrix4x4 min, Matrix4x4 max)
    {
      value.M11 = ClampEI(value.M11, min.M11, max.M11);
      value.M12 = ClampEI(value.M12, min.M12, max.M12);
      value.M13 = ClampEI(value.M13, min.M13, max.M13);
      value.M14 = ClampEI(value.M14, min.M14, max.M14);
      value.M21 = ClampEI(value.M21, min.M21, max.M21);
      value.M22 = ClampEI(value.M22, min.M22, max.M22);
      value.M23 = ClampEI(value.M23, min.M23, max.M23);
      value.M24 = ClampEI(value.M24, min.M24, max.M24);
      value.M31 = ClampEI(value.M31, min.M31, max.M31);
      value.M32 = ClampEI(value.M32, min.M32, max.M32);
      value.M33 = ClampEI(value.M33, min.M33, max.M33);
      value.M34 = ClampEI(value.M34, min.M34, max.M34);
      value.M41 = ClampEI(value.M41, min.M41, max.M41);
      value.M42 = ClampEI(value.M42, min.M42, max.M42);
      value.M43 = ClampEI(value.M43, min.M43, max.M43);
      value.M44 = ClampEI(value.M44, min.M44, max.M44);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Plane ClampII(Plane value, Plane min, Plane max)
    {
      value.Normal = ClampII(value.Normal, min.Normal, max.Normal);
      value.D = ClampII(value.D, min.D, max.D);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Plane ClampEE(Plane value, Plane min, Plane max)
    {
      value.Normal = ClampEE(value.Normal, min.Normal, max.Normal);
      value.D = ClampEE(value.D, min.D, max.D);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Plane ClampIE(Plane value, Plane min, Plane max)
    {
      value.Normal = ClampIE(value.Normal, min.Normal, max.Normal);
      value.D = ClampIE(value.D, min.D, max.D);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Plane ClampEI(Plane value, Plane min, Plane max)
    {
      value.Normal = ClampEI(value.Normal, min.Normal, max.Normal);
      value.D = ClampEI(value.D, min.D, max.D);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Quaternion ClampII(Quaternion value, Quaternion min, Quaternion max)
    {
      value.X = ClampII(value.X, min.X, max.X);
      value.Y = ClampII(value.Y, min.Y, max.Y);
      value.Z = ClampII(value.Z, min.Z, max.Z);
      value.W = ClampII(value.W, min.W, max.W);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Quaternion ClampEE(Quaternion value, Quaternion min, Quaternion max)
    {
      value.X = ClampEE(value.X, min.X, max.X);
      value.Y = ClampEE(value.Y, min.Y, max.Y);
      value.Z = ClampEE(value.Z, min.Z, max.Z);
      value.W = ClampEE(value.W, min.W, max.W);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Quaternion ClampIE(Quaternion value, Quaternion min, Quaternion max)
    {
      value.X = ClampIE(value.X, min.X, max.X);
      value.Y = ClampIE(value.Y, min.Y, max.Y);
      value.Z = ClampIE(value.Z, min.Z, max.Z);
      value.W = ClampIE(value.W, min.W, max.W);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Quaternion ClampEI(Quaternion value, Quaternion min, Quaternion max)
    {
      value.X = ClampEI(value.X, min.X, max.X);
      value.Y = ClampEI(value.Y, min.Y, max.Y);
      value.Z = ClampEI(value.Z, min.Z, max.Z);
      value.W = ClampEI(value.W, min.W, max.W);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Vector2 ClampII(Vector2 value, Vector2 min, Vector2 max)
    {
      value.X = ClampII(value.X, min.X, max.X);
      value.Y = ClampII(value.Y, min.Y, max.Y);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Vector2 ClampEE(Vector2 value, Vector2 min, Vector2 max)
    {
      value.X = ClampEE(value.X, min.X, max.X);
      value.Y = ClampEE(value.Y, min.Y, max.Y);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Vector2 ClampIE(Vector2 value, Vector2 min, Vector2 max)
    {
      value.X = ClampIE(value.X, min.X, max.X);
      value.Y = ClampIE(value.Y, min.Y, max.Y);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Vector2 ClampEI(Vector2 value, Vector2 min, Vector2 max)
    {
      value.X = ClampEI(value.X, min.X, max.X);
      value.Y = ClampEI(value.Y, min.Y, max.Y);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Vector3 ClampII(Vector3 value, Vector3 min, Vector3 max)
    {
      value.X = ClampII(value.X, min.X, max.X);
      value.Y = ClampII(value.Y, min.Y, max.Y);
      value.Z = ClampII(value.Z, min.Z, max.Z);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Vector3 ClampEE(Vector3 value, Vector3 min, Vector3 max)
    {
      value.X = ClampEE(value.X, min.X, max.X);
      value.Y = ClampEE(value.Y, min.Y, max.Y);
      value.Z = ClampEE(value.Z, min.Z, max.Z);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Vector3 ClampIE(Vector3 value, Vector3 min, Vector3 max)
    {
      value.X = ClampIE(value.X, min.X, max.X);
      value.Y = ClampIE(value.Y, min.Y, max.Y);
      value.Z = ClampIE(value.Z, min.Z, max.Z);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Vector3 ClampEI(Vector3 value, Vector3 min, Vector3 max)
    {
      value.X = ClampEI(value.X, min.X, max.X);
      value.Y = ClampEI(value.Y, min.Y, max.Y);
      value.Z = ClampEI(value.Z, min.Z, max.Z);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Vector4 ClampII(Vector4 value, Vector4 min, Vector4 max)
    {
      value.X = ClampII(value.X, min.X, max.X);
      value.Y = ClampII(value.Y, min.Y, max.Y);
      value.Z = ClampII(value.Z, min.Z, max.Z);
      value.W = ClampII(value.W, min.W, max.W);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Vector4 ClampEE(Vector4 value, Vector4 min, Vector4 max)
    {
      value.X = ClampEE(value.X, min.X, max.X);
      value.Y = ClampEE(value.Y, min.Y, max.Y);
      value.Z = ClampEE(value.Z, min.Z, max.Z);
      value.W = ClampEE(value.W, min.W, max.W);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Vector4 ClampIE(Vector4 value, Vector4 min, Vector4 max)
    {
      value.X = ClampIE(value.X, min.X, max.X);
      value.Y = ClampIE(value.Y, min.Y, max.Y);
      value.Z = ClampIE(value.Z, min.Z, max.Z);
      value.W = ClampIE(value.W, min.W, max.W);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static Vector4 ClampEI(Vector4 value, Vector4 min, Vector4 max)
    {
      value.X = ClampEI(value.X, min.X, max.X);
      value.Y = ClampEI(value.Y, min.Y, max.Y);
      value.Z = ClampEI(value.Z, min.Z, max.Z);
      value.W = ClampEI(value.W, min.W, max.W);
      return value;
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static T ClampII<T>(T value, T min, T max) where T : IClamp<T>
    {
      return value.ClampII(value, min, max);
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static T ClampEE<T>(T value, T min, T max) where T : IClamp<T>
    {
      return value.ClampEE(value, min, max);
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static T ClampIE<T>(T value, T min, T max) where T : IClamp<T>
    {
      return value.ClampIE(value, min, max);
    }

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public static T ClampEI<T>(T value, T min, T max) where T : IClamp<T>
    {
      return value.ClampEI(value, min, max);
    }
  }
  /************************************************************************************************/
}