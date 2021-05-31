/**************************************************************************************************/
/*!
\file   IClamp.cs
\author Craig Williams
\par    Last Updated
        2021-05-30
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for an interface used for marking a type as able to be clamped.

\par Bug List

\par References
*/
/**************************************************************************************************/
namespace CodeParadox.Tenor
{
  /************************************************************************************************/
  /// <summary>
  /// An interface to give a type the ability to clamp values within itself. Use in conjunction
  /// with the Clamp functions in the <see cref="Tools.Maths"/> tools.
  /// </summary>
  /// <typeparam name="T">The type to clamp. This is the inheriting type.</typeparam>
  public interface IClamp<T> where T : IClamp<T>
  {
    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public T ClampII(T value, T min, T max);

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public T ClampEE(T value, T min, T max);

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// [<paramref name="min"/>, <paramref name="max"/>).
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public T ClampIE(T value, T min, T max);

    /// <summary>
    /// A function for clamping a <paramref name="value"/> between
    /// (<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns a clamped <paramref name="value"/>.</returns>
    public T ClampEI(T value, T min, T max);
  }
  /************************************************************************************************/
}