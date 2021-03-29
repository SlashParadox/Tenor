using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Tenor.TestTools;
using Tenor.Tools.Math;
using Tenor.Tools.Text;
public class UT_MathTools_Clamp
{
  private struct ClampTest : System.IComparable<ClampTest>
  {
    public readonly int testValue;

    public ClampTest(int value)
    {
      testValue = value;
    }

    public int CompareTo(ClampTest other)
    {
      return testValue - other.testValue;
    }
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for clamping IComparable values.", TestOf = typeof(Math))]
  public void TestClampIComparable()
  {
    Assert.AreEqual(new ClampTest(5), Math.ClampII(new ClampTest(5), new ClampTest(0), new ClampTest(10)));
    Assert.AreEqual(new ClampTest(5), Math.ClampII(new ClampTest(5), new ClampTest(0), new ClampTest(5)));
    Assert.AreEqual(new ClampTest(5), Math.ClampII(new ClampTest(5), new ClampTest(5), new ClampTest(10)));
    Assert.AreEqual(new ClampTest(5), Math.ClampII(new ClampTest(4), new ClampTest(5), new ClampTest(10)));
    Assert.AreEqual(new ClampTest(10), Math.ClampII(new ClampTest(15), new ClampTest(0), new ClampTest(10)));

    Assert.AreEqual(UnicodeCategoryType.Lo, Math.ClampIING(UnicodeCategoryType.Lo, UnicodeCategoryType.L, UnicodeCategoryType.N));
    Assert.AreEqual(UnicodeCategoryType.Lo, Math.ClampIING(UnicodeCategoryType.Lo, UnicodeCategoryType.L, UnicodeCategoryType.Lo));
    Assert.AreEqual(UnicodeCategoryType.Lo, Math.ClampIING(UnicodeCategoryType.Lo, UnicodeCategoryType.Lo, UnicodeCategoryType.N));
    Assert.AreEqual(UnicodeCategoryType.Lo, Math.ClampIING(UnicodeCategoryType.Lm, UnicodeCategoryType.Lo, UnicodeCategoryType.N));
    Assert.AreEqual(UnicodeCategoryType.N, Math.ClampIING(UnicodeCategoryType.Nl, UnicodeCategoryType.L, UnicodeCategoryType.N));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for clamping sbyte values.", TestOf = typeof(Math))]
  public void TestClampSByte()
  {
    Assert.AreEqual((sbyte)5, Math.ClampII((sbyte)5, (sbyte)0, (sbyte)10));
    Assert.AreEqual((sbyte)5, Math.ClampII((sbyte)5, (sbyte)0, (sbyte)5));
    Assert.AreEqual((sbyte)5, Math.ClampII((sbyte)5, (sbyte)5, (sbyte)10));
    Assert.AreEqual((sbyte)0, Math.ClampII((sbyte)-5, (sbyte)0, (sbyte)10));
    Assert.AreEqual((sbyte)10, Math.ClampII((sbyte)15, (sbyte)0, (sbyte)10));

    Assert.AreEqual(5, Math.ClampEE((sbyte)5, (sbyte)0, (sbyte)10));
    Assert.AreEqual(4, Math.ClampEE((sbyte)5, (sbyte)0, (sbyte)5));
    Assert.AreEqual(6, Math.ClampEE((sbyte)5, (sbyte)5, (sbyte)10));
    Assert.AreEqual(1, Math.ClampEE((sbyte)-5, (sbyte)0, (sbyte)10));
    Assert.AreEqual(9, Math.ClampEE((sbyte)15, (sbyte)0, (sbyte)10));

    Assert.AreEqual(5, Math.ClampIE((sbyte)5, (sbyte)0, (sbyte)10));
    Assert.AreEqual(4, Math.ClampIE((sbyte)5, (sbyte)0, (sbyte)5));
    Assert.AreEqual(5, Math.ClampIE((sbyte)5, (sbyte)5, (sbyte)10));
    Assert.AreEqual(0, Math.ClampIE((sbyte)-5, (sbyte)0, (sbyte)10));
    Assert.AreEqual(9, Math.ClampIE((sbyte)15, (sbyte)0, (sbyte)10));

    Assert.AreEqual(5, Math.ClampEI((sbyte)5, (sbyte)0, (sbyte)10));
    Assert.AreEqual(5, Math.ClampEI((sbyte)5, (sbyte)0, (sbyte)5));
    Assert.AreEqual(6, Math.ClampEI((sbyte)5, (sbyte)5, (sbyte)10));
    Assert.AreEqual(1, Math.ClampEI((sbyte)-5, (sbyte)0, (sbyte)10));
    Assert.AreEqual(10, Math.ClampEI((sbyte)15, (sbyte)0, (sbyte)10));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for clamping byte values.", TestOf = typeof(Math))]
  public void TestClampByte()
  {
    Assert.AreEqual((byte)5, Math.ClampII((byte)5, (byte)0, (byte)10));
    Assert.AreEqual((byte)5, Math.ClampII((byte)5, (byte)0, (byte)5));
    Assert.AreEqual((byte)5, Math.ClampII((byte)5, (byte)5, (byte)10));
    Assert.AreEqual((byte)10, Math.ClampII((byte)15, (byte)0, (byte)10));

    Assert.AreEqual(5, Math.ClampEE((byte)5, (byte)0, (byte)10));
    Assert.AreEqual(4, Math.ClampEE((byte)5, (byte)0, (byte)5));
    Assert.AreEqual(6, Math.ClampEE((byte)5, (byte)5, (byte)10));
    Assert.AreEqual(9, Math.ClampEE((byte)15, (byte)0, (byte)10));

    Assert.AreEqual(5, Math.ClampIE((byte)5, (byte)0, (byte)10));
    Assert.AreEqual(4, Math.ClampIE((byte)5, (byte)0, (byte)5));
    Assert.AreEqual(5, Math.ClampIE((byte)5, (byte)5, (byte)10));
    Assert.AreEqual(9, Math.ClampIE((byte)15, (byte)0, (byte)10));

    Assert.AreEqual(5, Math.ClampEI((byte)5, (byte)0, (byte)10));
    Assert.AreEqual(5, Math.ClampEI((byte)5, (byte)0, (byte)5));
    Assert.AreEqual(6, Math.ClampEI((byte)5, (byte)5, (byte)10));
    Assert.AreEqual(10, Math.ClampEI((byte)15, (byte)0, (byte)10));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for clamping short values.", TestOf = typeof(Math))]
  public void TestClampShort()
  {
    Assert.AreEqual((short)5, Math.ClampII((short)5, (short)0, (short)10));
    Assert.AreEqual((short)5, Math.ClampII((short)5, (short)0, (short)5));
    Assert.AreEqual((short)5, Math.ClampII((short)5, (short)5, (short)10));
    Assert.AreEqual((short)0, Math.ClampII((short)-5, (short)0, (short)10));
    Assert.AreEqual((short)10, Math.ClampII((short)15, (short)0, (short)10));

    Assert.AreEqual(5, Math.ClampEE((short)5, (short)0, (short)10));
    Assert.AreEqual(4, Math.ClampEE((short)5, (short)0, (short)5));
    Assert.AreEqual(6, Math.ClampEE((short)5, (short)5, (short)10));
    Assert.AreEqual(1, Math.ClampEE((short)-5, (short)0, (short)10));
    Assert.AreEqual(9, Math.ClampEE((short)15, (short)0, (short)10));

    Assert.AreEqual(5, Math.ClampIE((short)5, (short)0, (short)10));
    Assert.AreEqual(4, Math.ClampIE((short)5, (short)0, (short)5));
    Assert.AreEqual(5, Math.ClampIE((short)5, (short)5, (short)10));
    Assert.AreEqual(0, Math.ClampIE((short)-5, (short)0, (short)10));
    Assert.AreEqual(9, Math.ClampIE((short)15, (short)0, (short)10));

    Assert.AreEqual(5, Math.ClampEI((short)5, (short)0, (short)10));
    Assert.AreEqual(5, Math.ClampEI((short)5, (short)0, (short)5));
    Assert.AreEqual(6, Math.ClampEI((short)5, (short)5, (short)10));
    Assert.AreEqual(1, Math.ClampEI((short)-5, (short)0, (short)10));
    Assert.AreEqual(10, Math.ClampEI((short)15, (short)0, (short)10));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for clamping ushort values.", TestOf = typeof(Math))]
  public void TestClampUShort()
  {
    Assert.AreEqual((ushort)5, Math.ClampII((ushort)5, (ushort)0, (ushort)10));
    Assert.AreEqual((ushort)5, Math.ClampII((ushort)5, (ushort)0, (ushort)5));
    Assert.AreEqual((ushort)5, Math.ClampII((ushort)5, (ushort)5, (ushort)10));
    Assert.AreEqual((ushort)10, Math.ClampII((ushort)15, (ushort)0, (ushort)10));

    Assert.AreEqual(5, Math.ClampEE((ushort)5, (ushort)0, (ushort)10));
    Assert.AreEqual(4, Math.ClampEE((ushort)5, (ushort)0, (ushort)5));
    Assert.AreEqual(6, Math.ClampEE((ushort)5, (ushort)5, (ushort)10));
    Assert.AreEqual(9, Math.ClampEE((ushort)15, (ushort)0, (ushort)10));

    Assert.AreEqual(5, Math.ClampIE((ushort)5, (ushort)0, (ushort)10));
    Assert.AreEqual(4, Math.ClampIE((ushort)5, (ushort)0, (ushort)5));
    Assert.AreEqual(5, Math.ClampIE((ushort)5, (ushort)5, (ushort)10));
    Assert.AreEqual(9, Math.ClampIE((ushort)15, (ushort)0, (ushort)10));

    Assert.AreEqual(5, Math.ClampEI((ushort)5, (ushort)0, (ushort)10));
    Assert.AreEqual(5, Math.ClampEI((ushort)5, (ushort)0, (ushort)5));
    Assert.AreEqual(6, Math.ClampEI((ushort)5, (ushort)5, (ushort)10));
    Assert.AreEqual(10, Math.ClampEI((ushort)15, (ushort)0, (ushort)10));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for clamping int values.", TestOf = typeof(Math))]
  public void TestClampInt()
  {
    Assert.AreEqual(5, Math.ClampII(5, 0, 10));
    Assert.AreEqual(5, Math.ClampII(5, 0, 5));
    Assert.AreEqual(5, Math.ClampII(5, 5, 10));
    Assert.AreEqual(0, Math.ClampII(-5, 0, 10));
    Assert.AreEqual(10, Math.ClampII(15, 0, 10));

    Assert.AreEqual(5, Math.ClampEE(5, 0, 10));
    Assert.AreEqual(4, Math.ClampEE(5, 0, 5));
    Assert.AreEqual(6, Math.ClampEE(5, 5, 10));
    Assert.AreEqual(1, Math.ClampEE(-5, 0, 10));
    Assert.AreEqual(9, Math.ClampEE(15, 0, 10));

    Assert.AreEqual(5, Math.ClampIE(5, 0, 10));
    Assert.AreEqual(4, Math.ClampIE(5, 0, 5));
    Assert.AreEqual(5, Math.ClampIE(5, 5, 10));
    Assert.AreEqual(0, Math.ClampIE(-5, 0, 10));
    Assert.AreEqual(9, Math.ClampIE(15, 0, 10));

    Assert.AreEqual(5, Math.ClampEI(5, 0, 10));
    Assert.AreEqual(5, Math.ClampEI(5, 0, 5));
    Assert.AreEqual(6, Math.ClampEI(5, 5, 10));
    Assert.AreEqual(1, Math.ClampEI(-5, 0, 10));
    Assert.AreEqual(10, Math.ClampEI(15, 0, 10));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for clamping uint values.", TestOf = typeof(Math))]
  public void TestClampUInt()
  {
    Assert.AreEqual((uint)5, Math.ClampII((uint)5, (uint)0, (uint)10));
    Assert.AreEqual((uint)5, Math.ClampII((uint)5, (uint)0, (uint)5));
    Assert.AreEqual((uint)5, Math.ClampII((uint)5, (uint)5, (uint)10));
    Assert.AreEqual((uint)10, Math.ClampII((uint)15, (uint)0, (uint)10));

    Assert.AreEqual(5, Math.ClampEE((uint)5, (uint)0, (uint)10));
    Assert.AreEqual(4, Math.ClampEE((uint)5, (uint)0, (uint)5));
    Assert.AreEqual(6, Math.ClampEE((uint)5, (uint)5, (uint)10));
    Assert.AreEqual(9, Math.ClampEE((uint)15, (uint)0, (uint)10));

    Assert.AreEqual(5, Math.ClampIE((uint)5, (uint)0, (uint)10));
    Assert.AreEqual(4, Math.ClampIE((uint)5, (uint)0, (uint)5));
    Assert.AreEqual(5, Math.ClampIE((uint)5, (uint)5, (uint)10));
    Assert.AreEqual(9, Math.ClampIE((uint)15, (uint)0, (uint)10));

    Assert.AreEqual(5, Math.ClampEI((uint)5, (uint)0, (uint)10));
    Assert.AreEqual(5, Math.ClampEI((uint)5, (uint)0, (uint)5));
    Assert.AreEqual(6, Math.ClampEI((uint)5, (uint)5, (uint)10));
    Assert.AreEqual(10, Math.ClampEI((uint)15, (uint)0, (uint)10));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for clamping long values.", TestOf = typeof(Math))]
  public void TestClampLong()
  {
    Assert.AreEqual(5L, Math.ClampII(5L, 0L, 10L));
    Assert.AreEqual(5L, Math.ClampII(5L, 0L, 5L));
    Assert.AreEqual(5L, Math.ClampII(5L, 5L, 10L));
    Assert.AreEqual(0L, Math.ClampII(-5, 0L, 10L));
    Assert.AreEqual(10L, Math.ClampII(15, 0L, 10L));

    Assert.AreEqual(5L, Math.ClampEE(5L, 0L, 10L));
    Assert.AreEqual(4L, Math.ClampEE(5L, 0L, 5L));
    Assert.AreEqual(6L, Math.ClampEE(5L, 5L, 10L));
    Assert.AreEqual(1L, Math.ClampEE(-5L, 0L, 10L));
    Assert.AreEqual(9L, Math.ClampEE(15L, 0L, 10L));

    Assert.AreEqual(5L, Math.ClampIE(5L, 0L, 10L));
    Assert.AreEqual(4L, Math.ClampIE(5L, 0L, 5L));
    Assert.AreEqual(5L, Math.ClampIE(5L, 5L, 10L));
    Assert.AreEqual(0L, Math.ClampIE(-5L, 0L, 10L));
    Assert.AreEqual(9L, Math.ClampIE(15L, 0L, 10L));

    Assert.AreEqual(5L, Math.ClampEI(5L, 0L, 10L));
    Assert.AreEqual(5L, Math.ClampEI(5L, 0L, 5L));
    Assert.AreEqual(6L, Math.ClampEI(5L, 5L, 10L));
    Assert.AreEqual(1L, Math.ClampEI(-5, 0L, 10L));
    Assert.AreEqual(10L, Math.ClampEI(15L, 0L, 10L));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for clamping ulong values.", TestOf = typeof(Math))]
  public void TestClampULong()
  {
    Assert.AreEqual((ulong)5, Math.ClampII((ulong)5, (ulong)0, (ulong)10));
    Assert.AreEqual((ulong)5, Math.ClampII((ulong)5, (ulong)0, (ulong)5));
    Assert.AreEqual((ulong)5, Math.ClampII((ulong)5, (ulong)5, (ulong)10));
    Assert.AreEqual((ulong)10, Math.ClampII((ulong)15, (ulong)0, (ulong)10));

    Assert.AreEqual((ulong)5, Math.ClampEE((ulong)5, (ulong)0, (ulong)10));
    Assert.AreEqual((ulong)4, Math.ClampEE((ulong)5, (ulong)0, (ulong)5));
    Assert.AreEqual((ulong)6, Math.ClampEE((ulong)5, (ulong)5, (ulong)10));
    Assert.AreEqual((ulong)9, Math.ClampEE((ulong)15, (ulong)0, (ulong)10));

    Assert.AreEqual((ulong)5, Math.ClampIE((ulong)5, (ulong)0, (ulong)10));
    Assert.AreEqual((ulong)4, Math.ClampIE((ulong)5, (ulong)0, (ulong)5));
    Assert.AreEqual((ulong)5, Math.ClampIE((ulong)5, (ulong)5, (ulong)10));
    Assert.AreEqual((ulong)9, Math.ClampIE((ulong)15, (ulong)0, (ulong)10));

    Assert.AreEqual((ulong)5, Math.ClampEI((ulong)5, (ulong)0, (ulong)10));
    Assert.AreEqual((ulong)5, Math.ClampEI((ulong)5, (ulong)0, (ulong)5));
    Assert.AreEqual((ulong)6, Math.ClampEI((ulong)5, (ulong)5, (ulong)10));
    Assert.AreEqual((ulong)10, Math.ClampEI((ulong)15, (ulong)0, (ulong)10));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for clamping float values.", TestOf = typeof(Math))]
  public void TestClampFloat()
  {
    Assert.AreEqual(5.5f, Math.ClampII(5.5f, 0.5f, 10.5f));
    Assert.AreEqual(5.5f, Math.ClampII(5.5f, 0.5f, 5.5f));
    Assert.AreEqual(5.5f, Math.ClampII(5.5f, 5.5f, 10.5f));
    Assert.AreEqual(0.5f, Math.ClampII(-5, 0.5f, 10.5f));
    Assert.AreEqual(10.5f, Math.ClampII(15.5f, 0.5f, 10.5f));

    Assert.AreEqual(5.5f, Math.ClampEE(5.5f, 0.5f, 10.5f));
    Assert.AreEqual(5.5f - float.Epsilon, Math.ClampEE(5.5f, 0.5f, 5.5f));
    Assert.AreEqual(5.5f + float.Epsilon, Math.ClampEE(5.5f, 5.5f, 10.5f));
    Assert.AreEqual(0.5f + float.Epsilon, Math.ClampEE(-5.5f, 0.5f, 10.5f));
    Assert.AreEqual(10.5f - float.Epsilon, Math.ClampEE(15.5f, 0.5f, 10.5f));

    Assert.AreEqual(5.5f, Math.ClampIE(5.5f, 0.5f, 10.5f));
    Assert.AreEqual(5.5f - float.Epsilon, Math.ClampIE(5.5f, 0.5f, 5.5f));
    Assert.AreEqual(5.5f, Math.ClampIE(5.5f, 5.5f, 10.5f));
    Assert.AreEqual(0.5f, Math.ClampIE(-5.5f, 0.5f, 10.5f));
    Assert.AreEqual(10.5f - float.Epsilon, Math.ClampIE(15.5f, 0.5f, 10.5f));

    Assert.AreEqual(5.5f, Math.ClampEI(5.5f, 0.5f, 10.5f));
    Assert.AreEqual(5.5f, Math.ClampEI(5.5f, 0.5f, 5.5f));
    Assert.AreEqual(5.5f + float.Epsilon, Math.ClampEI(5.5f, 5.5f, 10.5f));
    Assert.AreEqual(0.5f + float.Epsilon, Math.ClampEI(-5, 0.5f, 10.5f));
    Assert.AreEqual(10.5f, Math.ClampEI(15.5f, 0.5f, 10.5f));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for clamping double values.", TestOf = typeof(Math))]
  public void TestClampDouble()
  {
    Assert.AreEqual(5.5, Math.ClampII(5.5, 0.5, 10.5));
    Assert.AreEqual(5.5, Math.ClampII(5.5, 0.5, 5.5));
    Assert.AreEqual(5.5, Math.ClampII(5.5, 5.5, 10.5));
    Assert.AreEqual(0.5, Math.ClampII(-5, 0.5, 10.5));
    Assert.AreEqual(10.5, Math.ClampII(15.5, 0.5, 10.5));

    Assert.AreEqual(5.5, Math.ClampEE(5.5, 0.5, 10.5));
    Assert.AreEqual(5.5 - double.Epsilon, Math.ClampEE(5.5, 0.5, 5.5));
    Assert.AreEqual(5.5 + double.Epsilon, Math.ClampEE(5.5, 5.5, 10.5));
    Assert.AreEqual(0.5 + double.Epsilon, Math.ClampEE(-5.5, 0.5, 10.5));
    Assert.AreEqual(10.5 - double.Epsilon, Math.ClampEE(15.5, 0.5, 10.5));

    Assert.AreEqual(5.5, Math.ClampIE(5.5, 0.5, 10.5));
    Assert.AreEqual(5.5 - double.Epsilon, Math.ClampIE(5.5, 0.5, 5.5));
    Assert.AreEqual(5.5, Math.ClampIE(5.5, 5.5, 10.5));
    Assert.AreEqual(0.5, Math.ClampIE(-5.5, 0.5, 10.5));
    Assert.AreEqual(10.5 - double.Epsilon, Math.ClampIE(15.5, 0.5, 10.5));

    Assert.AreEqual(5.5, Math.ClampEI(5.5, 0.5, 10.5));
    Assert.AreEqual(5.5, Math.ClampEI(5.5, 0.5, 5.5));
    Assert.AreEqual(5.5 + double.Epsilon, Math.ClampEI(5.5, 5.5, 10.5));
    Assert.AreEqual(0.5 + double.Epsilon, Math.ClampEI(-5, 0.5, 10.5));
    Assert.AreEqual(10.5, Math.ClampEI(15.5, 0.5, 10.5));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for clamping decimal values.", TestOf = typeof(Math))]
  public void TestClampDecimal()
  {
    Assert.AreEqual((decimal)5.5, Math.ClampII((decimal)5.5, (decimal)0.5, (decimal)10.5));
    Assert.AreEqual((decimal)5.5, Math.ClampII((decimal)5.5, (decimal)0.5, (decimal)5.5));
    Assert.AreEqual((decimal)5.5, Math.ClampII((decimal)5.5, (decimal)5.5, (decimal)10.5));
    Assert.AreEqual((decimal)0.5, Math.ClampII((decimal)-5, (decimal)0.5, (decimal)10.5));
    Assert.AreEqual((decimal)10.5, Math.ClampII((decimal)15.5, (decimal)0.5, (decimal)10.5));

    Assert.AreEqual((decimal)5.5, Math.ClampEE((decimal)5.5, (decimal)0.5, (decimal)10.5));
    Assert.AreEqual((decimal)5.5 - (decimal)double.Epsilon, Math.ClampEE((decimal)5.5, (decimal)0.5, (decimal)5.5));
    Assert.AreEqual((decimal)5.5 + (decimal)double.Epsilon, Math.ClampEE((decimal)5.5, (decimal)5.5, (decimal)10.5));
    Assert.AreEqual((decimal)0.5 + (decimal)double.Epsilon, Math.ClampEE((decimal)-5.5, (decimal)0.5, (decimal)10.5));
    Assert.AreEqual((decimal)10.5 - (decimal)double.Epsilon, Math.ClampEE((decimal)15.5, (decimal)0.5, (decimal)10.5));

    Assert.AreEqual((decimal)5.5, Math.ClampIE((decimal)5.5, (decimal)0.5, (decimal)10.5));
    Assert.AreEqual((decimal)5.5 - (decimal)double.Epsilon, Math.ClampIE((decimal)5.5, (decimal)0.5, (decimal)5.5));
    Assert.AreEqual((decimal)5.5, Math.ClampIE((decimal)5.5, (decimal)5.5, (decimal)10.5));
    Assert.AreEqual((decimal)0.5, Math.ClampIE((decimal)-5.5, (decimal)0.5, (decimal)10.5));
    Assert.AreEqual((decimal)10.5 - (decimal)double.Epsilon, Math.ClampIE((decimal)15.5, (decimal)0.5, (decimal)10.5));

    Assert.AreEqual((decimal)5.5, Math.ClampEI((decimal)5.5, (decimal)0.5, (decimal)10.5));
    Assert.AreEqual((decimal)5.5, Math.ClampEI((decimal)5.5, (decimal)0.5, (decimal)5.5));
    Assert.AreEqual((decimal)5.5 + (decimal)double.Epsilon, Math.ClampEI((decimal)5.5, (decimal)5.5, (decimal)10.5));
    Assert.AreEqual((decimal)0.5 + (decimal)double.Epsilon, Math.ClampEI((decimal)-5, (decimal)0.5, (decimal)10.5));
    Assert.AreEqual((decimal)10.5, Math.ClampEI((decimal)15.5, (decimal)0.5, (decimal)10.5));
  }

  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for clamping char values.", TestOf = typeof(Math))]
  public void TestClampChar()
  {
    Assert.AreEqual('5', Math.ClampII('5', '0', '8'));
    Assert.AreEqual('5', Math.ClampII('5', '0', '5'));
    Assert.AreEqual('5', Math.ClampII('5', '5', '8'));
    Assert.AreEqual('8', Math.ClampII('9', '0', '8'));

    Assert.AreEqual('5', Math.ClampEE('5', '0', '8'));
    Assert.AreEqual('4', Math.ClampEE('5', '0', '5'));
    Assert.AreEqual('6', Math.ClampEE('5', '5', '8'));
    Assert.AreEqual('7', Math.ClampEE('9', '0', '8'));

    Assert.AreEqual('5', Math.ClampIE('5', '0', '8'));
    Assert.AreEqual('4', Math.ClampIE('5', '0', '5'));
    Assert.AreEqual('5', Math.ClampIE('5', '5', '8'));
    Assert.AreEqual('7', Math.ClampIE('9', '0', '8'));

    Assert.AreEqual('5', Math.ClampEI('5', '0', '8'));
    Assert.AreEqual('5', Math.ClampEI('5', '0', '5'));
    Assert.AreEqual('6', Math.ClampEI('5', '5', '8'));
    Assert.AreEqual('8', Math.ClampEI('9', '0', '8'));
  }
}