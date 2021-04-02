using NUnit.Framework;
using System.Text.RegularExpressions;
using Tenor.Tools.Text;
using Tenor.TestTools;

public class UT_RegexTools
{
  [TestCategory("Tenor", "Tools", "Text")]
  [Test(Author = "Craig Williams", Description = "A test for REGEX Numeric strings.", TestOf = typeof(Regexes))]
  public void TestRegexNumeric()
  {
    Assert.IsTrue(Regex.IsMatch("45000", Regexes.RegexNumeric));
    Assert.IsFalse(Regex.IsMatch("45,000", Regexes.RegexNumeric));
    Assert.IsTrue(Regex.IsMatch("-450", Regexes.RegexNumeric));
    Assert.IsFalse(Regex.IsMatch("45,000", Regexes.RegexNumeric));
    Assert.IsTrue(Regex.IsMatch("-450.53", Regexes.RegexNumeric));
    Assert.IsFalse(Regex.IsMatch("45000.25.15", Regexes.RegexNumeric));
  }

  [TestCategory("Tenor", "Tools", "Text")]
  [Test(Author = "Craig Williams", Description = "A test for REGEX Numerically Grouped strings.", TestOf = typeof(Regexes))]
  public void TestRegexNumericGroup()
  {
    Assert.IsTrue(Regex.IsMatch("45,000", Regexes.RegexNumericGroup));
    Assert.IsFalse(Regex.IsMatch("45000", Regexes.RegexNumericGroup));
    Assert.IsTrue(Regex.IsMatch("45,000,000", Regexes.RegexNumericGroup));
    Assert.IsFalse(Regex.IsMatch("450,00", Regexes.RegexNumericGroup));
    Assert.IsTrue(Regex.IsMatch("-45,000.5352", Regexes.RegexNumericGroup));
    Assert.IsFalse(Regex.IsMatch("45,000.252.25", Regexes.RegexNumericGroup));
  }
}
