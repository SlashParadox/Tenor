/**************************************************************************************************/
/*!
\file   Lerp.cs
\author Craig Williams
\par    Last Updated
        2021-05-30
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for tool functions related to linearly interpolating a value.

\par Bug List

\par References
*/
/**************************************************************************************************/

using CodeParadox.Tenor.Math;
using System.Numerics;

namespace CodeParadox.Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// A collection of functions for linearly interpreting between two values. This kit contains
  /// implementations for basic numeric types, types in <see cref="System.Numerics"/>, and anything
  /// that inherits and implements <see cref="ILerp{T}"/>.
  /// </summary>
  public static partial class Lerp
  {
    /// <summary>
    /// A function to linearly interpolate between two <see cref="sbyte"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="sbyte"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="sbyte"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static sbyte LerpValue(sbyte a, sbyte b, float t)
    {
      return (sbyte)(a + (b - a) * Maths.ClampII(t, 0.0f, 1.0f));
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="sbyte"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="sbyte"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="sbyte"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static sbyte LerpValue(sbyte a, sbyte b, double t)
    {
      return (sbyte)(a + (b - a) * Maths.ClampII(t, 0.0, 1.0));
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="sbyte"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="sbyte"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="sbyte"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static sbyte LerpUnclamped(sbyte a, sbyte b, float t)
    {
      return (sbyte)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="sbyte"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="sbyte"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="sbyte"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static sbyte LerpUnclamped(sbyte a, sbyte b, double t)
    {
      return (sbyte)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="byte"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="byte"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="byte"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static byte LerpValue(byte a, byte b, float t)
    {
      return (byte)(a + (b - a) * Maths.ClampII(t, 0.0f, 1.0f));
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="byte"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="byte"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="byte"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static byte LerpValue(byte a, byte b, double t)
    {
      return (byte)(a + (b - a) * Maths.ClampII(t, 0.0, 1.0));
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="byte"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="byte"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="byte"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static byte LerpUnclamped(byte a, byte b, float t)
    {
      return (byte)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="byte"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="byte"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="byte"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static byte LerpUnclamped(byte a, byte b, double t)
    {
      return (byte)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="short"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="short"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="short"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static short LerpValue(short a, short b, float t)
    {
      return (short)(a + (b - a) * Maths.ClampII(t, 0.0f, 1.0f));
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="short"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="short"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="short"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static short LerpValue(short a, short b, double t)
    {
      return (short)(a + (b - a) * Maths.ClampII(t, 0.0, 1.0));
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="short"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="short"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="short"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static short LerpUnclamped(short a, short b, float t)
    {
      return (short)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="short"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="short"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="short"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static short LerpUnclamped(short a, short b, double t)
    {
      return (short)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="ushort"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="ushort"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="ushort"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static ushort LerpValue(ushort a, ushort b, float t)
    {
      return (ushort)(a + (b - a) * Maths.ClampII(t, 0.0f, 1.0f));
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="ushort"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="ushort"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="ushort"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static ushort LerpValue(ushort a, ushort b, double t)
    {
      return (ushort)(a + (b - a) * Maths.ClampII(t, 0.0, 1.0));
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="ushort"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="ushort"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="ushort"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static ushort LerpUnclamped(ushort a, ushort b, float t)
    {
      return (ushort)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="ushort"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="ushort"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="ushort"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static ushort LerpUnclamped(ushort a, ushort b, double t)
    {
      return (ushort)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="int"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="int"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="int"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static int LerpValue(int a, int b, float t)
    {
      return (int)(a + (b - a) * Maths.ClampII(t, 0.0f, 1.0f));
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="int"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="int"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="int"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static int LerpValue(int a, int b, double t)
    {
      return (int)(a + (b - a) * Maths.ClampII(t, 0.0, 1.0));
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="int"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="int"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="int"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static int LerpUnclamped(int a, int b, float t)
    {
      return (int)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="int"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="int"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="int"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static int LerpUnclamped(int a, int b, double t)
    {
      return (int)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="uint"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="uint"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="uint"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static uint LerpValue(uint a, uint b, float t)
    {
      return (uint)(a + (b - a) * Maths.ClampII(t, 0.0f, 1.0f));
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="uint"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="uint"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="uint"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static uint LerpValue(uint a, uint b, double t)
    {
      return (uint)(a + (b - a) * Maths.ClampII(t, 0.0, 1.0));
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="uint"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="uint"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="uint"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static uint LerpUnclamped(uint a, uint b, float t)
    {
      return (uint)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="uint"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="uint"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="uint"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static uint LerpUnclamped(uint a, uint b, double t)
    {
      return (uint)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="long"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="long"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="long"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static long LerpValue(long a, long b, float t)
    {
      return (long)(a + (b - a) * Maths.ClampII(t, 0.0f, 1.0f));
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="long"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="long"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="long"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static long LerpValue(long a, long b, double t)
    {
      return (long)(a + (b - a) * Maths.ClampII(t, 0.0, 1.0));
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="long"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="long"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="long"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static long LerpUnclamped(long a, long b, float t)
    {
      return (long)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="long"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="long"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="long"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static long LerpUnclamped(long a, long b, double t)
    {
      return (long)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="ulong"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="ulong"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="ulong"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static ulong LerpValue(ulong a, ulong b, float t)
    {
      return (ulong)(a + (b - a) * Maths.ClampII(t, 0.0f, 1.0f));
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="ulong"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="ulong"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="ulong"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static ulong LerpValue(ulong a, ulong b, double t)
    {
      return (ulong)(a + (b - a) * Maths.ClampII(t, 0.0, 1.0));
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="ulong"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="ulong"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="ulong"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static ulong LerpUnclamped(ulong a, ulong b, float t)
    {
      return (ulong)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="ulong"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="ulong"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="ulong"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static ulong LerpUnclamped(ulong a, ulong b, double t)
    {
      return (ulong)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="float"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="float"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="float"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static float LerpValue(float a, float b, float t)
    {
      return a + (b - a) * Maths.ClampII(t, 0.0f, 1.0f);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="float"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="float"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="float"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static float LerpValue(float a, float b, double t)
    {
      return (float)(a + (b - a) * Maths.ClampII(t, 0.0, 1.0));
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="float"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="float"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="float"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static float LerpUnclamped(float a, float b, float t)
    {
      return a + (b - a) * t;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="float"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="float"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="float"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static float LerpUnclamped(float a, float b, double t)
    {
      return (float)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="double"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="double"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="double"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static double LerpValue(double a, double b, float t)
    {
      return a + (b - a) * Maths.ClampII(t, 0.0f, 1.0f);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="double"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="double"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="double"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static double LerpValue(double a, double b, double t)
    {
      return a + (b - a) * Maths.ClampII(t, 0.0, 1.0);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="double"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="double"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="double"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static double LerpUnclamped(double a, double b, float t)
    {
      return a + (b - a) * t;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="double"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="double"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="double"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static double LerpUnclamped(double a, double b, double t)
    {
      return a + (b - a) * t;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="decimal"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="decimal"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="decimal"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static decimal LerpValue(decimal a, decimal b, float t)
    {
      return a + (b - a) * (decimal)Maths.ClampII(t, 0.0f, 1.0f);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="decimal"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="decimal"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="decimal"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static decimal LerpValue(decimal a, decimal b, double t)
    {
      return a + (b - a) * (decimal)Maths.ClampII(t, 0.0, 1.0);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="decimal"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="decimal"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="decimal"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static decimal LerpUnclamped(decimal a, decimal b, float t)
    {
      return a + (b - a) * (decimal)t;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="decimal"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="decimal"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="decimal"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static decimal LerpUnclamped(decimal a, decimal b, double t)
    {
      return a + (b - a) * (decimal)t;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="BigInteger"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="BigInteger"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="BigInteger"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static BigInteger LerpValue(BigInteger a, BigInteger b, float t)
    {
      return a + (b - a) * (BigInteger)Maths.ClampII(t, 0.0f, 1.0f);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="BigInteger"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="BigInteger"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="BigInteger"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static BigInteger LerpValue(BigInteger a, BigInteger b, double t)
    {
      return a + (b - a) * (BigInteger)Maths.ClampII(t, 0.0, 1.0);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="BigInteger"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="BigInteger"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="BigInteger"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static BigInteger LerpUnclamped(BigInteger a, BigInteger b, float t)
    {
      return a + (b - a) * (BigInteger)t;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="BigInteger"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="BigInteger"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="BigInteger"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static BigInteger LerpUnclamped(BigInteger a, BigInteger b, double t)
    {
      return a + (b - a) * (BigInteger)t;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Complex"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Complex"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Complex"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Complex LerpValue(Complex a, Complex b, float t)
    {
      return a + (b - a) * Maths.ClampII(t, 0.0f, 1.0f);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Complex"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Complex"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Complex"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Complex LerpValue(Complex a, Complex b, double t)
    {
      return a + (b - a) * Maths.ClampII(t, 0.0, 1.0);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Complex"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Complex"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Complex"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Complex LerpUnclamped(Complex a, Complex b, float t)
    {
      return a + (b - a) * t;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Complex"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Complex"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Complex"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Complex LerpUnclamped(Complex a, Complex b, double t)
    {
      return a + (b - a) * t;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Matrix3x2"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Matrix3x2"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Matrix3x2"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Matrix3x2 LerpValue(Matrix3x2 a, Matrix3x2 b, float t)
    {
      // Each value has to be lerped individually, to avoid a dot product.
      a.M11 = LerpValue(a.M11, b.M11, t);
      a.M12 = LerpValue(a.M12, b.M12, t);
      a.M21 = LerpValue(a.M21, b.M21, t);
      a.M22 = LerpValue(a.M22, b.M22, t);
      a.M31 = LerpValue(a.M31, b.M31, t);
      a.M32 = LerpValue(a.M32, b.M32, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Matrix3x2"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Matrix3x2"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Matrix3x2"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Matrix3x2 LerpValue(Matrix3x2 a, Matrix3x2 b, double t)
    {
      // Each value has to be lerped individually, to avoid a dot product.
      a.M11 = LerpValue(a.M11, b.M11, t);
      a.M12 = LerpValue(a.M12, b.M12, t);
      a.M21 = LerpValue(a.M21, b.M21, t);
      a.M22 = LerpValue(a.M22, b.M22, t);
      a.M31 = LerpValue(a.M31, b.M31, t);
      a.M32 = LerpValue(a.M32, b.M32, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Matrix3x2"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Matrix3x2"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Matrix3x2"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Matrix3x2 LerpUnclamped(Matrix3x2 a, Matrix3x2 b, float t)
    {
      // Each value has to be lerped individually, to avoid a dot product.
      a.M11 = LerpUnclamped(a.M11, b.M11, t);
      a.M12 = LerpUnclamped(a.M12, b.M12, t);
      a.M21 = LerpUnclamped(a.M21, b.M21, t);
      a.M22 = LerpUnclamped(a.M22, b.M22, t);
      a.M31 = LerpUnclamped(a.M31, b.M31, t);
      a.M32 = LerpUnclamped(a.M32, b.M32, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Matrix3x2"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Matrix3x2"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Matrix3x2"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Matrix3x2 LerpUnclamped(Matrix3x2 a, Matrix3x2 b, double t)
    {
      // Each value has to be lerped individually, to avoid a dot product.
      a.M11 = LerpUnclamped(a.M11, b.M11, t);
      a.M12 = LerpUnclamped(a.M12, b.M12, t);
      a.M21 = LerpUnclamped(a.M21, b.M21, t);
      a.M22 = LerpUnclamped(a.M22, b.M22, t);
      a.M31 = LerpUnclamped(a.M31, b.M31, t);
      a.M32 = LerpUnclamped(a.M32, b.M32, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Matrix4x4"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Matrix4x4"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Matrix4x4"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Matrix4x4 LerpValue(Matrix4x4 a, Matrix4x4 b, float t)
    {
      // Each value has to be lerped individually, to avoid a dot product.
      a.M11 = LerpValue(a.M11, b.M11, t);
      a.M12 = LerpValue(a.M12, b.M12, t);
      a.M13 = LerpValue(a.M13, b.M13, t);
      a.M14 = LerpValue(a.M14, b.M14, t);
      a.M21 = LerpValue(a.M21, b.M21, t);
      a.M22 = LerpValue(a.M22, b.M22, t);
      a.M23 = LerpValue(a.M23, b.M23, t);
      a.M24 = LerpValue(a.M24, b.M24, t);
      a.M31 = LerpValue(a.M31, b.M31, t);
      a.M32 = LerpValue(a.M32, b.M32, t);
      a.M33 = LerpValue(a.M33, b.M33, t);
      a.M34 = LerpValue(a.M34, b.M34, t);
      a.M41 = LerpValue(a.M41, b.M41, t);
      a.M42 = LerpValue(a.M42, b.M42, t);
      a.M43 = LerpValue(a.M43, b.M43, t);
      a.M44 = LerpValue(a.M44, b.M44, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Matrix4x4"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Matrix4x4"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Matrix4x4"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Matrix4x4 LerpValue(Matrix4x4 a, Matrix4x4 b, double t)
    {
      // Each value has to be lerped individually, to avoid a dot product.
      a.M11 = LerpValue(a.M11, b.M11, t);
      a.M12 = LerpValue(a.M12, b.M12, t);
      a.M13 = LerpValue(a.M13, b.M13, t);
      a.M14 = LerpValue(a.M14, b.M14, t);
      a.M21 = LerpValue(a.M21, b.M21, t);
      a.M22 = LerpValue(a.M22, b.M22, t);
      a.M23 = LerpValue(a.M23, b.M23, t);
      a.M24 = LerpValue(a.M24, b.M24, t);
      a.M31 = LerpValue(a.M31, b.M31, t);
      a.M32 = LerpValue(a.M32, b.M32, t);
      a.M33 = LerpValue(a.M33, b.M33, t);
      a.M34 = LerpValue(a.M34, b.M34, t);
      a.M41 = LerpValue(a.M41, b.M41, t);
      a.M42 = LerpValue(a.M42, b.M42, t);
      a.M43 = LerpValue(a.M43, b.M43, t);
      a.M44 = LerpValue(a.M44, b.M44, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Matrix4x4"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Matrix4x4"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Matrix4x4"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Matrix4x4 LerpUnclamped(Matrix4x4 a, Matrix4x4 b, float t)
    {
      // Each value has to be lerped individually, to avoid a dot product.
      a.M11 = LerpUnclamped(a.M11, b.M11, t);
      a.M12 = LerpUnclamped(a.M12, b.M12, t);
      a.M13 = LerpUnclamped(a.M13, b.M13, t);
      a.M14 = LerpUnclamped(a.M14, b.M14, t);
      a.M21 = LerpUnclamped(a.M21, b.M21, t);
      a.M22 = LerpUnclamped(a.M22, b.M22, t);
      a.M23 = LerpUnclamped(a.M23, b.M23, t);
      a.M24 = LerpUnclamped(a.M24, b.M24, t);
      a.M31 = LerpUnclamped(a.M31, b.M31, t);
      a.M32 = LerpUnclamped(a.M32, b.M32, t);
      a.M33 = LerpUnclamped(a.M33, b.M33, t);
      a.M34 = LerpUnclamped(a.M34, b.M34, t);
      a.M41 = LerpUnclamped(a.M41, b.M41, t);
      a.M42 = LerpUnclamped(a.M42, b.M42, t);
      a.M43 = LerpUnclamped(a.M43, b.M43, t);
      a.M44 = LerpUnclamped(a.M44, b.M44, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Matrix4x4"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Matrix4x4"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Matrix4x4"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Matrix4x4 LerpUnclamped(Matrix4x4 a, Matrix4x4 b, double t)
    {
      // Each value has to be lerped individually, to avoid a dot product.
      a.M11 = LerpUnclamped(a.M11, b.M11, t);
      a.M12 = LerpUnclamped(a.M12, b.M12, t);
      a.M13 = LerpUnclamped(a.M13, b.M13, t);
      a.M14 = LerpUnclamped(a.M14, b.M14, t);
      a.M21 = LerpUnclamped(a.M21, b.M21, t);
      a.M22 = LerpUnclamped(a.M22, b.M22, t);
      a.M23 = LerpUnclamped(a.M23, b.M23, t);
      a.M24 = LerpUnclamped(a.M24, b.M24, t);
      a.M31 = LerpUnclamped(a.M31, b.M31, t);
      a.M32 = LerpUnclamped(a.M32, b.M32, t);
      a.M33 = LerpUnclamped(a.M33, b.M33, t);
      a.M34 = LerpUnclamped(a.M34, b.M34, t);
      a.M41 = LerpUnclamped(a.M41, b.M41, t);
      a.M42 = LerpUnclamped(a.M42, b.M42, t);
      a.M43 = LerpUnclamped(a.M43, b.M43, t);
      a.M44 = LerpUnclamped(a.M44, b.M44, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Plane"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Plane"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Plane"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Plane LerpValue(Plane a, Plane b, float t)
    {
      a.Normal = LerpValue(a.Normal, b.Normal, t);
      a.D = LerpValue(a.D, b.D, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Plane"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Plane"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Plane"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Plane LerpValue(Plane a, Plane b, double t)
    {
      a.Normal = LerpValue(a.Normal, b.Normal, t);
      a.D = LerpValue(a.D, b.D, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Plane"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Plane"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Plane"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Plane LerpUnclamped(Plane a, Plane b, float t)
    {
      a.Normal = LerpUnclamped(a.Normal, b.Normal, t);
      a.D = LerpUnclamped(a.D, b.D, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Plane"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Plane"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Plane"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Plane LerpUnclamped(Plane a, Plane b, double t)
    {
      a.Normal = LerpUnclamped(a.Normal, b.Normal, t);
      a.D = LerpUnclamped(a.D, b.D, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Quaternion"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Quaternion"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Quaternion"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Quaternion LerpValue(Quaternion a, Quaternion b, float t)
    {
      a.X = LerpValue(a.X, b.X, t);
      a.Y = LerpValue(a.Y, b.Y, t);
      a.Z = LerpValue(a.Z, b.Z, t);
      a.W = LerpValue(a.W, b.W, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Quaternion"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Quaternion"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Quaternion"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Quaternion LerpValue(Quaternion a, Quaternion b, double t)
    {
      a.X = LerpValue(a.X, b.X, t);
      a.Y = LerpValue(a.Y, b.Y, t);
      a.Z = LerpValue(a.Z, b.Z, t);
      a.W = LerpValue(a.W, b.W, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Quaternion"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Quaternion"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Quaternion"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Quaternion LerpUnclamped(Quaternion a, Quaternion b, float t)
    {
      a.X = LerpUnclamped(a.X, b.X, t);
      a.Y = LerpUnclamped(a.Y, b.Y, t);
      a.Z = LerpUnclamped(a.Z, b.Z, t);
      a.W = LerpUnclamped(a.W, b.W, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Quaternion"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Quaternion"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Quaternion"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Quaternion LerpUnclamped(Quaternion a, Quaternion b, double t)
    {
      a.X = LerpUnclamped(a.X, b.X, t);
      a.Y = LerpUnclamped(a.Y, b.Y, t);
      a.Z = LerpUnclamped(a.Z, b.Z, t);
      a.W = LerpUnclamped(a.W, b.W, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Vector2"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Vector2"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Vector2"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Vector2 LerpValue(Vector2 a, Vector2 b, float t)
    {
      a.X = LerpValue(a.X, b.X, t);
      a.Y = LerpValue(a.Y, b.Y, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Vector2"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Vector2"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Vector2"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Vector2 LerpValue(Vector2 a, Vector2 b, double t)
    {
      a.X = LerpValue(a.X, b.X, t);
      a.Y = LerpValue(a.Y, b.Y, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Vector2"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Vector2"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Vector2"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, float t)
    {
      a.X = LerpUnclamped(a.X, b.X, t);
      a.Y = LerpUnclamped(a.Y, b.Y, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Vector2"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Vector2"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Vector2"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, double t)
    {
      a.X = LerpUnclamped(a.X, b.X, t);
      a.Y = LerpUnclamped(a.Y, b.Y, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Vector3"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Vector3"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Vector3"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Vector3 LerpValue(Vector3 a, Vector3 b, float t)
    {
      a.X = LerpValue(a.X, b.X, t);
      a.Y = LerpValue(a.Y, b.Y, t);
      a.Z = LerpValue(a.Z, b.Z, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Vector3"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Vector3"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Vector3"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Vector3 LerpValue(Vector3 a, Vector3 b, double t)
    {
      a.X = LerpValue(a.X, b.X, t);
      a.Y = LerpValue(a.Y, b.Y, t);
      a.Z = LerpValue(a.Z, b.Z, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Vector3"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Vector3"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Vector3"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
    {
      a.X = LerpUnclamped(a.X, b.X, t);
      a.Y = LerpUnclamped(a.Y, b.Y, t);
      a.Z = LerpUnclamped(a.Z, b.Z, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Vector3"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Vector3"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Vector3"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, double t)
    {
      a.X = LerpUnclamped(a.X, b.X, t);
      a.Y = LerpUnclamped(a.Y, b.Y, t);
      a.Z = LerpUnclamped(a.Z, b.Z, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Vector4"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Vector4"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Vector4"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Vector4 LerpValue(Vector4 a, Vector4 b, float t)
    {
      a.X = LerpValue(a.X, b.X, t);
      a.Y = LerpValue(a.Y, b.Y, t);
      a.Z = LerpValue(a.Z, b.Z, t);
      a.W = LerpValue(a.W, b.W, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Vector4"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Vector4"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Vector4"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Vector4 LerpValue(Vector4 a, Vector4 b, double t)
    {
      a.X = LerpValue(a.X, b.X, t);
      a.Y = LerpValue(a.Y, b.Y, t);
      a.Z = LerpValue(a.Z, b.Z, t);
      a.W = LerpValue(a.W, b.W, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Vector4"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Vector4"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Vector4"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Vector4 LerpUnclamped(Vector4 a, Vector4 b, float t)
    {
      a.X = LerpUnclamped(a.X, b.X, t);
      a.Y = LerpUnclamped(a.Y, b.Y, t);
      a.Z = LerpUnclamped(a.Z, b.Z, t);
      a.W = LerpUnclamped(a.W, b.W, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Vector4"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Vector4"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Vector4"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Vector4 LerpUnclamped(Vector4 a, Vector4 b, double t)
    {
      a.X = LerpUnclamped(a.X, b.X, t);
      a.Y = LerpUnclamped(a.Y, b.Y, t);
      a.Z = LerpUnclamped(a.Z, b.Z, t);
      a.W = LerpUnclamped(a.W, b.W, t);
      return a;
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="T"/>s.
    /// </summary>
    /// <typeparam name="T">The <see cref="ILerp{T}"/> to interpolate.</typeparam>
    /// <param name="a">The start <typeparamref name="T"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <typeparamref name="T"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static T LerpValue<T>(T a, T b, float t) where T : ILerp<T>
    {
      return a.Lerp(a, b, t);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="T"/>s.
    /// </summary>
    /// <typeparam name="T">The <see cref="ILerp{T}"/> to interpolate.</typeparam>
    /// <param name="a">The start <typeparamref name="T"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <typeparamref name="T"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static T LerpValue<T>(T a, T b, double t) where T : ILerp<T>
    {
      return a.Lerp(a, b, t);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="T"/>, without a clamp.
    /// </summary>
    /// <typeparam name="T">The <see cref="ILerp{T}"/> to interpolate.</typeparam>
    /// <param name="a">The start <typeparamref name="T"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <typeparamref name="T"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static T LerpUnclamped<T>(T a, T b, float t) where T : ILerp<T>
    {
      return a.LerpUnclamped(a, b, t);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="T"/>, without a clamp.
    /// </summary>
    /// <typeparam name="T">The <see cref="ILerp{T}"/> to interpolate.</typeparam>
    /// <param name="a">The start <typeparamref name="T"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <typeparamref name="T"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static T LerpUnclamped<T>(T a, T b, double t) where T : ILerp<T>
    {
      return a.LerpUnclamped(a, b, t);
    }
  }
  /************************************************************************************************/
}