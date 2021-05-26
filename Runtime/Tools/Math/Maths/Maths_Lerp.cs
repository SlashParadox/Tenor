/**************************************************************************************************/
/*!
\file   Maths_Lerp.cs
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
  public static partial class Maths
  {
    /// <summary>
    /// A function to linearly interpolate between two values.
    /// </summary>
    /// <param name="a">The start value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static sbyte Lerp(sbyte a, sbyte b, float t)
    {
      return (sbyte)(a + (b - a) * ClampII(t, 0.0f, 1.0f));
    }

    /// <summary>
    /// A function to linearly interpolate between two values.
    /// </summary>
    /// <param name="a">The start value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static sbyte Lerp(sbyte a, sbyte b, double t)
    {
      return (sbyte)(a + (b - a) * ClampII(t, 0.0, 1.0));
    }

    /// <summary>
    /// A function to linearly interpolate between two values, without a clamp.
    /// </summary>
    /// <param name="a">The first value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static sbyte LerpUnclamped(sbyte a, sbyte b, float t)
    {
      return (sbyte)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two values, without a clamp.
    /// </summary>
    /// <param name="a">The first value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static sbyte LerpUnclamped(sbyte a, sbyte b, double t)
    {
      return (sbyte)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two values.
    /// </summary>
    /// <param name="a">The start value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static byte Lerp(byte a, byte b, float t)
    {
      return (byte)(a + (b - a) * ClampII(t, 0.0f, 1.0f));
    }

    /// <summary>
    /// A function to linearly interpolate between two values.
    /// </summary>
    /// <param name="a">The start value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static byte Lerp(byte a, byte b, double t)
    {
      return (byte)(a + (b - a) * ClampII(t, 0.0, 1.0));
    }

    /// <summary>
    /// A function to linearly interpolate between two values, without a clamp.
    /// </summary>
    /// <param name="a">The first value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static byte LerpUnclamped(byte a, byte b, float t)
    {
      return (byte)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two values, without a clamp.
    /// </summary>
    /// <param name="a">The first value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static byte LerpUnclamped(byte a, byte b, double t)
    {
      return (byte)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two values.
    /// </summary>
    /// <param name="a">The start value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static short Lerp(short a, short b, float t)
    {
      return (short)(a + (b - a) * ClampII(t, 0.0f, 1.0f));
    }

    /// <summary>
    /// A function to linearly interpolate between two values.
    /// </summary>
    /// <param name="a">The start value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static short Lerp(short a, short b, double t)
    {
      return (short)(a + (b - a) * ClampII(t, 0.0, 1.0));
    }

    /// <summary>
    /// A function to linearly interpolate between two values, without a clamp.
    /// </summary>
    /// <param name="a">The first value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static short LerpUnclamped(short a, short b, float t)
    {
      return (short)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two values, without a clamp.
    /// </summary>
    /// <param name="a">The first value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static short LerpUnclamped(short a, short b, double t)
    {
      return (short)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two values.
    /// </summary>
    /// <param name="a">The start value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static ushort Lerp(ushort a, ushort b, float t)
    {
      return (ushort)(a + (b - a) * ClampII(t, 0.0f, 1.0f));
    }

    /// <summary>
    /// A function to linearly interpolate between two values.
    /// </summary>
    /// <param name="a">The start value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static ushort Lerp(ushort a, ushort b, double t)
    {
      return (ushort)(a + (b - a) * ClampII(t, 0.0, 1.0));
    }

    /// <summary>
    /// A function to linearly interpolate between two values, without a clamp.
    /// </summary>
    /// <param name="a">The first value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static ushort LerpUnclamped(ushort a, ushort b, float t)
    {
      return (ushort)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two values, without a clamp.
    /// </summary>
    /// <param name="a">The first value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static ushort LerpUnclamped(ushort a, ushort b, double t)
    {
      return (ushort)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two values.
    /// </summary>
    /// <param name="a">The start value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static int Lerp(int a, int b, float t)
    {
      return (int)(a + (b - a) * ClampII(t, 0.0f, 1.0f));
    }

    /// <summary>
    /// A function to linearly interpolate between two values.
    /// </summary>
    /// <param name="a">The start value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static int Lerp(int a, int b, double t)
    {
      return (int)(a + (b - a) * ClampII(t, 0.0, 1.0));
    }

    /// <summary>
    /// A function to linearly interpolate between two values, without a clamp.
    /// </summary>
    /// <param name="a">The first value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static int LerpUnclamped(int a, int b, float t)
    {
      return (int)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two values, without a clamp.
    /// </summary>
    /// <param name="a">The first value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static int LerpUnclamped(int a, int b, double t)
    {
      return (int)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two values.
    /// </summary>
    /// <param name="a">The start value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static uint Lerp(uint a, uint b, float t)
    {
      return (uint)(a + (b - a) * ClampII(t, 0.0f, 1.0f));
    }

    /// <summary>
    /// A function to linearly interpolate between two values.
    /// </summary>
    /// <param name="a">The start value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static uint Lerp(uint a, uint b, double t)
    {
      return (uint)(a + (b - a) * ClampII(t, 0.0, 1.0));
    }

    /// <summary>
    /// A function to linearly interpolate between two values, without a clamp.
    /// </summary>
    /// <param name="a">The first value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static uint LerpUnclamped(uint a, uint b, float t)
    {
      return (uint)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two values, without a clamp.
    /// </summary>
    /// <param name="a">The first value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static uint LerpUnclamped(uint a, uint b, double t)
    {
      return (uint)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two values.
    /// </summary>
    /// <param name="a">The start value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static long Lerp(long a, long b, float t)
    {
      return (long)(a + (b - a) * ClampII(t, 0.0f, 1.0f));
    }

    /// <summary>
    /// A function to linearly interpolate between two values.
    /// </summary>
    /// <param name="a">The start value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static long Lerp(long a, long b, double t)
    {
      return (long)(a + (b - a) * ClampII(t, 0.0, 1.0));
    }

    /// <summary>
    /// A function to linearly interpolate between two values, without a clamp.
    /// </summary>
    /// <param name="a">The first value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static long LerpUnclamped(long a, long b, float t)
    {
      return (long)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two values, without a clamp.
    /// </summary>
    /// <param name="a">The first value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static long LerpUnclamped(long a, long b, double t)
    {
      return (long)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two values.
    /// </summary>
    /// <param name="a">The start value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static ulong Lerp(ulong a, ulong b, float t)
    {
      return (ulong)(a + (b - a) * ClampII(t, 0.0f, 1.0f));
    }

    /// <summary>
    /// A function to linearly interpolate between two values.
    /// </summary>
    /// <param name="a">The start value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static ulong Lerp(ulong a, ulong b, double t)
    {
      return (ulong)(a + (b - a) * ClampII(t, 0.0, 1.0));
    }

    /// <summary>
    /// A function to linearly interpolate between two values, without a clamp.
    /// </summary>
    /// <param name="a">The first value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static ulong LerpUnclamped(ulong a, ulong b, float t)
    {
      return (ulong)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two values, without a clamp.
    /// </summary>
    /// <param name="a">The first value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static ulong LerpUnclamped(ulong a, ulong b, double t)
    {
      return (ulong)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two values.
    /// </summary>
    /// <param name="a">The start value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static float Lerp(float a, float b, float t)
    {
      return a + (b - a) * ClampII(t, 0.0f, 1.0f);
    }

    /// <summary>
    /// A function to linearly interpolate between two values.
    /// </summary>
    /// <param name="a">The start value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static float Lerp(float a, float b, double t)
    {
      return (float)(a + (b - a) * ClampII(t, 0.0, 1.0));
    }

    /// <summary>
    /// A function to linearly interpolate between two values, without a clamp.
    /// </summary>
    /// <param name="a">The first value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static float LerpUnclamped(float a, float b, float t)
    {
      return a + (b - a) * t;
    }

    /// <summary>
    /// A function to linearly interpolate between two values, without a clamp.
    /// </summary>
    /// <param name="a">The first value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static float LerpUnclamped(float a, float b, double t)
    {
      return (float)(a + (b - a) * t);
    }

    /// <summary>
    /// A function to linearly interpolate between two values.
    /// </summary>
    /// <param name="a">The start value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static double Lerp(double a, double b, float t)
    {
      return a + (b - a) * ClampII(t, 0.0f, 1.0f);
    }

    /// <summary>
    /// A function to linearly interpolate between two values.
    /// </summary>
    /// <param name="a">The start value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static double Lerp(double a, double b, double t)
    {
      return a + (b - a) * ClampII(t, 0.0, 1.0);
    }

    /// <summary>
    /// A function to linearly interpolate between two values, without a clamp.
    /// </summary>
    /// <param name="a">The first value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static double LerpUnclamped(double a, double b, float t)
    {
      return a + (b - a) * t;
    }

    /// <summary>
    /// A function to linearly interpolate between two values, without a clamp.
    /// </summary>
    /// <param name="a">The first value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static double LerpUnclamped(double a, double b, double t)
    {
      return a + (b - a) * t;
    }

    /// <summary>
    /// A function to linearly interpolate between two values.
    /// </summary>
    /// <param name="a">The start value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static decimal Lerp(decimal a, decimal b, float t)
    {
      return a + (b - a) * (decimal)ClampII(t, 0.0f, 1.0f);
    }

    /// <summary>
    /// A function to linearly interpolate between two values.
    /// </summary>
    /// <param name="a">The start value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static decimal Lerp(decimal a, decimal b, double t)
    {
      return a + (b - a) * (decimal)ClampII(t, 0.0, 1.0);
    }

    /// <summary>
    /// A function to linearly interpolate between two values, without a clamp.
    /// </summary>
    /// <param name="a">The first value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second value, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static decimal LerpUnclamped(decimal a, decimal b, float t)
    {
      return a + (b - a) * (decimal)t;
    }

    /// <summary>
    /// A function to linearly interpolate between two values, without a clamp.
    /// </summary>
    /// <param name="a">The first value, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second value, at a <paramref name="t"/> of 1.</param>
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