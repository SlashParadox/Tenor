using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Tenor.Tools.Text;
using Tenor.Tools.Collection;
using Tenor.Tools.Debug;
using UnityEngine;
using Tenor.TestTools;

public class UT_IListTools
{
  [TestCategory("Tenor", "Tools", "Collection", "IList")]
  [Test(Author = "Craig Williams", Description = "A test for if an IList is null or empty.", TestOf = typeof(ILists))]
  public void TestNullOrEmpty()
  {
    List<int> testListA = null;
    IList testListB = null;

    Assert.IsNull(testListA, "{0} is not null.", nameof(testListA));
    Assert.IsTrue(testListA.IsEmptyOrNull(), "{0} is not empty, or not null.", nameof(testListA));
    Assert.IsTrue(testListA.IsEmptyOrNullNG(), "{0} is not empty, or not null.", nameof(testListA));
    
    Assert.IsNull(testListB, "{0} is not null.", nameof(testListB));
    Assert.IsTrue(testListB.IsEmptyOrNullNG(), "{0} is not empty, or not null.", nameof(testListB));

    Assertion.ThrowsAny(() => testListA.IsEmpty(), "{0} did not throw an exception.", nameof(testListA));
    Assertion.ThrowsAny(() => testListA.IsEmptyNG(), "{0} did not throw an exception.", nameof(testListA));
    Assertion.ThrowsAny(() => testListB.IsEmptyNG(), "{0} did not throw an exception.", nameof(testListB));

    testListA = new List<int>();
    Assert.IsNotNull(testListA, "'testListA' is null.");
    Assert.IsTrue(testListA.IsEmpty(), "'testListA' is not empty.");
    Assert.IsTrue(testListA.IsEmptyNG(), "'testListA' is not empty. [Via Generic]");
    Assert.IsTrue(testListA.IsEmptyOrNull(), "'testListA' is not empty, or not null.");
    Assert.IsTrue(testListA.IsEmptyOrNullNG(), "'testListA' is not empty, or not null. [Via Generic]");

    testListA.Add(1);
    Assert.IsFalse(testListA.IsEmpty(), "'testListA' is empty.");
    Assert.IsFalse(testListA.IsEmptyNG(), "'testListA' is empty. [Via Generic]");
    Assert.IsFalse(testListA.IsEmptyOrNull(), "'testListA' is empty. [Via 'IsEmptyOrNull'");
    Assert.IsFalse(testListA.IsEmptyOrNullNG(), "'testListA' is empty. [Via 'IsEmptyOrNullGeneric']");

    testListB = new int[5];
    Assert.IsFalse(testListB.IsEmptyNG(), "'testListB' is empty. [Via Generic]");
    Assert.IsFalse(testListB.IsEmptyOrNullNG(), "'testListA' is empty. [Via 'IsEmptyOrNullGeneric']");

    testListA.Clear();
    Assert.IsTrue(testListA.IsEmpty(), "'testListA' is not empty.");
    Assert.IsTrue(testListA.IsEmptyNG(), "'testListA' is not empty. [Via Generic]");
    Assert.IsTrue(testListA.IsEmptyOrNull(), "'testListA' is not empty, or not null.");
    Assert.IsTrue(testListA.IsEmptyOrNullNG(), "'testListA' is not empty, or not null. [Via Generic]");
  }

  [TestCategory("Tenor", "Tools", "Collection", "IList")]
  [Test(Author = "Craig Williams", Description = "A test for valid indexes of an IList.", TestOf = typeof(ILists))]
  public void TestValidIndex()
  {
    int arraySize = 200;
    int[] testArrayA = new int[arraySize];

    Assert.IsNotNull(testArrayA, "{0} is null.", nameof(testArrayA));

    for (int i = 0; i < arraySize; i++)
    {
      Assert.IsTrue(testArrayA.IsValidIndex(i), "Index {0} is not valid in an array of size {1}.", i, arraySize);
      Assert.IsTrue(testArrayA.IsValidIndexNG(i), "Index {0} is not valid in an array of size {1}.", i, arraySize);
    }
      

    Assert.IsFalse(testArrayA.IsValidIndex(arraySize), "Index {0} is valid in an array of size {0}.", arraySize);
    Assert.IsFalse(testArrayA.IsValidIndexNG(arraySize), "Index {0} is valid in an array of size {0}.", arraySize);
    Assert.IsFalse(testArrayA.IsValidIndex(-1), "Index -1 is not valid in an array of size {0}.", arraySize);
    Assert.IsFalse(testArrayA.IsValidIndexNG(-1), "Index -1 is not valid in an array of size {0}.", arraySize);
  }

  [TestCategory("Tenor", "Tools", "Collection", "IList")]
  [Test(Author = "Craig Williams", Description = "A test for getting the last index of an IList.", TestOf = typeof(ILists))]
  public void TestLastIndex()
  {
    List<int> testListA = new List<int>(5);

    Assert.IsNotNull(testListA, "{0} is null.", nameof(testListA));
    Assert.AreEqual(ILists.BadIndex, testListA.LastIndex(), "{0} has a valid last index when empty.", nameof(testListA));
    Assert.AreEqual(ILists.BadIndex, testListA.LastIndexNG(), "{0} has a valid last index when empty.", nameof(testListA));

    testListA.Add(5);
    Assert.AreEqual(0, testListA.LastIndex(), "{0}'s last valid index is not 0 with a size of 1.", nameof(testListA));
    Assert.AreEqual(0, testListA.LastIndexNG(), "{0}'s last valid index is not 0 with a size of 1.", nameof(testListA));

    testListA.RemoveAt(0);
    Assert.AreEqual(ILists.BadIndex, testListA.LastIndex(), "{0} has a valid last index when empty.", nameof(testListA));
    Assert.AreEqual(ILists.BadIndex, testListA.LastIndexNG(), "{0} has a valid last index when empty.", nameof(testListA));

    int arraySize = 5000;
    for (int i = 0; i < arraySize; i++)
      testListA.Add(i);

    Assert.AreEqual(arraySize - 1, testListA.LastIndex(), "{0}'s last valid index is not {1} with a size of {2}.", nameof(testListA), arraySize - 1, arraySize);
    Assert.AreEqual(arraySize - 1, testListA.LastIndexNG(), "{0}'s last valid index is not {1} with a size of {2}.", nameof(testListA), arraySize - 1, arraySize);
  }

  [TestCategory("Tenor", "Tools", "Collection", "IList")]
  [Test(Author = "Craig Williams", Description = "A test for getting the last element in an IList.", TestOf = typeof(ILists))]
  public void TestLastElement()
  {
    List<int> testListA = new List<int>(5);

    Assert.IsNotNull(testListA, "{0} is null.", nameof(testListA));
    Assert.AreEqual(ILists.BadIndex, testListA.LastIndex(), "{0} has a valid last index when empty.", nameof(testListA));
    Assert.AreEqual(ILists.BadIndex, testListA.LastIndexNG(), "{0} has a valid last index when empty.", nameof(testListA));

    Assert.IsFalse(testListA.LastElement(out int foundValue), "{0} found a value when empty: {1}", nameof(testListA), foundValue);
    Assert.IsFalse(testListA.LastElementNG(out object foundObject), "{0} found an object when empty: {1}", nameof(testListA), foundObject);

    foundValue = testListA.LastElement();
    foundObject = testListA.LastElementNG();
    Assert.AreEqual(default(int), foundValue, "{0} found a value when empty.", nameof(testListA));
    Assert.AreEqual(default, foundObject, "{0} found a value when empty.", nameof(testListA));

    int arraySize = 5000;
    for (int i = 0; i < arraySize; i++)
    {
      testListA.Add(i + 1);
      foundValue = testListA.LastElement();
      foundObject = testListA.LastElementNG();
      Assert.AreEqual(i + 1, foundValue, "{0} found a different value than expected.", nameof(testListA));
      Assert.AreEqual(i + 1, foundObject, "{0} found a value when empty.", nameof(testListA));
    }
      
  }

  [TestCategory("Tenor", "Tools", "Collection", "IList")]
  [Test(Author = "Craig Williams", Description = "A test for printing an IList's contents.", TestOf = typeof(ILists))]
  public void TestPrintElements()
  {
    List<int> testListA = new List<int>();

    Assert.IsNotNull(testListA, "{0} is null.", nameof(testListA));
    Assert.AreEqual(string.Empty, testListA.Print(), "{0} printed out a non-empty string.", nameof(testListA));
    Assert.AreEqual(string.Empty, testListA.PrintNG(), "{0} printed out a non-empty string.", nameof(testListA));

    testListA.Add(5);
    Assert.AreEqual("[5]", testListA.Print(), "{0} printed out an incorrect string.", nameof(testListA));
    Assert.AreEqual("[5]", testListA.PrintNG(), "{0} printed out an incorrect string.", nameof(testListA));

    testListA.Add(3);
    Assert.AreEqual("[5], [3]", testListA.Print(), "{0} printed out an incorrect string.", nameof(testListA));
    Assert.AreEqual("[5], [3]", testListA.PrintNG(), "{0} printed out an incorrect string.", nameof(testListA));

    testListA.Add(6);
    testListA.Add(1);
    testListA.Add(46);
    Assert.AreEqual("[5], [3], [6], [1], [46]", testListA.Print(), "{0} printed out an incorrect string.", nameof(testListA));
    Assert.AreEqual("[5], [3], [6], [1], [46]", testListA.PrintNG(), "{0} printed out an incorrect string.", nameof(testListA));
  }

  [TestCategory("Tenor", "Tools", "Collection", "IList")]
  [Test(Author = "Craig Williams", Description = "A test for replacing an IList's contents.", TestOf = typeof(ILists))]
  public void TestReplacement()
  {
    List<int> testListA = new List<int>();
    Assert.IsNotNull(testListA, "{0} is null.", nameof(testListA));

    int arraySize = 10000;
    int arrayValue = 10;
    int replacementValue = 20;

    for (int i = 0; i < arraySize; i++)
      testListA.Add(arrayValue);

    Assert.AreEqual(arraySize, testListA.Count, "{0} is not the right size after initialization.", nameof(testListA));

    int index = testListA.Replace(arrayValue, replacementValue);
    Assert.AreEqual(0, index, "Wrong index replaced.");
    Assert.AreEqual(20, testListA[0], "Replacement has failed.");

    index = testListA.ReplaceNG(arrayValue + replacementValue, replacementValue);
    Assert.AreEqual(ILists.BadIndex, index, "Replacement made when it should have failed.");

    bool result = testListA.ReplaceRange(arrayValue, replacementValue, 0);
    Assert.IsTrue(result, "No replacements made.");

    for (int i = 0; i < arraySize; i++)
    {
      Assert.AreEqual(replacementValue, testListA[i], "Replacement has failed at index {0}.", i);
      testListA[i] = arrayValue;
    }

    int start = 50;
    int end = 100;
    result = testListA.ReplaceRange(arrayValue, replacementValue, start, end);
    Assert.IsTrue(result, "No replacements made.");

    for (int i = start; i < end; i++)
    {
      Assert.AreEqual(replacementValue, testListA[i], "Replacement has failed at index {0}.", i);
      testListA[i] = arrayValue;
    }

    result = testListA.ReplaceRangeNG(arrayValue, replacementValue, start);
    Assert.IsTrue(result, "No replacements made.");

    for (int i = start; i < arraySize; i++)
    {
      Assert.AreEqual(replacementValue, testListA[i], "Replacement has failed at index {0}.", i);
      testListA[i] = arrayValue;
    }

    int skip = 5;
    result = testListA.ReplaceEveryOther(arrayValue, replacementValue, skip);
    Assert.IsTrue(result, "No replacements made.");

    for (int i = skip - 1; i < arraySize; i += skip)
    {
      Assert.AreEqual(replacementValue, testListA[i], "Replacement has failed at index {0}.", i);

      if (i > 0)
        Assert.AreNotEqual(replacementValue, testListA[i - 1], "Replacement wrongly made at index {0}.", i);
      testListA[i] = arrayValue;
    }

    skip = 20;
    result = testListA.ReplaceEveryOtherNG(arrayValue, replacementValue, start, skip);
    Assert.IsTrue(result, "No replacements made.");

    for (int i = start + skip - 1; i < arraySize; i += skip)
    {
      Assert.AreEqual(replacementValue, testListA[i], "Replacement has failed at index {0}.", i);

      if (i > 0)
        Assert.AreNotEqual(replacementValue, testListA[i - 1], "Replacement wrongly made at index {0}.", i);
      testListA[i] = arrayValue;
    }

    result = testListA.ReplaceEveryOther(arrayValue, replacementValue, start, end, skip);
    Assert.IsTrue(result, "No replacements made.");

    for (int i = start + skip - 1; i < end; i += skip)
    {
      Assert.AreEqual(replacementValue, testListA[i], "Replacement has failed at index {0}.", i);

      if (i > 0)
        Assert.AreNotEqual(replacementValue, testListA[i - 1], "Replacement wrongly made at index {0}.", i);
      testListA[i] = arrayValue;
    }
  }

  [TestCategory("Tenor", "Tools", "Collection", "IList")]
  [Test(Author = "Craig Williams", Description = "A test for filling an IList's contents.", TestOf = typeof(ILists))]
  public void TestFill()
  {
    List<int> testListA = new List<int>();
    Assert.IsNotNull(testListA, "{0} is null.", nameof(testListA));

    int arraySize = 10000;
    int arrayValue = 10;
    int replacementValue = 20;

    for (int i = 0; i < arraySize; i++)
      testListA.Add(arrayValue);

    Assert.AreEqual(arraySize, testListA.Count, "{0} is not the right size after initialization.", nameof(testListA));

    bool result = testListA.Fill(replacementValue);
    Assert.IsTrue(result, "No replacements made.");

    for (int i = 0; i < arraySize; i++)
    {
      Assert.AreEqual(replacementValue, testListA[i], "Replacement has failed at index {0}.", i);
      testListA[i] = arrayValue;
    }

    result = testListA.FillNG(replacementValue);
    Assert.IsTrue(result, "No replacements made.");

    for (int i = 0; i < arraySize; i++)
    {
      Assert.AreEqual(replacementValue, testListA[i], "Replacement has failed at index {0}.", i);
      testListA[i] = arrayValue;
    }

    int start = 50;
    int end = 100;
    result = testListA.Fill(replacementValue, start, end);
    Assert.IsTrue(result, "No replacements made.");

    for (int i = start; i < end; i++)
    {
      Assert.AreEqual(replacementValue, testListA[i], "Replacement has failed at index {0}.", i);
      testListA[i] = arrayValue;
    }

    result = testListA.FillNG(replacementValue, start);
    Assert.IsTrue(result, "No replacements made.");

    for (int i = start; i < arraySize; i++)
    {
      Assert.AreEqual(replacementValue, testListA[i], "Replacement has failed at index {0}.", i);
      testListA[i] = arrayValue;
    }

    int skip = 5;
    result = testListA.FillEveryOther(replacementValue, skip);
    Assert.IsTrue(result, "No replacements made.");

    for (int i = 0; i < arraySize; i += skip)
    {
      Assert.AreEqual(replacementValue, testListA[i], "Replacement has failed at index {0}.", i);

      if (i > 0)
        Assert.AreNotEqual(replacementValue, testListA[i - 1], "Replacement wrongly made at index {0}.", i);
      testListA[i] = arrayValue;
    }

    skip = 20;
    result = testListA.FillEveryOtherNG(replacementValue, start, skip);
    Assert.IsTrue(result, "No replacements made.");

    for (int i = start; i < arraySize; i += skip)
    {
      Assert.AreEqual(replacementValue, testListA[i], "Replacement has failed at index {0}.", i);

      if (i > 0)
        Assert.AreNotEqual(replacementValue, testListA[i - 1], "Replacement wrongly made at index {0}.", i);
      testListA[i] = arrayValue;
    }

    result = testListA.FillEveryOther(replacementValue, start, end, skip);
    Assert.IsTrue(result, "No replacements made.");

    for (int i = start; i < end; i += skip)
    {
      Assert.AreEqual(replacementValue, testListA[i], "Replacement has failed at index {0}.", i);

      if (i > 0)
        Assert.AreNotEqual(replacementValue, testListA[i - 1], "Replacement wrongly made at index {0}.", i);
      testListA[i] = arrayValue;
    }
  }
}
