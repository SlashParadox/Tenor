/**************************************************************************************************/
/*!
\file   Test_Lerp.cs
\author Craig Williams
\par    Last Updated
        2021-06-03
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A unit test file for the Lerp tools.

\par Bug List

\par References
*/
/**************************************************************************************************/

using SlashParadox.Tenor.Tools;
using System.Numerics;
using NUnit.Framework;

namespace SlashParadox.Tenor.Tests.Runtime.EditMode
{
  /************************************************************************************************/
  /// <summary>
  /// A test class for <see cref="Tenor.Tools.Lerp"/>, for checking lerp functions.
  /// </summary>
  [Explicit] // Comment this to allow this to be run automatically.
  public class Test_Lerp
  {
    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(sbyte, sbyte, float)"/>,
    /// <see cref="Lerp.LerpValue(sbyte, sbyte, double)"/>,
    /// <see cref="Lerp.LerpUnclamped(sbyte, sbyte, float)"/>, and
    /// <see cref="Lerp.LerpUnclamped(sbyte, sbyte, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Lerp_SByte_ReturnsSuccess([Random(-2.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);

      // Create a Vector3 to test Tenor's lerp against .NET's lerp.
      Vector3 testLerpStart = new Vector3(randStart, 0, 0);
      Vector3 testLerpEnd = new Vector3(randEnd, 0, 0);

      // Get an expected result for both a clamped value and unclamped value.
      Vector3 expectedResultC = Vector3.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1));
      Vector3 expectedResultU = Vector3.Lerp(testLerpStart, testLerpEnd, t);

      // Calculate the actual results for each possible function.
      sbyte actualResultFC = Lerp.LerpValue((sbyte)randStart, (sbyte)randEnd, t);
      sbyte actualResultDC = Lerp.LerpValue((sbyte)randStart, (sbyte)randEnd, (double)t);
      sbyte actualResultFU = Lerp.LerpUnclamped((sbyte)randStart, (sbyte)randEnd, t);
      sbyte actualResultDU = Lerp.LerpUnclamped((sbyte)randStart, (sbyte)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual((sbyte)expectedResultC.X, actualResultFC);
      Assert.AreEqual((sbyte)expectedResultC.X, actualResultDC);
      Assert.AreEqual((sbyte)expectedResultU.X, actualResultFU);
      Assert.AreEqual((sbyte)expectedResultU.X, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(byte, byte, float)"/>,
    /// <see cref="Lerp.LerpValue(byte, byte, double)"/>,
    /// <see cref="Lerp.LerpUnclamped(byte, byte, float)"/>, and
    /// <see cref="Lerp.LerpUnclamped(byte, byte, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Lerp_Byte_ReturnsSuccess([Random(0.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);

      // Create a Vector3 to test Tenor's lerp against .NET's lerp.
      Vector3 testLerpStart = new Vector3(randStart, 0, 0);
      Vector3 testLerpEnd = new Vector3(randEnd, 0, 0);

      // Get an expected result for both a clamped value and unclamped value.
      Vector3 expectedResultC = Vector3.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1));
      Vector3 expectedResultU = Vector3.Lerp(testLerpStart, testLerpEnd, t);

      // Calculate the actual results for each possible function.
      byte actualResultFC = Lerp.LerpValue((byte)randStart, (byte)randEnd, t);
      byte actualResultDC = Lerp.LerpValue((byte)randStart, (byte)randEnd, (double)t);
      byte actualResultFU = Lerp.LerpUnclamped((byte)randStart, (byte)randEnd, t);
      byte actualResultDU = Lerp.LerpUnclamped((byte)randStart, (byte)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual((byte)expectedResultC.X, actualResultFC);
      Assert.AreEqual((byte)expectedResultC.X, actualResultDC);
      Assert.AreEqual((byte)expectedResultU.X, actualResultFU);
      Assert.AreEqual((byte)expectedResultU.X, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(short, short, float)"/>,
    /// <see cref="Lerp.LerpValue(short, short, double)"/>,
    /// <see cref="Lerp.LerpUnclamped(short, short, float)"/>, and
    /// <see cref="Lerp.LerpUnclamped(short, short, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Lerp_Short_ReturnsSuccess([Random(-2.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);

      // Create a Vector3 to test Tenor's lerp against .NET's lerp.
      Vector3 testLerpStart = new Vector3(randStart, 0, 0);
      Vector3 testLerpEnd = new Vector3(randEnd, 0, 0);

      // Get an expected result for both a clamped value and unclamped value.
      Vector3 expectedResultC = Vector3.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1));
      Vector3 expectedResultU = Vector3.Lerp(testLerpStart, testLerpEnd, t);

      // Calculate the actual results for each possible function.
      short actualResultFC = Lerp.LerpValue((short)randStart, (short)randEnd, t);
      short actualResultDC = Lerp.LerpValue((short)randStart, (short)randEnd, (double)t);
      short actualResultFU = Lerp.LerpUnclamped((short)randStart, (short)randEnd, t);
      short actualResultDU = Lerp.LerpUnclamped((short)randStart, (short)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual((short)expectedResultC.X, actualResultFC);
      Assert.AreEqual((short)expectedResultC.X, actualResultDC);
      Assert.AreEqual((short)expectedResultU.X, actualResultFU);
      Assert.AreEqual((short)expectedResultU.X, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(ushort, ushort, float)"/>,
    /// <see cref="Lerp.LerpValue(ushort, ushort, double)"/>,
    /// <see cref="Lerp.LerpUnclamped(ushort, ushort, float)"/>, and
    /// <see cref="Lerp.LerpUnclamped(ushort, ushort, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Lerp_UShort_ReturnsSuccess([Random(0.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);

      // Create a Vector3 to test Tenor's lerp against .NET's lerp.
      Vector3 testLerpStart = new Vector3(randStart, 0, 0);
      Vector3 testLerpEnd = new Vector3(randEnd, 0, 0);

      // Get an expected result for both a clamped value and unclamped value.
      Vector3 expectedResultC = Vector3.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1));
      Vector3 expectedResultU = Vector3.Lerp(testLerpStart, testLerpEnd, t);

      // Calculate the actual results for each possible function.
      ushort actualResultFC = Lerp.LerpValue((ushort)randStart, (ushort)randEnd, t);
      ushort actualResultDC = Lerp.LerpValue((ushort)randStart, (ushort)randEnd, (double)t);
      ushort actualResultFU = Lerp.LerpUnclamped((ushort)randStart, (ushort)randEnd, t);
      ushort actualResultDU = Lerp.LerpUnclamped((ushort)randStart, (ushort)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual((ushort)expectedResultC.X, actualResultFC);
      Assert.AreEqual((ushort)expectedResultC.X, actualResultDC);
      Assert.AreEqual((ushort)expectedResultU.X, actualResultFU);
      Assert.AreEqual((ushort)expectedResultU.X, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(int, int, float)"/>,
    /// <see cref="Lerp.LerpValue(int, int, double)"/>,
    /// <see cref="Lerp.LerpUnclamped(int, int, float)"/>, and
    /// <see cref="Lerp.LerpUnclamped(int, int, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Lerp_Int_ReturnsSuccess([Random(-2.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);

      // Create a Vector3 to test Tenor's lerp against .NET's lerp.
      Vector3 testLerpStart = new Vector3(randStart, 0, 0);
      Vector3 testLerpEnd = new Vector3(randEnd, 0, 0);

      // Get an expected result for both a clamped value and unclamped value.
      Vector3 expectedResultC = Vector3.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1));
      Vector3 expectedResultU = Vector3.Lerp(testLerpStart, testLerpEnd, t);

      // Calculate the actual results for each possible function.
      int actualResultFC = Lerp.LerpValue((int)randStart, (int)randEnd, t);
      int actualResultDC = Lerp.LerpValue((int)randStart, (int)randEnd, (double)t);
      int actualResultFU = Lerp.LerpUnclamped((int)randStart, (int)randEnd, t);
      int actualResultDU = Lerp.LerpUnclamped((int)randStart, (int)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual((int)expectedResultC.X, actualResultFC);
      Assert.AreEqual((int)expectedResultC.X, actualResultDC);
      Assert.AreEqual((int)expectedResultU.X, actualResultFU);
      Assert.AreEqual((int)expectedResultU.X, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(uint, uint, float)"/>,
    /// <see cref="Lerp.LerpValue(uint, uint, double)"/>,
    /// <see cref="Lerp.LerpUnclamped(uint, uint, float)"/>, and
    /// <see cref="Lerp.LerpUnclamped(uint, uint, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Lerp_UInt_ReturnsSuccess([Random(0.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);

      // Create a Vector3 to test Tenor's lerp against .NET's lerp.
      Vector3 testLerpStart = new Vector3(randStart, 0, 0);
      Vector3 testLerpEnd = new Vector3(randEnd, 0, 0);

      // Get an expected result for both a clamped value and unclamped value.
      Vector3 expectedResultC = Vector3.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1));
      Vector3 expectedResultU = Vector3.Lerp(testLerpStart, testLerpEnd, t);

      // Calculate the actual results for each possible function.
      uint actualResultFC = Lerp.LerpValue((uint)randStart, (uint)randEnd, t);
      uint actualResultDC = Lerp.LerpValue((uint)randStart, (uint)randEnd, (double)t);
      uint actualResultFU = Lerp.LerpUnclamped((uint)randStart, (uint)randEnd, t);
      uint actualResultDU = Lerp.LerpUnclamped((uint)randStart, (uint)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual((uint)expectedResultC.X, actualResultFC);
      Assert.AreEqual((uint)expectedResultC.X, actualResultDC);
      Assert.AreEqual((uint)expectedResultU.X, actualResultFU);
      Assert.AreEqual((uint)expectedResultU.X, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(long, long, float)"/>,
    /// <see cref="Lerp.LerpValue(long, long, double)"/>,
    /// <see cref="Lerp.LerpUnclamped(long, long, float)"/>, and
    /// <see cref="Lerp.LerpUnclamped(long, long, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Lerp_Long_ReturnsSuccess([Random(-2.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);

      // Create a Vector3 to test Tenor's lerp against .NET's lerp.
      Vector3 testLerpStart = new Vector3(randStart, 0, 0);
      Vector3 testLerpEnd = new Vector3(randEnd, 0, 0);

      // Get an expected result for both a clamped value and unclamped value.
      Vector3 expectedResultC = Vector3.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1));
      Vector3 expectedResultU = Vector3.Lerp(testLerpStart, testLerpEnd, t);

      // Calculate the actual results for each possible function.
      long actualResultFC = Lerp.LerpValue((long)randStart, (long)randEnd, t);
      long actualResultDC = Lerp.LerpValue((long)randStart, (long)randEnd, (double)t);
      long actualResultFU = Lerp.LerpUnclamped((long)randStart, (long)randEnd, t);
      long actualResultDU = Lerp.LerpUnclamped((long)randStart, (long)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual((long)expectedResultC.X, actualResultFC);
      Assert.AreEqual((long)expectedResultC.X, actualResultDC);
      Assert.AreEqual((long)expectedResultU.X, actualResultFU);
      Assert.AreEqual((long)expectedResultU.X, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(ulong, ulong, float)"/>,
    /// <see cref="Lerp.LerpValue(ulong, ulong, double)"/>,
    /// <see cref="Lerp.LerpUnclamped(ulong, ulong, float)"/>, and
    /// <see cref="Lerp.LerpUnclamped(ulong, ulong, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Lerp_ULong_ReturnsSuccess([Random(0.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);

      // Create a Vector3 to test Tenor's lerp against .NET's lerp.
      Vector3 testLerpStart = new Vector3(randStart, 0, 0);
      Vector3 testLerpEnd = new Vector3(randEnd, 0, 0);

      // Get an expected result for both a clamped value and unclamped value.
      Vector3 expectedResultC = Vector3.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1));
      Vector3 expectedResultU = Vector3.Lerp(testLerpStart, testLerpEnd, t);

      // Calculate the actual results for each possible function.
      ulong actualResultFC = Lerp.LerpValue((ulong)randStart, (ulong)randEnd, t);
      ulong actualResultDC = Lerp.LerpValue((ulong)randStart, (ulong)randEnd, (double)t);
      ulong actualResultFU = Lerp.LerpUnclamped((ulong)randStart, (ulong)randEnd, t);
      ulong actualResultDU = Lerp.LerpUnclamped((ulong)randStart, (ulong)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual((ulong)expectedResultC.X, actualResultFC);
      Assert.AreEqual((ulong)expectedResultC.X, actualResultDC);
      Assert.AreEqual((ulong)expectedResultU.X, actualResultFU);
      Assert.AreEqual((ulong)expectedResultU.X, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(float, float, float)"/>,
    /// <see cref="Lerp.LerpValue(float, float, double)"/>,
    /// <see cref="Lerp.LerpUnclamped(float, float, float)"/>, and
    /// <see cref="Lerp.LerpUnclamped(float, float, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Lerp_Float_ReturnsSuccess([Random(-2.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);

      // Create a Vector3 to test Tenor's lerp against .NET's lerp.
      Vector3 testLerpStart = new Vector3(randStart, 0, 0);
      Vector3 testLerpEnd = new Vector3(randEnd, 0, 0);

      // Get an expected result for both a clamped value and unclamped value.
      Vector3 expectedResultC = Vector3.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1));
      Vector3 expectedResultU = Vector3.Lerp(testLerpStart, testLerpEnd, t);

      // Calculate the actual results for each possible function.
      float actualResultFC = Lerp.LerpValue((float)randStart, (float)randEnd, t);
      float actualResultDC = Lerp.LerpValue((float)randStart, (float)randEnd, (double)t);
      float actualResultFU = Lerp.LerpUnclamped((float)randStart, (float)randEnd, t);
      float actualResultDU = Lerp.LerpUnclamped((float)randStart, (float)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC.X, actualResultFC);
      Assert.AreEqual(expectedResultC.X, actualResultDC);
      Assert.AreEqual(expectedResultU.X, actualResultFU);
      Assert.AreEqual(expectedResultU.X, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(double, double, float)"/>,
    /// <see cref="Lerp.LerpValue(double, double, double)"/>,
    /// <see cref="Lerp.LerpUnclamped(double, double, float)"/>, and
    /// <see cref="Lerp.LerpUnclamped(double, double, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Lerp_Double_ReturnsSuccess([Random(-2.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);

      // Create a Vector3 to test Tenor's lerp against .NET's lerp.
      Vector3 testLerpStart = new Vector3(randStart, 0, 0);
      Vector3 testLerpEnd = new Vector3(randEnd, 0, 0);

      // Get an expected result for both a clamped value and unclamped value.
      Vector3 expectedResultC = Vector3.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1));
      Vector3 expectedResultU = Vector3.Lerp(testLerpStart, testLerpEnd, t);

      // Calculate the actual results for each possible function.
      double actualResultFC = Lerp.LerpValue((double)randStart, (double)randEnd, t);
      double actualResultDC = Lerp.LerpValue((double)randStart, (double)randEnd, (double)t);
      double actualResultFU = Lerp.LerpUnclamped((double)randStart, (double)randEnd, t);
      double actualResultDU = Lerp.LerpUnclamped((double)randStart, (double)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC.X, (float)actualResultFC);
      Assert.AreEqual(expectedResultC.X, (float)actualResultDC);
      Assert.AreEqual(expectedResultU.X, (float)actualResultFU);
      Assert.AreEqual(expectedResultU.X, (float)actualResultDU);
      
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(decimal, decimal, float)"/>,
    /// <see cref="Lerp.LerpValue(decimal, decimal, double)"/>,
    /// <see cref="Lerp.LerpUnclamped(decimal, decimal, float)"/>, and
    /// <see cref="Lerp.LerpUnclamped(decimal, decimal, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Lerp_Decimal_ReturnsSuccess([Random(-2.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);

      // Create a Vector3 to test Tenor's lerp against .NET's lerp.
      Vector3 testLerpStart = new Vector3(randStart, 0, 0);
      Vector3 testLerpEnd = new Vector3(randEnd, 0, 0);

      // Get an expected result for both a clamped value and unclamped value.
      Vector3 expectedResultC = Vector3.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1));
      Vector3 expectedResultU = Vector3.Lerp(testLerpStart, testLerpEnd, t);

      // Calculate the actual results for each possible function.
      decimal actualResultFC = Lerp.LerpValue((decimal)randStart, (decimal)randEnd, t);
      decimal actualResultDC = Lerp.LerpValue((decimal)randStart, (decimal)randEnd, (double)t);
      decimal actualResultFU = Lerp.LerpUnclamped((decimal)randStart, (decimal)randEnd, t);
      decimal actualResultDU = Lerp.LerpUnclamped((decimal)randStart, (decimal)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC.X, (float)actualResultFC, 0.0001);
      Assert.AreEqual(expectedResultC.X, (float)actualResultDC, 0.0001);
      Assert.AreEqual(expectedResultU.X, (float)actualResultFU, 0.0001);
      Assert.AreEqual(expectedResultU.X, (float)actualResultDU, 0.0001);
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(BigInteger, BigInteger, float)"/>,
    /// <see cref="Lerp.LerpValue(BigInteger, BigInteger, double)"/>,
    /// <see cref="Lerp.LerpUnclamped(BigInteger, BigInteger, float)"/>, and
    /// <see cref="Lerp.LerpUnclamped(BigInteger, BigInteger, double)"/>.
    /// </summary>
    /// <param name="t">The BigIntegererpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Lerp_BigInteger_ReturnsSuccess([Random(-2.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);
      t = (int)t;

      // Create a Vector3 to test Tenor's lerp against .NET's lerp.
      Vector3 testLerpStart = new Vector3(randStart, 0, 0);
      Vector3 testLerpEnd = new Vector3(randEnd, 0, 0);

      // Get an expected result for both a clamped value and unclamped value.
      Vector3 expectedResultC = Vector3.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1));
      Vector3 expectedResultU = Vector3.Lerp(testLerpStart, testLerpEnd, t);

      // Calculate the actual results for each possible function.
      BigInteger actualResultFC = Lerp.LerpValue((BigInteger)randStart, (BigInteger)randEnd, t);
      BigInteger actualResultDC = Lerp.LerpValue((BigInteger)randStart, (BigInteger)randEnd, (double)t);
      BigInteger actualResultFU = Lerp.LerpUnclamped((BigInteger)randStart, (BigInteger)randEnd, t);
      BigInteger actualResultDU = Lerp.LerpUnclamped((BigInteger)randStart, (BigInteger)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual((BigInteger)expectedResultC.X, actualResultFC);
      Assert.AreEqual((BigInteger)expectedResultC.X, actualResultDC);
      Assert.AreEqual((BigInteger)expectedResultU.X, actualResultFU);
      Assert.AreEqual((BigInteger)expectedResultU.X, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(Complex, Complex, float)"/>,
    /// <see cref="Lerp.LerpValue(Complex, Complex, double)"/>,
    /// <see cref="Lerp.LerpUnclamped(Complex, Complex, float)"/>, and
    /// <see cref="Lerp.LerpUnclamped(Complex, Complex, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Lerp_Complex_ReturnsSuccess([Random(-2.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);

      // Create a Vector3 to test Tenor's lerp against .NET's lerp.
      Vector3 testLerpStart = new Vector3(randStart, 0, 0);
      Vector3 testLerpEnd = new Vector3(randEnd, 0, 0);

      // Get an expected result for both a clamped value and unclamped value.
      Vector3 expectedResultC = Vector3.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1));
      Vector3 expectedResultU = Vector3.Lerp(testLerpStart, testLerpEnd, t);

      // Calculate the actual results for each possible function.
      Complex actualResultFC = Lerp.LerpValue((Complex)randStart, (Complex)randEnd, t);
      Complex actualResultDC = Lerp.LerpValue((Complex)randStart, (Complex)randEnd, (double)t);
      Complex actualResultFU = Lerp.LerpUnclamped((Complex)randStart, (Complex)randEnd, t);
      Complex actualResultDU = Lerp.LerpUnclamped((Complex)randStart, (Complex)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC.X, (float)actualResultFC.Real, 0.0001);
      Assert.AreEqual(expectedResultC.X, (float)actualResultDC.Real, 0.0001);
      Assert.AreEqual(expectedResultU.X, (float)actualResultFU.Real, 0.0001);
      Assert.AreEqual(expectedResultU.X, (float)actualResultDU.Real, 0.0001);
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(Matrix3x2, Matrix3x2, float)"/>,
    /// <see cref="Lerp.LerpValue(Matrix3x2, Matrix3x2, double)"/>,
    /// <see cref="Lerp.LerpUnclamped(Matrix3x2, Matrix3x2, float)"/>, and
    /// <see cref="Lerp.LerpUnclamped(Matrix3x2, Matrix3x2, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Lerp_Matrix3x2_ReturnsSuccess([Random(-2.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();

      Matrix3x2 testStart = new Matrix3x2();
      Matrix3x2 testEnd;

      testEnd.M11 = random.Next(50, 100);
      testEnd.M12 = random.Next(50, 100);
      testEnd.M21 = random.Next(50, 100);
      testEnd.M22 = random.Next(50, 100);
      testEnd.M31 = random.Next(50, 100);
      testEnd.M32 = random.Next(50, 100);

      // Get an expected result for both a clamped value and unclamped value.
      Matrix3x2 expectedResultC = Matrix3x2.Lerp(testStart, testEnd, Maths.ClampII(t, 0, 1));
      Matrix3x2 expectedResultU = Matrix3x2.Lerp(testStart, testEnd, t);

      // Calculate the actual results for each possible function.
      Matrix3x2 actualResultFC = Lerp.LerpValue(testStart, testEnd, t);
      Matrix3x2 actualResultDC = Lerp.LerpValue(testStart, testEnd, (double)t);
      Matrix3x2 actualResultFU = Lerp.LerpUnclamped(testStart, testEnd, t);
      Matrix3x2 actualResultDU = Lerp.LerpUnclamped(testStart, testEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC, actualResultFC);
      Assert.AreEqual(expectedResultC, actualResultDC);
      Assert.AreEqual(expectedResultU, actualResultFU);
      Assert.AreEqual(expectedResultU, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(Matrix3x2, Matrix3x2, float)"/>,
    /// <see cref="Lerp.LerpValue(Matrix3x2, Matrix3x2, double)"/>,
    /// <see cref="Lerp.LerpUnclamped(Matrix3x2, Matrix3x2, float)"/>, and
    /// <see cref="Lerp.LerpUnclamped(Matrix3x2, Matrix3x2, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Lerp_Matrix4x4_ReturnsSuccess([Random(-2.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();

      Matrix4x4 testStart = new Matrix4x4();
      Matrix4x4 testEnd;

      testEnd.M11 = random.Next(50, 100);
      testEnd.M12 = random.Next(50, 100);
      testEnd.M13 = random.Next(50, 100);
      testEnd.M14 = random.Next(50, 100);
      testEnd.M21 = random.Next(50, 100);
      testEnd.M22 = random.Next(50, 100);
      testEnd.M23 = random.Next(50, 100);
      testEnd.M24 = random.Next(50, 100);
      testEnd.M31 = random.Next(50, 100);
      testEnd.M32 = random.Next(50, 100);
      testEnd.M33 = random.Next(50, 100);
      testEnd.M34 = random.Next(50, 100);
      testEnd.M41 = random.Next(50, 100);
      testEnd.M42 = random.Next(50, 100);
      testEnd.M43 = random.Next(50, 100);
      testEnd.M44 = random.Next(50, 100);

      // Get an expected result for both a clamped value and unclamped value.
      Matrix4x4 expectedResultC = Matrix4x4.Lerp(testStart, testEnd, Maths.ClampII(t, 0, 1));
      Matrix4x4 expectedResultU = Matrix4x4.Lerp(testStart, testEnd, t);

      // Calculate the actual results for each possible function.
      Matrix4x4 actualResultFC = Lerp.LerpValue(testStart, testEnd, t);
      Matrix4x4 actualResultDC = Lerp.LerpValue(testStart, testEnd, (double)t);
      Matrix4x4 actualResultFU = Lerp.LerpUnclamped(testStart, testEnd, t);
      Matrix4x4 actualResultDU = Lerp.LerpUnclamped(testStart, testEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC, actualResultFC);
      Assert.AreEqual(expectedResultC, actualResultDC);
      Assert.AreEqual(expectedResultU, actualResultFU);
      Assert.AreEqual(expectedResultU, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(Plane, Plane, float)"/>,
    /// <see cref="Lerp.LerpValue(Plane, Plane, double)"/>,
    /// <see cref="Lerp.LerpUnclamped(Plane, Plane, float)"/>, and
    /// <see cref="Lerp.LerpUnclamped(Plane, Plane, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Lerp_Plane_ReturnsSuccess([Random(-2.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);

      // Create a Vector3 to test Tenor's lerp against .NET's lerp.
      Vector3 testLerpStart = new Vector3(randStart, 0, 0);
      Vector3 testLerpEnd = new Vector3(randEnd, 0, 0);

      // Get an expected result for both a clamped value and unclamped value.
      Vector3 expectedResultC = Vector3.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1));
      Vector3 expectedResultU = Vector3.Lerp(testLerpStart, testLerpEnd, t);

      Plane planeStart = new Plane(randStart, randStart, randStart, randStart);
      Plane planeEnd = new Plane(randEnd, randEnd, randEnd, randEnd);

      // Calculate the actual results for each possible function.
      Plane actualResultFC = Lerp.LerpValue(planeStart, planeEnd, t);
      Plane actualResultDC = Lerp.LerpValue(planeStart, planeEnd, (double)t);
      Plane actualResultFU = Lerp.LerpUnclamped(planeStart, planeEnd, t);
      Plane actualResultDU = Lerp.LerpUnclamped(planeStart, planeEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC.X, actualResultFC.Normal.X, 0.0001);
      Assert.AreEqual(expectedResultC.X, actualResultDC.Normal.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultFU.Normal.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultDU.Normal.X, 0.0001);

      Assert.AreEqual(expectedResultC.X, actualResultFC.Normal.Y, 0.0001);
      Assert.AreEqual(expectedResultC.X, actualResultDC.Normal.Y, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultFU.Normal.Y, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultDU.Normal.Y, 0.0001);

      Assert.AreEqual(expectedResultC.X, actualResultFC.Normal.Z, 0.0001);
      Assert.AreEqual(expectedResultC.X, actualResultDC.Normal.Z, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultFU.Normal.Z, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultDU.Normal.Z, 0.0001);

      Assert.AreEqual(expectedResultC.X, actualResultFC.D, 0.0001);
      Assert.AreEqual(expectedResultC.X, actualResultDC.D, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultFU.D, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultDU.D, 0.0001);
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(Quaternion, Quaternion, float)"/>,
    /// <see cref="Lerp.LerpValue(Quaternion, Quaternion, double)"/>,
    /// <see cref="Lerp.LerpUnclamped(Quaternion, Quaternion, float)"/>, and
    /// <see cref="Lerp.LerpUnclamped(Quaternion, Quaternion, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Lerp_Quaternion_ReturnsSuccess([Random(-2.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);

      // Create a Vector3 to test Tenor's lerp against .NET's lerp.
      Quaternion testLerpStart = new Quaternion(new Vector3(randStart, 0, 0), 1);
      Quaternion testLerpEnd = new Quaternion(new Vector3(randEnd, 0, 0), 1);

      // Get an expected result for both a clamped value and unclamped value.
      Quaternion expectedResultC = Quaternion.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1));
      Quaternion expectedResultU = Quaternion.Lerp(testLerpStart, testLerpEnd, t);

      // Calculate the actual results for each possible function.
      Quaternion actualResultFC = Lerp.LerpValue(testLerpStart, testLerpEnd, t);
      Quaternion actualResultDC = Lerp.LerpValue(testLerpStart, testLerpEnd, (double)t);
      Quaternion actualResultFU = Lerp.LerpUnclamped(testLerpStart, testLerpEnd, t);
      Quaternion actualResultDU = Lerp.LerpUnclamped(testLerpStart, testLerpEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC, Quaternion.Normalize(actualResultFC));
      Assert.AreEqual(expectedResultC, Quaternion.Normalize(actualResultDC));
      Assert.AreEqual(expectedResultU, Quaternion.Normalize(actualResultFU));
      Assert.AreEqual(expectedResultU, Quaternion.Normalize(actualResultDU));
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(Vector2, Vector2, float)"/>,
    /// <see cref="Lerp.LerpValue(Vector2, Vector2, double)"/>,
    /// <see cref="Lerp.LerpUnclamped(Vector2, Vector2, float)"/>, and
    /// <see cref="Lerp.LerpUnclamped(Vector2, Vector2, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Lerp_Vector2_ReturnsSuccess([Random(-2.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);

      // Create a Vector3 to test Tenor's lerp against .NET's lerp.
      Vector2 testLerpStart = new Vector2(randStart, 0);
      Vector2 testLerpEnd = new Vector2(randEnd, 0);

      // Get an expected result for both a clamped value and unclamped value.
      Vector2 expectedResultC = Vector2.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1));
      Vector2 expectedResultU = Vector2.Lerp(testLerpStart, testLerpEnd, t);

      // Calculate the actual results for each possible function.
      Vector2 actualResultFC = Lerp.LerpValue(testLerpStart, testLerpEnd, t);
      Vector2 actualResultDC = Lerp.LerpValue(testLerpStart, testLerpEnd, (double)t);
      Vector2 actualResultFU = Lerp.LerpUnclamped(testLerpStart, testLerpEnd, t);
      Vector2 actualResultDU = Lerp.LerpUnclamped(testLerpStart, testLerpEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC.X, actualResultFC.X, 0.0001);
      Assert.AreEqual(expectedResultC.X, actualResultDC.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultFU.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultDU.X, 0.0001);
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(Vector3, Vector3, float)"/>,
    /// <see cref="Lerp.LerpValue(Vector3, Vector3, double)"/>,
    /// <see cref="Lerp.LerpUnclamped(Vector3, Vector3, float)"/>, and
    /// <see cref="Lerp.LerpUnclamped(Vector3, Vector3, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Lerp_Vector3_ReturnsSuccess([Random(-2.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);

      // Create a Vector3 to test Tenor's lerp against .NET's lerp.
      Vector3 testLerpStart = new Vector3(randStart, 0, 0);
      Vector3 testLerpEnd = new Vector3(randEnd, 0, 0);

      // Get an expected result for both a clamped value and unclamped value.
      Vector3 expectedResultC = Vector3.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1));
      Vector3 expectedResultU = Vector3.Lerp(testLerpStart, testLerpEnd, t);

      // Calculate the actual results for each possible function.
      Vector3 actualResultFC = Lerp.LerpValue(testLerpStart, testLerpEnd, t);
      Vector3 actualResultDC = Lerp.LerpValue(testLerpStart, testLerpEnd, (double)t);
      Vector3 actualResultFU = Lerp.LerpUnclamped(testLerpStart, testLerpEnd, t);
      Vector3 actualResultDU = Lerp.LerpUnclamped(testLerpStart, testLerpEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC.X, actualResultFC.X, 0.0001);
      Assert.AreEqual(expectedResultC.X, actualResultDC.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultFU.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultDU.X, 0.0001);
    }

    /// <summary>
    /// A test for <see cref="Lerp.LerpValue(Vector4, Vector4, float)"/>,
    /// <see cref="Lerp.LerpValue(Vector4, Vector4, double)"/>,
    /// <see cref="Lerp.LerpUnclamped(Vector4, Vector4, float)"/>, and
    /// <see cref="Lerp.LerpUnclamped(Vector4, Vector4, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Lerp_Vector4_ReturnsSuccess([Random(-2.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);

      // Create a Vector4 to test Tenor's lerp against .NET's lerp.
      Vector4 testLerpStart = new Vector4(randStart, 0, 0, 0);
      Vector4 testLerpEnd = new Vector4(randEnd, 0, 0, 0);

      // Get an expected result for both a clamped value and unclamped value.
      Vector4 expectedResultC = Vector4.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1));
      Vector4 expectedResultU = Vector4.Lerp(testLerpStart, testLerpEnd, t);

      // Calculate the actual results for each possible function.
      Vector4 actualResultFC = Lerp.LerpValue(testLerpStart, testLerpEnd, t);
      Vector4 actualResultDC = Lerp.LerpValue(testLerpStart, testLerpEnd, (double)t);
      Vector4 actualResultFU = Lerp.LerpUnclamped(testLerpStart, testLerpEnd, t);
      Vector4 actualResultDU = Lerp.LerpUnclamped(testLerpStart, testLerpEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC.X, actualResultFC.X, 0.0001);
      Assert.AreEqual(expectedResultC.X, actualResultDC.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultFU.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultDU.X, 0.0001);
    }

    /// <summary>
    /// A test for <see cref="Lerp.NlerpValue(Plane, Plane, float)"/>,
    /// <see cref="Lerp.NlerpValue(Plane, Plane, double)"/>,
    /// <see cref="Lerp.NlerpUnclamped(Plane, Plane, float)"/>, and
    /// <see cref="Lerp.NlerpUnclamped(Plane, Plane, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Nlerp_Plane_ReturnsSuccess([Random(-2.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);

      // Create a Vector3 to test Tenor's lerp against .NET's lerp.
      Vector3 testLerpStart = new Vector3(randStart, randStart / 2, randStart / 3);
      Vector3 testLerpEnd = new Vector3(randEnd, randEnd / 2, randEnd / 3);

      // Get an expected result for both a clamped value and unclamped value.
      Vector3 expectedResultC = Vector3.Normalize(Vector3.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1)));
      Vector3 expectedResultU = Vector3.Normalize(Vector3.Lerp(testLerpStart, testLerpEnd, t));

      Plane planeStart = new Plane(randStart, randStart / 2, randStart / 3, 0);
      Plane planeEnd = new Plane(randEnd, randEnd / 2, randEnd / 3, 0);

      // Calculate the actual results for each possible function.
      Plane actualResultFC = Lerp.NlerpValue(planeStart, planeEnd, t);
      Plane actualResultDC = Lerp.NlerpValue(planeStart, planeEnd, (double)t);
      Plane actualResultFU = Lerp.NlerpUnclamped(planeStart, planeEnd, t);
      Plane actualResultDU = Lerp.NlerpUnclamped(planeStart, planeEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC.X, actualResultFC.Normal.X, 0.0001);
      Assert.AreEqual(expectedResultC.X, actualResultDC.Normal.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultFU.Normal.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultDU.Normal.X, 0.0001);

      Assert.AreEqual(expectedResultC.Y, actualResultFC.Normal.Y, 0.0001);
      Assert.AreEqual(expectedResultC.Y, actualResultDC.Normal.Y, 0.0001);
      Assert.AreEqual(expectedResultU.Y, actualResultFU.Normal.Y, 0.0001);
      Assert.AreEqual(expectedResultU.Y, actualResultDU.Normal.Y, 0.0001);

      Assert.AreEqual(expectedResultC.Z, actualResultFC.Normal.Z, 0.0001);
      Assert.AreEqual(expectedResultC.Z, actualResultDC.Normal.Z, 0.0001);
      Assert.AreEqual(expectedResultU.Z, actualResultFU.Normal.Z, 0.0001);
      Assert.AreEqual(expectedResultU.Z, actualResultDU.Normal.Z, 0.0001);
    }

    /// <summary>
    /// A test for <see cref="Lerp.NlerpValue(Quaternion, Quaternion, float)"/>,
    /// <see cref="Lerp.NlerpValue(Quaternion, Quaternion, double)"/>,
    /// <see cref="Lerp.NlerpUnclamped(Quaternion, Quaternion, float)"/>, and
    /// <see cref="Lerp.NlerpUnclamped(Quaternion, Quaternion, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Nlerp_Quaternion_ReturnsSuccess([Random(-2.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);

      // Create a Vector3 to test Tenor's lerp against .NET's lerp.
      Quaternion testLerpStart = new Quaternion(new Vector3(randStart, 0, 0), 1);
      Quaternion testLerpEnd = new Quaternion(new Vector3(randEnd, 0, 0), 1);

      // Get an expected result for both a clamped value and unclamped value.
      Quaternion expectedResultC = Quaternion.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1));
      Quaternion expectedResultU = Quaternion.Lerp(testLerpStart, testLerpEnd, t);

      // Calculate the actual results for each possible function.
      Quaternion actualResultFC = Lerp.NlerpValue(testLerpStart, testLerpEnd, t);
      Quaternion actualResultDC = Lerp.NlerpValue(testLerpStart, testLerpEnd, (double)t);
      Quaternion actualResultFU = Lerp.NlerpUnclamped(testLerpStart, testLerpEnd, t);
      Quaternion actualResultDU = Lerp.NlerpUnclamped(testLerpStart, testLerpEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(Quaternion.Normalize(expectedResultC).X, actualResultFC.X, 0.0001);
      Assert.AreEqual(Quaternion.Normalize(expectedResultC).X, actualResultDC.X, 0.0001);
      Assert.AreEqual(Quaternion.Normalize(expectedResultU).X, actualResultFU.X, 0.0001);
      Assert.AreEqual(Quaternion.Normalize(expectedResultU).X, actualResultDU.X, 0.0001);

      Assert.AreEqual(Quaternion.Normalize(expectedResultC).Y, actualResultFC.Y, 0.0001);
      Assert.AreEqual(Quaternion.Normalize(expectedResultC).Y, actualResultDC.Y, 0.0001);
      Assert.AreEqual(Quaternion.Normalize(expectedResultU).Y, actualResultFU.Y, 0.0001);
      Assert.AreEqual(Quaternion.Normalize(expectedResultU).Y, actualResultDU.Y, 0.0001);

      Assert.AreEqual(Quaternion.Normalize(expectedResultC).Z, actualResultFC.Z, 0.0001);
      Assert.AreEqual(Quaternion.Normalize(expectedResultC).Z, actualResultDC.Z, 0.0001);
      Assert.AreEqual(Quaternion.Normalize(expectedResultU).Z, actualResultFU.Z, 0.0001);
      Assert.AreEqual(Quaternion.Normalize(expectedResultU).Z, actualResultDU.Z, 0.0001);

      Assert.AreEqual(Quaternion.Normalize(expectedResultC).W, actualResultFC.W, 0.0001);
      Assert.AreEqual(Quaternion.Normalize(expectedResultC).W, actualResultDC.W, 0.0001);
      Assert.AreEqual(Quaternion.Normalize(expectedResultU).W, actualResultFU.W, 0.0001);
      Assert.AreEqual(Quaternion.Normalize(expectedResultU).W, actualResultDU.W, 0.0001);
    }

    /// <summary>
    /// A test for <see cref="Lerp.NlerpValue(Vector2, Vector2, float)"/>,
    /// <see cref="Lerp.NlerpValue(Vector2, Vector2, double)"/>,
    /// <see cref="Lerp.NlerpUnclamped(Vector2, Vector2, float)"/>, and
    /// <see cref="Lerp.NlerpUnclamped(Vector2, Vector2, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Nlerp_Vector2_ReturnsSuccess([Random(-2.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);

      // Create a Vector3 to test Tenor's lerp against .NET's lerp.
      Vector2 testLerpStart = new Vector2(randStart, 0);
      Vector2 testLerpEnd = new Vector2(randEnd, 0);

      // Get an expected result for both a clamped value and unclamped value.
      Vector2 expectedResultC = Vector2.Normalize(Vector2.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1)));
      Vector2 expectedResultU = Vector2.Normalize(Vector2.Lerp(testLerpStart, testLerpEnd, t));

      // Calculate the actual results for each possible function.
      Vector2 actualResultFC = Lerp.NlerpValue(testLerpStart, testLerpEnd, t);
      Vector2 actualResultDC = Lerp.NlerpValue(testLerpStart, testLerpEnd, (double)t);
      Vector2 actualResultFU = Lerp.NlerpUnclamped(testLerpStart, testLerpEnd, t);
      Vector2 actualResultDU = Lerp.NlerpUnclamped(testLerpStart, testLerpEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC.X, actualResultFC.X, 0.0001);
      Assert.AreEqual(expectedResultC.X, actualResultDC.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultFU.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultDU.X, 0.0001);
    }

    /// <summary>
    /// A test for <see cref="Lerp.NlerpValue(Vector3, Vector3, float)"/>,
    /// <see cref="Lerp.NlerpValue(Vector3, Vector3, double)"/>,
    /// <see cref="Lerp.NlerpUnclamped(Vector3, Vector3, float)"/>, and
    /// <see cref="Lerp.NlerpUnclamped(Vector3, Vector3, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Nlerp_Vector3_ReturnsSuccess([Random(-2.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);

      // Create a Vector3 to test Tenor's lerp against .NET's lerp.
      Vector3 testLerpStart = new Vector3(randStart, 0, 0);
      Vector3 testLerpEnd = new Vector3(randEnd, 0, 0);

      // Get an expected result for both a clamped value and unclamped value.
      Vector3 expectedResultC = Vector3.Normalize(Vector3.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1)));
      Vector3 expectedResultU = Vector3.Normalize(Vector3.Lerp(testLerpStart, testLerpEnd, t));

      // Calculate the actual results for each possible function.
      Vector3 actualResultFC = Lerp.NlerpValue(testLerpStart, testLerpEnd, t);
      Vector3 actualResultDC = Lerp.NlerpValue(testLerpStart, testLerpEnd, (double)t);
      Vector3 actualResultFU = Lerp.NlerpUnclamped(testLerpStart, testLerpEnd, t);
      Vector3 actualResultDU = Lerp.NlerpUnclamped(testLerpStart, testLerpEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC.X, actualResultFC.X, 0.0001);
      Assert.AreEqual(expectedResultC.X, actualResultDC.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultFU.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultDU.X, 0.0001);
    }

    /// <summary>
    /// A test for <see cref="Lerp.NlerpValue(Vector4, Vector4, float)"/>,
    /// <see cref="Lerp.NlerpValue(Vector4, Vector4, double)"/>,
    /// <see cref="Lerp.NlerpUnclamped(Vector4, Vector4, float)"/>, and
    /// <see cref="Lerp.NlerpUnclamped(Vector4, Vector4, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Lerp))]
    public void Nlerp_Vector4_ReturnsSuccess([Random(-2.0f, 2.0f, 10)] float t)
    {
      // Generate random values.
      System.Random random = new System.Random();
      float randStart = random.Next(0, 50);
      float randEnd = random.Next(50, 100);

      // Create a Vector4 to test Tenor's lerp against .NET's lerp.
      Vector4 testLerpStart = new Vector4(randStart, 0, 0, 0);
      Vector4 testLerpEnd = new Vector4(randEnd, 0, 0, 0);

      // Get an expected result for both a clamped value and unclamped value.
      Vector4 expectedResultC = Vector4.Normalize(Vector4.Lerp(testLerpStart, testLerpEnd, Maths.ClampII(t, 0, 1)));
      Vector4 expectedResultU = Vector4.Normalize(Vector4.Lerp(testLerpStart, testLerpEnd, t));

      // Calculate the actual results for each possible function.
      Vector4 actualResultFC = Lerp.NlerpValue(testLerpStart, testLerpEnd, t);
      Vector4 actualResultDC = Lerp.NlerpValue(testLerpStart, testLerpEnd, (double)t);
      Vector4 actualResultFU = Lerp.NlerpUnclamped(testLerpStart, testLerpEnd, t);
      Vector4 actualResultDU = Lerp.NlerpUnclamped(testLerpStart, testLerpEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC.X, actualResultFC.X, 0.0001);
      Assert.AreEqual(expectedResultC.X, actualResultDC.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultFU.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultDU.X, 0.0001);
    }
  }
  /************************************************************************************************/
}