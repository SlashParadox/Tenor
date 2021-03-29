/**************************************************************************************************/
/*!
\file   Strings.cs
\author Craig Williams
\par    Last Updated
        2021-03-07
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for tool functions related to strings, chars, and StringBuilders.

\par Bug List

\par References
*/
/**************************************************************************************************/


using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Tenor.Tools.Text
{
  /************************************************************************************************/
  /// <summary>
  /// A series of useful functions for dealing with <see cref="string"/>s, <see cref="char"/>s,
  /// and <see cref="StringBuilder"/>s.
  /// </summary>
  public static partial class Strings
  {
    /// <summary>
    /// An extension function to see if a string contains an integral value type.
    /// Note that this uses <see cref="NumberStyles.Integer"/>, which allows white space
    /// before and after.
    /// </summary>
    /// <param name="str">The string to attempt parsing on.</param>
    /// <returns>Returns if an integral value could be parsed from the string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsIntegral(this string str)
    {
      return str.IsIntegral(NumberFormatInfo.InvariantInfo);
    }

    /// <summary>
    /// An extension function to see if a string contains an integral value type.
    /// Note that this uses <see cref="NumberStyles.Integer"/>, which allows white space
    /// before and after.
    /// </summary>
    /// <param name="str">The string to attempt parsing on.</param>
    /// <param name="nfi">The <see cref="NumberFormatInfo"/> for this string.
    /// If unsure, pass <see cref="NumberFormatInfo.InvariantInfo"/>.</param>
    /// <returns>Returns if an integral value could be parsed from the string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsIntegral(this string str, NumberFormatInfo nfi)
    {
      return long.TryParse(str, NumberStyles.Integer, nfi, out long _);
    }

    /// <summary>
    /// An extension function to see if a string contains an integral value type.
    /// Note that this uses <see cref="NumberStyles.Integer"/>, which allows white space
    /// before and after.
    /// </summary>
    /// <param name="str">The string to attempt parsing on.</param>
    /// <returns>Returns if an integral value could be parsed from the string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFloatingPoint(this string str)
    {
      return str.IsFloatingPoint(NumberFormatInfo.InvariantInfo);
    }

    /// <summary>
    /// An extension function to see if a string contains an integral value type.
    /// Note that this uses <see cref="NumberStyles.Integer"/>, which allows white space
    /// before and after.
    /// </summary>
    /// <param name="str">The string to attempt parsing on.</param>
    /// <param name="nfi">The <see cref="NumberFormatInfo"/> for this string.
    /// If unsure, pass <see cref="NumberFormatInfo.InvariantInfo"/>.</param>
    /// <returns>Returns if an integral value could be parsed from the string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFloatingPoint(this string str, NumberFormatInfo nfi)
    {
      return double.TryParse(str, NumberStyles.Float, nfi, out double _);
    }

    /// <summary>
    /// An extension function to see if a string contains any sort of numeric value.
    /// </summary>
    /// <param name="str">The string to attempt parsing on.</param>
    /// <returns>Returns if a number value could be parsed from the string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNumeric(this string str)
    {
      return str.IsIntegral() || str.IsFloatingPoint();
    }

    /// <summary>
    /// An extension function to see if a string contains any sort of numeric value.
    /// </summary>
    /// <param name="str">The string to attempt parsing on.</param>
    /// <param name="nfi">The <see cref="NumberFormatInfo"/> for this string.
    /// If unsure, pass <see cref="NumberFormatInfo.InvariantInfo"/>.</param>
    /// <returns>Returns if a number value could be parsed from the string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNumeric(this string str, NumberFormatInfo nfi)
    {
      return str.IsIntegral(nfi) || str.IsFloatingPoint(nfi);
    }

    /// <summary>
    /// An extension function to see if a string contains a boolean value.
    /// </summary>
    /// <param name="str">The string to attempt parsing on.</param>
    /// <returns>Returns if a boolean value could be parsed from the string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBoolean(this string str)
    {
      return bool.TryParse(str, out bool _);
    }

    /// <summary>
    /// An extension function to see if a <see cref="StringBuilder"/> is an empty string.
    /// </summary>
    /// <param name="strb">The <see cref="StringBuilder"/> to check.</param>
    /// <returns>Returns if the builder is an empty string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEmpty(this StringBuilder strb)
    {
      return strb.Length <= 0;
    }

    /// <summary>
    /// An extension function to see if a <see cref="StringBuilder"/> is null or an empty string.
    /// </summary>
    /// <param name="strb">The <see cref="StringBuilder"/> to check.</param>
    /// <returns>Returns if the builder is null or an empty string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNullOrEmpty(this StringBuilder strb)
    {
      return strb == null || strb.Length <= 0;
    }

    /// <summary>
    /// An extension function to see if a <see cref="StringBuilder"/> only contains whitespace.
    /// </summary>
    /// <param name="strb">The <see cref="StringBuilder"/> to check.</param>
    /// <returns>Returns if the builder only contains whitespace.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsWhiteSpace(this StringBuilder strb)
    {
      return string.IsNullOrWhiteSpace(strb.ToString());
    }

    /// <summary>
    /// An extension function to see if a <see cref="StringBuilder"/> is null or only
    /// contains whitespace.
    /// </summary>
    /// <param name="strb">The <see cref="StringBuilder"/> to check.</param>
    /// <returns>Returns if the builder is null or only contains whitespace.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNullOrWhiteSpace(this StringBuilder strb)
    {
      return strb == null || string.IsNullOrWhiteSpace(strb.ToString());
    }
  }
  /************************************************************************************************/
}