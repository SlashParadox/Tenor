using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Tenor.TestTools;
using Tenor.Tools;
using UnityEngine;
using UnityEngine.TestTools;

public class UT_EnumTools
{
  private enum EnumUnitTest
  {
    TestValue0,
    TestValue1,
    TestValue2,
    TestValue3,
    TestValue4,
    MaxSize,
  }

  [TestCategory("Tenor", "Tools", "Enum")]
  [Test(Author = "Craig Williams", Description = "A test for getting the number of values in an enum.", TestOf = typeof(Enums))]
  public void TestEnumValueCount()
  {
    Assert.AreEqual((int)EnumUnitTest.MaxSize + 1, Enums.GetValueCount<EnumUnitTest>(), "The wrong value count was returned.");
    Assert.AreEqual((int)EnumUnitTest.MaxSize + 1, Enums.GetValueCount(typeof(EnumUnitTest)), "The wrong value count was returned.");
  }

  [TestCategory("Tenor", "Tools", "Enum")]
  [Test(Author = "Craig Williams", Description = "A test for getting all the values of an enum into an IList.", TestOf = typeof(Enums))]
  public void TestGettingEnumValues()
  {
    EnumUnitTest[] testArrayA = Enums.GetEnumValueArray<EnumUnitTest>();
    System.Array testArrayB = Enums.GetEnumValueArray(typeof(EnumUnitTest));
    List<EnumUnitTest> testListC = Enums.GetEnumValueList<EnumUnitTest>();

    for (int i = 0; i < (int)EnumUnitTest.MaxSize; i++)
    {
      Assert.AreEqual(((EnumUnitTest)i), testArrayA[i], "{0} did not return the right value at index {1}", nameof(testArrayA), i);
      Assert.AreEqual(((EnumUnitTest)i), testArrayB.GetValue(i), "{0} did not return the right value at index {1}", nameof(testArrayB), i);
      Assert.AreEqual(((EnumUnitTest)i), testListC[i], "{0} did not return the right value at index {1}", nameof(testListC), i);
    }
  }
}