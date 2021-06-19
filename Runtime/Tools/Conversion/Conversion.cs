/**************************************************************************************************/
/*!
\file   Conversion.cs
\author Craig Williams
\par    Last Updated
        2021-06-18
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for a class of functions for converting to different types.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CodeParadox.Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// A collection of tools for converting between different <see cref="Type"/>s and
  /// serializations.
  /// </summary>
  public static partial class Conversion
  {
    /// <summary>
    /// An extension function for getting the HEX number of a <see cref="ConsoleColor"/>. This does
    /// not append the '#' symbol.
    /// </summary>
    /// <param name="color">The <see cref="ConsoleColor"/> to convert.</param>
    /// <returns>Returns the string HEX number of the <paramref name="color"/>.</returns>
    public static string ToHEXColor(this ConsoleColor color)
    {
      // Switch on the color's type.
      return color switch
      {
        ConsoleColor.Black => "000000",
        ConsoleColor.DarkBlue => "00008B",
        ConsoleColor.DarkGreen => "006400",
        ConsoleColor.DarkCyan => "008B8B",
        ConsoleColor.DarkRed => "8B0000",
        ConsoleColor.DarkMagenta => "8B008B",
        ConsoleColor.DarkYellow => "D7C32A",
        ConsoleColor.Gray => "808080",
        ConsoleColor.DarkGray => "A9A9A9",
        ConsoleColor.Blue => "0000FF",
        ConsoleColor.Green => "008000",
        ConsoleColor.Cyan => "00FFFF",
        ConsoleColor.Red => "FF0000",
        ConsoleColor.Magenta => "FF00FF",
        ConsoleColor.Yellow => "FFFF00",
        _ => "FFFFFF", // ConsoleColor.White
      };
    }

    /// <summary>
    /// A function for converting an array of serialized <see cref="byte"/>s, usually from a
    /// file, into some object of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of what the <paramref name="bytes"/> represent. This must be
    /// a serializable type.</typeparam>
    /// <param name="bytes">The array to convert.</param>
    /// <param name="obj">The final object. This is the default value if there's an error.</param>
    /// <returns>Returns if the conversion was a success.</returns>
    public static bool DeserializeToObject<T>(byte[] bytes, out T obj)
    {
      // Make sure there are bytes to convert.
      if (bytes.IsNotEmptyOrNull())
      {
        // Write a memory stream and seek back to the very beginning.
        using MemoryStream mStream = new MemoryStream();
        mStream.Write(bytes, 0, bytes.Length);
        mStream.Seek(0, SeekOrigin.Begin);

        // Attempt to deserialize the bytes into an object of type T.
        try
        {
          obj = (T)(new BinaryFormatter()).Deserialize(mStream);
          return true;
        }
        catch
        {
          // In the event of an error, just return false.
        }
      }

      obj = default;
      return false;
    }

    /// <summary>
    /// A function for converting an object to an array of <see cref="byte"/>s.
    /// </summary>
    /// <typeparam name="T">The type of the <paramref name="obj"/>.</typeparam>
    /// <param name="bytes">The array to store the <paramref name="obj"/> into.</param>
    /// <param name="obj">The object to convert. It's type must be serializable!</param>
    /// <returns>Returns if the conversion was a success.</returns>
    public static bool SerializeFromObject<T>(T obj, out byte[] bytes)
    {
      // Make sure the object is serializable.
      if (obj != null && obj.GetType().IsSerializable)
      {
        using MemoryStream mStream = new MemoryStream();

        // Serialize the bytes.
        (new BinaryFormatter()).Serialize(mStream, obj);
        bytes = mStream.ToArray();

        return true;
      }

      // Otherwise, the bytes are null. Return false.
      bytes = null;
      return false;
    }
  }
  /************************************************************************************************/
}