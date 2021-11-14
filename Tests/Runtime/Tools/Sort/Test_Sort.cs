/**************************************************************************************************/
/*!
\file   Test_Sort.cs
\author Craig Williams
\par    Last Updated
        2021-06-08
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A unit test file for the Sort tools.

\par Bug List

\par References
*/
/**************************************************************************************************/

using SlashParadox.Tenor.Tools;
using System.Collections.Generic;
using NUnit.Framework;
using System;
using System.Runtime.CompilerServices;

namespace SlashParadox.Tenor.Tests.Runtime.EditMode
{
  /************************************************************************************************/
  /// <summary>
  /// A test class for <see cref="Tenor.Tools.Sort"/>, for checking sorting functions.
  /// </summary>
  public class Test_Sort
  {
    /// <summary>The <see cref="Comparison{T}"/> function used in these tests.</summary>
    private readonly Comparison<int> compareTest = Sort.CompareMinToMax;

    /// <summary>
    /// A test for <see cref="Sort.IsSortedLinear{T}(IList{T}, System.Comparison{T})"/>
    /// and <see cref="Sort.IsSortedLinear{T}(IList{T}, System.Comparison{T}, int, int)"/>.
    /// </summary>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    [Test(TestOf = typeof(Sort))]
    public void IsSorted_Linear_ReturnsSuccess([Random(10, 20, 5)] int min,
                                               [Random(60, 80, 5)] int max)
    {
      int[] array = Generate.NumberArray(min, max); // Make a number array.
      int[] random = Generate.RandomNumberArray(max - min + 1, min, max); // Make a random array.

      // Assert that the sorted array is sorted.
      Assert.IsTrue(array.IsSortedLinear(Sort.CompareMinToMax));
      Assert.IsTrue(array.IsSortedLinear(Sort.CompareMinToMax, 0, array.Length));

      // As a fail safe, make sure the random array did not get made in order.
      bool areNotSame = false;
      for (int i = 0; i < array.Length; i++)
      {
        if (array[i] != random[i])
        {
          areNotSame = true;
          break;
        }
      }

      // If not the same, the random should not be sorted.
      if (areNotSame)
      {
        Assert.IsFalse(random.IsSortedLinear(Sort.CompareMinToMax));
        Assert.IsFalse(random.IsSortedLinear(Sort.CompareMinToMax, 0, random.Length));
      }
    }

    /// <summary>
    /// A test for <see cref="Sort.IsSortedCocktail{T}(IList{T}, System.Comparison{T})"/>
    /// and <see cref="Sort.IsSortedCocktail{T}(IList{T}, System.Comparison{T}, int, int)"/>.
    /// </summary>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    [Test(TestOf = typeof(Sort))]
    public void IsSorted_Cocktail_ReturnsSuccess([Random(10, 20, 5)] int min,
                                                 [Random(60, 80, 5)] int max)
    {
      int[] array = Generate.NumberArray(min, max); // Make a number array.
      int[] random = Generate.RandomNumberArray(max - min + 1, min, max); // Make a random array.

      // Assert that the sorted array is sorted.
      Assert.IsTrue(array.IsSortedCocktail(Sort.CompareMinToMax));
      Assert.IsTrue(array.IsSortedCocktail(Sort.CompareMinToMax, 0, array.Length));

      // As a fail safe, make sure the random array did not get made in order.
      bool areNotSame = false;
      for (int i = 0; i < array.Length; i++)
      {
        if (array[i] != random[i])
        {
          areNotSame = true;
          break;
        }
      }

      // If not the same, the random should not be sorted.
      if (areNotSame)
      {
        Assert.IsFalse(random.IsSortedCocktail(Sort.CompareMinToMax));
        Assert.IsFalse(random.IsSortedCocktail(Sort.CompareMinToMax, 0, random.Length));
      }
    }

    /// <summary>
    /// A test for <see cref="Sort.IsSortedDivided{T}(IList{T}, System.Comparison{T})"/>
    /// and <see cref="Sort.IsSortedDivided{T}(IList{T}, System.Comparison{T}, int, int)"/>.
    /// </summary>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    [Test(TestOf = typeof(Sort))]
    public void IsSorted_Divided_ReturnsSuccess([Random(10, 20, 5)] int min,
                                                [Random(60, 80, 5)] int max)
    {
      int[] array = Generate.NumberArray(min, max); // Make a number array.
      int[] random = Generate.RandomNumberArray(max - min + 1, min, max); // Make a random array.

      // Assert that the sorted array is sorted.
      Assert.IsTrue(array.IsSortedDivided(Sort.CompareMinToMax));
      Assert.IsTrue(array.IsSortedDivided(Sort.CompareMinToMax, 0, array.Length));

      // As a fail safe, make sure the random array did not get made in order.
      bool areNotSame = false;
      for (int i = 0; i < array.Length; i++)
      {
        if (array[i] != random[i])
        {
          areNotSame = true;
          break;
        }
      }

      // If not the same, the random should not be sorted.
      if (areNotSame)
      {
        Assert.IsFalse(random.IsSortedDivided(Sort.CompareMinToMax));
        Assert.IsFalse(random.IsSortedDivided(Sort.CompareMinToMax, 0, random.Length));
      }
    }

    /// <summary>
    /// A test for <see cref="Sort.BubbleSort{T}(IList{T}, System.Comparison{T})"/>.
    /// </summary>
    /// <param name="size">The size of the array.</param>
    [Test(TestOf = typeof(Sort))] [Timeout(20000)]
    public void BubbleSort_Full_ReturnsSuccess([Random(30, 100, 5)] int size)
    {
      HandleSort(size, (a, c, s, l) => Sort.BubbleSort(a, c));
    }

    /// <summary>
    /// A test for <see cref="Sort.BubbleSort{T}(IList{T}, System.Comparison{T}, int, int)"/>.
    /// </summary>
    /// <param name="size">The size of the array.</param>
    /// <param name="startIndex">The inclusive starting index to sort from.</param>
    /// <param name="lastIndex">The exclusive last index to sort to.</param>
    [Test(TestOf = typeof(Sort))] [Timeout(20000)]
    public void BubbleSort_Partial_ReturnsSuccess([Random(80, 100, 5)] int size,
                                                  [Random(0, 40, 5)] int startIndex,
                                                  [Random(50, 80, 5)] int lastIndex)
    {
      HandleSort(size, startIndex, lastIndex, (a, c, s, l) => Sort.BubbleSort(a, c, s, l));
    }

    /// <summary>
    /// A test for <see cref="Sort.HeapSort{T}(IList{T}, System.Comparison{T})"/>.
    /// </summary>
    /// <param name="size">The size of the array.</param>
    [Test(TestOf = typeof(Sort))] [Timeout(20000)]
    public void HeapSort_Full_ReturnsSuccess([Random(30, 100, 5)] int size)
    {
      HandleSort(size, (a, c, s, l) => Sort.HeapSort(a, c));
    }

    /// <summary>
    /// A test for <see cref="Sort.HeapSort{T}(IList{T}, System.Comparison{T}, int, int)"/>.
    /// </summary>
    /// <param name="size">The size of the array.</param>
    /// <param name="startIndex">The inclusive starting index to sort from.</param>
    /// <param name="lastIndex">The exclusive last index to sort to.</param>
    [Test(TestOf = typeof(Sort))] [Timeout(20000)]
    public void HeapSort_Partial_ReturnsSuccess([Random(80, 100, 5)] int size,
                                                  [Random(0, 40, 5)] int startIndex,
                                                  [Random(50, 80, 5)] int lastIndex)
    {
      HandleSort(size, startIndex, lastIndex, (a, c, s, l) => Sort.HeapSort(a, c, s, l));
    }

    /// <summary>
    /// A test for <see cref="Sort.SelectionSort{T}(IList{T}, System.Comparison{T})"/>.
    /// </summary>
    /// <param name="size">The size of the array.</param>
    [Test(TestOf = typeof(Sort))] [Timeout(20000)]
    public void SelectionSort_Full_ReturnsSuccess([Random(30, 100, 5)] int size)
    {
      HandleSort(size, (a, c, s, l) => Sort.SelectionSort(a, c));
    }

    /// <summary>
    /// A test for <see cref="Sort.SelectionSort{T}(IList{T}, System.Comparison{T}, int, int)"/>.
    /// </summary>
    /// <param name="size">The size of the array.</param>
    /// <param name="startIndex">The inclusive starting index to sort from.</param>
    /// <param name="lastIndex">The exclusive last index to sort to.</param>
    [Test(TestOf = typeof(Sort))] [Timeout(20000)]
    public void SelectionSort_Partial_ReturnsSuccess([Random(80, 100, 5)] int size,
                                                     [Random(0, 40, 5)] int startIndex,
                                                     [Random(50, 80, 5)] int lastIndex)
    {
      HandleSort(size, startIndex, lastIndex, (a, c, s, l) => Sort.SelectionSort(a, c, s, l));
    }

    /// <summary>
    /// A test for <see cref="Sort.QuickSort{T}(IList{T}, System.Comparison{T})"/>.
    /// </summary>
    /// <param name="size">The size of the array.</param>
    [Test(TestOf = typeof(Sort))] [Timeout(20000)]
    public void QuickSort_Full_ReturnsSuccess([Random(30, 100, 5)] int size)
    {
      HandleSort(size, (a, c, s, l) => Sort.QuickSort(a, c));
    }

    /// <summary>
    /// A test for <see cref="Sort.QuickSort{T}(IList{T}, System.Comparison{T}, int, int)"/>.
    /// </summary>
    /// <param name="size">The size of the array.</param>
    /// <param name="startIndex">The inclusive starting index to sort from.</param>
    /// <param name="lastIndex">The exclusive last index to sort to.</param>
    [Test(TestOf = typeof(Sort))] [Timeout(20000)]
    public void QuickSort_Partial_ReturnsSuccess([Random(80, 100, 5)] int size,
                                                 [Random(0, 40, 5)] int startIndex,
                                                 [Random(50, 80, 5)] int lastIndex)
    {
      HandleSort(size, startIndex, lastIndex, (a, c, s, l) => Sort.QuickSort(a, c, s, l));
    }

    /// <summary>
    /// A test for <see cref="Sort.InsertionSort{T}(IList{T}, System.Comparison{T})"/>.
    /// </summary>
    /// <param name="size">The size of the array.</param>
    [Test(TestOf = typeof(Sort))] [Timeout(20000)]
    public void InsertionSort_Full_ReturnsSuccess([Random(30, 100, 5)] int size)
    {
      HandleSort(size, (a, c, s, l) => Sort.InsertionSort(a, c));
    }

    /// <summary>
    /// A test for <see cref="Sort.InsertionSort{T}(IList{T}, System.Comparison{T}, int, int)"/>.
    /// </summary>
    /// <param name="size">The size of the array.</param>
    /// <param name="startIndex">The inclusive starting index to sort from.</param>
    /// <param name="lastIndex">The exclusive last index to sort to.</param>
    [Test(TestOf = typeof(Sort))] [Timeout(20000)]
    public void InsertionSort_Partial_ReturnsSuccess([Random(80, 100, 5)] int size,
                                                     [Random(0, 40, 5)] int startIndex,
                                                     [Random(50, 80, 5)] int lastIndex)
    {
      HandleSort(size, startIndex, lastIndex, (a, c, s, l) => Sort.InsertionSort(a, c, s, l));
    }

    /// <summary>
    /// A helper function for handling each sort test.
    /// </summary>
    /// <param name="size">The size of the array.</param>
    /// <param name="sortFunc">The function that calls the type of sort to test. This takes
    /// in the array <paramref name="size"/>, <see cref="compareTest"/> function,
    /// 0 as a start index, and <paramref name="size"/> as the last index.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void HandleSort(int size, Action<int[], Comparison<int>, int, int> sortFunc)
    {
      HandleSort(size, 0, size, sortFunc);
    }

    /// <summary>
    /// A helper function for handling each sort test.
    /// </summary>
    /// <param name="size">The size of the array.</param>
    /// <param name="startIndex">The inclusive starting index to sort from.</param>
    /// <param name="lastIndex">The exclusive last index to sort to.</param>
    /// <param name="sortFunc">The function that calls the type of sort to test. This takes
    /// in the array <paramref name="size"/>, <see cref="compareTest"/> function,
    /// <paramref name="startIndex"/>, and <paramref name="lastIndex"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void HandleSort(int size, int startIndex, int lastIndex,
                            Action<int[], Comparison<int>, int, int> sortFunc)
    {
      // Make a random number array, along with a copy of its values.
      int[] array = Generate.RandomNumberArray(size, 0, size);
      int[] copy = array.ShallowCopy();

      // Sort the array and assert that it was sorted.
      sortFunc(array, compareTest, startIndex, lastIndex);
      Assert.IsTrue(array.IsSortedLinear(compareTest, startIndex, lastIndex), array.Print());

      // Make sure the starting elements were not affected.
      for (int i = 0; i < startIndex; i++)
        Assert.AreEqual(copy[i], array[i], $"Original: {copy[i]}, Sorted: {array[i]}");

      // Make sure the ending elements were not affected.
      for (int i = lastIndex; i < array.Length; i++)
        Assert.AreEqual(copy[i], array[i], $"Original: {copy[i]}, Sorted: {array[i]}");
    }
  }
  /************************************************************************************************/
}