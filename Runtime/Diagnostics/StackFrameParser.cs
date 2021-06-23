/**************************************************************************************************/
/*!
\file   StackFrameParser.cs
\author Craig Williams
\par    Last Updated
        2021-06-15
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for a class that can be used to parse information from a StackFrame.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;

namespace CodeParadox.Tenor.Diagnostics
{
  /************************************************************************************************/
  /// <summary>
  /// A parser for a <see cref="StackFrame"/>, which uses a similar formatting to what a
  /// <see cref="StackTrace"/> would provide.
  /// </summary>
  /// <remarks>The format uses a priority method to handle how to order each individual
  /// <see cref="StackFrame"/> component, in the order of:
  /// [Method, Filename, LineNumber, ColumnNumber]. Use the parse values to determine which
  /// of these components are printed.</remarks>
  public sealed partial class StackFrameParser
  {
    /**********************************************************************************************/
    /// <summary>
    /// An <see langword="enum"/> for how to format the parsed <see cref="StackFrame"/>.
    /// </summary>
    [Flags]
    private enum FormatFlag
    {
      /// <summary>Nothing is actually returned.</summary>
      None         = 0,
      /// <summary>Format with the method.</summary>
      Method       = 1,
      /// <summary>Format with the filename.</summary>
      Filename     = 2,
      /// <summary>Format with the line number.</summary>
      LineNumber   = 4,
      /// <summary>Format with the column number.</summary>
      ColumnNumber = 8
    }
    /**********************************************************************************************/

    /// <summary>The <see cref="string"/> that separates each<see cref="Type.DeclaringType"/>
    /// by default.</summary>
    private static readonly string DeclaringTypeSeparator = "+";
    /// <summary>The grouping characters for method generic arguments.</summary>
    private static readonly ValueTuple<string, string> GenericGroup = new ValueTuple<string, string>("<", ">");
    /// <summary>The grouping characters for method parameters.</summary>
    private static readonly ValueTuple<string, string> ParamGroup = new ValueTuple<string, string>("(", ")");
    private static readonly int InitialBuilderCapacity = 50;

#nullable enable
    /// <summary>The prefix put before the entire line, including spacing.</summary>
    public string? FramePrefix = " ";
    /// <summary>The prefix put before the method.</summary>
    public string? MethodPrefix = " at ";
    /// <summary>The separator put between a method's <see cref="Type.DeclaringType"/>s.</summary>
    public string? TypeSeparator = ".";
    /// <summary>The separator put between a method's generics and parameters.</summary>
    public string? ArgumentSeparator = ",";
    /// <summary>The separator put between the method and the file information.</summary>
    public string? FileInfoPrefix = " in ";
    /// <summary>The separator put between the line number and column number.</summary>
    public string? NumberSeparator = ":";
    /// <summary>The prefix before the line number, if the line is the top priority.</summary>
    public string? LinePrefix = " at Line ";
    /// <summary>The prefix before the column number, if the column is the top priority.</summary>
    public string? ColumnPrefix = " at Col ";
#nullable disable

    /// <summary>A toggle for appending the method. This is Priority 1.</summary>
    public bool AppendMethod
      { get { return appendMethod; } set { SetFormatFlag(ref appendMethod, value, FormatFlag.Method); } }
    /// <summary>A toggle for appending the filename. This is Priority 2.</summary>
    public bool AppendFilename
      { get { return appendFilename; } set { SetFormatFlag(ref appendFilename, value, FormatFlag.Filename); } }
    /// <summary>A toggle for appending the line number. This is Priority 3.</summary>
    public bool AppendLineNumber
      { get { return appendLine; } set { SetFormatFlag(ref appendLine, value, FormatFlag.LineNumber); } }
    /// <summary>A toggle for appending the column number. This is Priority 4.</summary>
    public bool AppendColumnNumber
      { get { return appendColumn; } set { SetFormatFlag(ref appendColumn, value, FormatFlag.ColumnNumber); } }

    /// <summary>See: <see cref="AppendMethod"/></summary>
    private bool appendMethod = true;
    /// <summary>See: <see cref="AppendFilename"/></summary>
    private bool appendFilename = true;
    /// <summary>See: <see cref="AppendLineNumber"/></summary>
    private bool appendLine = true;
    /// <summary>See: <see cref="AppendColumnNumber"/></summary>
    private bool appendColumn = false;

    /// <summary>The <see cref="FormatFlag"/> for how to organize the final parse.</summary>
    private FormatFlag format = FormatFlag.None;

    /// <summary>
    /// A constructor for a <see cref="StackFrameParser"/>.
    /// </summary>
    public StackFrameParser()
    {
      // Initialize the toggles again in order to set the format flag.
      AppendMethod = appendMethod;
      AppendFilename = appendFilename;
      AppendLineNumber = appendLine;
      AppendColumnNumber = appendColumn;
    }

    /// <summary>
    /// A constructor for a <see cref="StackFrameParser"/>.
    /// </summary>
    /// <param name="method">See: <see cref="AppendMethod"/></param>
    /// <param name="file">See: <see cref="AppendFilename"/></param>
    /// <param name="line">See: <see cref="AppendLineNumber"/></param>
    /// <param name="column">See: <see cref="AppendColumnNumber"/></param>
    public StackFrameParser(bool method, bool file, bool line, bool column)
    {
      AppendMethod = method;
      AppendFilename = file;
      AppendLineNumber = line;
      AppendColumnNumber = column;
    }

    /// <summary>
    /// A function for parsing out a <see cref="StackFrame"/> based on the parse settings.
    /// </summary>
    /// <param name="frame">The <see cref="StackFrame"/> to parse.</param>
    /// <returns>Returns the <see cref="string"/> representation of the <paramref name="frame"/>,
    /// if possible. Returns <see cref="string.Empty"/> otherwise.</returns>
    public string ParseFrame(StackFrame frame)
    {
      // If the frame is not null, parse with a StringBuilder and get the string.
      if (frame != null)
      {
        StringBuilder sb = new StringBuilder(string.Empty, InitialBuilderCapacity);
        ParseFrameInternal(frame, sb);
        return sb.ToString();
      }

      return string.Empty; // Return an empty string otherwise.
    }

    /// <summary>
    /// A function for parsing out a <see cref="StackFrame"/> based on the parse settings.
    /// </summary>
    /// <param name="frame">The <see cref="StackFrame"/> to parse.</param>
    /// <param name="builder">The <see cref="StringBuilder"/> to append to.</param>
    public void ParseFrame(StackFrame frame, StringBuilder builder)
    {
      if (frame != null && builder != null)
        ParseFrameInternal(frame, builder);
    }

    /// <summary>
    /// A function for getting a <see cref="StackFrame"/>'s method, formatting it to
    /// contain all necessary information.
    /// </summary>
    /// <param name="frame">The <see cref="StackFrame"/> to parse.</param>
    /// <returns>Returns the <see cref="string"/> representation of the <paramref name="frame"/>'s
    /// method, if possible. Returns <see cref="string.Empty"/> otherwise.</returns>
    public string GetMethod(StackFrame frame)
    {
      // If the frame is not null, parse with a StringBuilder and get the string.
      if (frame != null)
      {
        StringBuilder sb = new StringBuilder(string.Empty, InitialBuilderCapacity);
        GetMethodInternal(frame, sb);
        return sb.ToString();
      }

      return string.Empty; // Return an empty string otherwise.
    }

    /// <summary>
    /// A function for getting a <see cref="StackFrame"/>'s method, formatting it to
    /// contain all necessary information.
    /// </summary>
    /// <param name="frame">The <see cref="StackFrame"/> to parse.</param>
    /// <param name="builder">The <see cref="StringBuilder"/> to append to.</param>
    public void GetMethod(StackFrame frame, StringBuilder builder)
    {
      // If the frame and builder are not null, get the method.
      if (frame != null && builder != null)
        GetMethodInternal(frame, builder);
    }

    /// <summary>
    /// A function for getting a <see cref="StackFrame"/>'s filename safely.
    /// </summary>
    /// <param name="frame">The <see cref="StackFrame"/> to parse.</param>
    /// <returns>Returns the <paramref name="frame"/>'s filename if possible. Returns 
    /// <see cref="string.Empty"/> otherwise.</returns>
    public string GetFilename(StackFrame frame)
    {
      if (frame != null)
        return GetFilenameInternal(frame);

      return string.Empty;
    }

    /// <summary>
    /// A helper function for building a <see cref="StackFrame"/> parse when it is known that the
    /// method will be parsed.
    /// </summary>
    /// <param name="frame">The <see cref="StackFrame"/> to parse.</param>
    /// <param name="builder">The <see cref="StringBuilder"/> to append to.</param>
    private void BuildFromMethod(StackFrame frame, StringBuilder builder)
    {
      int formatInt = (int)format; // Convert to an int for easier switching.

      // It is known that the method prefix and method can be appended.
      builder.Append(MethodPrefix);
      GetMethodInternal(frame, builder);

      // Switch on all cases where the method would be appended.
      switch (formatInt)
      {
        // If just the method, break now.
        case (int)FormatFlag.Method:
          break;
        // Append the Filename.
        case (int)FormatFlag.Method + (int)FormatFlag.Filename:
          builder.Append(string.Concat(FileInfoPrefix, GetFilenameInternal(frame)));
          break;
        // Append the Line Number.
        case (int)FormatFlag.Method + (int)FormatFlag.LineNumber:
          builder.Append(string.Concat(LinePrefix, frame.GetFileLineNumber()));
          break;
        // Append the Column Number.
        case (int)FormatFlag.Method + (int)FormatFlag.ColumnNumber:
          builder.Append(string.Concat(ColumnPrefix, frame.GetFileColumnNumber()));
          break;
        // Append the Filename and Line Number.
        case (int)FormatFlag.Method + (int)FormatFlag.Filename + (int)FormatFlag.LineNumber:
          builder.Append(string.Concat(FileInfoPrefix, GetFilenameInternal(frame),
                         NumberSeparator, frame.GetFileLineNumber()));
          break;
        // Append the Filename and Column Number.
        case (int)FormatFlag.Method + (int)FormatFlag.Filename + (int)FormatFlag.ColumnNumber:
          builder.Append(string.Concat(FileInfoPrefix, GetFilenameInternal(frame),
                         NumberSeparator, frame.GetFileColumnNumber()));
          break;
        // Append the Line Number and Column Number.
        case (int)FormatFlag.Method + (int)FormatFlag.LineNumber + (int)FormatFlag.ColumnNumber:
          builder.Append(string.Concat(LinePrefix, frame.GetFileLineNumber(),
                         NumberSeparator, frame.GetFileColumnNumber()));
          break;
        // Append the Filename, Line Number, and Column Number.
        default:
          builder.Append(string.Concat(FileInfoPrefix, GetFilenameInternal(frame),
                         NumberSeparator, frame.GetFileLineNumber(), NumberSeparator,
                         frame.GetFileColumnNumber()));
          break;
      }
    }

    /// <summary>
    /// A helper function for building a <see cref="StackFrame"/> parse when it is known that the
    /// filename will be parsed.
    /// </summary>
    /// <param name="frame">The <see cref="StackFrame"/> to parse.</param>
    /// <param name="builder">The <see cref="StringBuilder"/> to append to.</param>
    private void BuildFromFilename(StackFrame frame, StringBuilder builder)
    {
      int formatInt = (int)format; // Convert to an int for easier switching.

      // Switch on all cases where the filename would be appended.
      switch (formatInt)
      {
        // Append the Filename.
        case (int)FormatFlag.Filename:
          builder.Append(string.Concat(FileInfoPrefix, GetFilenameInternal(frame)));
          break;
        // Append the Filename and Line Number.
        case (int)FormatFlag.Filename + (int)FormatFlag.LineNumber:
          builder.Append(string.Concat(FileInfoPrefix, GetFilenameInternal(frame),
                         NumberSeparator, frame.GetFileLineNumber()));
          break;
        // Append the Filename and Column Number.
        case (int)FormatFlag.Filename + (int)FormatFlag.ColumnNumber:
          builder.Append(string.Concat(FileInfoPrefix, GetFilenameInternal(frame),
                         NumberSeparator, frame.GetFileColumnNumber()));
          break;
        // Append the Filename, Line Number, and Column Number.
        default:
          builder.Append(string.Concat(FileInfoPrefix, GetFilenameInternal(frame),
                         NumberSeparator, frame.GetFileLineNumber(), NumberSeparator,
                         frame.GetFileColumnNumber()));
          break;
      }
    }

    /// <summary>
    /// A helper function for building a <see cref="StackFrame"/> parse when it is known that the
    /// line number will be parsed.
    /// </summary>
    /// <param name="frame">The <see cref="StackFrame"/> to parse.</param>
    /// <param name="builder">The <see cref="StringBuilder"/> to append to.</param>
    private void BuildFromLineNumber(StackFrame frame, StringBuilder builder)
    {
      int formatInt = (int)format; // Convert to an int for easier switching.

      // Switch on all cases where the line number would be appended.
      switch (formatInt)
      {
        // Append the Line Number.
        case (int)FormatFlag.LineNumber:
          builder.Append(string.Concat(LinePrefix, frame.GetFileLineNumber()));
          break;
        // Append the Line Number and Column Number.
        default:
          builder.Append(string.Concat(LinePrefix, frame.GetFileLineNumber(), NumberSeparator,
                         frame.GetFileColumnNumber()));
          break;
      }
    }

    /// <summary>
    /// A helper function for building a <see cref="StackFrame"/> parse when it is known that the
    /// column number will be parsed.
    /// </summary>
    /// <param name="frame">The <see cref="StackFrame"/> to parse.</param>
    /// <param name="builder">The <see cref="StringBuilder"/> to append to.</param>
    private void BuildFromColumnNumber(StackFrame frame, StringBuilder builder)
    {
      builder.Append(string.Concat(ColumnPrefix, frame.GetFileColumnNumber()));
    }

    /// <summary>
    /// A helper function for getting a <see cref="StackFrame"/>'s method, formatting it to
    /// contain all necessary information.
    /// </summary>
    /// <param name="frame">The <see cref="StackFrame"/> to parse.</param>
    /// <param name="builder">The <see cref="StringBuilder"/> to append to.</param>
    private void GetMethodInternal(StackFrame frame, StringBuilder builder)
    {
      // Get the originating Method. If it is null for any reason, return an empty string.
      MethodBase method = frame.GetMethod();
      if (method == null)
        return;

      bool appendSeparator = false; // A toggle for adding a separator between arguments.

      Type declaringType = method.DeclaringType; // Get the declaring type of the method.

      // If the declaring type is valid, append it, along with the subtypes.
      if (declaringType != null)
      {
        // Replace any old separators with the new one.
        builder.Append(declaringType.FullName.Replace(DeclaringTypeSeparator, TypeSeparator));
        builder.Append(TypeSeparator);
      }

      builder.Append(method.Name); // Append the actual method's name.

      // Check if the method can be converted to MethodInfo.
      if (method is MethodInfo)
      {
        MethodInfo info = method as MethodInfo;

        // If so, detect if it is a generic method. Generic arguments must be appended.
        if (info.IsGenericMethod)
        {
          Type[] arguments = info.GetGenericArguments(); // Get the arguments.
          int genCount = arguments.Length; // Get the count.
          builder.Append(GenericGroup.Item1); // Append the start of the generic group.

          // Append each of the arguments. The last argument does not get a separator appended.
          for (int i = 0; i < genCount; i++)
          {
            // Append a separator if not the first element. This skips adding one at the very end.
            if (appendSeparator)
              builder.Append(ArgumentSeparator);
            else
              appendSeparator = true;

            builder.Append(arguments[i].Name); // Append the argument's name.
          }

          builder.Append(GenericGroup.Item2); // Append the end of the generic group.
        }
      }

      builder.Append(ParamGroup.Item1); // Append the start of the parameters section.

      // Get all of the parameters, and the count.
      ParameterInfo[] parameters = method.GetParameters();
      int paramCount = parameters.Length;
      appendSeparator = false;

      // Append each of the parameters. The last parameter does not get a separator appended.
      for (int i = 0; i < paramCount; i++)
      {
        // Append a separator if not the first element. This skips adding one at the very end.
        if (appendSeparator)
          builder.Append(ArgumentSeparator);
        else
          appendSeparator = true;

        // Append the parameter type and name.
        ParameterInfo info = parameters[i];
        if (info.ParameterType != null)
          builder.Append($"{info.ParameterType.Name} {info.Name}");
      }

      builder.Append(ParamGroup.Item2); // Append the end of the parameters section.
    }

    /// <summary>
    /// A helper function for getting a <see cref="StackFrame"/>'s filename safely.
    /// </summary>
    /// <param name="frame">The <see cref="StackFrame"/> to parse.</param>
    /// <returns>Returns the <paramref name="frame"/>'s filename if possible. Returns 
    /// <see cref="string.Empty"/> otherwise.</returns>
    private string GetFilenameInternal(StackFrame frame)
    {
      // If the IL Offset is known, the file name can be obtained.
      if (frame.GetILOffset() != StackFrame.OFFSET_UNKNOWN)
      {
        try
        {
          return frame.GetFileName(); // Get the filename, and return it.
        }
        catch (SecurityException)
        {
        }
      }
      
      return string.Empty; // If the filename cannot be appended, simply return empty.
    }

    /// <summary>
    /// An internal function for converting a <see cref="StackFrame"/> to a <see cref="string"/>
    /// similar in format to one produced by a <see cref="StackTrace"/>.
    /// </summary>
    /// <param name="frame">The <see cref="StackFrame"/> to parse.</param>
    /// <param name="builder">The <see cref="StringBuilder"/> to append to.</param>
    private void ParseFrameInternal(StackFrame frame, StringBuilder builder)
    {
      // If no formatting is actually happening, just return.
      if (format == FormatFlag.None)
        return;

      builder.Append(FramePrefix); // Append the prefix.

      // Handle the parse based on the priority of each piece of information. Depending on what
      // is parsed first, there are different combinations of prefixes and information that can
      // be applied. Each function goes through that specific group of combinations.
      if (appendMethod)
        BuildFromMethod(frame, builder);
      else if (appendFilename)
        BuildFromFilename(frame, builder);
      else if (appendLine)
        BuildFromLineNumber(frame, builder);
      else
        BuildFromColumnNumber(frame, builder);

    }

    /// <summary>
    /// A helper function for setting one of the parse toggles, while also setting up the
    /// <see cref="format"/> flag.
    /// </summary>
    /// <param name="variable">The variable to set.</param>
    /// <param name="value">The value to set the <paramref name="variable"/> to.</param>
    /// <param name="flag">The <see cref="FormatFlag"/> to change.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SetFormatFlag(ref bool variable, bool value, FormatFlag flag)
    {
      variable = value; // Set the value.

      // Either append or remove the flag.
      if (variable)
        format |= flag;
      else
        format &= ~flag;
    }
  }
  /************************************************************************************************/
}