/**************************************************************************************************/
/*!
\file   Sort.cs
\author Craig Williams
\par    Last Updated
        2021-06-05
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for tool functions related to sorting and comparing values in collections.

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
  /// A collection of tools for sorting collections, along with useful <see cref="Comparison{T}"/>
  /// functions for use in multiple situations.
  /// </summary>
  public static partial class Sort
  {
    /// <summary>
    /// A function to check if some <see cref="IList{T}"/> is sorted, based on some
    /// <see cref="Comparison{T}"/> function. This uses a Linear Method.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <returns>Returns if the <paramref name="il"/> is sorted correctly.</returns>
    public static bool IsSortedLinear<T>(this IList<T> il, Comparison<T> compare)
    {
      // If the parameters are not valid, return false immediately.
      if (!ParametersAreValid(il, compare))
        return false;

      return IsSortedLinearInternal(il, compare, 0, il.Count);
    }

    /// <summary>
    /// A function to check if some <see cref="IList{T}"/> is sorted, based on some
    /// <see cref="Comparison{T}"/> function. This uses a Linear Method.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to check from.</param>
    /// <param name="lastIndex">The exclusive last index to check up to.</param>
    /// <returns>Returns if the <paramref name="il"/> is sorted correctly.</returns>
    public static bool IsSortedLinear<T>(this IList<T> il, Comparison<T> compare, int startIndex,
                                         int lastIndex)
    {
      // If the parameters are not valid, return false immediately.
      if (!ParametersAreValid(il, compare, startIndex, lastIndex))
        return false;

      return IsSortedLinearInternal(il, compare, startIndex, lastIndex);
    }

    /// <summary>
    /// A function to check if some <see cref="IList{T}"/> is sorted, based on some
    /// <see cref="Comparison{T}"/> function. This uses a Cocktail Method.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <returns>Returns if the <paramref name="il"/> is sorted correctly.</returns>
    public static bool IsSortedCocktail<T>(this IList<T> il, Comparison<T> compare)
    {
      // If the parameters are not valid, return false immediately.
      if (!ParametersAreValid(il, compare))
        return false;

      return IsSortedCocktailInternal(il, compare, 0, il.Count);
    }

    /// <summary>
    /// A function to check if some <see cref="IList{T}"/> is sorted, based on some
    /// <see cref="Comparison{T}"/> function. This uses a Cocktail Method.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to check from.</param>
    /// <param name="lastIndex">The exclusive last index to check up to.</param>
    /// <returns>Returns if the <paramref name="il"/> is sorted correctly.</returns>
    public static bool IsSortedCocktail<T>(this IList<T> il, Comparison<T> compare, int startIndex,
                                           int lastIndex)
    {
      // If the parameters are not valid, return false immediately.
      if (!ParametersAreValid(il, compare, startIndex, lastIndex))
        return false;

      return IsSortedCocktailInternal(il, compare, startIndex, lastIndex);
    }

    /// <summary>
    /// A function to check if some <see cref="IList{T}"/> is sorted, based on some
    /// <see cref="Comparison{T}"/> function. This uses a Divided Method.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <returns>Returns if the <paramref name="il"/> is sorted correctly.</returns>
    public static bool IsSortedDivided<T>(this IList<T> il, Comparison<T> compare)
    {
      // If the parameters are not valid, return false immediately.
      if (!ParametersAreValid(il, compare))
        return false;

      return IsSortedDividedInternal(il, compare, 0, il.Count);
    }

    /// <summary>
    /// A function to check if some <see cref="IList{T}"/> is sorted, based on some
    /// <see cref="Comparison{T}"/> function. This uses a Divided Method.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to check from.</param>
    /// <param name="lastIndex">The exclusive last index to check up to.</param>
    /// <returns>Returns if the <paramref name="il"/> is sorted correctly.</returns>
    public static bool IsSortedDivided<T>(this IList<T> il, Comparison<T> compare, int startIndex,
                                          int lastIndex)
    {
      // If the parameters are not valid, return false immediately.
      if (!ParametersAreValid(il, compare, startIndex, lastIndex))
        return false;

      return IsSortedDividedInternal(il, compare, startIndex, lastIndex);
    }

    /// <summary>
    /// A function which uses an improved Bogo Sort algorithm to sort some <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to sort.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <remarks>This algorithm is for educational purposes only. Never use this in 
    /// production code.</remarks>
    public static void BogoSort<T>(IList<T> il, Comparison<T> compare)
    {
      if (ParametersAreValid(il, compare))
        BogoSortInternal(il, compare, 0, il.Count);
    }

    /// <summary>
    /// A function which uses an improved Bogo Sort algorithm to sort some <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to sort.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to sort from.</param>
    /// <param name="lastIndex">The exclusive last index to sort to.</param>
    /// <remarks>This algorithm is for educational purposes only. Never use this in 
    /// production code.</remarks>
    public static void BogoSort<T>(IList<T> il, Comparison<T> compare, int startIndex,
                                            int lastIndex)
    {
      if (ParametersAreValid(il, compare, startIndex, lastIndex))
        BogoSortInternal(il, compare, startIndex, lastIndex);
    }

    /// <summary>
    /// An internal function to check if some <see cref="IList{T}"/> is sorted, based on some
    /// <see cref="Comparison{T}"/> function. This uses a Linear Method.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to check from.</param>
    /// <param name="lastIndex">The exclusive last index to check up to.</param>
    /// <returns>Returns if the <paramref name="il"/> is sorted correctly.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsSortedLinearInternal<T>(this IList<T> il, Comparison<T> compare,
                                                  int startIndex, int lastIndex)
    {
      // Iterate through the entire ilist. Start ahead by 1 to compare with a previous element.
      for (int i = startIndex + 1; i < lastIndex; i++)
      {
        // If the comparison is ever out of order (last > current), return false.
        if (compare(il[i - 1], il[i]) > 0)
          return false;
      }

      return true; // The ilist is sorted.
    }

    /// <summary>
    /// An internal function to check if some <see cref="IList{T}"/> is sorted, based on some
    /// <see cref="Comparison{T}"/> function. This uses a Cocktail Method.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to check from.</param>
    /// <param name="lastIndex">The exclusive last index to check up to.</param>
    /// <returns>Returns if the <paramref name="il"/> is sorted correctly.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsSortedCocktailInternal<T>(this IList<T> il, Comparison<T> compare,
                                                    int startIndex, int lastIndex)
    {
      // The numer of moves to make. This is half of the array's size.
      int moves = (int)System.Math.Ceiling((lastIndex - startIndex) / 2.0f);
      lastIndex--; // Decrement the last index to fit the number of moves in the loop.

      for (int i = 0; i < moves; i++)
      {
        // If the comparison on either side fails, return false.
        if (compare(il[startIndex], il[startIndex + 1]) > 0)
          return false;
        if (compare(il[lastIndex - 1], il[lastIndex]) > 0)
          return false;

        // Move the dual indexes closer to each other.
        startIndex++;
        lastIndex--;
      }

      return true; // The ilist is sorted.
    }

    /// <summary>
    /// An internal function to check if some <see cref="IList{T}"/> is sorted, based on some
    /// <see cref="Comparison{T}"/> function. This uses a Divide-And-Conquer Method.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to check from.</param>
    /// <param name="lastIndex">The exclusive last index to check up to.</param>
    /// <returns>Returns if the <paramref name="il"/> is sorted correctly.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsSortedDividedInternal<T>(this IList<T> il, Comparison<T> compare,
                                                   int startIndex, int lastIndex)
    {
      // Get two middle index points, as the ilist will be divided in two.
      int startMidIndex = lastIndex / 2;
      int endMidIndex = startMidIndex;

      // If the array is of even size, we need to compare the two middle indexes first.
      if (lastIndex % 2 == 0)
      {
        startMidIndex -= 1; // Decrement the low end to properly split.
        // Compare with the middle index.
        if (compare(il[startMidIndex], il[endMidIndex]) > 0)
          return false;
      }

      lastIndex--; // Decrement the end index by 1, as it is an exclusive input.

      // Return if both sides are sorted.
      if (IsSortedDividedPartition(il, compare, startIndex, startMidIndex))
        return IsSortedDividedPartition(il, compare, endMidIndex, lastIndex);

      return false; // The list was not sorted.
    }

    /// <summary>
    /// An internal function to check if some <see cref="IList{T}"/> is sorted, based on some
    /// <see cref="Comparison{T}"/> function. This uses a Divide-And-Conquer Method.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to check from.</param>
    /// <param name="lastIndex">The exclusive last index to check up to.</param>
    /// <returns>Returns if the <paramref name="il"/> is sorted correctly.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsSortedDividedPartition<T>(this IList<T> il, Comparison<T> compare,
                                                    int startIndex, int lastIndex)
    {
      // Continue wihle the indexes do not pass each other.
      while (startIndex < lastIndex)
      {
        // If the elements are out of order, return false.
        if (compare(il[startIndex], il[lastIndex]) > 0)
          return false;

        // Move the indexes closer to each other.
        startIndex++;
        lastIndex--;
      }

      return true; // This partition is sorted.
    }

    /// <summary>
    /// An internal function which uses an improved Bogo Sort algorithm to sort some
    /// <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to sort.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to sort from.</param>
    /// <param name="lastIndex">The exclusive last index to sort to.</param>
    /// <remarks>This algorithm is for educational purposes only. Never use this in 
    /// production code.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void BogoSortInternal<T>(IList<T> il, Comparison<T> compare, int startIndex,
                                            int lastIndex)
    {
      // Continue through the ilist until all items are sorted.
      while (startIndex < lastIndex)
      {
        T temp = il[startIndex]; // Get a temporary value to check order against.
        int position = startIndex; // Create a value for the current position in the iteration.

        // Check each position, up until something is found to be out of order.
        for (; position < lastIndex; position++)
        {
          if (compare(temp, il[position]) > 0)
            break;
        }

        // If the comparison so far is fine, increment where we start. Otherwise, shuffle.
        if (position == lastIndex)
          startIndex++;
        else
          il.Shuffle(startIndex, lastIndex);
      }
    }

    /// <summary>
    /// A helper function for checking if the typical parameters for a <see cref="Sort"/> function
    /// are valid.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <returns>Returns if all parameters are valid.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool ParametersAreValid<T>(IList<T> il, Comparison<T> compare)
    {
      return il.IsNotEmptyOrNull() && compare != null;
    }

    /// <summary>
    /// A helper function for checking if the typical parameters for a <see cref="Sort"/> function
    /// are valid.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to check.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to check from.</param>
    /// <param name="lastIndex">The exclusive last index to check up to.</param>
    /// <returns>Returns if all parameters are valid.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool ParametersAreValid<T>(IList<T> il, Comparison<T> compare, int startIndex,
                                              int lastIndex)
    {
      return il.IsNotEmptyOrNull() && compare != null && Maths.InRangeIE(startIndex, 0, lastIndex) &&
             Maths.InRangeII(lastIndex, startIndex, il.Count);
    }
  }
  /************************************************************************************************/
}