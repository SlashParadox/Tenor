/**************************************************************************************************/
/*!
\file   Randomization.cs
\author Craig Williams
\par    Last Updated
        2021-05-24
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for helper functions for randomly generating values. This also contains references to
  global random generators for use in an entire project.

\par Bug List

\par References
  - https://docs.microsoft.com/en-us/archive/msdn-magazine/2007/september/net-matters-tales-from-the-cryptorandom
  - https://stackoverflow.com/a/51590636
  - https://stackoverflow.com/a/52439575
  - https://stackoverflow.com/a/33328356
  - https://stackoverflow.com/a/610228
*/
/**************************************************************************************************/

using CodeParadox.Tenor.Math;
using CodeParadox.Tenor.Exceptions;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace CodeParadox.Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// A kit of functions for generating random values of many types.
  /// </summary>
  public static partial class Randomization
  {
    /**********************************************************************************************/
    /// <summary>
    /// A class of information used by <see cref="Randomization"/> to determine how to handle
    /// irregular floating point values, such as <see cref="float.PositiveInfinity"/>.
    /// <see cref="Randomization"/> only holds onto its own reference to this object, and copies
    /// values from other instances rather than pointing to those instances.
    /// </summary>
    public sealed class FloatingPointAdjustment// : ICopyable<FloatingPointAdjustment>
    {
      /// <summary>A toggle for if irregular values should be adjusted.</summary>
      public bool AdjustErrors = true;
      /// <summary>The value to return if <see cref="AdjustErrors"/> is false.</summary>
      public double DefaultReturn = double.MinValue;
      /// <summary>The adjusted value for positive infinity values.</summary>
      public double PositiveInfinityAdjustment = double.MaxValue;
      /// <summary>The adjusted value for negative infinity values.</summary>
      public double NegativeInfinityAdjustment = double.MinValue;
      /// <summary>The adjusted value for NaN (Not a Number) values.</summary>
      public double NaNAdjustment = double.MinValue;

      /// <summary>
      /// The default constructor for a <see cref="FloatingPointAdjustment"/>.
      /// </summary>
      public FloatingPointAdjustment() { }

      /// <summary>
      /// A constructor for a <see cref="FloatingPointAdjustment"/>.
      /// </summary>
      /// <param name="AdjustErrors">A toggle for if irregular values should be adjusted.</param>
      public FloatingPointAdjustment(bool AdjustErrors)
      {
        this.AdjustErrors = AdjustErrors;
      }

      /// <summary>
      /// A constructor for a <see cref="FloatingPointAdjustment"/>.
      /// </summary>
      /// <param name="AdjustErrors">A toggle for if irregular values should be adjusted.</param>
      /// <param name="DefaultReturn">What to return if <see cref="AdjustErrors"/> is false.</param>
      public FloatingPointAdjustment(bool AdjustErrors, double DefaultReturn)
      {
        this.AdjustErrors = AdjustErrors;
        this.DefaultReturn = DefaultReturn;
      }

      /// <summary>
      /// A constructor for a <see cref="FloatingPointAdjustment"/>.
      /// </summary>
      /// <param name="AdjustErrors">A toggle for if irregular values should be adjusted.</param>
      /// <param name="DefaultReturn">What to return if <see cref="AdjustErrors"/> is false.</param>
      /// <param name="PositiveInfinityAdjustment">The adjustment for + infinity values.</param>
      public FloatingPointAdjustment(bool AdjustErrors, double DefaultReturn,
                                     double PositiveInfinityAdjustment)
      {
        this.AdjustErrors = AdjustErrors;
        this.DefaultReturn = DefaultReturn;
        this.PositiveInfinityAdjustment = PositiveInfinityAdjustment;
      }

      /// <summary>
      /// A constructor for a <see cref="FloatingPointAdjustment"/>.
      /// </summary>
      /// <param name="AdjustErrors">A toggle for if irregular values should be adjusted.</param>
      /// <param name="DefaultReturn">What to return if <see cref="AdjustErrors"/> is false.</param>
      /// <param name="PositiveInfinityAdjustment">The adjustment for + infinity values.</param>
      /// <param name="NegativeInfinityAdjustment">The adjustment for - infinity values.</param>
      public FloatingPointAdjustment(bool AdjustErrors, double DefaultReturn,
                                     double PositiveInfinityAdjustment,
                                     double NegativeInfinityAdjustment)
      {
        this.AdjustErrors = AdjustErrors;
        this.DefaultReturn = DefaultReturn;
        this.PositiveInfinityAdjustment = PositiveInfinityAdjustment;
        this.NegativeInfinityAdjustment = NegativeInfinityAdjustment;
      }

      /// <summary>
      /// A constructor for a <see cref="FloatingPointAdjustment"/>.
      /// </summary>
      /// <param name="AdjustErrors">A toggle for if irregular values should be adjusted.</param>
      /// <param name="DefaultReturn">What to return if <see cref="AdjustErrors"/> is false.</param>
      /// <param name="PositiveInfinityAdjustment">The adjustment for + infinity values.</param>
      /// <param name="NegativeInfinityAdjustment">The adjustment for - infinity values.</param>
      /// <param name="NaNAdjustment">The adjustment for NaN (Not a Number) values.</param>
      public FloatingPointAdjustment(bool AdjustErrors, double DefaultReturn,
                                     double PositiveInfinityAdjustment,
                                     double NegativeInfinityAdjustment, double NaNAdjustment)
      {
        this.AdjustErrors = AdjustErrors;
        this.DefaultReturn = DefaultReturn;
        this.PositiveInfinityAdjustment = PositiveInfinityAdjustment;
        this.NegativeInfinityAdjustment = NegativeInfinityAdjustment;
        this.NaNAdjustment = NaNAdjustment;
      }

      /// <summary>
      /// A copy constructor for a <see cref="FloatingPointAdjustment"/>.
      /// </summary>
      /// <param name="original">The <see cref="FloatingPointAdjustment"/> to copy.</param>
      public FloatingPointAdjustment(FloatingPointAdjustment original)
      {
        CopyFrom(original);
      }

      /// <summary>
      /// A function for adjusting the values of a floating-point, based on the provided rules.
      /// </summary>
      /// <param name="value">The value to adjust.</param>
      /// <returns>Returns the adjusted <paramref name="value"/>.</returns>
      public float AdjustFloatingPoint(float value)
      {
        // If we can adjust errors, check the value.
        if (AdjustErrors)
        {
          if (float.IsPositiveInfinity(value))
            return (float)PositiveInfinityAdjustment; // The value is positive infinity.
          if (float.IsNegativeInfinity(value))
            return (float)NegativeInfinityAdjustment; // The value is negative infinity.
          if (float.IsNaN(value))
            return (float)NaNAdjustment; // The value is not a number
        }
        else if (float.IsInfinity(value) || float.IsNaN(value))
          return (float)DefaultReturn; // Otherwise, return the default if there's irregularity.

        return value; // If there is no issue, return the base value.
      }

      /// <summary>
      /// A function for adjusting the values of a floating-point, based on the provided rules.
      /// </summary>
      /// <param name="value">The value to adjust.</param>
      /// <returns>Returns the adjusted <paramref name="value"/>.</returns>
      public double AdjustFloatingPoint(double value)
      {
        // If we can adjust errors, check the value.
        if (AdjustErrors)
        {
          if (double.IsPositiveInfinity(value))
            return PositiveInfinityAdjustment; // The value is positive infinity.
          if (double.IsNegativeInfinity(value))
            return NegativeInfinityAdjustment; // The value is negative infinity.
          if (double.IsNaN(value))
            return NaNAdjustment; // The value is not a number
        }
        else if (double.IsInfinity(value) || double.IsNaN(value))
          return DefaultReturn; // Otherwise, return the default if there's irregularity.

        return value; // If there is no issue, return the base value.
      }

      public void CopyFrom(FloatingPointAdjustment original)
      {
        this.AdjustErrors = original.AdjustErrors;
        this.PositiveInfinityAdjustment = original.PositiveInfinityAdjustment;
        this.NegativeInfinityAdjustment = original.NegativeInfinityAdjustment;
        this.NaNAdjustment = original.NaNAdjustment;
      }

      public void CopyTo(FloatingPointAdjustment copy)
      {
        copy.CopyFrom(this);
      }
    }
    /**********************************************************************************************/

    /// <summary>The maximum amount of bits usable to create a secure double.</summary>
    private static readonly int MaxSecureDoubleBits = 53;
    /// <summary>The amount of lost precision when generating a double from 0 to 1.</summary>
    private static readonly double LostDoublePrecision = 1.0 - 0.9999999995343387126922607421875;
    /// <summary>The max hi for making a <see cref="decimal"/> with uniform distribution.</summary>
    private static readonly int MaxDecimalHi = 542101086;
    /// <summary>The max scale for a <see cref="decimal"/>.</summary>
    private static readonly byte MaxDecimalScale = 28;
    /// <summary>The rules used for dealing with certain floating point values.</summary>
    private static readonly FloatingPointAdjustment FloatPointRules = new FloatingPointAdjustment();

    /// <summary>A global random number generator, based on the .NET standard class.</summary>
    private static Random StandardGenerator = null;
    /// <summary>A global random number generator, based on the rejection method.</summary>
    private static RejectionRandom RejectionGenerator = null;
    /// <summary>A global random number generator, based on the .NET cryptography class.</summary>
    private static RNGCryptoServiceProvider CryptoGenerator = null;
    

    /// <summary>
    /// The static constructor for the <see cref="Randomization"/> tool.
    /// </summary>
    static Randomization()
    {
      StandardGenerator = new Random();
      RejectionGenerator = new RejectionRandom();
      CryptoGenerator = new RNGCryptoServiceProvider();
    }

    /// <summary>
    /// A function to reset the global standard generator with a new seed. This uses the current
    /// <see cref="System.Environment.TickCount"/> for the seed.
    /// </summary>
    public static void ResetStandardGenerator()
    {
      StandardGenerator = new Random();
    }

    /// <summary>
    /// A function to reset the global standard generator with a new seed.
    /// </summary>
    /// <param name="seed">The new seed to use for the generator.</param>
    public static void ResetStandardGenerator(int seed)
    {
      StandardGenerator = new Random(seed);
    }

    /// <summary>
    /// A function to reset the global rejection generator with a new seed. This uses the current
    /// <see cref="System.Environment.TickCount"/> for the seed.
    /// </summary>
    public static void ResetRejectionGenerator()
    {
      RejectionGenerator = new RejectionRandom();
    }

    /// <summary>
    /// A function to reset the global rjection generator with a new seed.
    /// </summary>
    /// <param name="seed">The new seed to use for the generator.</param>
    public static void ResetRejectionGenerator(int seed)
    {
      RejectionGenerator = new RejectionRandom(seed);
    }

    /// <summary>
    /// A function to reset the global crypto generator.
    /// </summary>
    public static void ResetCryptoGenerator()
    {
      CryptoGenerator = new RNGCryptoServiceProvider();
    }

    /// <summary>
    /// A function to update the rules used to adjust irregular floating point values.
    /// Please note this makes a copy of the passed-in <paramref name="rules"/>, via the
    /// <see cref="ICopyable{TType}"/> interface.
    /// </summary>
    /// <param name="rules">The new rules for adjusting irregular floating point values.</param>
    public static void UpdateFloatingPointAdjustmentRules(FloatingPointAdjustment rules)
    {
      // If the rules are not null, update the rule via copying.
      if (rules != null)
        FloatPointRules.CopyFrom(rules);
    }

    /// <summary>
    /// A function for getting a copy of the current rules for adjusting irregular floating point
    /// values.
    /// </summary>
    /// <param name="copy">The copy to store the values of <see cref="FloatPointRules"/> into.
    /// This is handled via the <see cref="ICopyable{TType}"/> interface. This should not be
    /// null. If so, use the 'out' variant of this function.</param>
    public static void GetFloatingPointAdjustmentRules(FloatingPointAdjustment copy)
    {
      if (copy != null)
        copy.CopyFrom(FloatPointRules);
    }

    /// <summary>
    /// A function for getting a copy of the current rules for adjusting irregular floating point
    /// values.
    /// </summary>
    /// <param name="copy">The copy of the current <see cref="FloatPointRules"/>.</param>
    public static void GetFloatingPointAdjustmentRules(out FloatingPointAdjustment copy)
    {
      copy = new FloatingPointAdjustment(FloatPointRules);
    }

    /// <summary>
    /// A function for getting a copy of the current rules for adjusting irregular floating point
    /// values.
    /// </summary>
    /// <returns>Returns a copy of the current <see cref="FloatPointRules"/>.</returns>
    public static FloatingPointAdjustment GetFloatingPointAdjustmentRules()
    {
      return new FloatingPointAdjustment(FloatPointRules);
    }

    /// <summary>
    /// A function for generating random values, and storing them into a byte array.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="bytes">The array to store the values into. This must be sized to the number of
    /// bytes to generate.</param>
    public static void GetRandomBytes(this Random random, byte[] bytes)
    {
      // If the generator and count are valid, create an array of random bytes.
      if (random != null && !bytes.IsEmptyOrNull())
        random.InternalGetRandomBytes(bytes);
    }

    /// <summary>
    /// A function for generating random values, and storing them into a byte array.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="count">The number of bytes to generate. This must be greater than 0.</param>
    /// <param name="bytes">The array of generated bytes.</param>
    public static void GetRandomBytes(this Random random, int count, out byte[] bytes)
    {
      bytes = null; // Initialize bytes.

      // If the generator and count are valid, create an array of random bytes.
      if (random != null && count > 0)
        random.InternalGetRandomBytes(count, out bytes);
    }

    /// <summary>
    /// A function for generating random values, and storing them into a byte array.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="count">The number of bytes to generate. This must be greater than 0.</param>
    /// <returns>Returns the generated byte array.</returns>
    public static byte[] GetRandomBytes(this Random random, int count)
    {
      // If the generator is valid, and the count is valid, generate the bytes.
      return (random != null && count > 0) ? random.InternalGetRandomBytes(count) : null;
    }

    /// <summary>
    /// A function for generating random values, and storing them into a byte array.
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="bytes">The array to store the values into. This must be sized to the number of
    /// bytes to generate.</param>
    public static void GetRandomBytes(this RandomNumberGenerator random, byte[] bytes)
    {
      // If the generator and count are valid, create an array of random bytes.
      if (random != null && !bytes.IsEmptyOrNull())
        random.InternalGetRandomBytes(bytes);
    }

    /// <summary>
    /// A function for generating random values, and storing them into a byte array.
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="count">The number of bytes to generate. This must be greater than 0.</param>
    /// <param name="bytes">The array of generated bytes.</param>
    public static void GetRandomBytes(this RandomNumberGenerator random, int count,
                                      out byte[] bytes)
    {
      bytes = null; // Initialize bytes.

      // If the generator and count are valid, create an array of random bytes.
      if (random != null && count > 0)
        random.InternalGetRandomBytes(count, out bytes);
    }

    /// <summary>
    /// A function for generating random values, and storing them into a byte array.
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="count">The number of bytes to generate. This must be greater than 0.</param>
    /// <returns>Returns the generated byte array.</returns>
    public static byte[] GetRandomBytes(this RandomNumberGenerator random, int count)
    {
      // If the generator is valid, and the count is valid, generate the bytes.
      return (random != null && count > 0) ? random.InternalGetRandomBytes(count) : null;
    }

    /// <summary>
    /// A function for generating random values, and storing them into a byte array.
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="bytes">The array to store the values into. This must be sized to the number of
    /// bytes to generate.</param>
    public static void GetRandomBytes(RandomGenerators generator, byte[] bytes)
    {
      if (!bytes.IsEmptyOrNull())
        InternalGetRandomBytes(generator, bytes);
    }

    /// <summary>
    /// A function for generating random values, and storing them into a byte array.
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="count">The number of bytes to generate. This must be greater than 0.</param>
    /// <param name="bytes">The array of generated bytes.</param>
    public static void GetRandomBytes(RandomGenerators generator, int count, out byte[] bytes)
    {
      bytes = null; // Initialize bytes.

      // If the count is valid, create an array of random bytes.
      if (count > 0)
        InternalGetRandomBytes(generator, count, out bytes);
    }

    /// <summary>
    /// A function for generating random values, and storing them into a byte array.
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="count">The number of bytes to generate. This must be greater than 0.</param>
    /// <returns>Returns the generated byte array.</returns>
    public static byte[] GetRandomBytes(RandomGenerators generator, int count)
    {
      // If the count is valid, generate the bytes.
      return count > 0 ? InternalGetRandomBytes(generator, count) : null;
    }

    /// <summary>
    /// A function for generating any <see cref="sbyte"/> on the range of
    /// [<see cref="sbyte.MinValue"/>, <see cref="sbyte.MaxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="sbyte"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static sbyte GetRandomSByte(this Random random)
    {
      // If the generator is not null, return a random sbyte on the full range.
      if (random != null)
        return random.InternalGetRandomSByte();
      // If null, throw an error.
      throw new ArgumentNullException(nameof(random), "Random generator is null.");
    }

    /// <summary>
    /// A function for generating any <see cref="sbyte"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="sbyte"/>.</returns>
    public static sbyte GetRandomSByteII(this Random random, sbyte minValue, sbyte maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (sbyte)random.InternalGetRandomIntII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="sbyte"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="sbyte"/>.</returns>
    public static sbyte GetRandomSByteEE(this Random random, sbyte minValue, sbyte maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (sbyte)random.InternalGetRandomIntEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="sbyte"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="sbyte"/>.</returns>
    public static sbyte GetRandomSByteIE(this Random random, sbyte minValue, sbyte maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (sbyte)random.InternalGetRandomIntIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="sbyte"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="sbyte"/>.</returns>
    public static sbyte GetRandomSByteEI(this Random random, sbyte minValue, sbyte maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (sbyte)random.InternalGetRandomIntEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="sbyte"/> on the range of
    /// [<see cref="sbyte.MinValue"/>, <see cref="sbyte.MaxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="sbyte"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static sbyte GetRandomSByte(this RandomNumberGenerator random)
    {
      // If the generator is not null, return a random sbyte on the full range.
      if (random != null)
        return random.InternalGetRandomSByte();
      // If null, throw an error.
      throw new ArgumentNullException(nameof(random), "Random generator is null.");
    }

    /// <summary>
    /// A function for generating any <see cref="sbyte"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="sbyte"/>.</returns>
    public static sbyte GetRandomSByteII(this RandomNumberGenerator random, sbyte minValue,
                                         sbyte maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (sbyte)random.InternalGetRandomIntII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="sbyte"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="sbyte"/>.</returns>
    public static sbyte GetRandomSByteEE(this RandomNumberGenerator random, sbyte minValue,
                                         sbyte maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (sbyte)random.InternalGetRandomIntEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="sbyte"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="sbyte"/>.</returns>
    public static sbyte GetRandomSByteIE(this RandomNumberGenerator random, sbyte minValue,
                                         sbyte maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (sbyte)random.InternalGetRandomIntIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="sbyte"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="sbyte"/>.</returns>
    public static sbyte GetRandomSByteEI(this RandomNumberGenerator random, sbyte minValue,
                                         sbyte maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (sbyte)random.InternalGetRandomIntEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="sbyte"/> on the range of
    /// [<see cref="sbyte.MinValue"/>, <see cref="sbyte.MaxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="sbyte"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static sbyte GetRandomSByte(RandomGenerators generator)
    {
      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomSByte(),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomSByte(),
        _ => StandardGenerator.InternalGetRandomSByte(),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="sbyte"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="sbyte"/>.</returns>
    public static sbyte GetRandomSByteII(RandomGenerators generator, sbyte minValue, sbyte maxValue)
    {
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => (sbyte)RejectionGenerator.InternalGetRandomIntII(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (sbyte)CryptoGenerator.InternalGetRandomIntII(minValue, maxValue),
        _ => (sbyte)StandardGenerator.InternalGetRandomIntII(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="sbyte"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="sbyte"/>.</returns>
    public static sbyte GetRandomSByteEE(RandomGenerators generator, sbyte minValue, sbyte maxValue)
    {
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => (sbyte)RejectionGenerator.InternalGetRandomIntEE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (sbyte)CryptoGenerator.InternalGetRandomIntEE(minValue, maxValue),
        _ => (sbyte)StandardGenerator.InternalGetRandomIntEE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="sbyte"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="sbyte"/>.</returns>
    public static sbyte GetRandomSByteIE(RandomGenerators generator, sbyte minValue, sbyte maxValue)
    {
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => (sbyte)RejectionGenerator.InternalGetRandomIntIE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (sbyte)CryptoGenerator.InternalGetRandomIntIE(minValue, maxValue),
        _ => (sbyte)StandardGenerator.InternalGetRandomIntIE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="sbyte"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="sbyte"/>.</returns>
    public static sbyte GetRandomSByteEI(RandomGenerators generator, sbyte minValue, sbyte maxValue)
    {
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => (sbyte)RejectionGenerator.InternalGetRandomIntEI(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (sbyte)CryptoGenerator.InternalGetRandomIntEI(minValue, maxValue),
        _ => (sbyte)StandardGenerator.InternalGetRandomIntEI(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="byte"/> on the range of
    /// [<see cref="byte.MinValue"/>, <see cref="byte.MaxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="byte"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static byte GetRandomByte(this Random random)
    {
      // If the generator is not null, return a random byte on the full range.
      if (random != null)
        return random.InternalGetRandomByte();
      // If null, throw an error.
      throw new ArgumentNullException(nameof(random), "Random generator is null.");
    }

    /// <summary>
    /// A function for generating any <see cref="byte"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="byte"/>.</returns>
    public static byte GetRandomByteII(this Random random, byte minValue, byte maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (byte)random.InternalGetRandomIntII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="byte"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="byte"/>.</returns>
    public static byte GetRandomByteEE(this Random random, byte minValue, byte maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (byte)random.InternalGetRandomIntEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="byte"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="byte"/>.</returns>
    public static byte GetRandomByteIE(this Random random, byte minValue, byte maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (byte)random.InternalGetRandomIntIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="byte"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="byte"/>.</returns>
    public static byte GetRandomByteEI(this Random random, byte minValue, byte maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (byte)random.InternalGetRandomIntEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="byte"/> on the range of
    /// [<see cref="byte.MinValue"/>, <see cref="byte.MaxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="byte"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static byte GetRandomByte(this RandomNumberGenerator random)
    {
      // If the generator is not null, return a random byte on the full range.
      if (random != null)
        return random.InternalGetRandomByte();
      // If null, throw an error.
      throw new ArgumentNullException(nameof(random), "Random generator is null.");
    }

    /// <summary>
    /// A function for generating any <see cref="byte"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="byte"/>.</returns>
    public static byte GetRandomByteII(this RandomNumberGenerator random, byte minValue,
                                       byte maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (byte)random.InternalGetRandomIntII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="byte"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="byte"/>.</returns>
    public static byte GetRandomByteEE(this RandomNumberGenerator random, byte minValue,
                                       byte maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (byte)random.InternalGetRandomIntEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="byte"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="byte"/>.</returns>
    public static byte GetRandomByteIE(this RandomNumberGenerator random, byte minValue,
                                       byte maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (byte)random.InternalGetRandomIntIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="byte"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="byte"/>.</returns>
    public static byte GetRandomByteEI(this RandomNumberGenerator random, byte minValue,
                                       byte maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (byte)random.InternalGetRandomIntEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="byte"/> on the range of
    /// [<see cref="byte.MinValue"/>, <see cref="byte.MaxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="byte"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static byte GetRandomByte(RandomGenerators generator)
    {
      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomByte(),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomByte(),
        _ => StandardGenerator.InternalGetRandomByte(),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="byte"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="byte"/>.</returns>
    public static byte GetRandomByteII(RandomGenerators generator, byte minValue, byte maxValue)
    {
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => (byte)RejectionGenerator.InternalGetRandomIntII(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (byte)CryptoGenerator.InternalGetRandomIntII(minValue, maxValue),
        _ => (byte)StandardGenerator.InternalGetRandomIntII(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="byte"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="byte"/>.</returns>
    public static byte GetRandomByteEE(RandomGenerators generator, byte minValue, byte maxValue)
    {
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => (byte)RejectionGenerator.InternalGetRandomIntEE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (byte)CryptoGenerator.InternalGetRandomIntEE(minValue, maxValue),
        _ => (byte)StandardGenerator.InternalGetRandomIntEE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="byte"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="byte"/>.</returns>
    public static byte GetRandomByteIE(RandomGenerators generator, byte minValue, byte maxValue)
    {
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => (byte)RejectionGenerator.InternalGetRandomIntIE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (byte)CryptoGenerator.InternalGetRandomIntIE(minValue, maxValue),
        _ => (byte)StandardGenerator.InternalGetRandomIntIE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="byte"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="byte"/>.</returns>
    public static byte GetRandomByteEI(RandomGenerators generator, byte minValue, byte maxValue)
    {
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => (byte)RejectionGenerator.InternalGetRandomIntEI(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (byte)CryptoGenerator.InternalGetRandomIntEI(minValue, maxValue),
        _ => (byte)StandardGenerator.InternalGetRandomIntEI(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="short"/> on the range of
    /// [<see cref="short.MinValue"/>, <see cref="short.MaxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="short"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static short GetRandomShort(this Random random)
    {
      // If the generator is not null, return a random short on the full range.
      if (random != null)
        return random.InternalGetRandomShort();
      // If null, throw an error.
      throw new ArgumentNullException(nameof(random), "Random generator is null.");
    }

    /// <summary>
    /// A function for generating any <see cref="short"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="short"/>.</returns>
    public static short GetRandomShortII(this Random random, short minValue, short maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (short)random.InternalGetRandomIntII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="short"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="short"/>.</returns>
    public static short GetRandomShortEE(this Random random, short minValue, short maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (short)random.InternalGetRandomIntEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="short"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="short"/>.</returns>
    public static short GetRandomShortIE(this Random random, short minValue, short maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (short)random.InternalGetRandomIntIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="short"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="short"/>.</returns>
    public static short GetRandomShortEI(this Random random, short minValue, short maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (short)random.InternalGetRandomIntEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="short"/> on the range of
    /// [<see cref="short.MinValue"/>, <see cref="short.MaxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="short"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static short GetRandomShort(this RandomNumberGenerator random)
    {
      // If the generator is not null, return a random short on the full range.
      if (random != null)
        return random.InternalGetRandomShort();
      // If null, throw an error.
      throw new ArgumentNullException(nameof(random), "Random generator is null.");
    }

    /// <summary>
    /// A function for generating any <see cref="short"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="short"/>.</returns>
    public static short GetRandomShortII(this RandomNumberGenerator random, short minValue,
                                         short maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (short)random.InternalGetRandomIntII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="short"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="short"/>.</returns>
    public static short GetRandomShortEE(this RandomNumberGenerator random, short minValue,
                                         short maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (short)random.InternalGetRandomIntEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="short"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="short"/>.</returns>
    public static short GetRandomShortIE(this RandomNumberGenerator random, short minValue,
                                         short maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (short)random.InternalGetRandomIntIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="short"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="short"/>.</returns>
    public static short GetRandomShortEI(this RandomNumberGenerator random, short minValue,
                                         short maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (short)random.InternalGetRandomIntEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="short"/> on the range of
    /// [<see cref="short.MinValue"/>, <see cref="short.MaxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="short"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static short GetRandomShort(RandomGenerators generator)
    {
      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomShort(),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomShort(),
        _ => StandardGenerator.InternalGetRandomShort(),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="short"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="short"/>.</returns>
    public static short GetRandomShortII(RandomGenerators generator, short minValue, short maxValue)
    {
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => (short)RejectionGenerator.InternalGetRandomIntII(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (short)CryptoGenerator.InternalGetRandomIntII(minValue, maxValue),
        _ => (short)StandardGenerator.InternalGetRandomIntII(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="short"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="short"/>.</returns>
    public static short GetRandomShortEE(RandomGenerators generator, short minValue, short maxValue)
    {
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => (short)RejectionGenerator.InternalGetRandomIntEE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (short)CryptoGenerator.InternalGetRandomIntEE(minValue, maxValue),
        _ => (short)StandardGenerator.InternalGetRandomIntEE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="short"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="short"/>.</returns>
    public static short GetRandomShortIE(RandomGenerators generator, short minValue, short maxValue)
    {
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => (short)RejectionGenerator.InternalGetRandomIntIE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (short)CryptoGenerator.InternalGetRandomIntIE(minValue, maxValue),
        _ => (short)StandardGenerator.InternalGetRandomIntIE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="short"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="short"/>.</returns>
    public static short GetRandomShortEI(RandomGenerators generator, short minValue, short maxValue)
    {
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => (short)RejectionGenerator.InternalGetRandomIntEI(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (short)CryptoGenerator.InternalGetRandomIntEI(minValue, maxValue),
        _ => (short)StandardGenerator.InternalGetRandomIntEI(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="ushort"/> on the range of
    /// [<see cref="ushort.MinValue"/>, <see cref="ushort.MaxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="ushort"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static ushort GetRandomUShort(this Random random)
    {
      // If the generator is not null, return a random ushort on the full range.
      if (random != null)
        return random.InternalGetRandomUShort();
      // If null, throw an error.
      throw new ArgumentNullException(nameof(random), "Random generator is null.");
    }

    /// <summary>
    /// A function for generating any <see cref="ushort"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ushort"/>.</returns>
    public static ushort GetRandomUShortII(this Random random, ushort minValue, ushort maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (ushort)random.InternalGetRandomIntII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="ushort"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ushort"/>.</returns>
    public static ushort GetRandomUShortEE(this Random random, ushort minValue, ushort maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (ushort)random.InternalGetRandomIntEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="ushort"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ushort"/>.</returns>
    public static ushort GetRandomUShortIE(this Random random, ushort minValue, ushort maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (ushort)random.InternalGetRandomIntIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="ushort"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ushort"/>.</returns>
    public static ushort GetRandomUShortEI(this Random random, ushort minValue, ushort maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (ushort)random.InternalGetRandomIntEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="ushort"/> on the range of
    /// [<see cref="ushort.MinValue"/>, <see cref="ushort.MaxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="ushort"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static ushort GetRandomUShort(this RandomNumberGenerator random)
    {
      // If the generator is not null, return a random ushort on the full range.
      if (random != null)
        return random.InternalGetRandomUShort();
      // If null, throw an error.
      throw new ArgumentNullException(nameof(random), "Random generator is null.");
    }

    /// <summary>
    /// A function for generating any <see cref="ushort"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ushort"/>.</returns>
    public static ushort GetRandomUShortII(this RandomNumberGenerator random, ushort minValue,
                                           ushort maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (ushort)random.InternalGetRandomIntII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="ushort"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ushort"/>.</returns>
    public static ushort GetRandomUShortEE(this RandomNumberGenerator random, ushort minValue,
                                           ushort maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (ushort)random.InternalGetRandomIntEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="ushort"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ushort"/>.</returns>
    public static ushort GetRandomUShortIE(this RandomNumberGenerator random, ushort minValue,
                                           ushort maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (ushort)random.InternalGetRandomIntIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="ushort"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ushort"/>.</returns>
    public static ushort GetRandomUShortEI(this RandomNumberGenerator random, ushort minValue,
                                           ushort maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return (ushort)random.InternalGetRandomIntEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="ushort"/> on the range of
    /// [<see cref="ushort.MinValue"/>, <see cref="ushort.MaxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="ushort"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static ushort GetRandomUShort(RandomGenerators generator)
    {
      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomUShort(),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomUShort(),
        _ => StandardGenerator.InternalGetRandomUShort(),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="ushort"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ushort"/>.</returns>
    public static ushort GetRandomUShortII(RandomGenerators generator, ushort minValue,
                                           ushort maxValue)
    {
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => (ushort)RejectionGenerator.InternalGetRandomIntII(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (ushort)CryptoGenerator.InternalGetRandomIntII(minValue, maxValue),
        _ => (ushort)StandardGenerator.InternalGetRandomIntII(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="ushort"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ushort"/>.</returns>
    public static ushort GetRandomUShortEE(RandomGenerators generator, ushort minValue,
                                           ushort maxValue)
    {
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => (ushort)RejectionGenerator.InternalGetRandomIntEE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (ushort)CryptoGenerator.InternalGetRandomIntEE(minValue, maxValue),
        _ => (ushort)StandardGenerator.InternalGetRandomIntEE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="ushort"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ushort"/>.</returns>
    public static ushort GetRandomUShortIE(RandomGenerators generator, ushort minValue,
                                           ushort maxValue)
    {
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => (ushort)RejectionGenerator.InternalGetRandomIntIE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (ushort)CryptoGenerator.InternalGetRandomIntIE(minValue, maxValue),
        _ => (ushort)StandardGenerator.InternalGetRandomIntIE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="ushort"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ushort"/>.</returns>
    public static ushort GetRandomUShortEI(RandomGenerators generator, ushort minValue,
                                           ushort maxValue)
    {
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => (ushort)RejectionGenerator.InternalGetRandomIntEI(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (ushort)CryptoGenerator.InternalGetRandomIntEI(minValue, maxValue),
        _ => (ushort)StandardGenerator.InternalGetRandomIntEI(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="int"/> on the range of
    /// [<see cref="int.MinValue"/>, <see cref="int.MaxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static int GetRandomInt(this Random random)
    {
      // If the generator is not null, return a random int on the full range.
      if (random != null)
        return random.InternalGetRandomInt();
      // If null, throw an error.
      throw new ArgumentNullException(nameof(random), "Random generator is null.");
    }

    /// <summary>
    /// A function for generating any <see cref="int"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    public static int GetRandomIntII(this Random random, int minValue, int maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return random.InternalGetRandomIntII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="int"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    public static int GetRandomIntEE(this Random random, int minValue, int maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return random.InternalGetRandomIntEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="int"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    public static int GetRandomIntIE(this Random random, int minValue, int maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      // If all checks clear, get a random integer.
      return random.InternalGetRandomIntIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="int"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    public static int GetRandomIntEI(this Random random, int minValue, int maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return random.InternalGetRandomIntEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="int"/> on the range of
    /// [<see cref="int.MinValue"/>, <see cref="int.MaxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static int GetRandomInt(this RandomNumberGenerator random)
    {
      // If the generator is not null, return a random int on the full range.
      if (random != null)
        return random.InternalGetRandomInt();
      // If null, throw an error.
      throw new ArgumentNullException(nameof(random), "Random generator is null.");
    }

    /// <summary>
    /// A function for generating any <see cref="int"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    public static int GetRandomIntII(this RandomNumberGenerator random, int minValue, int maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return random.InternalGetRandomIntII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="int"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    public static int GetRandomIntEE(this RandomNumberGenerator random, int minValue, int maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return random.InternalGetRandomIntEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="int"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    public static int GetRandomIntIE(this RandomNumberGenerator random, int minValue, int maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      // If all checks clear, get a random integer.
      return random.InternalGetRandomIntIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="int"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    public static int GetRandomIntEI(this RandomNumberGenerator random, int minValue, int maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      // If all checks clear, get a random integer.
      return random.InternalGetRandomIntEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="int"/> on the range of
    /// [<see cref="int.MinValue"/>, <see cref="int.MaxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static int GetRandomInt(RandomGenerators generator)
    {
      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomInt(),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomInt(),
        _ => StandardGenerator.InternalGetRandomInt(),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="int"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    public static int GetRandomIntII(RandomGenerators generator, int minValue, int maxValue)
    {
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomIntII(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomIntII(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomIntII(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="int"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    public static int GetRandomIntEE(RandomGenerators generator, int minValue, int maxValue)
    {
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomIntEE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomIntEE(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomIntEE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="int"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    public static int GetRandomIntIE(RandomGenerators generator, int minValue, int maxValue)
    {
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomIntIE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomIntIE(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomIntIE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="int"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    public static int GetRandomIntEI(RandomGenerators generator, int minValue, int maxValue)
    {
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomIntEI(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomIntEI(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomIntEI(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="uint"/> on the range of
    /// [<see cref="uint.MinValue"/>, <see cref="uint.MaxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static uint GetRandomUInt(this Random random)
    {
      // If the generator is not null, return a random uint on the full range.
      if (random != null)
        return random.InternalGetRandomUInt();
      // If null, throw an error.
      throw new ArgumentNullException(nameof(random), "Random generator is null.");
    }

    /// <summary>
    /// A function for generating any <see cref="uint"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    public static uint GetRandomUIntII(this Random random, uint minValue, uint maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random uinteger.
      return random.InternalGetRandomUIntII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="uint"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    public static uint GetRandomUIntEE(this Random random, uint minValue, uint maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random uinteger.
      return random.InternalGetRandomUIntEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="uint"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    public static uint GetRandomUIntIE(this Random random, uint minValue, uint maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      // If all checks clear, get a random uinteger.
      return random.InternalGetRandomUIntIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="uint"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    public static uint GetRandomUIntEI(this Random random, uint minValue, uint maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      // If all checks clear, get a random uinteger.
      return random.InternalGetRandomUIntEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="uint"/> on the range of
    /// [<see cref="uint.MinValue"/>, <see cref="uint.MaxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static uint GetRandomUInt(this RandomNumberGenerator random)
    {
      // If the generator is not null, return a random uint on the full range.
      if (random != null)
        return random.InternalGetRandomUInt();
      // If null, throw an error.
      throw new ArgumentNullException(nameof(random), "Random generator is null.");
    }

    /// <summary>
    /// A function for generating any <see cref="uint"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    public static uint GetRandomUIntII(this RandomNumberGenerator random, uint minValue,
                                       uint maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random uinteger.
      return random.InternalGetRandomUIntII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="uint"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    public static uint GetRandomUIntEE(this RandomNumberGenerator random, uint minValue,
                                       uint maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random uinteger.
      return random.InternalGetRandomUIntEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="uint"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    public static uint GetRandomUIntIE(this RandomNumberGenerator random, uint minValue,
                                       uint maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      // If all checks clear, get a random uinteger.
      return random.InternalGetRandomUIntIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="uint"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    public static uint GetRandomUIntEI(this RandomNumberGenerator random, uint minValue,
                                       uint maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      // If all checks clear, get a random uinteger.
      return random.InternalGetRandomUIntEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="uint"/> on the range of
    /// [<see cref="uint.MinValue"/>, <see cref="uint.MaxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static uint GetRandomUInt(RandomGenerators generator)
    {
      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomUInt(),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomUInt(),
        _ => StandardGenerator.InternalGetRandomUInt(),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="uint"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    public static uint GetRandomUIntII(RandomGenerators generator, uint minValue, uint maxValue)
    {
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomUIntII(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomUIntII(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomUIntII(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="uint"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    public static uint GetRandomUIntEE(RandomGenerators generator, uint minValue, uint maxValue)
    {
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomUIntEE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomUIntEE(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomUIntEE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="uint"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    public static uint GetRandomUIntIE(RandomGenerators generator, uint minValue, uint maxValue)
    {
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomUIntIE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomUIntIE(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomUIntIE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="uint"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    public static uint GetRandomUIntEI(RandomGenerators generator, uint minValue, uint maxValue)
    {
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomUIntEI(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomUIntEI(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomUIntEI(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="long"/> on the range of
    /// [<see cref="long.MinValue"/>, <see cref="long.MaxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static long GetRandomLong(this Random random)
    {
      // If the generator is not null, return a random long on the full range.
      if (random != null)
        return random.InternalGetRandomLong();
      // If null, throw an error.
      throw new ArgumentNullException(nameof(random), "Random generator is null.");
    }

    /// <summary>
    /// A function for generating any <see cref="long"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    public static long GetRandomLongII(this Random random, long minValue, long maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random long.
      return random.InternalGetRandomLongII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="long"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    public static long GetRandomLongEE(this Random random, long minValue, long maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random long.
      return random.InternalGetRandomLongEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="long"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    public static long GetRandomLongIE(this Random random, long minValue, long maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      // If all checks clear, get a random long.
      return random.InternalGetRandomLongIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="long"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    public static long GetRandomLongEI(this Random random, long minValue, long maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      // If all checks clear, get a random long.
      return random.InternalGetRandomLongEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="long"/> on the range of
    /// [<see cref="long.MinValue"/>, <see cref="long.MaxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static long GetRandomLong(this RandomNumberGenerator random)
    {
      // If the generator is not null, return a random long on the full range.
      if (random != null)
        return random.InternalGetRandomLong();
      // If null, throw an error.
      throw new ArgumentNullException(nameof(random), "Random generator is null.");
    }

    /// <summary>
    /// A function for generating any <see cref="long"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    public static long GetRandomLongII(this RandomNumberGenerator random, long minValue,
                                       long maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random long.
      return random.InternalGetRandomLongII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="long"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    public static long GetRandomLongEE(this RandomNumberGenerator random, long minValue,
                                       long maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random long.
      return random.InternalGetRandomLongEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="long"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    public static long GetRandomLongIE(this RandomNumberGenerator random, long minValue,
                                       long maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      // If all checks clear, get a random long.
      return random.InternalGetRandomLongIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="long"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    public static long GetRandomLongEI(this RandomNumberGenerator random, long minValue,
                                       long maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      // If all checks clear, get a random long.
      return random.InternalGetRandomLongEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="long"/> on the range of
    /// [<see cref="long.MinValue"/>, <see cref="long.MaxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static long GetRandomLong(RandomGenerators generator)
    {
      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomLong(),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomLong(),
        _ => StandardGenerator.InternalGetRandomLong(),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="long"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    public static long GetRandomLongII(RandomGenerators generator, long minValue, long maxValue)
    {
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomLongII(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomLongII(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomLongII(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="long"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    public static long GetRandomLongEE(RandomGenerators generator, long minValue, long maxValue)
    {
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomLongEE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomLongEE(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomLongEE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="long"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    public static long GetRandomLongIE(RandomGenerators generator, long minValue, long maxValue)
    {
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomLongIE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomLongIE(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomLongIE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="long"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    public static long GetRandomLongEI(RandomGenerators generator, long minValue, long maxValue)
    {
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomLongEI(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomLongEI(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomLongEI(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="ulong"/> on the range of
    /// [<see cref="ulong.MinValue"/>, <see cref="ulong.MaxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static ulong GetRandomULong(this Random random)
    {
      // If the generator is not null, return a random ulong on the full range.
      if (random != null)
        return random.InternalGetRandomULong();
      // If null, throw an error.
      throw new ArgumentNullException(nameof(random), "Random generator is null.");
    }

    /// <summary>
    /// A function for generating any <see cref="ulong"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    public static ulong GetRandomULongII(this Random random, ulong minValue, ulong maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random long.
      return random.InternalGetRandomULongII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="ulong"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    public static ulong GetRandomULongEE(this Random random, ulong minValue, ulong maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random long.
      return random.InternalGetRandomULongEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="ulong"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    public static ulong GetRandomULongIE(this Random random, ulong minValue, ulong maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      // If all checks clear, get a random long.
      return random.InternalGetRandomULongIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="ulong"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    public static ulong GetRandomULongEI(this Random random, ulong minValue, ulong maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      // If all checks clear, get a random long.
      return random.InternalGetRandomULongEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="ulong"/> on the range of
    /// [<see cref="ulong.MinValue"/>, <see cref="ulong.MaxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static ulong GetRandomULong(this RandomNumberGenerator random)
    {
      // If the generator is not null, return a random ulong on the full range.
      if (random != null)
        return random.InternalGetRandomULong();
      // If null, throw an error.
      throw new ArgumentNullException(nameof(random), "Random generator is null.");
    }

    /// <summary>
    /// A function for generating any <see cref="ulong"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    public static ulong GetRandomULongII(this RandomNumberGenerator random, ulong minValue,
                                         ulong maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random long.
      return random.InternalGetRandomULongII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="ulong"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    public static ulong GetRandomULongEE(this RandomNumberGenerator random, ulong minValue,
                                         ulong maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random long.
      return random.InternalGetRandomULongEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="ulong"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    public static ulong GetRandomULongIE(this RandomNumberGenerator random, ulong minValue,
                                         ulong maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      // If all checks clear, get a random long.
      return random.InternalGetRandomULongIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="ulong"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    public static ulong GetRandomULongEI(this RandomNumberGenerator random, ulong minValue,
                                         ulong maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      // If all checks clear, get a random long.
      return random.InternalGetRandomULongEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="ulong"/> on the range of
    /// [<see cref="ulong.MinValue"/>, <see cref="ulong.MaxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static ulong GetRandomULong(RandomGenerators generator)
    {
      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomULong(),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomULong(),
        _ => StandardGenerator.InternalGetRandomULong(),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="ulong"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    public static ulong GetRandomULongII(RandomGenerators generator, ulong minValue, ulong maxValue)
    {
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomULongII(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomULongII(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomULongII(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="ulong"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    public static ulong GetRandomULongEE(RandomGenerators generator, ulong minValue, ulong maxValue)
    {
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomULongEE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomULongEE(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomULongEE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="ulong"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    public static ulong GetRandomULongIE(RandomGenerators generator, ulong minValue, ulong maxValue)
    {
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomULongIE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomULongIE(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomULongIE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="ulong"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    public static ulong GetRandomULongEI(RandomGenerators generator, ulong minValue, ulong maxValue)
    {
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomULongEI(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomULongEI(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomULongEI(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// [<see cref="float.MinValue"/>, <see cref="float.MaxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static float GetRandomFloat(this Random random)
    {
      // If the generator is not null, return a random float on the full range.
      if (random != null)
        return (float)random.InternalGetRandomDouble();
      // If null, throw an error.
      throw new ArgumentNullException(nameof(random), "Random generator is null.");
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of [0.0, 1.0].
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloat01II(this Random random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return (float)random.InternalGetRandomDoubleII(0.0, 1.0);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of (0.0, 1.0).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloat01EE(this Random random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return (float)random.InternalGetRandomDoubleEE(0.0, 1.0);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of [0.0, 1.0).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloat01IE(this Random random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return (float)random.InternalGetRandomDoubleIE(0.0, 1.0);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of (0.0, 1.0].
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloat01EI(this Random random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return (float)random.InternalGetRandomDoubleEI(0.0, 1.0);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatII(this Random random, float minValue, float maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random float
      return (float)random.InternalGetRandomDoubleII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatEE(this Random random, float minValue, float maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random float
      return (float)random.InternalGetRandomDoubleEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatIE(this Random random, float minValue, float maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random float
      return (float)random.InternalGetRandomDoubleIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatEI(this Random random, float minValue, float maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random float
      return (float)random.InternalGetRandomDoubleEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatII(this Random random, float minValue, float maxValue,
                                         FloatingPointAdjustment rules)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random float
      return (float)random.InternalGetRandomDoubleII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatEE(this Random random, float minValue, float maxValue,
                                         FloatingPointAdjustment rules)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random float
      return (float)random.InternalGetRandomDoubleEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatIE(this Random random, float minValue, float maxValue,
                                         FloatingPointAdjustment rules)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random float
      return (float)random.InternalGetRandomDoubleIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatEI(this Random random, float minValue, float maxValue,
                                         FloatingPointAdjustment rules)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random float
      return (float)random.InternalGetRandomDoubleEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// [<see cref="float.MinValue"/>, <see cref="float.MaxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static float GetRandomFloat(this RandomNumberGenerator random)
    {
      // If the generator is not null, return a random float on the full range.
      if (random != null)
        return (float)random.InternalGetRandomDouble();
      // If null, throw an error.
      throw new ArgumentNullException(nameof(random), "Random generator is null.");
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of [0.0, 1.0].
    /// This variant is for cryptographic randon generators.
    /// This variant is for cryptographic randon generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloat01II(this RandomNumberGenerator random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return (float)random.InternalGetRandomDoubleII(0.0, 1.0);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of (0.0, 1.0).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloat01EE(this RandomNumberGenerator random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return (float)random.InternalGetRandomDoubleEE(0.0, 1.0);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of [0.0, 1.0).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloat01IE(this RandomNumberGenerator random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return (float)random.InternalGetRandomDoubleIE(0.0, 1.0);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of (0.0, 1.0].
    /// This variant is for cryptographic randon generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloat01EI(this RandomNumberGenerator random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return (float)random.InternalGetRandomDoubleEI(0.0, 1.0);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatII(this RandomNumberGenerator random, float minValue,
                                         float maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random float
      return (float)random.InternalGetRandomDoubleII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatEE(this RandomNumberGenerator random, float minValue,
                                         float maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random float
      return (float)random.InternalGetRandomDoubleEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatIE(this RandomNumberGenerator random, float minValue,
                                         float maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random float
      return (float)random.InternalGetRandomDoubleIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatEI(this RandomNumberGenerator random, float minValue,
                                         float maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random float
      return (float)random.InternalGetRandomDoubleEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatII(this RandomNumberGenerator random, float minValue,
                                         float maxValue, FloatingPointAdjustment rules)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random float
      return (float)random.InternalGetRandomDoubleII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatEE(this RandomNumberGenerator random, float minValue,
                                         float maxValue, FloatingPointAdjustment rules)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random float
      return (float)random.InternalGetRandomDoubleEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatIE(this RandomNumberGenerator random, float minValue,
                                         float maxValue, FloatingPointAdjustment rules)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random float
      return (float)random.InternalGetRandomDoubleIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatEI(this RandomNumberGenerator random, float minValue,
                                         float maxValue, FloatingPointAdjustment rules)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random float
      return (float)random.InternalGetRandomDoubleEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// [<see cref="float.MinValue"/>, <see cref="float.MaxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static float GetRandomFloat(this RandomGenerators generator)
    {
      return generator switch
      {
        RandomGenerators.RejectionRandom => (float)RejectionGenerator.GetRandomDouble(),
        RandomGenerators.CryptoServiceProvider => (float)CryptoGenerator.GetRandomDouble(),
        _ => (float)StandardGenerator.GetRandomDouble(),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of [0.0, 1.0].
    /// This variant is for selecting one of the global number generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloat01II(this RandomGenerators generator)
    {
      return generator switch
      {
        RandomGenerators.RejectionRandom => (float)RejectionGenerator.InternalGetRandomDoubleII(0.0, 1.0),
        RandomGenerators.CryptoServiceProvider => (float)CryptoGenerator.InternalGetRandomDoubleII(0.0, 1.0),
        _ => (float)StandardGenerator.InternalGetRandomDoubleII(0.0, 1.0),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of (0.0, 1.0).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloat01EE(this RandomGenerators generator)
    {
      return generator switch
      {
        RandomGenerators.RejectionRandom => (float)RejectionGenerator.InternalGetRandomDoubleEE(0.0, 1.0),
        RandomGenerators.CryptoServiceProvider => (float)CryptoGenerator.InternalGetRandomDoubleEE(0.0, 1.0),
        _ => (float)StandardGenerator.InternalGetRandomDoubleEE(0.0, 1.0),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of [0.0, 1.0).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloat01IE(this RandomGenerators generator)
    {
      return generator switch
      {
        RandomGenerators.RejectionRandom => (float)RejectionGenerator.InternalGetRandomDoubleIE(0.0, 1.0),
        RandomGenerators.CryptoServiceProvider => (float)CryptoGenerator.InternalGetRandomDoubleIE(0.0, 1.0),
        _ => (float)StandardGenerator.InternalGetRandomDoubleIE(0.0, 1.0),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of (0.0, 1.0].
    /// This variant is for selecting one of the global number generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloat01EI(this RandomGenerators generator)
    {
      return generator switch
      {
        RandomGenerators.RejectionRandom => (float)RejectionGenerator.InternalGetRandomDoubleEI(0.0, 1.0),
        RandomGenerators.CryptoServiceProvider => (float)CryptoGenerator.InternalGetRandomDoubleEI(0.0, 1.0),
        _ => (float)StandardGenerator.InternalGetRandomDoubleEI(0.0, 1.0),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatII(this RandomGenerators generator, float minValue,
                                         float maxValue)
    {
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      return generator switch
      {
        RandomGenerators.RejectionRandom => (float)RejectionGenerator.InternalGetRandomDoubleII(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (float)CryptoGenerator.InternalGetRandomDoubleII(minValue, maxValue),
        _ => (float)StandardGenerator.InternalGetRandomDoubleII(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatEE(this RandomGenerators generator, float minValue,
                                         float maxValue)
    {
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      return generator switch
      {
        RandomGenerators.RejectionRandom => (float)RejectionGenerator.InternalGetRandomDoubleEE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (float)CryptoGenerator.InternalGetRandomDoubleEE(minValue, maxValue),
        _ => (float)StandardGenerator.InternalGetRandomDoubleEE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatIE(this RandomGenerators generator, float minValue,
                                         float maxValue)
    {
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      return generator switch
      {
        RandomGenerators.RejectionRandom => (float)RejectionGenerator.InternalGetRandomDoubleIE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (float)CryptoGenerator.InternalGetRandomDoubleIE(minValue, maxValue),
        _ => (float)StandardGenerator.InternalGetRandomDoubleIE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatEI(this RandomGenerators generator, float minValue,
                                         float maxValue)
    {
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      return generator switch
      {
        RandomGenerators.RejectionRandom => (float)RejectionGenerator.InternalGetRandomDoubleEI(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (float)CryptoGenerator.InternalGetRandomDoubleEI(minValue, maxValue),
        _ => (float)StandardGenerator.InternalGetRandomDoubleEI(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatII(this RandomGenerators generator, float minValue,
                                         float maxValue, FloatingPointAdjustment rules)
    {
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      return generator switch
      {
        RandomGenerators.RejectionRandom => (float)RejectionGenerator.InternalGetRandomDoubleII(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (float)CryptoGenerator.InternalGetRandomDoubleII(minValue, maxValue),
        _ => (float)StandardGenerator.InternalGetRandomDoubleII(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatEE(this RandomGenerators generator, float minValue,
                                         float maxValue, FloatingPointAdjustment rules)
    {
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      return generator switch
      {
        RandomGenerators.RejectionRandom => (float)RejectionGenerator.InternalGetRandomDoubleEE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (float)CryptoGenerator.InternalGetRandomDoubleEE(minValue, maxValue),
        _ => (float)StandardGenerator.InternalGetRandomDoubleEE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatIE(this RandomGenerators generator, float minValue,
                                         float maxValue, FloatingPointAdjustment rules)
    {
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      return generator switch
      {
        RandomGenerators.RejectionRandom => (float)RejectionGenerator.InternalGetRandomDoubleIE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (float)CryptoGenerator.InternalGetRandomDoubleIE(minValue, maxValue),
        _ => (float)StandardGenerator.InternalGetRandomDoubleIE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="float"/> on the range of
    ///  (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="float"/>.</returns>
    public static float GetRandomFloatEI(this RandomGenerators generator, float minValue,
                                         float maxValue, FloatingPointAdjustment rules)
    {
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      return generator switch
      {
        RandomGenerators.RejectionRandom => (float)RejectionGenerator.InternalGetRandomDoubleEI(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => (float)CryptoGenerator.InternalGetRandomDoubleEI(minValue, maxValue),
        _ => (float)StandardGenerator.InternalGetRandomDoubleEI(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// [<see cref="double.MinValue"/>, <see cref="double.MaxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static double GetRandomDouble(this Random random)
    {
      // If the generator is not null, return a random double on the full range.
      if (random != null)
        return random.InternalGetRandomDouble();
      // If null, throw an error.
      throw new ArgumentNullException(nameof(random), "Random generator is null.");
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of [0.0, 1.0].
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDouble01II(this Random random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return random.InternalGetRandomDoubleII(0.0, 1.0);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of (0.0, 1.0).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDouble01EE(this Random random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return random.InternalGetRandomDoubleEE(0.0, 1.0);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of [0.0, 1.0).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDouble01IE(this Random random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return random.InternalGetRandomDoubleIE(0.0, 1.0);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of (0.0, 1.0].
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDouble01EI(this Random random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return random.InternalGetRandomDoubleEI(0.0, 1.0);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleII(this Random random, double minValue, double maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random double
      return random.InternalGetRandomDoubleII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleEE(this Random random, double minValue, double maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random double
      return random.InternalGetRandomDoubleEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleIE(this Random random, double minValue, double maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random double
      return random.InternalGetRandomDoubleIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleEI(this Random random, double minValue, double maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random double
      return random.InternalGetRandomDoubleEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleII(this Random random, double minValue, double maxValue,
                                           FloatingPointAdjustment rules)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random double
      return random.InternalGetRandomDoubleII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleEE(this Random random, double minValue, double maxValue,
                                           FloatingPointAdjustment rules)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random double
      return random.InternalGetRandomDoubleEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleIE(this Random random, double minValue, double maxValue,
                                           FloatingPointAdjustment rules)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random double
      return random.InternalGetRandomDoubleIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleEI(this Random random, double minValue, double maxValue,
                                           FloatingPointAdjustment rules)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random double
      return random.InternalGetRandomDoubleEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// [<see cref="double.MinValue"/>, <see cref="double.MaxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static double GetRandomDouble(this RandomNumberGenerator random)
    {
      // If the generator is not null, return a random double on the full range.
      if (random != null)
        return random.InternalGetRandomDouble();
      // If null, throw an error.
      throw new ArgumentNullException(nameof(random), "Random generator is null.");
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of [0.0, 1.0].
    /// This variant is for cryptographic randon generators.
    /// This variant is for cryptographic randon generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDouble01II(this RandomNumberGenerator random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return random.InternalGetRandomDoubleII(0.0, 1.0);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of (0.0, 1.0).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDouble01EE(this RandomNumberGenerator random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return random.InternalGetRandomDoubleEE(0.0, 1.0);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of [0.0, 1.0).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDouble01IE(this RandomNumberGenerator random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return random.InternalGetRandomDoubleIE(0.0, 1.0);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of (0.0, 1.0].
    /// This variant is for cryptographic randon generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDouble01EI(this RandomNumberGenerator random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return random.InternalGetRandomDoubleEI(0.0, 1.0);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleII(this RandomNumberGenerator random, double minValue,
                                           double maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random double
      return random.InternalGetRandomDoubleII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleEE(this RandomNumberGenerator random, double minValue,
                                           double maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random double
      return random.InternalGetRandomDoubleEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleIE(this RandomNumberGenerator random, double minValue,
                                           double maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random double
      return random.InternalGetRandomDoubleIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleEI(this RandomNumberGenerator random, double minValue,
                                           double maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random double
      return random.InternalGetRandomDoubleEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleII(this RandomNumberGenerator random, double minValue,
                                           double maxValue, FloatingPointAdjustment rules)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random double
      return random.InternalGetRandomDoubleII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleEE(this RandomNumberGenerator random, double minValue,
                                           double maxValue, FloatingPointAdjustment rules)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random double
      return random.InternalGetRandomDoubleEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleIE(this RandomNumberGenerator random, double minValue,
                                           double maxValue, FloatingPointAdjustment rules)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random double
      return random.InternalGetRandomDoubleIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleEI(this RandomNumberGenerator random, double minValue,
                                           double maxValue, FloatingPointAdjustment rules)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      // If all checks clear, get a random double
      return random.InternalGetRandomDoubleEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// [<see cref="double.MinValue"/>, <see cref="double.MaxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static double GetRandomDouble(this RandomGenerators generator)
    {
      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.GetRandomDouble(),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.GetRandomDouble(),
        _ => StandardGenerator.GetRandomDouble(),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of [0.0, 1.0].
    /// This variant is for selecting one of the global number generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDouble01II(this RandomGenerators generator)
    {
      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomDoubleII(0.0, 1.0),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomDoubleII(0.0, 1.0),
        _ => StandardGenerator.InternalGetRandomDoubleII(0.0, 1.0),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of (0.0, 1.0).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDouble01EE(this RandomGenerators generator)
    {
      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomDoubleEE(0.0, 1.0),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomDoubleEE(0.0, 1.0),
        _ => StandardGenerator.InternalGetRandomDoubleEE(0.0, 1.0),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of [0.0, 1.0).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDouble01IE(this RandomGenerators generator)
    {
      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomDoubleIE(0.0, 1.0),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomDoubleIE(0.0, 1.0),
        _ => StandardGenerator.InternalGetRandomDoubleIE(0.0, 1.0),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of (0.0, 1.0].
    /// This variant is for selecting one of the global number generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDouble01EI(this RandomGenerators generator)
    {
      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomDoubleEI(0.0, 1.0),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomDoubleEI(0.0, 1.0),
        _ => StandardGenerator.InternalGetRandomDoubleEI(0.0, 1.0),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleII(this RandomGenerators generator, double minValue,
                                           double maxValue)
    {
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomDoubleII(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomDoubleII(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomDoubleII(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleEE(this RandomGenerators generator, double minValue,
                                           double maxValue)
    {
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomDoubleEE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomDoubleEE(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomDoubleEE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleIE(this RandomGenerators generator, double minValue,
                                           double maxValue)
    {
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomDoubleIE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomDoubleIE(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomDoubleIE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleEI(this RandomGenerators generator, double minValue,
                                           double maxValue)
    {
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      minValue = FloatPointRules.AdjustFloatingPoint(minValue);
      maxValue = FloatPointRules.AdjustFloatingPoint(maxValue);

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomDoubleEI(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomDoubleEI(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomDoubleEI(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleII(this RandomGenerators generator, double minValue,
                                           double maxValue, FloatingPointAdjustment rules)
    {
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomDoubleII(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomDoubleII(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomDoubleII(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleEE(this RandomGenerators generator, double minValue,
                                           double maxValue, FloatingPointAdjustment rules)
    {
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomDoubleEE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomDoubleEE(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomDoubleEE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleIE(this RandomGenerators generator, double minValue,
                                           double maxValue, FloatingPointAdjustment rules)
    {
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomDoubleIE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomDoubleIE(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomDoubleIE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="double"/> on the range of
    ///  (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <param name="rules">The rules for adjusting irregular floating-point values.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    public static double GetRandomDoubleEI(this RandomGenerators generator, double minValue,
                                           double maxValue, FloatingPointAdjustment rules)
    {
      // Throw an error if the adjustment rules are null.
      if (rules == null)
        throw new ArgumentNullException(nameof(rules), "Passed-in adjustment rules were null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      minValue = rules.AdjustFloatingPoint(minValue);
      maxValue = rules.AdjustFloatingPoint(maxValue);

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomDoubleEI(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomDoubleEI(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomDoubleEI(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of
    /// [<see cref="decimal.MinValue"/>, <see cref="decimal.MaxValue"/>].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static decimal GetRandomDecimal(this Random random)
    {
      // If the generator is not null, return a random decimal on the full range.
      if (random != null)
        return random.InternalGetRandomDecimal();
      // If null, throw an error.
      throw new ArgumentNullException(nameof(random), "Random generator is null.");
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of [0.0, 1.0].
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimal01II(this Random random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return random.InternalGetRandomDecimalII(0.0m, 1.0m);
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of (0.0, 1.0).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimal01EE(this Random random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return random.InternalGetRandomDecimalEE(0.0m, 1.0m);
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of [0.0, 1.0).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimal01IE(this Random random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return random.InternalGetRandomDecimalIE(0.0m, 1.0m);
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of (0.0, 1.0].
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimal01EI(this Random random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return random.InternalGetRandomDecimalEI(0.0m, 1.0m);
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimalII(this Random random, decimal minValue, decimal maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;


      // If all checks clear, get a random decimal
      return random.InternalGetRandomDecimalII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimalEE(this Random random, decimal minValue, decimal maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random decimal
      return random.InternalGetRandomDecimalEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimalIE(this Random random, decimal minValue, decimal maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      // If all checks clear, get a random decimal
      return random.InternalGetRandomDecimalIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimalEI(this Random random, decimal minValue, decimal maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      // If all checks clear, get a random decimal
      return random.InternalGetRandomDecimalEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of
    /// [<see cref="decimal.MinValue"/>, <see cref="decimal.MaxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    /// <exception cref="ArgumentNullException">The random generator is null.</exception>
    public static decimal GetRandomDecimal(this RandomNumberGenerator random)
    {
      // If the generator is not null, return a random decimal on the full range.
      if (random != null)
        return random.InternalGetRandomDecimal();
      // If null, throw an error.
      throw new ArgumentNullException(nameof(random), "Random generator is null.");
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of [0.0, 1.0].
    /// This variant is for cryptographic randon generators.
    /// This variant is for cryptographic randon generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimal01II(this RandomNumberGenerator random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return random.InternalGetRandomDecimalII(0.0m, 1.0m);
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of (0.0, 1.0).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimal01EE(this RandomNumberGenerator random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return random.InternalGetRandomDecimalEE(0.0m, 1.0m);
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of [0.0, 1.0).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimal01IE(this RandomNumberGenerator random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return random.InternalGetRandomDecimalIE(0.0m, 1.0m);
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of (0.0, 1.0].
    /// This variant is for cryptographic randon generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimal01EI(this RandomNumberGenerator random)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");

      return random.InternalGetRandomDecimalEI(0.0m, 1.0m);
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimalII(this RandomNumberGenerator random, decimal minValue,
                                             decimal maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random decimal
      return random.InternalGetRandomDecimalII(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimalEE(this RandomNumberGenerator random, decimal minValue,
                                             decimal maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      // If all checks clear, get a random decimal
      return random.InternalGetRandomDecimalEE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimalIE(this RandomNumberGenerator random, decimal minValue,
                                             decimal maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      // If all checks clear, get a random decimal
      return random.InternalGetRandomDecimalIE(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimalEI(this RandomNumberGenerator random, decimal minValue,
                                             decimal maxValue)
    {
      // Throw an error if the generator is null.
      if (random == null)
        throw new ArgumentNullException(nameof(random), "Passed-in Random generator was null.");
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      // If all checks clear, get a random decimal
      return random.InternalGetRandomDecimalEI(minValue, maxValue);
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of [0.0, 1.0].
    /// This variant is for selecting one of the global number generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimal01II(this RandomGenerators generator)
    {
      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomDecimalII(0.0m, 1.0m),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomDecimalII(0.0m, 1.0m),
        _ => StandardGenerator.InternalGetRandomDecimalII(0.0m, 1.0m),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of (0.0, 1.0).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimal01EE(this RandomGenerators generator)
    {
      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomDecimalEE(0.0m, 1.0m),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomDecimalEE(0.0m, 1.0m),
        _ => StandardGenerator.InternalGetRandomDecimalEE(0.0m, 1.0m),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of [0.0, 1.0).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimal01IE(this RandomGenerators generator)
    {
      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomDecimalIE(0.0m, 1.0m),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomDecimalIE(0.0m, 1.0m),
        _ => StandardGenerator.InternalGetRandomDecimalIE(0.0m, 1.0m),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of (0.0, 1.0].
    /// This variant is for selecting one of the global number generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimal01EI(this RandomGenerators generator)
    {
      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomDecimalEI(0.0m, 1.0m),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomDecimalEI(0.0m, 1.0m),
        _ => StandardGenerator.InternalGetRandomDecimalEI(0.0m, 1.0m),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimalII(this RandomGenerators generator, decimal minValue,
                                             decimal maxValue)
    {
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomDecimalII(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomDecimalII(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomDecimalII(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimalEE(this RandomGenerators generator, decimal minValue,
                                             decimal maxValue)
    {
      // Throw an error if the min is greater than the max.
      if (minValue > maxValue)
        throw new MinMaxException(minValue, maxValue, true);
      // Return min immediately if the range is equal.
      if (minValue == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomDecimalEE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomDecimalEE(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomDecimalEE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for selecting one of the global number generators.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimalIE(this RandomGenerators generator, decimal minValue,
                                             decimal maxValue)
    {
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue == maxValue - 1)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomDecimalIE(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomDecimalIE(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomDecimalIE(minValue, maxValue),
      };
    }

    /// <summary>
    /// A function for generating any <see cref="decimal"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for selecting one of the global number generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    public static decimal GetRandomDecimalEI(this RandomGenerators generator, decimal minValue,
                                             decimal maxValue)
    {
      // Throw an error if the min is greater than or equal to the max.
      if (minValue >= maxValue)
        throw new MinMaxException(minValue, maxValue, false);
      // Return min immediately if the range, after adjustment, is equal.
      if (minValue + 1 == maxValue)
        return minValue;

      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomDecimalEI(minValue, maxValue),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomDecimalEI(minValue, maxValue),
        _ => StandardGenerator.InternalGetRandomDecimalEI(minValue, maxValue),
      };
    }

    /// <summary>
    /// An internal function for generating random values, and storing them into a byte array.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="bytes">The array to store the values into. This must be sized to the number of
    /// bytes to generate.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void InternalGetRandomBytes(this Random random, byte[] bytes)
    {
      random.NextBytes(bytes); // Get bytes as normal.
    }

    /// <summary>
    /// An internal function for generating random values, and storing them into a byte array.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="count">The number of bytes to generate. This must be greater than 0.</param>
    /// <param name="bytes">The array of generated bytes.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void InternalGetRandomBytes(this Random random, int count, out byte[] bytes)
    {
      // Create the correctly sized byte array and return the bytes.
      bytes = new byte[count];
      random.NextBytes(bytes);
    }

    /// <summary>
    /// An internal function for generating random values, and storing them into a byte array.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="count">The number of bytes to generate. This must be greater than 0.</param>
    /// <returns>Returns the generated byte array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte[] InternalGetRandomBytes(this Random random, int count)
    {
      // Create the correctly sized byte array and return the bytes.
      byte[] bytes = new byte[count];
      random.NextBytes(bytes);
      return bytes;
    }

    /// <summary>
    /// An internal function for generating random values, and storing them into a byte array.
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="bytes">The array to store the values into. This must be sized to the number of
    /// bytes to generate.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void InternalGetRandomBytes(this RandomNumberGenerator random, byte[] bytes)
    {
      random.GetBytes(bytes); // Get bytes as normal.
    }

    /// <summary>
    /// An internal function for generating random values, and storing them into a byte array.
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="count">The number of bytes to generate. This must be greater than 0.</param>
    /// <param name="bytes">The array of generated bytes.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void InternalGetRandomBytes(this RandomNumberGenerator random, int count,
                                               out byte[] bytes)
    {
      // Create the correctly sized byte array and return the bytes.
      bytes = new byte[count];
      random.GetBytes(bytes);
    }

    /// <summary>
    /// An internal function for generating random values, and storing them into a byte array.
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="count">The number of bytes to generate. This must be greater than 0.</param>
    /// <returns>Returns the generated byte array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte[] InternalGetRandomBytes(this RandomNumberGenerator random, int count)
    {
      // Create the correctly sized byte array and return the bytes.
      random.InternalGetRandomBytes(count, out byte[] bytes);
      return bytes;
    }

    /// <summary>
    /// A function for generating random values, and storing them into a byte array.
    /// This variant is for selecting one of the global number generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="bytes">The array to store the values into. This must be sized to the number of
    /// bytes to generate.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void InternalGetRandomBytes(RandomGenerators generator, byte[] bytes)
    {
      // Use the appropriate global generator, based on the enum.
      switch (generator)
      {
        case RandomGenerators.RejectionRandom:
          RejectionGenerator.InternalGetRandomBytes(bytes);
          break;
        case RandomGenerators.CryptoServiceProvider:
          CryptoGenerator.InternalGetRandomBytes(bytes);
          break;
        default:
          StandardGenerator.InternalGetRandomBytes(bytes);
          break;
      }
    }

    /// <summary>
    /// A function for generating random values, and storing them into a byte array.
    /// This variant is for selecting one of the global number generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="count">The number of bytes to generate. This must be greater than 0.</param>
    /// <param name="bytes">The array of generated bytes.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void InternalGetRandomBytes(RandomGenerators generator, int count,
                                               out byte[] bytes)
    {
      // Use the appropriate global generator, based on the enum.
      switch (generator)
      {
        case RandomGenerators.RejectionRandom:
          RejectionGenerator.InternalGetRandomBytes(count, out bytes);
          break;
        case RandomGenerators.CryptoServiceProvider:
          CryptoGenerator.InternalGetRandomBytes(count, out bytes);
          break;
        default:
          StandardGenerator.InternalGetRandomBytes(count, out bytes);
          break;
      }
    }

    /// <summary>
    /// A function for generating random values, and storing them into a byte array.
    /// This variant is for selecting one of the global number generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="generator">The global random generator to use.</param>
    /// <param name="count">The number of bytes to generate. This must be greater than 0.</param>
    /// <returns>Returns the generated byte array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte[] InternalGetRandomBytes(RandomGenerators generator, int count)
    {
      // Use the appropriate global generator, based on the enum.
      return generator switch
      {
        RandomGenerators.RejectionRandom => RejectionGenerator.InternalGetRandomBytes(count),
        RandomGenerators.CryptoServiceProvider => CryptoGenerator.InternalGetRandomBytes(count),
        _ => StandardGenerator.InternalGetRandomBytes(count),
      };
    }

    /// <summary>
    /// An internal function for generating any <see cref="sbyte"/> on the range of
    /// [<see cref="sbyte.MinValue"/>, <see cref="sbyte.MaxValue"/>].
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="sbyte"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static sbyte InternalGetRandomSByte(this Random random)
    {
      // Generate a random byte, and cast it to an sbyte. Values past 127 loop to negative.
      return (sbyte)random.InternalGetRandomBytes(sizeof(byte))[0];
    }

    /// <summary>
    /// An internal function for generating any <see cref="sbyte"/> on the range of
    /// [<see cref="sbyte.MinValue"/>, <see cref="sbyte.MaxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="sbyte"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static sbyte InternalGetRandomSByte(this RandomNumberGenerator random)
    {
      // Generate a random byte, and cast it to an sbyte. Values past 127 loop to negative.
      return (sbyte)random.InternalGetRandomBytes(sizeof(byte))[0];
    }

    /// <summary>
    /// An internal function for generating any <see cref="byte"/> on the range of
    /// [<see cref="byte.MinValue"/>, <see cref="byte.MaxValue"/>].
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="byte"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte InternalGetRandomByte(this Random random)
    {
      // Generate a random byte.
      return random.InternalGetRandomBytes(sizeof(byte))[0];
    }

    /// <summary>
    /// An internal function for generating any <see cref="byte"/> on the range of
    /// [<see cref="byte.MinValue"/>, <see cref="byte.MaxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="byte"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte InternalGetRandomByte(this RandomNumberGenerator random)
    {
      // Generate a random byte.
      return random.InternalGetRandomBytes(sizeof(byte))[0];
    }

    /// <summary>
    /// An internal function for generating any <see cref="short"/> on the range of
    /// [<see cref="short.MinValue"/>, <see cref="short.MaxValue"/>].
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="short"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static short InternalGetRandomShort(this Random random)
    {
      // Generate a random byte array the size of an short, and convert it.
      random.InternalGetRandomBytes(sizeof(short), out byte[] bytes);
      return BitConverter.ToInt16(bytes, 0);
    }

    /// <summary>
    /// An internal function for generating any <see cref="short"/> on the range of
    /// [<see cref="short.MinValue"/>, <see cref="short.MaxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="short"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static short InternalGetRandomShort(this RandomNumberGenerator random)
    {
      // Generate a random byte array the size of an short, and convert it.
      random.InternalGetRandomBytes(sizeof(short), out byte[] bytes);
      return BitConverter.ToInt16(bytes, 0);
    }

    /// <summary>
    /// An internal function for generating any <see cref="ushort"/> on the range of
    /// [<see cref="ushort.MinValue"/>, <see cref="ushort.MaxValue"/>].
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="ushort"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ushort InternalGetRandomUShort(this Random random)
    {
      // Generate a random byte array the size of a ushort, and convert it.
      random.InternalGetRandomBytes(sizeof(ushort), out byte[] bytes);
      return BitConverter.ToUInt16(bytes, 0);
    }

    /// <summary>
    /// An internal function for generating any <see cref="ushort"/> on the range of
    /// [<see cref="ushort.MinValue"/>, <see cref="ushort.MaxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="ushort"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ushort InternalGetRandomUShort(this RandomNumberGenerator random)
    {
      // Generate a random byte array the size of a ushort, and convert it.
      random.InternalGetRandomBytes(sizeof(ushort), out byte[] bytes);
      return BitConverter.ToUInt16(bytes, 0);
    }

    /// <summary>
    /// An internal function for generating any <see cref="int"/> on the range of
    /// [<see cref="int.MinValue"/>, <see cref="int.MaxValue"/>].
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int InternalGetRandomInt(this Random random)
    {
      // Generate a random byte array the size of an int, and convert it.
      random.InternalGetRandomBytes(sizeof(int), out byte[] bytes);
      return BitConverter.ToInt32(bytes, 0);
    }

    /// <summary>
    /// An internal function for generating any <see cref="int"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int InternalGetRandomIntII(this Random random, int minValue, int maxValue)
    {
      // Get the difference between the max and min. The max difference is the max uint value.
      uint difference = (uint)(maxValue - minValue);
      // If the difference is for the full inclusive int range, return using the byte method.
      if (difference == uint.MaxValue)
        return random.InternalGetRandomInt();

      difference += 1; // Add an extra point, as any Next function is inherently [I, E).

      // When maxValue is an int's max value, special care needs to be used by shifting down by
      // 1, then adding one after generating a value.
      if (maxValue == int.MaxValue)
        return random.Next(minValue - 1, (int)(minValue - 1 + difference)) + 1;

      // Otherwise, simply return on the given range. Add 1 due to [I, E) returns on Next.
      return random.Next(minValue, maxValue + 1);
    }

    /// <summary>
    /// An internal function for generating any <see cref="int"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int InternalGetRandomIntEE(this Random random, int minValue, int maxValue)
    {
      // Shift minValue by 1 due to exclusivity and return on the new range.
      return random.InternalGetRandomIntIE(minValue + 1, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="int"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int InternalGetRandomIntIE(this Random random, int minValue, int maxValue)
    {
      // Simply return on the given range.
      return random.Next(minValue, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="int"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int InternalGetRandomIntEI(this Random random, int minValue, int maxValue)
    {
      return random.InternalGetRandomIntII(minValue + 1, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="int"/> on the range of
    /// [<see cref="int.MinValue"/>, <see cref="int.MaxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int InternalGetRandomInt(this RandomNumberGenerator random)
    {
      // Generate a random byte array the size of an int, and convert it.
      random.InternalGetRandomBytes(sizeof(int), out byte[] bytes);
      return BitConverter.ToInt32(bytes, 0);
    }

    /// <summary>
    /// An internal function for generating any <see cref="int"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int InternalGetRandomIntII(this RandomNumberGenerator random, int minValue,
                                              int maxValue)
    {
      // Get the difference between the min and max values.
      // NOTE: The original implementation does not cast either minValue or maxValue to a uint,
      // resulting in generating numbers within (min, min + ABS(Max) - ABS(min)) due to overflow.
      uint difference = (uint)maxValue - (uint)minValue;

      // If the difference is the full int range, merely return using the byte method.
      if (difference == uint.MaxValue)
        return random.InternalGetRandomInt();

      difference += 1; // Increase difference by one, due to the new inclusivity of the maxValue.

      byte[] bytes = new byte[sizeof(uint)]; // Create a byte array.

      // Get an allowed maximum based on our range.
      uint maxAllowedRandomValue = uint.MaxValue - (uint.MaxValue % difference);

      uint value; // The randomly generated value.

      // Generating with cryptographic safety is slow, as it requires this while loop.
      // Larger ranges are faster. The value must be less than the allowed maximum.
      do
      {
        // Generate a random uint, to keep positive.
        random.InternalGetRandomBytes(bytes);
        value = BitConverter.ToUInt32(bytes, 0);
      }
      while (value >= maxAllowedRandomValue);

      // Modulo the value to fix the range and return it.
      return (int)(minValue + (value % difference));
    }

    /// <summary>
    /// An internal function for generating any <see cref="int"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int InternalGetRandomIntEE(this RandomNumberGenerator random, int minValue,
                                              int maxValue)
    {
      return random.InternalGetRandomIntIE(minValue + 1, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="int"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int InternalGetRandomIntIE(this RandomNumberGenerator random, int minValue,
                                              int maxValue)
    {
      uint value; // The randomly generated value.

      // Get the difference between the min and max values.
      // NOTE: The original implementation does not cast either minValue or maxValue to a uint,
      // resulting in generating numbers within (min, min + ABS(Max) - ABS(min)) due to overflow.
      uint difference = (uint)maxValue - (uint)minValue;
      byte[] bytes = new byte[sizeof(uint)]; // Create a byte array.

      // Get an allowed maximum based on our range.
      uint maxAllowedRandomValue = uint.MaxValue - (uint.MaxValue % difference);

      // Generating with cryptographic safety is slow, as it requires this while loop.
      // Larger ranges are faster. The value must be less than the allowed maximum.
      do
      {
        // Generate a random uint, to keep positive.
        random.InternalGetRandomBytes(bytes);
        value = BitConverter.ToUInt32(bytes, 0);
      }
      while (value >= maxAllowedRandomValue);

      // Modulo the value to fix the range and return it.
      return (int)(minValue + (value % difference));
    }

    /// <summary>
    /// An internal function for generating any <see cref="int"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="int"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int InternalGetRandomIntEI(this RandomNumberGenerator random, int minValue,
                                              int maxValue)
    {
      return random.InternalGetRandomIntII(minValue + 1, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="uint"/> on the range of
    /// [<see cref="uint.MinValue"/>, <see cref="uint.MaxValue"/>].
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint InternalGetRandomUInt(this Random random)
    {
      // Generate a random byte array the size of an uint, and convert it.
      random.InternalGetRandomBytes(sizeof(uint), out byte[] bytes);
      return BitConverter.ToUInt32(bytes, 0);
    }

    /// <summary>
    /// An internal function for generating any <see cref="uint"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint InternalGetRandomUIntII(this Random random, uint minValue, uint maxValue)
    {
      // Get the difference between the max and min. The max difference is the max uint value.
      uint difference = maxValue - minValue;
      // If the difference is for the full inclusive uint range, return using the byte method.
      if (difference == uint.MaxValue)
        return random.InternalGetRandomUInt();

      difference += 1; // Add an extra point, as any Next function is inherently [I, E).

      // When the difference is less than an int's max value, we can perform a standard Next.
      if (difference < int.MaxValue)
        return (uint)random.Next(0, (int)difference) + minValue;

      // Otherwise, special care is needed to handle wrapping around the normally negative values.
      // Get the wrapped values, and use it to create a wrap after the min value.
      long wrap = (long)int.MaxValue - difference;
      return (uint)(random.Next((int)wrap, int.MaxValue) - wrap + minValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="uint"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint InternalGetRandomUIntEE(this Random random, uint minValue, uint maxValue)
    {
      // Shift minValue and maxValue by 1 due to exclusivity and return on the new range.
      return random.InternalGetRandomUIntIE(minValue + 1, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="uint"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint InternalGetRandomUIntIE(this Random random, uint minValue, uint maxValue)
    {
      // Get the difference between the max and min. The max difference is the max uint value.
      uint difference = maxValue - minValue;

      // When the difference is less than an int's max value, we can perform a standard Next.
      if (difference < int.MaxValue)
        return (uint)random.Next(0, (int)difference) + minValue;

      // Otherwise, special care is needed to handle wrapping around the normally negative values.
      // Get the wrapped values, and use it to create a wrap after the min value.
      long wrap = (long)int.MaxValue - difference;
      return (uint)((long)random.Next((int)wrap, int.MaxValue) - wrap + minValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="uint"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint InternalGetRandomUIntEI(this Random random, uint minValue, uint maxValue)
    {
      // Shift minValue by 1 due to exclusivity and return on the new range.
      return random.InternalGetRandomUIntII(minValue + 1, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="uint"/> on the range of
    /// [<see cref="uint.MinValue"/>, <see cref="uint.MaxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint InternalGetRandomUInt(this RandomNumberGenerator random)
    {
      // Generate a random byte array the size of an uint, and convert it.
      random.InternalGetRandomBytes(sizeof(uint), out byte[] bytes);
      return BitConverter.ToUInt32(bytes, 0);
    }

    /// <summary>
    /// An internal function for generating any <see cref="uint"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint InternalGetRandomUIntII(this RandomNumberGenerator random, uint minValue, uint maxValue)
    {
      // Get the difference between the min and max values.
      // NOTE: The original implementation does not cast either minValue or maxValue to a long,
      // resulting in generating numbers within (min, min + ABS(Max) - ABS(min)) due to overflow.
      uint difference = maxValue - minValue;

      // If the difference is the full int range, merely return using the byte method.
      if (difference == uint.MaxValue)
        return random.InternalGetRandomUInt();

      difference += 1; // Increase difference by one, due to the new inclusivity of the maxValue.

      byte[] bytes = new byte[sizeof(uint)]; // Create a byte array.

      // Get an allowed maximum based on our range.
      uint maxAllowedRandomValue = uint.MaxValue - (uint.MaxValue % difference);

      uint value; // The randomly generated value.

      // Generating with cryptographic safety is slow, as it requires this while loop.
      // Larger ranges are faster. The value must be less than the allowed maximum.
      do
      {
        // Generate a random uint, to keep positive.
        random.InternalGetRandomBytes(bytes);
        value = BitConverter.ToUInt32(bytes, 0);
      }
      while (value >= maxAllowedRandomValue);

      // Modulo the value to fix the range and return it.
      return (uint)(minValue + (value % difference));
    }

    /// <summary>
    /// An internal function for generating any <see cref="uint"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint InternalGetRandomUIntEE(this RandomNumberGenerator random, uint minValue,
                                                uint maxValue)
    {
      return random.InternalGetRandomUIntIE(minValue + 1, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="uint"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint InternalGetRandomUIntIE(this RandomNumberGenerator random, uint minValue,
                                                uint maxValue)
    {
      uint value; // The randomly generated value.

      // Get the difference between the min and max values.
      // NOTE: The original implementation does not cast either minValue or maxValue to a long,
      // resulting in generating numbers within (min, min + ABS(Max) - ABS(min)) due to overflow.
      uint difference = maxValue - minValue;
      byte[] bytes = new byte[sizeof(uint)]; // Create a byte array.

      // Get an allowed maximum based on our range.
      uint maxAllowedRandomValue = uint.MaxValue - (uint.MaxValue % difference);

      // Generating with cryptographic safety is slow, as it requires this while loop.
      // Larger ranges are faster. The value must be less than the allowed maximum.
      do
      {
        // Generate a random uint, to keep positive.
        random.InternalGetRandomBytes(bytes);
        value = BitConverter.ToUInt32(bytes, 0);
      }
      while (value >= maxAllowedRandomValue);

      // Modulo the value to fix the range and return it.
      return (uint)(minValue + (value % difference));
    }

    /// <summary>
    /// An internal function for generating any <see cref="uint"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="uint"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint InternalGetRandomUIntEI(this RandomNumberGenerator random, uint minValue,
                                                uint maxValue)
    {
      return random.InternalGetRandomUIntII(minValue + 1, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="long"/> on the range of
    /// [<see cref="long.MinValue"/>, <see cref="long.MaxValue"/>].
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long InternalGetRandomLong(this Random random)
    {
      // Generate a random byte array the size of an long, and convert it.
      random.InternalGetRandomBytes(sizeof(long), out byte[] bytes);
      return BitConverter.ToInt64(bytes, 0);
    }

    /// <summary>
    /// An internal function for generating any <see cref="long"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long InternalGetRandomLongII(this Random random, long minValue, long maxValue)
    {
      ulong value; // The randomly generated value.

      // Get the difference between the max and min. The max difference is the max uint value.
      ulong difference = (ulong)maxValue - (ulong)minValue;
      byte[] bytes = new byte[sizeof(ulong)]; // Create a byte array.

      // If the difference is for the full inclusive long range, return using the byte method.
      if (difference == ulong.MaxValue)
        return random.InternalGetRandomLong();

      difference += 1; // Add an extra point, as any Next function is inherently [I, E).

      // Most random generators only generate up to an int, rather than a long. Because of this
      // general limitation, we must use the remainder method.
      ulong maxAllowedRandomValue = ulong.MaxValue - (ulong.MaxValue % difference);

      do
      {
        // Generate a random uint, to keep positive.
        random.InternalGetRandomBytes(bytes);
        value = BitConverter.ToUInt64(bytes, 0);
      }
      while (value >= maxAllowedRandomValue);

      // Modulo the value to fix the range and return it.
      return (long)((ulong)minValue + (value % difference));
    }

    /// <summary>
    /// An internal function for generating any <see cref="long"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long InternalGetRandomLongEE(this Random random, long minValue, long maxValue)
    {
      // Shift minValue by 1 due to exclusivity and return on the new range.
      return random.InternalGetRandomLongIE(minValue + 1, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="long"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long InternalGetRandomLongIE(this Random random, long minValue, long maxValue)
    {
      ulong value; // The randomly generated value.

      // Get the difference between the max and min. The max difference is the max uint value.
      ulong difference = (ulong)maxValue - (ulong)minValue;
      byte[] bytes = new byte[sizeof(ulong)]; // Create a byte array.

      // Most random generators only generate up to an int, rather than a long. Because of this
      // general limitation, we must use the remainder method.
      ulong maxAllowedRandomValue = ulong.MaxValue - (ulong.MaxValue % difference);

      do
      {
        // Generate a random uint, to keep positive.
        random.InternalGetRandomBytes(bytes);
        value = BitConverter.ToUInt64(bytes, 0);
      }
      while (value >= maxAllowedRandomValue);

      // Modulo the value to fix the range and return it.
      return (long)((ulong)minValue + (value % difference));
    }

    /// <summary>
    /// An internal function for generating any <see cref="long"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long InternalGetRandomLongEI(this Random random, long minValue, long maxValue)
    {
      return random.InternalGetRandomLongII(minValue + 1, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="long"/> on the range of
    /// [<see cref="long.MinValue"/>, <see cref="long.MaxValue"/>].
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long InternalGetRandomLong(this RandomNumberGenerator random)
    {
      // Generate a random byte array the size of an long, and convert it.
      random.InternalGetRandomBytes(sizeof(long), out byte[] bytes);
      return BitConverter.ToInt64(bytes, 0);
    }

    /// <summary>
    /// An internal function for generating any <see cref="long"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long InternalGetRandomLongII(this RandomNumberGenerator random, long minValue,
                                                long maxValue)
    {
      ulong value; // The randomly generated value.

      // Get the difference between the max and min. The max difference is the max uint value.
      ulong difference = (ulong)maxValue - (ulong)minValue;
      byte[] bytes = new byte[sizeof(ulong)]; // Create a byte array.

      // If the difference is for the full inclusive long range, return using the byte method.
      if (difference == ulong.MaxValue)
        return random.InternalGetRandomLong();

      difference += 1; // Add an extra point, as any Next function is inherently [I, E).

      // Most random generators only generate up to an int, rather than a long. Because of this
      // general limitation, we must use the remainder method.
      ulong maxAllowedRandomValue = ulong.MaxValue - (ulong.MaxValue % difference);

      do
      {
        // Generate a random uint, to keep positive.
        random.InternalGetRandomBytes(bytes);
        value = BitConverter.ToUInt64(bytes, 0);
      }
      while (value >= maxAllowedRandomValue);

      // Modulo the value to fix the range and return it.
      return (long)((ulong)minValue + (value % difference));
    }

    /// <summary>
    /// An internal function for generating any <see cref="long"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long InternalGetRandomLongEE(this RandomNumberGenerator random, long minValue,
                                                long maxValue)
    {
      return random.InternalGetRandomLongIE(minValue + 1, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="long"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long InternalGetRandomLongIE(this RandomNumberGenerator random, long minValue,
                                                long maxValue)
    {
      ulong value; // The randomly generated value.

      // Get the difference between the max and min. The max difference is the max uint value.
      ulong difference = (ulong)maxValue - (ulong)minValue;
      byte[] bytes = new byte[sizeof(ulong)]; // Create a byte array.

      // If the difference is for the full inclusive long range, return using the byte method.
      if (difference == ulong.MaxValue)
        return random.InternalGetRandomLong();

      // Most random generators only generate up to an int, rather than a long. Because of this
      // general limitation, we must use the remainder method.
      ulong maxAllowedRandomValue = ulong.MaxValue - (ulong.MaxValue % difference);

      do
      {
        // Generate a random uint, to keep positive.
        random.InternalGetRandomBytes(bytes);
        value = BitConverter.ToUInt64(bytes, 0);
      }
      while (value >= maxAllowedRandomValue);

      // Modulo the value to fix the range and return it.
      return (long)((ulong)minValue + (value % difference));
    }

    /// <summary>
    /// An internal function for generating any <see cref="long"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="long"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long InternalGetRandomLongEI(this RandomNumberGenerator random, long minValue,
                                                long maxValue)
    {
      return random.InternalGetRandomLongII(minValue + 1, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="ulong"/> on the range of
    /// [<see cref="ulong.MinValue"/>, <see cref="ulong.MaxValue"/>].
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ulong InternalGetRandomULong(this Random random)
    {
      // Generate a random byte array the size of an ulong, and convert it.
      random.InternalGetRandomBytes(sizeof(ulong), out byte[] bytes);
      return BitConverter.ToUInt64(bytes, 0);
    }

    /// <summary>
    /// An internal function for generating any <see cref="ulong"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ulong InternalGetRandomULongII(this Random random, ulong minValue,
                                                  ulong maxValue)
    {
      ulong value; // The randomly generated value.

      // Get the difference between the max and min. The max difference is the max uint value.
      ulong difference = maxValue - minValue;
      byte[] bytes = new byte[sizeof(ulong)]; // Create a byte array.

      // If the difference is for the full inclusive ulong range, return using the byte method.
      if (difference == ulong.MaxValue)
        return random.InternalGetRandomULong();

      difference += 1; // Add an extra point, as any Next function is inherently [I, E).

      // Most random generators only generate up to an int, rather than a ulong. Because of this
      // general limitation, we must use the remainder method.
      ulong maxAllowedRandomValue = ulong.MaxValue - (ulong.MaxValue % difference);

      do
      {
        // Generate a random uint, to keep positive.
        random.InternalGetRandomBytes(bytes);
        value = BitConverter.ToUInt64(bytes, 0);
      }
      while (value >= maxAllowedRandomValue);

      // Modulo the value to fix the range and return it.
      return minValue + (value % difference);
    }

    /// <summary>
    /// An internal function for generating any <see cref="ulong"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ulong InternalGetRandomULongEE(this Random random, ulong minValue,
                                                  ulong maxValue)
    {
      // Shift minValue by 1 due to exclusivity and return on the new range.
      return random.InternalGetRandomULongIE(minValue + 1, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="ulong"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ulong InternalGetRandomULongIE(this Random random, ulong minValue,
                                                  ulong maxValue)
    {
      ulong value; // The randomly generated value.

      // Get the difference between the max and min. The max difference is the max uint value.
      ulong difference = maxValue - minValue;
      byte[] bytes = new byte[sizeof(ulong)]; // Create a byte array.

      // Most random generators only generate up to an int, rather than a ulong. Because of this
      // general limitation, we must use the remainder method.
      ulong maxAllowedRandomValue = ulong.MaxValue - (ulong.MaxValue % difference);

      do
      {
        // Generate a random uint, to keep positive.
        random.InternalGetRandomBytes(bytes);
        value = BitConverter.ToUInt64(bytes, 0);
      }
      while (value >= maxAllowedRandomValue);

      // Modulo the value to fix the range and return it.
      return minValue + (value % difference);
    }

    /// <summary>
    /// An internal function for generating any <see cref="ulong"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ulong InternalGetRandomULongEI(this Random random, ulong minValue,
                                                  ulong maxValue)
    {
      return random.InternalGetRandomULongII(minValue + 1, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="ulong"/> on the range of
    /// [<see cref="ulong.MinValue"/>, <see cref="ulong.MaxValue"/>].
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ulong InternalGetRandomULong(this RandomNumberGenerator random)
    {
      // Generate a random byte array the size of an ulong, and convert it.
      random.InternalGetRandomBytes(sizeof(ulong), out byte[] bytes);
      return BitConverter.ToUInt64(bytes, 0);
    }

    /// <summary>
    /// An internal function for generating any <see cref="ulong"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ulong InternalGetRandomULongII(this RandomNumberGenerator random, ulong minValue,
                                                  ulong maxValue)
    {
      ulong value; // The randomly generated value.

      // Get the difference between the max and min. The max difference is the max uint value.
      ulong difference = maxValue - minValue;
      byte[] bytes = new byte[sizeof(ulong)]; // Create a byte array.

      // If the difference is for the full inclusive ulong range, return using the byte method.
      if (difference == ulong.MaxValue)
        return random.InternalGetRandomULong();

      difference += 1; // Add an extra point, as any Next function is inherently [I, E).

      // Most random generators only generate up to an int, rather than a ulong. Because of this
      // general limitation, we must use the remainder method.
      ulong maxAllowedRandomValue = ulong.MaxValue - (ulong.MaxValue % difference);

      do
      {
        // Generate a random uint, to keep positive.
        random.InternalGetRandomBytes(bytes);
        value = BitConverter.ToUInt64(bytes, 0);
      }
      while (value >= maxAllowedRandomValue);

      // Modulo the value to fix the range and return it.
      return minValue + (value % difference);
    }

    /// <summary>
    /// An internal function for generating any <see cref="ulong"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ulong InternalGetRandomULongEE(this RandomNumberGenerator random, ulong minValue,
                                                  ulong maxValue)
    {
      return random.InternalGetRandomULongIE(minValue + 1, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="ulong"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ulong InternalGetRandomULongIE(this RandomNumberGenerator random, ulong minValue,
                                                  ulong maxValue)
    {
      ulong value; // The randomly generated value.

      // Get the difference between the max and min. The max difference is the max uint value.
      ulong difference = maxValue - minValue;
      byte[] bytes = new byte[sizeof(ulong)]; // Create a byte array.

      // If the difference is for the full inclusive ulong range, return using the byte method.
      if (difference == ulong.MaxValue)
        return random.InternalGetRandomULong();

      // Most random generators only generate up to an int, rather than a ulong. Because of this
      // general limitation, we must use the remainder method.
      ulong maxAllowedRandomValue = ulong.MaxValue - (ulong.MaxValue % difference);

      do
      {
        // Generate a random uint, to keep positive.
        random.InternalGetRandomBytes(bytes);
        value = BitConverter.ToUInt64(bytes, 0);
      }
      while (value >= maxAllowedRandomValue);

      // Modulo the value to fix the range and return it.
      return minValue + (value % difference);
    }

    /// <summary>
    /// An internal function for generating any <see cref="ulong"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="ulong"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ulong InternalGetRandomULongEI(this RandomNumberGenerator random, ulong minValue,
                                                  ulong maxValue)
    {
      return random.InternalGetRandomULongII(minValue + 1, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="double"/> on the range of
    /// [<see cref="double.MinValue"/>, <see cref="double.MaxValue"/>].
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double InternalGetRandomDouble(this Random random)
    {
      return random.InternalGetRandomDoubleII(double.MinValue, double.MaxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="double"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double InternalGetRandomDoubleII(this Random random, double minValue,
                                                    double maxValue)
    {
      // Get the sample and perform a special equation that keeps linear on large ranges.
      // We add the lost double precision to get a near-true [0.0, 1.0] range.
      double value = random.InternalGetRandomDoubleIE(0.0, 1.0 + LostDoublePrecision);
      // The clamp is for small rounding errors that can occur with floating points.
      return Maths.ClampII((maxValue * value) + (minValue * (1.0 - value)), minValue, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="double"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double InternalGetRandomDoubleEE(this Random random, double minValue,
                                                    double maxValue)
    {
      // Get the sample and perform a special equation that keeps linear on large ranges.
      // We add the lost double precision to get a near-true [0.0, 1.0] range.
      double value = random.InternalGetRandomDoubleIE(LostDoublePrecision, 1.0);
      // The clamp is for small rounding errors that can occur with floating points.
      return Maths.ClampII((maxValue * value) + (minValue * (1.0 - value)), minValue, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="double"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double InternalGetRandomDoubleIE(this Random random, double minValue,
                                                    double maxValue)
    {
      // Get the sample and perform a special equation that keeps linear on large ranges.
      // The clamp is for small rounding errors that can occur with floating points.
      double value = random.NextDouble();
      return Maths.ClampII((maxValue * value) + (minValue * (1d - value)), minValue, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="double"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double InternalGetRandomDoubleEI(this Random random, double minValue,
                                                    double maxValue)
    {
      // Get the sample and perform a special equation that keeps linear on large ranges.
      // We add the lost double precision to get a near-true [0.0, 1.0] range.
      double value = random.InternalGetRandomDoubleIE(LostDoublePrecision, 1 + LostDoublePrecision);
      // The clamp is for small rounding errors that can occur with floating points.
      return Maths.ClampII((maxValue * value) + (minValue * (1.0 - value)), minValue, maxValue);
    }

    /// <summary>
    /// A helper function for generating a cryptographically secure random <see cref="double"/> on
    /// the range of [0.0, 1.0).
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a random, crypotgraphically secure <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double InternalGetSecureDouble01(this RandomNumberGenerator random)
    {
      // Get bytes for a random ulong and convert the array.
      byte[] bytes = new byte[sizeof(ulong)];
      random.GetBytes(bytes);
      ulong value = BitConverter.ToUInt64(bytes, 0);

      // We only have 53 bits to work with for generating a secure double.
      value &= (1ul << MaxSecureDoubleBits) - 1;
      // Return the value, divided by the bit mask.
      return (double)value / (double)(1ul << MaxSecureDoubleBits);
    }

    /// <summary>
    /// An internal function for generating any <see cref="double"/> on the range of
    /// [<see cref="double.MinValue"/>, <see cref="double.MaxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double InternalGetRandomDouble(this RandomNumberGenerator random)
    {
      return random.InternalGetRandomDoubleII(double.MinValue, double.MaxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="double"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double InternalGetRandomDoubleII(this RandomNumberGenerator random,
                                                    double minValue, double maxValue)
    {
      // Get the sample and perform a special equation that keeps linear on large ranges.
      // We add the lost double precision to get a near-true [0.0, 1.0] range.
      double value = random.InternalGetRandomDoubleIE(0.0, 1.0 + LostDoublePrecision);
      // The clamp is for small rounding errors that can occur with floating points.
      return Maths.ClampII((maxValue * value) + (minValue * (1.0 - value)), minValue, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="double"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double InternalGetRandomDoubleEE(this RandomNumberGenerator random,
                                                    double minValue, double maxValue)
    {
      // Get the sample and perform a special equation that keeps linear on large ranges.
      // We add the lost double precision to get a near-true [0.0, 1.0] range.
      double value = random.InternalGetRandomDoubleIE(LostDoublePrecision, 1.0);
      // The clamp is for small rounding errors that can occur with floating points.
      return Maths.ClampII((maxValue * value) + (minValue * (1.0 - value)), minValue, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="double"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double InternalGetRandomDoubleIE(this RandomNumberGenerator random,
                                                    double minValue, double maxValue)
    {
      // Get the sample and perform a special equation that keeps linear on large ranges.
      // The clamp is for small rounding errors that can occur with floating points.
      double value = random.InternalGetSecureDouble01();
      return Maths.ClampII((maxValue * value) + (minValue * (1d - value)), minValue, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="double"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double InternalGetRandomDoubleEI(this RandomNumberGenerator random,
                                                    double minValue, double maxValue)
    {
      // Get the sample and perform a special equation that keeps linear on large ranges.
      // We add the lost double precision to get a near-true [0.0, 1.0] range.
      double value = random.InternalGetRandomDoubleIE(LostDoublePrecision, 1 + LostDoublePrecision);
      // The clamp is for small rounding errors that can occur with floating points.
      return Maths.ClampII((maxValue * value) + (minValue * (1.0 - value)), minValue, maxValue);
    }

    /// <summary>
    /// An internal function for generating a <see cref="decimal"/> on the range of [0.0, 1.0].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal InternalGetRandomDecimal01II(this Random random)
    {
      // Generate a new decimal using several other random values.
      decimal value = new decimal(random.InternalGetRandomInt(), random.InternalGetRandomInt(),
                                  random.InternalGetRandomIntII(0, MaxDecimalHi),
                                  false, MaxDecimalScale);

      // decimal(intMax, intMax, MaxDecimalHi) is just greater than 1. No value granted by this
      // would normally be truly 1. So, we manually clamp for errors.
      return Maths.ClampII(value, 0, 1);
    }

    /// <summary>
    /// An internal function for generating a <see cref="decimal"/> on the range of [0.0, 1.0].
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal InternalGetRandomDecimal01IE(this Random random)
    {
      // Generate a new decimal using several other random values.
      return new decimal(random.InternalGetRandomInt(), random.InternalGetRandomInt(),
                         random.InternalGetRandomIntIE(0, MaxDecimalHi), false, MaxDecimalScale);
    }

    /// <summary>
    /// An internal function for generating any <see cref="decimal"/> on the range of
    /// [<see cref="decimal.MinValue"/>, <see cref="decimal.MaxValue"/>].
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static decimal InternalGetRandomDecimal(this Random random)
    {
      return random.InternalGetRandomDecimalII(decimal.MinValue, decimal.MaxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="decimal"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static decimal InternalGetRandomDecimalII(this Random random, decimal minValue,
                                                      decimal maxValue)
    {
      // Get the sample of a decimal.
      decimal value = random.InternalGetRandomDecimal01II();
      // Use a special function for making sure it works on large ranges.
      return (maxValue * value) + (minValue * (1m - value));
    }

    /// <summary>
    /// An internal function for generating any <see cref="decimal"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static decimal InternalGetRandomDecimalEE(this Random random, decimal minValue,
                                                      decimal maxValue)
    {
      decimal dec = new decimal(1, 0, 0, false, MaxDecimalScale);
      return random.InternalGetRandomDecimalIE(minValue + dec, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="decimal"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static decimal InternalGetRandomDecimalIE(this Random random, decimal minValue,
                                                      decimal maxValue)
    {
      // Get the sample of a decimal.
      decimal value = random.InternalGetRandomDecimal01IE();
      // Use a special function for making sure it works on large ranges.
      return (maxValue * value) + (minValue * (1m - value));
    }

    /// <summary>
    /// An internal function for generating any <see cref="decimal"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static decimal InternalGetRandomDecimalEI(this Random random, decimal minValue,
                                                      decimal maxValue)
    {
      decimal dec = new decimal(1, 0, 0, false, MaxDecimalScale);
      return random.InternalGetRandomDecimalII(minValue + dec, maxValue);
    }

    /// <summary>
    /// An internal function for generating a <see cref="decimal"/> on the range of [0.0, 1.0].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal InternalGetRandomDecimal01II(this RandomNumberGenerator random)
    {
      // Generate a new decimal using several other random values.
      decimal value = new decimal(random.InternalGetRandomInt(), random.InternalGetRandomInt(),
                                  random.InternalGetRandomIntII(0, MaxDecimalHi),
                                  false, MaxDecimalScale);

      // decimal(intMax, intMax, MaxDecimalHi) is just greater than 1. No value granted by this
      // would normally be truly 1. So, we manually clamp for errors.
      return Maths.ClampII(value, 0, 1);
    }

    /// <summary>
    /// An internal function for generating a <see cref="decimal"/> on the range of [0.0, 1.0].
    /// This variant is for cryptographic randon generators.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal InternalGetRandomDecimal01IE(this RandomNumberGenerator random)
    {
      // Generate a new decimal using several other random values.
      return new decimal(random.InternalGetRandomInt(), random.InternalGetRandomInt(),
                         random.InternalGetRandomIntIE(0, MaxDecimalHi), false, MaxDecimalScale);
    }

    /// <summary>
    /// An internal function for generating any <see cref="decimal"/> on the range of
    /// [<see cref="decimal.MinValue"/>, <see cref="decimal.MaxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static decimal InternalGetRandomDecimal(this RandomNumberGenerator random)
    {
      return random.InternalGetRandomDecimalII(decimal.MinValue, decimal.MaxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="decimal"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static decimal InternalGetRandomDecimalII(this RandomNumberGenerator random,
                                                      decimal minValue, decimal maxValue)
    {
      // Get the sample of a decimal.
      decimal value = random.InternalGetRandomDecimal01II();
      // Use a special function for making sure it works on large ranges.
      return (maxValue * value) + (minValue * (1m - value));
    }

    /// <summary>
    /// An internal function for generating any <see cref="decimal"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static decimal InternalGetRandomDecimalEE(this RandomNumberGenerator random,
                                                      decimal minValue, decimal maxValue)
    {
      decimal dec = new decimal(1, 0, 0, false, MaxDecimalScale);
      return random.InternalGetRandomDecimalIE(minValue + dec, maxValue);
    }

    /// <summary>
    /// An internal function for generating any <see cref="decimal"/> on the range of
    /// [<paramref name="minValue"/>, <paramref name="maxValue"/>).
    /// This variant is for cryptographic randon generators.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The inclusive minimum value allowed.</param>
    /// <param name="maxValue">The exclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static decimal InternalGetRandomDecimalIE(this RandomNumberGenerator random,
                                                      decimal minValue, decimal maxValue)
    {
      // Get the sample of a decimal.
      decimal value = random.InternalGetRandomDecimal01IE();
      // Use a special function for making sure it works on large ranges.
      return (maxValue * value) + (minValue * (1m - value));
    }

    /// <summary>
    /// An internal function for generating any <see cref="decimal"/> on the range of
    /// (<paramref name="minValue"/>, <paramref name="maxValue"/>].
    /// This variant is for cryptographic randon generators.
    /// Floating-points struggle with perfect accuracy. This function intends to get as close
    /// as programmatically possible via a precision fix.
    /// This is inlined, with no error checking. Public functions handle error checking.
    /// </summary>
    /// <param name="random">The random generator to use.</param>
    /// <param name="minValue">The exclusive minimum value allowed.</param>
    /// <param name="maxValue">The inclusive maximum value allowed.</param>
    /// <returns>Returns a randomly generated <see cref="decimal"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static decimal InternalGetRandomDecimalEI(this RandomNumberGenerator random, decimal minValue, decimal maxValue)
    {
      decimal dec = new decimal(1, 0, 0, false, MaxDecimalScale);
      return random.InternalGetRandomDecimalII(minValue + dec, maxValue);
    }
  }
  /************************************************************************************************/
}