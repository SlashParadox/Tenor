/**************************************************************************************************/
/*!
\file   ILists.cs
\author Craig Williams
\par    Last Updated
        2021-04-01
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for tool functions related to ILists. ILists include arrays and lists.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Tenor.Tools.Collection
{
  /************************************************************************************************/
  /// <summary>
  /// A class of extra functions for <see cref="IList"/>s in general.
  /// </summary>
  public static partial class ILists
  {
    /// <summary>A value to return when an array needs to return a bad index value.</summary>
    public static readonly int BadIndex = -1;

    /// <summary>
    /// An extension function for determining if a non-null <see cref="IList"/> is empty.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <returns>Returns if the <paramref name="ilist"/>'scount is less or equal to 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEmpty<T>(this IList<T> ilist)
    {
      return ilist.Count <= 0;
    }

    /// <summary>
    /// An extension function for determining if a non-null <see cref="IList"/> is empty.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <returns>Returns if the <paramref name="ilist"/>'s count is less or equal to 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEmptyNG(this IList ilist)
    {
      return ilist.Count <= 0;
    }

    /// <summary>
    /// An extension function for determining if a non-null <see cref="IList"/> is not empty.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <returns>Returns if the <paramref name="ilist"/>'scount is greater than 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotEmpty<T>(this IList<T> ilist)
    {
      return ilist.Count > 0;
    }

    /// <summary>
    /// An extension function for determining if a non-null <see cref="IList"/> is not empty.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <returns>Returns if the <paramref name="ilist"/>'s count is greater than 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotEmptyNG(this IList ilist)
    {
      return ilist.Count > 0;
    }

    /// <summary>
    /// An extension function for determining if a <see cref="IList"/> is empty or null.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <returns>Returns if the <paramref name="ilist"/> is null or the <paramref name="ilist"/>'s
    /// count is less than or equal to 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEmptyOrNull<T>(this IList<T> ilist)
    {
      return ilist == null || ilist.Count <= 0;
    }

    /// <summary>
    /// An extension function for determining if a <see cref="IList"/> is empty or null.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <returns>Returns if the <paramref name="ilist"/> is null or the <paramref name="ilist"/>'s
    /// count is less than or equal to 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEmptyOrNullNG(this IList ilist)
    {
      return ilist == null || ilist.Count <= 0;
    }

    /// <summary>
    /// An extension function for determining if a <see cref="IList"/> is not empty or null.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <returns>Returns if the <paramref name="ilist"/> is not null and the
    /// <paramref name="ilist"/>'s count is greater than 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotEmptyOrNull<T>(this IList<T> ilist)
    {
      return ilist != null && ilist.Count > 0;
    }

    /// <summary>
    /// An extension function for determining if a <see cref="IList"/> is not empty or null.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <returns>Returns if the <paramref name="ilist"/> is not null and the
    /// <paramref name="ilist"/>'s count is greater than 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotEmptyOrNullNG(this IList ilist)
    {
      return ilist != null && ilist.Count > 0;
    }

    /// <summary>
    /// An extension function for determining if a given index is valid for an <see cref="IList"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="index">The index to verify.</param>
    /// <returns>Returns if the index is valid for this <paramref name="ilist"/>.</returns>
    public static bool IsValidIndex<T>(this IList<T> ilist, int index)
    {
      // The list must not be null, and the index must be between 0 and the list's count.
      return index >= 0 && ilist != null && index < ilist.Count;
    }

    /// <summary>
    /// An extension function for determining if a given index is valid for an <see cref="IList"/>.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="index">The index to verify.</param>
    /// <returns>Returns if the index is valid for this <paramref name="ilist"/>.</returns>
    public static bool IsValidIndexNG(this IList ilist, int index)
    {
      // The list must not be null, and the index must be between 0 and the list's count.
      return index >= 0 && ilist != null && index < ilist.Count;
    }

    /// <summary>
    /// An extension function for determining the last valid index for an <see cref="IList"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <returns>Returns the last valid index. If the <paramref name="ilist"/> is null,
    /// it returns <see cref="BadIndex"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndex<T>(this IList<T> ilist)
    {
      // If the list is valid, return Count - 1. Otherwise, return a bad index.
      return ilist != null ? ilist.Count - 1 : BadIndex;
    }

    /// <summary>
    /// An extension function for determining the last valid index for an <see cref="IList"/>.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <returns>Returns the last valid index. If the <paramref name="ilist"/> is null,
    /// it returns <see cref="BadIndex"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndexNG(this IList ilist)
    {
      // If the list is valid, return Count - 1. Otherwise, return a bad index.
      return ilist != null ? ilist.Count - 1 : BadIndex;
    }

    /// <summary>
    /// An extension function for getting the last element in an <see cref="IList"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <returns>Returns the last element in the <paramref name="ilist"/>.
    /// If the <paramref name="ilist"/> is null or empty, it returns <typeparamref name="T"/>'s
    /// default value.</returns>
    public static T LastElement<T>(this IList<T> ilist)
    {
      // The IList must not be null, nor a size of 0 to return a value. Otherwise, it returns the default.
      return ilist != null && ilist.Count > 0 ? ilist[ilist.Count - 1] : default;
    }

    /// <summary>
    /// An extension function for getting the last element in an <see cref="IList"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="element">The outputted last element.
    /// If the <paramref name="ilist"/> is null or empty, it returns <typeparamref name="T"/>'s
    /// default value.</param>
    /// <returns>Returns if a final element was successfully found.</returns>
    public static bool LastElement<T>(this IList<T> ilist, out T element)
    {
      // The IList must not be null, nor a size of 0 to return a value.
      if (ilist != null && ilist.Count > 0)
      {
        element = ilist[ilist.Count - 1];
        return true;
      }

      // Otherwise, it returns the default.
      element = default;
      return false;
    }

    /// <summary>
    /// An extension function for getting the last element in an <see cref="IList"/>.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <returns>Returns the last element in the <paramref name="ilist"/>.
    /// If the <paramref name="ilist"/> is null or empty, it returns <typeparamref name="T"/>'s
    /// default value.</returns>
    public static object LastElementNG(this IList ilist)
    {
      // The IList must not be null, nor a size of 0 to return a value. Otherwise, it returns the default.
      return ilist != null && ilist.Count > 0 ? ilist[ilist.Count - 1] : default;
    }

    /// <summary>
    /// An extension function for getting the last element in an <see cref="IList"/>.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="element">The outputted last element.
    /// If the <paramref name="ilist"/> is null or empty, it returns <typeparamref name="T"/>'s
    /// default value.</param>
    /// <returns>Returns if a final element was successfully found.</returns>
    public static bool LastElementNG(this IList ilist, out object element)
    {
      // The IList must not be null, nor a size of 0 to return a value.
      if (ilist != null && ilist.Count > 0)
      {
        element = ilist[ilist.Count - 1];
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
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList{T}"/> to swap the values of.</param>
    /// <param name="indexA">The index of the value to swap to <paramref name="indexB"/>.</param>
    /// <param name="indexB">The index of the value to swap to <paramref name="indexA"/>.</param>
    /// <returns>Returns if the swap was successful.</returns>
    public static bool SwapValues<T>(this IList<T> ilist, int indexA, int indexB)
    {
      // Both indexes must be valid in order to swap.
      if (ilist.IsValidIndex(indexA) && ilist.IsValidIndex(indexB))
      {
        T temp = ilist[indexA]; // Make a temp value of A.
        ilist[indexA] = ilist[indexB]; // Swap A to B.
        ilist[indexB] = temp; // Swap B to the stored A value.

        return true; // The swap was successful.
      }

      return false; // The swap was not successful.
    }

    /// <summary>
    /// A function for swapping the first of two values. The index of <paramref name="A"/>
    /// gets the value of <paramref name="B"/>, and the index of <paramref name="B"/>
    /// gets the value of <paramref name="A"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList{T}"/> to swap the values of.</param>
    /// <param name="A">The value to swap to the index of <paramref name="B"/>.</param>
    /// <param name="B">The value to swap to the index of <paramref name="A"/>.</param>
    /// <returns>Returns if the swap was successful.</returns>
    public static bool SwapValues<T>(this IList<T> ilist, T A, T B)
    {
      // Get the indexes of the first of each value.
      int indexA = ilist.IndexOf(A);
      int indexB = ilist.IndexOf(B);

      // Both indexes must be valid in order to swap.
      if (ilist.IsValidIndex(indexA) && ilist.IsValidIndex(indexB))
      {
        T temp = ilist[indexA]; // Make a temp value of A.
        ilist[indexA] = ilist[indexB]; // Swap A to B.
        ilist[indexB] = temp; // Swap B to the stored A value.

        return true; // The swap was successful.
      }
      
      return false; // The swap was not successful.
    }

    /// <summary>
    /// A function for swapping values at two indexes. The value at <paramref name="indexA"/>
    /// becomes the value at <paramref name="indexB"/>, and the value at <paramref name="indexB"/>
    /// becomes the value at <paramref name="indexA"/>.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to swap the values of.</param>
    /// <param name="indexA">The index of the value to swap to <paramref name="indexB"/>.</param>
    /// <param name="indexB">The index of the value to swap to <paramref name="indexA"/>.</param>
    /// <returns>Returns if the swap was successful.</returns>
    public static bool SwapValuesNG(this IList ilist, int indexA, int indexB)
    {
      // Both indexes must be valid in order to swap.
      if (ilist.IsValidIndexNG(indexA) && ilist.IsValidIndexNG(indexB))
      {
        object temp = ilist[indexA]; // Make a temp value of A.
        ilist[indexA] = ilist[indexB]; // Swap A to B.
        ilist[indexB] = temp; // Swap B to the stored A value.

        return true; // The swap was successful.
      }

      return false; // The swap was not successful.
    }

    /// <summary>
    /// A function for swapping the first of two values. The index of <paramref name="A"/>
    /// gets the value of <paramref name="B"/>, and the index of <paramref name="B"/>
    /// gets the value of <paramref name="A"/>.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to swap the values of.</param>
    /// <param name="A">The value to swap to the index of <paramref name="B"/>.</param>
    /// <param name="B">The value to swap to the index of <paramref name="A"/>.</param>
    /// <returns>Returns if the swap was successful.</returns>
    public static bool SwapValuesNG(this IList ilist, object A, object B)
    {
      // Get the indexes of the first of each value.
      int indexA = ilist.IndexOf(A);
      int indexB = ilist.IndexOf(B);

      // Both indexes must be valid in order to swap.
      if (ilist.IsValidIndexNG(indexA) && ilist.IsValidIndexNG(indexB))
      {
        object temp = ilist[indexA]; // Make a temp value of A.
        ilist[indexA] = ilist[indexB]; // Swap A to B.
        ilist[indexB] = temp; // Swap B to the stored A value.

        return true; // The swap was successful.
      }

      return false; // The swap was not successful.
    }

    /// <summary>
    /// An extension function for replacing the first occurence of an element with a different value.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="oldElement">The element to find the occurence of.</param>
    /// <param name="newElement">The element to put into the <paramref name="ilist"/>."/></param>
    /// <returns>Returns the index of the replaced element.
    /// If no replacement was made, returns <see cref="BadIndex"/>.</returns>
    public static int Replace<T>(this IList<T> ilist, T oldElement, T newElement)
    {
      // Return immediately if the IList is null.
      if (ilist == null)
        return BadIndex;

      int index = ilist.IndexOf(oldElement); // Get the first index of the old element.

      // If the index is valid, replace the element.
      if (ilist.IsValidIndex(index))
        ilist[index] = newElement;

      return index; // Return the index.
    }

    /// <summary>
    /// An extension function for replacing all occurences of an element with a different value.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="oldElement">The element to find the occurence of.</param>
    /// <param name="newElement">The element to put into the <paramref name="ilist"/>."/></param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplaceAll<T>(this IList<T> ilist, T oldElement, T newElement)
    {
      if (!ilist.IsEmptyOrNull())
        return ReplaceRangeInternal(ilist, oldElement, newElement, 0, ilist.Count);

      return false;
    }

    /// <summary>
    /// An extension function for replacing the occurences of an element with a different value.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="oldElement">The element to find the occurence of.</param>
    /// <param name="newElement">The element to put into the <paramref name="ilist"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive.</param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplaceRange<T>(this IList<T> ilist, T oldElement, T newElement, int startIndex)
    {
      if (!ilist.IsEmptyOrNull())
        return ReplaceRangeInternal(ilist, oldElement, newElement, startIndex, ilist.Count);

      return false;
    }

    /// <summary>
    /// An extension function for replacing the occurences of an element with a different value.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="oldElement">The element to find the occurence of.</param>
    /// <param name="newElement">The element to put into the <paramref name="ilist"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplaceRange<T>(this IList<T> ilist, T oldElement, T newElement, int startIndex, int lastIndex)
    {
      if (!ilist.IsEmptyOrNull())
        return ReplaceRangeInternal(ilist, oldElement, newElement, startIndex, lastIndex);

      return false;
    }

    /// <summary>
    /// An internal function for replacing the occurences of an element with a different value.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="oldElement">The element to find the occurence of.</param>
    /// <param name="newElement">The element to put into the <paramref name="ilist"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <returns>Returns if any replacement was made.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool ReplaceRangeInternal<T>(this IList<T> ilist, T oldElement, T newElement, int startIndex, int lastIndex)
    {
      // Return false if the elements are equal, the indexes are invalid, or the old element is not found.
      if (oldElement.Equals(newElement) || !ilist.IsValidIndex(startIndex) || !ilist.IsValidIndex(lastIndex - 1) || !ilist.IsValidIndex(ilist.IndexOf(oldElement)))
        return false;

      // Iterate through the selection.
      for (int i = startIndex; i < lastIndex; i++)
      {
        // If the old element is found, replace with the new element.
        if (ilist[i].Equals(oldElement))
          ilist[i] = newElement;
      }

      return true;
    }

    /// <summary>
    /// An extension function for replacing the occurences of an element with a different value.
    /// This skips every <paramref name="skip"/> elements.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="oldElement">The element to find the occurence of.</param>
    /// <param name="newElement">The element to put into the <paramref name="ilist"/>.</param>
    /// <param name="skip">The amount of valid replacements to skip.</param>
    /// <returns></returns>
    public static bool ReplaceEveryOther<T>(this IList<T> ilist, T oldElement, T newElement, int skip)
    {
      if (!ilist.IsEmptyOrNull())
        return ReplaceEveryOtherInternal(ilist, oldElement, newElement, 0, ilist.Count, skip);

      return false;
    }

    /// <summary>
    /// An extension function for replacing the occurences of an element with a different value.
    /// This skips every <paramref name="skip"/> elements.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="oldElement">The element to find the occurence of.</param>
    /// <param name="newElement">The element to put into the <paramref name="ilist"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="skip">The amount of valid replacements to skip.</param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplaceEveryOther<T>(this IList<T> ilist, T oldElement, T newElement, int startIndex, int skip)
    {
      if (!ilist.IsEmptyOrNull())
        return ReplaceEveryOtherInternal(ilist, oldElement, newElement, startIndex, ilist.Count, skip);

      return false;
    }

    /// <summary>
    /// An extension function for replacing the occurences of an element with a different value.
    /// This skips every <paramref name="skip"/> elements.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="oldElement">The element to find the occurence of.</param>
    /// <param name="newElement">The element to put into the <paramref name="ilist"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <param name="skip">The amount of valid replacements to skip.</param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplaceEveryOther<T>(this IList<T> ilist, T oldElement, T newElement, int startIndex, int lastIndex, int skip)
    {
      if (!ilist.IsEmptyOrNull())
        return ReplaceEveryOtherInternal(ilist, oldElement, newElement, startIndex, lastIndex, skip);

      return false;
    }

    /// <summary>
    /// An internal function for replacing the occurences of an element with a different value.
    /// This skips every <paramref name="skip"/> elements.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="oldElement">The element to find the occurence of.</param>
    /// <param name="newElement">The element to put into the <paramref name="ilist"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <param name="skip">The amount of valid replacements to skip.</param>
    /// <returns>Returns if any replacement was made.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool ReplaceEveryOtherInternal<T>(this IList<T> ilist, T oldElement, T newElement, int startIndex, int lastIndex, int skip)
    {
      // Return false if the IList is null, the elements are equal, the indexes are invalid, or the old element is not found.
      if (oldElement.Equals(newElement) || !ilist.IsValidIndex(startIndex) || !ilist.IsValidIndex(lastIndex - 1) || !ilist.IsValidIndex(ilist.IndexOf(oldElement)))
        return false;

      int currentSkip = 0; // The current amount of elements skipped.

      // Iterate through the selection.
      for (int i = startIndex; i < lastIndex; i++)
      {
        // If the old element is found, replace with the new element.
        if (ilist[i].Equals(oldElement))
        {
          if (++currentSkip >= skip)
          {
            currentSkip = 0;
            ilist[i] = newElement;
          }
        }
      }

      return true;
    }

    /// <summary>
    /// An extension function for replacing the first occurence of an element with a different value.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="oldElement">The element to find the occurence of.</param>
    /// <param name="newElement">The element to put into the <paramref name="ilist"/>.</param>
    /// <returns>Returns the index of the replaced element.
    /// If no replacement was made, returns <see cref="BadIndex"/>.</returns>
    public static int ReplaceNG(this IList ilist, object oldElement, object newElement)
    {
      // Return immediately if the IList is null.
      if (ilist == null)
        return BadIndex;

      int index = ilist.IndexOf(oldElement); // Get the first index of the old element.

      // If the index is valid, replace the element.
      if (ilist.IsValidIndexNG(index))
        ilist[index] = newElement;

      return index; // Return the index.
    }

    /// <summary>
    /// An extension function for replacing all occurences of an element with a different value.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="oldElement">The element to find the occurence of.</param>
    /// <param name="newElement">The element to put into the <paramref name="ilist"/>.</param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplaceAllNG(this IList ilist, object oldElement, object newElement)
    {
      if (!ilist.IsEmptyOrNullNG())
        return ReplaceRangeInternalNG(ilist, oldElement, newElement, 0, ilist.Count);

      return false;
    }

    /// <summary>
    /// An extension function for replacing the occurences of an element with a different value.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="oldElement">The element to find the occurence of.</param>
    /// <param name="newElement">The element to put into the <paramref name="ilist"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive.</param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplaceRangeNG(this IList ilist, object oldElement, object newElement, int startIndex)
    {
      if (!ilist.IsEmptyOrNullNG())
        return ReplaceRangeInternalNG(ilist, oldElement, newElement, startIndex, ilist.Count);

      return false;
    }

    /// <summary>
    /// An extension function for replacing the occurences of an element with a different value.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="oldElement">The element to find the occurence of.</param>
    /// <param name="newElement">The element to put into the <paramref name="ilist"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplaceRangeNG(this IList ilist, object oldElement, object newElement, int startIndex, int lastIndex)
    {
      if (!ilist.IsEmptyOrNullNG())
        return ReplaceRangeInternalNG(ilist, oldElement, newElement, startIndex, lastIndex);

      return false;
    }

    /// <summary>
    /// An internal function for replacing the occurences of an element with a different value.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="oldElement">The element to find the occurence of.</param>
    /// <param name="newElement">The element to put into the <paramref name="ilist"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <returns>Returns if any replacement was made.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool ReplaceRangeInternalNG(this IList ilist, object oldElement, object newElement, int startIndex, int lastIndex)
    {
      // Return false if the elements are equal, the indexes are invalid, or the old element is not found.
      if (oldElement.Equals(newElement) || !ilist.IsValidIndexNG(startIndex) || !ilist.IsValidIndexNG(lastIndex - 1) || !ilist.IsValidIndexNG(ilist.IndexOf(oldElement)))
        return false;

      // Iterate through the selection.
      for (int i = startIndex; i < lastIndex; i++)
      {
        // If the old element is found, replace with the new element.
        if (ilist[i].Equals(oldElement))
          ilist[i] = newElement;
      }

      return true;
    }

    /// <summary>
    /// An extension function for replacing the occurences of an element with a different value.
    /// This skips every <paramref name="skip"/> elements.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="oldElement">The element to find the occurence of.</param>
    /// <param name="newElement">The element to put into the <paramref name="ilist"/>.</param>
    /// <param name="skip">The amount to skip each iteration.</param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplaceEveryOtherNG(this IList ilist, object oldElement, object newElement, int skip)
    {
      if (!ilist.IsEmptyOrNullNG())
        return ReplaceEveryOtherInternalNG(ilist, oldElement, newElement, 0, ilist.Count, skip);

      return false;
    }

    /// <summary>
    /// An extension function for replacing the occurences of an element with a different value.
    /// This skips every <paramref name="skip"/> elements.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="oldElement">The element to find the occurence of.</param>
    /// <param name="newElement">The element to put into the <paramref name="ilist"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="skip">The amount to skip each iteration.</param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplaceEveryOtherNG(this IList ilist, object oldElement, object newElement, int startIndex, int skip)
    {
      if (!ilist.IsEmptyOrNullNG())
        return ReplaceEveryOtherInternalNG(ilist, oldElement, newElement, startIndex, ilist.Count, skip);

      return false;
    }

    /// <summary>
    /// An extension function for replacing the occurences of an element with a different value.
    /// This skips every <paramref name="skip"/> elements.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="oldElement">The element to find the occurence of.</param>
    /// <param name="newElement">The element to put into the <paramref name="ilist"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <param name="skip">The amount to skip each iteration.</param>
    /// <returns>Returns if any replacement was made.</returns>
    public static bool ReplaceEveryOtherNG(this IList ilist, object oldElement, object newElement, int startIndex, int lastIndex, int skip)
    {
      if (!ilist.IsEmptyOrNullNG())
        return ReplaceEveryOtherInternalNG(ilist, oldElement, newElement, startIndex, lastIndex, skip);

      return false;
    }

    /// <summary>
    /// An internal function for replacing the occurences of an element with a different value.
    /// This skips every <paramref name="skip"/> elements.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="oldElement">The element to find the occurence of.</param>
    /// <param name="newElement">The element to put into the <paramref name="ilist"/>.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <param name="skip">The amount to skip each iteration.</param>
    /// <returns>Returns if any replacement was made.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool ReplaceEveryOtherInternalNG(this IList ilist, object oldElement, object newElement, int startIndex, int lastIndex, int skip)
    {
      // Return false if the IList is null, the elements are equal, the indexes are invalid, or the old element is not found.
      if (oldElement.Equals(newElement) || !ilist.IsValidIndexNG(startIndex) || !ilist.IsValidIndexNG(lastIndex - 1) || !ilist.IsValidIndexNG(ilist.IndexOf(oldElement)))
        return false;

      int currentSkip = 0; // The current amount of elements skipped.

      // Iterate through the selection.
      for (int i = startIndex; i < lastIndex; i++)
      {
        // If the old element is found, replace with the new element.
        if (ilist[i].Equals(oldElement))
        {
          if (++currentSkip >= skip)
          {
            currentSkip = 0;
            ilist[i] = newElement;
          }
        }
      }

      return true;
    }

    /// <summary>
    /// An extension function for filling an <see cref="IList"/> with a new value.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="value">The value to put into the <paramref name="ilist"/>.</param>
    /// <returns>Returns if the fill was successful.</returns>
    public static bool Fill<T>(this IList<T> ilist, T value)
    {
      if (!ilist.IsEmptyOrNull())
        return FillInternal(ilist, value, 0, ilist.Count, 1);

      return false;
    }

    /// <summary>
    /// An extension function for filling a range of indexes in an <see cref="IList"/> with a value.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="value">The value to put into the <paramref name="ilist"/>.</param>
    /// <param name="startIndex">The starting index to fill, inclusive.</param>
    /// <returns>Returns if the fill was successful.</returns>
    public static bool Fill<T>(this IList<T> ilist, T value, int startIndex)
    {
      if (!ilist.IsEmptyOrNull())
        return FillInternal(ilist, value, startIndex, ilist.Count, 1);

      return false;
    }

    /// <summary>
    /// An extension function for filling a range of indexes in an <see cref="IList"/> with a value.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="value">The value to put into the <paramref name="ilist"/>.</param>
    /// <param name="startIndex">The starting index to fill, inclusive.</param>
    /// <param name="lastIndex">The ending index to fill, exclusive.</param>
    /// <returns>Returns if the fill was successful.</returns>
    public static bool Fill<T>(this IList<T> ilist, T value, int startIndex, int lastIndex)
    {
      if (!ilist.IsEmptyOrNull())
        return FillInternal(ilist, value, startIndex, lastIndex, 1);

      return false;
    }

    /// <summary>
    /// An extension function for filling a range of indexes in an <see cref="IList"/> with a value.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="value">The value to put into the <paramref name="ilist"/>.</param>
    /// <param name="skip">The amount of elements to skip before filling.
    /// It must be greater than 0.</param>
    /// <returns>Returns if the fill was successful.</returns>
    public static bool FillEveryOther<T>(this IList<T> ilist, T value, int skip)
    {
      if (!ilist.IsEmptyOrNull())
        return FillInternal(ilist, value, 0, ilist.Count, skip);

      return false;
    }

    /// <summary>
    /// An extension function for filling a range of indexes in an <see cref="IList"/> with a value.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="value">The value to put into the <paramref name="ilist"/>.</param>
    /// <param name="startIndex">The starting index to fill, inclusive.</param>
    /// <param name="skip">The amount of elements to skip before filling.
    /// It must be greater than 0.</param>
    /// <returns>Returns if the fill was successful.</returns>
    public static bool FillEveryOther<T>(this IList<T> ilist, T value, int startIndex, int skip)
    {
      if (!ilist.IsEmptyOrNull())
        return FillInternal(ilist, value, startIndex, ilist.Count, skip);

      return false;
    }

    /// <summary>
    /// An extension function for filling a range of indexes in an <see cref="IList"/> with a value.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="value">The value to put into the <paramref name="ilist"/>.</param>
    /// <param name="startIndex">The starting index to fill, inclusive.</param>
    /// <param name="lastIndex">The ending index to fill, exclusive.</param>
    /// <param name="skip">The amount of elements to skip before filling.
    /// It must be greater than 0.</param>
    /// <returns>Returns if the fill was successful.</returns>
    public static bool FillEveryOther<T>(this IList<T> ilist, T value, int startIndex, int lastIndex, int skip)
    {
      if (!ilist.IsEmptyOrNull())
        return FillInternal(ilist, value, startIndex, lastIndex, skip);

      return false;
    }

    /// <summary>
    /// An internal function for filling a range of indexes in an <see cref="IList"/> with a value.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="value">The value to put into the <paramref name="ilist"/>.</param>
    /// <param name="startIndex">The starting index to fill, inclusive.</param>
    /// <param name="lastIndex">The ending index to fill, exclusive.</param>
    /// <param name="skip">The amount of elements to skip before filling.
    /// It must be greater than 0.</param>
    /// <returns>Returns if the fill was successful.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool FillInternal<T>(this IList<T> ilist, T value, int startIndex, int lastIndex, int skip)
    {
      // Return false if the indexes are invalid or the skip is less than 0.
      if (!ilist.IsValidIndex(startIndex) || !ilist.IsValidIndex(lastIndex - 1) || skip <= 0)
        return false;

      // For the range of values [startIndex, lastIndex), replace the value within.
      for (int i = startIndex; i < lastIndex; i += skip)
        ilist[i] = value;

      return true; // A replacement was made.
    }

    /// <summary>
    /// An extension function for filling an <see cref="IList"/> with a new value.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="value">The value to put into the <paramref name="ilist"/>.</param>
    /// <returns>Returns if the fill was successful.</returns>
    public static bool FillNG(this IList ilist, object value)
    {
      if (!ilist.IsEmptyOrNullNG())
        return FillInternalNG(ilist, value, 0, ilist.Count, 1);

      return false;
    }

    /// <summary>
    /// An extension function for filling a range of indexes in an <see cref="IList"/> with a value.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="value">The value to put into the <paramref name="ilist"/>.</param>
    /// <param name="startIndex">The starting index to fill, inclusive.</param>
    /// <returns>Returns if the fill was successful.</returns>
    public static bool FillNG(this IList ilist, object value, int startIndex)
    {
      if (!ilist.IsEmptyOrNullNG())
        return FillInternalNG(ilist, value, startIndex, ilist.Count, 1);

      return false;
    }

    /// <summary>
    /// An extension function for filling a range of indexes in an <see cref="IList"/> with a value.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="value">The value to put into the <paramref name="ilist"/>.</param>
    /// <param name="startIndex">The starting index to fill, inclusive.</param>
    /// <param name="lastIndex">The ending index to fill, exclusive.</param>
    /// <returns>Returns if the fill was successful.</returns>
    public static bool FillNG(this IList ilist, object value, int startIndex, int lastIndex)
    {
      if (!ilist.IsEmptyOrNullNG())
        return FillInternalNG(ilist, value, startIndex, lastIndex, 1);

      return false;
    }

    /// <summary>
    /// An extension function for filling a range of indexes in an <see cref="IList"/> with a value.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="value">The value to put into the <paramref name="ilist"/>.</param>
    /// <param name="skip">The amount of elements to skip before filling.
    /// It must be greater than 0.</param>
    /// <returns>Returns if the fill was successful.</returns>
    public static bool FillEveryOtherNG(this IList ilist, object value, int skip)
    {
      if (!ilist.IsEmptyOrNullNG())
        return FillInternalNG(ilist, value, 0, ilist.Count, skip);

      return false;
    }

    /// <summary>
    /// An extension function for filling a range of indexes in an <see cref="IList"/> with a value.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="value">The value to put into the <paramref name="ilist"/>.</param>
    /// <param name="startIndex">The starting index to fill, inclusive.</param>
    /// <param name="skip">The amount of elements to skip before filling.
    /// It must be greater than 0.</param>
    /// <returns>Returns if the fill was successful.</returns>
    public static bool FillEveryOtherNG(this IList ilist, object value, int startIndex, int skip)
    {
      if (!ilist.IsEmptyOrNullNG())
        return FillInternalNG(ilist, value, startIndex, ilist.Count, skip);

      return false;
    }

    /// <summary>
    /// An extension function for filling a range of indexes in an <see cref="IList"/> with a value.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="value">The value to put into the <paramref name="ilist"/>.</param>
    /// <param name="startIndex">The starting index to fill, inclusive.</param>
    /// <param name="lastIndex">The ending index to fill, exclusive.</param>
    /// <param name="skip">The amount of elements to skip before filling.
    /// It must be greater than 0.</param>
    /// <returns>Returns if the fill was successful.</returns>
    public static bool FillEveryOtherNG(this IList ilist, object value, int startIndex, int lastIndex, int skip)
    {
      if (!ilist.IsEmptyOrNullNG())
        return FillInternalNG(ilist, value, startIndex, lastIndex, skip);

      return false;
    }

    /// <summary>
    /// An internal function for filling a range of indexes in an <see cref="IList"/> with a value.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to check.</param>
    /// <param name="value">The value to put into the <paramref name="ilist"/>.</param>
    /// <param name="startIndex">The starting index to fill, inclusive.</param>
    /// <param name="lastIndex">The ending index to fill, exclusive.</param>
    /// <param name="skip">The amount of elements to skip before filling. It must be greater than 0.</param>
    /// <returns>Returns if the fill was successful.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool FillInternalNG(this IList ilist, object value, int startIndex, int lastIndex, int skip)
    {
      // Return false if the indexes are invalid or the skip is less than 0.
      if (!ilist.IsValidIndexNG(startIndex) || !ilist.IsValidIndexNG(lastIndex - 1) || skip <= 0)
        return false;

      // For the range of values [startIndex, lastIndex), replace the value within.
      for (int i = startIndex; i < lastIndex; i += skip)
        ilist[i] = value;

      return true; // A replacement was made.
    }

    /// <summary>
    /// An extension function which will only add an element if it is completely unique to the
    /// <see cref="IList"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the IList.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to add the element to.</param>
    /// <param name="element">The element to add to the <see cref="IList"/>.</param>
    /// <returns></returns>
    public static bool AddUnique<T>(this IList<T> ilist, T element)
    {
      // If the ilist does not contain the element, add it to the collection.
      if (!ilist.Contains(element))
      {
        ilist.Add(element);
        return true;
      }

      return false; // Return false if nothing is added.
    }

    /// <summary>
    /// An extension function which will only add an element if it is completely unique to the
    /// <see cref="IList"/>.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to add the element to.</param>
    /// <param name="element">The element to add to the <see cref="IList"/>.</param>
    /// <returns>Returns if the element was successfully added.</returns>
    public static bool AddUniqueNG(this IList ilist, object element)
    {
      // If the ilist does not contain the element, add it to the collection.
      if (!ilist.Contains(element))
      {
        ilist.Add(element);
        return true;
      }

      return false; // Return false if nothing is added.
    }

    /// <summary>
    /// An extension function which will only set an element if it is completely unique to the
    /// <see cref="IList"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <see cref="IList"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to set the element into.</param>
    /// <param name="element">The element to set into the <see cref="IList"/>.</param>
    /// <param name="index">The index to set the element into.</param>
    /// <returns>Returns if the element was successfully set.</returns>
    public static bool SetUnique<T>(this IList<T> ilist, T element, int index)
    {
      // If the ilist does not contain the element, set it to the collection.
      if (ilist.IsValidIndex(index) && !ilist.Contains(element))
      {
        ilist[index] = element;
        return true;
      }

      return false; // Return false if nothing is added.
    }

    /// <summary>
    /// An extension function which will only set an element if it is completely unique to the
    /// <see cref="IList"/>.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to set the element into.</param>
    /// <param name="element">The element to set into the <see cref="IList"/>.</param>
    /// <param name="index">The index to set the element into.</param>
    /// <returns>Returns if the element was successfully set.</returns>
    public static bool SetUniqueNG(this IList ilist, object element, int index)
    {
      // If the ilist does not contain the element, set it to the collection.
      if (ilist.IsValidIndexNG(index) && !ilist.Contains(element))
      {
        ilist[index] = element;
        return true;
      }

      return false; // Return false if nothing is added.
    }

    /// <summary>
    /// An extension function which will only insert an element if it is completely unique to the
    /// <see cref="IList"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <see cref="IList"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to insert the element to.</param>
    /// <param name="element">The element to insert into the <see cref="IList"/>.</param>
    /// <param name="index">The index to insert the element at.</param>
    /// <returns>Returns if the element was successfully set.</returns>
    public static bool InsertUnique<T>(this IList<T> ilist, T element, int index)
    {
      // If the ilist does not contain the element, set it to the collection.
      if (ilist.IsValidIndex(index) && !ilist.Contains(element))
      {
        ilist.Insert(index, element);
        return true;
      }

      return false; // Return false if nothing is added.
    }

    /// <summary>
    /// An extension function which will only set an element if it is completely unique to the
    /// <see cref="IList"/>.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to insert the element to.</param>
    /// <param name="element">The element to insert into the <see cref="IList"/>.</param>
    /// <param name="index">The index to insert the element at.</param>
    /// <returns>Returns if the element was successfully set.</returns>
    public static bool InsertUniqueNG(this IList ilist, object element, int index)
    {
      // If the ilist does not contain the element, set it to the collection.
      if (ilist.IsValidIndexNG(index) && !ilist.Contains(element))
      {
        ilist.Insert(index, element);
        return true;
      }

      return false; // Return false if nothing is added.
    }

    /// <summary>
    /// A function which prints out the contents of an <see cref="IList"/> to a string.
    /// This can be very useful for debugging purposes.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="ilist"/>.</typeparam>
    /// <param name="ilist">The <see cref="IList"/> to print the elements of.</param>
    /// <param name="separator">The separation string between each element.</param>
    /// <param name="prefix">The prefix before each element.</param>
    /// <param name="suffix">The suffix after each element.</param>
    /// <returns>Returns the string printout of <paramref name="ilist"/>'s contents.</returns>
    public static string Print<T>(this IList<T> ilist, string separator = ", ", string prefix = "[", string suffix = "]")
    {
      // If the IList is invalid, return an empty string.
      if (ilist.IsEmptyOrNull())
        return string.Empty;

      StringBuilder printout = new StringBuilder(); // Create a new StringBuilder.
      int count = ilist.Count; // Get the number of elements.

      // Append every element to the printout.
      for (int i = 0; i < count; i++)
        printout.Append(prefix).Append(ilist[i].ToString()).Append(suffix).Append(separator);

      // Remove the final separator.
      printout.Remove(printout.Length - separator.Length, separator.Length);

      return printout.ToString(); // Return the printout.
    }

    /// <summary>
    /// A function which prints out the contents of an <see cref="IList"/> to a string.
    /// This can be very useful for debugging purposes.
    /// </summary>
    /// <param name="ilist">The <see cref="IList"/> to print the elements of.</param>
    /// <param name="separator">The separation string between each element.</param>
    /// <param name="prefix">The prefix before each element.</param>
    /// <param name="suffix">The suffix after each element.</param>
    /// <returns>Returns the string printout of <paramref name="ilist"/>'s contents.</returns>
    public static string PrintNG(this IList ilist, string separator = ", ", string prefix = "[", string suffix = "]")
    {
      // If the IList is invalid, return an empty string.
      if (ilist.IsEmptyOrNullNG())
        return string.Empty;

      StringBuilder printout = new StringBuilder(); // Create a new StringBuilder.
      int count = ilist.Count; // Get the number of elements.

      // Append every element to the printout.
      for (int i = 0; i < count; i++)
        printout.Append(prefix).Append(ilist[i].ToString()).Append(suffix).Append(separator);

      // Remove the final separator.
      printout.Remove(printout.Length - separator.Length, separator.Length);

      return printout.ToString(); // Return the printout.
    }
  }
  /************************************************************************************************/
}