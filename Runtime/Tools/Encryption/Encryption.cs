/**************************************************************************************************/
/*!
\file   Encryption.cs
\author Craig Williams
\par    Last Updated
        2021-06-22
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for a class of functions for encrypting and decrypting data.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace CodeParadox.Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// A collection of tools for encrypting and decrypting data. It contains functionality for
  /// XOR and Symmetric Algorithms.
  /// </summary>
  public static partial class Encryption
  {
    /// <summary>The default <see cref="Encoding"/> used for <see cref="string"/>s.</summary>
    private static readonly Encoding DefaultEncoding = Encoding.Unicode;

    /// <summary>
    /// A useful function for creating a completely random <see cref="Aes"/>, with a unique
    /// <see cref="SymmetricAlgorithm.Key"/> and <see cref="SymmetricAlgorithm.IV"/>.
    /// </summary>
    /// <returns>Returns the random <see cref="Aes"/>.</returns>
    /// <remarks>The randomization is done with an <see cref="RNGCryptoServiceProvider"/>
    /// in the <see cref="Randomization"/> <see langword="class"/>.</remarks>
    public static Aes RandomAes()
    {
      Aes aes = Aes.Create();
      Randomization.GetRandomBytes(RandomGenerators.CryptoServiceProvider, aes.Key);
      Randomization.GetRandomBytes(RandomGenerators.CryptoServiceProvider, aes.IV);
      return aes;
    }

    /// <summary>
    /// A function for encrypting or decrypting a some serializable object using an XOR
    /// operation. If the <paramref name="value"/> is encrypted, it is decrypted. If it is
    /// decrypted, it is encrypted.
    /// </summary>
    /// <typeparam name="T">The type of the <paramref name="value"/>.</typeparam>
    /// <param name="value">The object to encrypt/decrypt. This must be serializable!</param>
    /// <param name="key">The key used to encrypt/decrypt the value.</param>
    /// <returns>Returns the final <see cref="byte"/> array.</returns>
    public static byte[] XORCrypt<T>(T value, byte[] key)
    {
      if (Conversion.SerializeFromObject(value, out byte[] bytes))
        return XORCrypt(bytes, key);

      throw new SerializationException("The value is not serializable.");
    }

    /// <summary>
    /// A function for encrypting or decrypting a <see cref="string"/> using an XOR
    /// operation. If the <paramref name="value"/> is encrypted, it is decrypted. If it is
    /// decrypted, it is encrypted. Defaults to <see cref="Encoding.Unicode"/>.
    /// </summary>
    /// <param name="value">The value to encrypt/decrypt.</param>
    /// <param name="key">The key used to encrypt/decrypt the value.</param>
    /// <returns>Returns the final <see cref="byte"/> array.</returns>
    public static byte[] XORCrypt(string value, string key)
    {
      return XORCrypt(value, key, DefaultEncoding);
    }

    /// <summary>
    /// A function for encrypting or decrypting a <see cref="string"/> using an XOR
    /// operation. If the <paramref name="value"/> is encrypted, it is decrypted. If it is
    /// decrypted, it is encrypted.
    /// </summary>
    /// <param name="value">The value to encrypt/decrypt.</param>
    /// <param name="key">The key used to encrypt/decrypt the value.</param>
    /// <param name="encoding">The <see cref="Encoding"/> of the <paramref name="value"/> and
    /// <paramref name="key"/>. The same <see cref="Encoding"/> must be used for both encrypting
    /// and decrypting.</param>
    /// <returns>Returns the final <see cref="byte"/> array.</returns>
    public static byte[] XORCrypt(string value, string key, Encoding encoding)
    {
      // Convert the strings to bytes based on the encoding.
      _ = ToByte64(value, encoding, out byte[] xorValue);
      byte[] xorKey = encoding.GetBytes(key);

      return XORCrypt(xorValue, xorKey);
    }

    /// <summary>
    /// A function for encrypting or decrypting a <see cref="byte"/> array using an XOR
    /// operation. If the <paramref name="value"/> is encrypted, it is decrypted. If it is
    /// decrypted, it is encrypted. Defaults to <see cref="Encoding.Unicode"/>.
    /// </summary>
    /// <param name="value">The value to encrypt/decrypt.</param>
    /// <param name="key">The key used to encrypt/decrypt the value.</param>
    /// <returns>Returns the final <see cref="byte"/> array.</returns>
    public static byte[] XORCrypt(byte[] value, string key)
    {
      return XORCrypt(value, key, DefaultEncoding);
    }

    /// <summary>
    /// A function for encrypting or decrypting a <see cref="byte"/> array using an XOR
    /// operation. If the <paramref name="value"/> is encrypted, it is decrypted. If it is
    /// decrypted, it is encrypted.
    /// </summary>
    /// <param name="value">The value to encrypt/decrypt.</param>
    /// <param name="key">The key used to encrypt/decrypt the value.</param>
    /// <param name="encoding">The <see cref="Encoding"/> of the <paramref name="key"/>.
    /// The same <see cref="Encoding"/> must be used for both encrypting
    /// and decrypting.</param>
    /// <returns>Returns the final <see cref="byte"/> array.</returns>
    public static byte[] XORCrypt(byte[] value, string key, Encoding encoding)
    {
      // Convert the string to bytes based on the encoding.
      byte[] xorKey = encoding.GetBytes(key);

      return XORCrypt(value, xorKey);
    }

    /// <summary>
    /// A function for encrypting or decrypting a <see cref="string"/> using an XOR
    /// operation. If the <paramref name="value"/> is encrypted, it is decrypted. If it is
    /// decrypted, it is encrypted. Defaults to <see cref="Encoding.Unicode"/>.
    /// </summary>
    /// <param name="value">The value to encrypt/decrypt.</param>
    /// <param name="key">The key used to encrypt/decrypt the value.</param>
    /// <returns>Returns the final <see cref="byte"/> array.</returns>
    public static byte[] XORCrypt(string value, byte[] key)
    {
      return XORCrypt(value, key, DefaultEncoding);
    }
    /// <summary>
    /// A function for encrypting or decrypting a <see cref="string"/> using an XOR
    /// operation. If the <paramref name="value"/> is encrypted, it is decrypted. If it is
    /// decrypted, it is encrypted.
    /// </summary>
    /// <param name="value">The value to encrypt/decrypt.</param>
    /// <param name="key">The key used to encrypt/decrypt the value.</param>
    /// <param name="encoding">The <see cref="Encoding"/> of the <paramref name="value"/>.
    /// The same <see cref="Encoding"/> must be used for both encrypting
    /// and decrypting.</param>
    /// <returns>Returns the final <see cref="byte"/> array.</returns>
    public static byte[] XORCrypt(string value, byte[] key, Encoding encoding)
    {
      // Convert the string to bytes based on the encoding.
      _ = ToByte64(value, encoding, out byte[] xorValue);
      return XORCrypt(xorValue, key);
    }

    /// <summary>
    /// A function for encrypting or decrypting a <see cref="byte"/> array using an XOR
    /// operation. If the <paramref name="value"/> is encrypted, it is decrypted. If it is
    /// decrypted, it is encrypted.
    /// </summary>
    /// <param name="value">The value to encrypt/decrypt.</param>
    /// <param name="key">The key used to encrypt/decrypt the value.</param>
    /// <returns>Returns the final <see cref="byte"/> array.</returns>
    public static byte[] XORCrypt(byte[] value, byte[] key)
    {
      // Create a new byte array.
      int valueCount = value.Length;
      int keyCount = key.Length;
      byte[] xorValue = new byte[valueCount];

      // Perform an XOR operation against each of the value's bytes.
      for (int i = 0; i < valueCount; i++)
        xorValue[i] = (byte)(value[i] ^ key[i % keyCount]);

      return xorValue; // Return the final product.
    }

    /// <summary>
    /// A function for encrypting or decrypting a <see cref="string"/> using an XOR
    /// operation. If the <paramref name="value"/> is encrypted, it is decrypted. If it is
    /// decrypted, it is encrypted. Defaults to <see cref="Encoding.Unicode"/>.
    /// </summary>
    /// <param name="value">The value to encrypt/decrypt.</param>
    /// <param name="key">The key used to encrypt/decrypt the value.</param>
    /// <returns>Returns the final <see cref="string"/>.</returns>
    public static string XORCryptString(string value, string key)
    {
      return XORCryptString(value, key, DefaultEncoding); // Re-encode from bytes to a string.
    }

    /// <summary>
    /// A function for encrypting or decrypting a <see cref="byte"/> array using an XOR
    /// operation. If the <paramref name="value"/> is encrypted, it is decrypted. If it is
    /// decrypted, it is encrypted. Defaults to <see cref="Encoding.Unicode"/>.
    /// </summary>
    /// <param name="value">The value to encrypt/decrypt.</param>
    /// <param name="key">The key used to encrypt/decrypt the value.</param>
    /// <returns>Returns the final <see cref="string"/>.</returns>
    public static string XORCryptString(byte[] value, string key)
    {
      return XORCryptString(value, key, DefaultEncoding); // Re-encode from bytes to a string.
    }

    /// <summary>
    /// A function for encrypting or decrypting a <see cref="string"/> using an XOR
    /// operation. If the <paramref name="value"/> is encrypted, it is decrypted. If it is
    /// decrypted, it is encrypted. Defaults to <see cref="Encoding.Unicode"/>.
    /// </summary>
    /// <param name="value">The value to encrypt/decrypt.</param>
    /// <param name="key">The key used to encrypt/decrypt the value.</param>
    /// <returns>Returns the final <see cref="string"/>.</returns>
    public static string XORCryptString(string value, byte[] key)
    {
      return XORCryptString(value, key, DefaultEncoding); // Re-encode from bytes to a string.
    }

    /// <summary>
    /// A function for encrypting or decrypting a <see cref="byte"/> array using an XOR
    /// operation. If the <paramref name="value"/> is encrypted, it is decrypted. If it is
    /// decrypted, it is encrypted. Defaults to <see cref="Encoding.Unicode"/>.
    /// </summary>
    /// <param name="value">The value to encrypt/decrypt.</param>
    /// <param name="key">The key used to encrypt/decrypt the value.</param>
    /// <returns>Returns the final <see cref="string"/>.</returns>
    public static string XORCryptString(byte[] value, byte[] key)
    {
      return XORCryptString(value, key, DefaultEncoding); // Re-encode from bytes to a string.
    }

    /// <summary>
    /// A function for encrypting or decrypting a <see cref="string"/> using an XOR
    /// operation. If the <paramref name="value"/> is encrypted, it is decrypted. If it is
    /// decrypted, it is encrypted.
    /// </summary>
    /// <param name="value">The value to encrypt/decrypt.</param>
    /// <param name="key">The key used to encrypt/decrypt the value.</param>
    /// <param name="encoding">The <see cref="Encoding"/> of the <paramref name="value"/> and
    /// <paramref name="key"/>. The same <see cref="Encoding"/> must be used for both encrypting
    /// and decrypting.</param>
    /// <returns>Returns the final <see cref="string"/>.</returns>
    public static string XORCryptString(string value, string key, Encoding encoding)
    {
      bool base64 = ToByte64(value, encoding, out byte[] valueBytes);
      return XORCryptStringInternal(valueBytes, encoding.GetBytes(key), encoding, base64);
    }

    /// <summary>
    /// A function for encrypting or decrypting a <see cref="byte"/> array using an XOR
    /// operation. If the <paramref name="value"/> is encrypted, it is decrypted. If it is
    /// decrypted, it is encrypted.
    /// </summary>
    /// <param name="value">The value to encrypt/decrypt.</param>
    /// <param name="key">The key used to encrypt/decrypt the value.</param>
    /// <param name="encoding">The <see cref="Encoding"/> of the <paramref name="key"/>.
    /// The same <see cref="Encoding"/> must be used for both encrypting
    /// and decrypting.</param>
    /// <returns>Returns the final <see cref="string"/>.</returns>
    public static string XORCryptString(byte[] value, string key, Encoding encoding)
    {
      return XORCryptStringInternal(value, encoding.GetBytes(key), encoding, Texts.IsBase64(value));
    }

    /// <summary>
    /// A function for encrypting or decrypting a <see cref="string"/> using an XOR
    /// operation. If the <paramref name="value"/> is encrypted, it is decrypted. If it is
    /// decrypted, it is encrypted.
    /// </summary>
    /// <param name="value">The value to encrypt/decrypt.</param>
    /// <param name="key">The key used to encrypt/decrypt the value.</param>
    /// <param name="encoding">The <see cref="Encoding"/> of the <paramref name="value"/>.
    /// The same <see cref="Encoding"/> must be used for both encrypting
    /// and decrypting.</param>
    /// <returns>Returns the final <see cref="string"/>.</returns>
    public static string XORCryptString(string value, byte[] key, Encoding encoding)
    {
      bool base64 = ToByte64(value, encoding, out byte[] valueBytes);
      return XORCryptStringInternal(valueBytes, key, encoding, base64);
    }

    /// <summary>
    /// A function for encrypting or decrypting a <see cref="byte"/> array using an XOR
    /// operation. If the <paramref name="value"/> is encrypted, it is decrypted. If it is
    /// decrypted, it is encrypted.
    /// </summary>
    /// <param name="value">The value to encrypt/decrypt.</param>
    /// <param name="key">The key used to encrypt/decrypt the value.</param>
    /// <param name="encoding">The <see cref="Encoding"/> of the <paramref name="value"/>.
    /// The same <see cref="Encoding"/> must be used for both encrypting
    /// and decrypting.</param>
    /// <returns>Returns the final <see cref="string"/>.</returns>
    public static string XORCryptString(byte[] value, byte[] key, Encoding encoding)
    {
      // Get the crypted bytes, and convert to Base64, back from Base64, and to the Encoding.
      return XORCryptStringInternal(value, key, encoding, Texts.IsBase64(value));
    }
    
    /// <summary>
    /// A function for encrypting via a <see cref="SymmetricAlgorithm"/>, creating a new
    /// <see cref="Aes"/> as the encryption.
    /// </summary>
    /// <param name="data">The data to encrypt.</param>
    /// <param name="sAlg">The random <see cref="SymmetricAlgorithm"/> that was created.</param>
    /// <param name="appendIV">A toggle to append the <see cref="SymmetricAlgorithm.IV"/> to the
    /// beginning of the encrypted <paramref name="data"/>, in an unencrypted state.</param>
    /// <returns>Returns the encrypted <paramref name="data"/>.</returns>
    public static byte[] SymmetricEncrypt(byte[] data, out SymmetricAlgorithm sAlg, bool appendIV)
    {
      sAlg = RandomAes();
      return SymmetricEncrypt(data, sAlg, appendIV);
    }

    /// <summary>
    /// A function for encrypting via a <see cref="SymmetricAlgorithm"/>, such as
    /// <see cref="Aes"/>.
    /// </summary>
    /// <param name="data">The data to encrypt.</param>
    /// <param name="sAlg">The <see cref="SymmetricAlgorithm"/> to use.</param>
    /// <param name="appendIV">A toggle to append the <see cref="SymmetricAlgorithm.IV"/> to the
    /// beginning of the encrypted <paramref name="data"/>, in an unencrypted state.</param>
    /// <returns>Returns the encrypted <paramref name="data"/>.</returns>
    public static byte[] SymmetricEncrypt(byte[] data, SymmetricAlgorithm sAlg, bool appendIV)
    {
      // Create an encrypting transform.
      ICryptoTransform encryptor = sAlg.CreateEncryptor(sAlg.Key, sAlg.IV);

      // Create the streams.
      using MemoryStream mStream = new MemoryStream();
      using CryptoStream cStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Write);

      // If appending the IV, append it early in an unencrypted state.
      if (appendIV)
        mStream.Write(sAlg.IV, 0, sAlg.IV.Length);

      // Write the data to the CryptoStream, encrypting it.
      cStream.Write(data, 0, data.Length);
      cStream.FlushFinalBlock();

      return mStream.ToArray(); // Return the data.
    }

    /// <summary>
    /// A function for encrypting a <see cref="string"/> via a <see cref="SymmetricAlgorithm"/>,
    /// creating a new <see cref="Aes"/> as the encryption. Defaults to
    /// <see cref="DefaultEncoding"/>.
    /// </summary>
    /// <param name="data">The <see cref="string"/> to encrypt.</param>
    /// <param name="appendIV">A toggle to append the <see cref="SymmetricAlgorithm.IV"/> to the
    /// beginning of the encrypted <paramref name="data"/>, in an unencrypted state.</param>
    /// <returns>Returns the encrypted <paramref name="data"/>.</returns>
    /// <param name="sAlg">The random <see cref="SymmetricAlgorithm"/> that was created.</param>
    /// <returns>Returns the encrypted <paramref name="data"/>.</returns>
    public static byte[] SymmetricEncrypt(string data, bool appendIV, out SymmetricAlgorithm sAlg)
    {
      sAlg = RandomAes();
      return SymmetricEncrypt(data, appendIV, sAlg, DefaultEncoding);
    }

    /// <summary>
    /// A function for encrypting a <see cref="string"/> via a <see cref="SymmetricAlgorithm"/>,
    /// creating a new <see cref="Aes"/> as the encryption.
    /// </summary>
    /// <param name="data">The <see cref="string"/> to encrypt.</param>
    /// <param name="appendIV">A toggle to append the <see cref="SymmetricAlgorithm.IV"/> to the
    /// beginning of the encrypted <paramref name="data"/>, in an unencrypted state.</param>
    /// <returns>Returns the encrypted <paramref name="data"/>.</returns>
    /// <param name="sAlg">The random <see cref="SymmetricAlgorithm"/> that was created.</param>
    /// <param name="encoding">The <see cref="Encoding"/> of the <paramref name="data"/>.</param>
    /// <returns>Returns the encrypted <paramref name="data"/>.</returns>
    public static byte[] SymmetricEncrypt(string data, bool appendIV, out SymmetricAlgorithm sAlg,
                                          Encoding encoding)
    {
      sAlg = RandomAes();
      return SymmetricEncrypt(data, appendIV, sAlg, encoding);
    }

    /// <summary>
    /// A function for encrypting a <see cref="string"/> via a <see cref="SymmetricAlgorithm"/>,
    /// such as <see cref="Aes"/>. Defaults to <see cref="DefaultEncoding"/>.
    /// </summary>
    /// <param name="data">The <see cref="string"/> to encrypt.</param>
    /// <param name="appendIV">A toggle to append the <see cref="SymmetricAlgorithm.IV"/> to the
    /// beginning of the encrypted <paramref name="data"/>, in an unencrypted state.</param>
    /// <returns>Returns the encrypted <paramref name="data"/>.</returns>
    /// <param name="sAlg">The <see cref="SymmetricAlgorithm"/> to use.</param>
    /// <returns>Returns the encrypted <paramref name="data"/>.</returns>
    public static byte[] SymmetricEncrypt(string data, bool appendIV, SymmetricAlgorithm sAlg)
    {
      return SymmetricEncrypt(data, appendIV, sAlg, DefaultEncoding);
    }

    /// <summary>
    /// A function for encrypting a <see cref="string"/> via a <see cref="SymmetricAlgorithm"/>,
    /// such as <see cref="Aes"/>.
    /// </summary>
    /// <param name="data">The <see cref="string"/> to encrypt.</param>
    /// <param name="appendIV">A toggle to append the <see cref="SymmetricAlgorithm.IV"/> to the
    /// beginning of the encrypted <paramref name="data"/>, in an unencrypted state.</param>
    /// <returns>Returns the encrypted <paramref name="data"/>.</returns>
    /// <param name="sAlg">The <see cref="SymmetricAlgorithm"/> to use.</param>
    /// <param name="encoding">The <see cref="Encoding"/> of the <paramref name="data"/>.</param>
    /// <returns>Returns the encrypted <paramref name="data"/>.</returns>
    public static byte[] SymmetricEncrypt(string data, bool appendIV, SymmetricAlgorithm sAlg,
                                          Encoding encoding)
    {
      byte[] bytes = encoding.GetBytes(data);
      bytes = Conversion.ToBase64Bytes(bytes);
      return SymmetricEncrypt(bytes, sAlg, appendIV);
    }

    /// <summary>
    /// A function for decrypting via a <see cref="SymmetricAlgorithm"/>, such as
    /// <see cref="Aes"/>.
    /// </summary>
    /// <param name="data">The data to decrypt.</param>
    /// <param name="sAlg">The <see cref="SymmetricAlgorithm"/> to use.</param>
    /// <param name="ivAppended">A check for if the <see cref="SymmetricAlgorithm.IV"/> is
    /// appended to the beginning of the <paramref name="data"/>.</param>
    /// <returns>Returns the decrypted <paramref name="data"/>.</returns>
    public static byte[] SymmetricDecrypt(byte[] data, SymmetricAlgorithm sAlg, bool ivAppended)
    {
      using MemoryStream mStream = new MemoryStream(data); // Initialize a MemoryStream.

      // If the IV is appended to the data, grab it first.
      if (ivAppended)
        mStream.Read(sAlg.IV, 0, sAlg.IV.Length);

      // Create a decrypting transform.
      ICryptoTransform decryptor = sAlg.CreateDecryptor(sAlg.Key, sAlg.IV);

      // Create the CryptoStream.
      using CryptoStream cStream = new CryptoStream(mStream, decryptor, CryptoStreamMode.Read);

      // Read the encrypted data to a new array, while keeping track of the actual data count.
      byte[] decrypted = new byte[data.Length];
      int count = cStream.Read(decrypted, 0, decrypted.Length);

      // Return immediately if there is no padding.
      if (decrypted.Length == count)
        return decrypted;

      // If there is padding, copy out the good data and return it without padding.
      byte[] trueArray = new byte[count];
      Array.Copy(decrypted, 0, trueArray, 0, count);
      return trueArray;
    }

    /// <summary>
    /// A function for decrypting a <see cref="string"/> via a <see cref="SymmetricAlgorithm"/>,
    /// such as <see cref="Aes"/>. Defaults to <see cref="DefaultEncoding"/>.
    /// </summary>
    /// <param name="data">The <see cref="string"/> to decrypt.</param>
    /// <param name="ivAppended">A check for if the <see cref="SymmetricAlgorithm.IV"/> is
    /// appended to the beginning of the <paramref name="data"/>.</param>
    /// <param name="sAlg">The <see cref="SymmetricAlgorithm"/> to use.</param>
    /// <returns>Returns the decrypted <paramref name="data"/>.</returns>
    public static string SymmetricDecrypt(byte[] data, bool ivAppended, SymmetricAlgorithm sAlg)
    {
      return SymmetricDecrypt(data, ivAppended, sAlg, DefaultEncoding);
    }

    /// <summary>
    /// A function for decrypting a <see cref="string"/> via a <see cref="SymmetricAlgorithm"/>,
    /// such as <see cref="Aes"/>.
    /// </summary>
    /// <param name="data">The <see cref="string"/> to decrypt.</param>
    /// <param name="ivAppended">A check for if the <see cref="SymmetricAlgorithm.IV"/> is
    /// appended to the beginning of the <paramref name="data"/>.</param>
    /// <param name="sAlg">The <see cref="SymmetricAlgorithm"/> to use.</param>
    /// <param name="encoding">The <see cref="Encoding"/> of the <paramref name="data"/>.</param>
    /// <returns>Returns the decrypted <paramref name="data"/>.</returns>
    public static string SymmetricDecrypt(byte[] data, bool ivAppended, SymmetricAlgorithm sAlg,
                                          Encoding encoding)
    {
      byte[] bytes = SymmetricDecrypt(data, sAlg, ivAppended);
      return encoding.GetString(bytes);
    }

    /// <summary>
    /// An internal function for encrypting or decrypting a <see cref="byte"/> array using an XOR
    /// operation. If the <paramref name="value"/> is encrypted, it is decrypted. If it is
    /// decrypted, it is encrypted.
    /// </summary>
    /// <param name="value">The value to encrypt/decrypt.</param>
    /// <param name="key">The key used to encrypt/decrypt the value.</param>
    /// <param name="encoding">The <see cref="Encoding"/> of the <paramref name="value"/>.
    /// The same <see cref="Encoding"/> must be used for both encrypting
    /// and decrypting.</param>
    /// <param name="base64">A check on if the <paramref name="value"/> is a Base64
    /// <see cref="string"/>.</param>
    /// <returns>Returns the final <see cref="string"/>.</returns>
    private static string XORCryptStringInternal(byte[] value, byte[] key, Encoding encoding,
                                                 bool base64)
    {
      // Get the bytes. If already Base64, encode the string. Otherwise, make the string Base64.
      byte[] xor = XORCrypt(value, key);
      return base64 ? encoding.GetString(xor) : Convert.ToBase64String(xor);
    }

    /// <summary>
    /// A helper function for checking if a <see cref="string"/> is Base64, and getting its
    /// <see cref="byte"/>s as a Base64.
    /// </summary>
    /// <param name="str">The <see cref="string"/> to check.</param>
    /// <param name="encoding">The <see cref="Encoding"/> of the <paramref name="str"/>.</param>
    /// <param name="base64">The outputted <see cref="byte"/> array.</param>
    /// <returns>Returns if the <paramref name="str"/> is Base64 or not.</returns>
    private static bool ToByte64(string str, Encoding encoding, out byte[] base64)
    {
      // TODO: Update When Unity Improves .NET Capabilities

      try
      {
        base64 = Convert.FromBase64String(str);
        return true;
      }
      catch
      {
        byte[] bytes = encoding.GetBytes(str);
        base64 = Convert.FromBase64String(Convert.ToBase64String(bytes));
        return false;
      }
    }
  }
  /************************************************************************************************/
}