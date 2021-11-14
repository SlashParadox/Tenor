/**************************************************************************************************/
/*!
\file   Maths.cs
\author Craig Williams
\par    Last Updated
        2021-07-09
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for a class of useful mathematical functions.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System.Collections.Generic;

namespace SlashParadox.Tenor.Tools
{
  /************************************************************************************************/
  public static partial class Maths
  {
    /// <summary>
    /// A function for converting radians into degrees.
    /// </summary>
    /// <param name="radians">The radians to convert.</param>
    /// <returns>Returns <paramref name="radians"/> in degrees.</returns>
    public static double RadiansToDegrees(double radians)
    {
      return radians * (180.0 / System.Math.PI);
    }

    /// <summary>
    /// A function for converting degrees into radians.
    /// </summary>
    /// <param name="degrees">The degrees to convert.</param>
    /// <returns>Returns <paramref name="degrees"/> in radians.</returns>
    public static double DegreesToRadians(double degrees)
    {
      return degrees * (System.Math.PI / 180.0);
    }

    /// <summary>
    /// A function for creating a <see cref="List{T}"/> of the digits within a
    /// <paramref name="number"/>.
    /// </summary>
    /// <param name="number">The number to get the digits of.</param>
    /// <param name="keepOrder">If true, the digits will be in the same order as the
    /// <paramref name="number"/>. Otherwise, the digits are reversed.</param>
    /// <returns>Returns the <see cref="List{T}"/> of digits.</returns>
    public static List<int> CreateDigitList(int number, bool keepOrder)
    {
      List<int> digits = new List<int>(); // The list of digits.
      
      // Add each individual digit, backwards. Use a do-while to enforce 0 being added.
      do
      {
        digits.Add(number % 10); // Get the current digit.
        number /= 10; // Divide evenly to remove the digit.
      }
      while (number > 0);

      // If keeping order, reverse into the right order.
      if (keepOrder)
        digits.Reverse();

      return digits;
    }
  }
  /************************************************************************************************/
}