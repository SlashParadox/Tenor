using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tenor.TestTools
{
  /// <summary>
  /// An enhanced version of <see cref="NUnit"/>'s <see cref="NUnit.Framework.CategoryAttribute"/>. This
  /// version allows taking in an array of category names in order to form a full category.
  /// This makes formatting for dropdowns in the <see cref="UnityEngine.TestRunner"/> much easier.
  /// </summary>
  public class TestCategoryAttribute : NUnit.Framework.CategoryAttribute
  {
    private const string CategorySeparator = "/";

    /// <summary>
    /// The default constructor for a <see cref="TestCategoryAttribute"/>.
    /// </summary>
    protected TestCategoryAttribute() : base() { }

    /// <summary>
    /// The constructor for a <see cref="TestCategoryAttribute"/>, taking in just one category.
    /// </summary>
    /// <param name="category">The category of this test.</param>
    public TestCategoryAttribute(string category)
    {
      categoryName = category != null ? category : string.Empty;
    }

    /// <summary>
    /// The constructor for a <see cref="TestCategoryAttribute"/>, taking in multiple categories.
    /// </summary>
    /// <param name="categories">The category names to give the test.</param>
    public TestCategoryAttribute(params string[] categories)
    {
      // Make sure there are categories.
      if (categories != null && categories.Length > 0)
      {
        int count = categories.Length; // Get the count.
        categoryName = categories[0]; // Append the first category.

        // Append a separator and all other categories.
        for (int i = 1; i < count; i++)
          categoryName += CategorySeparator + categories[i];
      }
    }
  }
}
