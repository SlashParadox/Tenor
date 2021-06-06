/**************************************************************************************************/
/*!
\file   MinMaxException.cs
\author Craig Williams
\par    Last Updated
        2021-05-24
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for an exception to be used when a min or max value is incorrect.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System.Runtime.CompilerServices;
using System.Text;

namespace CodeParadox.Tenor.Exceptions
{
  /************************************************************************************************/
  /// <summary>
  /// An exception class that should be thrown when the minimum and maximum boundaries passed
  /// are invalid. Typically, this is when the min is greater (or equal when it shouldn't be) to
  /// the max value.
  /// </summary>
  /// <typeparam name="T">The type of the min max. This should be a numeric type.</typeparam>
  public class MinMaxException<T> : System.Exception
  {
    /// <summary>
    /// The default constructor for a <see cref="MinMaxException{T}"/>.
    /// </summary>
    public MinMaxException() :
      base("The given minimum and maximum values were not valid. They may need to be swapped.")
    { }

    /// <summary>
    /// A constructor for a <see cref="MinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public MinMaxException(T givenMin, T givenMax, bool allowedEqual)
      : base(BuildMessage(givenMin, givenMax, allowedEqual)) { }

    /// <summary>
    /// A helper function to give more context to a <see cref="MinMaxException"/> when one is
    /// thrown. This should never be accessed outside of constructors.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    /// <returns>Returns a formatted message for a <see cref="MinMaxException"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string BuildMessage(string givenMin, string givenMax, bool allowedEqual)
    {
      // Create the string. Only append 'or equal to' if equality was allowed.
      StringBuilder strb = new StringBuilder("Given Minimum [");
      strb.Append(givenMin).Append("] was greater than ");
      strb.Append(!allowedEqual ? "or equal to " : string.Empty).Append("given Maximum [");
      strb.Append(givenMax).Append("].");

      return strb.ToString(); // Return the finalized string.S
    }

    /// <summary>
    /// A helper function to give more context to a <see cref="MinMaxException"/> when one is
    /// thrown. This should never be accessed outside of constructors.
    /// </summary>
    /// <typeparam name="T">The type of the min max. This should be a numeric type.</typeparam>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    /// <returns>Returns a formatted message for a <see cref="MinMaxException"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string BuildMessage<T>(T givenMin, T givenMax, bool allowedEqual)
    {
      return BuildMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }
  }
  /************************************************************************************************/
}