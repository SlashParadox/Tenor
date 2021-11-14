using SlashParadox.Tenor.Tools;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using System;
using System.Text;
using System.Security.Cryptography;

namespace SlashParadox.Tenor.Tests.Runtime.EditMode
{
  public class Test_Encryption
  {
    private static readonly int XORPasses = 100;
    private static readonly Encoding DefaultEncoding = Encoding.Unicode;

    // A Test behaves as an ordinary method
    [Test(TestOf = typeof(Encryption))]
    public void XOR_StringString_ReturnsSuccess([Random(1, 300, 10)] int bufferSize)
    {
      Random ranGen = new Random();
      byte[] buffer = new byte[bufferSize];

      for (int i = 0; i < XORPasses; i++)
      {
        ranGen.NextBytes(buffer);
        string value = DefaultEncoding.GetString(buffer);

        ranGen.NextBytes(buffer);
        string key = DefaultEncoding.GetString(buffer);

        string xor = Encryption.XORCryptString(value, key);

        Assert.AreNotEqual(xor, value);
        string final = Encryption.XORCryptString(xor, key);

        Assert.AreEqual(value, final);
      }
    }

    [Test(TestOf = typeof(Encryption))]
    public void AES_Byte_ReturnsSuccess([Random(1, 50, 10)] int bufferSize)
    {
      Aes aes = Encryption.RandomAes();

      byte[] data = new byte[bufferSize];
      for (byte i = 0; i < bufferSize; i++)
        data[i] = i;

      byte[] encrypted = Encryption.SymmetricEncrypt(data, aes, true);
      Assert.AreNotEqual(data, encrypted);

      byte[] decrypted = Encryption.SymmetricDecrypt(encrypted, aes, true);
      Assert.AreEqual(data.Print(), decrypted.Print());
    }

    [Test(TestOf = typeof(Encryption))]
    public void AES_String_ReturnsSuccess([Random(1, 50, 10)] int bufferSize)
    {
      Aes aes = Encryption.RandomAes();
      Random ranGen = new Random();
      byte[] buffer = new byte[bufferSize];

      ranGen.NextBytes(buffer);
      string value = DefaultEncoding.GetString(buffer);

      byte[] encrypted = Encryption.SymmetricEncrypt(value, true, aes, DefaultEncoding);

      Assert.AreNotEqual(buffer.Print(), encrypted.Print());
      string final = Encryption.SymmetricDecrypt(encrypted, true, aes, DefaultEncoding);

      Assert.AreEqual(value, final);
    }
  }
}
