/**************************************************************************************************/
/*!
\file   Generate.cs
\author Craig Williams
\par    Last Updated
        2021-06-05
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for tool functions related to generating some collection or value, based on data.

\par Bug List

\par References
*/
/**************************************************************************************************/

using SlashParadox.Tenor.Exceptions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace SlashParadox.Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// A collection of functions to generate some sort of collection or value, based on data.
  /// </summary>
  public static partial class Generate
  {
    /// <summary>
    /// A function for generating an <see cref="int"/> array from <paramref name="min"/> to
    /// <paramref name="max"/>.
    /// </summary>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    /// <returns>Returns an <see cref="int"/> array from <paramref name="min"/> to
    /// <paramref name="max"/>.</returns>
    public static int[] NumberArray(int min, int max)
    {
      // Throw an error if the min and max are not correct.
      if (min > max)
        throw new MinMaxException<int>(min, max, true);

      int size = max - min + 1; // Calculate the final size.
      int[] array = new int[size]; // Create the array, properly sized.

      // Fill the array with each integer value.
      for (int i = 0; i < size; i++)
        array[i] = min + i;

      return array; // Return the array.
    }

    /// <summary>
    /// A function for generating an <see cref="int"/> <see cref="List{T}"/> from
    /// <paramref name="min"/> to <paramref name="max"/>.
    /// </summary>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    /// <returns>Returns an <see cref="int"/> <see cref="List{T}"/> from <paramref name="min"/> to
    /// <paramref name="max"/>.</returns>
    public static List<int> NumberList(int min, int max)
    {
      // Throw an error if the min and max are not correct.
      if (min > max)
        throw new MinMaxException<int>(min, max, true);

      int size = max - min + 1; // Calculate the final size.
      List<int> list = new List<int>(size); // Create the list, properly sized.

      // Fill the list with each integer value.
      for (int i = 0; i < size; i++)
        list.Add(min + i);

      return list; // Return the list.
    }

    /// <summary>
    /// A function for generating an <see cref="int"/> array with random numbers
    /// from <paramref name="min"/> to <paramref name="max"/>. This version defaults to using
    /// <see cref="RandomGenerators.RejectionRandom"/>.
    /// </summary>
    /// <param name="size">The size of the array</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    /// <returns>Returns an <see cref="int"/> array with random numbers from <paramref name="min"/>
    /// to <paramref name="max"/>.</returns>
    public static int[] RandomNumberArray(int size, int min, int max)
    {
      // Throw an error if the min and max are not correct.
      if (min > max)
        throw new MinMaxException<int>(min, max, true);
      if (size <= 0)
        throw new ArgumentOutOfRangeException(nameof(size), "Size is less than 0.");

      return RandomNumberArrayInternal(size, min, max, RandomGenerators.RejectionRandom);
    }

    /// <summary>
    /// A function for generating an <see cref="int"/> array with random numbers
    /// from <paramref name="min"/> to <paramref name="max"/>.
    /// </summary>
    /// <param name="size">The size of the array</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    /// <param name="generator">The <see cref="Randomization"/> number generator to use.</param>
    /// <returns>Returns an <see cref="int"/> array with random numbers from <paramref name="min"/>
    /// to <paramref name="max"/>.</returns>
    public static int[] RandomNumberArray(int size, int min, int max, RandomGenerators generator)
    {
      // Throw an error if the min and max are not correct.
      if (min > max)
        throw new MinMaxException<int>(min, max, true);
      if (size <= 0)
        throw new ArgumentOutOfRangeException(nameof(size), "Size is less than 0.");

      return RandomNumberArrayInternal(size, min, max, generator);
    }

    /// <summary>
    /// A function for generating an <see cref="int"/> array with random numbers
    /// from <paramref name="min"/> to <paramref name="max"/>.
    /// </summary>
    /// <param name="size">The size of the array</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    /// <param name="generator">The <see cref="System.Random"/> generator to use.</param>
    /// <returns>Returns an <see cref="int"/> array with random numbers from <paramref name="min"/>
    /// to <paramref name="max"/>.</returns>
    public static int[] RandomNumberArray(int size, int min, int max, Random generator)
    {
      // Throw an error if the min and max are not correct.
      if (min > max)
        throw new MinMaxException<int>(min, max, true);
      if (size <= 0)
        throw new ArgumentOutOfRangeException(nameof(size), "Size is less than 0.");

      return RandomNumberArrayInternal(size, min, max, generator);
    }

    /// <summary>
    /// A function for generating an <see cref="int"/> array with random numbers
    /// from <paramref name="min"/> to <paramref name="max"/>.
    /// </summary>
    /// <param name="size">The size of the array</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    /// <param name="generator">The <see cref="RandomNumberGenerator"/> generator to use.</param>
    /// <returns>Returns an <see cref="int"/> array with random numbers from <paramref name="min"/>
    /// to <paramref name="max"/>.</returns>
    public static int[] RandomNumberArray(int size, int min, int max,
                                          RandomNumberGenerator generator)
    {
      // Throw an error if the min and max are not correct.
      if (min > max)
        throw new MinMaxException<int>(min, max, true);
      if (size <= 0)
        throw new ArgumentOutOfRangeException(nameof(size), "Size is less than 0.");

      return RandomNumberArrayInternal(size, min, max, generator);
    }

    /// <summary>
    /// A function for generating an <see cref="int"/> <see cref="List{T}"/> with random numbers
    /// from <paramref name="min"/> to <paramref name="max"/>. This version defaults to using
    /// <see cref="RandomGenerators.RejectionRandom"/>.
    /// </summary>
    /// <param name="size">The size of the array</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    /// <returns>Returns an <see cref="int"/> <see cref="List{T}"/> with random numbers from
    /// <paramref name="min"/> to <paramref name="max"/>.</returns>
    public static List<int> RandomNumberList(int size, int min, int max)
    {
      // Throw an error if the min and max are not correct.
      if (min > max)
        throw new MinMaxException<int>(min, max, true);
      if (size <= 0)
        throw new ArgumentOutOfRangeException(nameof(size), "Size is less than 0.");

      return RandomNumberListInternal(size, min, max, RandomGenerators.RejectionRandom);
    }

    /// <summary>
    /// A function for generating an <see cref="int"/> <see cref="List{T}"/> with random numbers
    /// from <paramref name="min"/> to <paramref name="max"/>.
    /// </summary>
    /// <param name="size">The size of the array</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    /// <param name="generator">The <see cref="Randomization"/> number generator to use.</param>
    /// <returns>Returns an <see cref="int"/> <see cref="List{T}"/> with random numbers from
    /// <paramref name="min"/> to <paramref name="max"/>.</returns>
    public static List<int> RandomNumberList(int size, int min, int max, RandomGenerators generator)
    {
      // Throw an error if the min and max are not correct.
      if (min > max)
        throw new MinMaxException<int>(min, max, true);
      if (size <= 0)
        throw new ArgumentOutOfRangeException(nameof(size), "Size is less than 0.");

      return RandomNumberListInternal(size, min, max, generator);
    }

    /// <summary>
    /// A function for generating an <see cref="int"/> <see cref="List{T}"/> with random numbers
    /// from <paramref name="min"/> to <paramref name="max"/>.
    /// </summary>
    /// <param name="size">The size of the array</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    /// <param name="generator">The <see cref="System.Random"/> generator to use.</param>
    /// <returns>Returns an <see cref="int"/> <see cref="List{T}"/> with random numbers from
    /// <paramref name="min"/> to <paramref name="max"/>.</returns>
    public static List<int> RandomNumberList(int size, int min, int max, Random generator)
    {
      // Throw an error if the min and max are not correct.
      if (min > max)
        throw new MinMaxException<int>(min, max, true);
      if (size <= 0)
        throw new ArgumentOutOfRangeException(nameof(size), "Size is less than 0.");

      return RandomNumberListInternal(size, min, max, generator);
    }

    /// <summary>
    /// A function for generating an <see cref="int"/> <see cref="List{T}"/> with random numbers
    /// from <paramref name="min"/> to <paramref name="max"/>.
    /// </summary>
    /// <param name="size">The size of the array</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    /// <param name="generator">The <see cref="RandomNumberGenerator"/> generator to use.</param>
    /// <returns>Returns an <see cref="int"/> <see cref="List{T}"/> with random numbers from
    /// <paramref name="min"/> to <paramref name="max"/>.</returns>
    public static List<int> RandomNumberList(int size, int min, int max,
                                             RandomNumberGenerator generator)
    {
      // Throw an error if the min and max are not correct.
      if (min > max)
        throw new MinMaxException<int>(min, max, true);
      if (size <= 0)
        throw new ArgumentOutOfRangeException(nameof(size), "Size is less than 0.");

      return RandomNumberListInternal(size, min, max, generator);
    }

    /// <summary>
    /// An internal function for generating an <see cref="int"/> array with random numbers
    /// from <paramref name="min"/> to <paramref name="max"/>.
    /// </summary>
    /// <param name="size">The size of the array</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    /// <param name="generator">The <see cref="Randomization"/> number generator to use.</param>
    /// <returns>Returns an <see cref="int"/> array with random numbers from <paramref name="min"/>
    /// to <paramref name="max"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int[] RandomNumberArrayInternal(int size, int min, int max,
                                                   RandomGenerators generator)
    {
      int[] array = new int[size]; // Create the array, properly sized.

      // Fill the array with a random integer value.
      for (int i = 0; i < size; i++)
        array[i] = Randomization.GetRandomIntII(generator, min, max);

      return array; // Return the array.
    }

    /// <summary>
    /// An internal function for generating an <see cref="int"/> array with random numbers
    /// from <paramref name="min"/> to <paramref name="max"/>.
    /// </summary>
    /// <param name="size">The size of the array</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    /// <param name="generator">The <see cref="System.Random"/> generator to use.</param>
    /// <returns>Returns an <see cref="int"/> array with random numbers from <paramref name="min"/>
    /// to <paramref name="max"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int[] RandomNumberArrayInternal(int size, int min, int max, Random generator)
    {
      int[] array = new int[size]; // Create the array, properly sized.

      // Fill the array with a random integer value.
      for (int i = 0; i < size; i++)
        array[i] = Randomization.GetRandomIntII(generator, min, max);

      return array; // Return the array.
    }

    /// <summary>
    /// An internal function for generating an <see cref="int"/> array with random numbers
    /// from <paramref name="min"/> to <paramref name="max"/>.
    /// </summary>
    /// <param name="size">The size of the array</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    /// <param name="generator">The <see cref="RandomNumberGenerator"/> generator to use.</param>
    /// <returns>Returns an <see cref="int"/> array with random numbers from <paramref name="min"/>
    /// to <paramref name="max"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int[] RandomNumberArrayInternal(int size, int min, int max,
                                                   RandomNumberGenerator generator)
    {
      int[] array = new int[size]; // Create the array, properly sized.

      // Fill the array with a random integer value.
      for (int i = 0; i < size; i++)
        array[i] = Randomization.GetRandomIntII(generator, min, max);

      return array; // Return the array.
    }

    /// <summary>
    /// An internal function for generating an <see cref="int"/> <see cref="List{T}"/> with random
    /// numbers from <paramref name="min"/> to <paramref name="max"/>.
    /// </summary>
    /// <param name="size">The size of the array</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    /// <param name="generator">The <see cref="Randomization"/> number generator to use.</param>
    /// <returns>Returns an <see cref="int"/> <see cref="List{T}"/> with random numbers from
    /// <paramref name="min"/> to <paramref name="max"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static List<int> RandomNumberListInternal(int size, int min, int max,
                                                      RandomGenerators generator)
    {
      List<int> list = new List<int>(size); // Create the list, properly sized.

      // Fill the array with a random integer value.
      for (int i = 0; i < size; i++)
        list.Add(Randomization.GetRandomIntII(generator, min, max));

      return list; // Return the array.
    }

    /// <summary>
    /// An internal function for generating an <see cref="int"/> <see cref="List{T}"/> with random
    /// numbers from <paramref name="min"/> to <paramref name="max"/>.
    /// </summary>
    /// <param name="size">The size of the array</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    /// <param name="generator">The <see cref="System.Random"/> generator to use.</param>
    /// <returns>Returns an <see cref="int"/> <see cref="List{T}"/> with random numbers from
    /// <paramref name="min"/> to <paramref name="max"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static List<int> RandomNumberListInternal(int size, int min, int max, Random generator)
    {
      List<int> list = new List<int>(size); // Create the list, properly sized.

      // Fill the array with a random integer value.
      for (int i = 0; i < size; i++)
        list.Add(Randomization.GetRandomIntII(generator, min, max));

      return list; // Return the array.
    }

    /// <summary>
    /// An internal function for generating an <see cref="int"/> <see cref="List{T}"/> with random
    /// numbers from <paramref name="min"/> to <paramref name="max"/>.
    /// </summary>
    /// <param name="size">The size of the array</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    /// <param name="generator">The <see cref="RandomNumberGenerator"/> generator to use.</param>
    /// <returns>Returns an <see cref="int"/> <see cref="List{T}"/> with random numbers from
    /// <paramref name="min"/> to <paramref name="max"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static List<int> RandomNumberListInternal(int size, int min, int max,
                                                      RandomNumberGenerator generator)
    {
      List<int> list = new List<int>(size); // Create the list, properly sized.

      // Fill the array with a random integer value.
      for (int i = 0; i < size; i++)
        list.Add(Randomization.GetRandomIntII(generator, min, max));

      return list; // Return the array.
    }
  }
  /************************************************************************************************/
}