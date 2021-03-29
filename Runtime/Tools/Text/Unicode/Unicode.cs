/**************************************************************************************************/
/*!
\file   Unicode.cs
\author Craig Williams
\par    Last Updated
        2021-03-07
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for tool functions related to Unicode characters.

\par Bug List

\par References
*/

/**************************************************************************************************/
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Tenor.Tools.Text
{
  /************************************************************************************************/
  /// <summary>
  /// A series of useful functions for dealing with Unicode characters. This can be used for
  /// several purposes, such as <see cref="System.Text.RegularExpressions.Regex"/> or localization.
  /// </summary>
  public static partial class Unicode
  {
    /// <summary>
    /// A Dictionary of every Unicode Named Block that is compatibile with .NET. Please refer to the
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// .NET Documentation</a> for more details.
    /// </summary>
    public static readonly Dictionary<UnicodeNamedBlockType, UnicodeNamedBlock> NamedBlocks = new Dictionary<UnicodeNamedBlockType, UnicodeNamedBlock>()
    {
      {UnicodeNamedBlockType.IsBasicLatin, new UnicodeNamedBlock("IsBasicLatin", '\u0000', '\u007F') },
      {UnicodeNamedBlockType.IsLatin_1Supplement, new UnicodeNamedBlock("IsLatin-1Supplement", '\u0080', '\u00FF') },
      {UnicodeNamedBlockType.IsLatinExtended_A, new UnicodeNamedBlock("IsLatinExtended-A", '\u0100', '\u017F') },
      {UnicodeNamedBlockType.IsLatinExtended_B, new UnicodeNamedBlock("IsLatinExtended-B", '\u0180', '\u024F') },
      {UnicodeNamedBlockType.IsIPAExtensions, new UnicodeNamedBlock("IsIPAExtensions", '\u0250', '\u02AF') },
      {UnicodeNamedBlockType.IsSpacingModifierLetters, new UnicodeNamedBlock("IsSpacingModifierLetters", '\u02B0', '\u02FF') },
      {UnicodeNamedBlockType.IsCombiningDiacriticalMarks, new UnicodeNamedBlock("IsCombiningDiacriticalMarks", '\u0300', '\u036F') },
      {UnicodeNamedBlockType.IsGreekandCoptic, new UnicodeNamedBlock("IsGreekandCoptic", '\u0370', '\u03FF') },
      {UnicodeNamedBlockType.IsCyrillic, new UnicodeNamedBlock("IsCyrillic", '\u0400', '\u04FF') },
      {UnicodeNamedBlockType.IsCyrillicSupplement, new UnicodeNamedBlock("IsCyrillicSupplement", '\u0500', '\u052F') },
      {UnicodeNamedBlockType.IsArmenian, new UnicodeNamedBlock("IsArmenian", '\u0530', '\u058F') },
      {UnicodeNamedBlockType.IsHebrew, new UnicodeNamedBlock("IsHebrew", '\u0590', '\u05FF') },
      {UnicodeNamedBlockType.IsArabic, new UnicodeNamedBlock("IsArabic", '\u0600', '\u06FF') },
      {UnicodeNamedBlockType.IsSyriac, new UnicodeNamedBlock("IsSyriac", '\u0700', '\u074F') },
      {UnicodeNamedBlockType.IsThaana, new UnicodeNamedBlock("IsThaana", '\u0780', '\u07BF') },
      {UnicodeNamedBlockType.IsDevanagari, new UnicodeNamedBlock("IsDevanagari", '\u0900', '\u097F') },
      {UnicodeNamedBlockType.IsBengali, new UnicodeNamedBlock("IsBengali", '\u0980', '\u09FF') },
      {UnicodeNamedBlockType.IsGurmukhi, new UnicodeNamedBlock("IsGurmukhi", '\u0A00', '\u0A7F') },
      {UnicodeNamedBlockType.IsGujarati, new UnicodeNamedBlock("IsGujarati", '\u0A80', '\u0AFF') },
      {UnicodeNamedBlockType.IsOriya, new UnicodeNamedBlock("IsOriya", '\u0B00', '\u0B7F') },
      {UnicodeNamedBlockType.IsTamil, new UnicodeNamedBlock("IsTamil", '\u0B80', '\u0BFF') },
      {UnicodeNamedBlockType.IsTelugu, new UnicodeNamedBlock("IsTelugu", '\u0C00', '\u0C7F') },
      {UnicodeNamedBlockType.IsKannada, new UnicodeNamedBlock("IsKannada", '\u0C80', '\u0CFF') },
      {UnicodeNamedBlockType.IsMalayalam, new UnicodeNamedBlock("IsMalayalam", '\u0D00', '\u0D7F') },
      {UnicodeNamedBlockType.IsSinhala, new UnicodeNamedBlock("IsSinhala", '\u0D80', '\u0DFF') },
      {UnicodeNamedBlockType.IsThai, new UnicodeNamedBlock("IsThai", '\u0E00', '\u0E7F') },
      {UnicodeNamedBlockType.IsLao, new UnicodeNamedBlock("IsLao", '\u0E80', '\u0EFF') },
      {UnicodeNamedBlockType.IsTibetan, new UnicodeNamedBlock("IsTibetan", '\u0F00', '\u0FFF') },
      {UnicodeNamedBlockType.IsMyanmar, new UnicodeNamedBlock("IsMyanmar", '\u1000', '\u109F') },
      {UnicodeNamedBlockType.IsGeorgian, new UnicodeNamedBlock("IsGeorgian", '\u10A0', '\u10FF') },
      {UnicodeNamedBlockType.IsHangulJamo, new UnicodeNamedBlock("IsHangulJamo", '\u1100', '\u11FF') },
      {UnicodeNamedBlockType.IsEthiopic, new UnicodeNamedBlock("IsEthiopic", '\u1200', '\u137F') },
      {UnicodeNamedBlockType.IsCherokee, new UnicodeNamedBlock("IsCherokee", '\u13A0', '\u13FF') },
      {UnicodeNamedBlockType.IsUnifiedCanadianAboriginalSyllabics, new UnicodeNamedBlock("IsUnifiedCanadianAboriginalSyllabics", '\u1400', '\u167F') },
      {UnicodeNamedBlockType.IsOgham, new UnicodeNamedBlock("IsOgham", '\u1680', '\u169F') },
      {UnicodeNamedBlockType.IsRunic, new UnicodeNamedBlock("IsRunic", '\u16A0', '\u16FF') },
      {UnicodeNamedBlockType.IsTagalog, new UnicodeNamedBlock("IsTagalog", '\u1700', '\u171F') },
      {UnicodeNamedBlockType.IsHanunoo, new UnicodeNamedBlock("IsHanunoo", '\u1720', '\u173F') },
      {UnicodeNamedBlockType.IsBuhid, new UnicodeNamedBlock("IsBuhid", '\u1740', '\u175F') },
      {UnicodeNamedBlockType.IsTagbanwa, new UnicodeNamedBlock("IsTagbanwa", '\u1760', '\u177F') },
      {UnicodeNamedBlockType.IsKhmer, new UnicodeNamedBlock("IsKhmer", '\u1780', '\u17FF') },
      {UnicodeNamedBlockType.IsMongolian, new UnicodeNamedBlock("IsMongolian", '\u1800', '\u18AF') },
      {UnicodeNamedBlockType.IsLimbu, new UnicodeNamedBlock("IsLimbu", '\u1900', '\u194F') },
      {UnicodeNamedBlockType.IsTaiLe, new UnicodeNamedBlock("IsTaiLe", '\u1950', '\u197F') },
      {UnicodeNamedBlockType.IsKhmerSymbols, new UnicodeNamedBlock("IsKhmerSymbols", '\u19E0', '\u19FF') },
      {UnicodeNamedBlockType.IsPhoneticExtensions, new UnicodeNamedBlock("IsPhoneticExtensions", '\u1D00', '\u1D7F') },
      {UnicodeNamedBlockType.IsLatinExtendedAdditional, new UnicodeNamedBlock("IsLatinExtendedAdditional", '\u1E00', '\u1EFF') },
      {UnicodeNamedBlockType.IsGreekExtended, new UnicodeNamedBlock("IsGreekExtended", '\u1F00', '\u1FFF') },
      {UnicodeNamedBlockType.IsGeneralPunctuation, new UnicodeNamedBlock("IsGeneralPunctuation", '\u2000', '\u206F') },
      {UnicodeNamedBlockType.IsSuperscriptsandSubscripts, new UnicodeNamedBlock("IsSuperscriptsandSubscripts", '\u2070', '\u209F') },
      {UnicodeNamedBlockType.IsCurrencySymbols, new UnicodeNamedBlock("IsCurrencySymbols", '\u20A0', '\u20CF') },
      {UnicodeNamedBlockType.IsCombiningMarksforSymbols, new UnicodeNamedBlock("IsCombiningMarksforSymbols", '\u20D0', '\u20FF') },
      {UnicodeNamedBlockType.IsLetterlikeSymbols, new UnicodeNamedBlock("IsLetterlikeSymbols", '\u2100', '\u214F') },
      {UnicodeNamedBlockType.IsNumberForms, new UnicodeNamedBlock("IsNumberForms", '\u2150', '\u218F') },
      {UnicodeNamedBlockType.IsArrows, new UnicodeNamedBlock("IsArrows", '\u2190', '\u21FF') },
      {UnicodeNamedBlockType.IsMathematicalOperators, new UnicodeNamedBlock("IsMathematicalOperators", '\u2200', '\u22FF') },
      {UnicodeNamedBlockType.IsMiscellaneousTechnical, new UnicodeNamedBlock("IsMiscellaneousTechnical", '\u2300', '\u23FF') },
      {UnicodeNamedBlockType.IsControlPictures, new UnicodeNamedBlock("IsControlPictures", '\u2400', '\u243F') },
      {UnicodeNamedBlockType.IsOpticalCharacterRecognition, new UnicodeNamedBlock("IsOpticalCharacterRecognition", '\u2440', '\u245F') },
      {UnicodeNamedBlockType.IsEnclosedAlphanumerics, new UnicodeNamedBlock("IsEnclosedAlphanumerics", '\u2460', '\u24FF') },
      {UnicodeNamedBlockType.IsBoxDrawing, new UnicodeNamedBlock("IsBoxDrawing", '\u2500', '\u257F') },
      {UnicodeNamedBlockType.IsBlockElements, new UnicodeNamedBlock("IsBlockElements", '\u2580', '\u259F') },
      {UnicodeNamedBlockType.IsGeometricShapes, new UnicodeNamedBlock("IsGeometricShapes", '\u25A0', '\u25FF') },
      {UnicodeNamedBlockType.IsMiscellaneousSymbols, new UnicodeNamedBlock("IsMiscellaneousSymbols", '\u2600', '\u26FF') },
      {UnicodeNamedBlockType.IsDingbats, new UnicodeNamedBlock("IsDingbats", '\u2700', '\u27BF') },
      {UnicodeNamedBlockType.IsMiscellaneousMathematicalSymbols_A, new UnicodeNamedBlock("IsMiscellaneousMathematicalSymbols-A", '\u27C0', '\u27EF') },
      {UnicodeNamedBlockType.IsSupplementalArrows_A, new UnicodeNamedBlock("IsSupplementalArrows-A", '\u27F0', '\u27FF') },
      {UnicodeNamedBlockType.IsBraillePatterns, new UnicodeNamedBlock("IsBraillePatterns", '\u2800', '\u28FF') },
      {UnicodeNamedBlockType.IsSupplementalArrows_B, new UnicodeNamedBlock("IsSupplementalArrows-B", '\u2900', '\u297F') },
      {UnicodeNamedBlockType.IsMiscellaneousMathematicalSymbols_B, new UnicodeNamedBlock("IsMiscellaneousMathematicalSymbols-B", '\u2980', '\u29FF') },
      {UnicodeNamedBlockType.IsSupplementalMathematicalOperators, new UnicodeNamedBlock("IsSupplementalMathematicalOperators", '\u2A00', '\u2AFF') },
      {UnicodeNamedBlockType.IsMiscellaneousSymbolsandArrows, new UnicodeNamedBlock("IsMiscellaneousSymbolsandArrows", '\u2B00', '\u2BFF') },
      {UnicodeNamedBlockType.IsCJKRadicalsSupplement, new UnicodeNamedBlock("IsCJKRadicalsSupplement", '\u2E80', '\u2EFF') },
      {UnicodeNamedBlockType.IsKangxiRadicals, new UnicodeNamedBlock("IsKangxiRadicals", '\u2F00', '\u2FDF') },
      {UnicodeNamedBlockType.IsIdeographicDescriptionCharacters, new UnicodeNamedBlock("IsIdeographicDescriptionCharacters", '\u2FF0', '\u2FFF') },
      {UnicodeNamedBlockType.IsCJKSymbolsandPunctuation, new UnicodeNamedBlock("IsCJKSymbolsandPunctuation", '\u3000', '\u303F') },
      {UnicodeNamedBlockType.IsHiragana, new UnicodeNamedBlock("IsHiragana", '\u3040', '\u309F') },
      {UnicodeNamedBlockType.IsKatakana, new UnicodeNamedBlock("IsKatakana", '\u30A0', '\u30FF') },
      {UnicodeNamedBlockType.IsBopomofo, new UnicodeNamedBlock("IsBopomofo", '\u3100', '\u312F') },
      {UnicodeNamedBlockType.IsHangulCompatibilityJamo, new UnicodeNamedBlock("IsHangulCompatibilityJamo", '\u3130', '\u318F') },
      {UnicodeNamedBlockType.IsKanbun, new UnicodeNamedBlock("IsKanbun", '\u3190', '\u319F') },
      {UnicodeNamedBlockType.IsBopomofoExtended, new UnicodeNamedBlock("IsBopomofoExtended", '\u31A0', '\u31BF') },
      {UnicodeNamedBlockType.IsKatakanaPhoneticExtensions, new UnicodeNamedBlock("IsKatakanaPhoneticExtensions", '\u31F0', '\u31FF') },
      {UnicodeNamedBlockType.IsEnclosedCJKLettersandMonths, new UnicodeNamedBlock("IsEnclosedCJKLettersandMonths", '\u3200', '\u32FF') },
      {UnicodeNamedBlockType.IsCJKCompatibility, new UnicodeNamedBlock("IsCJKCompatibility", '\u3300', '\u33FF') },
      {UnicodeNamedBlockType.IsCJKUnifiedIdeographsExtensionA, new UnicodeNamedBlock("IsCJKUnifiedIdeographsExtensionA", '\u3400', '\u4DBF') },
      {UnicodeNamedBlockType.IsYijingHexagramSymbols, new UnicodeNamedBlock("IsYijingHexagramSymbols", '\u4DC0', '\u4DFF') },
      {UnicodeNamedBlockType.IsCJKUnifiedIdeographs, new UnicodeNamedBlock("IsCJKUnifiedIdeographs", '\u4E00', '\u9FFF') },
      {UnicodeNamedBlockType.IsYiSyllables, new UnicodeNamedBlock("IsYiSyllables", '\uA000', '\uA48F') },
      {UnicodeNamedBlockType.IsYiRadicals, new UnicodeNamedBlock("IsYiRadicals", '\uA490', '\uA4CF') },
      {UnicodeNamedBlockType.IsHangulSyllables, new UnicodeNamedBlock("IsHangulSyllables", '\uAC00', '\uD7AF') },
      {UnicodeNamedBlockType.IsHighSurrogates, new UnicodeNamedBlock("IsHighSurrogates", '\uD800', '\uDB7F') },
      {UnicodeNamedBlockType.IsHighPrivateUseSurrogates, new UnicodeNamedBlock("IsHighPrivateUseSurrogates", '\uDB80', '\uDBFF') },
      {UnicodeNamedBlockType.IsLowSurrogates, new UnicodeNamedBlock("IsLowSurrogates", '\uDC00', '\uDFFF') },
      {UnicodeNamedBlockType.IsPrivateUseArea, new UnicodeNamedBlock("IsPrivateUseArea", '\uE000', '\uF8FF') },
      {UnicodeNamedBlockType.IsCJKCompatibilityIdeographs, new UnicodeNamedBlock("IsCJKCompatibilityIdeographs", '\uF900', '\uFAFF') },
      {UnicodeNamedBlockType.IsAlphabeticPresentationForms, new UnicodeNamedBlock("IsAlphabeticPresentationForms", '\uFB00', '\uFB4F') },
      {UnicodeNamedBlockType.IsArabicPresentationForms_A, new UnicodeNamedBlock("IsArabicPresentationForms-A", '\uFB50', '\uFDFF') },
      {UnicodeNamedBlockType.IsVariationSelectors, new UnicodeNamedBlock("IsVariationSelectors", '\uFE00', '\uFE0F') },
      {UnicodeNamedBlockType.IsCombiningHalfMarks, new UnicodeNamedBlock("IsCombiningHalfMarks", '\uFE20', '\uFE2F') },
      {UnicodeNamedBlockType.IsCJKCompatibilityForms, new UnicodeNamedBlock("IsCJKCompatibilityForms", '\uFE30', '\uFE4F') },
      {UnicodeNamedBlockType.IsSmallFormVariants, new UnicodeNamedBlock("IsSmallFormVariants", '\uFE50', '\uFE6F') },
      {UnicodeNamedBlockType.IsArabicPresentationForms_B, new UnicodeNamedBlock("IsArabicPresentationForms-B", '\uFE70', '\uFEFF') },
      {UnicodeNamedBlockType.IsHalfwidthandFullwidthForms, new UnicodeNamedBlock("IsHalfwidthandFullwidthForms", '\uFF00', '\uFFEF') },
      {UnicodeNamedBlockType.IsSpecials, new UnicodeNamedBlock("IsSpecials", '\uFFF0', '\uFFFF') },
    };

    /// <summary>
    /// An extension function to specially format a <see cref="UnicodeNamedBlockType"/>
    /// value into a string.
    /// </summary>
    /// <param name="type">The <see cref="UnicodeNamedBlockType"/> to format.</param>
    /// <returns>Returns the formatted string for the <paramref name="type"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string NamedBlockToString(this UnicodeNamedBlockType type)
    {
      return type.ToString().Replace('_', '-'); // All underscores must be replaced with hyphens.
    }

    /// <summary>
    /// A function to get the general category for a Unicode character. This returns it in the form
    /// of a general category used in Regex.
    /// </summary>
    /// <remarks>Since it only returns the most general categories, this function only returns
    /// <see cref="UnicodeCategoryType.L"/>, <see cref="UnicodeCategoryType.M"/>,
    /// <see cref="UnicodeCategoryType.N"/>, <see cref="UnicodeCategoryType.Z"/>,
    /// <see cref="UnicodeCategoryType.C"/>, <see cref="UnicodeCategoryType.P"/>,
    /// or <see cref="UnicodeCategoryType.S"/>.
    /// </remarks>
    /// <param name="ch">The character to get the type of.</param>
    /// <returns>Returns the general category for the Unicode character. Note how this returns a
    /// <see cref="UnicodeCategoryType"/> and not a <see cref="UnicodeCategory"/>. <see cref="UnicodeCategory"/>
    /// does not have generic categories. Only ranges of the categories.</returns>
    public static UnicodeCategoryType GetGenericCategoryType(char ch)
    {
      int category = (int)char.GetUnicodeCategory(ch); // Get the int value of the category enum.

      return category switch
      {
        // When between the Letter categories return general Letter.
        int l when (int)UnicodeCategory.UppercaseLetter <= l && l <= (int)UnicodeCategory.OtherLetter => UnicodeCategoryType.L,
        // When between the Mark categories return general Mark.
        int m when (int)UnicodeCategory.NonSpacingMark <= m && m <= (int)UnicodeCategory.EnclosingMark => UnicodeCategoryType.M,
        // When between the Number categories return general Number.
        int n when (int)UnicodeCategory.DecimalDigitNumber <= n && n <= (int)UnicodeCategory.OtherNumber => UnicodeCategoryType.N,
        // When between the Separator categories return general Separator.
        int z when (int)UnicodeCategory.SpaceSeparator <= z && z <= (int)UnicodeCategory.ParagraphSeparator => UnicodeCategoryType.Z,
        // When between the Other categories return general Other.
        int c when (int)UnicodeCategory.Control <= c && c <= (int)UnicodeCategory.PrivateUse => UnicodeCategoryType.C,
        // When between the Punctuation categories return general Punctuation.
        int p when (int)UnicodeCategory.ConnectorPunctuation <= p && p <= (int)UnicodeCategory.OtherPunctuation => UnicodeCategoryType.P,
        // When between the Symbol categories return general Symbol.
        int s when (int)UnicodeCategory.MathSymbol <= s && s <= (int)UnicodeCategory.OtherSymbol => UnicodeCategoryType.S,
        // By default [Not Assigned], return Other.
        _ => UnicodeCategoryType.C,
      };
    }

    /// <summary>
    /// A function to determine if a character is of the Unicode Letter category.
    /// For more details, see the official documentation on
    /// <a href="https://docs.microsoft.com/en-us/dotnet/api/system.globalization.unicodecategory?view=net-5.0">
    /// UnicodeCategory</a> and <see cref="UnicodeCategoryType"/>.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>Returns if the character is within the Letter Category values.</returns>
    public static bool IsUnicodeLetterCategory(char c)
    {
      int value = (int)char.GetUnicodeCategory(c); // Get the value.
      // The value must be between the min and max Letter Category enum values.
      return (int)UnicodeCategory.UppercaseLetter <= value && value <= (int)UnicodeCategory.OtherLetter;
    }

    /// <summary>
    /// A function to determine if a character is of the Unicode Mark category.
    /// For more details, see the official documentation on
    /// <a href="https://docs.microsoft.com/en-us/dotnet/api/system.globalization.unicodecategory?view=net-5.0">
    /// UnicodeCategory</a> and <see cref="UnicodeCategoryType"/>.
    /// <param name="c">The character to check.</param>
    /// <returns>Returns if the character is within the Mark Category values.</returns>
    public static bool IsUnicodeMarkCategory(char c)
    {
      int value = (int)char.GetUnicodeCategory(c); // Get the value.
      // The value must be between the min and max Mark Category enum values.
      return (int)UnicodeCategory.NonSpacingMark <= value && value <= (int)UnicodeCategory.EnclosingMark;
    }

    /// <summary>
    /// A function to determine if a character is of the Unicode Number category.
    /// For more details, see the official documentation on
    /// <a href="https://docs.microsoft.com/en-us/dotnet/api/system.globalization.unicodecategory?view=net-5.0">
    /// UnicodeCategory</a> and <see cref="UnicodeCategoryType"/>.
    /// <param name="c">The character to check.</param>
    /// <returns>Returns if the character is within the Number Category values.</returns>
    public static bool IsUnicodeNumberCategory(char c)
    {
      int value = (int)char.GetUnicodeCategory(c); // Get the value.
      // The value must be between the min and max Number Category enum values.
      return (int)UnicodeCategory.DecimalDigitNumber <= value && value <= (int)UnicodeCategory.OtherNumber;
    }

    /// <summary>
    /// A function to determine if a character is of the Unicode Separator category.
    /// For more details, see the official documentation on
    /// <a href="https://docs.microsoft.com/en-us/dotnet/api/system.globalization.unicodecategory?view=net-5.0">
    /// UnicodeCategory</a> and <see cref="UnicodeCategoryType"/>.
    /// <param name="c">The character to check.</param>
    /// <returns>Returns if the character is within the Separator Category values.</returns>
    public static bool IsUnicodeSeparatorCategory(char c)
    {
      int value = (int)char.GetUnicodeCategory(c); // Get the value.
      // The value must be between the min and max Number Category enum values.
      return (int)UnicodeCategory.SpaceSeparator <= value && value <= (int)UnicodeCategory.ParagraphSeparator;
    }

    /// <summary>
    /// A function to determine if a character is of the Unicode Other category.
    /// For more details, see the official documentation on
    /// <a href="https://docs.microsoft.com/en-us/dotnet/api/system.globalization.unicodecategory?view=net-5.0">
    /// UnicodeCategory</a> and <see cref="UnicodeCategoryType"/>.
    /// <param name="c">The character to check.</param>
    /// <returns>Returns if the character is within the Other Category values.</returns>
    public static bool IsUnicodeOtherCategory(char c)
    {
      int value = (int)char.GetUnicodeCategory(c); // Get the value.
      // The value must be between the min and max Number Other enum values, or not be assigned.
      return ((int)UnicodeCategory.Control <= value && value <= (int)UnicodeCategory.PrivateUse) || value == (int)UnicodeCategory.OtherNotAssigned;
    }

    /// <summary>
    /// A function to determine if a character is of the Unicode Punctuation category.
    /// For more details, see the official documentation on
    /// <a href="https://docs.microsoft.com/en-us/dotnet/api/system.globalization.unicodecategory?view=net-5.0">
    /// UnicodeCategory</a> and <see cref="UnicodeCategoryType"/>.
    /// <param name="c">The character to check.</param>
    /// <returns>Returns if the character is within the Punctuation Category values.</returns>
    public static bool IsUnicodePunctuationCategory(char c)
    {
      int value = (int)char.GetUnicodeCategory(c); // Get the value.
      // The value must be between the min and max Number Punctuation enum values.
      return (int)UnicodeCategory.ConnectorPunctuation <= value && value <= (int)UnicodeCategory.OtherPunctuation;
    }

    /// <summary>
    /// A function to determine if a character is of the Unicode Symbol category.
    /// For more details, see the official documentation on
    /// <a href="https://docs.microsoft.com/en-us/dotnet/api/system.globalization.unicodecategory?view=net-5.0">
    /// UnicodeCategory</a> and <see cref="UnicodeCategoryType"/>.
    /// <param name="c">The character to check.</param>
    /// <returns>Returns if the character is within the Symbol Category values.</returns>
    public static bool IsUnicodeSymbolCategory(char c)
    {
      int value = (int)char.GetUnicodeCategory(c); // Get the value.
      // The value must be between the min and max Number Category enum values.
      return (int)UnicodeCategory.MathSymbol <= value && value <= (int)UnicodeCategory.OtherSymbol;
    }
  }
  /************************************************************************************************/
}