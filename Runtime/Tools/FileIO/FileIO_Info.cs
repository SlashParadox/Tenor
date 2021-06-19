/**************************************************************************************************/
/*!
\file   FileIO.cs
\author Craig Williams
\par    Last Updated
        2021-06-18
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A toolkit of functions related to managing files or manipulating them.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System.IO;
using System.Runtime.CompilerServices;

namespace CodeParadox.Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// A collection of tools for file management. Use this with the <see cref="PathSanitizer"/> as
  /// well for safety. These tools are built to be usable on multiple different operating systems.
  /// </summary>
  public static partial class FileIO
  {
    /// <summary>
    /// A function for safely getting a file's size in bytes.
    /// </summary>
    /// <param name="filepath">The path to the wanted file.</param>
    /// <param name="size">The size of the file, in bytes. Returns the size if the file at the
    /// <paramref name="filepath"/> exists. Returns <see cref="BadFileSize"/> otherwise.</param>
    /// <returns>Returns if the file size was successfully acquired.</returns>
    public static bool GetFileSize(string filepath, out long size)
    {
      // If the file does not exist, return a bad size.
      if (!File.Exists(filepath))
      {
        size = BadFileSize;
        return false;
      }

      // Return the proper size.
      size = new FileInfo(filepath).Length;
      return true;
    }

    /// <summary>
    /// An extension function for safely getting a file's size in bytes.
    /// </summary>
    /// <param name="filepath">The path to the wanted file.</param>
    /// <returns>Returns the size if the file at the the <paramref name="filepath"/> exists.
    /// Returns <see cref="BadFileSize"/> otherwise.</returns>
    public static long GetFileSize(string filepath)
    {
      // Make sure the file exists before returning the length.
      return File.Exists(filepath) ? new FileInfo(filepath).Length : BadFileSize;
    }

    /// <summary>
    /// An extension function for safely getting a file's size in bytes.
    /// </summary>
    /// <param name="info">The <see cref="FileInfo"/> to get the size from.</param>
    /// <param name="size">The size of the file, in bytes. Returns <see cref="FileInfo.Length"/>
    /// if the <paramref name="info"/> exists. Returns <see cref="BadFileSize"/> otherwise.</param>
    /// <returns>Returns if the file size was successfully acquired.</returns>
    public static bool GetFileSize(this FileInfo info, out long size)
    {
      // If the file does not exist, return a bad size.
      if (info == null || !info.Exists)
      {
        size = BadFileSize;
        return false;
      }

      // Return the proper size.
      size = info.Length;
      return true;
    }

    /// <summary>
    /// An extension function for safely getting a file's size in bytes.
    /// </summary>
    /// <param name="info">The <see cref="FileInfo"/> to get the size from.</param>
    /// <returns>Returns <see cref="FileInfo.Length"/> if the <paramref name="info"/> exists.
    /// Returns <see cref="BadFileSize"/> otherwise.</returns>
    public static long GetFileSize(this FileInfo info)
    {
      // Make sure the file exists before returning the length.
      return info != null && info.Exists ? info.Length : BadFileSize;
    }

    /// <summary>
    /// A function for checking if a directory exists.
    /// </summary>
    /// <param name="directory">The directory to check.</param>
    /// <param name="createIfNull">A toggle for allowing the directory to be made if it does
    /// not exist.</param>
    /// <returns>Returns if the <paramref name="directory"/> exists.</returns>
    public static bool DoesDirectoryExist(string directory, bool createIfNull)
    {
      return DoesDirectoryExistInternal(directory, createIfNull);
    }

    /// <summary>
    /// A function for checking if a directory exists.
    /// </summary>
    /// <param name="info">The <see cref="DirectoryInfo"/> to check.</param>
    /// <param name="createIfNull">A toggle for allowing the directory to be made if it does
    /// not exist.</param>
    /// <returns>Returns if the <paramref name="info"/>'s directory exists.</returns>
    public static bool DoesDirectoryExist(this DirectoryInfo info, bool createIfNull)
    {
      return info != null && DoesDirectoryExistInternal(info.FullName, createIfNull);
    }

    /// <summary>
    /// A function for checking if a directory exists.
    /// </summary>
    /// <param name="info">The <see cref="FileInfo"/> to check.</param>
    /// <param name="createIfNull">A toggle for allowing the directory to be made if it does
    /// not exist.</param>
    /// <returns>Returns if the <paramref name="info"/>'s directory exists.</returns>
    public static bool DoesDirectoryExist(this FileInfo info, bool createIfNull)
    {
      return info != null && DoesDirectoryExistInternal(info.DirectoryName, createIfNull);
    }

    /// <summary>
    /// A function for checking if a file exists.
    /// </summary>
    /// <param name="filepath">The filepath to check.</param>
    /// <param name="createIfNull">A toggle for allowing the file to be made if it does
    /// not exist.</param>
    /// <returns>Returns if the <paramref name="filepath"/> exists.</returns>
    public static bool DoesFileExist(string filepath, bool createIfNull)
    {
      return DoesFileExistInternal(filepath, createIfNull);
    }

    /// <summary>
    /// A function for checking if a file exists.
    /// </summary>
    /// <param name="info">The <see cref="FileInfo"/> to check.</param>
    /// <param name="createIfNull">A toggle for allowing the file to be made if it does
    /// not exist.</param>
    /// <returns>Returns if the <paramref name="info"/>'s directory exists.</returns>
    public static bool DoesFileExist(this FileInfo info, bool createIfNull)
    {
      return info != null && DoesFileExistInternal(info.FullName, createIfNull);
    }

    /// <summary>
    /// An internal function for checking if a directory exists.
    /// </summary>
    /// <param name="directory">The directory to check.</param>
    /// <param name="createIfNull">A toggle for allowing the directory to be made if it does
    /// not exist.</param>
    /// <returns>Returns if the <paramref name="directory"/> exists.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool DoesDirectoryExistInternal(string directory, bool createIfNull)
    {
      // Return if the directory exists, or does after a permitted creation.
      return Directory.Exists(directory) || (createIfNull && CreateDirectoryInternal(directory));
    }

    /// <summary>
    /// An internal function for checking if a file exists.
    /// </summary>
    /// <param name="filepath">The directory to check.</param>
    /// <param name="createIfNull">A toggle for allowing the file to be made if it does
    /// not exist.</param>
    /// <returns>Returns if the <paramref name="filepath"/> exists.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool DoesFileExistInternal(string filepath, bool createIfNull)
    {
      // Return if the file exists, or does after a permitted creation.
      return File.Exists(filepath) || (createIfNull && CreateFileInternal(filepath, true));
    }
  }
  /************************************************************************************************/
}