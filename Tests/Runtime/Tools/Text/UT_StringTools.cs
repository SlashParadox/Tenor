using NUnit.Framework;
using Tenor.TestTools;
using System.Text;
using Tenor.Tools.Text;
using Tenor.Tools.Debug;

public class UT_StringTools
{
  [TestCategory("Tenor", "Extensions")]
  [Test(Author = "Craig Williams", Description = "A test for checking the parse of numeric integral strings.", TestOf = typeof(Strings))]
  public void TestStringIsIntegral()
  {
    Assert.IsTrue(("45").IsIntegral());
    Assert.IsTrue(("450 ").IsIntegral());
    Assert.IsFalse(("c45").IsIntegral());
    Assert.IsFalse(("45.5").IsIntegral());
    Assert.IsFalse(("45.82").IsIntegral());
    Assert.IsTrue(("-45").IsIntegral());
  }

  [TestCategory("Tenor", "Extensions")]
  [Test(Author = "Craig Williams", Description = "A test for checking the parse of numeric floating-point strings.", TestOf = typeof(Strings))]
  public void TestStringIsFloatingPoint()
  {
    Assert.IsTrue(("45").IsFloatingPoint());
    Assert.IsTrue(("450").IsFloatingPoint());
    Assert.IsFalse(("c45").IsFloatingPoint());
    Assert.IsTrue(("45.5").IsFloatingPoint());
    Assert.IsTrue(("45.82").IsFloatingPoint());
    Assert.IsTrue((" 45.82").IsFloatingPoint());
    Assert.IsTrue(("-45").IsFloatingPoint());
  }

  [TestCategory("Tenor", "Extensions")]
  [Test(Author = "Craig Williams", Description = "A test for checking the parse of numeric strings.", TestOf = typeof(Strings))]
  public void TestStringIsNumeric()
  {
    Assert.IsTrue(("45").IsNumeric());
    Assert.IsTrue(("450").IsNumeric());
    Assert.IsFalse(("c45").IsNumeric());
    Assert.IsTrue(("45.5").IsNumeric());
    Assert.IsTrue(("45.82").IsNumeric());
    Assert.IsTrue(("-45").IsNumeric());
  }

  [TestCategory("Tenor", "Extensions")]
  [Test(Author = "Craig Williams", Description = "A test for checking the parse of boolean strings.", TestOf = typeof(Strings))]
  public void TestStringIsBoolean()
  {
    Assert.IsTrue(("True").IsBoolean());
    Assert.IsTrue(("true").IsBoolean());
    Assert.IsTrue(("False").IsBoolean());
    Assert.IsTrue(("false").IsBoolean());
    Assert.IsTrue(("      true").IsBoolean());
    Assert.IsTrue(("false        ").IsBoolean());
    Assert.IsFalse(("_false").IsBoolean());
    Assert.IsFalse(("1").IsBoolean());
  }

  [TestCategory("Tenor", "Extensions")]
  [Test(Author = "Craig Williams", Description = "A test for checking the validity of StringBuilders.", TestOf = typeof(Strings))]
  public void TestStringBuilderValidity()
  {
    StringBuilder testStrb = null;
    Assert.IsTrue(testStrb.IsNullOrEmpty(), "The uninitialized StringBuilder is not null.");
    Assert.IsTrue(testStrb.IsNullOrWhiteSpace(), "The uninitialized StringBuilder is not null.");
    Assertion.ThrowsAny(() => testStrb.IsEmpty(), "The uninitialized StringBuilder did not throw an error.");
    Assertion.ThrowsAny(() => testStrb.IsWhiteSpace(), "The uninitialized StringBuilder did not throw an error.");

    testStrb = new StringBuilder();
    Assert.IsTrue(testStrb.IsNullOrEmpty(), "The StringBuilder is not empty.");
    Assert.IsTrue(testStrb.IsNullOrWhiteSpace(), "The StringBuilder is not empty.");
    Assert.IsTrue(testStrb.IsEmpty(), "The StringBuilder is not empty.");
    Assert.IsTrue(testStrb.IsWhiteSpace(), "The StringBuilder is considered whitespace.");

    testStrb.Append("  ");
    Assert.IsFalse(testStrb.IsNullOrEmpty(), "The StringBuilder is considered empty.");
    Assert.IsTrue(testStrb.IsNullOrWhiteSpace(), "The StringBuilder is not whitespace.");
    Assert.IsFalse(testStrb.IsEmpty(), "The StringBuilder is not empty.");
    Assert.IsTrue(testStrb.IsWhiteSpace(), "The StringBuilder is not considered whitespace.");

    testStrb.Append("Hello");
    Assert.IsFalse(testStrb.IsNullOrEmpty(), "The StringBuilder is considered empty.");
    Assert.IsFalse(testStrb.IsNullOrWhiteSpace(), "The StringBuilder is considered whitespace.");
    Assert.IsFalse(testStrb.IsEmpty(), "The StringBuilder is not empty.");
    Assert.IsFalse(testStrb.IsWhiteSpace(), "The StringBuilder is not considered whitespace.");
  }
}