/**************************************************************************************************/
/*!
\file   FileIO_Validation.cs
\author Craig Williams
\par    Last Updated
        2021-06-10
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A toolkit of functions related to managing files or manipulating them, specifically for
  validation.

\par Bug List

\par References
  - https://stackoverflow.com/questions/62771/how-do-i-check-if-a-given-string-is-a-legal-valid-file-name-under-windows#comment61991924_63235
  - https://ss64.com/osx/syntax-filenames.html
  - http://www.linfo.org/file_name.html
*/
/**************************************************************************************************/

using SlashParadox.Tenor.Data;
using SlashParadox.Tenor.Files;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace SlashParadox.Tenor.Tools
{
  /************************************************************************************************/
  public static partial class FileIO
  {
    /// <summary>The length of a lettered root (Such as 'C:').</summary>
    public static readonly int LetterRootLength = 2;
    /// <summary>The standard directory separator for Windows systems.</summary>
    public static readonly string SeparatorWindows = "\\";
    /// <summary>The universal directory separator.</summary>
    public static readonly string SeparatorUniversal = "/";

    /// <summary>A <see cref="PathSanitizer"/> that can be used for any operating system.</summary>
    private static PathSanitizer SanitizerUniversal = null;
    /// <summary>A <see cref="PathSanitizer"/> that can be used for any UNIX system.</summary>
    private static PathSanitizer SanitizerUNIX = null;
    /// <summary>The maximum filepath length on Windows machines.</summary>
    private static readonly int MaxPathWindows = 260;
    /// <summary>The maximum filepath length in general. Based on UNIX limits.</summary>
    private static readonly int MaxPathUniversal = 255;
    /// <summary>A <see cref="Regex"/> check for validating lettered directory roots.</summary>
    private static readonly string RootCheckLettered = @"^[a-z|A-Z]:";
    /// <summary>A <see cref="Regex"/> check for validating UNIX directory roots.</summary>
    private static readonly string RootCheckUNIX = @"^(?:\/)";
    /// <summary>A <see cref="Regex"/> check for validating UNIX directories.</summary>
    private static readonly string DirectoryCheckUNIX = @"^(?!$)[^\0\\]+$";
    /// <summary>A <see cref="Regex"/> check for validating Windows directories.</summary>
    private static readonly string DirectoryCheckWindows =
      @"^(?!(?:PRN|AUX|CLOCK\$|NUL|CON|COM\d|LPT\d)?$)[^\x00-\x1F\xA5?*:\""|<>]+(?<![\s.])$";
    /// <summary>A <see cref="Regex"/> check for validating any directory.</summary>
    private static readonly string DirectoryCheckUniversal =
      @"^(?!(?:PRN|AUX|CLOCK\$|NUL|CON|COM\d|LPT\d)?$)[^\x00-\x1F\xA5?*:\""|<>\\]+(?<![\s.])$";
    /// <summary>A <see cref="Regex"/> <see cref="string"/> for validating UNIX filenames.</summary>
    private static readonly string FilenameCheckUNIX = @"^(?!(?:\.)?$)[^\0\/]+(?<![\s.])$";
    /// <summary>A <see cref="Regex"/> <see cref="string"/> for validating any filename.</summary>
    private static readonly string FilenameCheckUniversal =
      @"^(?!(?:PRN|AUX|CLOCK\$|NUL|CON|COM\d|LPT\d)(?:\..+)?$)[^\x00-\x1F\xA5\\?*:\""|\/<>]+(?<![\s.])$";

    /// <summary>
    /// An extension function for creating a full name with a universal separator. This does not
    /// perform clean up. This merely replaces all <see cref="Path.DirectorySeparatorChar"/>s with
    /// <see cref="Path.AltDirectorySeparatorChar"/>s. <see cref="Path.AltDirectorySeparatorChar"/>
    /// should always be the same as <see cref="SeparatorUniversal"/>.
    /// </summary>
    /// <param name="info">The <see cref="FileInfo"/> to use.</param>
    /// <returns>Returns the universalized string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string UniversalFullName(this FileInfo info)
    {
      if (info != null)
        return info.FullName.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

      return string.Empty;
    }

    /// <summary>
    /// A function for determining if a given <see cref="string"/> is a valid directory. This uses
    /// a universal check.
    /// </summary>
    /// <param name="directory">The <see cref="string"/> to check.</param>
    /// <param name="rootRequired">A toggle for requiring a file system root.</param>
    /// <returns>Returns if the <paramref name="directory"/> is valid.</returns>
    public static bool IsValidDirectory(string directory, bool rootRequired = false)
    {
      return IsValidDirectoryUniversal(directory, rootRequired);
    }

    /// <summary>
    /// A function for determining if a given <see cref="string"/> is a valid directory.
    /// </summary>
    /// <param name="directory">The <see cref="string"/> to check.</param>
    /// <param name="os">The <see cref="OSType"/> to check the <paramref name="directory"/>
    /// against.</param>
    /// <param name="rootRequired">A toggle for requiring a file system root.</param>
    /// <returns>Returns if the <paramref name="directory"/> is valid.</returns>
    public static bool IsValidDirectory(string directory, OSType os, bool rootRequired = false)
    {
      // Return based on the type of the OS. Always check the known standards exactly.
      return os switch
      {
        OSType.Windows => IsValidDirectoryWindows(directory, rootRequired),
        OSType.Linux => IsValidDirectoryUNIX(directory, rootRequired),
        OSType.OSX => IsValidDirectoryUNIX(directory, rootRequired),
        _ => IsValidDirectoryUniversal(directory, rootRequired),
      };
    }

    /// <summary>
    /// A function for determining if a given <see cref="string"/> is a valid filename. This uses
    /// a universal check.
    /// </summary>
    /// <param name="filename">The <see cref="string"/> to check.</param>
    /// <returns>Returns if the <paramref name="filename"/> is valid.</returns>
    public static bool IsValidFilename(string filename)
    {
      return IsValidFilenameInternal(filename, FilenameCheckUniversal, MaxPathUniversal);
    }

    /// <summary>
    /// A function for determining if a given <see cref="string"/> is a valid filename.
    /// </summary>
    /// <param name="filename">The <see cref="string"/> to check.</param>
    /// <param name="os">The <see cref="OSType"/> to check the <paramref name="filename"/>
    /// against.</param>
    /// <returns>Returns if the <paramref name="filename"/> is valid.</returns>
    public static bool IsValidFilename(string filename, OSType os)
    {
      // Return based on the type of the OS. Always check the known standards exactly.
      return os switch
      {
        OSType.Windows => IsValidFilenameInternal(filename, FilenameCheckUniversal, MaxPathWindows),
        OSType.Linux => IsValidFilenameInternal(filename, FilenameCheckUNIX, MaxPathUniversal),
        OSType.OSX => IsValidFilenameInternal(filename, FilenameCheckUNIX, MaxPathUniversal),
        _ => IsValidFilenameInternal(filename, FilenameCheckUniversal, MaxPathUniversal),
      };
    }

    /// <summary>
    /// A function for determining if a given <see cref="string"/> is a valid filepath. This uses
    /// a universal check.
    /// </summary>
    /// <param name="filepath">The <see cref="string"/> to check.</param>
    /// <param name="rootRequired">A toggle for requiring a file system root.</param>
    /// <returns>Returns if the <paramref name="filepath"/> is valid.</returns>
    public static bool IsValidFilePath(string filepath, bool rootRequired = false)
    {
      return IsValidFilePathUniversal(filepath, rootRequired);
    }

    /// <summary>
    /// A function for determining if a given <see cref="string"/> is a valid directory.
    /// </summary>
    /// <param name="filepath">The <see cref="string"/> to check.</param>
    /// <param name="os">The <see cref="OSType"/> to check the <paramref name="filepath"/>
    /// against.</param>
    /// <param name="rootRequired">A toggle for requiring a file system root.</param>
    /// <returns>Returns if the <paramref name="filepath"/> is valid.</returns>
    public static bool IsValidFilePath(string filepath, OSType os, bool rootRequired = false)
    {
      // Return based on the type of the OS. Always check the known standards exactly.
      return os switch
      {
        OSType.Windows => IsValidFilePathWindows(filepath, rootRequired),
        OSType.Linux => IsValidFilePathUNIX(filepath, rootRequired),
        OSType.OSX => IsValidFilePathUNIX(filepath, rootRequired),
        _ => IsValidFilePathUniversal(filepath, rootRequired),
      };
    }

    /// <summary>
    /// A function for sanitizing given <paramref name="filepath"/>. This uses a universal
    /// sanitization process.
    /// </summary>
    /// <param name="filepath">The path to sanitize.</param>
    /// <returns>Returns The saniztized <paramref name="filepath"/>.</returns>
    /// <remarks>Path length is not sanitized.</remarks>
    public static string SanitizeFilePath(string filepath)
    {
      SanitizerUniversal.SanitizePath(filepath, out string sanitizedPath);
      return sanitizedPath;
    }

    /// <summary>
    /// A function for sanitizing given <paramref name="filepath"/>.
    /// </summary>
    /// <param name="filepath">The path to sanitize.</param>
    /// <param name="os">The <see cref="OSType"/> to sanitize the <paramref name="filepath"/>
    /// for.</param>
    /// <returns>Returns The saniztized <paramref name="filepath"/>.</returns>
    /// <remarks>Path length is not sanitized.</remarks>
    public static string SanitizeFilePath(string filepath, OSType os)
    {
      SanitizeFilePath(filepath, out string sanitizedPath, os);
      return sanitizedPath;
    }

    /// <summary>
    /// A function for sanitizing given <paramref name="filepath"/>.
    /// </summary>
    /// <param name="filepath">The path to sanitize.</param>
    /// <param name="sanitizer">The <see cref="PathSanitizer"/> to use.</param>
    /// <returns>Returns the sanitized <paramref name="filepath"/>.</returns>
    /// <remarks>Path length is not sanitized.</remarks>
    public static string SanitizeFilePath(string filepath, PathSanitizer sanitizer)
    {
      if (sanitizer != null)
      {
        sanitizer.SanitizePath(filepath, out string sanitizedPath);
        return sanitizedPath;
      }

      return string.Empty;
    }

    /// <summary>
    /// A function for sanitizing given <paramref name="filepath"/>. This uses a universal
    /// sanitization process.
    /// </summary>
    /// <param name="filepath">The path to sanitize.</param>
    /// <param name="sanitizedPath">The saniztized <paramref name="filepath"/>.</param>
    /// <returns>Returns if the sanitization was successful.</returns>
    /// <remarks>Path length is not sanitized.</remarks>
    public static bool SanitizeFilePath(string filepath, out string sanitizedPath)
    {
      return SanitizerUniversal.SanitizePath(filepath, out sanitizedPath);
    }

    /// <summary>
    /// A function for sanitizing given <paramref name="filepath"/>.
    /// </summary>
    /// <param name="filepath">The path to sanitize.</param>
    /// <param name="sanitizedPath">The saniztized <paramref name="filepath"/>.</param>
    /// <param name="os">The <see cref="OSType"/> to sanitize the <paramref name="filepath"/>
    /// for.</param>
    /// <returns>Returns if the sanitization was successful.</returns>
    /// <remarks>Path length is not sanitized.</remarks>
    public static bool SanitizeFilePath(string filepath, out string sanitizedPath, OSType os)
    {
      // Return based on the type of the OS. Always check the known standards exactly.
      return os switch
      {
        OSType.Windows => SanitizerUniversal.SanitizePath(filepath, out sanitizedPath),
        OSType.Linux => SanitizerUNIX.SanitizePath(filepath, out sanitizedPath),
        OSType.OSX => SanitizerUNIX.SanitizePath(filepath, out sanitizedPath),
        _ => SanitizerUniversal.SanitizePath(filepath, out sanitizedPath),
      };
    }

    /// <summary>
    /// A function for sanitizing given <paramref name="filepath"/>.
    /// </summary>
    /// <param name="filepath">The path to sanitize.</param>
    /// <param name="sanitizedPath">The saniztized <paramref name="filepath"/>.</param>
    /// <param name="sanitizer">The <see cref="PathSanitizer"/> to use.</param>
    /// <returns>Returns if the sanitization was successful.</returns>
    /// <remarks>Path length is not sanitized.</remarks>
    public static bool SanitizeFilePath(string filepath, out string sanitizedPath,
                                        PathSanitizer sanitizer)
    {
      if (sanitizer != null)
        return sanitizer.SanitizePath(filepath, out sanitizedPath);

      sanitizedPath = string.Empty;
      return false;
    }

    /// <summary>
    /// A function for sanitizing given <see cref="FileInfo"/>.
    /// </summary>
    /// <param name="info">The <see cref="FileInfo"/> to sanitize.</param>
    /// <returns>Returns if the sanitization was successful.</returns>
    /// <remarks>The <paramref name="info"/> is passed by reference, as the entire object must be
    /// reconstructed to use the new path. Keep this in mind!</remarks>
    public static bool SanitizeFileInfo(ref FileInfo info)
    {
      // Only sanitize if it is required.
      if (!IsValidFilePath(info.FullName))
      {
        // Get the result of the sanitization.
        bool result = SanitizeFilePath(info.FullName, out string sanitized);

        // If successful, create new FileInfo with the sanitized path.
        if (result)
          info = new FileInfo(sanitized);

        return result; // Return the result.
      }

      return true; // The sanitization was not required, so it is already clean.
    }

    /// <summary>
    /// A function for sanitizing given <see cref="FileInfo"/>.
    /// </summary>
    /// <param name="info">The <see cref="FileInfo"/> to sanitize.</param>
    /// <param name="os">The <see cref="OSType"/> to sanitize the <paramref name="info"/>
    /// for.</param>
    /// <returns>Returns if the sanitization was successful.</returns>
    /// <remarks>The <paramref name="info"/> is passed by reference, as the entire object must be
    /// reconstructed to use the new path. Keep this in mind!</remarks>
    public static bool SanitizeFileInfo(ref FileInfo info, OSType os)
    {
      // Only sanitize if it is required.
      if (!IsValidFilePath(info.FullName))
      {
        // Get the result of the sanitization.
        bool result = SanitizeFilePath(info.FullName, out string sanitized, os);

        // If successful, create new FileInfo with the sanitized path.
        if (result)
          info = new FileInfo(sanitized);

        return result; // Return the result.
      }

      return true; // The sanitization was not required, so it is already clean.
    }

    /// <summary>
    /// A function for sanitizing given <see cref="FileInfo"/>.
    /// </summary>
    /// <param name="info">The <see cref="FileInfo"/> to sanitize.</param>
    /// <param name="sanitizer">The <see cref="PathSanitizer"/> to use.</param>
    /// <returns>Returns if the sanitization was successful.</returns>
    /// <remarks>The <paramref name="info"/> is passed by reference, as the entire object must be
    /// reconstructed to use the new path. Keep this in mind!</remarks>
    public static bool SanitizeFileInfo(ref FileInfo info, PathSanitizer sanitizer)
    {
      // Only sanitize if it is required.
      if (!IsValidFilePath(info.FullName))
      {
        // Get the result of the sanitization.
        bool result = SanitizeFilePath(info.FullName, out string sanitized, sanitizer);

        // If successful, create new FileInfo with the sanitized path.
        if (result)
          info = new FileInfo(sanitized);

        return result; // Return the result.
      }

      return true; // The sanitization was not required, so it is already clean.
    }

    /// <summary>
    /// A function for creating a <see cref="PathSanitizer"/>, using the
    /// <see cref="SanitizerUniversal"/> as a base.
    /// </summary>
    /// <returns>Returns the copied <see cref="PathSanitizer"/>.</returns>
    public static PathSanitizer CopyUniversalSanitizer()
    {
      return new PathSanitizer(SanitizerUniversal);
    }

    /// <summary>
    /// A function for creating a <see cref="PathSanitizer"/>, using the
    /// <see cref="SanitizerUNIX"/> as a base.
    /// </summary>
    /// <returns>Returns the copied <see cref="PathSanitizer"/>.</returns>
    public static PathSanitizer CopyUNIXSanitizer()
    {
      return new PathSanitizer(SanitizerUNIX);
    }

    /// <summary>
    /// An internal function for determining if a given <see cref="string"/> is a valid filename.
    /// </summary>
    /// <param name="filename">The <see cref="string"/> to check.</param>
    /// <param name="checkString">The <see cref="Regex"/> <see cref="string"/> to use.</param>
    /// <param name="maxPath">The maximum path length allowed.</param>
    /// <returns>Returns if the <paramref name="filename"/> is valid.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsValidFilenameInternal(string filename, string checkString, int maxPath)
    {
      if (!Maths.InRangeII(filename.Length, 0, maxPath))
        return false;

      // If a match is made, the filename is valid.
      return Regex.IsMatch(filename, checkString, RegexOptions.CultureInvariant);
    }

    /// <summary>
    /// An internal function for determining if a given <see cref="string"/> is a valid directory.
    /// This uses a universal check.
    /// </summary>
    /// <param name="directory">The <see cref="string"/> to check.</param>
    /// <param name="rootRequired">A toggle for requiring a file system root.</param>
    /// <returns>Returns if the <paramref name="directory"/> is valid.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsValidDirectoryUniversal(string directory, bool rootRequired)
    {
      if (!Maths.InRangeII(directory.Length, 0, MaxPathUniversal))
        return false;

      // Get a possible Lettered Root from the directory.
      string possibleRoot = directory.Length >= LetterRootLength ?
        directory.Substring(0, LetterRootLength) : string.Empty;

      // If the root matches a universal lettered root, it must be removed.
      if (Regex.IsMatch(possibleRoot, RootCheckLettered, RegexOptions.CultureInvariant))
      {
        directory = directory.Substring(LetterRootLength, directory.Length - LetterRootLength);
      }
      else
      {
        // If invalid directory characters are found, return false immediately.
        if (!Regex.IsMatch(directory, DirectoryCheckUniversal, RegexOptions.CultureInvariant))
          return false;

        // If not a root for other operating systems, and a root is required, return false.
        if (!Path.IsPathRooted(directory) && rootRequired)
          return false;

        return true;
      }

      // If invalid directory characters are found, return false immediately.
      if (!Regex.IsMatch(directory, DirectoryCheckUniversal, RegexOptions.CultureInvariant))
        return false;

      return true;
    }

    /// <summary>
    /// An internal function for determining if a given <see cref="string"/> is a valid Windows
    /// directory.
    /// This uses a universal check.
    /// </summary>
    /// <param name="directory">The <see cref="string"/> to check.</param>
    /// <param name="rootRequired">A toggle for requiring a file system root.</param>
    /// <returns>Returns if the <paramref name="directory"/> is valid.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsValidDirectoryWindows(string directory, bool rootRequired)
    {
      if (!Maths.InRangeII(directory.Length, 0, MaxPathWindows))
        return false;

      // Get a possible Lettered Root from the directory.
      string possibleRoot = directory.Length >= LetterRootLength ?
        directory.Substring(0, LetterRootLength) : string.Empty;

      // If the root matches a universal lettered root, it must be removed.
      if (Regex.IsMatch(possibleRoot, RootCheckLettered, RegexOptions.CultureInvariant))
        directory = directory.Substring(LetterRootLength, directory.Length - LetterRootLength);
      else if (rootRequired)
        return false;

      // If invalid directory characters are found, return false immediately.
      if (!Regex.IsMatch(directory, DirectoryCheckWindows, RegexOptions.CultureInvariant))
        return false;

      return true; // The directory is valid for Windows.
    }

    /// <summary>
    /// An internal function for determining if a given <see cref="string"/> is a valid UNIX
    /// directory.
    /// This uses a universal check.
    /// </summary>
    /// <param name="directory">The <see cref="string"/> to check.</param>
    /// <param name="rootRequired">A toggle for requiring a file system root.</param>
    /// <returns>Returns if the <paramref name="directory"/> is valid.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsValidDirectoryUNIX(string directory, bool rootRequired)
    {
      if (!Maths.InRangeII(directory.Length, 0, MaxPathUniversal))
        return false;
      
      // If invalid directory characters are found, return false immediately.
      if (!Regex.IsMatch(directory, DirectoryCheckUNIX, RegexOptions.CultureInvariant))
        return false;

      // If the root is required, and not present, return false immediately.
      if (rootRequired && !Regex.IsMatch(directory, RootCheckUNIX, RegexOptions.CultureInvariant))
        return false;
      
      return true; // The directory is valid for UNIX systems.
    }

    /// <summary>
    /// An internal function for determining if a given <see cref="string"/> is a valid filepath.
    /// This uses a universal check.
    /// </summary>
    /// <param name="filepath">The <see cref="string"/> to check.</param>
    /// <param name="rootRequired">A toggle for requiring a file system root.</param>
    /// <returns>Returns if the <paramref name="filepath"/> is valid.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsValidFilePathUniversal(string filepath, bool rootRequired)
    {
      int pathLength = filepath.Length;

      if (!Maths.InRangeII(pathLength, 0, MaxPathUniversal))
        return false;

      // Get the last index of a valid directory separator.
      int nameIndex = filepath.LastIndexOf(SeparatorUniversal) + 1;

      // If no directory separator is found, assume the filepath is just a filename.
      if (nameIndex <= 0)
        return IsValidFilenameInternal(filepath, FilenameCheckUniversal, MaxPathUniversal);

      // Get the directory and filename, and then validate them both.
      string directory = filepath.Substring(0, nameIndex);
      string filename = filepath.Substring(nameIndex, pathLength - nameIndex);

      return IsValidDirectoryUniversal(directory, rootRequired)
        && IsValidFilenameInternal(filename, FilenameCheckUniversal, MaxPathUniversal);
    }

    /// <summary>
    /// An internal function for determining if a given <see cref="string"/> is a valid Windows
    /// filepath.
    /// </summary>
    /// <param name="filepath">The <see cref="string"/> to check.</param>
    /// <param name="rootRequired">A toggle for requiring a file system root.</param>
    /// <returns>Returns if the <paramref name="filepath"/> is valid.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsValidFilePathWindows(string filepath, bool rootRequired)
    {
      int pathLength = filepath.Length;

      if (!Maths.InRangeII(pathLength, 0, MaxPathWindows))
        return false;

      // Get the last index of a valid directory separator.
      int nameIndex = Maths.Max(filepath.LastIndexOf(Path.DirectorySeparatorChar),
                                filepath.LastIndexOf(Path.AltDirectorySeparatorChar)) + 1;

      // If no directory separator is found, assume the filepath is just a filename.
      if (nameIndex <= 0)
        return IsValidFilenameInternal(filepath, FilenameCheckUniversal, MaxPathWindows);

      // Get the directory and filename, and then validate them both.
      string directory = filepath.Substring(0, nameIndex);
      string filename = filepath.Substring(nameIndex, pathLength - nameIndex);

      return IsValidDirectoryWindows(directory, rootRequired)
        && IsValidFilenameInternal(filename, FilenameCheckUniversal, MaxPathWindows);
    }

    /// <summary>
    /// An internal function for determining if a given <see cref="string"/> is a valid UNIX
    /// filepath.
    /// </summary>
    /// <param name="filepath">The <see cref="string"/> to check.</param>
    /// <param name="rootRequired">A toggle for requiring a file system root.</param>
    /// <returns>Returns if the <paramref name="filepath"/> is valid.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsValidFilePathUNIX(string filepath, bool rootRequired)
    {
      int pathLength = filepath.Length;

      if (!Maths.InRangeII(pathLength, 0, MaxPathUniversal))
        return false;

      // Get the last index of a valid directory separator.
      int nameIndex = filepath.LastIndexOf(SeparatorUniversal) + 1;

      // If no directory separator is found, assume the filepath is just a filename.
      if (nameIndex <= 0)
        return IsValidFilenameInternal(filepath, FilenameCheckUNIX, MaxPathUniversal);

      // Get the directory and filename, and then validate them both.
      string directory = filepath.Substring(0, nameIndex);
      string filename = filepath.Substring(nameIndex, pathLength - nameIndex);

      return IsValidDirectoryUNIX(directory, rootRequired)
        && IsValidFilenameInternal(filename, FilenameCheckUniversal, MaxPathUniversal);
    }
  }
  /************************************************************************************************/
}