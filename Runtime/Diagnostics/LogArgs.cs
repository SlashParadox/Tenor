/**************************************************************************************************/
/*!
\file   LogArgs.cs
\author Craig Williams
\par    Last Updated
        2021-06-18
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for a class representing data passed after a logging event.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System;
using System.Diagnostics;

namespace CodeParadox.Tenor.Diagnostics
{
  /************************************************************************************************/
  /// <summary>
  /// A <see langword="class"/> of <see langword="event"/> arguments that are passed after a
  /// <see cref="Log"/> successfully logs a message to the <see cref="System.Console"/> or a file.
  /// </summary>
  public sealed class LogArgs
  {
    /// <summary>The <see cref="string"/> that was logged.</summary>
    public readonly string OriginalMessage;
    /// <summary>The <see cref="OriginalMessage"/> formatted by the <see cref="Log"/>.</summary>
    public readonly string FormattedMessage;
    /// <summary>The formatted <see cref="StackTrace"/>, if one was printed.</summary>
    public readonly string FormattedStackTrace;
    /// <summary>The entire message, as it was logged.</summary>
    public readonly string FullMessage;
    /// <summary>The <see cref="Level"/> of the message.</summary>
    public readonly Level SeverityLevel;
    /// <summary>The <see cref="Exception"/>, if any, that was thrown.</summary>
    public readonly Exception ThrownException;

    /// <summary>
    /// A constructor for some <see cref="LogArgs"/>.
    /// </summary>
    /// <param name="om">See: <see cref="OriginalMessage"/></param>
    /// <param name="fm">See: <see cref="FormattedMessage"/></param>
    /// <param name="fst">See: <see cref="FormattedStackTrace"/></param>
    /// <param name="full">See: <see cref="FullMessage"/></param>
    /// <param name="level">See: <see cref="SeverityLevel"/></param>
    /// <param name="e">See: <see cref="ThrownException"/></param>
    public LogArgs(string om, string fm, string fst, string full, Level level, Exception e = null)
    {
      OriginalMessage = om;
      FormattedMessage = fm;
      FormattedStackTrace = fst;
      FullMessage = full;
      SeverityLevel = level;
      ThrownException = e;
    }
  }
  /************************************************************************************************/
}