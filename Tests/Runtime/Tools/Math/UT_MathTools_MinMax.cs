using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Tenor.TestTools;
using Tenor.Tools.Math;
using Tenor.Tools.Text;

public class UT_MathTools_MinMax
{
  private struct MinMaxTest : System.IComparable<MinMaxTest>
  {
    public readonly int testValue;

    public MinMaxTest(int value)
    {
      testValue = value;
    }

    public int CompareTo(MinMaxTest other)
    {
      return testValue - other.testValue;
    }
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the min and max of IComparable values.", TestOf = typeof(Math))]
  public void TestMinMaxIComparable()
  {
    Assert.AreEqual(new MinMaxTest(5), Math.Min(new MinMaxTest(5), new MinMaxTest(10)));
    Assert.AreEqual(new MinMaxTest(5), Math.Min(new MinMaxTest(10), new MinMaxTest(5)));
    Assert.AreEqual(new MinMaxTest(5), Math.Min(new MinMaxTest(6), new MinMaxTest(10), new MinMaxTest(5), new MinMaxTest(25)));
    Assert.AreEqual(new MinMaxTest(5), Math.Min(new List<MinMaxTest> { new MinMaxTest(6), new MinMaxTest(10), new MinMaxTest(5), new MinMaxTest(25) }));

    Assert.AreEqual(new MinMaxTest(10), Math.Max(new MinMaxTest(5), new MinMaxTest(10)));
    Assert.AreEqual(new MinMaxTest(10), Math.Max(new MinMaxTest(10), new MinMaxTest(5)));
    Assert.AreEqual(new MinMaxTest(25), Math.Max(new MinMaxTest(6), new MinMaxTest(10), new MinMaxTest(5), new MinMaxTest(25)));
    Assert.AreEqual(new MinMaxTest(25), Math.Max(new List<MinMaxTest> { new MinMaxTest(6), new MinMaxTest(10), new MinMaxTest(5), new MinMaxTest(25) }));

    Assert.AreEqual(UnicodeCategoryType.Lo, Math.MinNG(UnicodeCategoryType.Lo, UnicodeCategoryType.N));
    Assert.AreEqual(UnicodeCategoryType.Lo, Math.MinNG(UnicodeCategoryType.N, UnicodeCategoryType.Lo));
    Assert.AreEqual(UnicodeCategoryType.Lo, Math.MinNG(UnicodeCategoryType.N, UnicodeCategoryType.N, UnicodeCategoryType.M, UnicodeCategoryType.Lo));


    Assert.AreEqual(UnicodeCategoryType.N, Math.MaxNG(UnicodeCategoryType.Lo, UnicodeCategoryType.N));
    Assert.AreEqual(UnicodeCategoryType.N, Math.MaxNG(UnicodeCategoryType.N, UnicodeCategoryType.Lo));
    Assert.AreEqual(UnicodeCategoryType.Cc, Math.MaxNG(UnicodeCategoryType.M, UnicodeCategoryType.N, UnicodeCategoryType.Lo, UnicodeCategoryType.Cc));
  }
  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the min and max of sbyte values.", TestOf = typeof(Math))]
  public void TestMinMaxSByte()
  {
    Assert.AreEqual((sbyte)5, Math.Min((sbyte)5, (sbyte)10));
    Assert.AreEqual((sbyte)5, Math.Min((sbyte)10, (sbyte)5));
    Assert.AreEqual((sbyte)5, Math.Min((sbyte)8, (sbyte)10, (sbyte)5, (sbyte)84, (sbyte)32));
    Assert.AreEqual((sbyte)5, Math.Min(new List<sbyte> { 8, 10, 5, 84, 32 }));

    Assert.AreEqual((sbyte)10, Math.Max((sbyte)5, (sbyte)10));
    Assert.AreEqual((sbyte)10, Math.Max((sbyte)10, (sbyte)5));
    Assert.AreEqual((sbyte)84, Math.Max((sbyte)8, (sbyte)10, (sbyte)5, (sbyte)84, (sbyte)32));
    Assert.AreEqual((sbyte)84, Math.Max(new List<sbyte> { 8, 10, 84, 5, 32 }));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the min and max of byte values.", TestOf = typeof(Math))]
  public void TestMinMaxByte()
  {
    Assert.AreEqual((byte)5, Math.Min((byte)5, (byte)10));
    Assert.AreEqual((byte)5, Math.Min((byte)10, (byte)5));
    Assert.AreEqual((byte)5, Math.Min((byte)8, (byte)10, (byte)5, (byte)84, (byte)32));
    Assert.AreEqual((byte)5, Math.Min(new List<byte> { 8, 10, 5, 84, 32 }));

    Assert.AreEqual((byte)10, Math.Max((byte)5, (byte)10));
    Assert.AreEqual((byte)10, Math.Max((byte)10, (byte)5));
    Assert.AreEqual((byte)84, Math.Max((byte)8, (byte)10, (byte)5, (byte)84, (byte)32));
    Assert.AreEqual((byte)84, Math.Max(new List<byte> { 8, 10, 84, 5, 32 }));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the min and max of short values.", TestOf = typeof(Math))]
  public void TestMinMaxShort()
  {
    Assert.AreEqual((short)5, Math.Min((short)5, (short)10));
    Assert.AreEqual((short)5, Math.Min((short)10, (short)5));
    Assert.AreEqual((short)5, Math.Min((short)8, (short)10, (short)5, (short)84, (short)32));
    Assert.AreEqual((short)5, Math.Min(new List<short> { 8, 10, 5, 84, 32 }));

    Assert.AreEqual((short)10, Math.Max((short)5, (short)10));
    Assert.AreEqual((short)10, Math.Max((short)10, (short)5));
    Assert.AreEqual((short)84, Math.Max((short)8, (short)10, (short)5, (short)84, (short)32));
    Assert.AreEqual((short)84, Math.Max(new List<short> { 8, 10, 84, 5, 32 }));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the min and max of ushort values.", TestOf = typeof(Math))]
  public void TestMinMaxUShort()
  {
    Assert.AreEqual((ushort)5, Math.Min((ushort)5, (ushort)10));
    Assert.AreEqual((ushort)5, Math.Min((ushort)10, (ushort)5));
    Assert.AreEqual((ushort)5, Math.Min((ushort)8, (ushort)10, (ushort)5, (ushort)84, (ushort)32));
    Assert.AreEqual((ushort)5, Math.Min(new List<ushort> { 8, 10, 5, 84, 32 }));

    Assert.AreEqual((ushort)10, Math.Max((ushort)5, (ushort)10));
    Assert.AreEqual((ushort)10, Math.Max((ushort)10, (ushort)5));
    Assert.AreEqual((ushort)84, Math.Max((ushort)8, (ushort)10, (ushort)5, (ushort)84, (ushort)32));
    Assert.AreEqual((ushort)84, Math.Max(new List<ushort> { 8, 10, 84, 5, 32 }));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the min and max of int values.", TestOf = typeof(Math))]
  public void TestMinMaxInt()
  {
    Assert.AreEqual(5, Math.Min(5, 10));
    Assert.AreEqual(5, Math.Min(10, 5));
    Assert.AreEqual(5, Math.Min(8, 10, 5, 84, 32));
    Assert.AreEqual(5, Math.Min(new List<ushort> { 8, 10, 5, 84, 32 }));

    Assert.AreEqual(10, Math.Max(5, 10));
    Assert.AreEqual(10, Math.Max(10, 5));
    Assert.AreEqual(84, Math.Max(8, 10, 5, 84, 32));
    Assert.AreEqual(84, Math.Max(new List<ushort> { 8, 10, 84, 5, 32 }));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the min and max of uint values.", TestOf = typeof(Math))]
  public void TestMinMaxUInt()
  {
    Assert.AreEqual((uint)5, Math.Min((uint)5, (uint)10));
    Assert.AreEqual((uint)5, Math.Min((uint)10, (uint)5));
    Assert.AreEqual((uint)5, Math.Min((uint)8, (uint)10, (uint)5, (uint)84, (uint)32));
    Assert.AreEqual((uint)5, Math.Min(new List<uint> { 8, 10, 5, 84, 32 }));

    Assert.AreEqual((uint)10, Math.Max((uint)5, (uint)10));
    Assert.AreEqual((uint)10, Math.Max((uint)10, (uint)5));
    Assert.AreEqual((uint)84, Math.Max((uint)8, (uint)10, (uint)5, (uint)84, (uint)32));
    Assert.AreEqual((uint)84, Math.Max(new List<uint> { 8, 10, 84, 5, 32 }));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the min and max of long values.", TestOf = typeof(Math))]
  public void TestMinMaxLong()
  {
    Assert.AreEqual(5L, Math.Min(5L, 10L));
    Assert.AreEqual(5L, Math.Min(10L, 5L));
    Assert.AreEqual(5L, Math.Min(8L, 10L, 5L, 84L, 32L));
    Assert.AreEqual(5L, Math.Min(new List<long> { 8L, 10L, 5L, 84L, 32L }));

    Assert.AreEqual(10L, Math.Max(5L, 10L));
    Assert.AreEqual(10L, Math.Max(10L, 5L));
    Assert.AreEqual(84L, Math.Max(8L, 10L, 5L, 84L, 32L));
    Assert.AreEqual(84L, Math.Max(new List<long> { 8L, 10L, 84L, 5L, 32L }));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the min and max of ulong values.", TestOf = typeof(Math))]
  public void TestMinMaxULong()
  {
    Assert.AreEqual((ulong)5, Math.Min((ulong)5, (ulong)10));
    Assert.AreEqual((ulong)5, Math.Min((ulong)10, (ulong)5));
    Assert.AreEqual((ulong)5, Math.Min((ulong)8, (ulong)10, (ulong)5, (ulong)84, (ulong)32));
    Assert.AreEqual((ulong)5, Math.Min(new List<ulong> { 8, 10, 5, 84, 32 }));

    Assert.AreEqual((ulong)10, Math.Max((ulong)5, (ulong)10));
    Assert.AreEqual((ulong)10, Math.Max((ulong)10, (ulong)5));
    Assert.AreEqual((ulong)84, Math.Max((ulong)8, (ulong)10, (ulong)5, (ulong)84, (ulong)32));
    Assert.AreEqual((ulong)84, Math.Max(new List<ulong> { 8, 10, 84, 5, 32 }));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the min and max of float values.", TestOf = typeof(Math))]
  public void TestMinMaxFloat()
  {
    Assert.AreEqual(5.4f, Math.Min(5.4f, 10.4f));
    Assert.AreEqual(5.4f, Math.Min(10.4f, 5.4f));
    Assert.AreEqual(5.4f, Math.Min(8.4f, 10.4f, 5.4f, 84.4f, 32.4f));
    Assert.AreEqual(5.4f, Math.Min(new List<float> { 8.4f, 10.4f, 5.4f, 84.4f, 32.4f }));

    Assert.AreEqual(10.4f, Math.Max(5.4f, 10.4f));
    Assert.AreEqual(10.4f, Math.Max(10.4f, 5.4f));
    Assert.AreEqual(84.4f, Math.Max(8.4f, 10.4f, 5.4f, 84.4f, 32.4f));
    Assert.AreEqual(84.4f, Math.Max(new List<float> { 8.4f, 10.4f, 84.4f, 5.4f, 32.4f }));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the min and max of double values.", TestOf = typeof(Math))]
  public void TestMinMaxDouble()
  {
    Assert.AreEqual(5.45, Math.Min(5.45, 10.45));
    Assert.AreEqual(5.45, Math.Min(10.45, 5.45));
    Assert.AreEqual(5.45, Math.Min(8.45, 10.45, 5.45, 84.45, 32.45));
    Assert.AreEqual(5.45, Math.Min(new List<double> { 8.45, 10.45, 5.45, 84.45, 32.45 }));

    Assert.AreEqual(10.45, Math.Max(5.45, 10.45));
    Assert.AreEqual(10.45, Math.Max(10.45, 5.45));
    Assert.AreEqual(84.45, Math.Max(8.45, 10.45, 5.45, 84.45, 32.45));
    Assert.AreEqual(84.45, Math.Max(new List<double> { 8.45, 10.45, 84.45, 5.45, 32.45 }));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the min and max of decimal values.", TestOf = typeof(Math))]
  public void TestMinMaxDecimal()
  {
    Assert.AreEqual((decimal)5.45, Math.Min((decimal)5.45, (decimal)10.45));
    Assert.AreEqual((decimal)5.45, Math.Min((decimal)10.45, (decimal)5.45));
    Assert.AreEqual((decimal)5.45, Math.Min((decimal)8.45, (decimal)10.45, (decimal)5.45, (decimal)84.45, (decimal)32.45));
    Assert.AreEqual((decimal)5.45, Math.Min(new List<decimal> { (decimal)8.45, (decimal)10.45, (decimal)5.45, (decimal)84.45, (decimal)32.45 }));

    Assert.AreEqual((decimal)10.45, Math.Max((decimal)5.45, (decimal)10.45));
    Assert.AreEqual((decimal)10.45, Math.Max((decimal)10.45, (decimal)5.45));
    Assert.AreEqual((decimal)84.45, Math.Max((decimal)8.45, (decimal)10.45, (decimal)5.45, (decimal)84.45, (decimal)32.45));
    Assert.AreEqual((decimal)84.45, Math.Max(new List<decimal> { (decimal)8.45, (decimal)10.45, (decimal)84.45, (decimal)5.45, (decimal)32.45 }));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the min and max of char values.", TestOf = typeof(Math))]
  public void TestMinMaxChar()
  {
    Assert.AreEqual('5', Math.Min('5', '7'));
    Assert.AreEqual('5', Math.Min('7', '5'));
    Assert.AreEqual('5', Math.Min('6', '7', '5', '9', '8'));
    Assert.AreEqual('5', Math.Min(new List<char> { '6', '7', '5', '9', '8' }));

    Assert.AreEqual('7', Math.Max('5', '7'));
    Assert.AreEqual('7', Math.Max('7', '5'));
    Assert.AreEqual('9', Math.Max('6', '7', '5', '9', '8'));
    Assert.AreEqual('9', Math.Max(new List<char> { '6', '7', '9', '5', '8' }));
  }
}
