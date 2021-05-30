/**************************************************************************************************/
/*!
\file   Lerp.cs
\author Craig Williams
\par    Last Updated
        2021-05-25
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for tool functions related to linearly interpolating a value.

\par Bug List

\par References
*/
/**************************************************************************************************/

namespace CodeParadox.Tenor.Tools
{
  /************************************************************************************************/
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
  }
  /************************************************************************************************/
}