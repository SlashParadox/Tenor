/**************************************************************************************************/
/*!
\file   ILists.cs
\author Craig Williams
\par    Last Updated
        2021-06-08
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A toolkit of functions related to ILists, such as Arrays and Lists.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;

namespace SlashParadox.Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// A series of useful extension and helper functions for dealing with <see cref="IList"/>s and
  /// <see cref="IList{T}"/>s.
  /// </summary>
  public static partial class ILists
  {
    /// <summary>A value to return when an array needs to return an invalid index value.</summary>
    public static readonly int InvalidIndex = -1;

    /// <summary>
    /// An extension function for determining if a non-null <see cref="IList{T}"/> is empty.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <returns>Returns if the <paramref name="il"/>'s count is less or equal to 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEmpty<T>(this IList<T> il)
    {
      return il.Count <= 0;
    }

    /// <summary>
    /// An extension function for determining if a non-null <see cref="IList"/> is empty.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <returns>Returns if the <paramref name="il"/>'s count is less or equal to 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEmptyNG(this IList il)
    {
      return il.Count <= 0;
    }

    /// <summary>
    /// An extension function for determining if a non-null <see cref="IList{T}"/> is not empty.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <returns>Returns if the <paramref name="il"/>'s count is greater than 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotEmpty<T>(this IList<T> il)
    {
      return il.Count > 0;
    }

    /// <summary>
    /// An extension function for determining if a non-null <see cref="IList"/> is not empty.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <returns>Returns if the <paramref name="il"/>'s count is greater than 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotEmptyNG(this IList il)
    {
      return il.Count > 0;
    }

    /// <summary>
    /// An extension function for determining if a <see cref="IList{T}"/> is empty or null.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <returns>Returns if the <paramref name="il"/> is null or the <paramref name="il"/>'s
    /// count is less than or equal to 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEmptyOrNull<T>(this IList<T> il)
    {
      return il == null || il.Count <= 0;
    }

    /// <summary>
    /// An extension function for determining if a <see cref="IList"/> is empty or null.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <returns>Returns if the <paramref name="il"/> is null or the <paramref name="il"/>'s
    /// count is less than or equal to 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEmptyOrNullNG(this IList il)
    {
      return il == null || il.Count <= 0;
    }

    /// <summary>
    /// An extension function for determining if a <see cref="IList{T}"/> is not empty or null.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <returns>Returns if the <paramref name="il"/> is not null and the
    /// <paramref name="il"/>'s count is greater than 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotEmptyOrNull<T>(this IList<T> il)
    {
      return il != null && il.Count > 0;
    }

    /// <summary>
    /// An extension function for determining if a <see cref="IList"/> is not empty or null.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <returns>Returns if the <paramref name="il"/> is not null and the
    /// <paramref name="il"/>'s count is greater than 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotEmptyOrNullNG(this IList il)
    {
      return il != null && il.Count > 0;
    }

    /// <summary>
    /// An extension function for determining if an index is valid for an <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="index">The index to verify.</param>
    /// <returns>Returns if the index is valid for this <paramref name="il"/>.</returns>
    public static bool IsValidIndex<T>(this IList<T> il, int index)
    {
      // The list must not be null, and the index must be between 0 and the list's count.
      return il != null && il.IsValidIndexInternal(index);
    }

    /// <summary>
    /// An extension function for determining if an index is valid for an <see cref="IList"/>.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <param name="index">The index to verify.</param>
    /// <returns>Returns if the index is valid for this <paramref name="il"/>.</returns>
    public static bool IsValidIndexNG(this IList il, int index)
    {
      // The list must not be null, and the index must be between 0 and the list's count.
      return il != null && il.IsValidIndexInternalNG(index);
    }

    /// <summary>
    /// An extension function for determining the last valid index for an <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <returns>Returns the last valid index. If the <paramref name="il"/> is null,
    /// it returns <see cref="ILists.InvalidIndex"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndex<T>(this IList<T> il)
    {
      // If the list is valid, return Count - 1. Otherwise, return an invalid index.
      return il != null ? il.Count - 1 : InvalidIndex;
    }

    /// <summary>
    /// An extension function for determining the last valid index for an <see cref="IList"/>.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <returns>Returns the last valid index. If the <paramref name="il"/> is null,
    /// it returns <see cref="ILists.InvalidIndex"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndexNG(this IList il)
    {
      // If the list is valid, return Count - 1. Otherwise, return an invalid index.
      return il != null ? il.Count - 1 : InvalidIndex;
    }

    /// <summary>
    /// An extension function for getting the last element in an <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <returns>Returns the last element in the <paramref name="il"/>. If the <paramref name="il"/>
    /// is null or empty, it returns <typeparamref name="T"/>'s default value.</returns>
    public static T LastElement<T>(this IList<T> il)
    {
      // The IList must not be null or empty to return a value. Otherwise, it returns the default.
      return il.IsNotEmptyOrNull() ? il[il.LastIndex()] : default;
    }

    /// <summary>
    /// An extension function for getting the last element in an <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="element">The outputted last element. If the <paramref name="il"/> is null or
    /// empty, it returns <typeparamref name="T"/>'s default value.</param>
    /// <returns>Returns if a final element was successfully found.</returns>
    public static bool LastElement<T>(this IList<T> il, out T element)
    {
      // The IList must not be null or empty to return a value.
      if (il.IsNotEmptyOrNull())
      {
        element = il[il.LastIndex()];
        return true;
      }

      // Otherwise, it returns the default.
      element = default;
      return false;
    }

    /// <summary>
    /// An extension function for getting the last element in an <see cref="IList"/>.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <returns>Returns the last element in the <paramref name="il"/>. If the <paramref name="il"/>
    /// is null or empty, it returns <typeparamref name="T"/>'s default value.</returns>
    public static object LastElementNG(this IList il)
    {
      // The IList must not be null or empty to return a value. Otherwise, it returns the default.
      return il.IsNotEmptyOrNullNG() ? il[il.LastIndexNG()] : default;
    }

    /// <summary>
    /// An extension function for getting the last element in an <see cref="IList"/>.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <param name="element">The outputted last element. If the <paramref name="il"/> is null or
    /// empty, it returns <typeparamref name="T"/>'s default value.</param>
    /// <returns>Returns if a final element was successfully found.</returns>
    public static bool LastElementNG(this IList il, out object element)
    {
      // The IList must not be null or empty to return a value.
      if (il.IsNotEmptyOrNullNG())
      {
        element = il[il.LastIndexNG()];
        return true;
      }

      // Otherwise, it returns the default.
      element = default;
      return false;
    }

    /// <summary>
    /// A function for swapping values at two indexes. The value at <paramref name="indexA"/>
    /// becomes the value at <paramref name="indexB"/>, and the value at <paramref name="indexB"/>
    /// becomes the value at <paramref name="indexA"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to swap the values of.</param>
    /// <param name="indexA">The index of the value to swap to <paramref name="indexB"/>.</param>
    /// <param name="indexB">The index of the value to swap to <paramref name="indexA"/>.</param>
    /// <returns>Returns if the swap was successful.</returns>
    public static bool SwapValues<T>(this IList<T> il, int indexA, int indexB)
    {
      // Both indexes must be valid in order to swap.
      if (il.IsValidIndex(indexA) && il.IsValidIndexInternal(indexB))
      {
        T temp = il[indexA]; // Make a temp copy of A.
        il[indexA] = il[indexB]; // Swap A to B.
        il[indexB] = temp; // Swap B to the stored A value.

        return true; // The swap was successful.
      }

      return false; // The swap was not successful.
    }

    /// <summary>
    /// A function for swapping the first of two values. The index of <paramref name="A"/>
    /// gets the value of <paramref name="B"/>, and the index of <paramref name="B"/>
    /// gets the value of <paramref name="A"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to swap the values of.</param>
    /// <param name="A">The value to swap to the index of <paramref name="B"/>.</param>
    /// <param name="B">The value to swap to the index of <paramref name="A"/>.</param>
    /// <returns>Returns if the swap was successful.</returns>
    public static bool SwapValues<T>(this IList<T> il, T A, T B)
    {
      // Get the indexes of the first of each value.
      int indexA;
      int indexB;

      // Special care is required for arrays, which do not implement 'IndexOf' directly.
      if (il is Array)
      {
        Array ar = (il as Array);
        indexA = Array.IndexOf(ar, A);
        indexB = Array.IndexOf(ar, B);
      }
      else
      {
        indexA = il.IndexOf(A);
        indexB = il.IndexOf(B);
      }
      
      // Both indexes must be valid in order to swap.
      if (il.IsValidIndex(indexA) && il.IsValidIndexInternal(indexB))
      {
        T temp = il[indexA]; // Make a temp copy of A.
        il[indexA] = il[indexB]; // Swap A to B.
        il[indexB] = temp; // Swap B to the stored A value.

        return true; // The swap was successful.
      }

      return false; // The swap was not successful.
    }

    /// <summary>
    /// A function for swapping values at two indexes. The value at <paramref name="indexA"/>
    /// becomes the value at <paramref name="indexB"/>, and the value at <paramref name="indexB"/>
    /// becomes the value at <paramref name="indexA"/>.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to swap the values of.</param>
    /// <param name="indexA">The index of the value to swap to <paramref name="indexB"/>.</param>
    /// <param name="indexB">The index of the value to swap to <paramref name="indexA"/>.</param>
    /// <returns>Returns if the swap was successful.</returns>
    public static bool SwapValuesNG(this IList il, int indexA, int indexB)
    {
      // Both indexes must be valid in order to swap.
      if (il.IsValidIndexNG(indexA) && il.IsValidIndexInternalNG(indexB))
      {
        object temp = il[indexA]; // Make a temp copy of A.
        il[indexA] = il[indexB]; // Swap A to B.
        il[indexB] = temp; // Swap B to the stored A value.

        return true; // The swap was successful.
      }

      return false; // The swap was not successful.
    }

    /// <summary>
    /// A function for swapping the first of two values. The index of <paramref name="A"/>
    /// gets the value of <paramref name="B"/>, and the index of <paramref name="B"/>
    /// gets the value of <paramref name="A"/>.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to swap the values of.</param>
    /// <param name="A">The value to swap to the index of <paramref name="B"/>.</param>
    /// <param name="B">The value to swap to the index of <paramref name="A"/>.</param>
    /// <returns>Returns if the swap was successful.</returns>
    public static bool SwapValuesNG(this IList il, object A, object B)
    {
      // Get the indexes of the first of each value.
      int indexA;
      int indexB;

      // Special care is required for arrays, which do not implement 'IndexOf' directly.
      if (il is Array)
      {
        Array ar = (il as Array);
        indexA = Array.IndexOf(ar, A);
        indexB = Array.IndexOf(ar, B);
      }
      else
      {
        indexA = il.IndexOf(A);
        indexB = il.IndexOf(B);
      }

      // Both indexes must be valid in order to swap.
      if (il.IsValidIndexNG(indexA) && il.IsValidIndexInternalNG(indexB))
      {
        object temp = il[indexA]; // Make a temp copy of A.
        il[indexA] = il[indexB]; // Swap A to B.
        il[indexB] = temp; // Swap B to the stored A value.

        return true; // The swap was successful.
      }

      return false; // The swap was not successful.
    }

    /// <summary>
    /// An extension function to add an <paramref name="element"/>, only if it is not already
    /// in the given <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to add to.</param>
    /// <param name="element">The element to add to the <paramref name="il"/>.</param>
    /// <returns>Returns if the <paramref name="element"/> was successfully added.</returns>
    /// <exception cref="NotSupportedException">An array or fixed-sized <see cref="IList{T}"/> is
    /// passed in.</exception>
    public static bool AddUnique<T>(this IList<T> il, T element)
    {
      // Make sure that the element is not already contained. If not, add it to the ilist.
      // We do not check for null to make this act like an 'Add' function.
      if (!il.Contains(element))
      {
        il.Add(element);
        return true;
      }

      return false; // If nothing was added, return false.
    }

    /// <summary>
    /// An extension function to add an <paramref name="element"/>, only if it is not already
    /// in the given <see cref="IList"/>.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to add to.</param>
    /// <param name="element">The element to add to the <paramref name="il"/>.</param>
    /// <returns>Returns if the <paramref name="element"/> was successfully added.</returns>
    /// <exception cref="NotSupportedException">An array or fixed-sized <see cref="IList"/> is
    /// passed in.</exception>
    public static bool AddUniqueNG(this IList il, object element)
    {
      // Make sure that the element is not already contained. If not, add it to the ilist.
      // We do not check for null to make this act like an 'Add' function.
      if (!il.Contains(element))
      {
        il.Add(element);
        return true;
      }

      return false; // If nothing was added, return false.
    }

    /// <summary>
    /// An extension function to set an <paramref name="element"/> at a given
    /// <paramref name="index"/>, only if it is not already in the given <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to add to.</param>
    /// <param name="element">The element to add to the <paramref name="il"/>.</param>
    /// <param name="index">The index to set the <paramref name="element"/> to.</param>
    /// <returns>Returns if the <paramref name="element"/> was successfully added.</returns>
    public static bool SetUnique<T>(this IList<T> il, T element, int index)
    {
      // Make sure that the element is not already contained, and the index is valid.
      // We do not check for null to make this act like an 'Add' function.
      if (il.IsValidIndexInternal(index) && !il.Contains(element))
      {
        il[index] = element;
        return true;
      }

      return false; // If nothing was set, return false.
    }

    /// <summary>
    /// An extension function to set an <paramref name="element"/> at a given
    /// <paramref name="index"/>, only if it is not already in the given <see cref="IList"/>.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to add to.</param>
    /// <param name="element">The element to add to the <paramref name="il"/>.</param>
    /// <param name="index">The index to set the <paramref name="element"/> to.</param>
    /// <returns>Returns if the <paramref name="element"/> was successfully added.</returns>
    public static bool SetUniqueNG(this IList il, object element, int index)
    {
      // Make sure that the element is not already contained, and the index is valid.
      // We do not check for null to make this act like an 'Add' function.
      if (il.IsValidIndexInternalNG(index) && !il.Contains(element))
      {
        il[index] = element;
        return true;
      }

      return false; // If nothing was set, return false.
    }

    /// <summary>
    /// An extension function to insert an <paramref name="element"/> at a given
    /// <paramref name="index"/>, only if it is not already in the given <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to add to.</param>
    /// <param name="element">The element to add to the <paramref name="il"/>.</param>
    /// <param name="index">The index to insert the <paramref name="element"/> to.</param>
    /// <returns>Returns if the <paramref name="element"/> was successfully added.</returns>
    /// <exception cref="NotSupportedException">An array or fixed-sized <see cref="IList{T}"/> is
    /// passed in.</exception>
    public static bool InsertUnique<T>(this IList<T> il, T element, int index)
    {
      // Make sure that the element is not already contained, and the index is valid.
      // We do not check for null to make this act like an 'Add' function.
      if (il.IsValidIndexInternal(index) && !il.Contains(element))
      {
        il.Insert(index, element);
        return true;
      }

      return false; // If nothing was inserted, return false.
    }

    /// <summary>
    /// An extension function to insert an <paramref name="element"/> at a given
    /// <paramref name="index"/>, only if it is not already in the given <see cref="IList"/>.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to add to.</param>
    /// <param name="element">The element to add to the <paramref name="il"/>.</param>
    /// <param name="index">The index to insert the <paramref name="element"/> to.</param>
    /// <returns>Returns if the <paramref name="element"/> was successfully added.</returns>
    /// <exception cref="NotSupportedException">An array or fixed-sized <see cref="IList"/> is
    /// passed in.</exception>
    public static bool InsertUniqueNG(this IList il, object element, int index)
    {
      // Make sure that the element is not already contained, and the index is valid.
      // We do not check for null to make this act like an 'Add' function.
      if (il.IsValidIndexInternalNG(index) && !il.Contains(element))
      {
        il.Insert(index, element);
        return true;
      }

      return false; // If nothing was inserted, return false.
    }

    /// <summary>
    /// An extension function for replacing the first occurence of an element with a new value.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="oldItem">The element to find the occurence of.</param>
    /// <param name="newItem">The element to put into the <paramref name="il"/>."/></param>
    /// <returns>Returns the index of the replaced element.
    /// If no replacement was made, returns <see cref="ILists.InvalidIndex"/>.</returns>
    public static int Replace<T>(this IList<T> il, T oldItem, T newItem)
    {
      // Return immediately if the IList is null.
      if (il == null)
        return InvalidIndex;

      int index = il.IndexOf(oldItem); // Get the first index of the old element.

      // If the index is valid, replace the element.  
      if (il.IsValidIndexInternal(index))
        il[index] = newItem;

      return index; // Return the index.
    }

    /// <summary>
    /// An extension function for replacing all occurences of an element with a different value.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="oldItem">The element to find the occurence of.</param>
    /// <param name="newItem">The element to put into the <paramref name="il"/>."/></param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplaceAll<T>(this IList<T> il, T oldItem, T newItem)
    {
      if (il.IsNotEmptyOrNull())
        return ReplaceRangeInternal(il, oldItem, newItem, 0, il.Count);

      return false;
    }

    /// <summary>
    /// An extension function for replacing the occurences of an element with a different value.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="oldItem">The element to find the occurence of.</param>
    /// <param name="newItem">The element to put into the <paramref name="il"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive.</param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplaceRange<T>(this IList<T> il, T oldItem, T newItem, int startIndex)
    {
      if (il.IsValidIndex(startIndex))
        return ReplaceRangeInternal(il, oldItem, newItem, startIndex, il.Count);

      return false;
    }

    /// <summary>
    /// An extension function for replacing the occurences of an element with a different value.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="oldItem">The element to find the occurence of.</param>
    /// <param name="newItem">The element to put into the <paramref name="il"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplaceRange<T>(this IList<T> il, T oldItem, T newItem, int startIndex,
                                      int lastIndex)
    {
      if (il.IsValidIndex(startIndex) && il.IsValidIndexInternal(lastIndex - 1))
        return ReplaceRangeInternal(il, oldItem, newItem, startIndex, lastIndex);

      return false;
    }

    /// <summary>
    /// An extension function for replacing the occurences of an element with a different value,
    /// based on a given pattern.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="newItem">The element to put into the <paramref name="il"/>.</param>
    /// <param name="pattern">The <see cref="Func{T1, T2, TResult}"/> to determine where to
    /// make replacements. It inputs the Current Item <typeparamref name="T"/> and the index,
    /// outputting a final <see cref="bool"/>.</param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplacePattern<T>(this IList<T> il, T newItem, Func<T, int, bool> pattern)
    {
      if (il.IsNotEmptyOrNull() && pattern != null)
        return il.ReplacePatternInternal(newItem, 0, il.Count, pattern);

      return false;
    }

    /// <summary>
    /// An extension function for replacing the occurences of an element with a different value,
    /// based on a given pattern.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="newItem">The element to put into the <paramref name="il"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="pattern">The <see cref="Func{T1, T2, TResult}"/> to determine where to
    /// make replacements. It inputs the Current Item <typeparamref name="T"/> and the index,
    /// outputting a final <see cref="bool"/>.</param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplacePattern<T>(this IList<T> il, T newItem, int startIndex,
                                          Func<T, int, bool> pattern)
    {
      if (il.IsValidIndex(startIndex) && pattern != null)
        return il.ReplacePatternInternal(newItem, startIndex, il.Count, pattern);

      return false;
    }

    /// <summary>
    /// An extension function for replacing the occurences of an element with a different value,
    /// based on a given pattern.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="newItem">The element to put into the <paramref name="il"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <param name="pattern">The <see cref="Func{T1, T2, TResult}"/> to determine where to
    /// make replacements. It inputs the Current Item <typeparamref name="T"/> and the index,
    /// outputting a final <see cref="bool"/>.</param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplacePattern<T>(this IList<T> il, T newItem, int startIndex,
                                          int lastIndex, Func<T, int, bool> pattern)
    {
      if (il.IsValidIndex(startIndex) && il.IsValidIndexInternal(lastIndex - 1) && pattern != null)
        return il.ReplacePatternInternal(newItem, startIndex, lastIndex, pattern);

      return false;
    }

    /// <summary>
    /// An extension function for replacing the first occurence of an element with a new value.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <param name="oldItem">The element to find the occurence of.</param>
    /// <param name="newItem">The element to put into the <paramref name="il"/>."/></param>
    /// <returns>Returns the index of the replaced element.
    /// If no replacement was made, returns <see cref="ILists.InvalidIndex"/>.</returns>
    public static int ReplaceNG(this IList il, object oldItem, object newItem)
    {
      // Return immediately if the IList is null.
      if (il == null)
        return InvalidIndex;

      int index = il.IndexOf(oldItem); // Get the first index of the old element.

      // If the index is valid, replace the element.  
      if (il.IsValidIndexInternalNG(index))
        il[index] = newItem;

      return index; // Return the index.
    }

    /// <summary>
    /// An extension function for replacing all occurences of an element with a different value.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <param name="oldItem">The element to find the occurence of.</param>
    /// <param name="newItem">The element to put into the <paramref name="il"/>."/></param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplaceAllNG(this IList il, object oldItem, object newItem)
    {
      if (il.IsNotEmptyOrNullNG())
        return ReplaceRangeInternalNG(il, oldItem, newItem, 0, il.Count);

      return false;
    }

    /// <summary>
    /// An extension function for replacing the occurences of an element with a different value.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <param name="oldItem">The element to find the occurence of.</param>
    /// <param name="newItem">The element to put into the <paramref name="il"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive.</param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplaceRangeNG(this IList il, object oldItem, object newItem, int startIndex)
    {
      if (il.IsValidIndexNG(startIndex))
        return ReplaceRangeInternalNG(il, oldItem, newItem, startIndex, il.Count);

      return false;
    }

    /// <summary>
    /// An extension function for replacing the occurences of an element with a different value.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <param name="oldItem">The element to find the occurence of.</param>
    /// <param name="newItem">The element to put into the <paramref name="il"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplaceRangeNG(this IList il, object oldItem, object newItem, int startIndex,
                                      int lastIndex)
    {
      if (il.IsValidIndexNG(startIndex) && il.IsValidIndexInternalNG(lastIndex - 1))
        return ReplaceRangeInternalNG(il, oldItem, newItem, startIndex, lastIndex);

      return false;
    }

    /// <summary>
    /// An extension function for replacing the occurences of an element with a different value,
    /// based on a given pattern.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <param name="newItem">The element to put into the <paramref name="il"/>.</param>
    /// <param name="pattern">The <see cref="Func{T1, T2, TResult}"/> to determine where to
    /// make replacements. It inputs the Current Item and the index, outputting a final
    /// <see cref="bool"/>.</param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplacePatternNG(this IList il, object newItem,
                                         Func<object, int, bool> pattern)
    {
      if (il.IsNotEmptyOrNullNG() && pattern != null)
        return il.ReplacePatternInternalNG(newItem, 0, il.Count, pattern);

      return false;
    }

    /// <summary>
    /// An extension function for replacing the occurences of an element with a different value,
    /// based on a given pattern.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <param name="newItem">The element to put into the <paramref name="il"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="pattern">The <see cref="Func{T1, T2, TResult}"/> to determine where to
    /// make replacements. It inputs the Current Item and the index, outputting a final
    /// <see cref="bool"/>.</param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplacePatternNG(this IList il, object newItem, int startIndex,
                                         Func<object, int, bool> pattern)
    {
      if (il.IsValidIndexNG(startIndex) && pattern != null)
        return il.ReplacePatternInternalNG(newItem, startIndex, il.Count, pattern);

      return false;
    }

    /// <summary>
    /// An extension function for replacing the occurences of an element with a different value,
    /// based on a given pattern.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <param name="newItem">The element to put into the <paramref name="il"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <param name="pattern">The <see cref="Func{T1, T2, TResult}"/> to determine where to
    /// make replacements. It inputs the Current Item and the index, outputting a final
    /// <see cref="bool"/>.</param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplacePatternNG(this IList il, object newItem, int startIndex,
                                         int lastIndex, Func<object, int, bool> pattern)
    {
      if (il.IsValidIndexNG(startIndex) && il.IsValidIndexNG(lastIndex - 1) && pattern != null)
        return il.ReplacePatternInternalNG(newItem, startIndex, lastIndex, pattern);

      return false;
    }

    /// <summary>
    /// An extension function for filling specific areas of an <see cref="IList{T}"/> with a given
    /// value.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="value">The new value to put into the <paramref name="il"/>.</param>
    /// <param name="skip">The number of indexes to skip between each value fill.</param>
    public static bool Fill<T>(this IList<T> il, T value, int skip = 0)
    {
      if (il.IsNotEmptyOrNull())
      {
        il.FillInternal(value, 0, il.Count, skip);
        return true;
      }

      return false;
    }

    /// <summary>
    /// An extension function for filling specific areas of an <see cref="IList{T}"/> with a given
    /// value.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="value">The new value to put into the <paramref name="il"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="skip">The number of indexes to skip between each value fill.</param>
    public static bool Fill<T>(this IList<T> il, T value, int startIndex, int skip = 0)
    {
      if (il.IsValidIndex(startIndex))
      {
        il.FillInternal(value, startIndex, il.Count, skip);
        return true;
      }

      return false;
    }

    /// <summary>
    /// An extension function for filling specific areas of an <see cref="IList{T}"/> with a given
    /// value.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="value">The new value to put into the <paramref name="il"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <param name="skip">The number of indexes to skip between each value fill.</param>
    public static bool Fill<T>(this IList<T> il, T value, int startIndex, int lastIndex,
                               int skip = 0)
    {
      if (il.IsValidIndex(startIndex) && il.IsValidIndexInternal(lastIndex - 1))
      {
        il.FillInternal(value, startIndex, lastIndex, skip);
        return true;
      }

      return false;
    }

    /// <summary>
    /// An extension function for filling specific areas of an <see cref="IList"/> with a given
    /// value.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <param name="value">The new value to put into the <paramref name="il"/>.</param>
    /// <param name="skip">The number of indexes to skip between each value fill.</param>
    public static bool FillNG(this IList il, object value, int skip = 0)
    {
      if (il.IsNotEmptyOrNullNG())
      {
        il.FillInternalNG(value, 0, il.Count, skip);
        return true;
      }

      return false;
    }

    /// <summary>
    /// An extension function for filling specific areas of an <see cref="IList"/> with a given
    /// value.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <param name="value">The new value to put into the <paramref name="il"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="skip">The number of indexes to skip between each value fill.</param>
    public static bool FillNG(this IList il, object value, int startIndex, int skip = 0)
    {
      if (il.IsValidIndexNG(startIndex))
      {
        il.FillInternalNG(value, startIndex, il.Count, skip);
        return true;
      }

      return false;
    }

    /// <summary>
    /// An extension function for filling specific areas of an <see cref="IList"/> with a given
    /// value.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <param name="value">The new value to put into the <paramref name="il"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <param name="skip">The number of indexes to skip between each value fill.</param>
    public static bool FillNG(this IList il, object value, int startIndex, int lastIndex,
                              int skip = 0)
    {
      if (il.IsValidIndexNG(startIndex) && il.IsValidIndexInternalNG(lastIndex - 1))
      {
        il.FillInternalNG(value, startIndex, lastIndex, skip);
        return true;
      }

      return false;
    }

    /// <summary>
    /// A function for shuffling an <see cref="IList{T}"/>'s elements. This uses
    /// <see cref="RandomGenerators.RejectionRandom"/> by default.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Shuffle<T>(this IList<T> il)
    {
      if (il.IsNotEmptyOrNull())
        ShuffleInternal(il, 0, il.Count, RandomGenerators.RejectionRandom);
    }

    /// <summary>
    /// A function for shuffling an <see cref="IList{T}"/>'s elements. This uses
    /// <see cref="RandomGenerators.RejectionRandom"/> by default.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Shuffle<T>(this IList<T> il, int startIndex, int lastIndex)
    {
      if (il.IsValidIndex(startIndex) && il.IsValidIndexInternal(lastIndex))
        ShuffleInternal(il, startIndex, lastIndex, RandomGenerators.RejectionRandom);
    }

    /// <summary>
    /// A function for shuffling an <see cref="IList{T}"/>'s elements.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <param name="generator">The <see cref="Randomization"/> generator to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Shuffle<T>(this IList<T> il, int startIndex, int lastIndex,
                                   RandomGenerators generator)
    {
      if (il.IsValidIndex(startIndex) && il.IsValidIndexInternal(lastIndex))
        ShuffleInternal(il, startIndex, lastIndex, generator);
    }

    /// <summary>
    /// A function for shuffling an <see cref="IList{T}"/>'s elements.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <param name="generator">The <see cref="Random"/> generator to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Shuffle<T>(this IList<T> il, int startIndex, int lastIndex,
                                  Random generator)
    {
      if (il.IsValidIndex(startIndex) && il.IsValidIndexInternal(lastIndex))
        ShuffleInternal(il, startIndex, lastIndex, generator);
    }

    /// <summary>
    /// A function for shuffling an <see cref="IList{T}"/>'s elements.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <param name="generator">The <see cref="RandomNumberGenerator"/> generator to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Shuffle<T>(this IList<T> il, int startIndex, int lastIndex,
                                  RandomNumberGenerator generator)
    {
      if (il.IsValidIndex(startIndex) && il.IsValidIndexInternal(lastIndex))
        ShuffleInternal(il, startIndex, lastIndex, generator);
    }

    /// <summary>
    /// A function for creating a shallow copy of a given <paramref name="array"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="array"/>.</typeparam>
    /// <param name="array">The array to make a copy of.</param>
    /// <returns>Returns the copied array. If the <paramref name="array"/> is empty or
    /// <see langword="null"/>, returns <see langword="null"/>.</returns>
    /// <remarks>As a shallow copy, new values are not created, if the values are reference
    /// types.</remarks>
    public static T[] ShallowCopy<T>(this T[] array)
    {
      // Make sure the array is not null.
      if (array.IsNotEmptyOrNull())
      {
        int length = array.Length; // Get the length.
        T[] copy = new T[length]; // Make a new array with the right length.

        // Copy every value.
        for (int i = 0; i < length; i++)
          copy[i] = array[i];

        return copy; // Return the copy.
      }

      return null; // Return null if the array is null or empty.
    }

    /// <summary>
    /// An extension function for printing the contents of an <see cref="IList{T}"/> to a
    /// <see cref="string"/>. This can be useful for debugging purposes.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to print.</param>
    /// <param name="separator">The separation <see cref="string"/> between each element.</param>
    /// <param name="prefix">The <see cref="string"/> printed before each element.</param>
    /// <param name="suffix">The <see cref="string"/> printed after each element.</param>
    /// <returns></returns>
    public static string Print<T>(this IList<T> il, string separator = ", ", string prefix = "[",
                                  string suffix = "]")
    {
      // If there is no list, return an empty string.
      if (il.IsEmptyOrNull())
        return string.Empty;

      StringBuilder print = new StringBuilder(); // The builder for the printed string.
      int count = il.Count - 1; // The size of the IList, bar the last element.

      // Append every element, bar the last, using the prefix, suffix, and separators.
      for (int i = 0; i < count; i++)
        print.Append(prefix).Append(il[i]).Append(suffix).Append(separator);

      // Append the last element separately so a separator is not added.
      print.Append(prefix).Append(il[count]).Append(suffix);

      return print.ToString(); // Return the final string.
    }

    /// <summary>
    /// An extension function for printing the contents of an <see cref="IList"/> to a
    /// <see cref="string"/>. This can be useful for debugging purposes.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to print.</param>
    /// <param name="separator">The separation <see cref="string"/> between each element.</param>
    /// <param name="prefix">The <see cref="string"/> printed before each element.</param>
    /// <param name="suffix">The <see cref="string"/> printed after each element.</param>
    /// <returns></returns>
    public static string PrintNG(this IList il, string separator = ", ", string prefix = "[",
                                  string suffix = "]")
    {
      // If there is no list, return an empty string.
      if (il.IsEmptyOrNullNG())
        return string.Empty;

      StringBuilder print = new StringBuilder(); // The builder for the printed string.
      int count = il.Count - 1; // The size of the IList, bar the last element.

      // Append every element, bar the last, using the prefix, suffix, and separators.
      for (int i = 0; i < count; i++)
        print.Append(prefix).Append(il[i]).Append(suffix).Append(separator);

      // Append the last element separately so a separator is not added.
      print.Append(prefix).Append(il[count]).Append(suffix);

      return print.ToString(); // Return the final string.
    }

    /// <summary>
    /// An internal function for checking if an index is valid for a given <see cref="IList{T}"/>.
    /// In this scenario, it is known that the <paramref name="il"/> is valid.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="index">The index to verify.</param>
    /// <returns>Returns if the index is valid for this <paramref name="il"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsValidIndexInternal<T>(this IList<T> il, int index)
    {
      return index >= 0 && index < il.Count; // The index must be between 0 and the count.
    }

    /// <summary>
    /// An internal function for checking if an index is valid for a given <see cref="IList"/>.
    /// In this scenario, it is known that the <paramref name="il"/> is valid.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <param name="index">The index to verify.</param>
    /// <returns>Returns if the index is valid for this <paramref name="il"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsValidIndexInternalNG(this IList il, int index)
    {
      return index >= 0 && index < il.Count; // The index must be between 0 and the count.
    }

    /// <summary>
    /// An internal function for replacing the occurences of an element with a new value.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="oldItem">The element to find the occurence of.</param>
    /// <param name="newItem">The element to put into the <paramref name="il"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive.</param>
    /// <param name="lastIndex">The ending index to handle replacing from, exclusive.</param>
    /// <returns>Returns if any replacement was made.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool ReplaceRangeInternal<T>(this IList<T> il, T oldItem, T newItem,
                                                int startIndex, int lastIndex)
    {
      bool validReplacement = false; // The end result of the replacement.

      // Return false if the items equal each other.
      if (!oldItem.Equals(newItem))
      {
        // Iterate through the selection.
        for (int i = startIndex; i < lastIndex; i++)
        {
          // If the old element is found, replace with the new element.
          if (il[i].Equals(oldItem))
          {
            il[i] = newItem;
            validReplacement = true;
          }
        }
      }

      return validReplacement;
    }

    /// <summary>
    /// An internal function for replacing the occurences of an element with a new value.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <param name="oldItem">The element to find the occurence of.</param>
    /// <param name="newItem">The element to put into the <paramref name="il"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive.</param>
    /// <param name="lastIndex">The ending index to handle replacing from, exclusive.</param>
    /// <returns>Returns if any replacement was made.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool ReplaceRangeInternalNG(this IList il, object oldItem, object newItem,
                                                int startIndex, int lastIndex)
    {
      bool validReplacement = false; // The end result of the replacement.

      // Return false if the items equal each other.
      if (!oldItem.Equals(newItem))
      {
        // Iterate through the selection.
        for (int i = startIndex; i < lastIndex; i++)
        {
          // If the old element is found, replace with the new element.
          if (il[i].Equals(oldItem))
          {
            il[i] = newItem;
            validReplacement = true;
          }
        }
      }

      return validReplacement;
    }

    /// <summary>
    /// An internal function for replacing the occurences of an element with a different value,
    /// based on a given pattern.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="newItem">The element to put into the <paramref name="il"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <param name="pattern">The <see cref="Func{T1, T2, TResult}"/> to determine where to
    /// make replacements. It inputs the Current Item <typeparamref name="T"/> and the index,
    /// outputting a final <see cref="bool"/>.</param>
    /// <returns>Returns if any replacement was made.</returns>
    private static bool ReplacePatternInternal<T>(this IList<T> il, T newItem, int startIndex,
                                                  int lastIndex, Func<T, int, bool> pattern)
    {
      bool validReplacement = false; // The end result of the replacement.

      // Iterate through the selection.
      for (int i = startIndex; i < lastIndex; i++)
      {
        // If the pattern matches, replace the current item with the new item.
        if (pattern(il[i], i))
        {
          il[i] = newItem;
          validReplacement = true;
        }
      }

      return validReplacement; // Return whether or not a valid replacement was made.
    }

    /// <summary>
    /// An internal function for replacing the occurences of an element with a different value,
    /// based on a given pattern.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <param name="newItem">The element to put into the <paramref name="il"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <param name="pattern">The <see cref="Func{T1, T2, TResult}"/> to determine where to
    /// make replacements. It inputs the Current Item and the index, outputting a final
    /// <see cref="bool"/>.</param>
    /// <returns>Returns if any replacement was made.</returns>
    private static bool ReplacePatternInternalNG(this IList il, object newItem, int startIndex,
                                                  int lastIndex, Func<object, int, bool> pattern)
    {
      bool validReplacement = false; // The end result of the replacement.

      // Iterate through the selection.
      for (int i = startIndex; i < lastIndex; i++)
      {
        // If the pattern matches, replace the current item with the new item.
        if (pattern(il[i], i))
        {
          il[i] = newItem;
          validReplacement = true;
        }
      }

      return validReplacement; // Return whether or not a valid replacement was made.
    }

    /// <summary>
    /// An internal function for filling specific areas of an <see cref="IList{T}"/> with a given
    /// value.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="value">The new value to put into the <paramref name="il"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <param name="skip">The number of indexes to skip between each value fill.</param>
    private static void FillInternal<T>(this IList<T> il, T value, int startIndex, int lastIndex,
                                        int skip = 0)
    {
      skip = skip <= 0 ? 1 : skip + 1; // Update the skip so we always move at least by 1 index.

      // Fill every index with the new value, based on the skip.
      for (int i = startIndex; i < lastIndex; i += skip)
        il[i] = value;
    }

    /// <summary>
    /// An internal function for filling specific areas of an <see cref="IList"/> with a given
    /// value.
    /// </summary>
    /// <param name="il">The <see cref="IList"/> to check.</param>
    /// <param name="value">The new value to put into the <paramref name="il"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <param name="skip">The number of indexes to skip between each value fill.</param>
    private static void FillInternalNG(this IList il, object value, int startIndex, int lastIndex,
                                       int skip = 0)
    {
      skip = skip <= 0 ? 1 : skip + 1; // Update the skip so we always move at least by 1 index.

      // Fill every index with the new value, based on the skip.
      for (int i = startIndex; i < lastIndex; i += skip)
        il[i] = value;
    }

    /// <summary>
    /// An internal function for shuffling an <see cref="IList{T}"/>'s elements.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <param name="generator">The <see cref="Randomization"/> generator to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ShuffleInternal<T>(this IList<T> il, int startIndex, int lastIndex,
                                           RandomGenerators generator)
    {
      // For every element, swap at least once with another random index.
      for (int i = startIndex; i < lastIndex; i++)
      {
        int randomIndex = Randomization.GetRandomIntIE(generator, i, lastIndex);
        il.SwapValues(i, randomIndex);
      }
    }

    /// <summary>
    /// An internal function for shuffling an <see cref="IList{T}"/>'s elements.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <param name="generator">The <see cref="Random"/> generator to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ShuffleInternal<T>(this IList<T> il, int startIndex, int lastIndex,
                                           Random generator)
    {
      // For every element, swap at least once with another random index.
      for (int i = startIndex; i < lastIndex; i++)
      {
        int randomIndex = Randomization.GetRandomIntIE(generator, i, lastIndex);
        il.SwapValues(i, randomIndex);
      }
    }

    /// <summary>
    /// An internal function for shuffling an <see cref="IList{T}"/>'s elements.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <param name="generator">The <see cref="RandomNumberGenerator"/> generator to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ShuffleInternal<T>(this IList<T> il, int startIndex, int lastIndex,
                                           RandomNumberGenerator generator)
    {
      // For every element, swap at least once with another random index.
      for (int i = startIndex; i < lastIndex; i++)
      {
        int randomIndex = Randomization.GetRandomIntIE(generator, i, lastIndex);
        il.SwapValues(i, randomIndex);
      }
    }
  }
  /************************************************************************************************/
}