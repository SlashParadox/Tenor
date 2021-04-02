/**************************************************************************************************/
/*!
\file   Math_Clamp.cs
\author Craig Williams
\par    Last Updated
        2021-03-07
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for tool functions related to clamping a value between two extremes.

\par Bug List

\par References
*/

/**************************************************************************************************/
using System;

namespace Tenor.Tools.Math
{
  /************************************************************************************************/
  public static partial class Math
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
    public static T ClampII<T>(T value, T min, T max) where T : IComparable<T>
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
    public static T ClampIING<T>(T value, T min, T max) where T : IComparable
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
    /// A function for clamping a <paramref name="value"/> between[<paramref name="min"/>, <paramref name="max"/>].
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
  }
  /************************************************************************************************/
}