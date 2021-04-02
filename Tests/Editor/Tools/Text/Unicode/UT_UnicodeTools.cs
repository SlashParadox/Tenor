using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Tenor.TestTools;
using Tenor.Tools.Text;
public class UT_UnicodeTools
{
  [TestCategory("Tenor", "Tools", "Unicode")]
  [Test(Author = "Craig Williams", Description = "A test for the Unicode Named Block dictionary values.", TestOf = typeof(Unicode))]
  public void TestUnicodeNamedBlockDictionary()
  {
    Assert.AreEqual(Enum.GetValues(typeof(UnicodeNamedBlockType)).Length, Unicode.NamedBlocks.Count, "Not all values are in the dictionary.");

    // Iterate through every pair.
    foreach (KeyValuePair<UnicodeNamedBlockType, UnicodeNamedBlock> pair in Unicode.NamedBlocks)
    {
      UnicodeNamedBlockType type = pair.Key;
      UnicodeNamedBlock block = pair.Value;

      Assert.AreEqual(type.NamedBlockToString(), block.name, "{0} does not have the same name as the block. Check {1} and {2}.", nameof(type), nameof(Unicode.NamedBlockToString), nameof(Unicode.NamedBlocks));

      for (char c = block.codeRangeStart; c < block.codeRangeEnd; c++)
      {
        string charType = "";
        UnicodeCategoryType category = Unicode.GetGenericCategoryType(c);

        switch (category)
        {
          case UnicodeCategoryType.L:
            charType = "Letter";
            break;
          case UnicodeCategoryType.M:
            charType = "Mark";
            break;
          case UnicodeCategoryType.N:
            charType = "Number";
            break;
          case UnicodeCategoryType.P:
            charType = "Punctuation";
            break;
          case UnicodeCategoryType.Z:
            charType = "Separator";
            break;
          case UnicodeCategoryType.S:
            charType = "Symbol";
            break;
          default:
            charType = "Control/Other";
            break;
        }

        Assert.IsTrue(System.Text.RegularExpressions.Regex.IsMatch(c.ToString(), Tenor.Tools.Text.Regexes.CreatePerfectMatch(Tenor.Tools.Text.Regexes.CreateAlphabet(type))), "{0}: {1} [{2}] [Code: {3}] is not in the range of {4}-{5}", type, charType, c, string.Format("U+{0:X4}", (int)c), string.Format("U+{0:X4}", (int)block.codeRangeStart), string.Format("U+{0:X4}", (int)block.codeRangeEnd));
        Assert.IsTrue(System.Text.RegularExpressions.Regex.IsMatch(c.ToString(), Tenor.Tools.Text.Regexes.CreatePerfectMatch(Tenor.Tools.Text.Regexes.CreateAlphabet(category))), "{0}: {1} [{2}] [Code: {3}] is not in the range of {4}-{5}", type, charType, c, string.Format("U+{0:X4}", (int)c), string.Format("U+{0:X4}", (int)block.codeRangeStart), string.Format("U+{0:X4}", (int)block.codeRangeEnd));
        Assert.IsTrue(System.Text.RegularExpressions.Regex.IsMatch(c.ToString(), Tenor.Tools.Text.Regexes.CreatePerfectMatch(Tenor.Tools.Text.Regexes.CreateAlphabet(new UnicodeNamedBlockType[] { type }))), "{0}: {1} [{2}] [Code: {3}] is not in the range of {4}-{5}", type, charType, c, string.Format("U+{0:X4}", (int)c), string.Format("U+{0:X4}", (int)block.codeRangeStart), string.Format("U+{0:X4}", (int)block.codeRangeEnd));
        Assert.IsTrue(System.Text.RegularExpressions.Regex.IsMatch(c.ToString(), Tenor.Tools.Text.Regexes.CreatePerfectMatch(Tenor.Tools.Text.Regexes.CreateAlphabet(new UnicodeCategoryType[] { category }))), "{0}: {1} [{2}] [Code: {3}] is not in the range of {4}-{5}", type, charType, c, string.Format("U+{0:X4}", (int)c), string.Format("U+{0:X4}", (int)block.codeRangeStart), string.Format("U+{0:X4}", (int)block.codeRangeEnd));
        Assert.IsFalse(System.Text.RegularExpressions.Regex.IsMatch(c.ToString(), Tenor.Tools.Text.Regexes.CreatePerfectMatch(Tenor.Tools.Text.Regexes.CreateAlphabet(type, category))), "{0}: {1} [{2}] [Code: {3}] is not in the range of {4}-{5}", type, charType, c, string.Format("U+{0:X4}", (int)c), string.Format("U+{0:X4}", (int)block.codeRangeStart), string.Format("U+{0:X4}", (int)block.codeRangeEnd));
        Assert.IsFalse(System.Text.RegularExpressions.Regex.IsMatch(c.ToString(), Tenor.Tools.Text.Regexes.CreatePerfectMatch(Tenor.Tools.Text.Regexes.CreateAlphabet(type, new UnicodeCategoryType[] { category }))), "{0}: {1} [{2}] [Code: {3}] is not in the range of {4}-{5}", type, charType, c, string.Format("U+{0:X4}", (int)c), string.Format("U+{0:X4}", (int)block.codeRangeStart), string.Format("U+{0:X4}", (int)block.codeRangeEnd));
        Assert.IsFalse(System.Text.RegularExpressions.Regex.IsMatch(c.ToString(), Tenor.Tools.Text.Regexes.CreatePerfectMatch(Tenor.Tools.Text.Regexes.CreateAlphabet(new UnicodeNamedBlockType[] { type }, category))), "{0}: {1} [{2}] [Code: {3}] is not in the range of {4}-{5}", type, charType, c, string.Format("U+{0:X4}", (int)c), string.Format("U+{0:X4}", (int)block.codeRangeStart), string.Format("U+{0:X4}", (int)block.codeRangeEnd));
        Assert.IsFalse(System.Text.RegularExpressions.Regex.IsMatch(c.ToString(), Tenor.Tools.Text.Regexes.CreatePerfectMatch(Tenor.Tools.Text.Regexes.CreateAlphabet(new UnicodeNamedBlockType[] { type }, new UnicodeCategoryType[] { category }))), "{0}: {1} [{2}] [Code: {3}] is not in the range of {4}-{5}", type, charType, c, string.Format("U+{0:X4}", (int)c), string.Format("U+{0:X4}", (int)block.codeRangeStart), string.Format("U+{0:X4}", (int)block.codeRangeEnd));
      }
    }
  }
}