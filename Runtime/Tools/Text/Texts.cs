/**************************************************************************************************/
/*!
\file   Texts.cs
\author Craig Williams
\par    Last Updated
        2021-06-18
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for a class of functions related to strings and text.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CodeParadox.Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// A collection of tools for handling <see cref="string"/>s and other forms of text.
  /// </summary>
  public static partial class Texts
  {
    /// <summary>
    /// A function for converting a <see cref="string"/> to <see cref="string.Empty"/>, if it is
    /// <see langword="null"/>.
    /// </summary>
    /// <param name="str">The <see cref="string"/>. If <see langword="null"/>, it is converted to
    /// <see cref="string.Empty"/>. Otherwise, it is unchanged.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NullToEmpty(ref string str)
    {
      if (str == null)
        str = string.Empty;
    }

    /// <summary>
    /// A function for getting a <see cref="string"/>'s last <see cref="char"/>.
    /// </summary>
    /// <param name="str">The <see cref="string"/> to examine.</param>
    /// <returns>Returns the <paramref name="str"/>'s last <see cref="char"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static char LastChar(this string str)
    {
      return str[str.Length - 1];
    }

    /// <summary>
    /// A function for checking if a <see cref="string"/>s last <see cref="char"/> is something
    /// specific.
    /// </summary>
    /// <param name="str">The <see cref="string"/> to examine.</param>
    /// <param name="ch">The <see cref="char"/> to check for.</param>
    /// <returns>Returns if the <paramref name="str"/>'s last <see cref="char"/> is
    /// <paramref name="ch"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLastChar(this string str, char ch)
    {
      return str[str.Length - 1] == ch;
    }

    public static bool IsBase64(this string str)
    {
      try
      {
        _ = Convert.FromBase64String(str);
        return true;
      }
      catch
      {
        return false;
      }
    }

    public static bool IsBase64(byte[] bytes)
    {
      try
      {
        _ = Convert.ToBase64String(bytes);
        return true;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    /// An extension function for checking if a string contains any items within an
    /// <see cref="IList"/>.
    /// </summary>
    /// <param name="str">The <see cref="string"/> to check.</param>
    /// <param name="candidates">The messages the <paramref name="str"/> might contain.</param>
    /// <returns>Returns if the <paramref name="str"/> contains any
    /// <paramref name="candidates"/>.</returns>
    public static bool ContainsAny(this string str, IList<string> candidates)
    {
      // Make sure the list is valid.
      if (candidates.IsNotEmptyOrNull())
      {
        // Iterate through all candidates.
        int count = candidates.Count;
        for (int i = 0; i < count; i++)
        {
          // If the string contains a candidate, return true immediately.
          if (str.Contains(candidates[i]))
            return true;
        }
      }

      return false; // Candidate Not Found.
    }
  }
  /************************************************************************************************/
}