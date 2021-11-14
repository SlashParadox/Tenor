/**************************************************************************************************/
/*!
\file   Level.cs
\author Craig Williams
\par    Last Updated
        2021-06-18
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for a class representing a severity level for logging information.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System;
using System.Diagnostics;

namespace SlashParadox.Tenor.Diagnostics
{
  /************************************************************************************************/
  /// <summary>
  /// A level of severity for a <see cref="Log"/>. <see cref="Log"/>s use these to keep a range of
  /// what types of messages they are able to log, and how they are logged to the
  /// <see cref="Console"/> or to a file.
  /// </summary>
  public class Level
  {
    /// <summary>A standard <see cref="Level"/> for extremely verbose <see cref="Log"/>s.
    /// <see cref="Log"/>s cannot access this by default.</summary>
    public static readonly Level Trace = new Level("Trace", 0, int.MaxValue, ConsoleColor.Green);
    /// <summary>A standard <see cref="Level"/> for useful debugging information.</summary>
    public static readonly Level Debug = new Level("Debug", 10);
    /// <summary>A standard <see cref="Level"/> for general runtime information.</summary>
    public static readonly Level Information = new Level("Information", 20);
    /// <summary>A standard <see cref="Level"/> for issues that don't stop execution.</summary>
    public static readonly Level Warning = new Level("Warning", 30, ConsoleColor.Yellow);
    /// <summary>A standard <see cref="Level"/> for issues that stop execution.</summary>
    public static readonly Level Error = new Level("Error", 40, ConsoleColor.Red);
    /// <summary>A standard <see cref="Level"/> for extreme issues, like system crashes.</summary>
    public static readonly Level Critical = new Level("Critical", 50, ConsoleColor.DarkRed);

    /// <summary>The name of this <see cref="Level"/>, typically printed to log files.</summary>
    public readonly string Name = null;
    /// <summary>The severity of this <see cref="Level"/>.</summary>
    public readonly int Severity = 0;
    /// <summary>The number of <see cref="StackFrame"/>s to acquire from the current
    /// <see cref="StackTrace"/>. Useful for any <see cref="Level"/>s that require trace
    /// information. By default, this value is 0 to disable <see cref="StackTrace"/>ing.</summary>
    public readonly int StackFrameCount = 0;

    /// <summary>A toggle to globally enable or disable this <see cref="Level"/>.</summary>
    public bool enabled = true;
    /// <summary>The <see cref="ConsoleColor"/> of the <see cref="Console"/>'s background.</summary>
    public ConsoleColor backgroundColor = ConsoleColor.Black;
    /// <summary>The <see cref="ConsoleColor"/> of the <see cref="Console"/>'s foreground.</summary>
    public ConsoleColor foregroundColor = ConsoleColor.White;

#nullable enable
#pragma warning disable IDE0003
    /// <summary>
    /// A constructor for a <see cref="Log"/> <see cref="Level"/>.
    /// </summary>
    /// <param name="name">See: <see cref="Name"/></param>
    /// <param name="severity">See: <see cref="Severity"/></param>
    public Level(string? name, int severity) : this(name, severity, 0) { }

    /// <summary>
    /// A constructor for a <see cref="Log"/> <see cref="Level"/>.
    /// </summary>
    /// <param name="name">See: <see cref="Name"/></param>
    /// <param name="severity">See: <see cref="Severity"/></param>
    /// <param name="stackFrameCount">See: <see cref="StackFrameCount"/></param>
    public Level(string? name, int severity, int stackFrameCount)
      : this(name, severity, stackFrameCount, ConsoleColor.White) { }

    /// <summary>
    /// A constructor for a <see cref="Log"/> <see cref="Level"/>.
    /// </summary>
    /// <param name="name">See: <see cref="Name"/></param>
    /// <param name="severity">See: <see cref="Severity"/></param>
    /// <param name="fgColor">See: <see cref="foregroundColor"/></param>
    public Level(string? name,  int severity, ConsoleColor fgColor)
      : this(name, severity, ConsoleColor.Black, fgColor) { }

    /// <summary>
    /// A constructor for a <see cref="Log"/> <see cref="Level"/>.
    /// </summary>
    /// <param name="name">See: <see cref="Name"/></param>
    /// <param name="severity">See: <see cref="Severity"/></param>
    /// <param name="stackFrameCount">See: <see cref="StackFrameCount"/></param>
    /// <param name="fgColor">See: <see cref="foregroundColor"/></param>
    public Level(string? name, int severity, int stackFrameCount, ConsoleColor fgColor)
      : this(name, severity, stackFrameCount, ConsoleColor.Black, fgColor) { }

    /// <summary>
    /// A constructor for a <see cref="Log"/> <see cref="Level"/>.
    /// </summary>
    /// <param name="name">See: <see cref="Name"/></param>
    /// <param name="severity">See: <see cref="Severity"/></param>
    /// <param name="bgColor">See: <see cref="backgroundColor"/></param>
    /// <param name="fgColor">See: <see cref="foregroundColor"/></param>
    public Level(string? name, int severity, ConsoleColor bgColor, ConsoleColor fgColor)
      : this(name, severity, 0, bgColor, fgColor) { }

    /// <summary>
    /// A constructor for a <see cref="Log"/> <see cref="Level"/>.
    /// </summary>
    /// <param name="name">See: <see cref="Name"/></param>
    /// <param name="severity">See: <see cref="Severity"/></param>
    /// <param name="stackFrameCount">See: <see cref="StackFrameCount"/></param>
    /// <param name="bgColor">See: <see cref="backgroundColor"/></param>
    /// <param name="fgColor">See: <see cref="foregroundColor"/></param>
    public Level(string? name, int severity, int stackFrameCount,
                 ConsoleColor bgColor, ConsoleColor fgColor)
    {
      this.Name = name;
      this.Severity = severity;
      this.StackFrameCount = stackFrameCount;
      this.backgroundColor = bgColor;
      this.foregroundColor = fgColor;
    }
#pragma warning restore IDE0003
#nullable disable
  }
  /************************************************************************************************/
}