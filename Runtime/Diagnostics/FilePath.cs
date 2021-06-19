/**************************************************************************************************/
/*!
\file   FilePath.cs
\author Craig Williams
\par    Last Updated
        2021-06-18
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for a class representing a Log's file path.

\par Bug List

\par References
*/
/**************************************************************************************************/

using CodeParadox.Tenor.Tools;
using System;
using System.IO;

namespace CodeParadox.Tenor.Diagnostics
{
  /************************************************************************************************/
  public abstract partial class Log
  {
    /**********************************************************************************************/
    /// <summary>
    /// A helper class for a <see cref="Log"/>'s file path that it sends messages to.
    /// </summary>
    private sealed class FilePath
    {
      /// <summary>The default <see cref="DateTime"/> formatting.</summary>
      private static readonly string DefaultDateFormat = "yyyy-MM-dd_HH-mm-ss";

      /// <summary>The <see cref="DateTime"/> formatting. A default is used if invalid.</summary>
      public string DateFormat { get { return dateFormat; } set { SetDateFormat(value); } }
      /// <summary>The original filepath, as given by the user.</summary>
      public string OriginalPath { get { return originalPath; } set { SetOriginalPath(value); } }
      /// <summary>A check on if the path is valid for use.</summary>
      public bool IsValid { get { return isValid; } }

      /// <summary>The full path, with the timestamp included.</summary>
      private string fixedPath;
      /// <summary>See: <see cref="IsValid"/></summary>
      private bool isValid = true;
      /// <summary>See: <see cref="OriginalPath"/></summary>
      private string originalPath;
      /// <summary>See: <see cref="DateFormat"/></summary>
      private string dateFormat;

      /// <summary>
      /// A constructor for a <see cref="FilePath"/>.
      /// </summary>
      /// <param name="path">See: <see cref="OriginalPath"/></param>
      public FilePath(string path)
      {
        originalPath = path;
        dateFormat = DefaultDateFormat;
      }
      
      /// <summary>
      /// A function for checking if the <see cref="FilePath"/> is valid.
      /// </summary>
      /// <param name="path">The full path. Returns <see langword="null"/> if invalid.</param>
      /// <param name="printErrorMessage">A toggle for printing an error message on failure.</param>
      /// <returns>Returns if the <see cref="FilePath"/> is valid.</returns>
      public bool CheckPath(out string path, bool printErrorMessage = false)
      {
        // Check if the path can test validity, but the path has yet to be created.
        if (isValid && fixedPath == null)
        {
          try
          {
            // Convert to a full path, and split up the directory and filename.
            string fullPath = Path.GetFullPath(OriginalPath);
            string directory = Path.GetDirectoryName(fullPath);
            string filename = Path.GetFileName(fullPath);

            // Append the timestamp and create the full path.
            filename = DateTime.Now.ToString(dateFormat) + "_" + filename;
            fixedPath = FileIO.SanitizeFilePath(Path.Combine(directory, filename));
          }
          catch
          {
            // If any error occurs, the path is not valid.
            isValid = false;
            fixedPath = null;

            if (printErrorMessage)
              PrintErrorMessage();
          }
        }

        path = fixedPath;
        return isValid;
      }

      /// <summary>
      /// A function for getting the current timestamp, using this <see cref="FilePath"/>'s
      /// <see cref="DateFormat"/>.
      /// </summary>
      /// <returns>Returns the current timestamp as a <see cref="string"/>.</returns>
      public string GetTimestamp()
      {
        return DateTime.Now.ToString(dateFormat);
      }

      /// <summary>
      /// A function for forcing the <see cref="FilePath"/> to be invalid.
      /// </summary>
      /// <param name="printErrorMessage">A toggle for printing an error message to the
      /// <see cref="Console"/>.</param>
      public void Invalidate(bool printErrorMessage = false)
      {
        isValid = false;
        fixedPath = null;

        if (printErrorMessage)
          PrintErrorMessage();
      }

      /// <summary>
      /// A function to reset the validity check.
      /// </summary>
      /// <param name="resetDateFormat">A toggle for resetting the <see cref="DateFormat"/>
      /// back to its default.</param>
      public void Reset(bool resetDateFormat = false)
      {
        isValid = true;
        fixedPath = null;

        if (resetDateFormat)
          dateFormat = DefaultDateFormat;
      }

      /// <summary>
      /// A helper function for setting the <see cref="dateFormat"/>.
      /// </summary>
      /// <param name="format">The new <see cref="DateTime"/> format.</param>
      private void SetDateFormat(string format)
      {
        try
        {
          // Attempt to use the new formatting. If valid, set it as the format.
          DateTime.Now.ToString(format);
          dateFormat = format;
        }
        catch
        {
          // Otherwise, fallback to the default format.
          dateFormat = DefaultDateFormat;
        }
      }

      /// <summary>
      /// A helper function for setting the <see cref="OriginalPath"/> and resetting the
      /// validity tests.
      /// </summary>
      /// <param name="path">The new <see cref="OriginalPath"/>.</param>
      private void SetOriginalPath(string path)
      {
        originalPath = path;
        Reset();
      }

      /// <summary>
      /// A helper function for printing an error message when the file cannot be logged to.
      /// </summary>
      private void PrintErrorMessage()
      {
        string message = $"THE FILE AT {originalPath} COULD NOT BE LOGGED TO.";
        LogToConsole(message, Level.Error);
      }
    }
    /**********************************************************************************************/
  }
  /************************************************************************************************/
}