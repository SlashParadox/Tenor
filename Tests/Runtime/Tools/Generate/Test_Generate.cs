/**************************************************************************************************/
/*!
\file   Test_Generate.cs
\author Craig Williams
\par    Last Updated
        2021-06-05
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A unit test file for the Generate tools.

\par Bug List

\par References
*/
/**************************************************************************************************/

using SlashParadox.Tenor.Tools;
using System.Collections.Generic;
using NUnit.Framework;
using System.Security.Cryptography;

namespace SlashParadox.Tenor.Tests.Runtime.EditMode
{
  /************************************************************************************************/
  /// <summary>
  /// A test class for <see cref="Tenor.Tools.Generate"/>, for checking generation functions.
  /// </summary>
  public class Test_Generate
  {
    /// <summary>
    /// A test for <see cref="Generate.NumberArray(int, int)"/>.
    /// </summary>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    [Test(TestOf = typeof(Generate))]
    public void Number_IntArray_ReturnsSuccess([Random(10, 20, 5)] int min,
                                               [Random(60, 80, 5)] int max)
    {
      // Generate the number array.
      int size = max - min + 1;
      int[] array = new int[size];
      for (int i = 0; i < size; i++)
        array[i] = min + i;

      int[] test = Generate.NumberArray(min, max); // Generate the test with the function.

      // Make sure the ends are correct.
      Assert.AreEqual(array[0], min);
      Assert.AreEqual(array.LastElement(), max);

      // Assert all are equal.
      for (int i = 0; i < array.Length; i++)
        Assert.AreEqual(array[i], test[i]);
    }

    /// <summary>
    /// A test for <see cref="Generate.NumberList(int, int)"/>.
    /// </summary>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    [Test(TestOf = typeof(Generate))]
    public void Number_IntList_ReturnsSuccess([Random(10, 20, 5)] int min,
                                              [Random(60, 80, 5)] int max)
    {
      // Generate the number list.
      int size = max - min + 1;
      List<int> list = new List<int>(size);
      for (int i = 0; i < size; i++)
        list.Add(min + i);

      int[] test = Generate.NumberArray(min, max); // Generate the test with the function.

      // Make sure the ends are correct.
      Assert.AreEqual(list[0], min);
      Assert.AreEqual(list.LastElement(), max);

      // Assert all are equal.
      for (int i = 0; i < list.Count; i++)
        Assert.AreEqual(list[i], test[i]);
    }

    /// <summary>
    /// A test for <see cref="Generate.RandomNumberArray(int, int, int)"/>.
    /// </summary>
    /// <param name="size">The size of the array.</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    [Test(TestOf = typeof(Generate))]
    public void Random_IntArray_ReturnsSuccess([Random(10, 20, 5)] int size,
                                               [Random(10, 20, 5)] int min,
                                               [Random(60, 80, 5)] int max)
    {
      int[] test = Generate.RandomNumberArray(size, min, max); // Generate the test.

      // Make sure the size is correct.
      Assert.AreEqual(test.Length, size);

      // Assert all are within the bounds.
      for (int i = 0; i < test.Length; i++)
        Assert.IsTrue(Maths.InRangeII(test[i], min, max));
    }

    /// <summary>
    /// A test for <see cref="Generate.RandomNumberArray(int, int, int, RandomGenerators)"/>.
    /// </summary>
    /// <param name="size">The size of the array.</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    /// <param name="generator">The <see cref="Randomization"/> generator to use.</param>
    [Test(TestOf = typeof(Generate))]
    public void RandomGenerator_IntArray_ReturnsSuccess([Random(10, 20, 5)] int size,
                                                        [Random(10, 20, 5)] int min,
                                                        [Random(60, 80, 5)] int max,
                                                        [Random(0, 3, 5)] int generator)
    {
      // Generate the test.
      int[] test = Generate.RandomNumberArray(size, min, max, (RandomGenerators)generator);

      // Make sure the size is correct.
      Assert.AreEqual(test.Length, size);

      // Assert all are within the bounds.
      for (int i = 0; i < test.Length; i++)
        Assert.IsTrue(Maths.InRangeII(test[i], min, max));
    }

    /// <summary>
    /// A test for <see cref="Generate.RandomNumberArray(int, int, int, System.Random)"/>.
    /// </summary>
    /// <param name="size">The size of the array.</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    [Test(TestOf = typeof(Generate))]
    public void RandomStandard_IntArray_ReturnsSuccess([Random(10, 20, 5)] int size,
                                                       [Random(10, 20, 5)] int min,
                                                       [Random(60, 80, 5)] int max)
    {
      // Generate the test.
      System.Random random = new System.Random();
      int[] test = Generate.RandomNumberArray(size, min, max, random);

      // Make sure the size is correct.
      Assert.AreEqual(test.Length, size);

      // Assert all are within the bounds.
      for (int i = 0; i < test.Length; i++)
        Assert.IsTrue(Maths.InRangeII(test[i], min, max));
    }

    /// <summary>
    /// A test for <see cref="Generate.RandomNumberArray(int, int, int, RandomNumberGenerator)"/>.
    /// </summary>
    /// <param name="size">The size of the array.</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    [Test(TestOf = typeof(Generate))]
    public void RandomCrypto_IntArray_ReturnsSuccess([Random(10, 20, 5)] int size,
                                                     [Random(10, 20, 5)] int min,
                                                     [Random(60, 80, 5)] int max)
    {
      // Generate the test.
      RandomNumberGenerator random = new RNGCryptoServiceProvider();
      int[] test = Generate.RandomNumberArray(size, min, max, random);

      // Make sure the size is correct.
      Assert.AreEqual(test.Length, size);

      // Assert all are within the bounds.
      for (int i = 0; i < test.Length; i++)
        Assert.IsTrue(Maths.InRangeII(test[i], min, max));
    }

    /// <summary>
    /// A test for <see cref="Generate.RandomNumberList(int, int, int)"/>.
    /// </summary>
    /// <param name="size">The size of the array.</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    [Test(TestOf = typeof(Generate))]
    public void Random_IntList_ReturnsSuccess([Random(10, 20, 5)] int size,
                                              [Random(10, 20, 5)] int min,
                                              [Random(60, 80, 5)] int max)
    {
      List<int> test = Generate.RandomNumberList(size, min, max); // Generate the test.

      // Make sure the size is correct.
      Assert.AreEqual(test.Count, size);

      // Assert all are within the bounds.
      for (int i = 0; i < test.Count; i++)
        Assert.IsTrue(Maths.InRangeII(test[i], min, max));
    }

    /// <summary>
    /// A test for <see cref="Generate.RandomNumberList(int, int, int, RandomGenerators)"/>.
    /// </summary>
    /// <param name="size">The size of the array.</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    /// <param name="generator">The <see cref="Randomization"/> generator to use.</param>
    [Test(TestOf = typeof(Generate))]
    public void RandomGenerator_IntList_ReturnsSuccess([Random(10, 20, 5)] int size,
                                                       [Random(10, 20, 5)] int min,
                                                       [Random(60, 80, 5)] int max,
                                                       [Random(0, 3, 5)] int generator)
    {
      // Generate the test.
      List<int> test = Generate.RandomNumberList(size, min, max, (RandomGenerators)generator);

      // Make sure the size is correct.
      Assert.AreEqual(test.Count, size);

      // Assert all are within the bounds.
      for (int i = 0; i < test.Count; i++)
        Assert.IsTrue(Maths.InRangeII(test[i], min, max));
    }

    /// <summary>
    /// A test for <see cref="Generate.RandomNumberList(int, int, int, System.Random)"/>.
    /// </summary>
    /// <param name="size">The size of the array.</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    [Test(TestOf = typeof(Generate))]
    public void RandomStandard_IntList_ReturnsSuccess([Random(10, 20, 5)] int size,
                                                      [Random(10, 20, 5)] int min,
                                                      [Random(60, 80, 5)] int max)
    {
      // Generate the test.
      System.Random random = new System.Random();
      List<int> test = Generate.RandomNumberList(size, min, max, random);

      // Make sure the size is correct.
      Assert.AreEqual(test.Count, size);

      // Assert all are within the bounds.
      for (int i = 0; i < test.Count; i++)
        Assert.IsTrue(Maths.InRangeII(test[i], min, max));
    }

    /// <summary>
    /// A test for <see cref="Generate.RandomNumberList(int, int, int, RandomNumberGenerator)"/>.
    /// </summary>
    /// <param name="size">The size of the array.</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    [Test(TestOf = typeof(Generate))]
    public void RandomCrypto_IntList_ReturnsSuccess([Random(10, 20, 5)] int size,
                                                    [Random(10, 20, 5)] int min,
                                                    [Random(60, 80, 5)] int max)
    {
      // Generate the test.
      RandomNumberGenerator random = new RNGCryptoServiceProvider();
      List<int> test = Generate.RandomNumberList(size, min, max, random);

      // Make sure the size is correct.
      Assert.AreEqual(test.Count, size);

      // Assert all are within the bounds.
      for (int i = 0; i < test.Count; i++)
        Assert.IsTrue(Maths.InRangeII(test[i], min, max));
    }
  }
  /************************************************************************************************/
}