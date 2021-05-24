/**************************************************************************************************/
/*!
\file   Enums.cs
\author Craig Williams
\par    Last Updated
        2021-05-21
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A toolkit of functions related to Enums.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CodeParadox.Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// A series of useful extension and helper functions for dealing with <see cref="Enum"/>s.
  /// </summary>
  public static partial class Enums
  {
    /// <summary>
    /// A function to get the number of values in a given <see cref="Enum"/>.
    /// </summary>
    /// <typeparam name="TEnum">The <see cref="Enum"/> type to get the value count of.</typeparam>
    /// <returns>Returns the number of values the <typeparamref name="TEnum"/> has.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetValueCount<TEnum>() where TEnum : Enum
    {
      return Enum.GetValues(typeof(TEnum)).Length; // Get the number of values.
    }

    /// <summary>
    /// A function to get the number of values in a given <see cref="Enum"/>.
    /// </summary>
    /// <param name="enumType">The <see cref="Enum"/> type to get the values of.</param>
    /// <returns>Returns the number of values the <paramref name="enumType"/> has. If
    /// <paramref name="enumType"/> is not an <see cref="Enum"/>, returns
    /// <see cref="ILists.InvalidIndex"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetValueCount(Type enumType)
    {
      // If the type is an enum, return its value count. Otherwise, return an invalid amount.
      return enumType.IsEnum ? Enum.GetValues(enumType).Length : ILists.InvalidIndex;
    }

    /// <summary>
    /// A function to get an array of the values within a given <see cref="Enum"/> type.
    /// </summary>
    /// <typeparam name="TEnum">The <see cref="Enum"/> type to get the values of.</typeparam>
    /// <returns>Returns an array of the <typeparamref name="TEnum"/> values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TEnum[] GetValueArray<TEnum>() where TEnum : Enum
    {
      return (TEnum[])Enum.GetValues(typeof(TEnum));
    }

    /// <summary>
    /// A function to get an array of the values within a given <see cref="Enum"/> type.
    /// </summary>
    /// <param name="enumType">The <see cref="Enum"/> type to get the values of.</param>
    /// <returns>Returns an array of the <typeparamref name="TEnum"/> values. If
    /// <paramref name="enumType"/> is not an <see cref="Enum"/>, returns null.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Array GetValueArray(Type enumType)
    {
      // If the type is an enum, return its value array. Otherwise, return null.
      return enumType.IsEnum ? Enum.GetValues(enumType) : null;
    }

    /// <summary>
    /// A function to get an <see cref="List{T}"/> of the values within a given <see cref="Enum"/>
    /// type.
    /// </summary>
    /// <typeparam name="TEnum">The <see cref="Enum"/> type to get the values of.</typeparam>
    /// <returns>Returns an <see cref="List{T}"/> of the <typeparamref name="TEnum"/>
    /// values.</returns>
    public static List<TEnum> GetValueList<TEnum>() where TEnum : Enum
    {
      TEnum[] array = GetValueArray<TEnum>(); // Get the array of values.

      // Create a list sized to the array length. We avoid using LINQ due to speed concerns.
      int count = array.Length;
      List<TEnum> list = new List<TEnum>(count);

      // Add all enum values to the list.
      for (int i = 0; i < count; i++)
        list.Add(array[i]);

      return list; // Return the final list.
    }
  }
  /************************************************************************************************/
}