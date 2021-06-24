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

using CodeParadox.Tenor.Tools;
using System.Numerics;
using NUnit.Framework;

namespace CodeParadox.Tenor.Tests.Runtime.EditMode
{
  /************************************************************************************************/
  /// <summary>
  /// A test class for <see cref="Tenor.Tools.Interpolation"/>, for checking lerp functions.
  /// </summary>
  [Explicit] // Comment this to allow this to be run automatically.
  public class Test_Lerp
  {
    /// <summary>
    /// A test for <see cref="Interpolation.LerpValue(sbyte, sbyte, float)"/>,
    /// <see cref="Interpolation.LerpValue(sbyte, sbyte, double)"/>,
    /// <see cref="Interpolation.LerpUnclamped(sbyte, sbyte, float)"/>, and
    /// <see cref="Interpolation.LerpUnclamped(sbyte, sbyte, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      sbyte actualResultFC = Interpolation.LerpValue((sbyte)randStart, (sbyte)randEnd, t);
      sbyte actualResultDC = Interpolation.LerpValue((sbyte)randStart, (sbyte)randEnd, (double)t);
      sbyte actualResultFU = Interpolation.LerpUnclamped((sbyte)randStart, (sbyte)randEnd, t);
      sbyte actualResultDU = Interpolation.LerpUnclamped((sbyte)randStart, (sbyte)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual((sbyte)expectedResultC.X, actualResultFC);
      Assert.AreEqual((sbyte)expectedResultC.X, actualResultDC);
      Assert.AreEqual((sbyte)expectedResultU.X, actualResultFU);
      Assert.AreEqual((sbyte)expectedResultU.X, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Interpolation.LerpValue(byte, byte, float)"/>,
    /// <see cref="Interpolation.LerpValue(byte, byte, double)"/>,
    /// <see cref="Interpolation.LerpUnclamped(byte, byte, float)"/>, and
    /// <see cref="Interpolation.LerpUnclamped(byte, byte, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      byte actualResultFC = Interpolation.LerpValue((byte)randStart, (byte)randEnd, t);
      byte actualResultDC = Interpolation.LerpValue((byte)randStart, (byte)randEnd, (double)t);
      byte actualResultFU = Interpolation.LerpUnclamped((byte)randStart, (byte)randEnd, t);
      byte actualResultDU = Interpolation.LerpUnclamped((byte)randStart, (byte)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual((byte)expectedResultC.X, actualResultFC);
      Assert.AreEqual((byte)expectedResultC.X, actualResultDC);
      Assert.AreEqual((byte)expectedResultU.X, actualResultFU);
      Assert.AreEqual((byte)expectedResultU.X, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Interpolation.LerpValue(short, short, float)"/>,
    /// <see cref="Interpolation.LerpValue(short, short, double)"/>,
    /// <see cref="Interpolation.LerpUnclamped(short, short, float)"/>, and
    /// <see cref="Interpolation.LerpUnclamped(short, short, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      short actualResultFC = Interpolation.LerpValue((short)randStart, (short)randEnd, t);
      short actualResultDC = Interpolation.LerpValue((short)randStart, (short)randEnd, (double)t);
      short actualResultFU = Interpolation.LerpUnclamped((short)randStart, (short)randEnd, t);
      short actualResultDU = Interpolation.LerpUnclamped((short)randStart, (short)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual((short)expectedResultC.X, actualResultFC);
      Assert.AreEqual((short)expectedResultC.X, actualResultDC);
      Assert.AreEqual((short)expectedResultU.X, actualResultFU);
      Assert.AreEqual((short)expectedResultU.X, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Interpolation.LerpValue(ushort, ushort, float)"/>,
    /// <see cref="Interpolation.LerpValue(ushort, ushort, double)"/>,
    /// <see cref="Interpolation.LerpUnclamped(ushort, ushort, float)"/>, and
    /// <see cref="Interpolation.LerpUnclamped(ushort, ushort, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      ushort actualResultFC = Interpolation.LerpValue((ushort)randStart, (ushort)randEnd, t);
      ushort actualResultDC = Interpolation.LerpValue((ushort)randStart, (ushort)randEnd, (double)t);
      ushort actualResultFU = Interpolation.LerpUnclamped((ushort)randStart, (ushort)randEnd, t);
      ushort actualResultDU = Interpolation.LerpUnclamped((ushort)randStart, (ushort)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual((ushort)expectedResultC.X, actualResultFC);
      Assert.AreEqual((ushort)expectedResultC.X, actualResultDC);
      Assert.AreEqual((ushort)expectedResultU.X, actualResultFU);
      Assert.AreEqual((ushort)expectedResultU.X, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Interpolation.LerpValue(int, int, float)"/>,
    /// <see cref="Interpolation.LerpValue(int, int, double)"/>,
    /// <see cref="Interpolation.LerpUnclamped(int, int, float)"/>, and
    /// <see cref="Interpolation.LerpUnclamped(int, int, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      int actualResultFC = Interpolation.LerpValue((int)randStart, (int)randEnd, t);
      int actualResultDC = Interpolation.LerpValue((int)randStart, (int)randEnd, (double)t);
      int actualResultFU = Interpolation.LerpUnclamped((int)randStart, (int)randEnd, t);
      int actualResultDU = Interpolation.LerpUnclamped((int)randStart, (int)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual((int)expectedResultC.X, actualResultFC);
      Assert.AreEqual((int)expectedResultC.X, actualResultDC);
      Assert.AreEqual((int)expectedResultU.X, actualResultFU);
      Assert.AreEqual((int)expectedResultU.X, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Interpolation.LerpValue(uint, uint, float)"/>,
    /// <see cref="Interpolation.LerpValue(uint, uint, double)"/>,
    /// <see cref="Interpolation.LerpUnclamped(uint, uint, float)"/>, and
    /// <see cref="Interpolation.LerpUnclamped(uint, uint, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      uint actualResultFC = Interpolation.LerpValue((uint)randStart, (uint)randEnd, t);
      uint actualResultDC = Interpolation.LerpValue((uint)randStart, (uint)randEnd, (double)t);
      uint actualResultFU = Interpolation.LerpUnclamped((uint)randStart, (uint)randEnd, t);
      uint actualResultDU = Interpolation.LerpUnclamped((uint)randStart, (uint)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual((uint)expectedResultC.X, actualResultFC);
      Assert.AreEqual((uint)expectedResultC.X, actualResultDC);
      Assert.AreEqual((uint)expectedResultU.X, actualResultFU);
      Assert.AreEqual((uint)expectedResultU.X, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Interpolation.LerpValue(long, long, float)"/>,
    /// <see cref="Interpolation.LerpValue(long, long, double)"/>,
    /// <see cref="Interpolation.LerpUnclamped(long, long, float)"/>, and
    /// <see cref="Interpolation.LerpUnclamped(long, long, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      long actualResultFC = Interpolation.LerpValue((long)randStart, (long)randEnd, t);
      long actualResultDC = Interpolation.LerpValue((long)randStart, (long)randEnd, (double)t);
      long actualResultFU = Interpolation.LerpUnclamped((long)randStart, (long)randEnd, t);
      long actualResultDU = Interpolation.LerpUnclamped((long)randStart, (long)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual((long)expectedResultC.X, actualResultFC);
      Assert.AreEqual((long)expectedResultC.X, actualResultDC);
      Assert.AreEqual((long)expectedResultU.X, actualResultFU);
      Assert.AreEqual((long)expectedResultU.X, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Interpolation.LerpValue(ulong, ulong, float)"/>,
    /// <see cref="Interpolation.LerpValue(ulong, ulong, double)"/>,
    /// <see cref="Interpolation.LerpUnclamped(ulong, ulong, float)"/>, and
    /// <see cref="Interpolation.LerpUnclamped(ulong, ulong, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      ulong actualResultFC = Interpolation.LerpValue((ulong)randStart, (ulong)randEnd, t);
      ulong actualResultDC = Interpolation.LerpValue((ulong)randStart, (ulong)randEnd, (double)t);
      ulong actualResultFU = Interpolation.LerpUnclamped((ulong)randStart, (ulong)randEnd, t);
      ulong actualResultDU = Interpolation.LerpUnclamped((ulong)randStart, (ulong)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual((ulong)expectedResultC.X, actualResultFC);
      Assert.AreEqual((ulong)expectedResultC.X, actualResultDC);
      Assert.AreEqual((ulong)expectedResultU.X, actualResultFU);
      Assert.AreEqual((ulong)expectedResultU.X, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Interpolation.LerpValue(float, float, float)"/>,
    /// <see cref="Interpolation.LerpValue(float, float, double)"/>,
    /// <see cref="Interpolation.LerpUnclamped(float, float, float)"/>, and
    /// <see cref="Interpolation.LerpUnclamped(float, float, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      float actualResultFC = Interpolation.LerpValue((float)randStart, (float)randEnd, t);
      float actualResultDC = Interpolation.LerpValue((float)randStart, (float)randEnd, (double)t);
      float actualResultFU = Interpolation.LerpUnclamped((float)randStart, (float)randEnd, t);
      float actualResultDU = Interpolation.LerpUnclamped((float)randStart, (float)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC.X, actualResultFC);
      Assert.AreEqual(expectedResultC.X, actualResultDC);
      Assert.AreEqual(expectedResultU.X, actualResultFU);
      Assert.AreEqual(expectedResultU.X, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Interpolation.LerpValue(double, double, float)"/>,
    /// <see cref="Interpolation.LerpValue(double, double, double)"/>,
    /// <see cref="Interpolation.LerpUnclamped(double, double, float)"/>, and
    /// <see cref="Interpolation.LerpUnclamped(double, double, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      double actualResultFC = Interpolation.LerpValue((double)randStart, (double)randEnd, t);
      double actualResultDC = Interpolation.LerpValue((double)randStart, (double)randEnd, (double)t);
      double actualResultFU = Interpolation.LerpUnclamped((double)randStart, (double)randEnd, t);
      double actualResultDU = Interpolation.LerpUnclamped((double)randStart, (double)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC.X, (float)actualResultFC);
      Assert.AreEqual(expectedResultC.X, (float)actualResultDC);
      Assert.AreEqual(expectedResultU.X, (float)actualResultFU);
      Assert.AreEqual(expectedResultU.X, (float)actualResultDU);
      
    }

    /// <summary>
    /// A test for <see cref="Interpolation.LerpValue(decimal, decimal, float)"/>,
    /// <see cref="Interpolation.LerpValue(decimal, decimal, double)"/>,
    /// <see cref="Interpolation.LerpUnclamped(decimal, decimal, float)"/>, and
    /// <see cref="Interpolation.LerpUnclamped(decimal, decimal, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      decimal actualResultFC = Interpolation.LerpValue((decimal)randStart, (decimal)randEnd, t);
      decimal actualResultDC = Interpolation.LerpValue((decimal)randStart, (decimal)randEnd, (double)t);
      decimal actualResultFU = Interpolation.LerpUnclamped((decimal)randStart, (decimal)randEnd, t);
      decimal actualResultDU = Interpolation.LerpUnclamped((decimal)randStart, (decimal)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC.X, (float)actualResultFC, 0.0001);
      Assert.AreEqual(expectedResultC.X, (float)actualResultDC, 0.0001);
      Assert.AreEqual(expectedResultU.X, (float)actualResultFU, 0.0001);
      Assert.AreEqual(expectedResultU.X, (float)actualResultDU, 0.0001);
    }

    /// <summary>
    /// A test for <see cref="Interpolation.LerpValue(BigInteger, BigInteger, float)"/>,
    /// <see cref="Interpolation.LerpValue(BigInteger, BigInteger, double)"/>,
    /// <see cref="Interpolation.LerpUnclamped(BigInteger, BigInteger, float)"/>, and
    /// <see cref="Interpolation.LerpUnclamped(BigInteger, BigInteger, double)"/>.
    /// </summary>
    /// <param name="t">The BigIntegererpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      BigInteger actualResultFC = Interpolation.LerpValue((BigInteger)randStart, (BigInteger)randEnd, t);
      BigInteger actualResultDC = Interpolation.LerpValue((BigInteger)randStart, (BigInteger)randEnd, (double)t);
      BigInteger actualResultFU = Interpolation.LerpUnclamped((BigInteger)randStart, (BigInteger)randEnd, t);
      BigInteger actualResultDU = Interpolation.LerpUnclamped((BigInteger)randStart, (BigInteger)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual((BigInteger)expectedResultC.X, actualResultFC);
      Assert.AreEqual((BigInteger)expectedResultC.X, actualResultDC);
      Assert.AreEqual((BigInteger)expectedResultU.X, actualResultFU);
      Assert.AreEqual((BigInteger)expectedResultU.X, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Interpolation.LerpValue(Complex, Complex, float)"/>,
    /// <see cref="Interpolation.LerpValue(Complex, Complex, double)"/>,
    /// <see cref="Interpolation.LerpUnclamped(Complex, Complex, float)"/>, and
    /// <see cref="Interpolation.LerpUnclamped(Complex, Complex, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      Complex actualResultFC = Interpolation.LerpValue((Complex)randStart, (Complex)randEnd, t);
      Complex actualResultDC = Interpolation.LerpValue((Complex)randStart, (Complex)randEnd, (double)t);
      Complex actualResultFU = Interpolation.LerpUnclamped((Complex)randStart, (Complex)randEnd, t);
      Complex actualResultDU = Interpolation.LerpUnclamped((Complex)randStart, (Complex)randEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC.X, (float)actualResultFC.Real, 0.0001);
      Assert.AreEqual(expectedResultC.X, (float)actualResultDC.Real, 0.0001);
      Assert.AreEqual(expectedResultU.X, (float)actualResultFU.Real, 0.0001);
      Assert.AreEqual(expectedResultU.X, (float)actualResultDU.Real, 0.0001);
    }

    /// <summary>
    /// A test for <see cref="Interpolation.LerpValue(Matrix3x2, Matrix3x2, float)"/>,
    /// <see cref="Interpolation.LerpValue(Matrix3x2, Matrix3x2, double)"/>,
    /// <see cref="Interpolation.LerpUnclamped(Matrix3x2, Matrix3x2, float)"/>, and
    /// <see cref="Interpolation.LerpUnclamped(Matrix3x2, Matrix3x2, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      Matrix3x2 actualResultFC = Interpolation.LerpValue(testStart, testEnd, t);
      Matrix3x2 actualResultDC = Interpolation.LerpValue(testStart, testEnd, (double)t);
      Matrix3x2 actualResultFU = Interpolation.LerpUnclamped(testStart, testEnd, t);
      Matrix3x2 actualResultDU = Interpolation.LerpUnclamped(testStart, testEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC, actualResultFC);
      Assert.AreEqual(expectedResultC, actualResultDC);
      Assert.AreEqual(expectedResultU, actualResultFU);
      Assert.AreEqual(expectedResultU, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Interpolation.LerpValue(Matrix3x2, Matrix3x2, float)"/>,
    /// <see cref="Interpolation.LerpValue(Matrix3x2, Matrix3x2, double)"/>,
    /// <see cref="Interpolation.LerpUnclamped(Matrix3x2, Matrix3x2, float)"/>, and
    /// <see cref="Interpolation.LerpUnclamped(Matrix3x2, Matrix3x2, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      Matrix4x4 actualResultFC = Interpolation.LerpValue(testStart, testEnd, t);
      Matrix4x4 actualResultDC = Interpolation.LerpValue(testStart, testEnd, (double)t);
      Matrix4x4 actualResultFU = Interpolation.LerpUnclamped(testStart, testEnd, t);
      Matrix4x4 actualResultDU = Interpolation.LerpUnclamped(testStart, testEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC, actualResultFC);
      Assert.AreEqual(expectedResultC, actualResultDC);
      Assert.AreEqual(expectedResultU, actualResultFU);
      Assert.AreEqual(expectedResultU, actualResultDU);
    }

    /// <summary>
    /// A test for <see cref="Interpolation.LerpValue(Plane, Plane, float)"/>,
    /// <see cref="Interpolation.LerpValue(Plane, Plane, double)"/>,
    /// <see cref="Interpolation.LerpUnclamped(Plane, Plane, float)"/>, and
    /// <see cref="Interpolation.LerpUnclamped(Plane, Plane, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      Plane actualResultFC = Interpolation.LerpValue(planeStart, planeEnd, t);
      Plane actualResultDC = Interpolation.LerpValue(planeStart, planeEnd, (double)t);
      Plane actualResultFU = Interpolation.LerpUnclamped(planeStart, planeEnd, t);
      Plane actualResultDU = Interpolation.LerpUnclamped(planeStart, planeEnd, (double)t);

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
    /// A test for <see cref="Interpolation.LerpValue(Quaternion, Quaternion, float)"/>,
    /// <see cref="Interpolation.LerpValue(Quaternion, Quaternion, double)"/>,
    /// <see cref="Interpolation.LerpUnclamped(Quaternion, Quaternion, float)"/>, and
    /// <see cref="Interpolation.LerpUnclamped(Quaternion, Quaternion, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      Quaternion actualResultFC = Interpolation.LerpValue(testLerpStart, testLerpEnd, t);
      Quaternion actualResultDC = Interpolation.LerpValue(testLerpStart, testLerpEnd, (double)t);
      Quaternion actualResultFU = Interpolation.LerpUnclamped(testLerpStart, testLerpEnd, t);
      Quaternion actualResultDU = Interpolation.LerpUnclamped(testLerpStart, testLerpEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC, Quaternion.Normalize(actualResultFC));
      Assert.AreEqual(expectedResultC, Quaternion.Normalize(actualResultDC));
      Assert.AreEqual(expectedResultU, Quaternion.Normalize(actualResultFU));
      Assert.AreEqual(expectedResultU, Quaternion.Normalize(actualResultDU));
    }

    /// <summary>
    /// A test for <see cref="Interpolation.LerpValue(Vector2, Vector2, float)"/>,
    /// <see cref="Interpolation.LerpValue(Vector2, Vector2, double)"/>,
    /// <see cref="Interpolation.LerpUnclamped(Vector2, Vector2, float)"/>, and
    /// <see cref="Interpolation.LerpUnclamped(Vector2, Vector2, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      Vector2 actualResultFC = Interpolation.LerpValue(testLerpStart, testLerpEnd, t);
      Vector2 actualResultDC = Interpolation.LerpValue(testLerpStart, testLerpEnd, (double)t);
      Vector2 actualResultFU = Interpolation.LerpUnclamped(testLerpStart, testLerpEnd, t);
      Vector2 actualResultDU = Interpolation.LerpUnclamped(testLerpStart, testLerpEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC.X, actualResultFC.X, 0.0001);
      Assert.AreEqual(expectedResultC.X, actualResultDC.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultFU.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultDU.X, 0.0001);
    }

    /// <summary>
    /// A test for <see cref="Interpolation.LerpValue(Vector3, Vector3, float)"/>,
    /// <see cref="Interpolation.LerpValue(Vector3, Vector3, double)"/>,
    /// <see cref="Interpolation.LerpUnclamped(Vector3, Vector3, float)"/>, and
    /// <see cref="Interpolation.LerpUnclamped(Vector3, Vector3, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      Vector3 actualResultFC = Interpolation.LerpValue(testLerpStart, testLerpEnd, t);
      Vector3 actualResultDC = Interpolation.LerpValue(testLerpStart, testLerpEnd, (double)t);
      Vector3 actualResultFU = Interpolation.LerpUnclamped(testLerpStart, testLerpEnd, t);
      Vector3 actualResultDU = Interpolation.LerpUnclamped(testLerpStart, testLerpEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC.X, actualResultFC.X, 0.0001);
      Assert.AreEqual(expectedResultC.X, actualResultDC.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultFU.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultDU.X, 0.0001);
    }

    /// <summary>
    /// A test for <see cref="Interpolation.LerpValue(Vector4, Vector4, float)"/>,
    /// <see cref="Interpolation.LerpValue(Vector4, Vector4, double)"/>,
    /// <see cref="Interpolation.LerpUnclamped(Vector4, Vector4, float)"/>, and
    /// <see cref="Interpolation.LerpUnclamped(Vector4, Vector4, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      Vector4 actualResultFC = Interpolation.LerpValue(testLerpStart, testLerpEnd, t);
      Vector4 actualResultDC = Interpolation.LerpValue(testLerpStart, testLerpEnd, (double)t);
      Vector4 actualResultFU = Interpolation.LerpUnclamped(testLerpStart, testLerpEnd, t);
      Vector4 actualResultDU = Interpolation.LerpUnclamped(testLerpStart, testLerpEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC.X, actualResultFC.X, 0.0001);
      Assert.AreEqual(expectedResultC.X, actualResultDC.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultFU.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultDU.X, 0.0001);
    }

    /// <summary>
    /// A test for <see cref="Interpolation.NlerpValue(Plane, Plane, float)"/>,
    /// <see cref="Interpolation.NlerpValue(Plane, Plane, double)"/>,
    /// <see cref="Interpolation.NlerpUnclamped(Plane, Plane, float)"/>, and
    /// <see cref="Interpolation.NlerpUnclamped(Plane, Plane, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      Plane actualResultFC = Interpolation.NlerpValue(planeStart, planeEnd, t);
      Plane actualResultDC = Interpolation.NlerpValue(planeStart, planeEnd, (double)t);
      Plane actualResultFU = Interpolation.NlerpUnclamped(planeStart, planeEnd, t);
      Plane actualResultDU = Interpolation.NlerpUnclamped(planeStart, planeEnd, (double)t);

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
    /// A test for <see cref="Interpolation.NlerpValue(Quaternion, Quaternion, float)"/>,
    /// <see cref="Interpolation.NlerpValue(Quaternion, Quaternion, double)"/>,
    /// <see cref="Interpolation.NlerpUnclamped(Quaternion, Quaternion, float)"/>, and
    /// <see cref="Interpolation.NlerpUnclamped(Quaternion, Quaternion, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      Quaternion actualResultFC = Interpolation.NlerpValue(testLerpStart, testLerpEnd, t);
      Quaternion actualResultDC = Interpolation.NlerpValue(testLerpStart, testLerpEnd, (double)t);
      Quaternion actualResultFU = Interpolation.NlerpUnclamped(testLerpStart, testLerpEnd, t);
      Quaternion actualResultDU = Interpolation.NlerpUnclamped(testLerpStart, testLerpEnd, (double)t);

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
    /// A test for <see cref="Interpolation.NlerpValue(Vector2, Vector2, float)"/>,
    /// <see cref="Interpolation.NlerpValue(Vector2, Vector2, double)"/>,
    /// <see cref="Interpolation.NlerpUnclamped(Vector2, Vector2, float)"/>, and
    /// <see cref="Interpolation.NlerpUnclamped(Vector2, Vector2, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      Vector2 actualResultFC = Interpolation.NlerpValue(testLerpStart, testLerpEnd, t);
      Vector2 actualResultDC = Interpolation.NlerpValue(testLerpStart, testLerpEnd, (double)t);
      Vector2 actualResultFU = Interpolation.NlerpUnclamped(testLerpStart, testLerpEnd, t);
      Vector2 actualResultDU = Interpolation.NlerpUnclamped(testLerpStart, testLerpEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC.X, actualResultFC.X, 0.0001);
      Assert.AreEqual(expectedResultC.X, actualResultDC.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultFU.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultDU.X, 0.0001);
    }

    /// <summary>
    /// A test for <see cref="Interpolation.NlerpValue(Vector3, Vector3, float)"/>,
    /// <see cref="Interpolation.NlerpValue(Vector3, Vector3, double)"/>,
    /// <see cref="Interpolation.NlerpUnclamped(Vector3, Vector3, float)"/>, and
    /// <see cref="Interpolation.NlerpUnclamped(Vector3, Vector3, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      Vector3 actualResultFC = Interpolation.NlerpValue(testLerpStart, testLerpEnd, t);
      Vector3 actualResultDC = Interpolation.NlerpValue(testLerpStart, testLerpEnd, (double)t);
      Vector3 actualResultFU = Interpolation.NlerpUnclamped(testLerpStart, testLerpEnd, t);
      Vector3 actualResultDU = Interpolation.NlerpUnclamped(testLerpStart, testLerpEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC.X, actualResultFC.X, 0.0001);
      Assert.AreEqual(expectedResultC.X, actualResultDC.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultFU.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultDU.X, 0.0001);
    }

    /// <summary>
    /// A test for <see cref="Interpolation.NlerpValue(Vector4, Vector4, float)"/>,
    /// <see cref="Interpolation.NlerpValue(Vector4, Vector4, double)"/>,
    /// <see cref="Interpolation.NlerpUnclamped(Vector4, Vector4, float)"/>, and
    /// <see cref="Interpolation.NlerpUnclamped(Vector4, Vector4, double)"/>.
    /// </summary>
    /// <param name="t">The interpolation between two random values.</param>
    [Test(TestOf = typeof(Interpolation))]
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
      Vector4 actualResultFC = Interpolation.NlerpValue(testLerpStart, testLerpEnd, t);
      Vector4 actualResultDC = Interpolation.NlerpValue(testLerpStart, testLerpEnd, (double)t);
      Vector4 actualResultFU = Interpolation.NlerpUnclamped(testLerpStart, testLerpEnd, t);
      Vector4 actualResultDU = Interpolation.NlerpUnclamped(testLerpStart, testLerpEnd, (double)t);

      // Assert that everything equals what is expected.
      Assert.AreEqual(expectedResultC.X, actualResultFC.X, 0.0001);
      Assert.AreEqual(expectedResultC.X, actualResultDC.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultFU.X, 0.0001);
      Assert.AreEqual(expectedResultU.X, actualResultDU.X, 0.0001);
    }
  }
  /************************************************************************************************/
}