using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Tenor.Tools.Math;
using Tenor.TestTools;
using Tenor.Tools.Text;

public class UT_MathTools_Range
{
  private struct RangeTest : System.IComparable<RangeTest>
  {
    public readonly int testValue;

    public RangeTest(int value)
    {
      testValue = value;
    }

    public int CompareTo(RangeTest other)
    {
      return testValue - other.testValue;
    }
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the range of IComparable values.", TestOf = typeof(Math))]
  public void TestInRangeIComparable()
  {
    Assert.IsTrue(Math.InRangeII(new RangeTest(5), new RangeTest(0), new RangeTest(10)));
    Assert.IsTrue(Math.InRangeII(new RangeTest(5), new RangeTest(0), new RangeTest(5)));
    Assert.IsTrue(Math.InRangeII(new RangeTest(5), new RangeTest(5), new RangeTest(10)));
    Assert.IsFalse(Math.InRangeII(new RangeTest(4), new RangeTest(5), new RangeTest(10)));
    Assert.IsFalse(Math.InRangeII(new RangeTest(15), new RangeTest(0), new RangeTest(10)));

    Assert.IsTrue(Math.InRangeEE(new RangeTest(5), new RangeTest(0), new RangeTest(10)));
    Assert.IsFalse(Math.InRangeEE(new RangeTest(5), new RangeTest(0), new RangeTest(5)));
    Assert.IsFalse(Math.InRangeEE(new RangeTest(5), new RangeTest(5), new RangeTest(10)));
    Assert.IsFalse(Math.InRangeEE(new RangeTest(4), new RangeTest(5), new RangeTest(10)));
    Assert.IsFalse(Math.InRangeEE(new RangeTest(15), new RangeTest(0), new RangeTest(10)));

    Assert.IsTrue(Math.InRangeIE(new RangeTest(5), new RangeTest(0), new RangeTest(10)));
    Assert.IsFalse(Math.InRangeIE(new RangeTest(5), new RangeTest(0), new RangeTest(5)));
    Assert.IsTrue(Math.InRangeIE(new RangeTest(5), new RangeTest(5), new RangeTest(10)));
    Assert.IsFalse(Math.InRangeIE(new RangeTest(4), new RangeTest(5), new RangeTest(10)));
    Assert.IsFalse(Math.InRangeIE(new RangeTest(15), new RangeTest(0), new RangeTest(10)));

    Assert.IsTrue(Math.InRangeEI(new RangeTest(5), new RangeTest(0), new RangeTest(10)));
    Assert.IsTrue(Math.InRangeEI(new RangeTest(5), new RangeTest(0), new RangeTest(5)));
    Assert.IsFalse(Math.InRangeEI(new RangeTest(5), new RangeTest(5), new RangeTest(10)));
    Assert.IsFalse(Math.InRangeEI(new RangeTest(4), new RangeTest(5), new RangeTest(10)));
    Assert.IsFalse(Math.InRangeEI(new RangeTest(15), new RangeTest(0), new RangeTest(10)));

    Assert.IsTrue(Math.InRangeIING(UnicodeCategoryType.Lo, UnicodeCategoryType.L, UnicodeCategoryType.N));
    Assert.IsTrue(Math.InRangeIING(UnicodeCategoryType.Lo, UnicodeCategoryType.L, UnicodeCategoryType.Lo));
    Assert.IsTrue(Math.InRangeIING(UnicodeCategoryType.Lo, UnicodeCategoryType.Lo, UnicodeCategoryType.N));
    Assert.IsFalse(Math.InRangeIING(UnicodeCategoryType.Lm, UnicodeCategoryType.Lo, UnicodeCategoryType.N));
    Assert.IsFalse(Math.InRangeIING(UnicodeCategoryType.Nl, UnicodeCategoryType.L, UnicodeCategoryType.N));

    Assert.IsTrue(Math.InRangeEENG(UnicodeCategoryType.Lo, UnicodeCategoryType.L, UnicodeCategoryType.N));
    Assert.IsFalse(Math.InRangeEENG(UnicodeCategoryType.Lo, UnicodeCategoryType.L, UnicodeCategoryType.Lo));
    Assert.IsFalse(Math.InRangeEENG(UnicodeCategoryType.Lo, UnicodeCategoryType.Lo, UnicodeCategoryType.N));
    Assert.IsFalse(Math.InRangeEENG(UnicodeCategoryType.Lm, UnicodeCategoryType.Lo, UnicodeCategoryType.N));
    Assert.IsFalse(Math.InRangeEENG(UnicodeCategoryType.Nl, UnicodeCategoryType.L, UnicodeCategoryType.N));

    Assert.IsTrue(Math.InRangeIENG(UnicodeCategoryType.Lo, UnicodeCategoryType.L, UnicodeCategoryType.N));
    Assert.IsFalse(Math.InRangeIENG(UnicodeCategoryType.Lo, UnicodeCategoryType.L, UnicodeCategoryType.Lo));
    Assert.IsTrue(Math.InRangeIENG(UnicodeCategoryType.Lo, UnicodeCategoryType.Lo, UnicodeCategoryType.N));
    Assert.IsFalse(Math.InRangeIENG(UnicodeCategoryType.Lm, UnicodeCategoryType.Lo, UnicodeCategoryType.N));
    Assert.IsFalse(Math.InRangeIENG(UnicodeCategoryType.Nl, UnicodeCategoryType.L, UnicodeCategoryType.N));

    Assert.IsTrue(Math.InRangeEING(UnicodeCategoryType.Lo, UnicodeCategoryType.L, UnicodeCategoryType.N));
    Assert.IsTrue(Math.InRangeEING(UnicodeCategoryType.Lo, UnicodeCategoryType.L, UnicodeCategoryType.Lo));
    Assert.IsFalse(Math.InRangeEING(UnicodeCategoryType.Lo, UnicodeCategoryType.Lo, UnicodeCategoryType.N));
    Assert.IsFalse(Math.InRangeEING(UnicodeCategoryType.Lm, UnicodeCategoryType.Lo, UnicodeCategoryType.N));
    Assert.IsFalse(Math.InRangeEING(UnicodeCategoryType.Nl, UnicodeCategoryType.L, UnicodeCategoryType.N));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the range of sbyte values.", TestOf = typeof(Math))]
  public void TestInRangeSByte()
  {
    Assert.IsTrue(Math.InRangeII((sbyte)5, (sbyte)0, (sbyte)10));
    Assert.IsTrue(Math.InRangeII((sbyte)5, (sbyte)0, (sbyte)5));
    Assert.IsTrue(Math.InRangeII((sbyte)5, (sbyte)5, (sbyte)10));
    Assert.IsFalse(Math.InRangeII((sbyte)-5, (sbyte)0, (sbyte)10));
    Assert.IsFalse(Math.InRangeII((sbyte)15, (sbyte)0, (sbyte)10));

    Assert.IsTrue(Math.InRangeEE((sbyte)5, (sbyte)0, (sbyte)10));
    Assert.IsFalse(Math.InRangeEE((sbyte)5, (sbyte)0, (sbyte)5));
    Assert.IsFalse(Math.InRangeEE((sbyte)5, (sbyte)5, (sbyte)10));
    Assert.IsFalse(Math.InRangeEE((sbyte)-5, (sbyte)0, (sbyte)10));
    Assert.IsFalse(Math.InRangeEE((sbyte)15, (sbyte)0, (sbyte)10));

    Assert.IsTrue(Math.InRangeIE((sbyte)5, (sbyte)0, (sbyte)10));
    Assert.IsFalse(Math.InRangeIE((sbyte)5, (sbyte)0, (sbyte)5));
    Assert.IsTrue(Math.InRangeIE((sbyte)5, (sbyte)5, (sbyte)10));
    Assert.IsFalse(Math.InRangeIE((sbyte)-5, (sbyte)0, (sbyte)10));
    Assert.IsFalse(Math.InRangeIE((sbyte)15, (sbyte)0, (sbyte)10));

    Assert.IsTrue(Math.InRangeEI((sbyte)5, (sbyte)0, (sbyte)10));
    Assert.IsTrue(Math.InRangeEI((sbyte)5, (sbyte)0, (sbyte)5));
    Assert.IsFalse(Math.InRangeEI((sbyte)5, (sbyte)5, (sbyte)10));
    Assert.IsFalse(Math.InRangeEI((sbyte)-5, (sbyte)0, (sbyte)10));
    Assert.IsFalse(Math.InRangeEI((sbyte)15, (sbyte)0, (sbyte)10));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the range of byte values.", TestOf = typeof(Math))]
  public void TestInRangeByte()
  {
    Assert.IsTrue(Math.InRangeII((byte)5, (byte)0, (byte)10));
    Assert.IsTrue(Math.InRangeII((byte)5, (byte)0, (byte)5));
    Assert.IsTrue(Math.InRangeII((byte)5, (byte)5, (byte)10));
    Assert.IsFalse(Math.InRangeII((byte)15, (byte)0, (byte)10));

    Assert.IsTrue(Math.InRangeEE((byte)5, (byte)0, (byte)10));
    Assert.IsFalse(Math.InRangeEE((byte)5, (byte)0, (byte)5));
    Assert.IsFalse(Math.InRangeEE((byte)5, (byte)5, (byte)10));
    Assert.IsFalse(Math.InRangeEE((byte)15, (byte)0, (byte)10));

    Assert.IsTrue(Math.InRangeIE((byte)5, (byte)0, (byte)10));
    Assert.IsFalse(Math.InRangeIE((byte)5, (byte)0, (byte)5));
    Assert.IsTrue(Math.InRangeIE((byte)5, (byte)5, (byte)10));
    Assert.IsFalse(Math.InRangeIE((byte)15, (byte)0, (byte)10));

    Assert.IsTrue(Math.InRangeEI((byte)5, (byte)0, (byte)10));
    Assert.IsTrue(Math.InRangeEI((byte)5, (byte)0, (byte)5));
    Assert.IsFalse(Math.InRangeEI((byte)5, (byte)5, (byte)10));
    Assert.IsFalse(Math.InRangeEI((byte)15, (byte)0, (byte)10));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the range of short values.", TestOf = typeof(Math))]
  public void TestInRangeShort()
  {
    Assert.IsTrue(Math.InRangeII((short)5, (short)0, (short)10));
    Assert.IsTrue(Math.InRangeII((short)5, (short)0, (short)5));
    Assert.IsTrue(Math.InRangeII((short)5, (short)5, (short)10));
    Assert.IsFalse(Math.InRangeII((short)-5, (short)0, (short)10));
    Assert.IsFalse(Math.InRangeII((short)15, (short)0, (short)10));

    Assert.IsTrue(Math.InRangeEE((short)5, (short)0, (short)10));
    Assert.IsFalse(Math.InRangeEE((short)5, (short)0, (short)5));
    Assert.IsFalse(Math.InRangeEE((short)5, (short)5, (short)10));
    Assert.IsFalse(Math.InRangeEE((short)-5, (short)0, (short)10));
    Assert.IsFalse(Math.InRangeEE((short)15, (short)0, (short)10));

    Assert.IsTrue(Math.InRangeIE((short)5, (short)0, (short)10));
    Assert.IsFalse(Math.InRangeIE((short)5, (short)0, (short)5));
    Assert.IsTrue(Math.InRangeIE((short)5, (short)5, (short)10));
    Assert.IsFalse(Math.InRangeIE((short)-5, (short)0, (short)10));
    Assert.IsFalse(Math.InRangeIE((short)15, (short)0, (short)10));

    Assert.IsTrue(Math.InRangeEI((short)5, (short)0, (short)10));
    Assert.IsTrue(Math.InRangeEI((short)5, (short)0, (short)5));
    Assert.IsFalse(Math.InRangeEI((short)5, (short)5, (short)10));
    Assert.IsFalse(Math.InRangeEI((short)-5, (short)0, (short)10));
    Assert.IsFalse(Math.InRangeEI((short)15, (short)0, (short)10));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the range of ushort values.", TestOf = typeof(Math))]
  public void TestInRangeUShort()
  {
    Assert.IsTrue(Math.InRangeII((ushort)5, (ushort)0, (ushort)10));
    Assert.IsTrue(Math.InRangeII((ushort)5, (ushort)0, (ushort)5));
    Assert.IsTrue(Math.InRangeII((ushort)5, (ushort)5, (ushort)10));
    Assert.IsFalse(Math.InRangeII((ushort)15, (ushort)0, (ushort)10));

    Assert.IsTrue(Math.InRangeEE((ushort)5, (ushort)0, (ushort)10));
    Assert.IsFalse(Math.InRangeEE((ushort)5, (ushort)0, (ushort)5));
    Assert.IsFalse(Math.InRangeEE((ushort)5, (ushort)5, (ushort)10));
    Assert.IsFalse(Math.InRangeEE((ushort)15, (ushort)0, (ushort)10));

    Assert.IsTrue(Math.InRangeIE((ushort)5, (ushort)0, (ushort)10));
    Assert.IsFalse(Math.InRangeIE((ushort)5, (ushort)0, (ushort)5));
    Assert.IsTrue(Math.InRangeIE((ushort)5, (ushort)5, (ushort)10));
    Assert.IsFalse(Math.InRangeIE((ushort)15, (ushort)0, (ushort)10));

    Assert.IsTrue(Math.InRangeEI((ushort)5, (ushort)0, (ushort)10));
    Assert.IsTrue(Math.InRangeEI((ushort)5, (ushort)0, (ushort)5));
    Assert.IsFalse(Math.InRangeEI((ushort)5, (ushort)5, (ushort)10));
    Assert.IsFalse(Math.InRangeEI((ushort)15, (ushort)0, (ushort)10));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the range of int values.", TestOf = typeof(Math))]
  public void TestInRangeInt()
  {
    Assert.IsTrue(Math.InRangeII(5, 0, 10));
    Assert.IsTrue(Math.InRangeII(5, 0, 5));
    Assert.IsTrue(Math.InRangeII(5, 5, 10));
    Assert.IsFalse(Math.InRangeII(-5, 0, 10));
    Assert.IsFalse(Math.InRangeII(15, 0, 10));

    Assert.IsTrue(Math.InRangeEE(5, 0, 10));
    Assert.IsFalse(Math.InRangeEE(5, 0, 5));
    Assert.IsFalse(Math.InRangeEE(5, 5, 10));
    Assert.IsFalse(Math.InRangeEE(-5, 0, 10));
    Assert.IsFalse(Math.InRangeEE(15, 0, 10));

    Assert.IsTrue(Math.InRangeIE(5, 0, 10));
    Assert.IsFalse(Math.InRangeIE(5, 0, 5));
    Assert.IsTrue(Math.InRangeIE(5, 5, 10));
    Assert.IsFalse(Math.InRangeIE(-5, 0, 10));
    Assert.IsFalse(Math.InRangeIE(15, 0, 10));

    Assert.IsTrue(Math.InRangeEI(5, 0, 10));
    Assert.IsTrue(Math.InRangeEI(5, 0, 5));
    Assert.IsFalse(Math.InRangeEI(5, 5, 10));
    Assert.IsFalse(Math.InRangeEI(-5, 0, 10));
    Assert.IsFalse(Math.InRangeEI(15, 0, 10));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the range of uint values.", TestOf = typeof(Math))]
  public void TestInRangeUInt()
  {
    Assert.IsTrue(Math.InRangeII((uint)5, (uint)0, (uint)10));
    Assert.IsTrue(Math.InRangeII((uint)5, (uint)0, (uint)5));
    Assert.IsTrue(Math.InRangeII((uint)5, (uint)5, (uint)10));
    Assert.IsFalse(Math.InRangeII((uint)15, (uint)0, (uint)10));

    Assert.IsTrue(Math.InRangeEE((uint)5, (uint)0, (uint)10));
    Assert.IsFalse(Math.InRangeEE((uint)5, (uint)0, (uint)5));
    Assert.IsFalse(Math.InRangeEE((uint)5, (uint)5, (uint)10));
    Assert.IsFalse(Math.InRangeEE((uint)15, (uint)0, (uint)10));

    Assert.IsTrue(Math.InRangeIE((uint)5, (uint)0, (uint)10));
    Assert.IsFalse(Math.InRangeIE((uint)5, (uint)0, (uint)5));
    Assert.IsTrue(Math.InRangeIE((uint)5, (uint)5, (uint)10));
    Assert.IsFalse(Math.InRangeIE((uint)15, (uint)0, (uint)10));

    Assert.IsTrue(Math.InRangeEI((uint)5, (uint)0, (uint)10));
    Assert.IsTrue(Math.InRangeEI((uint)5, (uint)0, (uint)5));
    Assert.IsFalse(Math.InRangeEI((uint)5, (uint)5, (uint)10));
    Assert.IsFalse(Math.InRangeEI((uint)15, (uint)0, (uint)10));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the range of long values.", TestOf = typeof(Math))]
  public void TestInRangeLong()
  {
    Assert.IsTrue(Math.InRangeII((long)5, (long)0, (long)10));
    Assert.IsTrue(Math.InRangeII((long)5, (long)0, (long)5));
    Assert.IsTrue(Math.InRangeII((long)5, (long)5, (long)10));
    Assert.IsFalse(Math.InRangeII((long)-5, (long)0, (long)10));
    Assert.IsFalse(Math.InRangeII((long)15, (long)0, (long)10));

    Assert.IsTrue(Math.InRangeEE((long)5, (long)0, (long)10));
    Assert.IsFalse(Math.InRangeEE((long)5, (long)0, (long)5));
    Assert.IsFalse(Math.InRangeEE((long)5, (long)5, (long)10));
    Assert.IsFalse(Math.InRangeEE((long)-5, (long)0, (long)10));
    Assert.IsFalse(Math.InRangeEE((long)15, (long)0, (long)10));

    Assert.IsTrue(Math.InRangeIE((long)5, (long)0, (long)10));
    Assert.IsFalse(Math.InRangeIE((long)5, (long)0, (long)5));
    Assert.IsTrue(Math.InRangeIE((long)5, (long)5, (long)10));
    Assert.IsFalse(Math.InRangeIE((long)-5, (long)0, (long)10));
    Assert.IsFalse(Math.InRangeIE((long)15, (long)0, (long)10));

    Assert.IsTrue(Math.InRangeEI((long)5, (long)0, (long)10));
    Assert.IsTrue(Math.InRangeEI((long)5, (long)0, (long)5));
    Assert.IsFalse(Math.InRangeEI((long)5, (long)5, (long)10));
    Assert.IsFalse(Math.InRangeEI((long)-5, (long)0, (long)10));
    Assert.IsFalse(Math.InRangeEI((long)15, (long)0, (long)10));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the range of ulong values.", TestOf = typeof(Math))]
  public void TestInRangeULong()
  {
    Assert.IsTrue(Math.InRangeII((ulong)5, (ulong)0, (ulong)10));
    Assert.IsTrue(Math.InRangeII((ulong)5, (ulong)0, (ulong)5));
    Assert.IsTrue(Math.InRangeII((ulong)5, (ulong)5, (ulong)10));
    Assert.IsFalse(Math.InRangeII((ulong)15, (ulong)0, (ulong)10));

    Assert.IsTrue(Math.InRangeEE((ulong)5, (ulong)0, (ulong)10));
    Assert.IsFalse(Math.InRangeEE((ulong)5, (ulong)0, (ulong)5));
    Assert.IsFalse(Math.InRangeEE((ulong)5, (ulong)5, (ulong)10));
    Assert.IsFalse(Math.InRangeEE((ulong)15, (ulong)0, (ulong)10));

    Assert.IsTrue(Math.InRangeIE((ulong)5, (ulong)0, (ulong)10));
    Assert.IsFalse(Math.InRangeIE((ulong)5, (ulong)0, (ulong)5));
    Assert.IsTrue(Math.InRangeIE((ulong)5, (ulong)5, (ulong)10));
    Assert.IsFalse(Math.InRangeIE((ulong)15, (ulong)0, (ulong)10));

    Assert.IsTrue(Math.InRangeEI((ulong)5, (ulong)0, (ulong)10));
    Assert.IsTrue(Math.InRangeEI((ulong)5, (ulong)0, (ulong)5));
    Assert.IsFalse(Math.InRangeEI((ulong)5, (ulong)5, (ulong)10));
    Assert.IsFalse(Math.InRangeEI((ulong)15, (ulong)0, (ulong)10));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the range of float values.", TestOf = typeof(Math))]
  public void TestInRangeFloat()
  {
    Assert.IsTrue(Math.InRangeII(5.4f, 0.4f, 10.4f));
    Assert.IsTrue(Math.InRangeII(5.4f, 0.4f, 5.4f));
    Assert.IsTrue(Math.InRangeII(5.4f, 5.4f, 10.4f));
    Assert.IsFalse(Math.InRangeII(-5.4f, 0.4f, 10.4f));
    Assert.IsFalse(Math.InRangeII(15.4f, 0.4f, 10.4f));

    Assert.IsTrue(Math.InRangeEE(5.4f, 0.4f, 10.4f));
    Assert.IsFalse(Math.InRangeEE(5.4f, 0.4f, 5.4f));
    Assert.IsFalse(Math.InRangeEE(5.4f, 5.4f, 10.4f));
    Assert.IsFalse(Math.InRangeEE(-5.4f, 0.4f, 10.4f));
    Assert.IsFalse(Math.InRangeEE(15.4f, 0.4f, 10.4f));

    Assert.IsTrue(Math.InRangeIE(5.4f, 0.4f, 10.4f));
    Assert.IsFalse(Math.InRangeIE(5.4f, 0.4f, 5.4f));
    Assert.IsTrue(Math.InRangeIE(5.4f, 5.4f, 10.4f));
    Assert.IsFalse(Math.InRangeIE(-5.4f, 0.4f, 10.4f));
    Assert.IsFalse(Math.InRangeIE(15.4f, 0.4f, 10.4f));

    Assert.IsTrue(Math.InRangeEI(5.4f, 0.4f, 10.4f));
    Assert.IsTrue(Math.InRangeEI(5.4f, 0.4f, 5.4f));
    Assert.IsFalse(Math.InRangeEI(5.4f, 5.4f, 10.4f));
    Assert.IsFalse(Math.InRangeEI(-5.4f, 0.4f, 10.4f));
    Assert.IsFalse(Math.InRangeEI(15.4f, 0.4f, 10.4f));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the range of double values.", TestOf = typeof(Math))]
  public void TestInRangeDouble()
  {
    Assert.IsTrue(Math.InRangeII(5.4, 0.4, 10.4));
    Assert.IsTrue(Math.InRangeII(5.4, 0.4, 5.4));
    Assert.IsTrue(Math.InRangeII(5.4, 5.4, 10.4));
    Assert.IsFalse(Math.InRangeII(-5.4, 0.4, 10.4));
    Assert.IsFalse(Math.InRangeII(15.4, 0.4, 10.4));

    Assert.IsTrue(Math.InRangeEE(5.4, 0.4, 10.4));
    Assert.IsFalse(Math.InRangeEE(5.4, 0.4, 5.4));
    Assert.IsFalse(Math.InRangeEE(5.4, 5.4, 10.4));
    Assert.IsFalse(Math.InRangeEE(-5.4, 0.4, 10.4));
    Assert.IsFalse(Math.InRangeEE(15.4, 0.4, 10.4));

    Assert.IsTrue(Math.InRangeIE(5.4, 0.4, 10.4));
    Assert.IsFalse(Math.InRangeIE(5.4, 0.4, 5.4));
    Assert.IsTrue(Math.InRangeIE(5.4, 5.4, 10.4));
    Assert.IsFalse(Math.InRangeIE(-5.4, 0.4, 10.4));
    Assert.IsFalse(Math.InRangeIE(15.4, 0.4, 10.4));

    Assert.IsTrue(Math.InRangeEI(5.4, 0.4, 10.4));
    Assert.IsTrue(Math.InRangeEI(5.4, 0.4, 5.4));
    Assert.IsFalse(Math.InRangeEI(5.4, 5.4, 10.4));
    Assert.IsFalse(Math.InRangeEI(-5.4, 0.4, 10.4));
    Assert.IsFalse(Math.InRangeEI(15.4, 0.4, 10.4));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the range of decimal values.", TestOf = typeof(Math))]
  public void TestInRangeDecimal()
  {
    Assert.IsTrue(Math.InRangeII((decimal)5.4, (decimal)0.4, (decimal)10.4));
    Assert.IsTrue(Math.InRangeII((decimal)5.4, (decimal)0.4, (decimal)5.4));
    Assert.IsTrue(Math.InRangeII((decimal)5.4, (decimal)5.4, (decimal)10.4));
    Assert.IsFalse(Math.InRangeII((decimal)-5.4, (decimal)0.4, (decimal)10.4));
    Assert.IsFalse(Math.InRangeII((decimal)15.4, (decimal)0.4, (decimal)10.4));

    Assert.IsTrue(Math.InRangeEE((decimal)5.4, (decimal)0.4, (decimal)10.4));
    Assert.IsFalse(Math.InRangeEE((decimal)5.4, (decimal)0.4, (decimal)5.4));
    Assert.IsFalse(Math.InRangeEE((decimal)5.4, (decimal)5.4, (decimal)10.4));
    Assert.IsFalse(Math.InRangeEE((decimal)-5.4, (decimal)0.4, (decimal)10.4));
    Assert.IsFalse(Math.InRangeEE((decimal)15.4, (decimal)0.4, (decimal)10.4));

    Assert.IsTrue(Math.InRangeIE((decimal)5.4, (decimal)0.4, (decimal)10.4));
    Assert.IsFalse(Math.InRangeIE((decimal)5.4, (decimal)0.4, (decimal)5.4));
    Assert.IsTrue(Math.InRangeIE((decimal)5.4, (decimal)5.4, (decimal)10.4));
    Assert.IsFalse(Math.InRangeIE((decimal)-5.4, (decimal)0.4, (decimal)10.4));
    Assert.IsFalse(Math.InRangeIE((decimal)15.4, (decimal)0.4, (decimal)10.4));

    Assert.IsTrue(Math.InRangeEI((decimal)5.4, (decimal)0.4, (decimal)10.4));
    Assert.IsTrue(Math.InRangeEI((decimal)5.4, (decimal)0.4, (decimal)5.4));
    Assert.IsFalse(Math.InRangeEI((decimal)5.4, (decimal)5.4, (decimal)10.4));
    Assert.IsFalse(Math.InRangeEI((decimal)-5.4, (decimal)0.4, (decimal)10.4));
    Assert.IsFalse(Math.InRangeEI((decimal)15.4, (decimal)0.4, (decimal)10.4));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for checking the range of char values.", TestOf = typeof(Math))]
  public void TestInRangeChar()
  {
    Assert.IsTrue(Math.InRangeII('5', '0', '8'));
    Assert.IsTrue(Math.InRangeII('5', '0', '5'));
    Assert.IsTrue(Math.InRangeII('5', '5', '8'));
    Assert.IsFalse(Math.InRangeII(-'5', '0', '8'));
    Assert.IsFalse(Math.InRangeII('9', '0', '8'));

    Assert.IsTrue(Math.InRangeEE('5', '0', '8'));
    Assert.IsFalse(Math.InRangeEE('5', '0', '5'));
    Assert.IsFalse(Math.InRangeEE('5', '5', '8'));
    Assert.IsFalse(Math.InRangeEE(-'5', '0', '8'));
    Assert.IsFalse(Math.InRangeEE('9', '0', '8'));

    Assert.IsTrue(Math.InRangeIE('5', '0', '8'));
    Assert.IsFalse(Math.InRangeIE('5', '0', '5'));
    Assert.IsTrue(Math.InRangeIE('5', '5', '8'));
    Assert.IsFalse(Math.InRangeIE(-'5', '0', '8'));
    Assert.IsFalse(Math.InRangeIE('9', '0', '8'));

    Assert.IsTrue(Math.InRangeEI('5', '0', '8'));
    Assert.IsTrue(Math.InRangeEI('5', '0', '5'));
    Assert.IsFalse(Math.InRangeEI('5', '5', '8'));
    Assert.IsFalse(Math.InRangeEI(-'5', '0', '8'));
    Assert.IsFalse(Math.InRangeEI('9', '0', '8'));
  }
}