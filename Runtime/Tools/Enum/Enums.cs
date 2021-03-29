/**************************************************************************************************/
/*!
\file   Enums.cs
\author Craig Williams
\par    Last Updated
        2021-03-07
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for tool functions related to any kind of enumeration.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System.Collections.Generic;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// A series of useful functions for getting information about <see cref="Enum"/>s.
  /// </summary>
  public static partial class Enums
  {
    /// <summary>
    /// A function to get the number of values of an <see cref="Enum"/>.
    /// </summary>
    /// <typeparam name="TEnum">The type of <see cref="Enum"/> to get the values of.</typeparam>
    /// <returns>Returns the number of values the <see cref="Enum"/> has.
    /// If <typeparamref name="TEnum"/> is not valid, returns -1.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetValueCount<TEnum>() where TEnum : Enum
    {
      return GetValueCount(typeof(TEnum));
    }

    /// <summary>
    /// A function to get the number of values of an <see cref="Enum"/>.
    /// </summary>
    /// <param name="tenum">The type of <see cref="Enum"/> to get the values of.</param>
    /// <returns>Returns the number of values the <see cref="Enum"/> has.
    /// If <paramref name="tenum"/> is not valid, returns -1.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetValueCount(Type tenum)
    {
      return tenum.IsEnum ? Enum.GetValues(tenum).Length : Collection.ILists.BadIndex;
    }

    /// <summary>
    /// A function to create a list of an <see cref="Enum"/>'s value.
    /// </summary>
    /// <typeparam name="TEnum">The type of <see cref="Enum"/> to get the values of.</typeparam>
    /// <returns>Returns a list of <typeparamref name="TEnum"/>s of all values in the enum.
    /// If <typeparamref name="TEnum"/> is not an <see cref="Enum"/>, returns null.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static List<TEnum> GetEnumValueList<TEnum>() where TEnum : Enum
    {
      return GetEnumValueArray<TEnum>().ToList();
    }

    /// <summary>
    /// A function to create an array of an <see cref="Enum"/>'s value.
    /// </summary>
    /// <typeparam name="TEnum">The type of <see cref="Enum"/> to get the values of.</typeparam>
    /// <returns>Returns an array of <typeparamref name="TEnum"/>s of all values in the enum.
    /// If <typeparamref name="TEnum"/> is not an <see cref="Enum"/>, returns null.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TEnum[] GetEnumValueArray<TEnum>() where TEnum : Enum
    {
      return (TEnum[])GetEnumValueArray(typeof(TEnum));
    }

    /// <summary>
    /// A function to create an array of an <see cref="Enum"/>'s value. This returns a
    /// <see cref="System.Array"/>, not a normal array.
    /// </summary>
    /// <param name="tenum">The type of <see cref="Enum"/> to get the values of.</param>
    /// <returns>Returns a <see cref="System.Array"/> of all values in the enum.
    /// If <paramref name="tenum"/> is not an <see cref="Enum"/>, returns null.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Array GetEnumValueArray(Type tenum)
    {
      return tenum.IsEnum ? Enum.GetValues(tenum) : null;
    }
  }
  /************************************************************************************************/
}