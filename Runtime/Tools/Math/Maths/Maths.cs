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

namespace CodeParadox.Tenor.Tools
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
  }
  /************************************************************************************************/
}