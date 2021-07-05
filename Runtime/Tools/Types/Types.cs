/**************************************************************************************************/
/*!
\file   Types.cs
\author Craig Williams
\par    Last Updated
        2021-07-05
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for helper functions for dealing with Types.

\par Bug List

\par References
  - https://gist.github.com/wappenull/2391b3c23dd20ede74483d0da4cab3f1
*/
/**************************************************************************************************/

using System;

namespace CodeParadox.Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// A series of useful extension and helper functions for dealing with <see cref="Type"/>s.
  /// </summary>
  public static partial class Types
  {
    /// <summary>
    /// An extension function to check if a given <see cref="Type"/> is, or is a subclass of,
    /// another <see cref="Type"/>.
    /// </summary>
    /// <param name="thisType">The <see cref="Type"/> being checked.</param>
    /// <param name="check">The <see cref="Type"/> to check against.</param>
    /// <returns>Returns if <paramref name="thisType"/> is or is a subclass of
    /// <paramref name="check"/>.</returns>
    public static bool IsOrIsSubclassOf(this Type thisType, Type check)
    {
      // Continuously check base types until something matches.
      Type current = thisType;
      while (current != null)
      {
        if (current == check)
          return true;

        current = current.BaseType;
      }

      return false; // Nothing ever matched.
    }

    /// <summary>
    /// An extension function to check if a given <see cref="Type"/> is, or is a subclass of,
    /// another generic <see cref="Type"/>.
    /// </summary>
    /// <param name="thisType">The <see cref="Type"/> being checked.</param>
    /// <param name="check">The <see cref="Type"/> to check against.</param>
    /// <returns>Returns if <paramref name="thisType"/> is or is a subclass of the generic
    /// <paramref name="check"/>.</returns>
    public static bool IsGenericSubclassOf(this Type thisType, Type check)
    {
      // If the wanted type isn't even generic, return false immediately.
      if (!check.IsGenericType)
        return false;

      Type current = thisType;

      // Loop until the current type matches the parent type.
      while (current != null)
      {
        if (current.IsGenericType &&
            current.GetGenericTypeDefinition() == check.GetGenericTypeDefinition())
        {
          return true;
        }

        current = current.BaseType;
      }

      return false; // No match was found.
    }
  }
  /************************************************************************************************/
}