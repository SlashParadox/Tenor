/**************************************************************************************************/
/*!
\file   Sort.cs
\author Craig Williams
\par    Last Updated
        2021-06-08
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for tool functions related to sorting and comparing values in collections.

\par Bug List

\par References
  - https://www.geeksforgeeks.org/heap-sort/
  - https://www.geeksforgeeks.org/iterative-quick-sort/
*/
/**************************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SlashParadox.Tenor.Tools
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
    /// A function which uses a true Bogo Sort algorithm to sort some <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to sort.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <remarks>This algorithm is for educational purposes only. Never use this in 
    /// production code.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void TrueBogoSort<T>(IList<T> il, Comparison<T> compare)
    {
      if (ParametersAreValid(il, compare))
        TrueBogoSortInternal(il, compare, 0, il.Count);
    }

    /// <summary>
    /// A function which uses a true Bogo Sort algorithm to sort some <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to sort.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to sort from.</param>
    /// <param name="lastIndex">The exclusive last index to sort to.</param>
    /// <remarks>This algorithm is for educational purposes only. Never use this in 
    /// production code.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void TrueBogoSort<T>(IList<T> il, Comparison<T> compare, int startIndex,
                                       int lastIndex)
    {
      if (ParametersAreValid(il, compare, startIndex, lastIndex))
        TrueBogoSortInternal(il, compare, startIndex, lastIndex);
    }

    /// <summary>
    /// A function which uses a Bubble Sort algorithm to sort some <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to sort.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void BubbleSort<T>(IList<T> il, Comparison<T> compare)
    {
      if (ParametersAreValid(il, compare))
        BubbleSortInternal(il, compare, 0, il.Count);
    }

    /// <summary>
    /// A function which uses a Bubble Sort algorithm to sort some <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to sort.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to sort from.</param>
    /// <param name="lastIndex">The exclusive last index to sort to.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void BubbleSort<T>(IList<T> il, Comparison<T> compare, int startIndex,
                                     int lastIndex)
    {
      if (ParametersAreValid(il, compare, startIndex, lastIndex))
        BubbleSortInternal(il, compare, startIndex, lastIndex);
    }

    /// <summary>
    /// A function which uses a Heap Sort algorithm to sort some <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to sort.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void HeapSort<T>(IList<T> il, Comparison<T> compare)
    {
      if (ParametersAreValid(il, compare))
        HeapSortInternal(il, compare, 0, il.Count);
    }

    /// <summary>
    /// A function which uses a Heap Sort algorithm to sort some <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to sort.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to sort from.</param>
    /// <param name="lastIndex">The exclusive last index to sort to.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void HeapSort<T>(IList<T> il, Comparison<T> compare, int startIndex,
                                   int lastIndex)
    {
      if (ParametersAreValid(il, compare, startIndex, lastIndex))
        HeapSortInternal(il, compare, startIndex, lastIndex);
    }

    /// <summary>
    /// A function which uses a Selection Sort algorithm to sort some <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to sort.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SelectionSort<T>(IList<T> il, Comparison<T> compare)
    {
      if (ParametersAreValid(il, compare))
        SelectionSortInternal(il, compare, 0, il.Count);
    }

    /// <summary>
    /// A function which uses a Selection Sort algorithm to sort some <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to sort.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to sort from.</param>
    /// <param name="lastIndex">The exclusive last index to sort to.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SelectionSort<T>(IList<T> il, Comparison<T> compare, int startIndex,
                                        int lastIndex)
    {
      if (ParametersAreValid(il, compare, startIndex, lastIndex))
        SelectionSortInternal(il, compare, startIndex, lastIndex);
    }

    /// <summary>
    /// A function which uses a Quick Sort algorithm to sort some <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to sort.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void QuickSort<T>(IList<T> il, Comparison<T> compare)
    {
      if (ParametersAreValid(il, compare))
        QuickSortInternal(il, compare, 0, il.Count);
    }

    /// <summary>
    /// A function which uses a Quick Sort algorithm to sort some <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to sort.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to sort from.</param>
    /// <param name="lastIndex">The exclusive last index to sort to.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void QuickSort<T>(IList<T> il, Comparison<T> compare, int startIndex,
                                    int lastIndex)
    {
      if (ParametersAreValid(il, compare, startIndex, lastIndex))
        QuickSortInternal(il, compare, startIndex, lastIndex);
    }

    /// <summary>
    /// A function which uses an Insertion Sort algorithm to sort some <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to sort.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void InsertionSort<T>(IList<T> il, Comparison<T> compare)
    {
      if (ParametersAreValid(il, compare))
        InsertionSortInternal(il, compare, 0, il.Count);
    }

    /// <summary>
    /// A function which uses an Insertion Sort algorithm to sort some <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to sort.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to sort from.</param>
    /// <param name="lastIndex">The exclusive last index to sort to.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void InsertionSort<T>(IList<T> il, Comparison<T> compare, int startIndex,
                                        int lastIndex)
    {
      if (ParametersAreValid(il, compare, startIndex, lastIndex))
        InsertionSortInternal(il, compare, startIndex, lastIndex);
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
    /// An internal function which uses a true Bogo Sort algorithm to sort some
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
    private static void TrueBogoSortInternal<T>(IList<T> il, Comparison<T> compare, int startIndex,
                                               int lastIndex)
    {
      while (!IsSortedLinearInternal(il, compare, startIndex, lastIndex))
        il.Shuffle(startIndex, lastIndex);
    }

    /// <summary>
    /// An internal function which uses a Bubble Sort algorithm to sort some <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to sort.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to sort from.</param>
    /// <param name="lastIndex">The exclusive last index to sort to.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void BubbleSortInternal<T>(IList<T> il, Comparison<T> compare, int startIndex,
                                              int lastIndex)
    {
      // Create two temporary values for holding onto data.
      T tempA;
      T tempB;

      // Swap for the entire array section.
      for (int i = startIndex; i < lastIndex - 1; i++)
      {
        bool hasSwapped = false; // A toggle for if any swaps were done.
        tempA = il[startIndex]; // Get the first comparison value.
        int innerLastIndex = lastIndex - (i - startIndex);

        // Swap for the remainder of the array section.
        for (int j = startIndex + 1; j < innerLastIndex; j++)
        {
          tempB = il[j]; // Get the second comparison value.

          // If the values are not sorted, swap adjacent values.
          if (compare(tempA, tempB) > 0)
          {
            il.SwapValues(j, j - 1);
            hasSwapped = true;
          }
          else
            tempA = tempB; // Otherwise, replace the first comparison to make a new pivot.
        }

        // If nothing was swapped, the array is in order, and the loop can be stopped.
        if (!hasSwapped)
          break;
      }
    }

    /// <summary>
    /// An internal function which uses a Heap Sort algorithm to sort some <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to sort.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to sort from.</param>
    /// <param name="lastIndex">The exclusive last index to sort to.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void HeapSortInternal<T>(IList<T> il, Comparison<T> compare, int startIndex,
                                            int lastIndex)
    {
      int lastSortedIndex = lastIndex - startIndex; // Get the last valid index to sort.
      int heapStart = startIndex + (lastSortedIndex - 2) / 2; // Get the starting heap index.
      
      // Perform the loop backwards to begin creating heaps.
      for (int i = heapStart; i >= startIndex; i--)
        HeapSortBuildHeap(il, compare, startIndex, lastIndex, i);

      // Swap values within the array section.
      for (int i = 1; i < lastSortedIndex; i++)
      {
        int highPartition = lastIndex - i; // The highest index to go to in the heap.
        il.SwapValues(startIndex, highPartition); // Swap the values in the partition.
        HeapSortBuildHeap(il, compare, startIndex, highPartition, startIndex); // Update the heap.
      }
    }

    /// <summary>
    /// An internal function for building a heap for use with the Heap Sort Algorithm.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to sort.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to sort from.</param>
    /// <param name="lastIndex">The exclusive last index to sort to.</param>
    /// <param name="pivot">The pivot index to create the partition around.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void HeapSortBuildHeap<T>(IList<T> il, Comparison<T> compare, int startIndex,
                                             int lastIndex, int pivot)
    {
      T pivotItem = il[pivot]; // Get the item that represents the pivot.

      // Continue until the heap is built.
      while (true)
      {
        // Create two pivot indexes around the given pivot.
        int pivotLow = startIndex + (pivot - startIndex) * 2 + 1;
        int pivotHigh = pivotLow + 1;

        // If past the max, break immediately.
        if (pivotLow >= lastIndex)
          break;

        T itemLeft = il[pivotLow]; // Get the first compared value.

        // If the higher pivot is valid, compare with a second value.
        if (pivotHigh < lastIndex)
        {
          T itemRight = il[pivotHigh]; // Get the second compared value.

          // If the values are in order, update the low item and pivot.
          if (compare(itemLeft, itemRight) <= 0)
          {
            pivotLow = pivotHigh;
            itemLeft = itemRight;
          }
        }

        // If the pivot is in order with the left, lower item, then this can break.
        if (compare(itemLeft, pivotItem) <= 0)
          break;

        // Otherwise, swap the values, and update the main pivot.
        il.SwapValues(pivot, pivotLow);
        pivot = pivotLow;
      }
    }

    /// <summary>
    /// An internal function which uses a Selection Sort algorithm to sort some
    /// <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to sort.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to sort from.</param>
    /// <param name="lastIndex">The exclusive last index to sort to.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void SelectionSortInternal<T>(IList<T> il, Comparison<T> compare, int startIndex,
                                                 int lastIndex)
    {
      // Loop through the section of the ilist to be sorted.
      for (int i = startIndex; i < lastIndex - 1; i++)
      {
        int minIndex = i; // The index containing the smallest value.

        // For the remaining indexes, continuously compare to get the smallest value.
        for (int j = i; j < lastIndex; j++)
        {
          if (compare(il[minIndex], il[j]) > 0)
            minIndex = j;
        }

        il.SwapValues(minIndex, i); // Swap the minimum index with the current index.
      }
    }

    /// <summary>
    /// An internal function which uses a Quick Sort algorithm to sort some <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to sort.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to sort from.</param>
    /// <param name="lastIndex">The exclusive last index to sort to.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void QuickSortInternal<T>(IList<T> il, Comparison<T> compare, int startIndex,
                                             int lastIndex)
    {
      int[] stack = new int[lastIndex - startIndex]; // Create a stack of the swaps required.
      int stackTopIndex = -1; // The top index of the stack.
      int stackLow = startIndex; // The low value of the stack.
      int stackHigh = lastIndex - 1; // The high value of the stack.

      // Initialize the stack by pushing on the low and high values.
      stack[++stackTopIndex] = stackLow;
      stack[++stackTopIndex] = stackHigh;

      // Continue removing from the stack until all swaps are finished.
      while (stackTopIndex >= 0)
      {
        // Remove the current high and low values from the stack.
        stackHigh = stack[stackTopIndex--];
        stackLow = stack[stackTopIndex--];

        // Find a pivot value at random and set it to its sorted position.
        int pivot = QuickSortGetPivot(il, compare, stackLow, stackHigh);

        // If there are elements to the left of the pivot, add them to the stack.
        if (pivot - 1 > stackLow)
        {
          stack[++stackTopIndex] = stackLow;
          stack[++stackTopIndex] = pivot - 1;
        }

        // If there are elements to the right of the pivot, add them to the stack.
        if (pivot + 1 < stackHigh)
        {
          stack[++stackTopIndex] = pivot + 1;
          stack[++stackTopIndex] = stackHigh;
        }
      }
    }

    /// <summary>
    /// An internal function for finding a pivot position when using the Quick Sort Algorithm.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to sort.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to sort from.</param>
    /// <param name="lastIndex">The exclusive last index to sort to.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int QuickSortGetPivot<T>(IList<T> il, Comparison<T> compare, int startIndex,
                                            int lastIndex)
    {
      // Generate a random pivot for better accuracy and speed.
      int randomPivot = Randomization.GetRandomIntII(RandomGenerators.RejectionRandom, startIndex, lastIndex);
      il.SwapValues(randomPivot, lastIndex);
      T pivotItem = il[lastIndex];

      int pivotIndex = startIndex - 1; // Get a starting index.

      // Loop through the entire ilist section.
      for (int i = startIndex; i < lastIndex; i++)
      {
        // If the pivot is not in order, swap the value and increment the pivot.
        if (compare(pivotItem, il[i]) > 0)
        {
          pivotIndex++;
          il.SwapValues(pivotIndex, i);
        }
      }

      // Make one final swap with the highest index, before returning.
      pivotIndex++;
      il.SwapValues(pivotIndex, lastIndex);
      return pivotIndex;
    }

    /// <summary>
    /// An internal function which uses an Insertion Sort algorithm to sort some
    /// <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type stored in the <paramref name="il"/>.</typeparam>
    /// <param name="il">The <see cref="IList{T}"/> to sort.</param>
    /// <param name="compare">The <see cref="Comparison{T}"/> function to use.</param>
    /// <param name="startIndex">The inclusive starting index to sort from.</param>
    /// <param name="lastIndex">The exclusive last index to sort to.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void InsertionSortInternal<T>(IList<T> il, Comparison<T> compare, int startIndex,
                                                 int lastIndex)
    {
      for (int i = startIndex + 1; i < lastIndex; i++)
      {
        T currentKey = il[i]; // Get a key item to compare everything to.
        int j = i - 1; // Make an indexer for moving elements ahead.

        for (; j >= startIndex; j--)
        {
          // If the current elements are in order with the key, break early.
          if (compare(il[j], currentKey) < 0)
            break;

          il[j + 1] = il[j]; // Shift elements one ahead of their current position.
        }

        il[j + 1] = currentKey; // Place the key back into the ilist, regardless of the break.
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
      return ParametersAreValid(il, compare)
             && Maths.InRangeIE(startIndex, 0, lastIndex - 1)
             && Maths.InRangeII(lastIndex, startIndex, il.Count);
    }
  }
  /************************************************************************************************/
}