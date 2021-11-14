/**************************************************************************************************/
/*!
\file   Test_ILists.cs
\author Craig Williams
\par    Last Updated
        2021-05-20
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A unit test file for the IList tools.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using SlashParadox.Tenor.Tools;
using NUnit.Framework;

namespace SlashParadox.Tenor.Tests.Runtime.EditMode
{
  /************************************************************************************************/
  /// <summary>
  /// A test class for <see cref="Tenor.Tools.ILists"/>.
  /// </summary>
  [Explicit] // Comment this to allow this to be run automatically.
  public sealed class Test_ILists
  {
    /// <summary>The general amount of cases to generate.</summary>
    private const int StandardMaxCases = 20;

    /// <summary>
    /// A test for <see cref="ILists.IsEmpty{T}(IList{T})"/>, <see cref="ILists.IsEmptyNG(IList)"/>,
    /// <see cref="ILists.IsNotEmpty{T}(IList{T})"/>, and <see cref="ILists.IsNotEmptyNG(IList)"/>,
    /// specifically on arrays.
    /// </summary>
    /// <param name="testSize">The test size of the array.</param>
    [Test(TestOf = typeof(ILists))]
    public void Empty_Array_ReturnsEmptiness([Range(1, StandardMaxCases)] int testSize)
    {
      // Create a new array. Arrays can never be empty when not null.
      int[] array = new int[testSize];
      Assert.IsFalse(array.IsEmpty());
      Assert.IsFalse(array.IsEmptyNG());
      Assert.IsTrue(array.IsNotEmpty());
      Assert.IsTrue(array.IsNotEmptyNG());
    }

    /// <summary>
    /// A test for <see cref="ILists.IsEmpty{T}(IList{T})"/>, <see cref="ILists.IsEmptyNG(IList)"/>,
    /// <see cref="ILists.IsNotEmpty{T}(IList{T})"/>, and <see cref="ILists.IsNotEmptyNG(IList)"/>,
    /// specifically on <see cref="List{T}"/>s.
    /// </summary>
    /// <param name="testSize">The test size of the <see cref="List{T}"/>.</param>
    [Test(TestOf = typeof(ILists))]
    public void Empty_List_ReturnsEmptiness([Range(1, StandardMaxCases)] int testSize)
    {
      // Create a new list. By default, lists are empty when created.
      List<int> list = new List<int>();
      Assert.IsTrue(list.IsEmpty());
      Assert.IsTrue(list.IsEmptyNG());
      Assert.IsFalse(list.IsNotEmpty());
      Assert.IsFalse(list.IsNotEmptyNG());

      // Add in values, and test emptiness again.
      for (int i = 0; i < testSize; i++)
        list.Add(i);

      Assert.IsFalse(list.IsEmpty());
      Assert.IsFalse(list.IsEmptyNG());
      Assert.IsTrue(list.IsNotEmpty());
      Assert.IsTrue(list.IsNotEmptyNG());

      // Create another list. By default, lists are empty when given just a capacity.
      list = new List<int>(testSize);
      Assert.IsTrue(list.IsEmpty());
      Assert.IsTrue(list.IsEmptyNG());
      Assert.IsFalse(list.IsNotEmpty());
      Assert.IsFalse(list.IsNotEmptyNG());

      // Add in values, and test emptiness again.
      for (int i = 0; i < testSize; i++)
        list.Add(i);

      Assert.IsFalse(list.IsEmpty());
      Assert.IsFalse(list.IsEmptyNG());
      Assert.IsTrue(list.IsNotEmpty());
      Assert.IsTrue(list.IsNotEmptyNG());
    }

    /// <summary>
    /// A test for <see cref="ILists.IsEmptyOrNull{T}(IList{T}){T}(IList{T})"/>,
    /// <see cref="ILists.IsEmptyOrNullNG(IList)"/>,
    /// <see cref="ILists.IsNotEmptyOrNull{T}(IList{T})"/>, and
    /// <see cref="ILists.IsNotEmptyOrNullNG(IList)"/>, specifically on null arrays.
    /// </summary>
    [Test(TestOf = typeof(ILists))]
    public void EmptyOrNull_NullArray_ReturnsEmptiness()
    {
      // Create a new array. Arrays can never be empty, except when null.
      int[] array = null;
      Assert.IsTrue(array.IsEmptyOrNull());
      Assert.IsTrue(array.IsEmptyOrNullNG());
      Assert.IsFalse(array.IsNotEmptyOrNull());
      Assert.IsFalse(array.IsNotEmptyOrNullNG());
    }

    /// <summary>
    /// A test for <see cref="ILists.IsEmptyOrNull{T}(IList{T}){T}(IList{T})"/>,
    /// <see cref="ILists.IsEmptyOrNullNG(IList)"/>,
    /// <see cref="ILists.IsNotEmptyOrNull{T}(IList{T})"/>, and
    /// <see cref="ILists.IsNotEmptyOrNullNG(IList)"/>, specifically on arrays.
    /// </summary>
    /// <param name="testSize">The test size of the array.</param>
    [Test(TestOf = typeof(ILists))]
    public void EmptyOrNull_Array_ReturnsEmptiness([Range(1, StandardMaxCases)] int testSize)
    {
      // Test the array, filled with basic data.
      int[] array = new int[testSize];
      Assert.IsFalse(array.IsEmptyOrNull());
      Assert.IsFalse(array.IsEmptyOrNullNG());
      Assert.IsTrue(array.IsNotEmptyOrNull());
      Assert.IsTrue(array.IsNotEmptyOrNullNG());
    }

    /// <summary>
    /// A test for <see cref="ILists.IsEmptyOrNull{T}(IList{T}){T}(IList{T})"/>,
    /// <see cref="ILists.IsEmptyOrNullNG(IList)"/>,
    /// <see cref="ILists.IsNotEmptyOrNull{T}(IList{T})"/>, and
    /// <see cref="ILists.IsNotEmptyOrNullNG(IList)"/>, specifically on null <see cref="List{T}"/>s.
    /// </summary>
    [Test(TestOf = typeof(ILists))]
    public void EmptyOrNull_NullList_ReturnsEmptiness()
    {
      // Create a new null list, and test it.
      List<int> list = null;
      Assert.IsTrue(list.IsEmptyOrNull());
      Assert.IsTrue(list.IsEmptyOrNullNG());
      Assert.IsFalse(list.IsNotEmptyOrNull());
      Assert.IsFalse(list.IsNotEmptyOrNullNG());

      // Create a new list. By default, lists are empty when created.
      list = new List<int>();
      Assert.IsTrue(list.IsEmptyOrNull());
      Assert.IsTrue(list.IsEmptyOrNullNG());
      Assert.IsFalse(list.IsNotEmptyOrNull());
      Assert.IsFalse(list.IsNotEmptyOrNullNG());
    }

    /// <summary>
    /// A test for <see cref="ILists.IsEmptyOrNull{T}(IList{T}){T}(IList{T})"/>,
    /// <see cref="ILists.IsEmptyOrNullNG(IList)"/>,
    /// <see cref="ILists.IsNotEmptyOrNull{T}(IList{T})"/>, and
    /// <see cref="ILists.IsNotEmptyOrNullNG(IList)"/>, specifically on <see cref="List{T}"/>s.
    /// </summary>
    /// <param name="testSize">The test size of the <see cref="List{T}"/>.</param>
    [Test(TestOf = typeof(ILists))]
    public void EmptyOrNull_List_ReturnsEmptiness([Range(1, StandardMaxCases)] int testSize)
    {
      // Create another list. By default, lists are empty when given just a capacity.
      List<int> list = new List<int>(testSize);
      Assert.IsTrue(list.IsEmptyOrNull());
      Assert.IsTrue(list.IsEmptyOrNullNG());
      Assert.IsFalse(list.IsNotEmptyOrNull());
      Assert.IsFalse(list.IsNotEmptyOrNullNG());

      // Add in values, and test emptiness again.
      for (int i = 0; i < testSize; i++)
        list.Add(i);

      Assert.IsFalse(list.IsEmptyOrNull());
      Assert.IsFalse(list.IsEmptyOrNullNG());
      Assert.IsTrue(list.IsNotEmptyOrNull());
      Assert.IsTrue(list.IsNotEmptyOrNullNG());
    }

    /// <summary>
    /// A test for <see cref="ILists.IsValidIndex{T}(IList{T}, int)"/> and
    /// <see cref="ILists.IsValidIndexNG(IList, int)"/>, specifically for arrays.
    /// </summary>
    /// <param name="testSize">The test size of the array.</param>
    /// <param name="index">The test index to validate.</param>
    [Test(TestOf = typeof(ILists))]
    public void ValidIndex_Array_ReturnsValidity([Random(0, 100, StandardMaxCases)] int testSize,
                                                 [Random(-50, 150, StandardMaxCases)] int index)
    {
      // Create a new array, and test when null.
      int[] array = null;
      Assert.IsFalse(array.IsValidIndex(index));
      Assert.IsFalse(array.IsValidIndexNG(index));

      // From here on out, the array is not null, so determine if the index is valid at all.
      bool expectedValidity = index >= 0 && index < testSize;
      array = new int[testSize];

      // Test if we get our expected validity.
      Assert.AreEqual(expectedValidity, array.IsValidIndex(index));
      Assert.AreEqual(expectedValidity, array.IsValidIndexNG(index));
    }


    /// <summary>
    /// A test for <see cref="ILists.IsValidIndex{T}(IList{T}, int)"/> and
    /// <see cref="ILists.IsValidIndexNG(IList, int)"/>, specifically for <see cref="IList{T}"/>.
    /// </summary>
    /// <param name="testSize">The test size of the <see cref="IList{T}"/>.</param>
    /// <param name="index">The test index to validate.</param>
    [Test(TestOf = typeof(ILists))]
    public void ValidIndex_List_ReturnsValidity([Random(0, 100, StandardMaxCases)] int testSize,
                                                [Random(-50, 150, StandardMaxCases)] int index)
    {
      // Create a new list, and test when null.
      List<int> list = null;
      Assert.IsFalse(list.IsValidIndex(index));
      Assert.IsFalse(list.IsValidIndexNG(index));

      // From here on out, the list is not null, so determine if the index is valid at all.
      bool expectedValidity = index >= 0 && index < testSize;
      list = new List<int>(testSize);
      for (int i = 0; i < testSize; i++)
        list.Add(i);

      // Test if we get our expected validity.
      Assert.AreEqual(expectedValidity, list.IsValidIndex(index));
      Assert.AreEqual(expectedValidity, list.IsValidIndexNG(index));
    }

    /// <summary>
    /// A test for <see cref="ILists.LastIndex{T}(IList{T})"/>, and
    /// <see cref="ILists.LastIndexNG(IList)"/>, specifically on null and empty arrays.
    /// </summary>
    [Test(TestOf = typeof(ILists))]
    public void LastIndex_NullArray_ReturnsValidity()
    {
      // Create a null array, and test it.
      int[] array = null;
      Assert.AreEqual(ILists.InvalidIndex, array.LastIndex());
      Assert.AreEqual(ILists.InvalidIndex, array.LastIndexNG());

      // Create an empty array, and test it.
      array = new int[0];
      Assert.AreEqual(ILists.InvalidIndex, array.LastIndex());
      Assert.AreEqual(ILists.InvalidIndex, array.LastIndexNG());
    }

    /// <summary>
    /// A test for <see cref="ILists.LastIndex{T}(IList{T})"/>, and
    /// <see cref="ILists.LastIndexNG(IList)"/>, specifically on arrays.
    /// </summary>
    /// <param name="testSize">The test size of the array.</param>
    [Test(TestOf = typeof(ILists))]
    public void LastIndex_Array_ReturnsValidity([Range(1, StandardMaxCases)] int testSize)
    {
      // Create a full array, and test its last index.
      int[] array = new int[testSize];
      Assert.AreEqual(array.Length - 1, array.LastIndex());
      Assert.AreEqual(array.Length - 1, array.LastIndexNG());

      // Make sure that the last index can actually be used.
      Assert.DoesNotThrow(() => array[array.LastIndex()] = 1);
      Assert.DoesNotThrow(() => array[array.LastIndexNG()] = 1);
    }

    /// <summary>
    /// A test for <see cref="ILists.LastIndex{T}(IList{T})"/>, and
    /// <see cref="ILists.LastIndexNG(IList)"/>, specifically on null and empty
    /// <see cref="List{T}"/>s.
    /// </summary>
    [Test(TestOf = typeof(ILists))]
    public void LastIndex_NullList_ReturnsValidity()
    {
      // Create a null list, and test it.
      List<int> list = null;
      Assert.AreEqual(ILists.InvalidIndex, list.LastIndex());
      Assert.AreEqual(ILists.InvalidIndex, list.LastIndexNG());

      // Create an empty list, and test it.
      list = new List<int>();
      Assert.AreEqual(ILists.InvalidIndex, list.LastIndex());
      Assert.AreEqual(ILists.InvalidIndex, list.LastIndexNG());

      // Create an empty list with a capacity, and test it.
      list = new List<int>(100);
      Assert.AreEqual(ILists.InvalidIndex, list.LastIndex());
      Assert.AreEqual(ILists.InvalidIndex, list.LastIndexNG());
    }

    /// <summary>
    /// A test for <see cref="ILists.LastIndex{T}(IList{T})"/>, and
    /// <see cref="ILists.LastIndexNG(IList)"/>, specifically on <see cref="List{T}"/>s.
    /// </summary>
    /// <param name="testSize">The test size of the <see cref="List{T}"/>.</param>
    [Test(TestOf = typeof(ILists))]
    public void LastIndex_List_ReturnsValidity([Range(1, StandardMaxCases)] int testSize)
    {
      // Create a full list, and test its last index.
      List<int> list = new List<int>(testSize);
      for (int i = 0; i < testSize; i++)
        list.Add(i);

      Assert.AreEqual(list.Count - 1, list.LastIndex());
      Assert.AreEqual(list.Count - 1, list.LastIndexNG());

      // Make sure that the last index can actually be used.
      Assert.DoesNotThrow(() => list[list.LastIndex()] = 1);
      Assert.DoesNotThrow(() => list[list.LastIndexNG()] = 1);
    }

    /// <summary>
    /// A test for <see cref="ILists.LastElement{T}(IList{T})"/>,
    /// <see cref="ILists.LastElement{T}(IList{T}, out T)"/>,
    /// <see cref="ILists.LastElementNG(IList)"/>, and
    /// <see cref="ILists.LastElementNG(IList, out object)"/>, specifically on null and empty
    /// arrays.
    /// </summary>
    [Test(TestOf = typeof(ILists))]
    public void LastElement_NullArray_ReturnsValidity()
    {
      TestAttribute testStrongOutput; // The test output for strongly typed functions.
      object testWeakOutput; // The test output for weakly typed functions.

      // Create a null array, and test it.
      TestAttribute[] array = null;
      Assert.AreEqual(null, array.LastElement());
      Assert.AreEqual(null, array.LastElementNG());
      Assert.IsFalse(array.LastElement(out testStrongOutput));
      Assert.AreEqual(null, testStrongOutput);
      Assert.IsFalse(array.LastElementNG(out testWeakOutput));
      Assert.AreEqual(null, testWeakOutput);

      // Create an empty array, and test it.
      array = new TestAttribute[0];
      Assert.AreEqual(null, array.LastElement());
      Assert.AreEqual(null, array.LastElementNG());
      Assert.IsFalse(array.LastElement(out testStrongOutput));
      Assert.AreEqual(null, testStrongOutput);
      Assert.IsFalse(array.LastElementNG(out testWeakOutput));
      Assert.AreEqual(null, testWeakOutput);
    }

    /// <summary>
    /// A test for <see cref="ILists.LastElement{T}(IList{T})"/>,
    /// <see cref="ILists.LastElement{T}(IList{T}, out T)"/>,
    /// <see cref="ILists.LastElementNG(IList)"/>, and
    /// <see cref="ILists.LastElementNG(IList, out object)"/>, specifically on null and empty
    /// arrays.
    /// </summary>
    /// <param name="testSize">The test size of the array.</param>
    [Test(TestOf = typeof(ILists))]
    public void LastElement_Array_ReturnsValidity([Range(1, StandardMaxCases)] int testSize)
    {
      TestAttribute testStrongOutput; // The test output for strongly typed functions.
      object testWeakOutput; // The test output for weakly typed functions.
      string testDescription = "VALID"; // A test description for the created TestAttributes.

      // Create a full array, and test it.
      TestAttribute[] array = new TestAttribute[testSize];
      for (int i = 0; i < testSize; i++)
        array[i] = new TestAttribute(){ Description = testDescription };

      // Test the strong output.
      testStrongOutput = array.LastElement();
      Assert.IsNotNull(testStrongOutput);
      Assert.AreEqual(testDescription, testStrongOutput.Description);
      Assert.IsTrue(array.LastElement(out testStrongOutput));
      Assert.AreEqual(testDescription, testStrongOutput.Description);

      // Test the weak output.
      testWeakOutput = array.LastElementNG();
      Assert.IsNotNull(testWeakOutput);
      Assert.AreEqual(testDescription, ((TestAttribute)testWeakOutput).Description);
      Assert.IsTrue(array.LastElementNG(out testWeakOutput));
      Assert.AreEqual(testDescription, ((TestAttribute)testWeakOutput).Description);
    }

    /// <summary>
    /// A test for <see cref="ILists.LastElement{T}(IList{T})"/>,
    /// <see cref="ILists.LastElement{T}(IList{T}, out T)"/>,
    /// <see cref="ILists.LastElementNG(IList)"/>, and
    /// <see cref="ILists.LastElementNG(IList, out object)"/>, specifically on null and empty
    /// <see cref="List{T}"/>s.
    /// </summary>
    [Test(TestOf = typeof(ILists))]
    public void LastElement_NullList_ReturnsValidity()
    {
      TestAttribute testStrongOutput; // The test output for strongly typed functions.
      object testWeakOutput; // The test output for weakly typed functions.

      // Create a null list, and test it.
      List<TestAttribute> list = null;
      Assert.AreEqual(null, list.LastElement());
      Assert.AreEqual(null, list.LastElementNG());
      Assert.IsFalse(list.LastElement(out testStrongOutput));
      Assert.AreEqual(null, testStrongOutput);
      Assert.IsFalse(list.LastElementNG(out testWeakOutput));
      Assert.AreEqual(null, testWeakOutput);

      // Create an empty list, and test it.
      list = new List<TestAttribute>();
      Assert.AreEqual(null, list.LastElement());
      Assert.AreEqual(null, list.LastElementNG());
      Assert.IsFalse(list.LastElement(out testStrongOutput));
      Assert.AreEqual(null, testStrongOutput);
      Assert.IsFalse(list.LastElementNG(out testWeakOutput));
      Assert.AreEqual(null, testWeakOutput);

      // Create an empty list with a capacity, and test it.
      list = new List<TestAttribute>(100);
      Assert.AreEqual(null, list.LastElement());
      Assert.AreEqual(null, list.LastElementNG());
      Assert.IsFalse(list.LastElement(out testStrongOutput));
      Assert.AreEqual(null, testStrongOutput);
      Assert.IsFalse(list.LastElementNG(out testWeakOutput));
      Assert.AreEqual(null, testWeakOutput);
    }

    /// <summary>
    /// A test for <see cref="ILists.LastElement{T}(IList{T})"/>,
    /// <see cref="ILists.LastElement{T}(IList{T}, out T)"/>,
    /// <see cref="ILists.LastElementNG(IList)"/>, and
    /// <see cref="ILists.LastElementNG(IList, out object)"/>, specifically on null and empty
    /// <see cref="List{T}"/>s.
    /// </summary>
    /// <param name="testSize">The test size of the <see cref="List{T}"/>.</param>
    [Test(TestOf = typeof(ILists))]
    public void LastElement_List_ReturnsValidity([Range(1, StandardMaxCases)] int testSize)
    {
      TestAttribute testStrongOutput; // The test output for strongly typed functions.
      object testWeakOutput; // The test output for weakly typed functions.
      string testDescription = "VALID"; // A test description for the created TestAttributes.

      // Create a full list, and test it.
      List<TestAttribute> list = new List<TestAttribute>(testSize);
      for (int i = 0; i < testSize; i++)
        list.Add(new TestAttribute() { Description = testDescription });

      // Test the strong output.
      testStrongOutput = list.LastElement();
      Assert.IsNotNull(testStrongOutput);
      Assert.AreEqual(testDescription, testStrongOutput.Description);
      Assert.IsTrue(list.LastElement(out testStrongOutput));
      Assert.AreEqual(testDescription, testStrongOutput.Description);

      // Test the weak output.
      testWeakOutput = list.LastElementNG();
      Assert.IsNotNull(testWeakOutput);
      Assert.AreEqual(testDescription, ((TestAttribute)testWeakOutput).Description);
      Assert.IsTrue(list.LastElementNG(out testWeakOutput));
      Assert.AreEqual(testDescription, ((TestAttribute)testWeakOutput).Description);
    }

    /// <summary>
    /// A test for <see cref="ILists.SwapValues{T}(IList{T}, int, int)"/>,
    /// <see cref="ILists.SwapValuesNG(IList, int, int)"/>,
    /// <see cref="ILists.SwapValues{T}(IList{T}, T, T)"/>, and
    /// <see cref="ILists.SwapValuesNG(IList, object, object)"/>, specifically on null value arrays.
    /// </summary>
    [Test(TestOf = typeof(ILists))]
    public void SwapValues_NullValueArray_ReturnsFailure()
    {
      // Create a null array and make sure values cannot be swapped.
      int[] array = null;
      Assert.IsFalse(array.SwapValues(0, 1));
      Assert.IsFalse(array.SwapValuesNG(0, 1));

      // Create an array and test non-null arrays with invalid indexes.
      array = new int[1];
      Assert.IsFalse(array.SwapValues(-1, 0));
      Assert.IsFalse(array.SwapValuesNG(-1, 0));
    }

    /// <summary>
    /// A test for <see cref="ILists.SwapValues{T}(IList{T}, int, int)"/>,
    /// <see cref="ILists.SwapValuesNG(IList, int, int)"/>,
    /// <see cref="ILists.SwapValues{T}(IList{T}, T, T)"/>, and
    /// <see cref="ILists.SwapValuesNG(IList, object, object)"/>, specifically on value arrays.
    /// </summary>
    /// <param name="testSize">The test size of the array.</param>
    /// <param name="indexA">The first index to swap.</param>
    /// <param name="indexB">The second index to swap.</param>
    [Test(TestOf = typeof(ILists))]
    public void SwapValues_ValueArray_ReturnsSuccess([Values(15)] int testSize,
                                                     [Random(0, 15, StandardMaxCases)] int indexA, 
                                                     [Random(0, 15, StandardMaxCases)] int indexB)
    {
      // Assert that the indexes are valid.
      Assert.IsTrue(indexA >= 0 && indexA < testSize, "{0} is not a valid index.", indexA);
      Assert.IsTrue(indexA >= 0 && indexB < testSize, "{0} is not a valid index.", indexB);

      // Create an array of value types and fill it.
      int[] array = new int[testSize];
      for (int i = 0; i < testSize; i++)
        array[i] = i;

      // Get the initial values.
      int valueA = array[indexA];
      int valueB = array[indexB];

      // Assert that the first swap is valid.
      Assert.IsTrue(array.SwapValues(indexA, indexB));
      Assert.IsTrue(valueB == array[indexA]);
      Assert.IsTrue(valueA == array[indexB]);

      // Assert that the swap back is valid.
      Assert.IsTrue(array.SwapValuesNG(indexB, indexA));
      Assert.IsTrue(valueA == array[indexA]);
      Assert.IsTrue(valueB == array[indexB]);
    }

    /// <summary>
    /// A test for <see cref="ILists.SwapValues{T}(IList{T}, int, int)"/>,
    /// <see cref="ILists.SwapValuesNG(IList, int, int)"/>,
    /// <see cref="ILists.SwapValues{T}(IList{T}, T, T)"/>, and
    /// <see cref="ILists.SwapValuesNG(IList, object, object)"/>, specifically on null ref arrays.
    /// </summary>
    [Test(TestOf = typeof(ILists))]
    public void SwapValues_NullRefArray_ReturnsFailure()
    {
      // Create a null array and make sure values cannot be swapped.
      TestAttribute[] array = null;
      Assert.IsFalse(array.SwapValues(0, 1));
      Assert.IsFalse(array.SwapValuesNG(0, 1));
      
      // Create an array and test non-null arrays with invalid indexes.
      array = new TestAttribute[1];
      Assert.IsFalse(array.SwapValues(-1, 0));
      Assert.IsFalse(array.SwapValuesNG(-1, 0));

      // Test with random reference values that aren't actually in the array.
      TestAttribute testA = new TestAttribute() { Description = "A" };
      TestAttribute testB = new TestAttribute() { Description = "B" };

      Assert.IsFalse(array.SwapValues(testA, testB));
      Assert.IsFalse(array.SwapValues(testB, testA));
      Assert.IsFalse(array.SwapValuesNG(testA, testB));
      Assert.IsFalse(array.SwapValuesNG(testB, testA));
    }

    /// <summary>
    /// A test for <see cref="ILists.SwapValues{T}(IList{T}, int, int)"/>,
    /// <see cref="ILists.SwapValuesNG(IList, int, int)"/>,
    /// <see cref="ILists.SwapValues{T}(IList{T}, T, T)"/>, and
    /// <see cref="ILists.SwapValuesNG(IList, object, object)"/>, specifically on ref arrays.
    /// </summary>
    /// <param name="testSize">The test size of the array.</param>
    /// <param name="indexA">The first index to swap.</param>
    /// <param name="indexB">The second index to swap.</param>
    [Test(TestOf = typeof(ILists))]
    public void SwapValues_RefArray_ReturnsSuccess([Values(15)] int testSize,
                                                   [Random(0, 15, StandardMaxCases)] int indexA,
                                                   [Random(0, 15, StandardMaxCases)] int indexB)
    {
      // Assert that the indexes are valid.
      Assert.IsTrue(indexA >= 0 && indexA < testSize, "{0} is not a valid index.", indexA);
      Assert.IsTrue(indexA >= 0 && indexB < testSize, "{0} is not a valid index.", indexB);

      // Create an array of value types and fill it.
      TestAttribute[] array = new TestAttribute[testSize];
      for (int i = 0; i < testSize; i++)
        array[i] = new TestAttribute() { Description = i.ToString() };

      // Get the initial values.
      TestAttribute valueA = array[indexA];
      TestAttribute valueB = array[indexB];

      // Assert that the first swap is valid.
      Assert.IsTrue(array.SwapValues(indexA, indexB));
      Assert.IsTrue(valueB == array[indexA]);
      Assert.IsTrue(valueA == array[indexB]);

      // Assert that the swap back is valid.
      Assert.IsTrue(array.SwapValuesNG(indexB, indexA));
      Assert.IsTrue(valueA == array[indexA]);
      Assert.IsTrue(valueB == array[indexB]);

      // Assert that the first ref swap is valid.
      Assert.IsTrue(array.SwapValues(valueA, valueB));
      Assert.IsTrue(valueB == array[indexA]);
      Assert.IsTrue(valueA == array[indexB]);

      // Assert that the ref swap back is valid.
      Assert.IsTrue(array.SwapValuesNG(valueB, valueA));
      Assert.IsTrue(valueA == array[indexA]);
      Assert.IsTrue(valueB == array[indexB]);
    }

    /// <summary>
    /// A test for <see cref="ILists.SwapValues{T}(IList{T}, int, int)"/>,
    /// <see cref="ILists.SwapValuesNG(IList, int, int)"/>,
    /// <see cref="ILists.SwapValues{T}(IList{T}, T, T)"/>, and
    /// <see cref="ILists.SwapValuesNG(IList, object, object)"/>, specifically on null value
    /// <see cref="List{T}"/>s.
    /// </summary>
    [Test(TestOf = typeof(ILists))]
    public void SwapValues_NullValueList_ReturnsFailure()
    {
      // Create a null list and make sure values cannot be swapped.
      List<int> list = null;
      Assert.IsFalse(list.SwapValues(0, 1));
      Assert.IsFalse(list.SwapValuesNG(0, 1));

      // Create an list and test non-null lists with invalid indexes.
      list = new List<int>(1);
      Assert.IsFalse(list.SwapValues(-1, 0));
      Assert.IsFalse(list.SwapValuesNG(-1, 0));
    }

    /// <summary>
    /// A test for <see cref="ILists.SwapValues{T}(IList{T}, int, int)"/>,
    /// <see cref="ILists.SwapValuesNG(IList, int, int)"/>,
    /// <see cref="ILists.SwapValues{T}(IList{T}, T, T)"/>, and
    /// <see cref="ILists.SwapValuesNG(IList, object, object)"/>, specifically on value
    /// <see cref="List{T}"/>s.
    /// </summary>
    /// <param name="testSize">The test size of the <see cref="List{T}"/>.</param>
    /// <param name="indexA">The first index to swap.</param>
    /// <param name="indexB">The second index to swap.</param>
    [Test(TestOf = typeof(ILists))]
    public void SwapValues_ValueList_ReturnsSuccess([Values(15)] int testSize,
                                                    [Random(0, 15, StandardMaxCases)] int indexA,
                                                    [Random(0, 15, StandardMaxCases)] int indexB)
    {
      // Assert that the indexes are valid.
      Assert.IsTrue(indexA >= 0 && indexA < testSize, "{0} is not a valid index.", nameof(indexA));
      Assert.IsTrue(indexA >= 0 && indexB < testSize, "{0} is not a valid index.", nameof(indexB));

      // Create an list of value types and fill it.
      List<int> list = new List<int>(testSize);
      for (int i = 0; i < testSize; i++)
        list.Add(i);

      // Get the initial values.
      int valueA = list[indexA];
      int valueB = list[indexB];

      // Assert that the first swap is valid.
      Assert.IsTrue(list.SwapValues(indexA, indexB));
      Assert.IsTrue(valueB == list[indexA]);
      Assert.IsTrue(valueA == list[indexB]);

      // Assert that the swap back is valid.
      Assert.IsTrue(list.SwapValuesNG(indexB, indexA));
      Assert.IsTrue(valueA == list[indexA]);
      Assert.IsTrue(valueB == list[indexB]);
    }

    /// <summary>
    /// A test for <see cref="ILists.SwapValues{T}(IList{T}, int, int)"/>,
    /// <see cref="ILists.SwapValuesNG(IList, int, int)"/>,
    /// <see cref="ILists.SwapValues{T}(IList{T}, T, T)"/>, and
    /// <see cref="ILists.SwapValuesNG(IList, object, object)"/>, specifically on null ref
    /// <see cref="List{T}"/>s.
    /// </summary>
    [Test(TestOf = typeof(ILists))]
    public void SwapValues_NullRefList_ReturnsFailure()
    {
      // Create a null list and make sure values cannot be swapped.
      List<TestAttribute> list = null;
      Assert.IsFalse(list.SwapValues(0, 1));
      Assert.IsFalse(list.SwapValuesNG(0, 1));

      // Create an list and test non-null lists with invalid indexes.
      list = new List<TestAttribute>(1);
      Assert.IsFalse(list.SwapValues(-1, 0));
      Assert.IsFalse(list.SwapValuesNG(-1, 0));

      // Test with random reference values that aren't actually in the list.
      TestAttribute testA = new TestAttribute() { Description = "A" };
      TestAttribute testB = new TestAttribute() { Description = "B" };

      Assert.IsFalse(list.SwapValues(testA, testB));
      Assert.IsFalse(list.SwapValues(testB, testA));
      Assert.IsFalse(list.SwapValuesNG(testA, testB));
      Assert.IsFalse(list.SwapValuesNG(testB, testA));
    }

    /// <summary>
    /// A test for <see cref="ILists.SwapValues{T}(IList{T}, int, int)"/>,
    /// <see cref="ILists.SwapValuesNG(IList, int, int)"/>,
    /// <see cref="ILists.SwapValues{T}(IList{T}, T, T)"/>, and
    /// <see cref="ILists.SwapValuesNG(IList, object, object)"/>, specifically on ref
    /// <see cref="List{T}"/>s.
    /// </summary>
    /// <param name="testSize">The test size of the <see cref="List{T}"/>.</param>
    /// <param name="indexA">The first index to swap.</param>
    /// <param name="indexB">The second index to swap.</param>
    [Test(TestOf = typeof(ILists))]
    public void SwapValues_RefList_ReturnsSuccess([Values(15)] int testSize,
                                                  [Random(0, 15, StandardMaxCases)] int indexA,
                                                  [Random(0, 15, StandardMaxCases)] int indexB)
    {
      // Assert that the indexes are valid.
      Assert.IsTrue(indexA >= 0 && indexA < testSize, "{0} is not a valid index.", nameof(indexA));
      Assert.IsTrue(indexA >= 0 && indexB < testSize, "{0} is not a valid index.", nameof(indexB));

      // Create an list of value types and fill it.
      List<TestAttribute> list = new List<TestAttribute>(testSize);
      for (int i = 0; i < testSize; i++)
        list.Add(new TestAttribute() { Description = i.ToString() });
      
      // Get the initial values.
      TestAttribute valueA = list[indexA];
      TestAttribute valueB = list[indexB];

      // Assert that the first swap is valid.
      Assert.IsTrue(list.SwapValues(indexA, indexB));
      Assert.IsTrue(valueB == list[indexA]);
      Assert.IsTrue(valueA == list[indexB]);

      // Assert that the swap back is valid.
      Assert.IsTrue(list.SwapValuesNG(indexB, indexA));
      Assert.IsTrue(valueA == list[indexA]);
      Assert.IsTrue(valueB == list[indexB]);

      // Assert that the first ref swap is valid.
      Assert.IsTrue(list.SwapValues(valueA, valueB));
      Assert.IsTrue(valueB == list[indexA]);
      Assert.IsTrue(valueA == list[indexB]);

      // Assert that the ref swap back is valid.
      Assert.IsTrue(list.SwapValuesNG(valueB, valueA));
      Assert.IsTrue(valueA == list[indexA]);
      Assert.IsTrue(valueB == list[indexB]);
    }

    /// <summary>
    /// A test for <see cref="ILists.Replace{T}(IList{T}, T, T)"/>,
    /// <see cref="ILists.ReplaceAll{T}(IList{T}, T, T)"/>,
    /// <see cref="ILists.ReplaceRange{T}(IList{T}, T, T, int)"/>,
    /// <see cref="ILists.ReplaceRange{T}(IList{T}, T, T, int, int)"/>,
    /// <see cref="ILists.ReplaceNG(IList, object, object)"/>,
    /// <see cref="ILists.ReplaceAllNG(IList, object, object)"/>,
    /// <see cref="ILists.ReplaceRangeNG(IList, object, object, int)"/>, and
    /// <see cref="ILists.ReplaceRangeNG(IList, object, object, int, int)"/>, specifically
    /// on null arrays.
    /// </summary>
    [Test(TestOf = typeof(ILists))]
    public void Replace_NullArray_ReturnsFailure()
    {
      // Create a null array, and make sure the replacements fail.
      int[] array = null;
      Assert.AreEqual(array.Replace(0, 1), ILists.InvalidIndex);
      Assert.IsFalse(array.ReplaceAll(0, 1));
      Assert.IsFalse(array.ReplaceRange(0, 1, 1));
      Assert.IsFalse(array.ReplaceRange(0, 1, 1, 5));
      Assert.AreEqual(array.ReplaceNG(0, 1), ILists.InvalidIndex);
      Assert.IsFalse(array.ReplaceAllNG(0, 1));
      Assert.IsFalse(array.ReplaceRangeNG(0, 1, 1));
      Assert.IsFalse(array.ReplaceRangeNG(0, 1, 1, 5));
    }

    /// <summary>
    /// A test for <see cref="ILists.Replace{T}(IList{T}, T, T)"/> and
    /// <see cref="ILists.ReplaceNG(IList, object, object)"/>, specifically on arrays.
    /// </summary>
    /// <param name="testSize">The test size of the array.</param>
    /// <param name="oldItem">The old item to replace.</param>
    /// <param name="newItem">The item to replace the <paramref name="oldItem"/> with.</param>
    [Test(TestOf = typeof(ILists))]
    public void Replace_Array_ReturnsSuccess([Values(15)] int testSize,
                                             [Random(0, 15, StandardMaxCases)] int oldItem,
                                             [Random(15, 30, StandardMaxCases)] int newItem)
    {
      // Make sure the test is valid.
      Assert.IsTrue(oldItem >= 0, "{0} is less than 0", nameof(oldItem));
      Assert.IsTrue(testSize > oldItem, "{0} is less than {1}", nameof(oldItem), nameof(testSize));
      Assert.AreNotEqual(oldItem, newItem, "Both items are the same.");

      // Create and fill the array.
      int[] array = new int[testSize];
      for (int i = 0; i < testSize; i++)
        array[i] = i;

      // Assert that a bad value fails.
      Assert.AreEqual(ILists.InvalidIndex, array.Replace(testSize, newItem));
      Assert.AreEqual(ILists.InvalidIndex, array.ReplaceNG(testSize, newItem));

      // Find the old item's index.
      int realIndex = System.Array.IndexOf(array, oldItem);
      Assert.AreNotEqual(ILists.InvalidIndex, realIndex);

      // Replace the old item and make sure the replacement worked.
      int testIndex = array.Replace(oldItem, newItem);
      Assert.AreNotEqual(ILists.InvalidIndex, testIndex);
      Assert.AreEqual(newItem, array[testIndex]);

      // Replace using the non-generic version.
      array[testIndex] = oldItem;
      testIndex = array.ReplaceNG(oldItem, newItem);
      Assert.AreNotEqual(ILists.InvalidIndex, testIndex);
      Assert.AreEqual(newItem, array[testIndex]);
    }

    /// <summary>
    /// A test for <see cref="ILists.ReplaceAll{T}(IList{T}, T, T)"/> and
    /// <see cref="ILists.ReplaceAllNG(IList, object, object)"/>, specifically on arrays.
    /// </summary>
    /// <param name="testSize">The test size of the array.</param>
    /// <param name="oldItem">The old item to replace.</param>
    /// <param name="newItem">The item to replace the <paramref name="oldItem"/> with.</param>
    [Test(TestOf = typeof(ILists))]
    public void ReplaceAll_Array_ReturnsSuccess([Values(15)] int testSize,
                                                [Random(1, 15, StandardMaxCases)] int oldItem,
                                                [Random(15, 30, StandardMaxCases)] int newItem)
    {
      // Make sure the test is valid.
      Assert.IsTrue(oldItem >= 0, "{0} is less than 0", nameof(oldItem));
      Assert.IsTrue(testSize > oldItem, "{0} is less than {1}", nameof(oldItem), nameof(testSize));
      Assert.AreNotEqual(oldItem, newItem, "Both items are the same.");

      // Create and fill the array.
      int[] array = new int[testSize];
      for (int i = 0; i < testSize; i++)
        array[i] = oldItem;

      // Assert that a bad value fails.
      Assert.IsFalse(array.ReplaceAll(testSize, newItem));
      Assert.IsFalse(array.ReplaceAllNG(testSize, newItem));

      // Replace the old item and make sure the replacement worked.
      Assert.IsTrue(array.ReplaceAll(oldItem, newItem));
      for (int i = 0; i < testSize; i++)
        Assert.AreEqual(newItem, array[i]);

      // Set up the array to test partially filled arrays of the wanted value.
      for (int i = testSize / 2; i < testSize; i++)
        array[i] = oldItem + newItem;

      // Test that the non-generic replacement works.
      Assert.IsTrue(array.ReplaceAllNG(oldItem + newItem, newItem));
      for (int i = testSize / 2; i < testSize; i++)
        Assert.AreEqual(newItem, array[i]);
    }

    /// <summary>
    /// A test for <see cref="ILists.ReplaceRange{T}(IList{T}, T, T, int)"/>,
    /// <see cref="ILists.ReplaceRange{T}(IList{T}, T, T, int, int)"/>,
    /// <see cref="ILists.ReplaceRangeNG(IList, object, object, int)"/>, and
    /// <see cref="ILists.ReplaceRangeNG(IList, object, object, int, int)"/>, specifically on
    /// arrays.
    /// </summary>
    /// <param name="testSize">The test size of the array.</param>
    /// <param name="oldItem">The old item to replace.</param>
    /// <param name="newItem">The item to replace the <paramref name="oldItem"/> with.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    [Test(TestOf = typeof(ILists))]
    public void ReplaceRange_Array_ReturnsSuccess([Values(20)] int testSize,
                                                  [Random(1, 15, StandardMaxCases)] int oldItem,
                                                  [Random(15, 30, StandardMaxCases)] int newItem,
                                                  [Random(0, 10, 1)] int startIndex,
                                                  [Random(10, 20, 1)] int lastIndex)
    {
      // Make sure the test is valid.
      Assert.IsTrue(oldItem >= 0, "{0} is less than 0", nameof(oldItem));
      Assert.IsTrue(testSize > oldItem, "{0} is less than {1}", nameof(oldItem), nameof(testSize));
      Assert.AreNotEqual(oldItem, newItem, "Both items are the same.");

      // Create and fill the array.
      int[] array = new int[testSize];
      for (int i = 0; i < testSize; i++)
        array[i] = oldItem;

      // Assert that a bad value fails.
      Assert.IsFalse(array.ReplaceRange(testSize, newItem, startIndex, lastIndex));
      Assert.IsFalse(array.ReplaceRange(testSize, newItem, startIndex));
      Assert.IsFalse(array.ReplaceRangeNG(testSize, newItem, startIndex, lastIndex));
      Assert.IsFalse(array.ReplaceRangeNG(testSize, newItem, startIndex));

      // Replace the old item and make sure the replacement worked.
      Assert.IsTrue(array.ReplaceRange(oldItem, newItem, startIndex));
      for (int i = 0; i < testSize; i++)
      {
        if (i >= startIndex)
          Assert.AreEqual(newItem, array[i]);
        else
          Assert.AreNotEqual(newItem, array[i]);
      }

      Assert.IsTrue(array.ReplaceRangeNG(newItem, oldItem, startIndex));
      for (int i = 0; i < testSize; i++)
        Assert.AreEqual(oldItem, array[i]);

      Assert.IsTrue(array.ReplaceRange(oldItem, newItem, startIndex, lastIndex));
      for (int i = 0; i < testSize; i++)
      {
        if (i >= startIndex && i < lastIndex)
          Assert.AreEqual(newItem, array[i]);
        else
          Assert.AreNotEqual(newItem, array[i]);
      }

      Assert.IsTrue(array.ReplaceRangeNG(newItem, oldItem, startIndex, lastIndex));
      for (int i = 0; i < testSize; i++)
        Assert.AreEqual(oldItem, array[i]);
    }

    /// <summary>
    /// A test for <see cref="ILists.Replace{T}(IList{T}, T, T)"/>,
    /// <see cref="ILists.ReplaceAll{T}(IList{T}, T, T)"/>,
    /// <see cref="ILists.ReplaceRange{T}(IList{T}, T, T, int)"/>,
    /// <see cref="ILists.ReplaceRange{T}(IList{T}, T, T, int, int)"/>,
    /// <see cref="ILists.ReplaceNG(IList, object, object)"/>,
    /// <see cref="ILists.ReplaceAllNG(IList, object, object)"/>,
    /// <see cref="ILists.ReplaceRangeNG(IList, object, object, int)"/>, and
    /// <see cref="ILists.ReplaceRangeNG(IList, object, object, int, int)"/>, specifically
    /// on null <see cref="List{T}"/>s.
    /// </summary>
    [Test(TestOf = typeof(ILists))]
    public void Replace_NullList_ReturnsFailure()
    {
      // Create a null list, and make sure the replacements fail.
      List<int> list = null;
      Assert.AreEqual(list.Replace(0, 1), ILists.InvalidIndex);
      Assert.IsFalse(list.ReplaceAll(0, 1));
      Assert.IsFalse(list.ReplaceRange(0, 1, 1));
      Assert.IsFalse(list.ReplaceRange(0, 1, 1, 5));
      Assert.AreEqual(list.ReplaceNG(0, 1), ILists.InvalidIndex);
      Assert.IsFalse(list.ReplaceAllNG(0, 1));
      Assert.IsFalse(list.ReplaceRangeNG(0, 1, 1));
      Assert.IsFalse(list.ReplaceRangeNG(0, 1, 1, 5));

      // Give the list a capacity, and make sure the replacements fail.
      list = new List<int>(5);
      Assert.AreEqual(list.Replace(0, 1), ILists.InvalidIndex);
      Assert.IsFalse(list.ReplaceAll(0, 1));
      Assert.IsFalse(list.ReplaceRange(0, 1, 1));
      Assert.IsFalse(list.ReplaceRange(0, 1, 1, 5));
      Assert.AreEqual(list.ReplaceNG(0, 1), ILists.InvalidIndex);
      Assert.IsFalse(list.ReplaceAllNG(0, 1));
      Assert.IsFalse(list.ReplaceRangeNG(0, 1, 1));
      Assert.IsFalse(list.ReplaceRangeNG(0, 1, 1, 5));
    }

    /// <summary>
    /// A test for <see cref="ILists.Replace{T}(IList{T}, T, T)"/> and
    /// <see cref="ILists.ReplaceNG(IList, object, object)"/>, specifically on
    /// <see cref="List{T}"/>s.
    /// </summary>
    /// <param name="testSize">The test size of the <see cref="List{T}"/>.</param>
    /// <param name="oldItem">The old item to replace.</param>
    /// <param name="newItem">The item to replace the <paramref name="oldItem"/> with.</param>
    [Test(TestOf = typeof(ILists))]
    public void Replace_List_ReturnsSuccess([Values(15)] int testSize,
                                             [Random(0, 15, StandardMaxCases)] int oldItem,
                                             [Random(15, 30, StandardMaxCases)] int newItem)
    {
      // Make sure the test is valid.
      Assert.IsTrue(oldItem >= 0, "{0} is less than 0", nameof(oldItem));
      Assert.IsTrue(testSize > oldItem, "{0} is less than {1}", nameof(oldItem), nameof(testSize));
      Assert.AreNotEqual(oldItem, newItem, "Both items are the same.");

      // Create and fill the array.
      List<int> list = new List<int>(testSize);
      for (int i = 0; i < testSize; i++)
        list.Add(i);

      // Assert that a bad value fails.
      Assert.AreEqual(ILists.InvalidIndex, list.Replace(testSize, newItem));
      Assert.AreEqual(ILists.InvalidIndex, list.ReplaceNG(testSize, newItem));

      // Find the old item's index.
      int realIndex = list.IndexOf(oldItem);
      Assert.AreNotEqual(ILists.InvalidIndex, realIndex);

      // Replace the old item and make sure the replacement worked.
      int testIndex = list.Replace(oldItem, newItem);
      Assert.AreNotEqual(ILists.InvalidIndex, testIndex);
      Assert.AreEqual(newItem, list[testIndex]);

      // Replace using the non-generic version.
      list[testIndex] = oldItem;
      testIndex = list.ReplaceNG(oldItem, newItem);
      Assert.AreNotEqual(ILists.InvalidIndex, testIndex);
      Assert.AreEqual(newItem, list[testIndex]);
    }

    /// <summary>
    /// A test for <see cref="ILists.ReplaceAll{T}(IList{T}, T, T)"/> and
    /// <see cref="ILists.ReplaceAllNG(IList, object, object)"/>, specifically on
    /// <see cref="List{T}"/>s.
    /// </summary>
    /// <param name="testSize">The test size of the list.</param>
    /// <param name="oldItem">The old item to replace.</param>
    /// <param name="newItem">The item to replace the <paramref name="oldItem"/> with.</param>
    [Test(TestOf = typeof(ILists))]
    public void ReplaceAll_List_ReturnsSuccess([Values(15)] int testSize,
                                                [Random(1, 15, StandardMaxCases)] int oldItem,
                                                [Random(15, 30, StandardMaxCases)] int newItem)
    {
      // Make sure the test is valid.
      Assert.IsTrue(oldItem >= 0, "{0} is less than 0", nameof(oldItem));
      Assert.IsTrue(testSize > oldItem, "{0} is less than {1}", nameof(oldItem), nameof(testSize));
      Assert.AreNotEqual(oldItem, newItem, "Both items are the same.");

      // Create and fill the list.
      List<int> list = new List<int>(testSize);
      for (int i = 0; i < testSize; i++)
        list.Add(oldItem);

      // Assert that a bad value fails.
      Assert.IsFalse(list.ReplaceAll(testSize, newItem));
      Assert.IsFalse(list.ReplaceAllNG(testSize, newItem));

      // Replace the old item and make sure the replacement worked.
      Assert.IsTrue(list.ReplaceAll(oldItem, newItem));
      for (int i = 0; i < testSize; i++)
        Assert.AreEqual(newItem, list[i]);

      // Set up the list to test partially filled lists of the wanted value.
      for (int i = testSize / 2; i < testSize; i++)
        list[i] = oldItem + newItem;

      // Test that the non-generic replacement works.
      Assert.IsTrue(list.ReplaceAllNG(oldItem + newItem, newItem));
      for (int i = testSize / 2; i < testSize; i++)
        Assert.AreEqual(newItem, list[i]);
    }

    /// <summary>
    /// A test for <see cref="ILists.ReplaceRange{T}(IList{T}, T, T, int)"/>,
    /// <see cref="ILists.ReplaceRange{T}(IList{T}, T, T, int, int)"/>,
    /// <see cref="ILists.ReplaceRangeNG(IList, object, object, int)"/>, and
    /// <see cref="ILists.ReplaceRangeNG(IList, object, object, int, int)"/>, specifically on
    /// <see cref="List{T}"/>s.
    /// </summary>
    /// <param name="testSize">The test size of the list.</param>
    /// <param name="oldItem">The old item to replace.</param>
    /// <param name="newItem">The item to replace the <paramref name="oldItem"/> with.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    [Test(TestOf = typeof(ILists))]
    public void ReplaceRange_List_ReturnsSuccess([Values(20)] int testSize,
                                                  [Random(1, 15, StandardMaxCases)] int oldItem,
                                                  [Random(15, 30, StandardMaxCases)] int newItem,
                                                  [Random(0, 10, 1)] int startIndex,
                                                  [Random(10, 20, 1)] int lastIndex)
    {
      // Make sure the test is valid.
      Assert.IsTrue(oldItem >= 0, "{0} is less than 0", nameof(oldItem));
      Assert.IsTrue(testSize > oldItem, "{0} is less than {1}", nameof(oldItem), nameof(testSize));
      Assert.AreNotEqual(oldItem, newItem, "Both items are the same.");

      // Create and fill the list.
      List<int> list = new List<int>(testSize);
      for (int i = 0; i < testSize; i++)
        list.Add(oldItem);

      // Assert that a bad value fails.
      Assert.IsFalse(list.ReplaceRange(testSize, newItem, startIndex, lastIndex));
      Assert.IsFalse(list.ReplaceRange(testSize, newItem, startIndex));
      Assert.IsFalse(list.ReplaceRangeNG(testSize, newItem, startIndex, lastIndex));
      Assert.IsFalse(list.ReplaceRangeNG(testSize, newItem, startIndex));

      // Replace the old item and make sure the replacement worked.
      Assert.IsTrue(list.ReplaceRange(oldItem, newItem, startIndex));
      for (int i = 0; i < testSize; i++)
      {
        if (i >= startIndex)
          Assert.AreEqual(newItem, list[i]);
        else
          Assert.AreNotEqual(newItem, list[i]);
      }

      Assert.IsTrue(list.ReplaceRangeNG(newItem, oldItem, startIndex));
      for (int i = 0; i < testSize; i++)
        Assert.AreEqual(oldItem, list[i]);

      Assert.IsTrue(list.ReplaceRange(oldItem, newItem, startIndex, lastIndex));
      for (int i = 0; i < testSize; i++)
      {
        if (i >= startIndex && i < lastIndex)
          Assert.AreEqual(newItem, list[i]);
        else
          Assert.AreNotEqual(newItem, list[i]);
      }

      Assert.IsTrue(list.ReplaceRangeNG(newItem, oldItem, startIndex, lastIndex));
      for (int i = 0; i < testSize; i++)
        Assert.AreEqual(oldItem, list[i]);
    }

    /// <summary>
    /// A test for <see cref="ILists.ReplacePattern{T}(IList{T}, T, System.Func{T, int, bool})"/>,
    /// <see cref="ILists.ReplacePattern{T}(IList{T}, T, int, System.Func{T, int, bool})"/>,
    /// <see cref="ILists.ReplacePattern{T}(IList{T}, T, int, int, System.Func{T, int, bool})"/>,
    /// <see cref="ILists.ReplacePatternNG(IList, object, System.Func{object, int, bool})"/>,
    /// <see cref="ILists.ReplacePatternNG(IList, object, int, System.Func{object, int, bool})"/>,
    /// and <see cref="ILists.ReplacePatternNG(IList, object, int, int,
    /// System.Func{object, int, bool})"/>, specifically on null arrays.
    /// </summary>
    [Test(TestOf = typeof(ILists))]
    public void ReplacePattern_NullArray_ReturnsFailure()
    {
      // Create a null array, and make sure the replacements fail.
      int[] array = null;
      Assert.IsFalse(array.ReplacePattern(1, (t, i) => i % 2 == 0));
      Assert.IsFalse(array.ReplacePattern(1, 0, (t, i) => i % 2 == 0));
      Assert.IsFalse(array.ReplacePattern(1, 0, 1, (t, i) => i % 2 == 0));
      Assert.IsFalse(array.ReplacePatternNG(1, (t, i) => i % 2 == 0));
      Assert.IsFalse(array.ReplacePatternNG(1, 0, (t, i) => i % 2 == 0));
      Assert.IsFalse(array.ReplacePatternNG(1, 0, 1, (t, i) => i % 2 == 0));
    }

    /// <summary>
    /// A test for <see cref="ILists.ReplacePattern{T}(IList{T}, T, System.Func{T, int, bool})"/>,
    /// <see cref="ILists.ReplacePattern{T}(IList{T}, T, int, System.Func{T, int, bool})"/>,
    /// <see cref="ILists.ReplacePattern{T}(IList{T}, T, int, int, System.Func{T, int, bool})"/>,
    /// <see cref="ILists.ReplacePatternNG(IList, object, System.Func{object, int, bool})"/>,
    /// <see cref="ILists.ReplacePatternNG(IList, object, int, System.Func{object, int, bool})"/>,
    /// and <see cref="ILists.ReplacePatternNG(IList, object, int, int,
    /// System.Func{object, int, bool})"/>, specifically on <see cref="List{T}"/>s.
    /// </summary>
    [Test(TestOf = typeof(ILists))]
    public void ReplacePattern_NullList_ReturnsFailure()
    {
      // Create a null list, and make sure the replacements fail.
      List<int> list = null;
      Assert.IsFalse(list.ReplacePattern(1, (t, i) => i % 2 == 0));
      Assert.IsFalse(list.ReplacePattern(1, 0, (t, i) => i % 2 == 0));
      Assert.IsFalse(list.ReplacePattern(1, 0, 1, (t, i) => i % 2 == 0));
      Assert.IsFalse(list.ReplacePatternNG(1, (t, i) => i % 2 == 0));
      Assert.IsFalse(list.ReplacePatternNG(1, 0, (t, i) => i % 2 == 0));
      Assert.IsFalse(list.ReplacePatternNG(1, 0, 1, (t, i) => i % 2 == 0));

      // Perform the same tests on an empty list.
      list = new List<int>(5);
      Assert.IsFalse(list.ReplacePattern(1, (t, i) => i % 2 == 0));
      Assert.IsFalse(list.ReplacePattern(1, 0, (t, i) => i % 2 == 0));
      Assert.IsFalse(list.ReplacePattern(1, 0, 1, (t, i) => i % 2 == 0));
      Assert.IsFalse(list.ReplacePatternNG(1, (t, i) => i % 2 == 0));
      Assert.IsFalse(list.ReplacePatternNG(1, 0, (t, i) => i % 2 == 0));
      Assert.IsFalse(list.ReplacePatternNG(1, 0, 1, (t, i) => i % 2 == 0));
    }

    /// <summary>
    /// A test for <see cref="ILists.ReplacePattern{T}(IList{T}, T, System.Func{T, int, bool})"/>,
    /// <see cref="ILists.ReplacePattern{T}(IList{T}, T, int, System.Func{T, int, bool})"/>,
    /// <see cref="ILists.ReplacePattern{T}(IList{T}, T, int, int, System.Func{T, int, bool})"/>,
    /// <see cref="ILists.ReplacePatternNG(IList, object, System.Func{object, int, bool})"/>,
    /// <see cref="ILists.ReplacePatternNG(IList, object, int, System.Func{object, int, bool})"/>,
    /// and <see cref="ILists.ReplacePatternNG(IList, object, int, int,
    /// System.Func{object, int, bool})"/>, specifically on arrays.
    /// </summary>
    /// <param name="testSize">The test size of the array.</param>
    /// <param name="oldItem">The old item to replace.</param>
    /// <param name="newItem">The item to replace the <paramref name="oldItem"/> with.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    [Test(TestOf = typeof(ILists))]
    public void ReplacePattern_Array_ReturnsSuccess([Values(20)] int testSize,
                                                [Random(1, 15, StandardMaxCases)] int oldItem,
                                                [Random(15, 30, StandardMaxCases)] int newItem,
                                                [Random(0, 10, 1)] int startIndex,
                                                [Random(10, 20, 1)] int lastIndex)
    {
      // Make sure the test is valid.
      Assert.IsTrue(oldItem >= 0, "{0} is less than 0", nameof(oldItem));
      Assert.IsTrue(testSize > oldItem, "{0} is less than {1}", nameof(oldItem), nameof(testSize));
      Assert.AreNotEqual(oldItem, newItem, "Both items are the same.");

      // Create and fill the array.
      int[] array = new int[testSize];
      for (int i = 0; i < testSize; i++)
        array[i] = oldItem;

      // Assert that a bad value fails.
      Func<int, int, bool> badTest = (t, i) => false;
      Func<object, int, bool> badTestNG = (t, i) => false;
      Assert.IsFalse(array.ReplacePattern(newItem, badTest));
      Assert.IsFalse(array.ReplacePattern(newItem, startIndex, badTest));
      Assert.IsFalse(array.ReplacePattern(newItem, startIndex, lastIndex, badTest));
      Assert.IsFalse(array.ReplacePatternNG(newItem, badTestNG));
      Assert.IsFalse(array.ReplacePatternNG(newItem, startIndex, badTestNG));
      Assert.IsFalse(array.ReplacePatternNG(newItem, startIndex, lastIndex, badTestNG));

      // Replace the old item based on a pattern and make sure the replacement worked.
      Func<int, int, bool> goodTest = (t, i) => t == oldItem && i % 2 == 0;
      Func<object, int, bool> goodTestNG = (t, i) => (int)t == oldItem && i % 2 == 0;

      // Test the basic pattern.
      Assert.IsTrue(array.ReplacePatternNG(newItem, goodTestNG));
      for (int i = 0; i < testSize; i++)
      {
        if (i % 2 == 0)
          Assert.AreEqual(newItem, array[i]);
        else
          Assert.AreNotEqual(newItem, array[i]);
      }
      // Reset the array.
      for (int i = 0; i < testSize; i++)
        array[i] = oldItem;

      // Test the generic basic pattern.
      Assert.IsTrue(array.ReplacePatternNG(newItem, goodTestNG));
      for (int i = 0; i < testSize; i++)
      {
        if (i % 2 == 0)
          Assert.AreEqual(newItem, array[i]);
        else
          Assert.AreNotEqual(newItem, array[i]);
      }
      // Reset the array.
      for (int i = 0; i < testSize; i++)
        array[i] = oldItem;

      // Test the startIndex pattern.
      Assert.IsTrue(array.ReplacePattern(newItem, startIndex, goodTest));
      for (int i = 0; i < testSize; i++)
      {
        if (i >= startIndex && i % 2 == 0)
          Assert.AreEqual(newItem, array[i]);
        else
          Assert.AreNotEqual(newItem, array[i]);
      }
      // Reset the array.
      for (int i = 0; i < testSize; i++)
        array[i] = oldItem;

      // Test the generic startIndex pattern.
      Assert.IsTrue(array.ReplacePatternNG(newItem, startIndex, goodTestNG));
      for (int i = 0; i < testSize; i++)
      {
        if (i >= startIndex && i % 2 == 0)
          Assert.AreEqual(newItem, array[i]);
        else
          Assert.AreNotEqual(newItem, array[i]);
      }
      // Reset the array.
      for (int i = 0; i < testSize; i++)
        array[i] = oldItem;

      // Test the startIndex/lastIndex pattern.
      Assert.IsTrue(array.ReplacePattern(newItem, startIndex, lastIndex, goodTest));
      for (int i = 0; i < testSize; i++)
      {
        if (i >= startIndex && i < lastIndex && i % 2 == 0)
          Assert.AreEqual(newItem, array[i]);
        else
          Assert.AreNotEqual(newItem, array[i]);
      }
      // Reset the array.
      for (int i = 0; i < testSize; i++)
        array[i] = oldItem;

      // Test the startIndex/lastIndex pattern.
      Assert.IsTrue(array.ReplacePatternNG(newItem, startIndex, lastIndex, goodTestNG));
      for (int i = 0; i < testSize; i++)
      {
        if (i >= startIndex && i < lastIndex && i % 2 == 0)
          Assert.AreEqual(newItem, array[i]);
        else
          Assert.AreNotEqual(newItem, array[i]);
      }
    }

    /// <summary>
    /// A test for <see cref="ILists.ReplacePattern{T}(IList{T}, T, System.Func{T, int, bool})"/>,
    /// <see cref="ILists.ReplacePattern{T}(IList{T}, T, int, System.Func{T, int, bool})"/>,
    /// <see cref="ILists.ReplacePattern{T}(IList{T}, T, int, int, System.Func{T, int, bool})"/>,
    /// <see cref="ILists.ReplacePatternNG(IList, object, System.Func{object, int, bool})"/>,
    /// <see cref="ILists.ReplacePatternNG(IList, object, int, System.Func{object, int, bool})"/>,
    /// and <see cref="ILists.ReplacePatternNG(IList, object, int, int,
    /// System.Func{object, int, bool})"/>, specifically on <see cref="List{T}"/>s.
    /// </summary>
    /// <param name="testSize">The test size of the list.</param>
    /// <param name="oldItem">The old item to replace.</param>
    /// <param name="newItem">The item to replace the <paramref name="oldItem"/> with.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    [Test(TestOf = typeof(ILists))]
    public void ReplacePattern_List_ReturnsSuccess([Values(20)] int testSize,
                                                [Random(1, 15, StandardMaxCases)] int oldItem,
                                                [Random(15, 30, StandardMaxCases)] int newItem,
                                                [Random(0, 10, 1)] int startIndex,
                                                [Random(10, 20, 1)] int lastIndex)
    {
      // Make sure the test is valid.
      Assert.IsTrue(oldItem >= 0, "{0} is less than 0", nameof(oldItem));
      Assert.IsTrue(testSize > oldItem, "{0} is less than {1}", nameof(oldItem), nameof(testSize));
      Assert.AreNotEqual(oldItem, newItem, "Both items are the same.");

      // Create and fill the list.
      List<int> list = new List<int>(testSize);
      for (int i = 0; i < testSize; i++)
        list.Add(oldItem);

      // Assert that a bad value fails.
      Func<int, int, bool> badTest = (t, i) => false;
      Func<object, int, bool> badTestNG = (t, i) => false;
      Assert.IsFalse(list.ReplacePattern(newItem, badTest));
      Assert.IsFalse(list.ReplacePattern(newItem, startIndex, badTest));
      Assert.IsFalse(list.ReplacePattern(newItem, startIndex, lastIndex, badTest));
      Assert.IsFalse(list.ReplacePatternNG(newItem, badTestNG));
      Assert.IsFalse(list.ReplacePatternNG(newItem, startIndex, badTestNG));
      Assert.IsFalse(list.ReplacePatternNG(newItem, startIndex, lastIndex, badTestNG));

      // Replace the old item based on a pattern and make sure the replacement worked.
      Func<int, int, bool> goodTest = (t, i) => t == oldItem && i % 2 == 0;
      Func<object, int, bool> goodTestNG = (t, i) => (int)t == oldItem && i % 2 == 0;

      // Test the basic pattern.
      Assert.IsTrue(list.ReplacePatternNG(newItem, goodTestNG));
      for (int i = 0; i < testSize; i++)
      {
        if (i % 2 == 0)
          Assert.AreEqual(newItem, list[i]);
        else
          Assert.AreNotEqual(newItem, list[i]);
      }
      // Reset the list.
      for (int i = 0; i < testSize; i++)
        list[i] = oldItem;

      // Test the generic basic pattern.
      Assert.IsTrue(list.ReplacePatternNG(newItem, goodTestNG));
      for (int i = 0; i < testSize; i++)
      {
        if (i % 2 == 0)
          Assert.AreEqual(newItem, list[i]);
        else
          Assert.AreNotEqual(newItem, list[i]);
      }
      // Reset the list.
      for (int i = 0; i < testSize; i++)
        list[i] = oldItem;

      // Test the startIndex pattern.
      Assert.IsTrue(list.ReplacePattern(newItem, startIndex, goodTest));
      for (int i = 0; i < testSize; i++)
      {
        if (i >= startIndex && i % 2 == 0)
          Assert.AreEqual(newItem, list[i]);
        else
          Assert.AreNotEqual(newItem, list[i]);
      }
      // Reset the list.
      for (int i = 0; i < testSize; i++)
        list[i] = oldItem;

      // Test the generic startIndex pattern.
      Assert.IsTrue(list.ReplacePatternNG(newItem, startIndex, goodTestNG));
      for (int i = 0; i < testSize; i++)
      {
        if (i >= startIndex && i % 2 == 0)
          Assert.AreEqual(newItem, list[i]);
        else
          Assert.AreNotEqual(newItem, list[i]);
      }
      // Reset the list.
      for (int i = 0; i < testSize; i++)
        list[i] = oldItem;

      // Test the startIndex/lastIndex pattern.
      Assert.IsTrue(list.ReplacePattern(newItem, startIndex, lastIndex, goodTest));
      for (int i = 0; i < testSize; i++)
      {
        if (i >= startIndex && i < lastIndex && i % 2 == 0)
          Assert.AreEqual(newItem, list[i]);
        else
          Assert.AreNotEqual(newItem, list[i]);
      }
      // Reset the list.
      for (int i = 0; i < testSize; i++)
        list[i] = oldItem;

      // Test the startIndex/lastIndex pattern.
      Assert.IsTrue(list.ReplacePatternNG(newItem, startIndex, lastIndex, goodTestNG));
      for (int i = 0; i < testSize; i++)
      {
        if (i >= startIndex && i < lastIndex && i % 2 == 0)
          Assert.AreEqual(newItem, list[i]);
        else
          Assert.AreNotEqual(newItem, list[i]);
      }
    }

    /// <summary>
    /// A test for <see cref="ILists.Fill{T}(IList{T}, T, int)"/>,
    /// <see cref="ILists.Fill{T}(IList{T}, T, int, int)"/>,
    /// <see cref="ILists.Fill{T}(IList{T}, T, int, int, int)"/>,
    /// <see cref="ILists.FillNG(IList, object, int)"/>,
    /// <see cref="ILists.FillNG(IList, object, int, int)"/>,
    /// and <see cref="ILists.FillNG(IList, object, int, int, int)"/>, specifically on null arrays.
    /// </summary>
    [Test(TestOf = typeof(ILists))]
    public void Fill_NullArray_ReturnsFailure()
    {
      // Create a null array, and make sure the replacements fail.
      int[] array = null;
      Assert.IsFalse(array.Fill(10, 5));
      Assert.IsFalse(array.Fill(10, 1, 5));
      Assert.IsFalse(array.Fill(10, 0, 1, 5));
      Assert.IsFalse(array.FillNG(10, 5));
      Assert.IsFalse(array.FillNG(10, 1, 5));
      Assert.IsFalse(array.FillNG(10, 0, 1, 5));
    }

    /// <summary>
    /// A test for <see cref="ILists.Fill{T}(IList{T}, T, int)"/>,
    /// <see cref="ILists.Fill{T}(IList{T}, T, int, int)"/>,
    /// <see cref="ILists.Fill{T}(IList{T}, T, int, int, int)"/>,
    /// <see cref="ILists.FillNG(IList, object, int)"/>,
    /// <see cref="ILists.FillNG(IList, object, int, int)"/>,
    /// and <see cref="ILists.FillNG(IList, object, int, int, int)"/>, specifically on null
    /// <see cref="List{T}"/>s.
    /// </summary>
    [Test(TestOf = typeof(ILists))]
    public void Fill_NullList_ReturnsFailure()
    {
      // Create a null list, and make sure the replacements fail.
      List<int> list = null;
      Assert.IsFalse(list.Fill(10, 5));
      Assert.IsFalse(list.Fill(10, 1, 5));
      Assert.IsFalse(list.Fill(10, 0, 1, 5));
      Assert.IsFalse(list.FillNG(10, 5));
      Assert.IsFalse(list.FillNG(10, 1, 5));
      Assert.IsFalse(list.FillNG(10, 0, 1, 5));

      // Do the test again with an empty list.
      list = new List<int>(5);
      Assert.IsFalse(list.Fill(10, 5));
      Assert.IsFalse(list.Fill(10, 1, 5));
      Assert.IsFalse(list.Fill(10, 0, 1, 5));
      Assert.IsFalse(list.FillNG(10, 5));
      Assert.IsFalse(list.FillNG(10, 1, 5));
      Assert.IsFalse(list.FillNG(10, 0, 1, 5));
    }

    /// <summary>
    /// A test for <see cref="ILists.Fill{T}(IList{T}, T, int)"/>,
    /// <see cref="ILists.Fill{T}(IList{T}, T, int, int)"/>,
    /// <see cref="ILists.Fill{T}(IList{T}, T, int, int, int)"/>,
    /// <see cref="ILists.FillNG(IList, object, int)"/>,
    /// <see cref="ILists.FillNG(IList, object, int, int)"/>,
    /// and <see cref="ILists.FillNG(IList, object, int, int, int)"/>, specifically on arrays.
    /// </summary>
    /// <param name="testSize">The test size of the array.</param>
    /// <param name="value">The new value to put into the array.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <param name="skip">The number of indexes to skip between each value fill.</param>
    [Test(TestOf = typeof(ILists))]
    public void Fill_Array_ReturnsSuccess([Values(20)] int testSize,
                                          [Random(1, 15, StandardMaxCases)] int value,
                                          [Random(0, 10, 1)] int startIndex,
                                          [Random(10, 20, 1)] int lastIndex,
                                          [Random(1, 5, 5)] int skip)
    {
      // Create and fill the array.
      int oldValue = value - 5;
      int fixedSkip = skip <= 0 ? 1 : skip + 1;
      int[] array = new int[testSize];
      for (int i = 0; i < testSize; i++)
        array[i] = oldValue;

      // Assert that a bad value fails.
      Assert.IsFalse(array.Fill(value, testSize, 5));
      Assert.IsFalse(array.Fill(value, testSize, testSize + 5, 5));
      Assert.IsFalse(array.FillNG(value, testSize, 5));
      Assert.IsFalse(array.FillNG(value, testSize, testSize + 5, 5));

      // Test the basic fill.
      Assert.IsTrue(array.Fill(value));
      for (int i = 0; i < testSize; i++)
        Assert.AreEqual(value, array[i]);

      // Reset the array.
      for (int i = 0; i < testSize; i++)
        array[i] = oldValue;

      // Test the generic basic fill.
      Assert.IsTrue(array.FillNG(value));
      for (int i = 0; i < testSize; i++)
        Assert.AreEqual(value, array[i]);

      // Reset the array.
      for (int i = 0; i < testSize; i++)
        array[i] = oldValue;

      // Test the basic fill with a skip.
      Assert.IsTrue(array.Fill(value, skip));
      for (int i = 0; i < testSize; i += fixedSkip)
        Assert.AreEqual(value, array[i]);

      // Reset the array.
      for (int i = 0; i < testSize; i++)
        array[i] = oldValue;

      // Test the generic basic fill with a skip.
      Assert.IsTrue(array.FillNG(value, skip));
      for (int i = 0; i < testSize; i += fixedSkip)
        Assert.AreEqual(value, array[i]);

      // Reset the array.
      for (int i = 0; i < testSize; i++)
        array[i] = oldValue;

      // Test the startIndex fill.
      Assert.IsTrue(array.Fill(value, startIndex, skip));
      for (int i = 0; i < testSize; i += (i >= startIndex ? fixedSkip : 1))
      {
        if (i >= startIndex)
          Assert.AreEqual(value, array[i]);
        else
          Assert.AreEqual(oldValue, array[i]);
      }

      // Reset the array.
      for (int i = 0; i < testSize; i++)
        array[i] = oldValue;

      // Test the generic startIndex fill.
      Assert.IsTrue(array.FillNG(value, startIndex, skip));
      for (int i = 0; i < testSize; i += (i >= startIndex ? fixedSkip : 1))
      {
        if (i >= startIndex)
          Assert.AreEqual(value, array[i]);
        else
          Assert.AreEqual(oldValue, array[i]);
      }

      // Reset the array.
      for (int i = 0; i < testSize; i++)
        array[i] = oldValue;

      // Test the startIndex/lastIndex fill.
      Assert.IsTrue(array.Fill(value, startIndex, lastIndex, skip));
      for (int i = 0; i < testSize; i += (i >= startIndex && i < lastIndex ? fixedSkip : 1))
      {
        if (i >= startIndex && i < lastIndex)
          Assert.AreEqual(value, array[i]);
        else
          Assert.AreEqual(oldValue, array[i]);
      }

      // Reset the array.
      for (int i = 0; i < testSize; i++)
        array[i] = oldValue;

      // Test the generic startIndex/lastIndex fill.
      Assert.IsTrue(array.FillNG(value, startIndex, lastIndex, skip));
      for (int i = 0; i < testSize; i += (i >= startIndex && i < lastIndex ? fixedSkip : 1))
      {
        if (i >= startIndex && i < lastIndex)
          Assert.AreEqual(value, array[i]);
        else
          Assert.AreEqual(oldValue, array[i]);
      }
    }

    /// <summary>
    /// A test for <see cref="ILists.Fill{T}(IList{T}, T, int)"/>,
    /// <see cref="ILists.Fill{T}(IList{T}, T, int, int)"/>,
    /// <see cref="ILists.Fill{T}(IList{T}, T, int, int, int)"/>,
    /// <see cref="ILists.FillNG(IList, object, int)"/>,
    /// <see cref="ILists.FillNG(IList, object, int, int)"/>,
    /// and <see cref="ILists.FillNG(IList, object, int, int, int)"/>, specifically on
    /// <see cref="List{T}"/>s.
    /// </summary>
    /// <param name="testSize">The test size of the list.</param>
    /// <param name="value">The new value to put into the list.</param>
    /// <param name="startIndex">The starting index to handle replacing from, inclusive</param>
    /// <param name="lastIndex">The last index to handle replacing from, exclusive.</param>
    /// <param name="skip">The number of indexes to skip between each value fill.</param>
    [Test(TestOf = typeof(ILists))]
    public void Fill_List_ReturnsSuccess([Values(20)] int testSize,
                                         [Random(1, 15, StandardMaxCases)] int value,
                                         [Random(0, 10, 1)] int startIndex,
                                         [Random(10, 20, 1)] int lastIndex,
                                         [Random(1, 5, 5)] int skip)
    {
      // Create and fill the list.
      int oldValue = value - 5;
      int fixedSkip = skip <= 0 ? 1 : skip + 1;
      List<int> list = new List<int>(testSize);
      for (int i = 0; i < testSize; i++)
        list.Add(oldValue);

      // Assert that a bad value fails.
      Assert.IsFalse(list.Fill(value, testSize, 5));
      Assert.IsFalse(list.Fill(value, testSize, testSize + 5, 5));
      Assert.IsFalse(list.FillNG(value, testSize, 5));
      Assert.IsFalse(list.FillNG(value, testSize, testSize + 5, 5));

      // Test the basic fill.
      Assert.IsTrue(list.Fill(value));
      for (int i = 0; i < testSize; i++)
        Assert.AreEqual(value, list[i]);

      // Reset the list.
      for (int i = 0; i < testSize; i++)
        list[i] = oldValue;

      // Test the generic basic fill.
      Assert.IsTrue(list.FillNG(value));
      for (int i = 0; i < testSize; i++)
        Assert.AreEqual(value, list[i]);

      // Reset the list.
      for (int i = 0; i < testSize; i++)
        list[i] = oldValue;

      // Test the basic fill with a skip.
      Assert.IsTrue(list.Fill(value, skip));
      for (int i = 0; i < testSize; i += fixedSkip)
        Assert.AreEqual(value, list[i]);

      // Reset the list.
      for (int i = 0; i < testSize; i++)
        list[i] = oldValue;

      // Test the generic basic fill with a skip.
      Assert.IsTrue(list.FillNG(value, skip));
      for (int i = 0; i < testSize; i += fixedSkip)
        Assert.AreEqual(value, list[i]);

      // Reset the list.
      for (int i = 0; i < testSize; i++)
        list[i] = oldValue;

      // Test the startIndex fill.
      Assert.IsTrue(list.Fill(value, startIndex, skip));
      for (int i = 0; i < testSize; i += (i >= startIndex ? fixedSkip : 1))
      {
        if (i >= startIndex)
          Assert.AreEqual(value, list[i]);
        else
          Assert.AreEqual(oldValue, list[i]);
      }

      // Reset the list.
      for (int i = 0; i < testSize; i++)
        list[i] = oldValue;

      // Test the generic startIndex fill.
      Assert.IsTrue(list.FillNG(value, startIndex, skip));
      for (int i = 0; i < testSize; i += (i >= startIndex ? fixedSkip : 1))
      {
        if (i >= startIndex)
          Assert.AreEqual(value, list[i]);
        else
          Assert.AreEqual(oldValue, list[i]);
      }

      // Reset the list.
      for (int i = 0; i < testSize; i++)
        list[i] = oldValue;

      // Test the startIndex/lastIndex fill.
      Assert.IsTrue(list.Fill(value, startIndex, lastIndex, skip));
      for (int i = 0; i < testSize; i += (i >= startIndex && i < lastIndex ? fixedSkip : 1))
      {
        if (i >= startIndex && i < lastIndex)
          Assert.AreEqual(value, list[i]);
        else
          Assert.AreEqual(oldValue, list[i]);
      }

      // Reset the list.
      for (int i = 0; i < testSize; i++)
        list[i] = oldValue;

      // Test the generic startIndex/lastIndex fill.
      Assert.IsTrue(list.FillNG(value, startIndex, lastIndex, skip));
      for (int i = 0; i < testSize; i += (i >= startIndex && i < lastIndex ? fixedSkip : 1))
      {
        if (i >= startIndex && i < lastIndex)
          Assert.AreEqual(value, list[i]);
        else
          Assert.AreEqual(oldValue, list[i]);
      }
    }

    /// <summary>
    /// A test for <see cref="ILists.AddUnique{T}(IList{T}, T)"/> and
    /// <see cref="ILists.AddUniqueNG(IList, object)"/>, specifically on arrays. This should
    /// always fail, as <see cref="IList.Add(object)"/> does not work on them.
    /// </summary>
    /// <param name="value">The value to add to the array.</param>
    [Test(TestOf = typeof(ILists))]
    public void AddUnique_Array_ReturnsFailure([Random(1, 10, StandardMaxCases)] int value)
    {
      // Create an array. It should always throw errors.
      int[] array = null;
      Assert.IsNotNull(Assert.Catch(() => array.AddUnique(value)));
      Assert.IsNotNull(Assert.Catch(() => array.AddUniqueNG(value)));
      array = new int[5];
      Assert.IsNotNull(Assert.Catch(() => array.AddUnique(value)));
      Assert.IsNotNull(Assert.Catch(() => array.AddUniqueNG(value)));
    }

    /// <summary>
    /// A test for <see cref="ILists.AddUnique{T}(IList{T}, T)"/> and
    /// <see cref="ILists.AddUniqueNG(IList, object)"/>, specifically on <see cref="List{T}"/>s.
    /// </summary>
    /// <param name="value">The value to add to the array.</param>
    [Test(TestOf = typeof(ILists))]
    public void AddUnique_List_ReturnsAddSuccess([Random(20, 50, StandardMaxCases)] int value)
    {
      int size = 5;
      
      // Create a null list. These should be errors.
      List<int> list = null;
      Assert.IsNotNull(Assert.Catch(() => list.AddUnique(value)));
      Assert.IsNotNull(Assert.Catch(() => list.AddUniqueNG(value)));

      // Fill the list.
      list = new List<int>(size);
      for (int i = 0; i < size; i++)
        list.Add(i);

      // Test adding the value.
      Assert.IsTrue(list.AddUnique(value));
      Assert.AreEqual(list.LastElement(), value);

      // Reset the value.
      list[list.LastIndex()] = value - (value * 2);
      Assert.AreNotEqual(list.LastElement(), value);

      // Test adding the value generically.
      Assert.IsTrue(list.AddUniqueNG(value));
      Assert.AreEqual(list.LastElement(), value);
    }

    /// <summary>
    /// A test for <see cref="ILists.SetUnique{T}(IList{T}, T, int)"/> and
    /// <see cref="ILists.SetUniqueNG(IList, object, int)"/>, specifically on arrays.
    /// </summary>
    /// <param name="testSize">The test size of the array.</param>
    /// <param name="value">The value to add to the array.</param>
    /// <param name="index">The index to set the <paramref name="value"/> into.</param>
    [Test(TestOf = typeof(ILists))]
    public void SetUnique_Array_ReturnsSetSuccess([Random(5, 10, 5)] int testSize,
                                                  [Random(1, 10, StandardMaxCases)] int value,
                                                  [Random(1, 5, 5)] int index)
    {
      // Create an array. It should be an error.
      int[] array = null;
      Assert.IsNotNull(Assert.Catch(() => array.SetUnique(value, index)));
      Assert.IsNotNull(Assert.Catch(() => array.SetUniqueNG(value, index)));

      // Give the array values and test setting.
      array = new int[testSize];
      Assert.IsTrue(array.SetUnique(value, index));
      Assert.AreEqual(array[index], value);

      // Reset and test the generic version.
      array[index] = 0;
      Assert.IsTrue(array.SetUniqueNG(value, index));
      Assert.AreEqual(array[index], value);
    }

    /// <summary>
    /// A test for <see cref="ILists.SetUnique{T}(IList{T}, T, int)"/> and
    /// <see cref="ILists.SetUniqueNG(IList, object, int)"/>, specifically on
    /// <see cref="List{T}"/>s.
    /// </summary>
    /// <param name="testSize">The test size of the list.</param>
    /// <param name="value">The value to add to the list.</param>
    /// <param name="index">The index to set the <paramref name="value"/> into.</param>
    [Test(TestOf = typeof(ILists))]
    public void SetUnique_List_ReturnsSetSuccess([Random(5, 10, 5)] int testSize,
                                                  [Random(1, 10, StandardMaxCases)] int value,
                                                  [Random(1, 5, 5)] int index)
    {
      // Create an list. It should be errors.
      List<int> list = null;
      Assert.IsNotNull(Assert.Catch(() => list.SetUnique(value, index)));
      Assert.IsNotNull(Assert.Catch(() => list.SetUniqueNG(value, index)));

      // Make the list empty and test again for false.
      list = new List<int>(testSize);
      Assert.IsFalse(list.SetUnique(value, index));
      Assert.IsFalse(list.SetUniqueNG(value, index));

      // Give the list values and test setting.
      for (int i = 0; i < testSize; i++)
        list.Add(0);

      Assert.IsTrue(list.SetUnique(value, index));
      Assert.AreEqual(list[index], value);

      // Reset and test the generic version.
      list[index] = 0;
      Assert.IsTrue(list.SetUniqueNG(value, index));
      Assert.AreEqual(list[index], value);
    }

    /// <summary>
    /// A test for <see cref="ILists.InsertUnique{T}(IList{T}, T, int)"/> and
    /// <see cref="ILists.InsertUniqueNG(IList, object, int)"/>, specifically on arrays. This should
    /// always fail, as <see cref="IList.Add(object)"/> does not work on them.
    /// </summary>
    /// <param name="testSize">The test size of the array.</param>
    /// <param name="value">The value to add to the array.</param>
    /// <param name="index">The index to set the <paramref name="value"/> into.</param>
    [Test(TestOf = typeof(ILists))]
    public void InsertUnique_Array_ReturnsFailure([Random(5, 10, 5)] int testSize,
                                                  [Random(1, 10, StandardMaxCases)] int value,
                                                  [Random(1, 5, 5)] int index)
    {
      // Create an array. It should be an error.
      int[] array = null;
      Assert.IsNotNull(Assert.Catch(() => array.InsertUnique(value, index)));
      Assert.IsNotNull(Assert.Catch(() => array.InsertUniqueNG(value, index)));

      // Give the array values, which should still return errors.
      array = new int[testSize];
      Assert.IsNotNull(Assert.Catch(() => array.InsertUnique(value, index)));
      Assert.IsNotNull(Assert.Catch(() => array.InsertUniqueNG(value, index)));
    }

    /// <summary>
    /// A test for <see cref="ILists.InsertUnique{T}(IList{T}, T, int)"/> and
    /// <see cref="ILists.InsertUniqueNG(IList, object, int)"/>, specifically on
    /// <see cref="List{T}"/>s.
    /// </summary>
    /// <param name="testSize">The test size of the list.</param>
    /// <param name="value">The value to add to the list.</param>
    /// <param name="index">The index to set the <paramref name="value"/> into.</param>
    [Test(TestOf = typeof(ILists))]
    public void InsertUnique_List_ReturnsSetSuccess([Random(5, 10, 5)] int testSize,
                                                    [Random(1, 10, StandardMaxCases)] int value,
                                                    [Random(1, 5, 5)] int index)
    {
      // Create an list. It should be errors.
      List<int> list = null;
      Assert.IsNotNull(Assert.Catch(() => list.InsertUnique(value, index)));
      Assert.IsNotNull(Assert.Catch(() => list.InsertUniqueNG(value, index)));

      // Make the list empty and test again for false.
      list = new List<int>(testSize);
      Assert.IsFalse(list.InsertUnique(value, index));
      Assert.IsFalse(list.InsertUniqueNG(value, index));

      // Give the list values and test setting.
      for (int i = 0; i < testSize; i++)
        list.Add(0);

      Assert.IsTrue(list.InsertUnique(value, index));
      Assert.AreEqual(list[index], value);
      Assert.AreEqual(list.Count, testSize + 1);

      // Reset and test the generic version.
      list.RemoveAt(index);

      Assert.IsTrue(list.InsertUniqueNG(value, index));
      Assert.AreEqual(list[index], value);
      Assert.AreEqual(list.Count, testSize + 1);
    }
  }
  /************************************************************************************************/
}