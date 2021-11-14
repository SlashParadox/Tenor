/**************************************************************************************************/
/*!
\file   Log.cs
\author Craig Williams
\par    Last Updated
        2021-06-18
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for a class representing a debug logger that can be extended into multiple loggers.

\par Bug List
  CRITICAL
    * In Unity, the ErrorLog functionality does not work. This must be fixed by Unity.

\par References
*/
/**************************************************************************************************/

using SlashParadox.Tenor.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;

namespace SlashParadox.Tenor.Diagnostics
{
  /************************************************************************************************/
  /// <summary>
  /// An extendable debug logger. A custom <see cref="Log"/> <see langword="class"/> can be extended
  /// from this base to allow for handling errors or messages in custom ways depending on the
  /// system. To make one, a <see cref="Log"/> <see langword="class"/> must have a
  /// <see langword="static"/> constructor which creates a new instance of that
  /// <see langword="class"/>, and a <see langword="protected"/> or <see langword="private"/>
  /// constructor that calls the <see langword="base"/> constructor.
  /// </summary>
  /// <example>
  /// <code>
  /// public class MyLog
  /// {
  ///   static MyLog()
  ///   {
  ///     new MyLog();
  ///   }
  ///   
  ///   private MyLog() : base()
  ///   {
  ///     // Extra initialization here...
  ///   }
  /// }
  /// </code>
  /// </example>
  public abstract partial class Log
  {
    /// <summary>An event called when any message is logged.</summary>
    public static event EventHandler<LogArgs> OnAnyMessageLogged;

    /// <summary>An estimate of the space required of a <see cref="StackFrame"/> log.</summary>
    private static readonly int StringBuilderCapacityPerLine = 60;

    /// <summary>The default filepath for all <see cref="Log"/> files.</summary>
    public static string GlobalLogPath
      { get { return globalFilePath.OriginalPath; } set { globalFilePath.OriginalPath = value; } }
    /// <summary>The default <see cref="DateTime"/> formatting for all <see cref="Log"/>s.</summary>
    public static string GlobalDateFormat
      { get { return globalFilePath.DateFormat; } set { globalFilePath.DateFormat = value; } }
    /// <summary>The mode for the <see cref="errorLog"/>, and how it handles
    /// <see cref="Exception"/>s.</summary>
    public static ErrorLogMode ErrorMode { get { return errorLogMode; } set { SetErrorLogMode(value); } }
    /// <summary>The default <see cref="Level"/> for logging normal messages. Defaults to
    /// <see cref="Level.Information"/>.</summary>
    public static Level DefaultLogLevel
      { get { return defaultLogLevel; } set { SetDefaultLogLevel(value); } }
    /// <summary>The default <see cref="Level"/> for logging <see cref="Exception"/>s. Defaults to
    /// <see cref="Level.Critical"/>.</summary>
    public static Level DefaultExceptionLevel
      { get { return defaultExceptionLevel; } set { SetDefaultExceptionLevel(value); } }

    /// <summary>A <see cref="Dictionary{TKey, TValue}"/> of registered <see cref="Log"/>s.
    /// This keeps track of the sole instances of each <see cref="Log"/>.</summary>
    private static readonly Dictionary<Type, Log> Logs;
    /// <summary>The default <see cref="StackFrameParser"/> to use.</summary>
    private static readonly StackFrameParser FrameParser = new StackFrameParser();
    /// <summary>The real <see cref="FilePath"/> to use globally.</summary>
    private static FilePath globalFilePath = new FilePath(@"DevLogs/PlayerLog.txt");
    /// <summary>The sole <see cref="Log"/> that reads <see cref="Exception"/>s across the
    /// entire program. Only one <see cref="Log"/> can do this at a time to prevent spam.</summary>
    private static Log errorLog = null;
    /// <summary>See: <see cref="ErrorMode"/></summary>
    private static ErrorLogMode errorLogMode = ErrorLogMode.All;
    /// <summary>See: <see cref="DefaultLogLevel"/></summary>
    private static Level defaultLogLevel;
    /// <summary>See: <see cref="DefaultExceptionLevel"/></summary>
    private static Level defaultExceptionLevel;

    /// <summary>An event called when this <see cref="Log"/> logs a message.</summary>
    protected event EventHandler<LogArgs> OnMessageLogged;

    /// <summary>The range of <see cref="Level.Severity"/> that this <see cref="Log"/> can handle
    /// Both the minimum and maximum values are inclusive. By default, the minimum value is 1 to
    /// prevent <see cref="Level.Trace"/> severities and below.</summary>
    protected ValueTuple<int, int> SeverityRange { get { return severityRange; } }
    /// <summary>The unique filepath for this <see cref="Log"/>'s file.</summary>
    protected string LogPath
    { get { return userFilePath.OriginalPath; } set { userFilePath.OriginalPath = value; } }
    /// <summary>The unique <see cref="DateTime"/> formatting for this <see cref="Log"/>.</summary>
    protected string DateFormat
    { get { return userFilePath.DateFormat; } set { userFilePath.DateFormat = value; } }

    /// <summary>The method that this <see cref="Log"/> uses to log messages.</summary>
    protected MessageMode messageMode = MessageMode.ConsoleOnly;
    /// <summary>A toggle for using the <see cref="GlobalLogPath"/> or the individual
    /// <see cref="LogPath"/> when logging to a file.</summary>
    protected bool useGlobalLogPath = true;
    /// <summary>A toggle for only allowing this <see cref="Log"/> to run if a 'DEBUG' preprocessor
    /// is defined.</summary>
    protected bool debugBuildOnly = true;

    /// <summary>See: <see cref="SeverityRange"/></summary>
    private ValueTuple<int, int> severityRange = new ValueTuple<int, int>(1, int.MaxValue);
    /// <summary>The <see cref="StringBuilder"/> being used to build out the trace log.</summary>
    private StringBuilder traceBuilder = null;
    /// <summary>This <see cref="Log"/>'s unique <see cref="FilePath"/>.</summary>
    private readonly FilePath userFilePath;
    /// <summary>A check for if this <see cref="Log"/> tries to log a new message to a file when
    /// one is already being processed. This is useful for preventing infinite loops when logging
    /// an IO error.</summary>
    private bool isProcessingFile = false;

    /// <summary>
    /// A static constructor for a <see cref="Log"/>.
    /// </summary>
    /// <remarks>Every <see cref="Log"/> <see langword="class"/> must have a
    /// <see langword="static"/> constructor that creates an instance of itself.
    /// The constructor should then call the base constructor (See: <see cref="Log()"/>).</remarks>
    /// <example>
    /// <code>
    /// static MyLog()
    /// {
    ///   new MyLog(); // The only line required.
    ///   // Extra initialization here...
    /// }
    /// </code>
    /// </example>
    static Log()
    {
      Logs = new Dictionary<Type, Log>();

      // Enforce that the default Levels are set.
      SetDefaultLogLevel(null);
      SetDefaultExceptionLevel(null);
    }

    /// <summary>
    /// A constructor for a <see cref="Log"/>.
    /// </summary>
    /// <remarks>No <see cref="Log"/> <see langword="class"/> should have a <see langword="public"/>
    /// constructor. Instead, they should be <see langword="protected"/> or
    /// <see langword="private"/>, and must call the <see langword="base"/> constructor.</remarks>
    /// <example>
    /// <code>
    /// private MyLog() : base()
    /// {
    ///   // Extra initialization here...
    /// }
    /// </code>
    /// </example>
    protected Log()
    {
      userFilePath = new FilePath(GlobalLogPath); // Set the default FilePath.
      Register(); // Register this Log, if it has not been already.
    }

    /// <summary>
    /// A function for attempting to ensure that a type of <see cref="Log"/> is registered for use.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Log"/>.</typeparam>
    /// <returns>Returns if the <see cref="Log"/> was sucessfully registered.</returns>
    public static bool EnsureRegistry<T>() where T : Log
    {
      // Get the type and run its static constructor. This is costless for registered Logs.
      Type type = typeof(T);
      return EnsureRegistry(type);
    }

    /// <summary>
    /// A function for checking if a given <see cref="Level.Severity"/> is within the range that
    /// a <see cref="Log"/> can handle.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Log"/>.</typeparam>
    /// <param name="severity">The severity to check.</param>
    /// <returns>Returns if the <paramref name="severity"/> is within range.</returns>
    public static bool IsWithinSeverityRange<T>(int severity) where T : Log
    {
      return EnsureRegistry(typeof(T), out Log log) && log.IsWithinSeverityRange(severity);
    }

    /// <summary>
    /// A function for logging a given <paramref name="message"/> to some
    /// <see cref="Log"/>.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of the <see cref="Log"/> to use.</typeparam>
    /// <param name="message">The <see cref="string"/> to log.</param>
    public static void LogMessage<T>(string message) where T : Log
    {
      LogMessage<T>(message, defaultLogLevel);
    }

    /// <summary>
    /// A function for logging a given <paramref name="message"/> to some
    /// <see cref="Log"/>.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of the <see cref="Log"/> to use.</typeparam>
    /// <param name="message">The <see cref="string"/> to log.</param>
    /// <param name="level">The <see cref="Level"/> of severity.</param>
    public static void LogMessage<T>(string message, Level level) where T : Log
    {
      // Make sure that the log is valid. If so, log the message.
      if (IsValidLogOperation<T>(message, level, out Log log))
        log.LogMessageInternal(message, level);
    }

    /// <summary>
    /// A function for logging a given <see cref="Exception"/> to some <see cref="Log"/>.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of the <see cref="Log"/> to use.</typeparam>
    /// <param name="exception">The <see cref="Exception"/> to log.</param>
    /// <param name="useExceptionTrace">A toggle for printing the <paramref name="exception"/>'s
    /// <see cref="StackTrace"/>.</param>
    public static void LogException<T>(Exception exception,
                                       bool useExceptionTrace = true) where T : Log
    {
      // Make sure that the log is valid. If so, log the message.
      if (exception != null && IsValidLogOperation<T>(out Log log))
        log.LogExceptionInternal(string.Empty, exception, useExceptionTrace, defaultExceptionLevel);
    }

    /// <summary>
    /// A function for logging a given <see cref="Exception"/> to some <see cref="Log"/>.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of the <see cref="Log"/> to use.</typeparam>
    /// <param name="exception">The <see cref="Exception"/> to log.</param>
    /// <param name="level">The <see cref="Level"/> of severity.</param>
    /// <param name="useExceptionTrace">A toggle for printing the <paramref name="exception"/>'s
    /// <see cref="StackTrace"/>.</param>
    public static void LogException<T>(Exception exception, Level level,
                                       bool useExceptionTrace = true) where T : Log
    {
      // Make sure that the log is valid. If so, log the message.
      if (exception != null && IsValidLogOperation<T>(string.Empty, level, out Log log))
        log.LogExceptionInternal(string.Empty, exception, useExceptionTrace, level);
    }

    /// <summary>
    /// A function for logging a given <see cref="Exception"/> to some <see cref="Log"/>.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of the <see cref="Log"/> to use.</typeparam>
    /// <param name="exception">The <see cref="Exception"/> to log.</param>
    /// <param name="message">An extra <see cref="string"/> to log.</param>
    /// <param name="useExceptionTrace">A toggle for printing the <paramref name="exception"/>'s
    /// <see cref="StackTrace"/>.</param>
    public static void LogException<T>(Exception exception, string message,
                                       bool useExceptionTrace = true) where T : Log
    {
      // Make sure that the log is valid. If so, log the message.
      if (exception != null && IsValidLogOperation<T>(message, out Log log))
        log.LogExceptionInternal(message, exception, useExceptionTrace, defaultExceptionLevel);
    }

    /// <summary>
    /// A function for logging a given <see cref="Exception"/> to some <see cref="Log"/>.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of the <see cref="Log"/> to use.</typeparam>
    /// <param name="exception">The <see cref="Exception"/> to log.</param>
    /// <param name="message">An extra <see cref="string"/> to log.</param>
    /// <param name="level">The <see cref="Level"/> of severity.</param>
    /// <param name="useExceptionTrace">A toggle for printing the <paramref name="exception"/>'s
    /// <see cref="StackTrace"/>.</param>
    public static void LogException<T>(Exception exception, string message, Level level,
                                       bool useExceptionTrace = true) where T : Log
    {
      // Make sure that the log is valid. If so, log the message.
      if (exception != null && IsValidLogOperation<T>(message, level, out Log log))
        log.LogExceptionInternal(message, exception, useExceptionTrace, level);
    }

    /// <summary>
    /// A function for logging a given <see cref="Exception"/> to some <see cref="Log"/>, and then
    /// immediately throw it. If the <paramref name="exception"/> has already been thrown, it is
    /// captured in <see cref="ExceptionDispatchInfo"/> to prevent overwriting.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of the <see cref="Log"/> to use.</typeparam>
    /// <param name="exception">The <see cref="Exception"/> to log.</param>
    /// <param name="useExceptionTrace">A toggle for printing the <paramref name="exception"/>'s
    /// <see cref="StackTrace"/>.</param>
    public static void LogAndThrow<T>(Exception exception, bool useExceptionTrace = true)
                                      where T : Log
    {
      // Make sure that the log is valid. If so, log the message.
      if (exception != null)
      {
        if (IsValidLogOperation<T>(out Log log))
          log.LogExceptionInternal(string.Empty, exception, useExceptionTrace, defaultExceptionLevel);

        ExceptionDispatchInfo.Capture(exception).Throw();
      }
    }

    /// <summary>
    /// A function for logging a given <see cref="Exception"/> to some <see cref="Log"/>, and then
    /// immediately throw it. If the <paramref name="exception"/> has already been thrown, it is
    /// captured in <see cref="ExceptionDispatchInfo"/> to prevent overwriting.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of the <see cref="Log"/> to use.</typeparam>
    /// <param name="exception">The <see cref="Exception"/> to log.</param>
    /// <param name="level">The <see cref="Level"/> of severity.</param>
    /// <param name="useExceptionTrace">A toggle for printing the <paramref name="exception"/>'s
    /// <see cref="StackTrace"/>.</param>
    public static void LogAndThrow<T>(Exception exception, Level level,
                                      bool useExceptionTrace = true) where T : Log
    {
      // Make sure that the log is valid. If so, log the message.
      if (exception != null)
      {
        if (IsValidLogOperation<T>(string.Empty, level, out Log log))
          log.LogExceptionInternal(string.Empty, exception, useExceptionTrace, level);

        ExceptionDispatchInfo.Capture(exception).Throw();
      }
    }

    /// <summary>
    /// A function for logging a given <see cref="Exception"/> to some <see cref="Log"/>, and then
    /// immediately throw it. If the <paramref name="exception"/> has already been thrown, it is
    /// captured in <see cref="ExceptionDispatchInfo"/> to prevent overwriting.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of the <see cref="Log"/> to use.</typeparam>
    /// <param name="exception">The <see cref="Exception"/> to log.</param>
    /// <param name="message">An extra <see cref="string"/> to log.</param>
    /// <param name="useExceptionTrace">A toggle for printing the <paramref name="exception"/>'s
    /// <see cref="StackTrace"/>.</param>
    public static void LogAndThrow<T>(Exception exception, string message,
                                      bool useExceptionTrace = true) where T : Log
    {
      // Make sure that the log is valid. If so, log the message.
      if (exception != null)
      {
        if (IsValidLogOperation<T>(message, out Log log))
          log.LogExceptionInternal(message, exception, useExceptionTrace, defaultExceptionLevel);

        ExceptionDispatchInfo.Capture(exception).Throw();
      }
    }

    /// <summary>
    /// A function for logging a given <see cref="Exception"/> to some <see cref="Log"/>, and then
    /// immediately throw it. If the <paramref name="exception"/> has already been thrown, it is
    /// captured in <see cref="ExceptionDispatchInfo"/> to prevent overwriting.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of the <see cref="Log"/> to use.</typeparam>
    /// <param name="exception">The <see cref="Exception"/> to log.</param>
    /// <param name="message">An extra <see cref="string"/> to log.</param>
    /// <param name="level">The <see cref="Level"/> of severity.</param>
    /// <param name="useExceptionTrace">A toggle for printing the <paramref name="exception"/>'s
    /// <see cref="StackTrace"/>.</param>
    public static void LogAndThrow<T>(Exception exception, string message, Level level,
                                      bool useExceptionTrace = true) where T : Log
    {
      // Make sure that the log is valid. If so, log the message.
      if (exception != null )
      {
        if (IsValidLogOperation<T>(message, level, out Log log))
          log.LogExceptionInternal(message, exception, useExceptionTrace, level);

        ExceptionDispatchInfo.Capture(exception).Throw();
      }
    }

    /// <summary>
    /// A function for logging some <paramref name="message"/> to the <see cref="Console"/>.
    /// This does not use any actual <see cref="Log"/>.
    /// </summary>
    /// <param name="message">The <see cref="string"/> to print.</param>
    /// <param name="fgColor">The <see cref="ConsoleColor"/> for the foreground/text.</param>
    /// <param name="bgColor">The <see cref="ConsoleColor"/> for the background.</param>
    public static void LogToConsole(string message, ConsoleColor fgColor = ConsoleColor.White,
                                    ConsoleColor bgColor = ConsoleColor.Black)
    {
      if (message != null)
      {
        LogToConsoleInternal(message, bgColor, fgColor);

        LogArgs args = new LogArgs(message, message, string.Empty, message, defaultLogLevel);
        OnAnyMessageLogged?.Invoke(null, args);
      }
    }

    /// <summary>
    /// A function for logging some <paramref name="message"/> to the <see cref="Console"/>.
    /// This does not use any actual <see cref="Log"/>.
    /// </summary>
    /// <param name="message">The <see cref="string"/> to print.</param>
    /// <param name="level">The <see cref="Level"/> of severity.</param>
    public static void LogToConsole(string message, Level level)
    {
      if (message != null && level != null)
      {
        LogToConsoleInternal(message, level.foregroundColor, level.backgroundColor);

        LogArgs args = new LogArgs(message, message, string.Empty, message, level);
        OnAnyMessageLogged?.Invoke(null, args);
      }
    }

    /// <summary>
    /// A getter for a <see cref="Log"/>'s <see cref="MessageMode"/>.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Log"/>.</typeparam>
    /// <returns>Returns the <see cref="Log"/>'s <see cref="messageMode"/>.</returns>
    public static MessageMode GetMessageMode<T>()
    {
      if (EnsureRegistry(typeof(T), out Log log))
        return log.messageMode;

      return MessageMode.Off;
    }

    /// <summary>
    /// A setter for a <see cref="Log"/>'s <see cref="MessageMode"/>.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Log"/>.</typeparam>
    /// <param name="mode">The new <see cref="MessageMode"/>.</param>
    public static void SetMessageMode<T>(MessageMode mode)
    {
      if (EnsureRegistry(typeof(T), out Log log))
        log.messageMode = mode;
    }

    /// <summary>
    /// A getter for a <see cref="Log"/>'s <see cref="LogPath"/>.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Log"/>.</typeparam>
    /// <returns>Returns the <see cref="Log"/>'s <see cref="LogPath"/>.</returns>
    public static string GetLogPath<T>()
    {
      if (EnsureRegistry(typeof(T), out Log log))
        return log.LogPath;

      return string.Empty;
    }

    /// <summary>
    /// A setter for a <see cref="Log"/>'s <see cref="LogPath"/>.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Log"/>.</typeparam>
    /// <param name="path">The new <see cref="LogPath"/>.</param>
    public static void SetLogPath<T>(string path)
    {
      if (EnsureRegistry(typeof(T), out Log log))
        log.LogPath = path;
    }

    /// <summary>
    /// A getter for a <see cref="Log"/>'s <see cref="useGlobalLogPath"/> status.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Log"/>.</typeparam>
    /// <returns>Returns the <see cref="Log"/>'s <see cref="useGlobalLogPath"/> status.</returns>
    public static bool GetGlobalPathUsage<T>()
    {
      if (EnsureRegistry(typeof(T), out Log log))
        return log.useGlobalLogPath;

      return true;
    }

    /// <summary>
    /// A setter for a <see cref="Log"/>'s <see cref="useGlobalLogPath"/> status.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Log"/>.</typeparam>
    /// <param name="useGlobalPath">The new <see cref="useGlobalLogPath"/> status.</param>
    public static void SetGlobalPathUsage<T>(bool useGlobalPath)
    {
      if (EnsureRegistry(typeof(T), out Log log))
        log.useGlobalLogPath = useGlobalPath;
    }

    /// <summary>
    /// A getter for a <see cref="Log"/>'s <see cref="DateFormat"/>.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Log"/>.</typeparam>
    /// <returns>Returns the <see cref="Log"/>'s <see cref="DateFormat"/>.</returns>
    public static string GetDateFormat<T>()
    {
      if (EnsureRegistry(typeof(T), out Log log))
        return log.DateFormat;

      return string.Empty;
    }

    /// <summary>
    /// A setter for a <see cref="Log"/>'s <see cref="DateFormat"/>.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Log"/>.</typeparam>
    /// <param name="format">The new <see cref="DateFormat"/>.</param>
    public static void SetDateFormat<T>(string format)
    {
      if (EnsureRegistry(typeof(T), out Log log))
        log.DateFormat = format;
    }

    /// <summary>
    /// A getter for a <see cref="Log"/>'s <see cref="debugBuildOnly"/> status.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Log"/>.</typeparam>
    /// <returns>Returns the <see cref="Log"/>'s <see cref="debugBuildOnly"/> status.</returns>
    public static bool GetDebugOnlyStatus<T>()
    {
      if (EnsureRegistry(typeof(T), out Log log))
        return log.debugBuildOnly;

      return true;
    }

    /// <summary>
    /// A setter for a <see cref="Log"/>'s <see cref="debugBuildOnly"/> status.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Log"/>.</typeparam>
    /// <param name="debugOnly">The new <see cref="debugBuildOnly"/> status.</param>
    public static void SetDebugOnlyStatus<T>(bool debugOnly)
    {
      if (EnsureRegistry(typeof(T), out Log log))
        log.debugBuildOnly = debugOnly;
    }

    /// <summary>
    /// A function for getting the minimum severity in the <see cref="SeverityRange"/>
    /// of a <see cref="Log"/>.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Log"/>.</typeparam>
    /// <returns>Returns the <see cref="Log"/>'s minimum severity level.</returns>
    public static int GetMinSeverity<T>() where T : Log
    {
      if (EnsureRegistry(typeof(T), out Log log))
        return log.severityRange.Item1;

      return 1;
    }

    /// <summary>
    /// A function for getting the maximum severity in the <see cref="SeverityRange"/>
    /// of a <see cref="Log"/>.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Log"/>.</typeparam>
    /// <returns>Returns the <see cref="Log"/>'s maximum severity level.</returns>
    public static int GetMaxSeverity<T>() where T : Log
    {
      if (EnsureRegistry(typeof(T), out Log log))
        return log.severityRange.Item2;

      return int.MaxValue;
    }

    /// <summary>
    /// A function for getting the <see cref="SeverityRange"/> of a <see cref="Log"/>.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Log"/>.</typeparam>
    /// <param name="min">The minimum <see cref="Level"/> severity that's allowed.</param>
    /// <param name="max">The maximum <see cref="Level"/> severity that's allowed.</param>
    public static void GetSeverityRange<T>(out int min, out int max) where T : Log
    {
      if (EnsureRegistry(typeof(T), out Log log))
      {
        min = log.severityRange.Item1;
        max = log.severityRange.Item2;
      }

      min = 1;
      max = int.MaxValue;
    }

    /// <summary>
    /// A function for setting the minimum severity in the <see cref="SeverityRange"/>
    /// of a <see cref="Log"/>.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Log"/>.</typeparam>
    /// <param name="severity">The new minimum <see cref="Level"/> severity.</param>
    public static void SetMinSeverity<T>(int severity) where T : Log
    {
      if (EnsureRegistry(typeof(T), out Log log))
        log.SetMinSeverity(severity);
    }

    /// <summary>
    /// A function for setting the maximum severity in the <see cref="SeverityRange"/>
    /// of a <see cref="Log"/>.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Log"/>.</typeparam>
    /// <param name="severity">The new maximum <see cref="Level"/> severity.</param>
    public static void SetMaxSeverity<T>(int severity) where T : Log
    {
      if (EnsureRegistry(typeof(T), out Log log))
        log.SetMaxSeverity(severity);
    }

    /// <summary>
    /// A function for setting the <see cref="SeverityRange"/> of a <see cref="Log"/>.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Log"/>.</typeparam>
    /// <param name="min">The minimum <see cref="Level"/> severity to allow.</param>
    /// <param name="max">The maximum <see cref="Level"/> severity to allow.</param>
    public static void SetSeverityRange<T>(int min, int max) where T : Log
    {
      if (EnsureRegistry(typeof(T), out Log log))
        log.SetSeverityRange(min, max);
    }

    /// <summary>
    /// A function for setting a type of <see cref="Log"/> as the sole <see cref="Log"/> that
    /// catches the <see cref="AppDomain.CurrentDomain"/>'s <see cref="Exception"/>
    /// <see langword="event"/>s.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Log"/>.</typeparam>
    /// <remarks>Also see <see cref="ErrorLogMode"/> and <see cref="ErrorMode"/>.</remarks>
    public static void SetErrorLog<T>() where T : Log
    {
      // Make sure the Log is registered.
      if (EnsureRegistry(typeof(T), out Log log))
      {
        // If there is a previous Log, unbind it's events.
        if (errorLog != null)
          UnbindErrorLog();

        // Set the new Log and rebind the events.
        errorLog = log;
        BindErrorLog();
      }
    }

    /// <summary>
    /// A function for unbinding the <see cref="errorLog"/> from the <see cref="Exception"/>
    /// <see langword="event"/>s.
    /// </summary>
    private static void UnbindErrorLog()
    {
      switch (errorLogMode)
      {
        case ErrorLogMode.Unhandled:
          AppDomain.CurrentDomain.UnhandledException -= errorLog.OnUnhandledException;
          break;
        case ErrorLogMode.All:
          AppDomain.CurrentDomain.UnhandledException -= errorLog.OnUnhandledException;
          AppDomain.CurrentDomain.FirstChanceException -= errorLog.OnFirstChanceException;
              break;
        default:
          break;
      }
    }

    /// <summary>
    /// A function for binding the <see cref="errorLog"/> to the <see cref="Exception"/>
    /// <see langword="event"/>s.
    /// </summary>
    private static void BindErrorLog()
    {
      switch (errorLogMode)
      {
        case ErrorLogMode.Unhandled:
          AppDomain.CurrentDomain.UnhandledException += errorLog.OnUnhandledException;
          break;
        case ErrorLogMode.All:
          AppDomain.CurrentDomain.UnhandledException += errorLog.OnUnhandledException;
          AppDomain.CurrentDomain.FirstChanceException += errorLog.OnFirstChanceException;
          break;
        default:
          break;
      }
    }

    /// <summary>
    /// A function for setting how the <see cref="errorLog"/> handles <see cref="Exception"/>
    /// <see langword="events"/>s.
    /// </summary>
    /// <param name="mode"></param>
    private static void SetErrorLogMode(ErrorLogMode mode)
    {
      // If a Log exists, unbind the events, set the mode, and rebind. Otherwise, just set the mode.
      if (errorLog != null)
      {
        UnbindErrorLog();
        errorLogMode = mode;
        BindErrorLog();
      }
      else
        errorLogMode = mode;
    }

    /// <summary>
    /// A function for attempting to ensure that a type of <see cref="Log"/> is registered for use.
    /// </summary>
    /// <param name="type">The <see cref="Log"/>'s <see cref="Type"/>.</param>
    /// <returns>Returns if the <see cref="Log"/> was sucessfully registered.</returns>
    private static bool EnsureRegistry(Type type)
    {
      // Run the type's static constructor. This is costless for registered Logs.
      RuntimeHelpers.RunClassConstructor(type.TypeHandle);
      return Logs.ContainsKey(type);
    }

    /// <summary>
    /// A function for attempting to ensure that a type of <see cref="Log"/> is registered for use.
    /// </summary>
    /// <param name="type">The <see cref="Log"/>'s <see cref="Type"/>.</param>
    /// <param name="log">The <see cref="Log"/>, if it is registered.</param>
    /// <returns>Returns if the <see cref="Log"/> was sucessfully registered.</returns>
    private static bool EnsureRegistry(Type type, out Log log)
    {
      // Run the type's static constructor. This is costless for registered Logs.
      if (EnsureRegistry(type))
      {
        log = Logs[type];
        return true;
      }

      log = null;
      return false;
    }

    /// <summary>
    /// A helper function for determining if the current logging operation is valid by checking
    /// each of the parameters.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of the <see cref="Log"/>.</typeparam>
    /// <param name="log">The <see cref="Log"/>, if one is registered and valid.</param>
    /// <returns>Returns if the entire series of parameters is valid.</returns>
    private static bool IsValidLogOperation<T>(out Log log) where T : Log
    {
      log = null;
      Type type = typeof(T); // We know that the Type is a Log no matter what.

      // Make sure that the Type is registered.
      if (!EnsureRegistry(type))
        return false;

      Log checkLog = Logs[type]; // Get the registered Log.

#if !DEBUG
        // In Release builds, Debug-Only Logs cannot run.
        if (checkLog.debugBuildOnly)
          return false;
#endif

      // The Log must actually be turned on in order to be used.
      if (checkLog.messageMode == MessageMode.Off)
        return false;

      // All checks have passed, so return true.
      log = checkLog;
      return true;
    }

    /// <summary>
    /// A helper function for determining if the current logging operation is valid by checking
    /// each of the parameters.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of the <see cref="Log"/>.</typeparam>
    /// <param name="message">The <see cref="string"/> to log.</param>
    /// <param name="log">The <see cref="Log"/>, if one is registered and valid.</param>
    /// <returns>Returns if the entire series of parameters is valid.</returns>
    private static bool IsValidLogOperation<T>(string message, out Log log) where T : Log
    {
      // The message cannot be null in order to be logged.
      if (message != null)
        return IsValidLogOperation<T>(out log);

      // The checks did not pass, so return false.
      log = null;
      return false;
    }

    /// <summary>
    /// A helper function for determining if the current logging operation is valid by checking
    /// each of the parameters.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of the <see cref="Log"/>.</typeparam>
    /// <param name="message">The <see cref="string"/> to log.</param>
    /// <param name="level">The <see cref="Level"/> of the <paramref name="message"/>.</param>
    /// <param name="log">The <see cref="Log"/>, if one is registered and valid.</param>
    /// <returns>Returns if the entire series of parameters is valid.</returns>
    private static bool IsValidLogOperation<T>(string message, Level level, out Log log)
      where T : Log
    {
      // The Level must not be null, must be enabled, and the Log and message must be valid.
      if (level != null && level.enabled && IsValidLogOperation<T>(message, out log))
      {
        // The Log must also be able to handle the severity of the Level.
        if (log.IsWithinSeverityRange(level.Severity))
          return true;
      }

      // The checks did not pass, so return false.
      log = null;
      return false;
    }

    /// <summary>
    /// An internal function for logging some <paramref name="message"/> to the
    /// <see cref="Console"/>. This does not use any actual <see cref="Log"/>.
    /// </summary>
    /// <param name="message">The <see cref="string"/> to print.</param>
    /// <param name="bgColor">The <see cref="ConsoleColor"/> for the background.</param>
    /// <param name="fgColor">The <see cref="ConsoleColor"/> for the foreground/text.</param>
    private static void LogToConsoleInternal(string message, ConsoleColor bgColor,
                                             ConsoleColor fgColor)
    {
      Console.BackgroundColor = bgColor;
      Console.ForegroundColor = fgColor;
      Console.WriteLine(message);
      Console.ResetColor();
    }

    /// <summary>
    /// A helper function for setting the <see cref="defaultLogLevel"/>.
    /// </summary>
    /// <param name="level">The <see cref="Level"/> to set.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void SetDefaultLogLevel(Level level)
    {
      defaultLogLevel = level ?? Level.Information;
    }

    /// <summary>
    /// A helper function for setting the <see cref="defaultExceptionLevel"/>.
    /// </summary>
    /// <param name="level">The <see cref="Level"/> to set.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void SetDefaultExceptionLevel(Level level)
    {
      // Only set if the level is not null.
      defaultExceptionLevel = level ?? Level.Critical;
    }

    /// <summary>
    /// A function for checking if a given <see cref="Level.Severity"/> is within the range that
    /// this <see cref="Log"/> can handle.
    /// </summary>
    /// <param name="severity">The severity to check.</param>
    /// <returns>Returns if the <paramref name="severity"/> is within range.</returns>
    protected bool IsWithinSeverityRange(int severity)
    {
      return severity >= severityRange.Item1 && severity <= severityRange.Item2;
    }

    /// <summary>
    /// A function for setting the minimum severity in the <see cref="SeverityRange"/>.
    /// </summary>
    /// <param name="severity">The new minimum <see cref="Level"/> severity.</param>
    protected void SetMinSeverity(int severity)
    {
      severityRange.Item1 = severity; 
    }

    /// <summary>
    /// A function for setting the maximum severity in the <see cref="SeverityRange"/>.
    /// </summary>
    /// <param name="severity">The new maximum <see cref="Level"/> severity.</param>
    protected void SetMaxSeverity(int severity)
    {
      // The maximum must be greater than the minimum.
      severityRange.Item2 = severity < severityRange.Item1 ? severityRange.Item1 : severity;
    }

    /// <summary>
    /// A function for setting the <see cref="SeverityRange"/>.
    /// </summary>
    /// <param name="min">The minimum <see cref="Level"/> severity to allow.</param>
    /// <param name="max">The maximum <see cref="Level"/> severity to allow.</param>
    protected void SetSeverityRange(int min, int max)
    {
      SetMinSeverity(min);
      SetMaxSeverity(max);
    }

    /// <summary>
    /// A helper function for getting the <see cref="LogArgs"/> for a given <see cref="Log"/>.
    /// </summary>
    /// <param name="message">The <see cref="string"/> to log.</param>
    /// <param name="level">The <see cref="Level"/> of severity.</param>
    /// <param name="logArgs">The final <see cref="LogArgs"/>.</param>
    private void GetLogArgs(string message, Level level, out LogArgs logArgs)
    {
      // Get a formatted message.
      string fMessage = FormatMessage(message);

      // Get a formatted StackTrace. This already will not be null.
      string fStackTrace = GetStackTrace(level);

      // Start making the full message.
      string fullMessage = string.Empty;

      // Only add the formatted message if it is not null or empty.
      if (!string.IsNullOrEmpty(fMessage))
        fullMessage += fMessage;

      // Only add the formatted StackTrace if it is not null or empty.
      if (!string.IsNullOrEmpty(fStackTrace))
        fullMessage += $"\n{fStackTrace}";

      // Create the LogArgs.
      logArgs = new LogArgs(message, fMessage, fStackTrace, fullMessage, level);
    }

    /// <summary>
    /// A helper function for getting the <see cref="LogArgs"/> for a given <see cref="Log"/>.
    /// </summary>
    /// <param name="message">The <see cref="string"/> to log.</param>
    /// <param name="level">The <see cref="Level"/> of severity.</param>
    /// <param name="exception">The <see cref="Exception"/> to log.</param>
    /// <param name="showExceptionTrace">A toggle for using the <paramref name="exception"/>'s
    /// <see cref="StackTrace"/>.</param>
    /// <param name="logArgs">The final <see cref="LogArgs"/>.</param>
    private void GetLogArgs(string message, Level level, Exception exception,
                            bool showExceptionTrace, out LogArgs logArgs)
    {
      // Get a formatted message.
      string fMessage = FormatMessage(message);

      // Get the Exception's message.
      string eMessage = exception.GetType().Name + ": " + exception.Message;

      // Get the Exception's StackTrace, if applicable.
      string eStackTrace = string.Empty;
      string fStackTrace = string.Empty;

      if (showExceptionTrace)
      {
        eStackTrace = exception.StackTrace;

        // If the Exception has not been thrown, force a StackTrace to be built.
        if (string.IsNullOrEmpty(eStackTrace))
          eStackTrace = GetStackTrace(int.MaxValue, 4);
      }
      else
        fStackTrace = GetStackTrace(level); // Get a regular formatted StackTrace, if applicable.

      // Start making the full message.
      string fullMessage = string.Empty;

      // Only add the formatted message if it is not null or empty.
      if (!string.IsNullOrEmpty(fMessage))
      {
        fullMessage += fMessage;

        // Only add the Exception's message if it is not null or empty.
        if (!string.IsNullOrEmpty(eMessage))
          fullMessage += $"\n  {eMessage}";
      }
      else if (!string.IsNullOrEmpty(eMessage)) // Alternatively, start with the exception.
        fullMessage += $"{eMessage}";

      // Only add the Exception's StackTrace if it is not null or empty.
      if (!string.IsNullOrEmpty(eStackTrace))
        fullMessage += $"\n{eStackTrace}";

      // Only add the formatted StackTrace if it is not null or empty.
      if (!string.IsNullOrEmpty(fStackTrace))
        fullMessage += $"\n{fStackTrace}";

      // Create the LogArgs.
      logArgs = new LogArgs(message, fMessage, fStackTrace, fullMessage, level);
    }

    /// <summary>
    /// A function for getting the current <see cref="StackTrace"/>, while removing any frames it
    /// took to get to this function.
    /// </summary>
    /// <param name="level">The <see cref="Level"/> whose settings are used to get the
    /// <see cref="StackTrace"/>.</param>
    /// <returns>Returns the final <see cref="StackTrace"/> <see cref="string"/>. If the
    /// <paramref name="level"/> does not allow any trace, returns
    /// <see cref="string.Empty"/> instead.</returns>
    private string GetStackTrace(Level level)
    {
      // If there are no frames to add, return an empty string.
      if (level.StackFrameCount <= 0)
        return string.Empty;

      // Otherwise, get the stack trace based on the frame count.
      return GetStackTrace(level.StackFrameCount);
    }

    /// <summary>
    /// A function for getting the current <see cref="StackTrace"/>, while removing any frames it
    /// took to get to this function.
    /// </summary>
    /// <param name="frameCount">The number of <see cref="StackFrame"/>s to process.</param>
    /// <param name="frameSkip">The number of <see cref="StackFrame"/>s to skip. This should be the
    /// number of functions it took to get to this point.</param>
    /// <returns>Returns the final <see cref="StackTrace"/> <see cref="string"/>.</returns>
    private string GetStackTrace(int frameCount, int frameSkip = 5)
    {
      // Get the stack trace, skipping the frames it took to get to this function.
      StackTrace st = new StackTrace(frameSkip, true);
      int count = Maths.Min(frameCount, st.FrameCount);

      // Initialize a builder with a rough estimate to the size of the final string.
      traceBuilder = new StringBuilder(count * StringBuilderCapacityPerLine);

      for (int i = 0; i < count; i++)
      {
        StackFrame frame = st.GetFrame(i); // Get the current frame.

        // Format the frame and ensure that it is not null.
        string str = FormatStackFrame(frame);
        Texts.NullToEmpty(ref str);

        traceBuilder.AppendLine(str); // Append the StackFrame string.
      }

      return traceBuilder.ToString(); // Return the final string.
    }

    /// <summary>
    /// A helper function for handling processing a <paramref name="message"/> based on the
    /// <see cref="messageMode"/>.
    /// </summary>
    /// <param name="message">The <see cref="string"/> to log.</param>
    /// <param name="level">The <see cref="Level"/> of severity.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void HandleMessageModeLogging(string message, Level level)
    {
      // Perform differently based on the messaging mode.
      switch (messageMode)
      {
        case MessageMode.ConsoleOnly:
          LogToConsoleInternal(message, level.foregroundColor, level.backgroundColor);
          break;
        case MessageMode.FileOnly:
          LogToFileInternal(message, level);
          break;
        case MessageMode.ConsoleAndFile:
          LogToConsoleInternal(message, level.foregroundColor, level.backgroundColor);
          LogToFileInternal(message, level);
          break;
      }
    }

    /// <summary>
    /// An internal function for logging a given <paramref name="message"/> to this
    /// <see cref="Log"/>.
    /// </summary>
    /// <param name="message">The <see cref="string"/> to log.</param>
    /// <param name="level">The <see cref="Level"/> of severity.</param>
    private void LogMessageInternal(string message, Level level)
    {
      GetLogArgs(message, level, out LogArgs logArgs); // Get the LogArgs, which house the strings.

      HandleMessageModeLogging(logArgs.FullMessage, level); // Log based on the MessageMode.

      // Invoke the message logging events.
      OnAnyMessageLogged?.Invoke(null, logArgs);
      OnMessageLogged?.Invoke(null, logArgs);
    }


    /// <summary>
    /// An internal function for logging a given <see cref="Exception"/> to some <see cref="Log"/>.
    /// </summary>
    /// <param name="message">The <see cref="string"/> to log.</param>
    /// <param name="exception">The <see cref="Exception"/> to log.</param>
    /// <param name="useExceptionTrace">A toggle for using the <paramref name="exception"/>'s
    /// <see cref="StackTrace"/>.</param>
    /// <param name="level">The <see cref="Level"/> of severity.</param>
    private void LogExceptionInternal(string message, Exception exception, bool useExceptionTrace,
                                      Level level)
    {
      // Get the LogArgs, which house the strings.
      GetLogArgs(message, level, exception, useExceptionTrace, out LogArgs logArgs);

      HandleMessageModeLogging(logArgs.FullMessage, level); // Log based on the MessageMode.

      // Invoke the message logging events.
      OnAnyMessageLogged?.Invoke(null, logArgs);
      OnMessageLogged?.Invoke(null, logArgs);
    }

    /// <summary>
    /// A function for registering a <see cref="Log"/> to the <see cref="Logs"/> register.
    /// </summary>
    private void Register()
    {
      // If the register does not already have this Log type, register the new type.
      Type type = GetType();
      if (!Logs.ContainsKey(type))
        Logs.Add(type, this);
    }

    /// <summary>
    /// A function for registering a <see cref="Log"/> to the <see cref="Logs"/> register.
    /// </summary>
    /// <param name="type">The <see cref="Type"/> of the <see cref="Log"/>.</param>
    private void Register(Type type)
    {
      // If the register does not already have this Log type, register the new type.
      if (!Logs.ContainsKey(type))
        Logs.Add(type, this);
    }

    /// <summary>
    /// An internal function for logging a <paramref name="message"/> to a <see cref="Log"/>'s
    /// file.
    /// </summary>
    /// <param name="message">The <see cref="string"/> to log.</param>
    /// <param name="level">The severity <see cref="Level"/>.</param>
    private void LogToFileInternal(string message, Level level)
    {
      // Determine which FilePath to use.
      FilePath filePath = useGlobalLogPath ? globalFilePath : userFilePath;

      // If this function was already called once before, prevent an infinite loop and invalidate
      // the given filepath. This can happen with logging File IO errors.
      if (isProcessingFile)
      {
        isProcessingFile = false;
        filePath.Invalidate(true);
        return;
      }

      // Check and create the full global path. If the path is no longer invalid, return.
      if (!filePath.CheckPath(out string path, true))
        return;
      // Format the message with the timestamp and the severity level.
      message = FormatFileMessage(message, filePath.GetTimestamp(), level);

      isProcessingFile = true; // The file is now being processed.

      // Append the message, the success of which determines validity.
      if (true)//!FileIO.AppendFileString(path, message, true, true))
        filePath.Invalidate(true);

      // The file is no longer being processed.
      isProcessingFile = false;
    }

    /// <summary>
    /// A function for logging <see cref="Exception"/>s thrown by the
    /// <see cref="AppDomain.CurrentDomain"/>'s <see cref="AppDomain.UnhandledException"/>
    /// <see langword="event"/>.
    /// </summary>
    /// <param name="sender">The original sender. Unused.</param>
    /// <param name="e">The arguments of the <see langword="event"/>.</param>
    private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      LogExceptionInternal(string.Empty, (Exception)e.ExceptionObject, true, defaultExceptionLevel);
    }

    /// <summary>
    /// A function for logging <see cref="Exception"/>s thrown by the
    /// <see cref="AppDomain.CurrentDomain"/>'s <see cref="AppDomain.FirstChanceException"/>
    /// <see langword="event"/>.
    /// </summary>
    /// <param name="sender">The original sender. Unused.</param>
    /// <param name="e">The arguments of the <see langword="event"/>.</param>
    private void OnFirstChanceException(object sender, FirstChanceExceptionEventArgs e)
    {
      LogExceptionInternal(string.Empty, e.Exception, true, defaultExceptionLevel);
    }

    /// <summary>
    /// A helper function for formatting a given <paramref name="message"/> for logging.
    /// </summary>
    /// <param name="message">The <see cref="string"/> to format.</param>
    /// <returns>Returns the finalized <see cref="string"/>. Ensure that this is not
    /// <see langword="null"/>.</returns>
    protected virtual string FormatMessage(string message)
    {
      return message;
    }

    protected virtual string FormatFileMessage(string message, string timestamp, Level level)
    {
      return $"[{timestamp}] ({level.Name}) >> {message}";
    }

    /// <summary>
    /// A helper function for formatting a <see cref="StackFrame"/> for the final log.
    /// </summary>
    /// <param name="frame">The <see cref="StackFrame"/> to process.</param>
    /// <returns>Returns the finalized <see cref="string"/>. Ensure that this is not
    /// <see langword="null"/>.</returns>
    protected virtual string FormatStackFrame(StackFrame frame)
    {
      FrameParser.ParseFrame(frame, traceBuilder);
      return string.Empty;
    }
  }
  /************************************************************************************************/
}