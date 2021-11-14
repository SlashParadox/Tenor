/**************************************************************************************************/
/*!
\file   RejectionRandom.cs
\author Craig Williams
\par    Last Updated
        2021-05-24
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for a variant of a Random class. This version uses the Rejection Method.

\par Bug List

\par References
  - Flannery, Teukolsky, Vetterling, Press, William. "Numerical Recipes for C [2nd Edition]". 1992.
*/
/**************************************************************************************************/

using SlashParadox.Tenor.Exceptions;
using SlashParadox.Tenor.Tools;

namespace SlashParadox.Tenor.Math
{
  /************************************************************************************************/
  /// <summary>
  /// A random number generating class. This is based on the Rejection Method detailed in
  /// 'Numerical Recipes for C [Second Edition] (1992)', the same function that .NET's
  /// <see cref="System.Random"/> is based on. This version comes with a key improvement.
  /// A mistyped '21' in the original class is now a proper '31'. This bug is not fixed
  /// in the original class due to compatibility issues.
  /// </summary>
  public partial class RejectionRandom : System.Random
  {
    /// <summary>A giant addition value. This can be any number, according to D.E. Knuth.</summary>
    private static readonly int MBIG = int.MaxValue;
    /// <summary>The starting seed value. This must be smaller than <see cref="MBIG"/>.</summary>
    private static readonly int MSEED = 161803398;
    /// <summary>The min value of a seed before it is added onto.</summary>
    private static readonly int MINVALUE = 0;
    /// <summary>The size of the buffer, specified by D.E. Knuth.</summary>
    private static readonly int KnuthsSize = 56;
    /// <summary>The starting value of <see cref="inextp"/>, specified by D.E. Knuth.</summary>
    private static readonly int KnuthsConstant = 31;
    /// <summary>A multiplier to turn an randomly generated integer into a floating point.</summary>
    private static readonly double FloatingPointMultiplier = 4.6566128752457969E-10;

    /// <summary>The lower accessor variable into the <see cref="seedArray"/> buffer.</summary>
    private int inext = 0;
    /// <summary>The higher accessor variable into the <see cref="seedArray"/> buffer.</summary>
    private int inextp = KnuthsConstant;
    /// <summary>The buffer for the seeded values.</summary>
    private readonly int[] seedArray = new int[KnuthsSize];

    /// <summary>
    /// A default constructor for a <see cref="RejectionRandom"/> generator. This will create
    /// a generator with the current <see cref="System.Environment.TickCount"/> as a seed.
    /// </summary>
    public RejectionRandom() : this(System.Environment.TickCount) { }

    /// <summary>
    /// A constructor for a <see cref="RejectionRandom"/> generator This will create a generator
    /// with the given <paramref name="seed"/>.
    /// </summary>
    /// <param name="seed">The seed to use with this generator.</param>
    public RejectionRandom(int seed)
    {
      int bufferSize = KnuthsSize - 1; // Get the buffer size.

      int value0 = MSEED - System.Math.Abs(seed); // Get a starting seed value.
      seedArray[bufferSize] = value0; // Insert as the last element in the buffer.

      int value1 = 1; // Get another seed value.

      for (int i = 1; i < bufferSize; i++)
      {
        int index = 21 * i % bufferSize; // Get an index into the buffer.
        seedArray[index] = value1; // Set the previous value into the buffer.

        // Update the second value. If negative, add a large number on to it.
        value1 = value0 - value1;
        if (value1 < MINVALUE)
          value1 += MBIG;

        // Add more random seeds to the buffer.
        for (int j = 1; j < 5; j++)
        {
          for (int k = 1; k < KnuthsSize; k++)
          {
            int value2 = seedArray[k] - seedArray[1 + (k + 30) % bufferSize];
            if (value2 < MINVALUE)
              value2 += MBIG;

            seedArray[k] = value2;
          }
        }

        // Safety set. Can be removed.
        inext = 0;
        inextp = KnuthsConstant;
      }
    }

    /// <summary>
    /// An internal function for getting a sample value randomly.
    /// </summary>
    /// <returns>Returns a random integer value.</returns>
    protected virtual int GetIntSample()
    {
      // Increment the inext and inextp indexes, wrapping back to one at max index.
      if (++inext >= KnuthsSize)
        inext = 1;
      if (++inextp >= KnuthsSize)
        inextp = 1;

      // Generate a new value via subtraction. Fix it to make sure it stays on range.
      int value = seedArray[inext] - seedArray[inextp];
      if (value < MINVALUE)
        value += MBIG;

      // Return the value to the buffer.
      seedArray[inext] = value;
      return value;
    }

    /// <summary>
    /// An internal function for getting a random sample value when there is a
    /// large range of values.
    /// </summary>
    /// <returns>Returns a random, larger double value.</returns>
    protected virtual double GetLargeDoubleSample()
    {
      double value = GetIntSample(); // Get a default sample.

      // If the next sample is divisible by 2, it's best to negate the value.
      if (GetIntSample() % 2 == 0)
        value = -value;

      // Extra math is required for getting a valid double sample.
      value += (int.MaxValue - 1);
      return value / (((long)int.MaxValue * 2) + 1);
    }

    /// <summary>
    /// A function for filling a given array of bytes with random values.
    /// </summary>
    /// <param name="buffer">The array to place values into. This must be sized to the number of
    /// bytes to generate.</param>
    public override void NextBytes(byte[] buffer)
    {
      if (!buffer.IsEmptyOrNull())
      {
        int length = buffer.Length;
        for (int i = 0; i < length; i++)
          buffer[i] = (byte)(GetIntSample() % (byte.MaxValue + 1));
      }
    }

    /// <summary>
    /// A function that gets the next non-negative integer in the seed, in a range of
    /// [0, <see cref="int.MaxValue"/>).
    /// </summary>
    /// <returns>Returns a pseudo-random, non-negative integer from the current seed.</returns>
    public override int Next()
    {
      return GetIntSample();
    }

    /// <summary>
    /// A function that gets a random integer in a range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="minValue">The minimum value of the range.</param>
    /// <param name="maxValue">The maximum value of the range.</param>
    /// <returns>Returns a random integer within the range.</returns>
    public override int Next(int minValue, int maxValue)
    {
      if (minValue > maxValue)
        throw new MinMaxException<int>(minValue, maxValue, true);

      long difference = (long)maxValue - minValue;
      if (difference <= int.MaxValue)
        return (int)(Sample() * difference) + minValue;

      return (int)((long)(GetLargeDoubleSample() * difference) + minValue);
    }

    /// <summary>
    /// A function that gets the next double in the seed, in a range of [0.0, 1.0).
    /// </summary>
    /// <returns>Returns a pseudo-random double, valued from 0.0 to 1.0.</returns>
    public override double NextDouble()
    {
      return Sample(); // Return a double sample.
    }

    /// <summary>
    /// An internal function for getting a double sample value randomly between 0.0 and 1.0.
    /// </summary>
    /// <returns>Returns a random double value between 0.0 and 1.0.</returns>
    protected override double Sample()
    {
      // Get an int sample, and turn it into a double.
      return GetIntSample() * FloatingPointMultiplier;
    }
  }
  /************************************************************************************************/
}