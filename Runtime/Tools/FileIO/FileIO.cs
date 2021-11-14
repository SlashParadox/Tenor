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
  Many functions will be rewritten once Unity updates Mono and .NET for new base API functions.

\par Bug List
  LOW
    - If an error occurs when deleting a file, the empty directory that may have been created is
      deleted. However, if multiple empty directories were made, only the furthest one is deleted.

\par References
*/
/**************************************************************************************************/

using SlashParadox.Tenor.Files;
using SlashParadox.Tenor.Threading;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SlashParadox.Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// A collection of tools for file management. Use this with the <see cref="PathSanitizer"/> as
  /// well for safety. These tools are built to be usable on multiple different operating systems.
  /// </summary>
  public static partial class FileIO
  {
    /// <summary>The value returned when getting a file's size goes wrong.</summary>
    public static readonly long BadFileSize = -1;
    /// <summary>The default size for <see cref="File"/>'s method buffers.</summary>
    private static readonly int DefaultCopyBuffer = 81920;
    /// <summary>The default <see cref="Encoding"/> for <see cref="Stream"/>s.</summary>
    private static readonly UTF8Encoding DefaultEncoding = new UTF8Encoding(false, true);

    /// <summary>A toggle for deleting temporary copies when some operation goes wrong.</summary>
    public static bool DeleteTempCopies = true;

    /// <summary>
    /// The static constructor for the <see cref="FileIO"/> class.
    /// </summary>
    static FileIO()
    {
      BuildSanitizerUniversal();
      BuildSanitizerUNIX();
    }

    /// <summary>
    /// A function for appending an array of <see cref="byte"/>s to a file.
    /// </summary>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="bytes">The array of <see cref="byte"/>s to append.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static bool AppendFileBytes(string filepath, byte[] bytes, bool createIfNull = false)
    {
      if (DoesFileExist(filepath, createIfNull))
        return AppendFileBytesInternal(filepath, bytes);

      return false;
    }

    /// <summary>
    /// A function for appending an array of <see cref="byte"/>s to a file.
    /// </summary>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="bytes">The array of <see cref="byte"/>s to append.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static bool AppendFileBytes(FileInfo fileInfo, byte[] bytes, bool createIfNull = false)
    {
      if (DoesFileExist(fileInfo, createIfNull))
        return AppendFileBytesInternal(fileInfo.FullName, bytes);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending an array of <see cref="byte"/>s to a file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filePath">The file to append to.</param>
    /// <param name="bytes">The array of <see cref="byte"/>s to append.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static async Task<bool> AppendFileBytesAsync(CancellationToken token, string filePath,
                                                        byte[] bytes, bool createIfNull = false)
    {
      if (DoesFileExist(filePath, createIfNull))
        return await AppendFileBytesAsyncInternal(token, filePath, bytes);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending an array of <see cref="byte"/>s to a file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="bytes">The array of <see cref="byte"/>s to append.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static async Task<bool> AppendFileBytesAsync(CancellationToken token, FileInfo fileInfo,
                                                        byte[] bytes, bool createIfNull = false)
    {
      if (DoesFileExist(fileInfo, createIfNull))
        return await AppendFileBytesAsyncInternal(token, fileInfo.FullName, bytes);

      return false;
    }

    /// <summary>
    /// A function for appending an array of <see cref="byte"/>s to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="bytes">The array of <see cref="byte"/>s to append.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static bool AppendFileBytesSafe(string filepath, byte[] bytes, bool createIfNull = false)
    {
      if (DoesFileExist(filepath, createIfNull))
        return AppendFileBytesSafeInternal(filepath, bytes);

      return false;
    }

    /// <summary>
    /// A function for appending an array of <see cref="byte"/>s to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="bytes">The array of <see cref="byte"/>s to append.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static bool AppendFileBytesSafe(FileInfo fileInfo, byte[] bytes,
                                           bool createIfNull = false)
    {
      if (DoesFileExist(fileInfo, createIfNull))
        return AppendFileBytesSafeInternal(fileInfo.FullName, bytes);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending an array of <see cref="byte"/>s to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="bytes">The array of <see cref="byte"/>s to append.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static async Task<bool> AppendFileBytesSafeAsync(CancellationToken token,
                                                            string filepath, byte[] bytes,
                                                            bool createIfNull = false)
    {
      if (DoesFileExist(filepath, createIfNull))
        return await AppendFileBytesSafeAsyncInternal(token, filepath, bytes);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending an array of <see cref="byte"/>s to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="bytes">The array of <see cref="byte"/>s to append.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static async Task<bool> AppendFileBytesSafeAsync(CancellationToken token,
                                                            FileInfo fileInfo, byte[] bytes,
                                                            bool createIfNull = false)
    {
      if (DoesFileExist(fileInfo, createIfNull))
        return await AppendFileBytesSafeAsyncInternal(token, fileInfo.FullName, bytes);

      return false;
    }

    /// <summary>
    /// A function for appending a serializable object to a file.
    /// </summary>
    /// <typeparam name="T">The type of the object. This must be serializable!</typeparam>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="obj">The object to serialize and append.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was sucessfully appended to.</returns>
    public static bool AppendFileBytes<T>(string filepath, T obj, bool createIfNull = false)
    {
      if (DoesFileExist(filepath, createIfNull))
        return AppendFileBytesInternal(filepath, obj);

      return false;
    }

    /// <summary>
    /// A function for appending a serializable object to a file.
    /// </summary>
    /// <typeparam name="T">The type of the object. This must be serializable!</typeparam>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="obj">The object to serialize and append.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was sucessfully appended to.</returns>
    public static bool AppendFileBytes<T>(FileInfo fileInfo, T obj, bool createIfNull = false)
    {
      if (DoesFileExist(fileInfo, createIfNull))
        return AppendFileBytesInternal(fileInfo.FullName, obj);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending a serializable object to a file.
    /// </summary>
    /// <typeparam name="T">The type of the object. This must be serializable!</typeparam>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="obj">The object to serialize and append.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was sucessfully appended to.</returns>
    public static async Task<bool> AppendFileBytesAsync<T>(CancellationToken token, string filepath,
                                                           T obj, bool createIfNull = false)
    {
      if (DoesFileExist(filepath, createIfNull))
        return await AppendFileBytesAsyncInternal(token, filepath, obj);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending a serializable object to a file.
    /// </summary>
    /// <typeparam name="T">The type of the object. This must be serializable!</typeparam>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="obj">The object to serialize and append.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was sucessfully appended to.</returns>
    public static async Task<bool> AppendFileBytesAsync<T>(CancellationToken token,
                                                           FileInfo fileInfo, T obj,
                                                           bool createIfNull = false)
    {
      if (DoesFileExist(fileInfo, createIfNull))
        return await AppendFileBytesAsyncInternal(token, fileInfo.FullName, obj);

      return false;
    }

    /// <summary>
    /// A function for appending a serializable object to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <typeparam name="T">The type of the object. This must be serializable!</typeparam>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="obj">The object to serialize and append.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was sucessfully appended to.</returns>
    public static bool AppendFileBytesSafe<T>(string filepath, T obj, bool createIfNull = false)
    {
      if (DoesFileExist(filepath, createIfNull))
        return AppendFileBytesSafeInternal(filepath, obj);

      return false;
    }

    /// <summary>
    /// A function for appending a serializable object to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <typeparam name="T">The type of the object. This must be serializable!</typeparam>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="obj">The object to serialize and append.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was sucessfully appended to.</returns>
    public static bool AppendFileBytesSafe<T>(FileInfo fileInfo, T obj, bool createIfNull = false)
    {
      if (DoesFileExist(fileInfo, createIfNull))
        return AppendFileBytesSafeInternal(fileInfo.FullName, obj);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending a serializable object to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <typeparam name="T">The type of the object. This must be serializable!</typeparam>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="obj">The object to serialize and append.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was sucessfully appended to.</returns>
    public static async Task<bool> AppendFileBytesSafeAsync<T>(CancellationToken token,
                                                               string filepath, T obj,
                                                               bool createIfNull = false)
    {
      if (DoesFileExist(filepath, createIfNull))
        return await AppendFileBytesSafeAsyncInternal(token, filepath, obj);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending a serializable object to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <typeparam name="T">The type of the object. This must be serializable!</typeparam>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="obj">The object to serialize and append.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was sucessfully appended to.</returns>
    public static async Task<bool> AppendFileBytesSafeAsync<T>(CancellationToken token,
                                                               FileInfo fileInfo, T obj,
                                                               bool createIfNull = false)
    {
      if (DoesFileExist(fileInfo, createIfNull))
        return await AppendFileBytesSafeAsyncInternal(token, fileInfo.FullName, obj);

      return false;
    }

    /// <summary>
    /// A function for appending a <see cref="string"/> to a file.
    /// </summary>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="message">The <see cref="string"/> to write.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static bool AppendFileString(string filepath, string message, bool newline = true,
                                        bool createIfNull = false)
    {
      if (DoesFileExist(filepath, createIfNull))
        return AppendFileStringInternal(filepath, message, newline, DefaultEncoding);

      return false;
    }

    /// <summary>
    /// A function for appending a <see cref="string"/> to a file.
    /// </summary>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="message">The <see cref="string"/> to write.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static bool AppendFileString(FileInfo fileInfo, string message, bool newline = true,
                                        bool createIfNull = false)
    {
      if (DoesFileExist(fileInfo, createIfNull))
        return AppendFileStringInternal(fileInfo.FullName, message, newline, DefaultEncoding);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending a <see cref="string"/> to a file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="message">The <see cref="string"/> to write.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static async Task<bool> AppendFileStringAsync(CancellationToken token, string filepath,
                                                         string message, bool newline = true,
                                                         bool createIfNull = false)
    {
      if (DoesFileExist(filepath, createIfNull))
        return await AppendFileStringAsyncInternal(token, filepath, message, newline, DefaultEncoding);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending a <see cref="string"/> to a file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="message">The <see cref="string"/> to write.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static async Task<bool> AppendFileStringAsync(CancellationToken token, FileInfo fileInfo,
                                                         string message, bool newline = true,
                                                         bool createIfNull = false)
    {
      if (DoesFileExist(fileInfo, createIfNull))
        return await AppendFileStringAsyncInternal(token, fileInfo.FullName, message, newline, DefaultEncoding);

      return false;
    }

    /// <summary>
    /// A function for appending a <see cref="string"/> to a file.
    /// </summary>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="message">The <see cref="string"/> to write.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static bool AppendFileString(string filepath, string message, Encoding encoding,
                                        bool newline = true, bool createIfNull = false)
    {
      if (encoding != null && DoesFileExist(filepath, createIfNull))
        return AppendFileStringInternal(filepath, message, newline, encoding);

      return false;
    }

    /// <summary>
    /// A function for appending a <see cref="string"/> to a file.
    /// </summary>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="message">The <see cref="string"/> to write.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static bool AppendFileString(FileInfo fileInfo, string message, Encoding encoding,
                                        bool newline = true, bool createIfNull = false)
    {
      if (encoding != null && DoesFileExist(fileInfo, createIfNull))
        return AppendFileStringInternal(fileInfo.FullName, message, newline, encoding);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending a <see cref="string"/> to a file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="message">The <see cref="string"/> to write.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static async Task<bool> AppendFileStringAsync(CancellationToken token, string filepath,
                                                         string message, Encoding encoding,
                                                         bool newline = true,
                                                         bool createIfNull = false)
    {
      if (DoesFileExist(filepath, createIfNull))
        return await AppendFileStringAsyncInternal(token, filepath, message, newline, encoding);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending a <see cref="string"/> to a file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="message">The <see cref="string"/> to write.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static async Task<bool> AppendFileStringAsync(CancellationToken token, FileInfo fileInfo,
                                                         string message, Encoding encoding,
                                                         bool newline = true,
                                                         bool createIfNull = false)
    {
      if (DoesFileExist(fileInfo, createIfNull))
        return await AppendFileStringAsyncInternal(token, fileInfo.FullName, message, newline, encoding);

      return false;
    }

    /// <summary>
    /// A function for appending a <see cref="string"/> to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="message">The <see cref="string"/> to write.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static bool AppendFileStringSafe(string filepath, string message, bool newline = true,
                                            bool createIfNull = false)
    {
      if (DoesFileExist(filepath, createIfNull))
        return AppendFileStringSafeInternal(filepath, message, newline, DefaultEncoding);

      return false;
    }

    /// <summary>
    /// A function for appending a <see cref="string"/> to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="message">The <see cref="string"/> to write.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static bool AppendFileStringSafe(FileInfo fileInfo, string message, bool newline = true,
                                            bool createIfNull = false)
    {
      if (DoesFileExist(fileInfo, createIfNull))
        return AppendFileStringSafeInternal(fileInfo.FullName, message, newline, DefaultEncoding);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending a <see cref="string"/> to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="message">The <see cref="string"/> to write.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static async Task<bool> AppendFileStringSafeAsync(CancellationToken token,
                                                             string filepath, string message,
                                                             bool newline = true,
                                                             bool createIfNull = false)
    {
      if (DoesFileExist(filepath, createIfNull))
        return await AppendFileStringSafeAsyncInternal(token, filepath, message, newline, DefaultEncoding);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending a <see cref="string"/> to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="message">The <see cref="string"/> to write.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static async Task<bool> AppendFileStringSafeAsync(CancellationToken token,
                                                             FileInfo fileInfo, string message,
                                                             bool newline = true,
                                                             bool createIfNull = false)
    {
      if (DoesFileExist(fileInfo, createIfNull))
        return await AppendFileStringSafeAsyncInternal(token, fileInfo.FullName, message, newline, DefaultEncoding);

      return false;
    }

    /// <summary>
    /// A function for appending a <see cref="string"/> to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="message">The <see cref="string"/> to write.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static bool AppendFileStringSafe(string filepath, string message, Encoding encoding,
                                            bool newline = true, bool createIfNull = false)
    {
      if (encoding != null && DoesFileExist(filepath, createIfNull))
        return AppendFileStringSafeInternal(filepath, message, newline, encoding);

      return false;
    }

    /// <summary>
    /// A function for appending a <see cref="string"/> to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="message">The <see cref="string"/> to write.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static bool AppendFileStringSafe(FileInfo fileInfo, string message, Encoding encoding,
                                            bool newline = true, bool createIfNull = false)
    {
      if (encoding != null && DoesFileExist(fileInfo, createIfNull))
        return AppendFileStringSafeInternal(fileInfo.FullName, message, newline, encoding);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending a <see cref="string"/> to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="message">The <see cref="string"/> to write.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static async Task<bool> AppendFileStringSafeAsync(CancellationToken token,
                                                             string filepath, string message,
                                                             Encoding encoding, bool newline = true,
                                                             bool createIfNull = false)
    {
      if (DoesFileExist(filepath, createIfNull))
        return await AppendFileStringSafeAsyncInternal(token, filepath, message, newline, encoding);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending a <see cref="string"/> to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="message">The <see cref="string"/> to write.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static async Task<bool> AppendFileStringSafeAsync(CancellationToken token,
                                                             FileInfo fileInfo, string message,
                                                             Encoding encoding, bool newline = true,
                                                             bool createIfNull = false)
    {
      if (DoesFileExist(fileInfo, createIfNull))
        return await AppendFileStringSafeAsyncInternal(token, fileInfo.FullName, message, newline, encoding);

      return false;
    }

    /// <summary>
    /// A function for appending several <see cref="string"/>s to a file.
    /// </summary>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="messages">The <see cref="string"/>s to write.</param>
    /// <param name="newline">A toggle for adding a new line after each message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static bool AppendFileStrings(string filepath, IEnumerable<string> messages,
                                         bool newline = true, bool createIfNull = false)
    {
      if (DoesFileExist(filepath, createIfNull))
        return AppendFileStringsInternal(filepath, messages, newline, DefaultEncoding);

      return false;
    }

    /// <summary>
    /// A function for appending several <see cref="string"/>s to a file.
    /// </summary>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="messages">The <see cref="string"/>s to write.</param>
    /// <param name="newline">A toggle for adding a new line after each message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static bool AppendFileStrings(FileInfo fileInfo, IEnumerable<string> messages,
                                         bool newline = true, bool createIfNull = false)
    {
      if (DoesFileExist(fileInfo, createIfNull))
        return AppendFileStringsInternal(fileInfo.FullName, messages, newline, DefaultEncoding);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending several <see cref="string"/>s to a file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="messages">The <see cref="string"/>s to write.</param>
    /// <param name="newline">A toggle for adding a new line after each message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static async Task<bool> AppendFileStringsAsync(CancellationToken token, string filepath,
                                                          IEnumerable<string> messages,
                                                          bool newline = true,
                                                          bool createIfNull = false)
    {
      if (DoesFileExist(filepath, createIfNull))
        return await AppendFileStringsAsyncInternal(token, filepath, messages, newline, DefaultEncoding);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending several <see cref="string"/>s to a file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="messages">The <see cref="string"/>s to write.</param>
    /// <param name="newline">A toggle for adding a new line after each message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static async Task<bool> AppendFileStringsAsync(CancellationToken token, FileInfo fileInfo,
                                                          IEnumerable<string> messages,
                                                          bool newline = true,
                                                          bool createIfNull = false)
    {
      if (DoesFileExist(fileInfo, createIfNull))
        return await AppendFileStringsAsyncInternal(token, fileInfo.FullName, messages, newline, DefaultEncoding);

      return false;
    }

    /// <summary>
    /// A function for appending several <see cref="string"/>s to a file.
    /// </summary>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="messages">The <see cref="string"/>s to write.</param>
    /// <param name="newline">A toggle for adding a new line after each message.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static bool AppendFileStrings(string filepath, IEnumerable<string> messages,
                                         Encoding encoding, bool newline = true,
                                         bool createIfNull = false)
    {
      if (encoding != null && DoesFileExist(filepath, createIfNull))
        return AppendFileStringsInternal(filepath, messages, newline, encoding);

      return false;
    }

    /// <summary>
    /// A function for appending several <see cref="string"/>s to a file.
    /// </summary>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="messages">The <see cref="string"/>s to write.</param>
    /// <param name="newline">A toggle for adding a new line after each message.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static bool AppendFileStrings(FileInfo fileInfo, IEnumerable<string> messages,
                                         Encoding encoding, bool newline = true,
                                         bool createIfNull = false)
    {
      if (encoding != null && DoesFileExist(fileInfo, createIfNull))
        return AppendFileStringsInternal(fileInfo.FullName, messages, newline, encoding);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending several <see cref="string"/>s to a file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="messages">The <see cref="string"/>s to write.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <param name="newline">A toggle for adding a new line after each message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static async Task<bool> AppendFileStringsAsync(CancellationToken token, string filepath,
                                                          IEnumerable<string> messages,
                                                          Encoding encoding, bool newline = true,
                                                          bool createIfNull = false)
    {
      if (DoesFileExist(filepath, createIfNull))
        return await AppendFileStringsAsyncInternal(token, filepath, messages, newline, encoding);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending several <see cref="string"/>s to a file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="messages">The <see cref="string"/>s to write.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <param name="newline">A toggle for adding a new line after each message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static async Task<bool> AppendFileStringsAsync(CancellationToken token, FileInfo fileInfo,
                                                          IEnumerable<string> messages,
                                                          Encoding encoding, bool newline = true,
                                                          bool createIfNull = false)
    {
      if (DoesFileExist(fileInfo, createIfNull))
        return await AppendFileStringsAsyncInternal(token, fileInfo.FullName, messages, newline, encoding);

      return false;
    }

    /// <summary>
    /// A function for appending several <see cref="string"/>s to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="messages">The <see cref="string"/>s to write.</param>
    /// <param name="newline">A toggle for adding a new line after each message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static bool AppendFileStringsSafe(string filepath, IEnumerable<string> messages,
                                             bool newline = true, bool createIfNull = false)
    {
      if (DoesFileExist(filepath, createIfNull))
        return AppendFileStringsSafeInternal(filepath, messages, newline, DefaultEncoding);

      return false;
    }

    /// <summary>
    /// A function for appending several <see cref="string"/>s to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="messages">The <see cref="string"/>s to write.</param>
    /// <param name="newline">A toggle for adding a new line after each message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static bool AppendFileStringsSafe(FileInfo fileInfo, IEnumerable<string> messages,
                                             bool newline = true, bool createIfNull = false)
    {
      if (DoesFileExist(fileInfo, createIfNull))
        return AppendFileStringsSafeInternal(fileInfo.FullName, messages, newline, DefaultEncoding);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending several <see cref="string"/>s to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="messages">The <see cref="string"/>s to write.</param>
    /// <param name="newline">A toggle for adding a new line after each message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static async Task<bool> AppendFileStringsSafeAsync(CancellationToken token,
                                                              string filepath,
                                                              IEnumerable<string> messages,
                                                              bool newline = true,
                                                              bool createIfNull = false)
    {
      if (DoesFileExist(filepath, createIfNull))
        return await AppendFileStringsSafeAsyncInternal(token, filepath, messages, newline, DefaultEncoding);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending several <see cref="string"/>s to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="messages">The <see cref="string"/>s to write.</param>
    /// <param name="newline">A toggle for adding a new line after each message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static async Task<bool> AppendFileStringsSafeAsync(CancellationToken token,
                                                              FileInfo fileInfo,
                                                              IEnumerable<string> messages,
                                                              bool newline = true,
                                                              bool createIfNull = false)
    {
      if (DoesFileExist(fileInfo, createIfNull))
        return await AppendFileStringsSafeAsyncInternal(token, fileInfo.FullName, messages, newline, DefaultEncoding);

      return false;
    }

    /// <summary>
    /// A function for appending several <see cref="string"/>s to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="messages">The <see cref="string"/>s to write.</param>
    /// <param name="newline">A toggle for adding a new line after each message.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static bool AppendFileStringsSafe(string filepath, IEnumerable<string> messages,
                                             Encoding encoding, bool newline = true,
                                             bool createIfNull = false)
    {
      if (encoding != null && DoesFileExist(filepath, createIfNull))
        return AppendFileStringsSafeInternal(filepath, messages, newline, encoding);

      return false;
    }

    /// <summary>
    /// A function for appending several <see cref="string"/>s to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="messages">The <see cref="string"/>s to write.</param>
    /// <param name="newline">A toggle for adding a new line after each message.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static bool AppendFileStringsSafe(FileInfo fileInfo, IEnumerable<string> messages,
                                             Encoding encoding, bool newline = true,
                                             bool createIfNull = false)
    {
      if (encoding != null && DoesFileExist(fileInfo, createIfNull))
        return AppendFileStringsSafeInternal(fileInfo.FullName, messages, newline, encoding);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending several <see cref="string"/>s to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="messages">The <see cref="string"/>s to write.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <param name="newline">A toggle for adding a new line after each message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static async Task<bool> AppendFileStringsSafeAsync(CancellationToken token,
                                                              string filepath,
                                                              IEnumerable<string> messages,
                                                              Encoding encoding, bool newline = true,
                                                              bool createIfNull = false)
    {
      if (DoesFileExist(filepath, createIfNull))
        return await AppendFileStringsSafeAsyncInternal(token, filepath, messages, newline, encoding);

      return false;
    }

    /// <summary>
    /// A function for asynchronously appending several <see cref="string"/>s to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The file to append to.</param>
    /// <param name="messages">The <see cref="string"/>s to write.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <param name="newline">A toggle for adding a new line after each message.</param>
    /// <param name="createIfNull">A toggle for creating the file if it does not exist.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    public static async Task<bool> AppendFileStringsSafeAsync(CancellationToken token,
                                                              FileInfo fileInfo,
                                                              IEnumerable<string> messages,
                                                              Encoding encoding, bool newline = true,
                                                              bool createIfNull = false)
    {
      if (DoesFileExist(fileInfo, createIfNull))
        return await AppendFileStringsSafeAsyncInternal(token, fileInfo.FullName, messages, newline, encoding);

      return false;
    }

    /// <summary>
    /// A function for copying a file to a new location.
    /// </summary>
    /// <param name="filepath">The original file to copy.</param>
    /// <param name="destination">The destination path to copy to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully copied.</returns>
    public static bool CopyFile(string filepath, string destination, bool overwrite)
    {
      if (!string.IsNullOrEmpty(filepath) && !string.IsNullOrEmpty(destination))
        return CopyFileInternal(filepath, destination, overwrite);

      return false;
    }

    /// <summary>
    /// A function for copying a file to a new location.
    /// </summary>
    /// <param name="fileInfo">The original file to copy.</param>
    /// <param name="destination">The destination path to copy to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully copied.</returns>
    public static bool CopyFile(FileInfo fileInfo, string destination, bool overwrite)
    {
      if (fileInfo != null && !string.IsNullOrEmpty(destination))
        return CopyFileInternal(fileInfo.FullName, destination, overwrite);

      return false;
    }

    /// <summary>
    /// A function for copying a file to a new location.
    /// </summary>
    /// <param name="filepath">The original file to copy.</param>
    /// <param name="destination">The destination path to copy to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully copied.</returns>
    public static bool CopyFile(string filepath, FileInfo destination, bool overwrite)
    {
      if (!string.IsNullOrEmpty(filepath) && destination != null)
        return CopyFileInternal(filepath, destination.FullName, overwrite);

      return false;
    }

    /// <summary>
    /// A function for copying a file to a new location.
    /// </summary>
    /// <param name="fileInfo">The original file to copy.</param>
    /// <param name="destination">The destination path to copy to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully copied.</returns>
    public static bool CopyFile(FileInfo fileInfo, FileInfo destination, bool overwrite)
    {
      if (fileInfo != null && destination != null)
        return CopyFileInternal(fileInfo.FullName, destination.FullName, overwrite);

      return false;
    }

    /// <summary>
    /// A function for copying a file to a new location.
    /// </summary>
    /// <param name="fileInfo">The original file to copy.</param>
    /// <param name="destination">The destination directory to copy to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully copied.</returns>
    public static bool CopyFile(FileInfo fileInfo, DirectoryInfo destination, bool overwrite)
    {
      if (fileInfo != null && destination != null)
      {
        // Retain the same filename.
        string path = Path.Combine(destination.FullName, fileInfo.Name);
        return CopyFileInternal(fileInfo.FullName, path, overwrite);
      }

      return false;
    }

    /// <summary>
    /// A function asynchronously for copying a file to a new location.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The original file to copy.</param>
    /// <param name="destination">The destination path to copy to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully copied.</returns>
    public static async Task<bool> CopyFileAsync(CancellationToken token, string filepath,
                                                 string destination, bool overwrite)
    {
      if (!string.IsNullOrEmpty(filepath) && !string.IsNullOrEmpty(destination))
        return await CopyFileAsyncInternal(token, filepath, destination, overwrite);

      return false;
    }

    /// <summary>
    /// A function asynchronously for copying a file to a new location.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The original file to copy.</param>
    /// <param name="destination">The destination path to copy to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully copied.</returns>
    public static async Task<bool> CopyFileAsync(CancellationToken token, FileInfo fileInfo,
                                                 string destination, bool overwrite)
    {
      if (fileInfo != null && !string.IsNullOrEmpty(destination))
        return await CopyFileAsyncInternal(token, fileInfo.FullName, destination, overwrite);

      return false;
    }

    /// <summary>
    /// A function asynchronously for copying a file to a new location.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The original file to copy.</param>
    /// <param name="destination">The destination path to copy to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully copied.</returns>
    public static async Task<bool> CopyFileAsync(CancellationToken token, string filepath,
                                                 FileInfo destination, bool overwrite)
    {
      if (!string.IsNullOrEmpty(filepath) && destination != null)
        return await CopyFileAsyncInternal(token, filepath, destination.FullName, overwrite);

      return false;
    }

    /// <summary>
    /// A function asynchronously for copying a file to a new location.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The original file to copy.</param>
    /// <param name="destination">The destination path to copy to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully copied.</returns>
    public static async Task<bool> CopyFileAsync(CancellationToken token, FileInfo fileInfo,
                                                 FileInfo destination, bool overwrite)
    {
      if (fileInfo != null && destination != null)
        return await CopyFileAsyncInternal(token, fileInfo.FullName, destination.FullName, overwrite);

      return false;
    }

    /// <summary>
    /// A function for creating a directory safely.
    /// </summary>
    /// <param name="directory">The path of the directory to create.</param>
    /// <returns>Returns if the directory was successfully created.</returns>
    public static bool CreateDirectory(string directory)
    {
      return !string.IsNullOrWhiteSpace(directory) && CreateDirectoryInternal(directory);
    }

    /// <summary>
    /// A function for creating a directory safely.
    /// </summary>
    /// <param name="info">The <see cref="DirectoryInfo"/> to base the directory on.</param>
    /// <returns>Returns if the directory was successfully created.</returns>
    public static bool CreateDirectory(DirectoryInfo info)
    {
      return info != null && CreateDirectoryInternal(info.FullName);
    }

    /// <summary>
    /// A function for creating a directory safely.
    /// </summary>
    /// <param name="info">The <see cref="FileInfo"/> to base the directory on.</param>
    /// <returns>Returns if the directory was successfully created.</returns>
    public static bool CreateDirectory(FileInfo info)
    {
      return info != null && CreateDirectoryInternal(info.DirectoryName);
    }

    /// <summary>
    /// A function for creating a file safely.
    /// </summary>
    /// <param name="filepath">The filepath for the file.</param>
    /// <param name="info">The <see cref="FileInfo"/> of the created file, if successful.</param>
    /// <param name="makeDirectory">A toggle for allowing directories to be made if they
    /// do not already exist.</param>
    /// <param name="overwrite">A toggle for allowing overwriting an existing file.</param>
    /// <returns>Returns if the file was successfully created.</returns>
    public static bool CreateFile(string filepath, out FileInfo info, bool makeDirectory,
                                  bool overwrite)
    {
      info = null; // Initialize the FileInfo as null.

      // Only create the file if it does not exist, or can be overwritten.
      if ((!File.Exists(filepath) || overwrite) && CreateFileInternal(filepath, makeDirectory))
      {
        // If successful, get the FileInfo and return true.
        info = new FileInfo(filepath);
        return true;
      }

      return false; // The filepath was not valid, or the file could not be made.
    }

    /// <summary>
    /// A function for creating a file safely.
    /// </summary>
    /// <param name="info">The <see cref="FileInfo"/> of the file to create.</param>
    /// <param name="makeDirectory">A toggle for allowing directories to be made if they
    /// do not already exist.</param>
    /// <param name="overwrite">A toggle for allowing overwriting an existing file.</param>
    /// <returns>Returns if the file was successfully created.</returns>
    public static bool CreateFile(FileInfo info, bool makeDirectory, bool overwrite)
    {
      // If the FileInfo does not exist, return false immediately.
      if (info == null)
        return false;

      string filepath = info.FullName; // Get the full filepath.

      // Only create the file if it does not exist, or can be overwritten.
      if ((!File.Exists(filepath) || overwrite) && CreateFileInternal(filepath, makeDirectory))
        return true;

      return false; // The filepath was not valid, or the file could not be made.
    }

    /// <summary>
    /// A function for creating a file safely.
    /// </summary>
    /// <param name="filepath">The filepath for the file.</param>
    /// <param name="makeDirectory">A toggle for allowing directories to be made if they
    /// do not already exist.</param>
    /// <param name="overwrite">A toggle for allowing overwriting an existing file.</param>
    /// <returns>Returns the file's <see cref="FileInfo"/>, if successful or if the file already
    /// exists. Returns <see langword="null"/> otherwise.</returns>
    public static FileInfo CreateFile(string filepath, bool makeDirectory, bool overwrite)
    {
      // If the file already exists, we can make the FileInfo early.
      if (File.Exists(filepath))
      {
        // If overwriting, only return correctly if the file was properly overwritten.
        if (overwrite)
        {
          if (CreateFileInternal(filepath, makeDirectory))
            return new FileInfo(filepath);

          return null;
        }

        return new FileInfo(filepath); // Otherwise, just return the FileInfo to the file.
      }

      // If not overwriting, attempt to create the file.
      if (CreateFileInternal(filepath, makeDirectory))
        return new FileInfo(filepath);

      return null; // The filepath was not valid, or the file could not be made.
    }

    /// <summary>
    /// A function for safely creating a temporary file.
    /// </summary>
    /// <param name="temppath">The path to the temporary file.</param>
    /// <returns>Returns if the temporary file was created successfully.</returns>
    public static bool CreateTempFile(out string temppath)
    {
      try
      {
        temppath = Path.GetTempFileName();
        return true;
      }
      catch
      {

      }

      temppath = string.Empty;
      return false;
    }

    /// <summary>
    /// A function for deleting a directory.
    /// </summary>
    /// <param name="directory">The directory to delete.</param>
    /// <param name="recursive">A toggle for also deleting any files in the directory and any
    /// empty subdirectories.</param>
    /// <returns>Returns if the deletion was successful.</returns>
    public static bool DeleteDirectory(string directory, bool recursive)
    {
      if (directory != null)
        return DeleteDirectoryInternal(directory, recursive);

      return false;
    }

    /// <summary>
    /// A function for deleting a directory.
    /// </summary>
    /// <param name="directory">The directory to delete.</param>
    /// <param name="recursive">A toggle for also deleting any files in the directory and any
    /// empty subdirectories.</param>
    /// <returns>Returns if the deletion was successful.</returns>
    public static bool DeleteDirectory(DirectoryInfo directory, bool recursive)
    {
      if (directory != null)
        return DeleteDirectoryInternal(directory.FullName, recursive);

      return false;
    }

    /// <summary>
    /// A function for safely deleting some file at a <paramref name="filepath"/>.
    /// </summary>
    /// <param name="filepath">The path to the file to delete.</param>
    /// <returns>Returns if the file was successfully deleted.</returns>
    public static bool DeleteFile(string filepath)
    {
      if (!string.IsNullOrEmpty(filepath))
        return DeleteFileInternal(filepath);

      return false;
    }

    /// <summary>
    /// A function for safely deleting some file.
    /// </summary>
    /// <param name="fileInfo">The path to the file to delete.</param>
    /// <returns>Returns if the file was successfully deleted.</returns>
    public static bool DeleteFile(FileInfo fileInfo)
    {
      if (fileInfo != null)
        return DeleteFileInternal(fileInfo.FullName);

      return false;
    }

    /// <summary>
    /// A function for moving a file to a new location.
    /// </summary>
    /// <param name="filepath">The original file to move.</param>
    /// <param name="destination">The destination path to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    public static bool MoveFile(string filepath, string destination, bool overwrite)
    {
      if (!string.IsNullOrEmpty(filepath) && !string.IsNullOrEmpty(destination))
        return MoveFileInternal(filepath, destination, overwrite);

      return false;
    }

    /// <summary>
    /// A function for moving a file to a new location.
    /// </summary>
    /// <param name="fileInfo">The original file to move.</param>
    /// <param name="destination">The destination path to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    public static bool MoveFile(FileInfo fileInfo, string destination, bool overwrite)
    {
      if (fileInfo != null && !string.IsNullOrEmpty(destination))
        return MoveFileInternal(fileInfo.FullName, destination, overwrite);

      return false;
    }

    /// <summary>
    /// A function for moving a file to a new location.
    /// </summary>
    /// <param name="filepath">The original file to move.</param>
    /// <param name="destination">The destination path to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    public static bool MoveFile(string filepath, FileInfo destination, bool overwrite)
    {
      if (!string.IsNullOrEmpty(filepath) && destination != null)
        return MoveFileInternal(filepath, destination.FullName, overwrite);

      return false;
    }

    /// <summary>
    /// A function for moving a file to a new location.
    /// </summary>
    /// <param name="fileInfo">The original file to move.</param>
    /// <param name="destination">The destination path to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    public static bool MoveFile(FileInfo fileInfo, FileInfo destination, bool overwrite)
    {
      if (fileInfo != null && destination != null)
        return MoveFileInternal(fileInfo.FullName, destination.FullName, overwrite);

      return false;
    }

    /// <summary>
    /// A function for moving a file to a new location.
    /// </summary>
    /// <param name="fileInfo">The original file to move.</param>
    /// <param name="destination">The destination directory to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    public static bool MoveFile(FileInfo fileInfo, DirectoryInfo destination, bool overwrite)
    {
      if (fileInfo != null && destination != null)
      {
        // Retain the same filename.
        string path = Path.Combine(destination.FullName, fileInfo.Name);
        return MoveFileInternal(fileInfo.FullName, path, overwrite);
      }

      return false;
    }

    /// <summary>
    /// A function for asynchronously moving a file to a new location.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The original file to move.</param>
    /// <param name="destination">The destination path to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    public static async Task<bool> MoveFileAsync(CancellationToken token, string filepath,
                                                 string destination, bool overwrite)
    {
      if (!string.IsNullOrEmpty(filepath) && !string.IsNullOrEmpty(destination))
        return await MoveFileAsyncInternal(token, filepath, destination, overwrite);

      return false;
    }

    /// <summary>
    /// A function for asynchronously moving a file to a new location.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The original file to move.</param>
    /// <param name="destination">The destination path to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    public static async Task<bool> MoveFileAsync(CancellationToken token, FileInfo fileInfo,
                                                 string destination, bool overwrite)
    {
      if (fileInfo != null && !string.IsNullOrEmpty(destination))
        return await MoveFileAsyncInternal(token, fileInfo.FullName, destination, overwrite);

      return false;
    }

    /// <summary>
    /// A function for asynchronously moving a file to a new location.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The original file to move.</param>
    /// <param name="destination">The destination path to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    public static async Task<bool> MoveFileAsync(CancellationToken token, string filepath,
                                                 FileInfo destination, bool overwrite)
    {
      if (!string.IsNullOrEmpty(filepath) && destination != null)
        return await MoveFileAsyncInternal(token, filepath, destination.FullName, overwrite);

      return false;
    }

    /// <summary>
    /// A function for asynchronously moving a file to a new location.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The original file to move.</param>
    /// <param name="destination">The destination path to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    public static async Task<bool> MoveFileAsync(CancellationToken token, FileInfo fileInfo,
                                                 FileInfo destination, bool overwrite)
    {
      if (fileInfo != null && destination != null)
        return await MoveFileAsyncInternal(token, fileInfo.FullName, destination.FullName, overwrite);

      return false;
    }

    /// <summary>
    /// A function for asynchronously moving a file to a new location.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The original file to move.</param>
    /// <param name="destination">The destination directory to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    public static async Task<bool> MoveFileAsync(CancellationToken token, FileInfo fileInfo,
                                                 DirectoryInfo destination, bool overwrite)
    {
      if (fileInfo != null && destination != null)
      {
        string path = Path.Combine(destination.FullName, fileInfo.Name);
        return await MoveFileAsyncInternal(token, fileInfo.FullName, path, overwrite);
      } 

      return false;
    }

    /// <summary>
    /// A function for moving a file to a new location. This makes a copy of the original
    /// file, if overwriting.
    /// </summary>
    /// <param name="filepath">The original file to move.</param>
    /// <param name="destination">The destination path to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    public static bool MoveFileSafe(string filepath, string destination, bool overwrite)
    {
      if (!string.IsNullOrEmpty(filepath) && !string.IsNullOrEmpty(destination))
        return MoveFileSafeInternal(filepath, destination, overwrite);

      return false;
    }

    /// <summary>
    /// A function for moving a file to a new location. This makes a copy of the original
    /// file, if overwriting.
    /// </summary>
    /// <param name="fileInfo">The original file to move.</param>
    /// <param name="destination">The destination path to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    public static bool MoveFileSafe(FileInfo fileInfo, string destination, bool overwrite)
    {
      if (fileInfo != null && !string.IsNullOrEmpty(destination))
        return MoveFileSafeInternal(fileInfo.FullName, destination, overwrite);

      return false;
    }

    /// <summary>
    /// A function for moving a file to a new location. This makes a copy of the original
    /// file, if overwriting.
    /// </summary>
    /// <param name="filepath">The original file to move.</param>
    /// <param name="destination">The destination path to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    public static bool MoveFileSafe(string filepath, FileInfo destination, bool overwrite)
    {
      if (!string.IsNullOrEmpty(filepath) && destination != null)
        return MoveFileSafeInternal(filepath, destination.FullName, overwrite);

      return false;
    }

    /// <summary>
    /// A function for moving a file to a new location. This makes a copy of the original
    /// file, if overwriting.
    /// </summary>
    /// <param name="fileInfo">The original file to move.</param>
    /// <param name="destination">The destination path to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    public static bool MoveFileSafe(FileInfo fileInfo, FileInfo destination, bool overwrite)
    {
      if (fileInfo != null && destination != null)
        return MoveFileSafeInternal(fileInfo.FullName, destination.FullName, overwrite);

      return false;
    }

    /// <summary>
    /// A function for moving a file to a new location. This makes a copy of the original
    /// file, if overwriting.
    /// </summary>
    /// <param name="fileInfo">The original file to move.</param>
    /// <param name="destination">The destination directory to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    public static bool MoveFileSafe(FileInfo fileInfo, DirectoryInfo destination, bool overwrite)
    {
      if (fileInfo != null && destination != null)
      {
        // Retain the same filename.
        string path = Path.Combine(destination.FullName, fileInfo.Name);
        return MoveFileSafeInternal(fileInfo.FullName, path, overwrite);
      }

      return false;
    }

    /// <summary>
    /// A function for asynchronously moving a file to a new location. This makes a
    /// copy of the original file, if overwriting.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The original file to move.</param>
    /// <param name="destination">The destination path to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    public static async Task<bool> MoveFileSafeAsync(CancellationToken token, string filepath,
                                                     string destination, bool overwrite)
    {
      if (!string.IsNullOrEmpty(filepath) && !string.IsNullOrEmpty(destination))
        return await MoveFileSafeAsyncInternal(token, filepath, destination, overwrite);

      return false;
    }

    /// <summary>
    /// A function for asynchronously moving a file to a new location. This makes a
    /// copy of the original file, if overwriting.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The original file to move.</param>
    /// <param name="destination">The destination path to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    public static async Task<bool> MoveFileSafeAsync(CancellationToken token, FileInfo fileInfo,
                                                     string destination, bool overwrite)
    {
      if (fileInfo != null && !string.IsNullOrEmpty(destination))
        return await MoveFileSafeAsyncInternal(token, fileInfo.FullName, destination, overwrite);

      return false;
    }

    /// <summary>
    /// A function for asynchronously moving a file to a new location. This makes a
    /// copy of the original file, if overwriting.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The original file to move.</param>
    /// <param name="destination">The destination path to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    public static async Task<bool> MoveFileSafeAsync(CancellationToken token, string filepath,
                                                     FileInfo destination, bool overwrite)
    {
      if (!string.IsNullOrEmpty(filepath) && destination != null)
        return await MoveFileSafeAsyncInternal(token, filepath, destination.FullName, overwrite);

      return false;
    }

    /// <summary>
    /// A function for asynchronously moving a file to a new location. This makes a
    /// copy of the original file, if overwriting.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The original file to move.</param>
    /// <param name="destination">The destination path to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    public static async Task<bool> MoveFileSafeAsync(CancellationToken token, FileInfo fileInfo,
                                                     FileInfo destination, bool overwrite)
    {
      if (fileInfo != null && destination != null)
        return await MoveFileSafeAsyncInternal(token, fileInfo.FullName, destination.FullName, overwrite);

      return false;
    }

    /// <summary>
    /// A function for asynchronously moving a file to a new location. This makes a
    /// copy of the original file, if overwriting.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The original file to move.</param>
    /// <param name="destination">The destination directory to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    public static async Task<bool> MoveFileSafeAsync(CancellationToken token, FileInfo fileInfo,
                                                     DirectoryInfo destination, bool overwrite)
    {
      if (fileInfo != null && destination != null)
      {
        string path = Path.Combine(destination.FullName, fileInfo.Name);
        return await MoveFileSafeAsyncInternal(token, fileInfo.FullName, path, overwrite);
      }

      return false;
    }

    /// <summary>
    /// A function for reading the <see cref="byte"/>s of a file.
    /// </summary>
    /// <param name="filepath">The file to read from.</param>
    /// <param name="bytes">The array to store the read <see cref="byte"/>s in.</param>
    /// <param name="offset">The number of <see cref="byte"/>s to skip from the start.</param>
    /// <param name="count">The number of <see cref="byte"/>s to read from the
    /// <paramref name="offset"/>. Values 0 or less indicate reading the whole file.</param>
    /// <returns>Returns if the file was succesfully read.</returns>
    public static bool ReadFileBytes(string filepath, out byte[] bytes, int offset = 0,
                                     int count = 0)
    {
      if (!string.IsNullOrEmpty(filepath))
        return ReadFileBytesInternal(filepath, out bytes, Maths.Max(0, offset), count);

      bytes = null;
      return false;
    }

    /// <summary>
    /// A function for reading the <see cref="byte"/>s of a file.
    /// </summary>
    /// <param name="fileInfo">The file to read from.</param>
    /// <param name="bytes">The array to store the read <see cref="byte"/>s in.</param>
    /// <param name="offset">The number of <see cref="byte"/>s to skip from the start.</param>
    /// <param name="count">The number of <see cref="byte"/>s to read from the
    /// <paramref name="offset"/>. Values 0 or less indicate reading the whole file.</param>
    /// <returns>Returns if the file was succesfully read.</returns>
    public static bool ReadFileBytes(FileInfo fileInfo, out byte[] bytes, int offset = 0,
                                     int count = 0)
    {
      if (fileInfo != null)
        return ReadFileBytesInternal(fileInfo.FullName, out bytes, Maths.Max(0, offset), count);

      bytes = null;
      return false;
    }

    /// <summary>
    /// A function for asynchronously reading the <see cref="byte"/>s of a file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to read from.</param>
    /// <param name="offset">The number of <see cref="byte"/>s to skip from the start.</param>
    /// <param name="count">The number of <see cref="byte"/>s to read from the
    /// <paramref name="offset"/>. Values 0 or less indicate reading the whole file.</param>
    /// <returns>Returns a <see cref="TReturn{T0}"/> based on the read's results.</returns>
    public static async Task<TReturn<byte[]>> ReadFileBytesAsync(CancellationToken token,
                                                                 string filepath, int offset = 0,
                                                                 int count = 0)
    {
      if (!string.IsNullOrEmpty(filepath))
        return await ReadFileBytesAsyncInternal(token, filepath, Maths.Max(0, offset), count);

      return default;
    }

    /// <summary>
    /// A function for asynchronously reading the <see cref="byte"/>s of a file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The file to read from.</param>
    /// <param name="offset">The number of <see cref="byte"/>s to skip from the start.</param>
    /// <param name="count">The number of <see cref="byte"/>s to read from the
    /// <paramref name="offset"/>. Values 0 or less indicate reading the whole file.</param>
    /// <returns>Returns a <see cref="TReturn{T0}"/> based on the read's results.</returns>
    public static async Task<TReturn<byte[]>> ReadFileBytesAsync(CancellationToken token,
                                                                 FileInfo fileInfo,
                                                                 int offset = 0, int count = 0)
    {
      if (fileInfo != null)
        return await ReadFileBytesAsyncInternal(token, fileInfo.FullName, Maths.Max(0, offset), count);

      return default;
    }

    /// <summary>
    /// A function for reading the <see cref="byte"/>s of a file and converting them
    /// into an object.
    /// </summary>
    /// <typeparam name="T">The type of the <paramref name="obj"/>.</typeparam>
    /// <param name="filepath">The file to read from.</param>
    /// <param name="obj">The final object.</param>
    /// <param name="offset">The number of <see cref="byte"/>s to skip from the start.</param>
    /// <param name="count">The number of <see cref="byte"/>s to read from the
    /// <paramref name="offset"/>. Values 0 or less indicate reading the whole file.</param>
    /// <returns>Returns if the file was succesfully read.</returns>
    public static bool ReadFileBytes<T>(string filepath, out T obj, int offset = 0, int count = 0)
    {
      // Read the bytes. If successful, deserialize the bytes into an object.
      if (!string.IsNullOrEmpty(filepath))
        return ReadFileBytesInternal(filepath, out obj, Maths.Max(0, offset), count);

      obj = default;
      return false;
    }

    /// <summary>
    /// A function for reading the <see cref="byte"/>s of a file and converting them
    /// into an object.
    /// </summary>
    /// <typeparam name="T">The type of the <paramref name="obj"/>.</typeparam>
    /// <param name="fileInfo">The file to read from.</param>
    /// <param name="obj">The final object.</param>
    /// <param name="offset">The number of <see cref="byte"/>s to skip from the start.</param>
    /// <param name="count">The number of <see cref="byte"/>s to read from the
    /// <paramref name="offset"/>. Values 0 or less indicate reading the whole file.</param>
    /// <returns>Returns if the file was succesfully read.</returns>
    public static bool ReadFileBytes<T>(FileInfo fileInfo, out T obj, int offset = 0, int count = 0)
    {
      // Read the bytes. If successful, deserialize the bytes into an object.
      if (fileInfo != null)
        return ReadFileBytesInternal(fileInfo.FullName, out obj, Maths.Max(0, offset), count);

      obj = default;
      return false;
    }

    /// <summary>
    /// A function for asynchronously reading the <see cref="byte"/>s of a file and
    /// converting them into an object.
    /// </summary>
    /// <typeparam name="T">The type of the object the <see cref="byte"/>s represent.</typeparam>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to read from.</param>
    /// <param name="offset">The number of <see cref="byte"/>s to skip from the start.</param>
    /// <param name="count">The number of <see cref="byte"/>s to read from the
    /// <paramref name="offset"/>. Values 0 or less indicate reading the whole file.</param>
    /// <returns>Returns a <see cref="TReturn{T}"/> based on the read's results.</returns>
    public static async Task<TReturn<T>> ReadFileBytes<T>(CancellationToken token,
                                                          string filepath, int offset = 0,
                                                          int count = 0)
    {
      if (!string.IsNullOrEmpty(filepath))
        return await ReadFileBytesAsyncInternal<T>(token, filepath, Maths.Max(0, offset), count);

      return default;
    }

    /// <summary>
    /// A function for asynchronously reading the <see cref="byte"/>s of a file and
    /// converting them into an object.
    /// </summary>
    /// <typeparam name="T">The type of the object the <see cref="byte"/>s represent.</typeparam>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The file to read from.</param>
    /// <param name="offset">The number of <see cref="byte"/>s to skip from the start.</param>
    /// <param name="count">The number of <see cref="byte"/>s to read from the
    /// <paramref name="offset"/>. Values 0 or less indicate reading the whole file.</param>
    /// <returns>Returns a <see cref="TReturn{T}"/> based on the read's results.</returns>
    public static async Task<TReturn<T>> ReadFileBytes<T>(CancellationToken token,
                                                          FileInfo fileInfo, int offset = 0,
                                                          int count = 0)
    {
      if (offset >= 0 && fileInfo != null)
        return await ReadFileBytesAsyncInternal<T>(token, fileInfo.FullName, offset, count);

      return default;
    }

    /// <summary>
    /// A function for reading a specific line of a file.
    /// </summary>
    /// <param name="filepath">The file to read.</param>
    /// <param name="line">The line that was read.</param>
    /// <param name="lineNumber">The line to read, starting with 0.</param>
    /// <returns>Returns if the file was successfully read.</returns>
    public static bool ReadFileString(string filepath, out string line, long lineNumber)
    {
      if (!string.IsNullOrEmpty(filepath))
        return ReadFileStringInternal(filepath, out line, lineNumber);

      line = string.Empty;
      return false;
    }

    /// <summary>
    /// A function for reading a specific line of a file.
    /// </summary>
    /// <param name="fileInfo">The file to read.</param>
    /// <param name="line">The line that was read.</param>
    /// <param name="lineNumber">The line to read, starting with 0.</param>
    /// <returns>Returns if the file was successfully read.</returns>
    public static bool ReadFileString(FileInfo fileInfo, out string line, long lineNumber)
    {
      if (fileInfo != null)
        return ReadFileStringInternal(fileInfo.FullName, out line, lineNumber);

      line = string.Empty;
      return false;
    }

    /// <summary>
    /// A function for asynchronously reading a specific line of a file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to read.</param>
    /// <param name="lineNumber">The line to read, starting with 0.</param>
    /// <returns>Returns a <see cref="TReturn{T0}"/> based on the read's results.</returns>
    public static async Task<TReturn<string>> ReadFileStringAsync(CancellationToken token,
                                                                  string filepath,
                                                                  long lineNumber)
    {
      if (!string.IsNullOrEmpty(filepath))
        return await ReadFileStringAsyncInternal(token, filepath, lineNumber);

      return default;
    }

    /// <summary>
    /// A function for asynchronously reading a specific line of a file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The file to read.</param>
    /// <param name="lineNumber">The line to read, starting with 0.</param>
    /// <returns>Returns a <see cref="TReturn{T0}"/> based on the read's results.</returns>
    public static async Task<TReturn<string>> ReadFileStringAsync(CancellationToken token,
                                                                  FileInfo fileInfo,
                                                                  long lineNumber)
    {
      if (fileInfo != null)
        return await ReadFileStringAsyncInternal(token, fileInfo.FullName, lineNumber);

      return default;
    }

    /// <summary>
    /// A function for reading a section of a text file.
    /// </summary>
    /// <param name="filepath">The file to read.</param>
    /// <param name="lines">The outputted <see cref="List{T}"/> of read lines.</param>
    /// <param name="startIndex">The inclusive first index to read from.</param>
    /// <param name="lastIndex">The inclusive last index to read to.</param>
    /// <returns>Returns if the file was successfully read.</returns>
    public static bool ReadFileStrings(string filepath, out List<string> lines,
                                       long startIndex, long lastIndex)
    {
      if (!string.IsNullOrEmpty(filepath) && Maths.InRangeIE(startIndex, 0, lastIndex) &&
          Maths.InRangeEI(lastIndex, startIndex, long.MaxValue))
      {
        return ReadFileStringsInternal(filepath, out lines, startIndex, lastIndex);
      }

      lines = null;
      return false;
    }

    /// <summary>
    /// A function for reading a section of a text file.
    /// </summary>
    /// <param name="fileInfo">The file to read.</param>
    /// <param name="lines">The outputted <see cref="List{T}"/> of read lines.</param>
    /// <param name="startIndex">The inclusive first index to read from.</param>
    /// <param name="lastIndex">The inclusive last index to read to.</param>
    /// <returns>Returns if the file was successfully read.</returns>
    public static bool ReadFileStrings(FileInfo fileInfo, out List<string> lines,
                                       long startIndex, long lastIndex)
    {
      if (fileInfo != null && Maths.InRangeIE(startIndex, 0, lastIndex) &&
          Maths.InRangeEI(lastIndex, startIndex, long.MaxValue))
      {
        return ReadFileStringsInternal(fileInfo.FullName, out lines, startIndex, lastIndex);
      }

      lines = null;
      return false;
    }

    /// <summary>
    /// A function for asynchronously reading a section of a text file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to read.</param>
    /// <param name="startIndex">The inclusive first index to read from.</param>
    /// <param name="lastIndex">The inclusive last index to read to.</param>
    /// <returns>Returns a <see cref="TReturn{T0}"/> based on the read's results.</returns>
    public static async Task<TReturn<List<string>>> ReadFileStringsAsync(CancellationToken token,
                                                                         string filepath,
                                                                         long startIndex,
                                                                         long lastIndex)
    {
      if (!string.IsNullOrEmpty(filepath) && Maths.InRangeIE(startIndex, 0, lastIndex) &&
          Maths.InRangeEI(lastIndex, startIndex, long.MaxValue))
      {
        return await ReadFileStringsAsyncInternal(token, filepath, startIndex, lastIndex);
      }

      return default;
    }

    /// <summary>
    /// A function for asynchronously reading a section of a text file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="fileInfo">The file to read.</param>
    /// <param name="startIndex">The inclusive first index to read from.</param>
    /// <param name="lastIndex">The inclusive last index to read to.</param>
    /// <returns>Returns a <see cref="TReturn{T0}"/> based on the read's results.</returns>
    public static async Task<TReturn<List<string>>> ReadFileStringsAsync(CancellationToken token,
                                                                         FileInfo fileInfo,
                                                                         long startIndex,
                                                                         long lastIndex)
    {
      if (fileInfo != null && Maths.InRangeIE(startIndex, 0, lastIndex) &&
          Maths.InRangeEI(lastIndex, startIndex, long.MaxValue))
      {
        return await ReadFileStringsAsyncInternal(token, fileInfo.FullName, startIndex, lastIndex);
      }

      return default;
    }

    /// <summary>
    /// A function for reading all of the text of a file.
    /// </summary>
    /// <param name="filepath">The file to read.</param>
    /// <param name="text">The text of the file.</param>
    /// <returns>Returns if the <paramref name="text"/> was read successfully.</returns>
    public static bool ReadFileText(string filepath, out string text)
    {
      if (!string.IsNullOrEmpty(filepath))
        return ReadFileTextInternal(filepath, out text);

      text = string.Empty;
      return false;
    }

    /// <summary>
    /// A function for reading all of the text of a file.
    /// </summary>
    /// <param name="fileInfo">The file to read.</param>
    /// <param name="text">The text of the file.</param>
    /// <returns>Returns if the <paramref name="text"/> was read successfully.</returns>
    public static bool ReadFileText(FileInfo fileInfo, out string text)
    {
      if (fileInfo != null)
        return ReadFileTextInternal(fileInfo.FullName, out text);

      text = string.Empty;
      return false;
    }

    /// <summary>
    /// A function for asynchronously reading all of the text of a file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch. Currently, this
    /// function does not properly support cancellation due to API limitations.</param>
    /// <param name="filepath">The file to read.</param>
    /// <returns>Returns a <see cref="TReturn{T0}"/> based on the read's results.</returns>
    public static async Task<TReturn<string>> ReadFileTextAsync(CancellationToken token,
                                                                string filepath)
    {
      if (!string.IsNullOrEmpty(filepath))
        return await ReadFileTextAsyncInternal(token, filepath);

      return default;
    }

    /// <summary>
    /// A function for asynchronously reading all of the text of a file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch. Currently, this
    /// function does not properly support cancellation due to API limitations.</param>
    /// <param name="fileInfo">The file to read.</param>
    /// <returns>Returns a <see cref="TReturn{T0}"/> based on the read's results.</returns>
    public static async Task<TReturn<string>> ReadFileTextAsync(CancellationToken token,
                                                                FileInfo fileInfo)
    {
      if (fileInfo != null)
        return await ReadFileTextAsyncInternal(token, fileInfo.FullName);

      return default;
    }

    /// <summary>
    /// A function for getting an enumerator for a file, being able to read each
    /// individual line.
    /// </summary>
    /// <param name="filepath">The file to read.</param>
    /// <param name="enumerator">The <see cref="IEnumerable{T}"/> of the file's lines.</param>
    /// <returns>Returns if the file was successfully read.</returns>
    public static bool ReadFileText(string filepath, out IEnumerable<string> enumerator)
    {
      if (!string.IsNullOrEmpty(filepath))
        return ReadFileTextInternal(filepath, out enumerator);

      enumerator = null;
      return false;
    }

    /// <summary>
    /// A function for getting an enumerator for a file, being able to read each
    /// individual line.
    /// </summary>
    /// <param name="fileInfo">The file to read.</param>
    /// <param name="enumerator">The <see cref="IEnumerable{T}"/> of the file's lines.</param>
    /// <returns>Returns if the file was successfully read.</returns>
    public static bool ReadFileText(FileInfo fileInfo, out IEnumerable<string> enumerator)
    {
      if (fileInfo != null)
        return ReadFileTextInternal(fileInfo.FullName, out enumerator);

      enumerator = null;
      return false;
    }

    /// <summary>
    /// An internal function for appending an array of <see cref="byte"/>s to a file.
    /// </summary>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="bytes">The array of <see cref="byte"/>s to append.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    private static bool AppendFileBytesInternal(string filepath, byte[] bytes)
    {
      try
      {
        // Open up a FileStream and attempt to append the array.
        using FileStream fStream = new FileStream(filepath, FileMode.Append, FileAccess.Write);
        fStream.Write(bytes, 0, bytes.Length);
        return true;
      }
      catch
      {
        
      }

      // If the stream couldn't be written to, return false.
      return false;
    }

    /// <summary>
    /// An internal function for asynchronously appending an array of <see cref="byte"/>s to a file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="bytes">The array of <see cref="byte"/>s to append.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    private static async Task<bool> AppendFileBytesAsyncInternal(CancellationToken token,
                                                                 string filepath, byte[] bytes)
    {
      try
      {
        // Open up a FileStream and attempt to append the array.
        using FileStream fStream = new FileStream(filepath, FileMode.Append, FileAccess.Write);
        await fStream.WriteAsync(bytes, 0, bytes.Length, token);
        return !token.IsCancellationRequested;
      }
      catch
      {

      }

      // If the stream couldn't be written to, return false.
      return false;
    }

    /// <summary>
    /// An internal function for appending an array of <see cref="byte"/>s to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="bytes">The array of <see cref="byte"/>s to append.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    private static bool AppendFileBytesSafeInternal(string filepath, byte[] bytes)
    {
      // If a temporary file cannot be copied to, return false.
      if (!CreateTempFile(out string temp) || !CopyFileInternal(filepath, temp, false))
        return false;

      // If the append is successful, delete the temporary file and return validity.
      if (AppendFileBytesInternal(filepath, bytes))
      {
        DeleteTempFile(temp);
        return true;
      }

      // In the event of an error, copy back over the temporary copy and clean up.
      CopyFileInternal(temp, filepath, true);
      DeleteTempFile(temp);
      return false;
    }

    /// <summary>
    /// An internal function for asynchronously appending an array of <see cref="byte"/>s to a file.
    /// This function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="bytes">The array of <see cref="byte"/>s to append.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    private static async Task<bool> AppendFileBytesSafeAsyncInternal(CancellationToken token,
                                                                     string filepath, byte[] bytes)
    {
      // If a temporary file cannot be copied to, return false.
      if (!CreateTempFile(out string temp) || !CopyFileInternal(filepath, temp, false))
        return false;

      // If the append is successful, delete the temporary file and return validity.
      if (await AppendFileBytesAsyncInternal(token, filepath, bytes))
      {
        DeleteTempFile(temp);
        return true;
      }

      // In the event of an error, copy back over the temporary copy and clean up.
      await CopyFileAsyncInternal(new CancellationTokenSource().Token, temp, filepath, true);
      DeleteTempFile(temp);
      return false;
    }

    /// <summary>
    /// An internal function for appending a serializable object to a file.
    /// </summary>
    /// <typeparam name="T">The type of the object. This must be serializable!</typeparam>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="obj">The object to serialize and append.</param>
    /// <returns>Returns if the file was sucessfully appended to.</returns>
    private static bool AppendFileBytesInternal<T>(string filepath, T obj)
    {
      if (Conversion.SerializeFromObject<T>(obj, out byte[] bytes))
        return false;

      return AppendFileBytesInternal(filepath, bytes);
    }

    /// <summary>
    /// An internal function for asynchronously appending a serializable object to a file.
    /// </summary>
    /// <typeparam name="T">The type of the object. This must be serializable!</typeparam>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="obj">The object to serialize and append.</param>
    /// <returns>Returns if the file was sucessfully appended to.</returns>
    private static async Task<bool> AppendFileBytesAsyncInternal<T>(CancellationToken token,
                                                                    string filepath, T obj)
    {
      if (Conversion.SerializeFromObject(obj, out byte[] bytes))
        return false;

      return await AppendFileBytesAsyncInternal(token, filepath, bytes);
    }

    /// <summary>
    /// An internal function for appending a serializable object to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <typeparam name="T">The type of the object. This must be serializable!</typeparam>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="obj">The object to serialize and append.</param>
    /// <returns>Returns if the file was sucessfully appended to.</returns>
    private static bool AppendFileBytesSafeInternal<T>(string filepath, T obj)
    {
      if (Conversion.SerializeFromObject<T>(obj, out byte[] bytes))
        return false;

      return AppendFileBytesSafeInternal(filepath, bytes);
    }

    /// <summary>
    /// An internal function for asynchronously appending a serializable object to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <typeparam name="T">The type of the object. This must be serializable!</typeparam>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="obj">The object to serialize and append.</param>
    /// <returns>Returns if the file was sucessfully appended to.</returns>
    private static async Task<bool> AppendFileBytesSafeAsyncInternal<T>(CancellationToken token,
                                                                        string filepath, T obj)
    {
      if (Conversion.SerializeFromObject(obj, out byte[] bytes))
        return false;

      return await AppendFileBytesSafeAsyncInternal(token, filepath, bytes);
    }

    /// <summary>
    /// An internal function for appending a <see cref="string"/> to a file.
    /// </summary>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="message">The <see cref="string"/> to write.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    private static bool AppendFileStringInternal(string filepath, string message, bool newline,
                                                 Encoding encoding)
    {
      try
      {
        // Open up a FileStream and attempt to append the array.
        using StreamWriter sWriter = new StreamWriter(filepath, true, encoding);

        // Write with a newline or no newline.
        if (newline)
          sWriter.WriteLine(message);
        else
          sWriter.Write(message);

        return true; // The file was successfully appended to.
      }
      catch
      {

      }

      // If the stream couldn't be written to, return false.
      return false;
    }

    /// <summary>
    /// An internal function for asynchronously appending a <see cref="string"/> to a file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch. Currently, this
    /// function does not properly support cancellation due to API limitations.</param>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="message">The <see cref="string"/> to write.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    private static async Task<bool> AppendFileStringAsyncInternal(CancellationToken token,
                                                                  string filepath, string message,
                                                                  bool newline, Encoding encoding)
    {
      if (token.IsCancellationRequested)
        return false;

      try
      {
        // Open up a FileStream and attempt to append the array.
        using StreamWriter sWriter = new StreamWriter(filepath, true, encoding);

        // Write with a newline or no newline.
        if (newline)
          await sWriter.WriteLineAsync(message);
        else
          await sWriter.WriteAsync(message);

        return true; // The file was successfully appended to.
      }
      catch
      {

      }

      // If the stream couldn't be written to, return false.
      return false;
    }

    /// <summary>
    /// An internal function for appending a <see cref="string"/> to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="message">The <see cref="string"/> to write.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    private static bool AppendFileStringSafeInternal(string filepath, string message, bool newline,
                                                     Encoding encoding)
    {
      // Attempt to make a temporary file. If not possible, return false immediately.
      if (!CreateTempFile(out string temp) || !CopyFileInternal(filepath, temp, false))
        return false;

      // If the append is successful, delete the temporary file and return validity.
      if (AppendFileStringInternal(filepath, message, newline, encoding))
      {
        DeleteTempFile(temp);
        return true;
      }


      // In the event of an error, copy back over the temporary copy and clean up.
      CopyFileInternal(temp, filepath, true);
      DeleteTempFile(temp);
      return false;
    }

    /// <summary>
    /// An internal function for asynchronously appending a <see cref="string"/> to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="message">The <see cref="string"/> to write.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    private static async Task<bool> AppendFileStringSafeAsyncInternal(CancellationToken token,
                                                                      string filepath,
                                                                      string message, bool newline,
                                                                      Encoding encoding)
    {
      // Attempt to make a temporary file. If not possible, return false immediately.
      if (!CreateTempFile(out string temp) || !CopyFileInternal(filepath, temp, false))
        return false;

      // If the append is successful, delete the temporary file and return validity.
      if (await AppendFileStringAsyncInternal(token, filepath, message, newline, encoding))
      {
        DeleteTempFile(temp);
        return true;
      }

      // In the event of an error, copy back over the temporary copy and clean up.
      await CopyFileAsyncInternal(new CancellationTokenSource().Token, temp, filepath, true);
      DeleteTempFile(temp);
      return false;
    }

    /// <summary>
    /// An internal function for appending several <see cref="string"/>s to a file.
    /// </summary>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="messages">The <see cref="string"/>s to write.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    private static bool AppendFileStringsInternal(string filepath, IEnumerable<string> messages,
                                                  bool newline, Encoding encoding)
    {
      try
      {
        // Open up a FileStream and attempt to append the array.
        using StreamWriter sWriter = new StreamWriter(filepath, true, encoding);

        // Write each message with a newline or no newline.
        if (newline)
        {
          foreach (string message in messages)
            sWriter.WriteLine(message);
        }
        else
        {
          foreach (string message in messages)
            sWriter.Write(message);
        }
        return true;
      }
      catch
      {

      }

      // If the stream couldn't be written to, return false.
      return false;
    }

    /// <summary>
    /// An internal function for asynchronously appending several <see cref="string"/>s to a file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="messages">The <see cref="string"/>s to write.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    private static async Task<bool> AppendFileStringsAsyncInternal(CancellationToken token,
                                                                   string filepath,
                                                                   IEnumerable<string> messages,
                                                                   bool newline, Encoding encoding)
    {
      try
      {
        // Open up a FileStream and attempt to append the array.
        using StreamWriter sWriter = new StreamWriter(filepath, true, encoding);

        // Write each message with a newline or no newline.
        if (newline)
        {
          foreach (string message in messages)
          {
            if (token.IsCancellationRequested)
              return false;

            await sWriter.WriteLineAsync(message);
          }
            
        }
        else
        {
          foreach (string message in messages)
          {
            if (token.IsCancellationRequested)
              return false;

            await sWriter.WriteAsync(message);
          }
        }

        return true;
      }
      catch
      {

      }

      // If the stream couldn't be written to, return false.
      return false;
    }

    /// <summary>
    /// An internal function for appending several <see cref="string"/>s to a file. This
    /// function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="messages">The <see cref="string"/>s to write.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    private static bool AppendFileStringsSafeInternal(string filepath, IEnumerable<string> messages,
                                                      bool newline, Encoding encoding)
    {
      // If a temporary file cannot be copied to, return false.
      if (!CreateTempFile(out string temp) || !CopyFileInternal(filepath, temp, false))
        return false;

      // If the append is successful, delete the temporary file and return validity.
      if (AppendFileStringsInternal(filepath, messages, newline, encoding))
      {
        DeleteTempFile(temp);
        return true;
      }

      // In the event of an error, copy back over the temporary copy and clean up.
      CopyFileInternal(temp, filepath, true);
      DeleteTempFile(temp);
      return false;
    }

    /// <summary>
    /// An internal function for asynchronously appending several <see cref="string"/>s to a file.
    /// This function creates a temporary copy in case something goes wrong.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to append to.</param>
    /// <param name="messages">The <see cref="string"/>s to write.</param>
    /// <param name="newline">A toggle for adding a new line after the message.</param>
    /// <param name="encoding">The <see cref="Encoding"/> type of the file.</param>
    /// <returns>Returns if the file was successfully appended to.</returns>
    private static async Task<bool> AppendFileStringsSafeAsyncInternal(CancellationToken token,
                                                                       string filepath,
                                                                       IEnumerable<string> messages,
                                                                       bool newline,
                                                                       Encoding encoding)
    {
      // If a temporary file cannot be copied to, return false.
      if (!CreateTempFile(out string temp) || !CopyFileInternal(filepath, temp, false))
        return false;

      // If the append is successful, delete the temporary file and return validity.
      if (await AppendFileStringsAsyncInternal(token, filepath, messages, newline, encoding))
      {
        DeleteTempFile(temp);
        return true;
      }

      // In the event of an error, copy back over the temporary copy and clean up.
      await CopyFileAsyncInternal(new CancellationTokenSource().Token, temp, filepath, true);
      DeleteTempFile(temp);
      return false;
    }

    /// <summary>
    /// A helper function for building the <see cref="SanitizerUniversal"/>.
    /// </summary>
    private static void BuildSanitizerUniversal()
    {
      // Create a new sanitizer, along with declaring all standard properties.
      SanitizerUniversal = new PathSanitizer(SeparatorUniversal)
      {
        fullyQualify = false,
        rootMode = PathSanitizer.RootMode.AllowAllRoots,
        replacementMode = PathSanitizer.ReplacementMode.QuickOnly,
        removeRedundantSeparators = true,
        forceRootSeparator = false,
        autoRebuildQuickReplacements = false
      };

      // Add any unique separators that are not the universal one.
      SanitizerUniversal.possibleSeparators.Add(SeparatorWindows);

      // Add the universally banned symbols.
      SanitizerUniversal.AddQuickReplacements(@"\\", @"\/", @"\""", @"\|", @"\*", "<", ">", @"\?", ":");

      // Add the universally banned names, in Regex form.
      SanitizerUniversal.AddQuickReplacements("PRN", "AUX", "CLOCK$", "NUL", "CON", @"COM\d", @"LPT\d");

      // Add the banned ASCII control characters, and possibly breaking Yen sign.
      SanitizerUniversal.AddQuickReplacements(@"[\x00-\x1F]", @"\xA5");

      // Build the quick replacement Regex.
      SanitizerUniversal.BuildQuickReplacementRegex();
    }

    /// <summary>
    /// A helper function for building the <see cref="SanitizerUNIX"/>.
    /// </summary>
    private static void BuildSanitizerUNIX()
    {
      // Create a new sanitizer, along with declaring all standard properties.
      SanitizerUNIX = new PathSanitizer(SeparatorUniversal)
      {
        fullyQualify = false,
        rootMode = PathSanitizer.RootMode.SeparatorOnly,
        replacementMode = PathSanitizer.ReplacementMode.QuickOnly,
        removeRedundantSeparators = true,
        forceRootSeparator = false,
        autoRebuildQuickReplacements = false
      };

      // Add any unique separators that are not the universal one.
      SanitizerUNIX.possibleSeparators.Add(SeparatorWindows);

      // Add the banned ASCII NUL character, and the slashes.
      SanitizerUNIX.AddQuickReplacements(@"\x00-\x1F", @"\/", @"\\");

      // Build the quick replacement Regex.
      SanitizerUNIX.BuildQuickReplacementRegex();
    }

    /// <summary>
    /// A helper function for deleting several subdirectories that may have been created when a
    /// file failed to be created as well.
    /// </summary>
    /// <param name="directory">The full directory path.</param>
    /// <param name="creationUTC">The UTC time that the directory was made.</param>
    private static void CleanupBadDirectories(string directory, DateTime creationUTC)
    {
      // For safety, replace all separators to the universal one.
      directory.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

      // If the last char is a separator, remove it to prevent issues with the function.
      if (directory.IsLastChar(Path.AltDirectorySeparatorChar))
        directory = directory.Remove(directory.LastIndexOf(Path.AltDirectorySeparatorChar));

      try
      {
        // Perform until a break happens.
        while (true)
        {
          DirectoryInfo info = new DirectoryInfo(directory); // Get the directory.
          UnityEngine.Debug.Log(directory);
          // If the directory was made after or during the creation time, delete it.
          if (info.CreationTimeUtc >= creationUTC)
          {
            info.Delete();
            // Trim off the last separator, and the directory before it.
            directory = directory.Remove(directory.LastIndexOf(Path.AltDirectorySeparatorChar));
          }
          else
            break;
        }
      }
      catch
      {
        // Catches would happen once reaching something that can't be deleted.
      }
    }

    /// <summary>
    /// An internal function for copying a file to a new location.
    /// </summary>
    /// <param name="filepath">The original file to copy.</param>
    /// <param name="destination">The destination path to copy to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully copied.</returns>
    private static bool CopyFileInternal(string filepath, string destination, bool overwrite)
    {
      // Attempt to copy the file over, using a try-catch for safety.
      try
      {
        // Copy already throws errors if something doesn't exist or overwriting can't happen.
        File.Copy(filepath, destination, overwrite);
        return true;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    /// An internal function asynchronously for copying a file to a new location.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The original file to copy.</param>
    /// <param name="destination">The destination path to copy to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully copied.</returns>
    private static async Task<bool> CopyFileAsyncInternal(CancellationToken token, string filepath,
                                                          string destination, bool overwrite)
    {
      // Attempt to copy the file over, using a try-catch for safety.
      try
      {
        // If the file does not exist, create it. If it does, make sure overwriting is allowed.
        if (!File.Exists(destination))
          File.Create(destination);
        else if (!overwrite)
          return false;


        // Open up two streams between the files, and copy the file. CopyAsync overwrites.
        using FileStream fRead = new FileStream(filepath, FileMode.Open, FileAccess.Read);
        using FileStream fWrite = new FileStream(destination, FileMode.Open, FileAccess.Write);
        await fRead.CopyToAsync(fWrite, DefaultCopyBuffer, token);
        return !token.IsCancellationRequested; // Return based on the copy's success.
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    /// An internal function for creating a directory safely.
    /// </summary>
    /// <param name="directory">The path of the directory to create.</param>
    /// <returns>Returns if the directory was successfully created.</returns>
    private static bool CreateDirectoryInternal(string directory)
    {
      try
      {
        // Attempt to create the directory.
        return Directory.CreateDirectory(directory).Exists;
      }
      catch
      {
        // In the case of any error, simply return false.
        return false;
      }
    }

    /// <summary>
    /// An internal function for creating a file safely. This internal function assumes that the
    /// filepath is valid, and the file does not already exist/can be overwritten.
    /// </summary>
    /// <param name="filepath">The filepath for the file.</param>
    /// <param name="makeDirectory">A toggle for allowing directories to be made if they
    /// do not already exist.</param>
    /// <returns>Returns if the file was successfully created.</returns>
    private static bool CreateFileInternal(string filepath, bool makeDirectory)
    {
      try
      {
        filepath = Path.GetFullPath(filepath);
      }
      catch
      {
        return false;
      }

      bool directoryWasMade = false; // Hold a check on if a directory had to be made.
      DateTime creationTime = DateTime.UtcNow; // Hold the creation time for directories.
      string directory = Path.GetDirectoryName(filepath); // Parse out the directory.
      
      // Handle situations where the directory does not already exist.
      if (!Directory.Exists(directory))
      {
        // If allowed to make the directory, attempt to make it.
        if (makeDirectory && CreateDirectoryInternal(directory))
          directoryWasMade = true;
        else
          return false; // A directory could not be made.
      }

      try
      {
        // Attempt to create the file, and close the FileStream right afterwards.
        File.Create(filepath).Close();
        return true;
      }
      catch
      {
        // In the event of an error, delete the directory if it was freshly created.
        if (directoryWasMade)
          CleanupBadDirectories(directory, creationTime);

        return false; // The file could not be made.
      }
    }

    /// <summary>
    /// An internal function for deleting a directory.
    /// </summary>
    /// <param name="directory">The directory to delete.</param>
    /// <param name="recursive">A toggle for also deleting any files in the directory and any
    /// empty subdirectories.</param>
    /// <returns>Returns if the deletion was successful.</returns>
    private static bool DeleteDirectoryInternal(string directory, bool recursive) 
    {
      // Attempt to delete the directory. Being in a try block, directories that don't exist
      // would throw an error anyways. Exists already uses a try-catch.
      try
      {
        Directory.Delete(directory, recursive);
        return true;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    /// An internal function for safely deleting some file at a <paramref name="filepath"/>.
    /// </summary>
    /// <param name="filepath">The path to the file to delete.</param>
    /// <returns>Returns if the file was successfully deleted.</returns>
    private static bool DeleteFileInternal(string filepath)
    {
      // Attempt to delete the file. If something goes wrong, just return false.
      try
      {
        File.Delete(filepath);
        return true;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    /// An internal function for moving a file to a new location.
    /// </summary>
    /// <param name="filepath">The original file to move.</param>
    /// <param name="destination">The destination path to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    private static bool MoveFileInternal(string filepath, string destination, bool overwrite)
    {
      // This check is required for older versions of .NET, where Move is not overloaded.
      if (File.Exists(destination) && !overwrite)
        return false;

      // Attempt to move the file over, using a try-catch for safety.
      try
      {
        // Move already throws errors if something doesn't exist.
        File.Move(filepath, destination);
        return true;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    /// An internal function for moving a file to a new location. This makes a copy of the original
    /// file, if overwriting.
    /// </summary>
    /// <param name="filepath">The original file to move.</param>
    /// <param name="destination">The destination path to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    private static bool MoveFileSafeInternal(string filepath, string destination, bool overwrite)
    {
      bool oldExisted = File.Exists(destination); // Check if the file already exists.
      string tempPath = null;

      // Attempt to move the file over, using a try-catch for safety.
      try
      {
        // If the file does not exist, create it.
        if (!oldExisted)
          File.Create(destination);
        else
        {
          // If the file cannot be overwritten, return false. Otherwise, make a backup.
          if (!overwrite || CreateTempFile(out tempPath))
            return false;

          // Copy the file that is being destroyed.
          CopyFileInternal(destination, tempPath, true);
        }

        // Move already throws errors if something doesn't exist.
        File.Move(filepath, destination);
        return true;
      }
      catch
      {
        // If the old file existed, copy it back over.
        if (oldExisted)
        {
          CopyFileInternal(tempPath, destination, true);
          File.Delete(tempPath); // Delete the temporary copy.
        }

        return false;
      }
    }

    /// <summary>
    /// An internal function for asynchronously moving a file to a new location.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The original file to move.</param>
    /// <param name="destination">The destination path to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    private static async Task<bool> MoveFileAsyncInternal(CancellationToken token, string filepath,
                                                          string destination, bool overwrite)
    {
      // Attempt to copy the file over, using a try-catch for safety.
      try
      {
        // If the file does not exist, create it. If it does, make sure overwriting is allowed.
        if (!File.Exists(destination))
          File.Create(destination);
        else if (!overwrite)
          return false;


        // Open up two streams between the files, and copy the file. CopyAsync overwrites.
        using FileStream fRead = new FileStream(filepath, FileMode.Open, FileAccess.Read);
        using FileStream fWrite = new FileStream(destination, FileMode.Open, FileAccess.Write);
        await fRead.CopyToAsync(fWrite, DefaultCopyBuffer, token);

        // If the file was successfully copied, delete the old file.
        if (!token.IsCancellationRequested)
          File.Delete(filepath);


        return !token.IsCancellationRequested; // Return based on the copy's success.
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    /// An internal function for asynchronously moving a file to a new location. This makes a
    /// copy of the original file, if overwriting.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The original file to move.</param>
    /// <param name="destination">The destination path to move to.</param>
    /// <param name="overwrite">A toggle for allowing the <paramref name="destination"/> to be
    /// overwritten, if that file already exists.</param>
    /// <returns>Returns if the file was successfully moved.</returns>
    private static async Task<bool> MoveFileSafeAsyncInternal(CancellationToken token,
                                                              string filepath, string destination,
                                                              bool overwrite)
    {
      bool oldExisted = File.Exists(destination); // Check if the file already exists.
      string tempPath = null;

      // Attempt to copy the file over, using a try-catch for safety.
      try
      {
        // If the file does not exist, create it.
        if (!oldExisted)
          File.Create(destination);
        else
        {
          // If the file cannot be overwritten, return false. Otherwise, make a backup.
          if (!overwrite || CreateTempFile(out tempPath))
            return false;

          // Copy the file that is being destroyed.
          using FileStream fOriginal = new FileStream(destination, FileMode.Open, FileAccess.Read);
          using FileStream fTemp = new FileStream(filepath, FileMode.Open, FileAccess.Write);
          await (fOriginal.CopyToAsync(fTemp, DefaultCopyBuffer, token));

          // If cancelled at any point, delete the temporary file.
          if (token.IsCancellationRequested)
          {
            if (DeleteTempCopies)
              DeleteFileInternal(tempPath);
            return false;
          }
        }

        // Open up two streams between the files, and copy the file. CopyAsync overwrites.
        using FileStream fRead = new FileStream(filepath, FileMode.Open, FileAccess.Read);
        using FileStream fWrite = new FileStream(destination, FileMode.Open, FileAccess.Write);
        await fRead.CopyToAsync(fWrite, DefaultCopyBuffer, token);

        // If the file was successfully copied, delete the old file.
        if (!token.IsCancellationRequested)
        {
          File.Delete(filepath);

          // Delete the temporary copy as well.
          if (oldExisted)
            File.Delete(tempPath);
        }

        return !token.IsCancellationRequested; // Return based on the copy's success.
      }
      catch
      {
        // Move back over the original file if required.
        if (oldExisted)
        {
          // Copy the file that is being destroyed.
          using FileStream fOriginal = new FileStream(destination, FileMode.Open, FileAccess.Write);
          using FileStream fTemp = new FileStream(filepath, FileMode.Open, FileAccess.Read);
          await fTemp.CopyToAsync(fOriginal, DefaultCopyBuffer);

          if (DeleteTempCopies)
            DeleteFileInternal(tempPath);
        }
        else
          File.Delete(destination); // Otherwise, delete the newly created file.

        return false;
      }
    }

    /// <summary>
    /// An internal function for reading the <see cref="byte"/>s of a file.
    /// </summary>
    /// <param name="filepath">The file to read from.</param>
    /// <param name="bytes">The array to store the read <see cref="byte"/>s in.</param>
    /// <param name="offset">The number of <see cref="byte"/>s to skip from the start.</param>
    /// <param name="count">The number of <see cref="byte"/>s to read from the
    /// <paramref name="offset"/>. Values 0 or less indicate reading the whole file.</param>
    /// <returns>Returns if the file was succesfully read.</returns>
    private static bool ReadFileBytesInternal(string filepath, out byte[] bytes, int offset,
                                              int count)
    {
      try
      {
        // Create a reading filestream.
        using (FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
          // If no data can actually be read from the file, return false.
          if (!Maths.InRangeIE(stream.Length - offset, 0, int.MaxValue))
          {
            bytes = null;
            return false;
          }

          // If the count is 0 or less, this signals to read the entire file.
          if (count <= 0)
            count = (int)stream.Length;
          // If the offset and count is too great, the count becomes all readable data.
          if (count + offset >= stream.Length)
            count = (int)stream.Length - offset;

          // Initialize the byte array and read the data.
          bytes = new byte[count];
          stream.Read(bytes, offset, count);
        }

        return true;
      }
      catch
      {
      }

      // In the event of an error, revert the byte array to null and return false.
      bytes = null;
      return false;
    }

    /// <summary>
    /// An internal function for asynchronously reading the <see cref="byte"/>s of a file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to read from.</param>
    /// <param name="offset">The number of <see cref="byte"/>s to skip from the start.</param>
    /// <param name="count">The number of <see cref="byte"/>s to read from the
    /// <paramref name="offset"/>. Values 0 or less indicate reading the whole file.</param>
    /// <returns>Returns a <see cref="TReturn{T0}"/> based on the read's results.</returns>
    private static async Task<TReturn<byte[]>> ReadFileBytesAsyncInternal(CancellationToken token,
                                                                          string filepath,
                                                                          int offset, int count)
    {
      TReturn<byte[]> treturn = default; // Create a return value.

      try
      {
        // Create a reading filestream.
        using (FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
          // If no data can actually be read from the file, return false.
          if (!Maths.InRangeIE(stream.Length - offset, 0, int.MaxValue))
            return treturn;

          // If the count is 0 or less, this signals to read the entire file.
          if (count <= 0)
            count = (int)stream.Length;
          // If the offset and count is too great, the count becomes all readable data.
          if (count + offset >= stream.Length)
            count = (int)stream.Length - offset;

          // Initialize the byte array and read the data.
          treturn.value = new byte[count];
          await stream.ReadAsync(treturn.value, offset, count, token);
        }

        treturn.isValid = true;
        return treturn;
      }
      catch
      {
      }

      // In the event of an error, reset to default settings and return.
      treturn.value = null;
      return treturn;
    }

    /// <summary>
    /// An internal function for reading the <see cref="byte"/>s of a file and converting them
    /// into an object.
    /// </summary>
    /// <typeparam name="T">The type of the <paramref name="obj"/>.</typeparam>
    /// <param name="filepath">The file to read from.</param>
    /// <param name="obj">The final object.</param>
    /// <param name="offset">The number of <see cref="byte"/>s to skip from the start.</param>
    /// <param name="count">The number of <see cref="byte"/>s to read from the
    /// <paramref name="offset"/>. Values 0 or less indicate reading the whole file.</param>
    /// <returns>Returns if the file was succesfully read.</returns>
    private static bool ReadFileBytesInternal<T>(string filepath, out T obj, int offset, int count)
    {
      // Read the bytes. If successful, deserialize the bytes into an object.
      if (ReadFileBytesInternal(filepath, out byte[] bytes, offset, count))
        return Conversion.DeserializeToObject(bytes, out obj);

      obj = default;
      return false;
    }

    /// <summary>
    /// An internal function for asynchronously reading the <see cref="byte"/>s of a file and
    /// converting them into an object.
    /// </summary>
    /// <typeparam name="T">The type of the object the <see cref="byte"/>s represent.</typeparam>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to read from.</param>
    /// <param name="offset">The number of <see cref="byte"/>s to skip from the start.</param>
    /// <param name="count">The number of <see cref="byte"/>s to read from the
    /// <paramref name="offset"/>. Values 0 or less indicate reading the whole file.</param>
    /// <returns>Returns a <see cref="TReturn{T}"/> based on the read's results.</returns>
    private static async Task<TReturn<T>> ReadFileBytesAsyncInternal<T>(CancellationToken token,
                                                                        string filepath,
                                                                        int offset, int count)
    {
      // Attempt to get the bytes read asyncly.
      TReturn<byte[]> breturn = await ReadFileBytesAsyncInternal(token, filepath, offset, count);
      TReturn<T> oreturn = default;

      // If the read was valid, and cancellation has not occured, deserialize the bytes.
      if (breturn.isValid && !token.IsCancellationRequested)
        oreturn.isValid = Conversion.DeserializeToObject(breturn.value, out oreturn.value);

      return oreturn; // Return the results.
    }

    /// <summary>
    /// An internal function for reading a specific line of a file.
    /// </summary>
    /// <param name="filepath">The file to read.</param>
    /// <param name="line">The line that was read.</param>
    /// <param name="lineNumber">The line to read, starting with 0.</param>
    /// <returns>Returns if the file was successfully read.</returns>
    private static bool ReadFileStringInternal(string filepath, out string line, long lineNumber)
    {
      // Check if the file can be read at all.
      if (ReadFileTextInternal(filepath, out IEnumerable<string> enumerator))
      {
        long index = 0; // Initialize an index.

        // Iterate through each line.
        foreach (string l in enumerator)
        {
          // If we've hit the wanted line, return that line. Otherwise, increment the index.
          if (index++ == lineNumber)
          {
            line = l;
            return true;
          }
        }
      }

      // If the file is too short, or couldn't be opened, return an empty line.
      line = string.Empty;
      return false;
    }

    /// <summary>
    /// An internal function for asynchronously reading a specific line of a file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to read.</param>
    /// <param name="lineNumber">The line to read, starting with 0.</param>
    /// <returns>Returns a <see cref="TReturn{T0}"/> based on the read's results.</returns>
    private static async Task<TReturn<string>> ReadFileStringAsyncInternal(CancellationToken token,
                                                                           string filepath,
                                                                           long lineNumber)
    {
      TReturn<string> sreturn = default; // Initialize a default return.

      // Check if the file can be read at all.
      if (ReadFileTextInternal(filepath, out IEnumerable<string> enumerator))
      {
        long index = 0; // Initialize an index.

        // Iterate through each line.
        foreach (string l in enumerator)
        {
          if (token.IsCancellationRequested)
            return sreturn;

          // If we've hit the wanted line, return that line. Otherwise, increment the index.
          if (index++ == lineNumber)
          {
            sreturn.isValid = true;
            sreturn.value = l;
            return sreturn;
          }

          await Task.Yield(); // Yield between each line.
        }
      }

      // If the file is too short, or couldn't be opened, return an empty line.
      return sreturn;
    }

    /// <summary>
    /// An internal function for reading a section of a text file.
    /// </summary>
    /// <param name="filepath">The file to read.</param>
    /// <param name="lines">The outputted <see cref="List{T}"/> of read lines.</param>
    /// <param name="startIndex">The inclusive first index to read from.</param>
    /// <param name="lastIndex">The inclusive last index to read to.</param>
    /// <returns>Returns if the file was successfully read.</returns>
    private static bool ReadFileStringsInternal(string filepath, out List<string> lines,
                                                long startIndex, long lastIndex)
    {
      // Make sure that everything can fit into the List.
      if ((lastIndex - startIndex + 1) <= int.MaxValue)
      {
        // Check if the file can be read at all.
        if (ReadFileTextInternal(filepath, out IEnumerable<string> enumerator))
        {
          long index = 0; // Initialize an index.
          lines = new List<string>((int)(lastIndex - startIndex + 1)); // Create the list.

          // Iterate through each line.
          foreach (string l in enumerator)
          {
            // If the index is within range, add the current line.
            if (Maths.InRangeII(index, startIndex, lastIndex))
              lines.Add(l);
            else if (index > lastIndex) // If past the last index, break out.
              break;

            index++; // Increment the index.
          }

          // If any lines were properly read, return true.
          if (lines.Count > 0)
            return true;
        }
      }

      // Reset the List and return false.
      lines = null;
      return false;
    }

    /// <summary>
    /// An internal function for asynchronously reading a section of a text file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch.</param>
    /// <param name="filepath">The file to read.</param>
    /// <param name="startIndex">The inclusive first index to read from.</param>
    /// <param name="lastIndex">The inclusive last index to read to.</param>
    /// <returns>Returns a <see cref="StringListReturn"/> based on the read's results.</returns>
    private static async Task<TReturn<List<string>>> ReadFileStringsAsyncInternal(CancellationToken token,
                                                                                  string filepath,
                                                                                  long startIndex,
                                                                                  long lastIndex)
    {
      TReturn<List<string>> slreturn = default;

      // Make sure that everything can fit into the List.
      if ((lastIndex - startIndex + 1) <= int.MaxValue)
      {
        // Check if the file can be read at all.
        if (ReadFileTextInternal(filepath, out IEnumerable<string> enumerator))
        {
          long index = 0; // Initialize an index.
          slreturn.value = new List<string>((int)(lastIndex - startIndex + 1)); // Make the list.

          // Iterate through each line.
          foreach (string l in enumerator)
          {
            // If the request is cancelled, return a null array with invalidity.
            if (token.IsCancellationRequested)
            {
              slreturn.value = null;
              return slreturn;
            }

            // If the index is within range, add the current line.
            if (Maths.InRangeII(index, startIndex, lastIndex))
              slreturn.value.Add(l);
            else if (index > lastIndex) // If past the last index, break out.
              break;

            index++; // Increment the index.
            await Task.Yield();
          }

          // If any lines were properly read, return true.
          slreturn.isValid = slreturn.value.Count > 0;
        }
      }

      return slreturn; // Return the results.
    }

    /// <summary>
    /// An internal function for reading all of the text of a file.
    /// </summary>
    /// <param name="filepath">The file to read.</param>
    /// <param name="text">The text of the file.</param>
    /// <returns>Returns if the <paramref name="text"/> was read successfully.</returns>
    private static bool ReadFileTextInternal(string filepath, out string text)
    {
      try
      {
        // Attempt to create a reader. If successful, read teh entire file.
        using StreamReader reader = new StreamReader(filepath);
        text = reader.ReadToEnd();
        return text != null;
      }
      catch
      {
        // In the event of an error, return an empty string.
        text = string.Empty;
        return false;
      }
    }

    /// <summary>
    /// An internal function for asynchronously reading all of the text of a file.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken"/> to watch. Currently, this
    /// function does not properly support cancellation due to API limitations.</param>
    /// <param name="filepath">The file to read.</param>
    /// <returns>Returns a <see cref="StringReturn"/> based on the read's results.</returns>
    private static async Task<TReturn<string>> ReadFileTextAsyncInternal(CancellationToken token,
                                                                         string filepath)
    {
      TReturn<string> sreturn = default; // Create a default return.

      try
      {
        if (!token.IsCancellationRequested)
        {
          // Attempt to create a reader. If successful, read teh entire file.
          using StreamReader reader = new StreamReader(filepath);
          sreturn.value = await reader.ReadToEndAsync();
          sreturn.isValid = sreturn.value != null;
        }
      }
      catch
      {
      }
      
      return sreturn;
    }

    /// <summary>
    /// An internal function for getting an enumerator for a file, being able to read each
    /// individual line.
    /// </summary>
    /// <param name="filepath">The file to read.</param>
    /// <param name="enumerator">The <see cref="IEnumerable{T}"/> of the file's lines.</param>
    /// <returns>Returns if the file was successfully read.</returns>
    private static bool ReadFileTextInternal(string filepath, out IEnumerable<string> enumerator)
    {
      try
      {
        // Attempt to create the enumerable.
        enumerator = File.ReadLines(filepath);
        return true;
      }
      catch
      {
        // Otherwise, return false with a null enumerable.
        enumerator = null;
        return false;
      }
    }

    /// <summary>
    /// A very small helper function for checking if a temporary file should be deleted.
    /// </summary>
    /// <param name="tempFilepath">The path to the temporary file.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void DeleteTempFile(string tempFilepath)
    {
      if (DeleteTempCopies)
        DeleteFileInternal(tempFilepath);
    }
  }
  /************************************************************************************************/
}