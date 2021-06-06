/**************************************************************************************************/
/*!
\file   Test_Sort.cs
\author Craig Williams
\par    Last Updated
        2021-06-05
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A unit test file for the Sort tools.

\par Bug List

\par References
*/
/**************************************************************************************************/

using CodeParadox.Tenor.Tools;
using System.Collections.Generic;
using NUnit.Framework;

namespace CodeParadox.Tenor.Tests.Runtime.EditMode
{
  /************************************************************************************************/
  /// <summary>
  /// A test class for <see cref="Tenor.Tools.Sort"/>, for checking sorting functions.
  /// </summary>
  public class Test_Sort
  {
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
    /// A test for <see cref="Sort.BogoSort{T}(IList{T}, System.Comparison{T}, int, int)"/>.
    /// </summary>
    /// <param name="size">The size of the array.</param>
    /// <param name="min">The inclusive starting index to sort from.</param>
    /// <param name="max">The exclusive last index to sort to.</param>
    [Test(TestOf = typeof(Sort))] [Timeout(20000)]
    public void BogoSort_Partial_ReturnsSuccess([Random(10, 15, 5)] int size,
                                                [Random(1, 8, 5)] int min,
                                                [Random(8, 10, 5)] int max)
    {
      int[] array = Generate.RandomNumberArray(size, 0, size); // Make a random array.
      Sort.BogoSort(array, Sort.CompareMinToMax, min, max); // Sort the array.
      // Assert the array is sorted.
      Assert.IsTrue(array.IsSortedLinear(Sort.CompareMinToMax, min, max));
    }
  }
  /************************************************************************************************/
}