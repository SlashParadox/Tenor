/**************************************************************************************************/
/*!
\file   Maths_Vectors.cs
\author Craig Williams
\par    Last Updated
        2021-07-09
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for a class of useful mathematical functions related to numerical vectors.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System.Numerics;

namespace CodeParadox.Tenor.Tools
{
  /************************************************************************************************/
  public static partial class Maths
  {
    /// <summary>A universal normal <see cref="Vector3"/>. Change this depending on your
    /// environment.</summary>
    public static Vector3 UniversalNormal = new Vector3(0, 0, 1);

    /// <summary>
    /// An extension function for getting the magnitude of a <see cref="Vector3"/>.
    /// </summary>
    /// <param name="vector">The vector to get the magnitude of.</param>
    /// <returns>Returns the <paramref name="vector"/>'s magnitude.</returns>
    public static float Magnitude(this Vector3 vector)
    {
      return (float)System.Math.Sqrt(SquaredMagnitude(vector));
    }

    /// <summary>
    /// An extension function for getting the squared magnitude of a <see cref="Vector3"/>.
    /// </summary>
    /// <param name="vector">The vector to get the squared magnitude of.</param>
    /// <returns>Returns the <paramref name="vector"/>'s squared magnitude.</returns>
    public static float SquaredMagnitude(this Vector3 vector)
    {
      return vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z;
    }

    /// <summary>
    /// A function for getting the angle (0, 360] between two <see cref="Vector3"/>s. Uses the
    /// <see cref="UniversalNormal"/>.
    /// </summary>
    /// <param name="a">The first <see cref="Vector3"/>.</param>
    /// <param name="b">The second <see cref="Vector3"/>.</param>
    /// <returns>Returns the angle between <paramref name="a"/> and <paramref name="b"/>.</returns>
    /// <remarks>The order of <paramref name="a"/> and <paramref name="b"/> matters, due to
    /// the usage of a cross product.</remarks>
    public static float GetAngle(Vector3 a, Vector3 b)
    {
      return GetAngle(a, b, UniversalNormal);
    }

    /// <summary>
    /// A function for getting the angle (0, 360] between two <see cref="Vector3"/>s.
    /// </summary>
    /// <param name="a">The first <see cref="Vector3"/>.</param>
    /// <param name="b">The second <see cref="Vector3"/>.</param>
    /// <param name="normal">The normal to determine the clockwise orientation.</param>
    /// <returns>Returns the angle between <paramref name="a"/> and <paramref name="b"/>.</returns>
    /// <remarks>The order of <paramref name="a"/> and <paramref name="b"/> matters, due to
    /// the usage of a cross product.</remarks>
    public static float GetAngle(Vector3 a, Vector3 b, Vector3 normal)
    {
      // Get the angle between the two vectors.
      float cosine = Vector3.Dot(a, b) / (a.Magnitude() * b.Magnitude());
      float angle = (float)System.Math.Acos(cosine);

      angle = (float)RadiansToDegrees(angle); // Convert from radians to degrees.

      // Get the sign of the dot between the normal and the cross product to determine orientation.
      float sign = System.Math.Sign(Vector3.Dot(normal, Vector3.Cross(a, b)));

      // If the sign is valid, multiply to set the orientation.
      if (sign != 0)
        angle *= sign;

      // Perform a small calculation to keep the angle between (0, 360].
      angle = (angle + 360.0f) % 360.0f;
      
      return angle; // Return the final angle.
    }
  }
  /************************************************************************************************/
}