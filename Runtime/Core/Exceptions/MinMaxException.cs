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
  public class MinMaxException : System.Exception
  {
    /// <summary>
    /// The default constructor for a <see cref="MinMaxException"/>.
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
    public MinMaxException(sbyte givenMin, sbyte givenMax, bool allowedEqual) :
      base(BuildMessage(givenMin, givenMax, allowedEqual))
    { }

    /// <summary>
    /// A constructor for a <see cref="MinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public MinMaxException(byte givenMin, byte givenMax, bool allowedEqual) :
      base(BuildMessage(givenMin, givenMax, allowedEqual))
    { }

    /// <summary>
    /// A constructor for a <see cref="MinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public MinMaxException(short givenMin, short givenMax, bool allowedEqual) :
      base(BuildMessage(givenMin, givenMax, allowedEqual))
    { }

    /// <summary>
    /// A constructor for a <see cref="MinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public MinMaxException(ushort givenMin, ushort givenMax, bool allowedEqual) :
      base(BuildMessage(givenMin, givenMax, allowedEqual))
    { }

    /// <summary>
    /// A constructor for a <see cref="MinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public MinMaxException(int givenMin, int givenMax, bool allowedEqual) :
      base(BuildMessage(givenMin, givenMax, allowedEqual))
    { }

    /// <summary>
    /// A constructor for a <see cref="MinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public MinMaxException(uint givenMin, uint givenMax, bool allowedEqual) :
      base(BuildMessage(givenMin, givenMax, allowedEqual))
    { }

    /// <summary>
    /// A constructor for a <see cref="MinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public MinMaxException(long givenMin, long givenMax, bool allowedEqual) :
      base(BuildMessage(givenMin, givenMax, allowedEqual))
    { }

    /// <summary>
    /// A constructor for a <see cref="MinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public MinMaxException(ulong givenMin, ulong givenMax, bool allowedEqual) :
      base(BuildMessage(givenMin, givenMax, allowedEqual))
    { }

    /// <summary>
    /// A constructor for a <see cref="MinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public MinMaxException(float givenMin, float givenMax, bool allowedEqual) :
      base(BuildMessage(givenMin, givenMax, allowedEqual))
    { }

    /// <summary>
    /// A constructor for a <see cref="MinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public MinMaxException(double givenMin, double givenMax, bool allowedEqual) :
      base(BuildMessage(givenMin, givenMax, allowedEqual))
    { }

    /// <summary>
    /// A constructor for a <see cref="MinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public MinMaxException(decimal givenMin, decimal givenMax, bool allowedEqual) :
      base(BuildMessage(givenMin, givenMax, allowedEqual))
    { }

    /// <summary>
    /// A constructor for a <see cref="MinMaxException"/>, which will create a formatted
    /// string based on the values that were passed.
    /// </summary>
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    public MinMaxException(char givenMin, char givenMax, bool allowedEqual) :
      base(BuildMessage(givenMin, givenMax, allowedEqual))
    { }

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
    /// <param name="givenMin">The minimum value passed in.</param>
    /// <param name="givenMax">The maximum value passed in.</param>
    /// <param name="allowedEqual">A bool determining if <paramref name="givenMin"/>
    /// was allowed to equal <paramref name="givenMax"/>.</param>
    /// <returns>Returns a formatted message for a <see cref="MinMaxException"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string BuildMessage(sbyte givenMin, sbyte givenMax, bool allowedEqual)
    {
      return BuildMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }

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
    private static string BuildMessage(byte givenMin, byte givenMax, bool allowedEqual)
    {
      return BuildMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }

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
    private static string BuildMessage(short givenMin, short givenMax, bool allowedEqual)
    {
      return BuildMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }

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
    private static string BuildMessage(ushort givenMin, ushort givenMax, bool allowedEqual)
    {
      return BuildMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }

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
    private static string BuildMessage(int givenMin, int givenMax, bool allowedEqual)
    {
      return BuildMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }

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
    private static string BuildMessage(uint givenMin, uint givenMax, bool allowedEqual)
    {
      return BuildMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }

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
    private static string BuildMessage(long givenMin, long givenMax, bool allowedEqual)
    {
      return BuildMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }

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
    private static string BuildMessage(ulong givenMin, ulong givenMax, bool allowedEqual)
    {
      return BuildMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }

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
    private static string BuildMessage(float givenMin, float givenMax, bool allowedEqual)
    {
      return BuildMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }

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
    private static string BuildMessage(double givenMin, double givenMax, bool allowedEqual)
    {
      return BuildMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }

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
    private static string BuildMessage(decimal givenMin, decimal givenMax, bool allowedEqual)
    {
      return BuildMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }

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
    private static string BuildMessage(char givenMin, char givenMax, bool allowedEqual)
    {
      return BuildMessage(givenMin.ToString(), givenMax.ToString(), allowedEqual);
    }
  }
  /************************************************************************************************/
}
