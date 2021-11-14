/**************************************************************************************************/
/*!
\file   Lerp.cs
\author Craig Williams
\par    Last Updated
        2021-06-02
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for tool functions related to linearly interpolating a value.

\par Bug List
  NORMAL
    * Quaternions are hard. 360 degree rotation cannot occur.

\par References
  - https://keithmaggio.wordpress.com/2011/02/15/math-magician-lerp-slerp-and-nlerp/
  - https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/Numerics/Quaternion.cs
  - https://doc.magnum.graphics/magnum/namespaceMagnum_1_1Math.html#aa52d32b2fcb66f28a4330fb39fa50589
  - https://doc.magnum.graphics/magnum/namespaceMagnum_1_1Math.html#a0d790000a3656bf3b1bad2098ec00ea0
  - https://doc.magnum.graphics/magnum/namespaceMagnum_1_1Math.html#a7af2a318a3c70abee764adfa3b5a3a02
*/
/**************************************************************************************************/

using SlashParadox.Tenor.Math;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace SlashParadox.Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// A collection of functions for linearly interpreting between two values. This kit contains
  /// implementations for basic numeric types, types in <see cref="System.Numerics"/>, and anything
  /// that inherits and implements <see cref="ILerp{T}"/>.
  /// </summary>
  public static partial class Lerp
  {
    /// <summary>A constant error-based value, matching the one used with .NET.</summary>
    public const float SlerpEpsilon = 1e-6f;

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
      t = Maths.ClampII(t, 0, 1);
      // Each value has to be lerped individually, to avoid a dot product.
      a.M11 = a.M11 + (b.M11 - a.M11) * t;
      a.M12 = a.M12 + (b.M12 - a.M12) * t;
      a.M21 = a.M21 + (b.M21 - a.M21) * t;
      a.M22 = a.M22 + (b.M22 - a.M22) * t;
      a.M31 = a.M31 + (b.M31 - a.M31) * t;
      a.M32 = a.M32 + (b.M32 - a.M32) * t;
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
      t = Maths.ClampII(t, 0, 1);
      // Each value has to be lerped individually, to avoid a dot product.
      a.M11 = (float)(a.M11 + (b.M11 - a.M11) * t);
      a.M12 = (float)(a.M12 + (b.M12 - a.M12) * t);
      a.M21 = (float)(a.M21 + (b.M21 - a.M21) * t);
      a.M22 = (float)(a.M22 + (b.M22 - a.M22) * t);
      a.M31 = (float)(a.M31 + (b.M31 - a.M31) * t);
      a.M32 = (float)(a.M32 + (b.M32 - a.M32) * t);
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
      a.M11 = a.M11 + (b.M11 - a.M11) * t;
      a.M12 = a.M12 + (b.M12 - a.M12) * t;
      a.M21 = a.M21 + (b.M21 - a.M21) * t;
      a.M22 = a.M22 + (b.M22 - a.M22) * t;
      a.M31 = a.M31 + (b.M31 - a.M31) * t;
      a.M32 = a.M32 + (b.M32 - a.M32) * t;
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
      a.M11 = (float)(a.M11 + (b.M11 - a.M11) * t);
      a.M12 = (float)(a.M12 + (b.M12 - a.M12) * t);
      a.M21 = (float)(a.M21 + (b.M21 - a.M21) * t);
      a.M22 = (float)(a.M22 + (b.M22 - a.M22) * t);
      a.M31 = (float)(a.M31 + (b.M31 - a.M31) * t);
      a.M32 = (float)(a.M32 + (b.M32 - a.M32) * t);
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
      t = Maths.ClampII(t, 0, 1);
      // Each value has to be lerped individually, to avoid a dot product.
      a.M11 = a.M11 + (b.M11 - a.M11) * t;
      a.M12 = a.M12 + (b.M12 - a.M12) * t;
      a.M13 = a.M13 + (b.M13 - a.M13) * t;
      a.M14 = a.M14 + (b.M14 - a.M14) * t;
      a.M21 = a.M21 + (b.M21 - a.M21) * t;
      a.M22 = a.M22 + (b.M22 - a.M22) * t;
      a.M23 = a.M23 + (b.M23 - a.M23) * t;
      a.M24 = a.M24 + (b.M24 - a.M24) * t;
      a.M31 = a.M31 + (b.M31 - a.M31) * t;
      a.M32 = a.M32 + (b.M32 - a.M32) * t;
      a.M33 = a.M33 + (b.M33 - a.M33) * t;
      a.M34 = a.M34 + (b.M34 - a.M34) * t;
      a.M41 = a.M41 + (b.M41 - a.M41) * t;
      a.M42 = a.M42 + (b.M42 - a.M42) * t;
      a.M43 = a.M43 + (b.M43 - a.M43) * t;
      a.M44 = a.M44 + (b.M44 - a.M44) * t;
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
      t = Maths.ClampII(t, 0, 1);
      // Each value has to be lerped individually, to avoid a dot product.
      a.M11 = (float)(a.M11 + (b.M11 - a.M11) * t);
      a.M12 = (float)(a.M12 + (b.M12 - a.M12) * t);
      a.M13 = (float)(a.M13 + (b.M13 - a.M13) * t);
      a.M14 = (float)(a.M14 + (b.M14 - a.M14) * t);
      a.M21 = (float)(a.M21 + (b.M21 - a.M21) * t);
      a.M22 = (float)(a.M22 + (b.M22 - a.M22) * t);
      a.M23 = (float)(a.M23 + (b.M23 - a.M23) * t);
      a.M24 = (float)(a.M24 + (b.M24 - a.M24) * t);
      a.M31 = (float)(a.M31 + (b.M31 - a.M31) * t);
      a.M32 = (float)(a.M32 + (b.M32 - a.M32) * t);
      a.M33 = (float)(a.M33 + (b.M33 - a.M33) * t);
      a.M34 = (float)(a.M34 + (b.M34 - a.M34) * t);
      a.M41 = (float)(a.M41 + (b.M41 - a.M41) * t);
      a.M42 = (float)(a.M42 + (b.M42 - a.M42) * t);
      a.M43 = (float)(a.M43 + (b.M43 - a.M43) * t);
      a.M44 = (float)(a.M44 + (b.M44 - a.M44) * t);
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
      a.M11 = a.M11 + (b.M11 - a.M11) * t;
      a.M12 = a.M12 + (b.M12 - a.M12) * t;
      a.M13 = a.M13 + (b.M13 - a.M13) * t;
      a.M14 = a.M14 + (b.M14 - a.M14) * t;
      a.M21 = a.M21 + (b.M21 - a.M21) * t;
      a.M22 = a.M22 + (b.M22 - a.M22) * t;
      a.M23 = a.M23 + (b.M23 - a.M23) * t;
      a.M24 = a.M24 + (b.M24 - a.M24) * t;
      a.M31 = a.M31 + (b.M31 - a.M31) * t;
      a.M32 = a.M32 + (b.M32 - a.M32) * t;
      a.M33 = a.M33 + (b.M33 - a.M33) * t;
      a.M34 = a.M34 + (b.M34 - a.M34) * t;
      a.M41 = a.M41 + (b.M41 - a.M41) * t;
      a.M42 = a.M42 + (b.M42 - a.M42) * t;
      a.M43 = a.M43 + (b.M43 - a.M43) * t;
      a.M44 = a.M44 + (b.M44 - a.M44) * t;
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
      a.M11 = (float)(a.M11 + (b.M11 - a.M11) * t);
      a.M12 = (float)(a.M12 + (b.M12 - a.M12) * t);
      a.M13 = (float)(a.M13 + (b.M13 - a.M13) * t);
      a.M14 = (float)(a.M14 + (b.M14 - a.M14) * t);
      a.M21 = (float)(a.M21 + (b.M21 - a.M21) * t);
      a.M22 = (float)(a.M22 + (b.M22 - a.M22) * t);
      a.M23 = (float)(a.M23 + (b.M23 - a.M23) * t);
      a.M24 = (float)(a.M24 + (b.M24 - a.M24) * t);
      a.M31 = (float)(a.M31 + (b.M31 - a.M31) * t);
      a.M32 = (float)(a.M32 + (b.M32 - a.M32) * t);
      a.M33 = (float)(a.M33 + (b.M33 - a.M33) * t);
      a.M34 = (float)(a.M34 + (b.M34 - a.M34) * t);
      a.M41 = (float)(a.M41 + (b.M41 - a.M41) * t);
      a.M42 = (float)(a.M42 + (b.M42 - a.M42) * t);
      a.M43 = (float)(a.M43 + (b.M43 - a.M43) * t);
      a.M44 = (float)(a.M44 + (b.M44 - a.M44) * t);
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
    /// <param name="shortPath">A toggle for forcing the shortest or longest path.</param>
    /// <returns>Returns the interpolated value.</returns>
    /// <remarks>This does NOT normalize the <see cref="Quaternion"/>! Use
    /// <see cref="NlerpValue(Quaternion, Quaternion, float, bool)"/> for that!</remarks>
    public static Quaternion LerpValue(Quaternion a, Quaternion b, float t, bool shortPath = true)
    {
      t = Maths.ClampII(t, 0, 1);
      return shortPath ? HandleLerpShort(a, b, t) : HandleLerpLong(a, b, t);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Quaternion"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Quaternion"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Quaternion"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <param name="shortPath">A toggle for forcing the shortest or longest path.</param>
    /// <returns>Returns the interpolated value.</returns>
    /// <remarks>This does NOT normalize the <see cref="Quaternion"/>! Use
    /// <see cref="NlerpValue(Quaternion, Quaternion, double, bool)"/> for that!</remarks>
    public static Quaternion LerpValue(Quaternion a, Quaternion b, double t, bool shortPath = true)
    {
      float tF = (float)Maths.ClampII(t, 0, 1);
      return shortPath ? HandleLerpShort(a, b, tF) : HandleLerpLong(a, b, tF);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Quaternion"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Quaternion"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Quaternion"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <param name="shortPath">A toggle for forcing the shortest or longest path.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    /// <remarks>This does NOT normalize the <see cref="Quaternion"/>! Use
    /// <see cref="NlerpUnclamped(Quaternion, Quaternion, float, bool)"/> for that!</remarks>
    public static Quaternion LerpUnclamped(Quaternion a, Quaternion b, float t,
                                           bool shortPath = true)
    {
      return shortPath ? HandleLerpShort(a, b, t) : HandleLerpLong(a, b, t);
    }

    /// <summary>
    /// A function to linearly interpolate between two <see cref="Quaternion"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Quaternion"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Quaternion"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <param name="shortPath">A toggle for forcing the shortest or longest path.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    /// <remarks>This does NOT normalize the <see cref="Quaternion"/>! Use
    /// <see cref="NlerpUnclamped(Quaternion, Quaternion, double, bool)"/> for that!</remarks>
    public static Quaternion LerpUnclamped(Quaternion a, Quaternion b, double t,
                                           bool shortPath = true)
    {
      return shortPath ? HandleLerpShort(a, b, (float)t) : HandleLerpLong(a, b, (float)t);
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
      return a + (b - a) * Maths.ClampII(t, 0.0f, 1.0f);
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
      return a + (b - a) * Maths.ClampII((float)t, 0.0f, 1.0f);
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
      return a + (b - a) * t;
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
      return a + (b - a) * (float)t;
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
      return a + (b - a) * Maths.ClampII(t, 0.0f, 1.0f);
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
      return a + (b - a) * Maths.ClampII((float)t, 0.0f, 1.0f);
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
      return a + (b - a) * t;
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
      return a + (b - a) * (float)t;
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
      return a + (b - a) * Maths.ClampII(t, 0.0f, 1.0f);
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
      return a + (b - a) * Maths.ClampII((float)t, 0.0f, 1.0f);
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
      return a + (b - a) * t;
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
      return a + (b - a) * (float)t;
    }

    /// <summary>
    /// A function to linearly interpolate between two <typeparamref name="T"/>s.
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
    /// A function to linearly interpolate between two <typeparamref name="T"/>s.
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
    /// A function to linearly interpolate between two <typeparamref name="T"/>, without a clamp.
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
    /// A function to linearly interpolate between two <typeparamref name="T"/>, without a clamp.
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

    /// <summary>
    /// A function to normalized-linearly interpolate between two <see cref="Plane"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Plane"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Plane"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Plane NlerpValue(Plane a, Plane b, float t)
    {
      a.Normal = LerpValue(a.Normal, b.Normal, t);
      a.D = LerpValue(a.D, b.D, t);
      return Plane.Normalize(a); // Normalize afterwards.
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <see cref="Plane"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Plane"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Plane"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Plane NlerpValue(Plane a, Plane b, double t)
    {
      a.Normal = LerpValue(a.Normal, b.Normal, t);
      a.D = LerpValue(a.D, b.D, t);
      return Plane.Normalize(a); // Normalize afterwards.
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <see cref="Plane"/>s, without a
    /// clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Plane"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Plane"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Plane NlerpUnclamped(Plane a, Plane b, float t)
    {
      a.Normal = LerpUnclamped(a.Normal, b.Normal, t);
      a.D = LerpUnclamped(a.D, b.D, t);
      return Plane.Normalize(a); // Normalize afterwards.
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <see cref="Plane"/>s, without a
    /// clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Plane"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Plane"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Plane NlerpUnclamped(Plane a, Plane b, double t)
    {
      a.Normal = LerpUnclamped(a.Normal, b.Normal, t);
      a.D = LerpUnclamped(a.D, b.D, t);
      return Plane.Normalize(a); // Normalize afterwards.
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <see cref="Quaternion"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Quaternion"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Quaternion"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <param name="shortPath">A toggle for forcing the shortest or longest path.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Quaternion NlerpValue(Quaternion a, Quaternion b, float t, bool shortPath = true)
    {
      t = Maths.ClampII(t, 0, 1);
      Quaternion r = shortPath ? HandleLerpShort(a, b, t) : HandleLerpLong(a, b, t);
      return Quaternion.Normalize(r); // Normalize afterwards.
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <see cref="Quaternion"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Quaternion"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Quaternion"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <param name="shortPath">A toggle for forcing the shortest or longest path.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Quaternion NlerpValue(Quaternion a, Quaternion b, double t, bool shortPath = true)
    {
      float tF = (float)Maths.ClampII(t, 0, 1);
      Quaternion r = shortPath ? HandleLerpShort(a, b, tF) : HandleLerpLong(a, b, tF);
      return Quaternion.Normalize(r); // Normalize afterwards.
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <see cref="Quaternion"/>s,
    /// without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Quaternion"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Quaternion"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <param name="shortPath">A toggle for forcing the shortest or longest path.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Quaternion NlerpUnclamped(Quaternion a, Quaternion b, float t,
                                            bool shortPath = true)
    {
      Quaternion r = shortPath ? HandleLerpShort(a, b, t) : HandleLerpLong(a, b, t);
      return Quaternion.Normalize(r); // Normalize afterwards.
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <see cref="Quaternion"/>s,
    /// without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Quaternion"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Quaternion"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <param name="shortPath">A toggle for forcing the shortest or longest path.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Quaternion NlerpUnclamped(Quaternion a, Quaternion b, double t,
                                            bool shortPath = true)
    {
      Quaternion r = shortPath ? HandleLerpShort(a, b, (float)t) : HandleLerpLong(a, b, (float)t);
      return Quaternion.Normalize(r); // Normalize afterwards.
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <see cref="Vector2"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Vector2"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Vector2"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Vector2 NlerpValue(Vector2 a, Vector2 b, float t)
    {
      return Vector2.Normalize(a + (b - a) * Maths.ClampII(t, 0.0f, 1.0f));
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <see cref="Vector2"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Vector2"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Vector2"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Vector2 NlerpValue(Vector2 a, Vector2 b, double t)
    {
      return Vector2.Normalize(a + (b - a) * Maths.ClampII((float)t, 0.0f, 1.0f));
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <see cref="Vector2"/>s,
    /// without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Vector2"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Vector2"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Vector2 NlerpUnclamped(Vector2 a, Vector2 b, float t)
    {
      return Vector2.Normalize(a + (b - a) * t);
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <see cref="Vector2"/>s,
    /// without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Vector2"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Vector2"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Vector2 NlerpUnclamped(Vector2 a, Vector2 b, double t)
    {
      return Vector2.Normalize(a + (b - a) * (float)t);
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <see cref="Vector3"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Vector3"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Vector3"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Vector3 NlerpValue(Vector3 a, Vector3 b, float t)
    {
      return Vector3.Normalize(a + (b - a) * Maths.ClampII(t, 0.0f, 1.0f));
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <see cref="Vector3"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Vector3"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Vector3"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Vector3 NlerpValue(Vector3 a, Vector3 b, double t)
    {
      return Vector3.Normalize(a + (b - a) * Maths.ClampII((float)t, 0.0f, 1.0f));
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <see cref="Vector3"/>s,
    /// without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Vector3"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Vector3"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Vector3 NlerpUnclamped(Vector3 a, Vector3 b, float t)
    {
      return Vector3.Normalize(a + (b - a) * t);
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <see cref="Vector3"/>s,
    /// without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Vector3"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Vector3"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Vector3 NlerpUnclamped(Vector3 a, Vector3 b, double t)
    {
      return Vector3.Normalize(a + (b - a) * (float)t);
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <see cref="Vector4"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Vector4"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Vector4"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Vector4 NlerpValue(Vector4 a, Vector4 b, float t)
    {
      return Vector4.Normalize(a + (b - a) * Maths.ClampII(t, 0.0f, 1.0f));
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <see cref="Vector4"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Vector4"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Vector4"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Vector4 NlerpValue(Vector4 a, Vector4 b, double t)
    {
      return Vector4.Normalize(a + (b - a) * Maths.ClampII((float)t, 0.0f, 1.0f));
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <see cref="Vector4"/>s,
    /// without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Vector4"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Vector4"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Vector4 NlerpUnclamped(Vector4 a, Vector4 b, float t)
    {
      return Vector4.Normalize(a + (b - a) * t);
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <see cref="Vector4"/>s,
    /// without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Vector4"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Vector4"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Vector4 NlerpUnclamped(Vector4 a, Vector4 b, double t)
    {
      return Vector4.Normalize(a + (b - a) * (float)t);
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <typeparamref name="T"/>s.
    /// </summary>
    /// <typeparam name="T">The <see cref="INlerp{T}"/> to interpolate.</typeparam>
    /// <param name="a">The start <typeparamref name="T"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <typeparamref name="T"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static T NlerpValue<T>(T a, T b, float t) where T : INlerp<T>
    {
      return a.Nlerp(a, b, t);
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <typeparamref name="T"/>s.
    /// </summary>
    /// <typeparam name="T">The <see cref="INlerp{T}"/> to interpolate.</typeparam>
    /// <param name="a">The start <typeparamref name="T"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <typeparamref name="T"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static T NlerpValue<T>(T a, T b, double t) where T : INlerp<T>
    {
      return a.Nlerp(a, b, t);
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <typeparamref name="T"/>,
    /// without a clamp.
    /// </summary>
    /// <typeparam name="T">The <see cref="INlerp{T}"/> to interpolate.</typeparam>
    /// <param name="a">The start <typeparamref name="T"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <typeparamref name="T"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static T NlerpUnclamped<T>(T a, T b, float t) where T : INlerp<T>
    {
      return a.NlerpUnclamped(a, b, t);
    }

    /// <summary>
    /// A function to normalized-linearly interpolate between two <typeparamref name="T"/>,
    /// without a clamp.
    /// </summary>
    /// <typeparam name="T">The <see cref="INlerp{T}"/> to interpolate.</typeparam>
    /// <param name="a">The start <typeparamref name="T"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <typeparamref name="T"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static T NlerpUnclamped<T>(T a, T b, double t) where T : INlerp<T>
    {
      return a.NlerpUnclamped(a, b, t);
    }

    /// <summary>
    /// A function to spherically interpolate between two <see cref="Quaternion"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Quaternion"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Quaternion"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <param name="shortPath">A toggle for forcing the shortest or longest path.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Quaternion SlerpValue(Quaternion a, Quaternion b, float t, bool shortPath = true)
    {
      t = Maths.ClampII(t, 0, 1);
      return shortPath ? HandleSlerpShort(a, b, t) : HandleSlerpLong(a, b, t);
    }

    /// <summary>
    /// A function to spherically interpolate between two <see cref="Quaternion"/>s.
    /// </summary>
    /// <param name="a">The start <see cref="Quaternion"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Quaternion"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <param name="shortPath">A toggle for forcing the shortest or longest path.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static Quaternion SlerpValue(Quaternion a, Quaternion b, double t, bool shortPath = true)
    {
      t = Maths.ClampII(t, 0, 1);
      return shortPath ? HandleSlerpShort(a, b, t) : HandleSlerpLong(a, b, t);
    }

    /// <summary>
    /// A function to spherically interpolate between two <see cref="Quaternion"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Quaternion"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Quaternion"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <param name="shortPath">A toggle for forcing the shortest or longest path.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Quaternion SlerpUnclamped(Quaternion a, Quaternion b, float t,
                                           bool shortPath = true)
    {
      return shortPath ? HandleSlerpShort(a, b, t) : HandleSlerpLong(a, b, t);
    }

    /// <summary>
    /// A function to spherically interpolate between two <see cref="Quaternion"/>s, without a clamp.
    /// </summary>
    /// <param name="a">The first <see cref="Quaternion"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The second <see cref="Quaternion"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <param name="shortPath">A toggle for forcing the shortest or longest path.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static Quaternion SlerpUnclamped(Quaternion a, Quaternion b, double t,
                                           bool shortPath = true)
    {
      return shortPath ? HandleSlerpShort(a, b, t) : HandleSlerpLong(a, b, t);
    }

    /// <summary>
    /// A function to spherically interpolate between two <typeparamref name="T"/>s.
    /// </summary>
    /// <typeparam name="T">The <see cref="ISlerp{T}"/> to interpolate.</typeparam>
    /// <param name="a">The start <typeparamref name="T"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <typeparamref name="T"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static T SlerpValue<T>(T a, T b, float t) where T : ISlerp<T>
    {
      return a.Slerp(a, b, t);
    }

    /// <summary>
    /// A function to spherically interpolate between two <typeparamref name="T"/>s.
    /// </summary>
    /// <typeparam name="T">The <see cref="ISlerp{T}"/> to interpolate.</typeparam>
    /// <param name="a">The start <typeparamref name="T"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <typeparamref name="T"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation, on a scale of 0 to 1 between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    public static T SlerpValue<T>(T a, T b, double t) where T : ISlerp<T>
    {
      return a.Slerp(a, b, t);
    }

    /// <summary>
    /// A function to spherically interpolate between two <typeparamref name="T"/>,
    /// without a clamp.
    /// </summary>
    /// <typeparam name="T">The <see cref="ISlerp{T}"/> to interpolate.</typeparam>
    /// <param name="a">The start <typeparamref name="T"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <typeparamref name="T"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static T SlerpUnclamped<T>(T a, T b, float t) where T : ISlerp<T>
    {
      return a.SlerpUnclamped(a, b, t);
    }

    /// <summary>
    /// A function to spherically interpolate between two <typeparamref name="T"/>,
    /// without a clamp.
    /// </summary>
    /// <typeparam name="T">The <see cref="ISlerp{T}"/> to interpolate.</typeparam>
    /// <param name="a">The start <typeparamref name="T"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <typeparamref name="T"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value. <paramref name="t"/> values outside the range of
    /// 0 to 1 still affect the returned value.</returns>
    public static T SlerpUnclamped<T>(T a, T b, double t) where T : ISlerp<T>
    {
      return a.SlerpUnclamped(a, b, t);
    }

    /// <summary>
    /// An internal function for handling a short-path linear interpolation.
    /// </summary>
    /// <param name="a">The start <see cref="Quaternion"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Quaternion"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Quaternion HandleLerpShort(Quaternion a, Quaternion b, float t)
    {
      float tInverse = 1.0f - t;
      // Get the dot product of the quaternions.
      float d = a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;

      // Flip the second quaternion if d is less than 0.
      if (d >= 0.0f)
      {
        // Multiply the individual components of the quaternion.
        a.X = tInverse * a.X + t * b.X;
        a.Y = tInverse * a.Y + t * b.Y;
        a.Z = tInverse * a.Z + t * b.Z;
        a.W = tInverse * a.W + t * b.W;
      }
      else
      {
        // Multiply the individual components of the quaternion.
        a.X = tInverse * a.X - t * b.X;
        a.Y = tInverse * a.Y - t * b.Y;
        a.Z = tInverse * a.Z - t * b.Z;
        a.W = tInverse * a.W - t * b.W;
      }

      return a;
    }

    /// <summary>
    /// An internal function for handling a long-path linear interpolation.
    /// </summary>
    /// <param name="a">The start <see cref="Quaternion"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Quaternion"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Quaternion HandleLerpLong(Quaternion a, Quaternion b, float t)
    {
      float tInverse = 1.0f - t;

      // Multiply the individual components of the quaternion.
      a.X = tInverse * a.X + t * b.X;
      a.Y = tInverse * a.Y + t * b.Y;
      a.Z = tInverse * a.Z + t * b.Z;
      a.W = tInverse * a.W + t * b.W;

      return a;
    }

    /// <summary>
    /// An internal function for handling a short-path spherical interpolation.
    /// </summary>
    /// <param name="a">The start <see cref="Quaternion"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Quaternion"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Quaternion HandleSlerpShort(Quaternion a, Quaternion b, double t)
    {
      bool negate = false;
      // Get the dot product of the quaternions.
      float d = a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;

      // If d is negative, we use the negation of the second quaternion, and d's absolute value.
      if (d < 0.0f)
      {
        negate = true;
        d = -d;
      }

      // Create hold variables for the sin calculations. These are multiplied later.
      float equationA;
      float equationB;

      // If the values are too close, default to normal interpolation.
      if (d > (1.0f - SlerpEpsilon))
      {
        equationA = (float)(1.0f - t); // The first quaternion's multiple.
        equationB = (float)(negate ? -t : t); // The second quaternion's multiple;
      }
      else
      {
        double theta = System.Math.Acos(d); // Get the theta angle.
        double inverseSin = 1.0f / System.Math.Sin(theta); // Get the denominator.

        // Get the quaternion multiples.
        equationA = (float)(System.Math.Sin((1.0f - t) * theta) * inverseSin);
        equationB = (float)(negate ? -System.Math.Sin(t * theta) * inverseSin :
                                      System.Math.Sin(t * theta) * inverseSin);
      }

      // Multiply the individual components of the quaternion.
      a.X = equationA * a.X + equationB * b.X;
      a.Y = equationA * a.Y + equationB * b.Y;
      a.Z = equationA * a.Z + equationB * b.Z;
      a.W = equationA * a.W + equationB * b.W;

      return a;
    }

    /// <summary>
    /// An internal function for handling a long-path spherical interpolation.
    /// </summary>
    /// <param name="a">The start <see cref="Quaternion"/>, at a <paramref name="t"/> of 0.</param>
    /// <param name="b">The end <see cref="Quaternion"/>, at a <paramref name="t"/> of 1.</param>
    /// <param name="t">The interpolation between the two values.</param>
    /// <returns>Returns the interpolated value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Quaternion HandleSlerpLong(Quaternion a, Quaternion b, double t)
    {
      bool negate = false;
      // Get the dot product of the quaternions.
      float d = a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;

      // If d is negative, we must use the negation of the second quaternion.
      if (d < 0.0f)
      {
        negate = true;

        // Edge case: An odd number of vectors are opposite.
        if (d == -1.0f)
          d = -d;
      }

      // Create hold variables for the sin calculations. These are multiplied later.
      float equationA;
      float equationB;

      // If the values are too close, default to normal interpolation.
      if (d > (1.0f - SlerpEpsilon))
      {
        equationA = (float)(1.0f - t); // The first quaternion's multiple.
        equationB = (float)(negate ? -t : t); // The second quaternion's multiple;
      }
      else
      {
        double theta = System.Math.Acos(d); // Get the theta angle.
        double inverseSin = 1.0f / System.Math.Sin(theta); // Get the denominator.

        // Get the quaternion multiples.
        equationA = (float)(System.Math.Sin((1.0f - t) * theta) * inverseSin);
        equationB = (float)(System.Math.Sin(t * theta) * inverseSin);
      }

      // Multiply the individual components of the quaternion.
      a.X = equationA * a.X + equationB * b.X;
      a.Y = equationA * a.Y + equationB * b.Y;
      a.Z = equationA * a.Z + equationB * b.Z;
      a.W = equationA * a.W + equationB * b.W;

      return a;
    }
  }
  /************************************************************************************************/
}