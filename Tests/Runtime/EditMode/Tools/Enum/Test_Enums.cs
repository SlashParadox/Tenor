/**************************************************************************************************/
/*!
\file   Test_Enums.cs
\author Craig Williams
\par    Last Updated
        2021-05-21
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A unit test file for the Enum tools.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System;
using System.Collections.Generic;
using CodeParadox.Tenor.Tools;
using NUnit.Framework;

namespace CodeParadox.Tenor.Tests.Runtime.EditMode
{
  /************************************************************************************************/
  /// <summary>
  /// A test class for <see cref="Tenor.Tools.Enums"/>.
  /// </summary>
  [Explicit] // Comment this to allow this to be run automatically.
  public sealed class Test_Enums
  {
    /// <summary>
    /// A test enumeration for the purposes of this test script.
    /// </summary>
    private enum TestEnumeration
    {
      TESTA,
      TESTB,
      TESTC,
      TESTD,
      /// <summary>A final value for the enum's size. THIS MUST ALWAYS BE THE LAST VALUE.</summary>
      SIZE,
    }

    /// <summary>
    /// A test for <see cref="Enums.GetValueCount{TEnum}"/>.
    /// </summary>
    [Test(TestOf = typeof(Enums))]
    public void ValueCount_Generic_ReturnsEquality()
    {
      // Make sure the count returned is exact.
      int count = Enums.GetValueCount<TestEnumeration>();
      Assert.AreEqual((int)TestEnumeration.SIZE + 1, count);
    }

    /// <summary>
    /// A test for <see cref="Enums.GetValueCount(Type)"/>.
    /// </summary>
    [Test(TestOf = typeof(Enums))]
    public void ValueCount_Type_ReturnsEquality()
    {
      // Make sure the count returned is exact.
      int count = Enums.GetValueCount(typeof(TestEnumeration));
      Assert.AreEqual((int)TestEnumeration.SIZE + 1, count);
    }

    /// <summary>
    /// A test for <see cref="Enums.GetValueArray{TEnum}"/>.
    /// </summary>
    [Test(TestOf = typeof(Enums))]
    public void ValueArray_Generic_ReturnsCorrectness()
    {
      // Get the expected and actual arrays of values.
      Array expected = Enum.GetValues(typeof(TestEnumeration));
      TestEnumeration[] actual = Enums.GetValueArray<TestEnumeration>();

      // Check that the value counts are equal.
      int count = expected.Length;
      Assert.AreEqual(count, actual.Length);

      // Compare every value to make sure everything is correct.
      for (int i = 0; i < count; i++)
        Assert.AreEqual((TestEnumeration)expected.GetValue(i), actual[i]);
    }

    /// <summary>
    /// A test for <see cref="Enums.GetValueArray(Type)"/>.
    /// </summary>
    [Test(TestOf = typeof(Enums))]
    public void ValueArray_Type_ReturnsCorrectness()
    {
      // Get the expected and actual arrays of values.
      Array expected = Enum.GetValues(typeof(TestEnumeration));
      Array actual = Enums.GetValueArray(typeof(TestEnumeration));

      // Check that the value counts are equal.
      int count = expected.Length;
      Assert.AreEqual(count, actual.Length);

      // Compare every value to make sure everything is correct.
      for (int i = 0; i < count; i++)
        Assert.AreEqual((TestEnumeration)expected.GetValue(i), (TestEnumeration)actual.GetValue(i));
    }

    /// <summary>
    /// A test for <see cref="Enums.GetValueList{TEnum}"/>.
    /// </summary>
    [Test(TestOf = typeof(Enums))]
    public void ValueList_ReturnsCorrectness()
    {
      // Get the expected and actual arrays of values.
      Array expected = Enum.GetValues(typeof(TestEnumeration));
      List<TestEnumeration> actual = Enums.GetValueList<TestEnumeration>();

      // Check that the value counts are equal.
      int count = expected.Length;
      Assert.AreEqual(count, actual.Count);

      // Compare every value to make sure everything is correct.
      for (int i = 0; i < count; i++)
        Assert.AreEqual((TestEnumeration)expected.GetValue(i), actual[i]);
    }
  }
  /************************************************************************************************/
}