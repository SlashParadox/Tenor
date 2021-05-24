using MiscUtil.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace CodeParadox.Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// A class for dealing with Unicode characters, based on the Unicode standard and what is
  /// available in .NET.
  /// </summary>
  public static partial class Unicode
  {
    /// <summary>
    /// A <see cref="Dictionary{TKey, TValue}"/> of every Unicode Named Block that is compatible
    /// with .NET. Please refer to the
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// .NET Documentation</a> for more details.
    /// </summary>
    public static readonly Dictionary<UnicodeBlockType, UnicodeBlock> NamedBlocks = new Dictionary<UnicodeBlockType, UnicodeBlock>()
    {
      {UnicodeBlockType.IsBasicLatin, new UnicodeBlock("IsBasicLatin", '\u0000', '\u007F') },
      {UnicodeBlockType.IsLatin_1Supplement, new UnicodeBlock("IsLatin-1Supplement", '\u0080', '\u00FF') },
      {UnicodeBlockType.IsLatinExtended_A, new UnicodeBlock("IsLatinExtended-A", '\u0100', '\u017F') },
      {UnicodeBlockType.IsLatinExtended_B, new UnicodeBlock("IsLatinExtended-B", '\u0180', '\u024F') },
      {UnicodeBlockType.IsIPAExtensions, new UnicodeBlock("IsIPAExtensions", '\u0250', '\u02AF') },
      {UnicodeBlockType.IsSpacingModifierLetters, new UnicodeBlock("IsSpacingModifierLetters", '\u02B0', '\u02FF') },
      {UnicodeBlockType.IsCombiningDiacriticalMarks, new UnicodeBlock("IsCombiningDiacriticalMarks", '\u0300', '\u036F') },
      {UnicodeBlockType.IsGreekandCoptic, new UnicodeBlock("IsGreekandCoptic", '\u0370', '\u03FF') },
      {UnicodeBlockType.IsCyrillic, new UnicodeBlock("IsCyrillic", '\u0400', '\u04FF') },
      {UnicodeBlockType.IsCyrillicSupplement, new UnicodeBlock("IsCyrillicSupplement", '\u0500', '\u052F') },
      {UnicodeBlockType.IsArmenian, new UnicodeBlock("IsArmenian", '\u0530', '\u058F') },
      {UnicodeBlockType.IsHebrew, new UnicodeBlock("IsHebrew", '\u0590', '\u05FF') },
      {UnicodeBlockType.IsArabic, new UnicodeBlock("IsArabic", '\u0600', '\u06FF') },
      {UnicodeBlockType.IsSyriac, new UnicodeBlock("IsSyriac", '\u0700', '\u074F') },
      {UnicodeBlockType.IsThaana, new UnicodeBlock("IsThaana", '\u0780', '\u07BF') },
      {UnicodeBlockType.IsDevanagari, new UnicodeBlock("IsDevanagari", '\u0900', '\u097F') },
      {UnicodeBlockType.IsBengali, new UnicodeBlock("IsBengali", '\u0980', '\u09FF') },
      {UnicodeBlockType.IsGurmukhi, new UnicodeBlock("IsGurmukhi", '\u0A00', '\u0A7F') },
      {UnicodeBlockType.IsGujarati, new UnicodeBlock("IsGujarati", '\u0A80', '\u0AFF') },
      {UnicodeBlockType.IsOriya, new UnicodeBlock("IsOriya", '\u0B00', '\u0B7F') },
      {UnicodeBlockType.IsTamil, new UnicodeBlock("IsTamil", '\u0B80', '\u0BFF') },
      {UnicodeBlockType.IsTelugu, new UnicodeBlock("IsTelugu", '\u0C00', '\u0C7F') },
      {UnicodeBlockType.IsKannada, new UnicodeBlock("IsKannada", '\u0C80', '\u0CFF') },
      {UnicodeBlockType.IsMalayalam, new UnicodeBlock("IsMalayalam", '\u0D00', '\u0D7F') },
      {UnicodeBlockType.IsSinhala, new UnicodeBlock("IsSinhala", '\u0D80', '\u0DFF') },
      {UnicodeBlockType.IsThai, new UnicodeBlock("IsThai", '\u0E00', '\u0E7F') },
      {UnicodeBlockType.IsLao, new UnicodeBlock("IsLao", '\u0E80', '\u0EFF') },
      {UnicodeBlockType.IsTibetan, new UnicodeBlock("IsTibetan", '\u0F00', '\u0FFF') },
      {UnicodeBlockType.IsMyanmar, new UnicodeBlock("IsMyanmar", '\u1000', '\u109F') },
      {UnicodeBlockType.IsGeorgian, new UnicodeBlock("IsGeorgian", '\u10A0', '\u10FF') },
      {UnicodeBlockType.IsHangulJamo, new UnicodeBlock("IsHangulJamo", '\u1100', '\u11FF') },
      {UnicodeBlockType.IsEthiopic, new UnicodeBlock("IsEthiopic", '\u1200', '\u137F') },
      {UnicodeBlockType.IsCherokee, new UnicodeBlock("IsCherokee", '\u13A0', '\u13FF') },
      {UnicodeBlockType.IsUnifiedCanadianAboriginalSyllabics, new UnicodeBlock("IsUnifiedCanadianAboriginalSyllabics", '\u1400', '\u167F') },
      {UnicodeBlockType.IsOgham, new UnicodeBlock("IsOgham", '\u1680', '\u169F') },
      {UnicodeBlockType.IsRunic, new UnicodeBlock("IsRunic", '\u16A0', '\u16FF') },
      {UnicodeBlockType.IsTagalog, new UnicodeBlock("IsTagalog", '\u1700', '\u171F') },
      {UnicodeBlockType.IsHanunoo, new UnicodeBlock("IsHanunoo", '\u1720', '\u173F') },
      {UnicodeBlockType.IsBuhid, new UnicodeBlock("IsBuhid", '\u1740', '\u175F') },
      {UnicodeBlockType.IsTagbanwa, new UnicodeBlock("IsTagbanwa", '\u1760', '\u177F') },
      {UnicodeBlockType.IsKhmer, new UnicodeBlock("IsKhmer", '\u1780', '\u17FF') },
      {UnicodeBlockType.IsMongolian, new UnicodeBlock("IsMongolian", '\u1800', '\u18AF') },
      {UnicodeBlockType.IsLimbu, new UnicodeBlock("IsLimbu", '\u1900', '\u194F') },
      {UnicodeBlockType.IsTaiLe, new UnicodeBlock("IsTaiLe", '\u1950', '\u197F') },
      {UnicodeBlockType.IsKhmerSymbols, new UnicodeBlock("IsKhmerSymbols", '\u19E0', '\u19FF') },
      {UnicodeBlockType.IsPhoneticExtensions, new UnicodeBlock("IsPhoneticExtensions", '\u1D00', '\u1D7F') },
      {UnicodeBlockType.IsLatinExtendedAdditional, new UnicodeBlock("IsLatinExtendedAdditional", '\u1E00', '\u1EFF') },
      {UnicodeBlockType.IsGreekExtended, new UnicodeBlock("IsGreekExtended", '\u1F00', '\u1FFF') },
      {UnicodeBlockType.IsGeneralPunctuation, new UnicodeBlock("IsGeneralPunctuation", '\u2000', '\u206F') },
      {UnicodeBlockType.IsSuperscriptsandSubscripts, new UnicodeBlock("IsSuperscriptsandSubscripts", '\u2070', '\u209F') },
      {UnicodeBlockType.IsCurrencySymbols, new UnicodeBlock("IsCurrencySymbols", '\u20A0', '\u20CF') },
      {UnicodeBlockType.IsCombiningMarksforSymbols, new UnicodeBlock("IsCombiningMarksforSymbols", '\u20D0', '\u20FF') },
      {UnicodeBlockType.IsLetterlikeSymbols, new UnicodeBlock("IsLetterlikeSymbols", '\u2100', '\u214F') },
      {UnicodeBlockType.IsNumberForms, new UnicodeBlock("IsNumberForms", '\u2150', '\u218F') },
      {UnicodeBlockType.IsArrows, new UnicodeBlock("IsArrows", '\u2190', '\u21FF') },
      {UnicodeBlockType.IsMathematicalOperators, new UnicodeBlock("IsMathematicalOperators", '\u2200', '\u22FF') },
      {UnicodeBlockType.IsMiscellaneousTechnical, new UnicodeBlock("IsMiscellaneousTechnical", '\u2300', '\u23FF') },
      {UnicodeBlockType.IsControlPictures, new UnicodeBlock("IsControlPictures", '\u2400', '\u243F') },
      {UnicodeBlockType.IsOpticalCharacterRecognition, new UnicodeBlock("IsOpticalCharacterRecognition", '\u2440', '\u245F') },
      {UnicodeBlockType.IsEnclosedAlphanumerics, new UnicodeBlock("IsEnclosedAlphanumerics", '\u2460', '\u24FF') },
      {UnicodeBlockType.IsBoxDrawing, new UnicodeBlock("IsBoxDrawing", '\u2500', '\u257F') },
      {UnicodeBlockType.IsBlockElements, new UnicodeBlock("IsBlockElements", '\u2580', '\u259F') },
      {UnicodeBlockType.IsGeometricShapes, new UnicodeBlock("IsGeometricShapes", '\u25A0', '\u25FF') },
      {UnicodeBlockType.IsMiscellaneousSymbols, new UnicodeBlock("IsMiscellaneousSymbols", '\u2600', '\u26FF') },
      {UnicodeBlockType.IsDingbats, new UnicodeBlock("IsDingbats", '\u2700', '\u27BF') },
      {UnicodeBlockType.IsMiscellaneousMathematicalSymbols_A, new UnicodeBlock("IsMiscellaneousMathematicalSymbols-A", '\u27C0', '\u27EF') },
      {UnicodeBlockType.IsSupplementalArrows_A, new UnicodeBlock("IsSupplementalArrows-A", '\u27F0', '\u27FF') },
      {UnicodeBlockType.IsBraillePatterns, new UnicodeBlock("IsBraillePatterns", '\u2800', '\u28FF') },
      {UnicodeBlockType.IsSupplementalArrows_B, new UnicodeBlock("IsSupplementalArrows-B", '\u2900', '\u297F') },
      {UnicodeBlockType.IsMiscellaneousMathematicalSymbols_B, new UnicodeBlock("IsMiscellaneousMathematicalSymbols-B", '\u2980', '\u29FF') },
      {UnicodeBlockType.IsSupplementalMathematicalOperators, new UnicodeBlock("IsSupplementalMathematicalOperators", '\u2A00', '\u2AFF') },
      {UnicodeBlockType.IsMiscellaneousSymbolsandArrows, new UnicodeBlock("IsMiscellaneousSymbolsandArrows", '\u2B00', '\u2BFF') },
      {UnicodeBlockType.IsCJKRadicalsSupplement, new UnicodeBlock("IsCJKRadicalsSupplement", '\u2E80', '\u2EFF') },
      {UnicodeBlockType.IsKangxiRadicals, new UnicodeBlock("IsKangxiRadicals", '\u2F00', '\u2FDF') },
      {UnicodeBlockType.IsIdeographicDescriptionCharacters, new UnicodeBlock("IsIdeographicDescriptionCharacters", '\u2FF0', '\u2FFF') },
      {UnicodeBlockType.IsCJKSymbolsandPunctuation, new UnicodeBlock("IsCJKSymbolsandPunctuation", '\u3000', '\u303F') },
      {UnicodeBlockType.IsHiragana, new UnicodeBlock("IsHiragana", '\u3040', '\u309F') },
      {UnicodeBlockType.IsKatakana, new UnicodeBlock("IsKatakana", '\u30A0', '\u30FF') },
      {UnicodeBlockType.IsBopomofo, new UnicodeBlock("IsBopomofo", '\u3100', '\u312F') },
      {UnicodeBlockType.IsHangulCompatibilityJamo, new UnicodeBlock("IsHangulCompatibilityJamo", '\u3130', '\u318F') },
      {UnicodeBlockType.IsKanbun, new UnicodeBlock("IsKanbun", '\u3190', '\u319F') },
      {UnicodeBlockType.IsBopomofoExtended, new UnicodeBlock("IsBopomofoExtended", '\u31A0', '\u31BF') },
      {UnicodeBlockType.IsKatakanaPhoneticExtensions, new UnicodeBlock("IsKatakanaPhoneticExtensions", '\u31F0', '\u31FF') },
      {UnicodeBlockType.IsEnclosedCJKLettersandMonths, new UnicodeBlock("IsEnclosedCJKLettersandMonths", '\u3200', '\u32FF') },
      {UnicodeBlockType.IsCJKCompatibility, new UnicodeBlock("IsCJKCompatibility", '\u3300', '\u33FF') },
      {UnicodeBlockType.IsCJKUnifiedIdeographsExtensionA, new UnicodeBlock("IsCJKUnifiedIdeographsExtensionA", '\u3400', '\u4DBF') },
      {UnicodeBlockType.IsYijingHexagramSymbols, new UnicodeBlock("IsYijingHexagramSymbols", '\u4DC0', '\u4DFF') },
      {UnicodeBlockType.IsCJKUnifiedIdeographs, new UnicodeBlock("IsCJKUnifiedIdeographs", '\u4E00', '\u9FFF') },
      {UnicodeBlockType.IsYiSyllables, new UnicodeBlock("IsYiSyllables", '\uA000', '\uA48F') },
      {UnicodeBlockType.IsYiRadicals, new UnicodeBlock("IsYiRadicals", '\uA490', '\uA4CF') },
      {UnicodeBlockType.IsHangulSyllables, new UnicodeBlock("IsHangulSyllables", '\uAC00', '\uD7AF') },
      {UnicodeBlockType.IsHighSurrogates, new UnicodeBlock("IsHighSurrogates", '\uD800', '\uDB7F') },
      {UnicodeBlockType.IsHighPrivateUseSurrogates, new UnicodeBlock("IsHighPrivateUseSurrogates", '\uDB80', '\uDBFF') },
      {UnicodeBlockType.IsLowSurrogates, new UnicodeBlock("IsLowSurrogates", '\uDC00', '\uDFFF') },
      {UnicodeBlockType.IsPrivateUseArea, new UnicodeBlock("IsPrivateUseArea", '\uE000', '\uF8FF') },
      {UnicodeBlockType.IsCJKCompatibilityIdeographs, new UnicodeBlock("IsCJKCompatibilityIdeographs", '\uF900', '\uFAFF') },
      {UnicodeBlockType.IsAlphabeticPresentationForms, new UnicodeBlock("IsAlphabeticPresentationForms", '\uFB00', '\uFB4F') },
      {UnicodeBlockType.IsArabicPresentationForms_A, new UnicodeBlock("IsArabicPresentationForms-A", '\uFB50', '\uFDFF') },
      {UnicodeBlockType.IsVariationSelectors, new UnicodeBlock("IsVariationSelectors", '\uFE00', '\uFE0F') },
      {UnicodeBlockType.IsCombiningHalfMarks, new UnicodeBlock("IsCombiningHalfMarks", '\uFE20', '\uFE2F') },
      {UnicodeBlockType.IsCJKCompatibilityForms, new UnicodeBlock("IsCJKCompatibilityForms", '\uFE30', '\uFE4F') },
      {UnicodeBlockType.IsSmallFormVariants, new UnicodeBlock("IsSmallFormVariants", '\uFE50', '\uFE6F') },
      {UnicodeBlockType.IsArabicPresentationForms_B, new UnicodeBlock("IsArabicPresentationForms-B", '\uFE70', '\uFEFF') },
      {UnicodeBlockType.IsHalfwidthandFullwidthForms, new UnicodeBlock("IsHalfwidthandFullwidthForms", '\uFF00', '\uFFEF') },
      {UnicodeBlockType.IsSpecials, new UnicodeBlock("IsSpecials", '\uFFF0', '\uFFFF') },
    };

    /// <summary>The separation character used in the <see cref="UnicodeBlockType"/>s.</summary>
    private static readonly char EnumSeparator = '_';
    /// <summary>The separation character officially used for named blocks.</summary>
    private static readonly char NamedBlockSeparator = '-';

    /// <summary>
    /// A helper function for converting a <see cref="UnicodeBlockType"/> into its proper
    /// named block <see cref="string"/>.
    /// </summary>
    /// <param name="type">The <see cref="UnicodeBlockType"/> to convert.</param>
    /// <returns>Returns the properly formatted <see cref="string"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string AsBlockString(this UnicodeBlockType type)
    {
      //UnicodeRange.AlphabeticPresentationForms.st
      return type.ToString().Replace(EnumSeparator, NamedBlockSeparator);
    }
  }
  /************************************************************************************************/
}