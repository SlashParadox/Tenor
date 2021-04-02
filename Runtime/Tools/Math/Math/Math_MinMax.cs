/**************************************************************************************************/
/*!
\file   Math_MinMax.cs
\author Craig Williams
\par    Last Updated
        2021-03-07
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for tool functions related to determining a minimum or maximum value.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System;
using System.Collections.Generic;
using Tenor.Tools.Collection;

namespace Tenor.Tools.Math
{
  /************************************************************************************************/
  public static partial class Math
  {
    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="IComparable"/> value.</typeparam>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static T Min<T>(T a, T b) where T : IComparable<T>
    {
      return a.CompareTo(b) < 0 ? a : b;
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="IComparable"/> value.</typeparam>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static T Min<T>(params T[] values) where T : IComparable<T>
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      T min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        T value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value.CompareTo(min) < 0)
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="IComparable"/> value.</typeparam>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static T Min<T>(IList<T> values) where T : IComparable<T>
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      T min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        T value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value.CompareTo(min) < 0)
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="IComparable"/> value.</typeparam>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static T Max<T>(T a, T b) where T : IComparable<T>
    {
      return a.CompareTo(b) > 0 ? a : b;
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="IComparable"/> value.</typeparam>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static T Max<T>(params T[] values) where T : IComparable<T>
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      T max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        T value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value.CompareTo(max) > 0)
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="IComparable"/> value.</typeparam>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static T Max<T>(IList<T> values) where T : IComparable<T>
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      T max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        T value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value.CompareTo(max) > 0)
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="IComparable"/> value.</typeparam>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static T MinNG<T>(T a, T b) where T : IComparable
    {
      return a.CompareTo(b) < 0 ? a : b;
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="IComparable"/> value.</typeparam>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static T MinNG<T>(params T[] values) where T : IComparable
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      T min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        T value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value.CompareTo(min) < 0)
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="IComparable"/> value.</typeparam>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static T MaxNG<T>(T a, T b) where T : IComparable
    {
      return a.CompareTo(b) > 0 ? a : b;
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="IComparable"/> value.</typeparam>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static T MaxNG<T>(params T[] values) where T : IComparable
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      T max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        T value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value.CompareTo(max) > 0)
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static sbyte Min<T>(sbyte a, sbyte b)
    {
      return a < b ? a : b;
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static sbyte Min(params sbyte[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      sbyte min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        sbyte value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value < min)
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static sbyte Min(IList<sbyte> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      sbyte min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        sbyte value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value < min)
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static sbyte Max<T>(sbyte a, sbyte b)
    {
      return a > b ? a : b;
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static sbyte Max(params sbyte[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      sbyte max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        sbyte value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value > max)
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static sbyte Max(IList<sbyte> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      sbyte max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        sbyte value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value > max)
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static byte Min<T>(byte a, byte b)
    {
      return a < b ? a : b;
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static byte Min(params byte[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      byte min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        byte value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value < min)
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static byte Min(IList<byte> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      byte min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        byte value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value < min)
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static byte Max<T>(byte a, byte b)
    {
      return a > b ? a : b;
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static byte Max(params byte[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      byte max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        byte value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value > max)
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static byte Max(IList<byte> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      byte max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        byte value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value > max)
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static short Min<T>(short a, short b)
    {
      return a < b ? a : b;
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static short Min(params short[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      short min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        short value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value < min)
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static short Min(IList<short> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      short min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        short value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value < min)
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static short Max<T>(short a, short b)
    {
      return a > b ? a : b;
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static short Max(params short[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      short max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        short value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value > max)
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static short Max(IList<short> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      short max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        short value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value > max)
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static ushort Min<T>(ushort a, ushort b)
    {
      return a < b ? a : b;
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static ushort Min(params ushort[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      ushort min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        ushort value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value < min)
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static ushort Min(IList<ushort> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      ushort min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        ushort value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value < min)
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static ushort Max<T>(ushort a, ushort b)
    {
      return a > b ? a : b;
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static ushort Max(params ushort[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      ushort max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        ushort value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value > max)
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static ushort Max(IList<ushort> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      ushort max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        ushort value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value > max)
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static int Min<T>(int a, int b)
    {
      return a < b ? a : b;
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static int Min(params int[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      int min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        int value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value < min)
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static int Min(IList<int> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      int min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        int value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value < min)
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static int Max<T>(int a, int b)
    {
      return a > b ? a : b;
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static int Max(params int[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      int max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        int value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value > max)
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static int Max(IList<int> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      int max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        int value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value > max)
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static uint Min<T>(uint a, uint b)
    {
      return a < b ? a : b;
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static uint Min(params uint[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      uint min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        uint value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value < min)
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static uint Min(IList<uint> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      uint min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        uint value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value < min)
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static uint Max<T>(uint a, uint b)
    {
      return a > b ? a : b;
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static uint Max(params uint[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      uint max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        uint value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value > max)
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static uint Max(IList<uint> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      uint max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        uint value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value > max)
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static long Min<T>(long a, long b)
    {
      return a < b ? a : b;
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static long Min(params long[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      long min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        long value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value < min)
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static long Min(IList<long> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      long min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        long value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value < min)
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static long Max<T>(long a, long b)
    {
      return a > b ? a : b;
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static long Max(params long[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      long max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        long value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value > max)
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static long Max(IList<long> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      long max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        long value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value > max)
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static ulong Min<T>(ulong a, ulong b)
    {
      return a < b ? a : b;
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static ulong Min(params ulong[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      ulong min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        ulong value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value < min)
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static ulong Min(IList<ulong> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      ulong min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        ulong value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value < min)
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static ulong Max<T>(ulong a, ulong b)
    {
      return a > b ? a : b;
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static ulong Max(params ulong[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      ulong max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        ulong value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value > max)
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static ulong Max(IList<ulong> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      ulong max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        ulong value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value > max)
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static float Min<T>(float a, float b)
    {
      return a < b || float.IsNaN(a) ? a : b;
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static float Min(params float[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      float min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        float value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value < min || float.IsNaN(value))
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static float Min(IList<float> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      float min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        float value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value < min || float.IsNaN(value))
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static float Max<T>(float a, float b)
    {
      return a > b || float.IsNaN(a) ? a : b;
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static float Max(params float[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      float max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        float value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value > max || float.IsNaN(value))
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static float Max(IList<float> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      float max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        float value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value > max || float.IsNaN(value))
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static double Min<T>(double a, double b)
    {
      return a < b || double.IsNaN(a) ? a : b;
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static double Min(params double[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      double min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        double value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value < min || double.IsNaN(value))
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static double Min(IList<double> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      double min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        double value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value < min || double.IsNaN(value))
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static double Max<T>(double a, double b)
    {
      return a > b || double.IsNaN(a) ? a : b;
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static double Max(params double[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      double max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        double value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value > max || double.IsNaN(value))
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static double Max(IList<double> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      double max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        double value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value > max || double.IsNaN(value))
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static decimal Min<T>(decimal a, decimal b)
    {
      return System.Math.Min(a, b); // Decimals must be handled with a special case.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static decimal Min(params decimal[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      decimal min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        decimal value = values[i];

        // If the current value is less than the current min, set it as the min.
        // Decimals must be handled with a special case.
        min = System.Math.Min(min, value);
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static decimal Min(IList<decimal> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      decimal min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        decimal value = values[i];

        // If the current value is less than the current min, set it as the min.
        // Decimals must be handled with a special case.
        min = System.Math.Min(min, value);
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static decimal Max<T>(decimal a, decimal b)
    {
      return System.Math.Max(a, b); // Decimals must be handled with a special case.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static decimal Max(params decimal[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      decimal max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        decimal value = values[i];

        // If the current value is greater than the current max, set it as the max.
        // Decimals must be handled with a special case.
        max = System.Math.Max(max, value);
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static decimal Max(IList<decimal> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      decimal max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        decimal value = values[i];

        // If the current value is greater than the current max, set it as the max.
        // Decimals must be handled with a special case.
        max = System.Math.Max(max, value);
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static char Min<T>(char a, char b)
    {
      return a < b ? a : b;
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static char Min(params char[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      char min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        char value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value < min)
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the minimum between two values.
    /// </summary>
    /// <param name="values">The values to get the minimum of.</param>
    /// <returns>Returns the min between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static char Min(IList<char> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      char min = values[0]; // Get the starting min.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        char value = values[i];

        // If the current value is less than the current min, set it as the min.
        if (value < min)
          min = value;
      }

      return min; // Return the final min.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static char Max<T>(char a, char b)
    {
      return a > b ? a : b;
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static char Max(params char[] values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Length; // Get the number of values.
      char max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        char value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value > max)
          max = value;
      }

      return max; // Return the final max.
    }

    /// <summary>
    /// A function to get the maximum between two values.
    /// </summary>
    /// <param name="values">The values to get the maximum of.</param>
    /// <returns>Returns the max between <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static char Max(IList<char> values)
    {
      // If there are no values, return a default value.
      if (values.IsEmptyOrNull())
        return default;

      int count = values.Count; // Get the number of values.
      char max = values[0]; // Get the starting max.

      // Iterate through all values.
      for (int i = 1; i < count; i++)
      {
        char value = values[i];

        // If the current value is greater than the current max, set it as the max.
        if (value > max)
          max = value;
      }

      return max; // Return the final max.
    }
  }
  /************************************************************************************************/
}