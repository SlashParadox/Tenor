/**************************************************************************************************/
/*!
\file   BadMinMaxException.cs
\author Craig Williams
\par    Last Updated
        2021-03-07
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for an exception class that is thrown when a min or max value passed in is incorrect.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System.Runtime.CompilerServices;
using System.Text;

namespace Tenor.Error
{
  /************************************************************************************************/
  /// <summary>
  /// An exception class that should be thrown when the minimum and maximum boundaries passed
  /// are invalid. Typically, this is when the min is greater (or equal when it shouldn't be) to
  /// the max value.
  /// </summary>
  public class BadMinMaxException : System.Exception
  {
    /// <summary>
    /// The default constructor for a <see cref="BadMinMaxException"/>.
    /// </summary>
    public BadMinMaxException() :
      base("The given minimum and maximum values were not valid. They may need to be swapped.") { }

    /// <summary>
    /// A constructor for a <see cref="BadMinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public BadMinMaxException(sbyte givenMin, sbyte givenMax, bool allowedEqual) :
      base(BuildExceptionMessage(givenMin, givenMax, allowedEqual)) { }

    /// <summary>
    /// A constructor for a <see cref="BadMinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public BadMinMaxException(byte givenMin, byte givenMax, bool allowedEqual) :
      base(BuildExceptionMessage(givenMin, givenMax, allowedEqual)) { }

    /// <summary>
    /// A constructor for a <see cref="BadMinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public BadMinMaxException(short givenMin, short givenMax, bool allowedEqual) :
      base(BuildExceptionMessage(givenMin, givenMax, allowedEqual)) { }

    /// <summary>
    /// A constructor for a <see cref="BadMinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public BadMinMaxException(ushort givenMin, ushort givenMax, bool allowedEqual) :
      base(BuildExceptionMessage(givenMin, givenMax, allowedEqual)) { }

    /// <summary>
    /// A constructor for a <see cref="BadMinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public BadMinMaxException(int givenMin, int givenMax, bool allowedEqual) :
      base(BuildExceptionMessage(givenMin, givenMax, allowedEqual)) { }

    /// <summary>
    /// A constructor for a <see cref="BadMinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public BadMinMaxException(uint givenMin, uint givenMax, bool allowedEqual) :
      base(BuildExceptionMessage(givenMin, givenMax, allowedEqual)) { }

    /// <summary>
    /// A constructor for a <see cref="BadMinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public BadMinMaxException(long givenMin, long givenMax, bool allowedEqual) :
      base(BuildExceptionMessage(givenMin, givenMax, allowedEqual)) { }

    /// <summary>
    /// A constructor for a <see cref="BadMinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public BadMinMaxException(ulong givenMin, ulong givenMax, bool allowedEqual) :
      base(BuildExceptionMessage(givenMin, givenMax, allowedEqual)) { }

    /// <summary>
    /// A constructor for a <see cref="BadMinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public BadMinMaxException(float givenMin, float givenMax, bool allowedEqual) :
      base(BuildExceptionMessage(givenMin, givenMax, allowedEqual)) { }

    /// <summary>
    /// A constructor for a <see cref="BadMinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public BadMinMaxException(double givenMin, double givenMax, bool allowedEqual) :
      base(BuildExceptionMessage(givenMin, givenMax, allowedEqual)) { }

    /// <summary>
    /// A constructor for a <see cref="BadMinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public BadMinMaxException(decimal givenMin, decimal givenMax, bool allowedEqual) :
      base(BuildExceptionMessage(givenMin, givenMax, allowedEqual)) { }

    /// <summary>
    /// A constructor for a <see cref="BadMinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public BadMinMaxException(char givenMin, char givenMax, bool allowedEqual) :
      base(BuildExceptionMessage(givenMin, givenMax, allowedEqual)) { }

    /// <summary>
    /// A helper function to give more context to a <see cref="BadMinMaxException"/> when one is
    /// thrown. This should never be accessed outside of constructors.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    /// <returns>Returns a formatted message for a <see cref="BadMinMaxException"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string BuildExceptionMessage(string givenMin, string givenMax, bool allowedEqual)
    {
      // Create the string. Only append 'or equal to' if equality was allowed.
      StringBuilder strb = new StringBuilder("Given Minimum [");
      strb.Append(givenMin).Append("] was greater than ");
      strb.Append(!allowedEqual ? "or equal to " : string.Empty).Append("given Maximum [");
      strb.Append(givenMax).Append("].");

      return strb.ToString(); // Return the finalized string.S
    }

    /// <summary>
    /// A helper function to give more context to a <see cref="BadMinMaxException"/> when one is
    /// thrown. This should never be accessed outside of constructors.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    /// <returns>Returns a formatted message for a <see cref="BadMinMaxException"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string BuildExceptionMessage(sbyte givenMin, sbyte givenMax, bool allowedEqual)
    {
      return BuildExceptionMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }

    /// <summary>
    /// A helper function to give more context to a <see cref="BadMinMaxException"/> when one is
    /// thrown. This should never be accessed outside of constructors.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    /// <returns>Returns a formatted message for a <see cref="BadMinMaxException"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string BuildExceptionMessage(byte givenMin, byte givenMax, bool allowedEqual)
    {
      return BuildExceptionMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }

    /// <summary>
    /// A helper function to give more context to a <see cref="BadMinMaxException"/> when one is
    /// thrown. This should never be accessed outside of constructors.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    /// <returns>Returns a formatted message for a <see cref="BadMinMaxException"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string BuildExceptionMessage(short givenMin, short givenMax, bool allowedEqual)
    {
      return BuildExceptionMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }

    /// <summary>
    /// A helper function to give more context to a <see cref="BadMinMaxException"/> when one is
    /// thrown. This should never be accessed outside of constructors.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    /// <returns>Returns a formatted message for a <see cref="BadMinMaxException"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string BuildExceptionMessage(ushort givenMin, ushort givenMax, bool allowedEqual)
    {
      return BuildExceptionMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }

    /// <summary>
    /// A helper function to give more context to a <see cref="BadMinMaxException"/> when one is
    /// thrown. This should never be accessed outside of constructors.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    /// <returns>Returns a formatted message for a <see cref="BadMinMaxException"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string BuildExceptionMessage(int givenMin, int givenMax, bool allowedEqual)
    {
      return BuildExceptionMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }

    /// <summary>
    /// A helper function to give more context to a <see cref="BadMinMaxException"/> when one is
    /// thrown. This should never be accessed outside of constructors.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    /// <returns>Returns a formatted message for a <see cref="BadMinMaxException"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string BuildExceptionMessage(uint givenMin, uint givenMax, bool allowedEqual)
    {
      return BuildExceptionMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }

    /// <summary>
    /// A helper function to give more context to a <see cref="BadMinMaxException"/> when one is
    /// thrown. This should never be accessed outside of constructors.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    /// <returns>Returns a formatted message for a <see cref="BadMinMaxException"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string BuildExceptionMessage(long givenMin, long givenMax, bool allowedEqual)
    {
      return BuildExceptionMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }

    /// <summary>
    /// A helper function to give more context to a <see cref="BadMinMaxException"/> when one is
    /// thrown. This should never be accessed outside of constructors.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    /// <returns>Returns a formatted message for a <see cref="BadMinMaxException"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string BuildExceptionMessage(ulong givenMin, ulong givenMax, bool allowedEqual)
    {
      return BuildExceptionMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }

    /// <summary>
    /// A helper function to give more context to a <see cref="BadMinMaxException"/> when one is
    /// thrown. This should never be accessed outside of constructors.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    /// <returns>Returns a formatted message for a <see cref="BadMinMaxException"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string BuildExceptionMessage(float givenMin, float givenMax, bool allowedEqual)
    {
      return BuildExceptionMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }

    /// <summary>
    /// A helper function to give more context to a <see cref="BadMinMaxException"/> when one is
    /// thrown. This should never be accessed outside of constructors.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    /// <returns>Returns a formatted message for a <see cref="BadMinMaxException"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string BuildExceptionMessage(double givenMin, double givenMax, bool allowedEqual)
    {
      return BuildExceptionMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }

    /// <summary>
    /// A helper function to give more context to a <see cref="BadMinMaxException"/> when one is
    /// thrown. This should never be accessed outside of constructors.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    /// <returns>Returns a formatted message for a <see cref="BadMinMaxException"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string BuildExceptionMessage(decimal givenMin, decimal givenMax, bool allowedEqual)
    {
      return BuildExceptionMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }

    /// <summary>
    /// A helper function to give more context to a <see cref="BadMinMaxException"/> when one is
    /// thrown. This should never be accessed outside of constructors.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    /// <returns>Returns a formatted message for a <see cref="BadMinMaxException"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string BuildExceptionMessage(char givenMin, char givenMax, bool allowedEqual)
    {
      return BuildExceptionMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }
  }
  /************************************************************************************************/
}