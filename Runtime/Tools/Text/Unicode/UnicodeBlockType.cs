/**************************************************************************************************/
/*!
\file   UnicodeBlockType.cs
\author Craig Williams
\par    Last Updated
        2021-05-21
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file containing an enum for every Unicode Named Block supported by .NET.

\par Bug List

\par References
*/
/**************************************************************************************************/
namespace SlashParadox.Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// An enumeration of every Unicode Named Block supported by .NET. This list can be found in the
  /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
  /// .NET Documentation</a>. These are named exactly as they would be placed into a
  /// <see cref="System.Text.RegularExpressions.Regex"/>, with hypens replaced by underscores.
  /// </summary>
  public enum UnicodeBlockType
  {
    /// <summary>u0000 - u007F</summary>
    IsBasicLatin,
    /// <summary>u0080 - u00FF</summary>
    IsLatin_1Supplement,
    /// <summary>u0100 - u017F</summary>
    IsLatinExtended_A,
    /// <summary>u0180 - u024F</summary>
    IsLatinExtended_B,
    /// <summary>u0250 - u02AF</summary>
    IsIPAExtensions,
    /// <summary>u02B0 - u02FF</summary>
    IsSpacingModifierLetters,
    /// <summary>u0300 - u036F</summary>
    IsCombiningDiacriticalMarks,
    /// <summary>u0370 - u03FF</summary>
    IsGreekandCoptic,
    /// <summary>u0400 - u04FF</summary>
    IsCyrillic,
    /// <summary>u0500 - u052F</summary>
    IsCyrillicSupplement,
    /// <summary>u0530 - u058F</summary>
    IsArmenian,
    /// <summary>u0590 - u05FF</summary>
    IsHebrew,
    /// <summary>u0600 - u06FF</summary>
    IsArabic,
    /// <summary>u0700 - u074F</summary>
    IsSyriac,
    /// <summary>u0780 - u07BF</summary>
    IsThaana,
    /// <summary>u0900 - u097F</summary>
    IsDevanagari,
    /// <summary>u0980 - u09FF</summary>
    IsBengali,
    /// <summary>u0A00 - u0A7F</summary>
    IsGurmukhi,
    /// <summary>u0A80 - u0AFF</summary>
    IsGujarati,
    /// <summary>u0B00 - u0B7F</summary>
    IsOriya,
    /// <summary>u0B80 - u0BFF</summary>
    IsTamil,
    /// <summary>u0C00 - u0C7F</summary>
    IsTelugu,
    /// <summary>u0C80 - u0CFF</summary>
    IsKannada,
    /// <summary>u0D00 - u0D7F</summary>
    IsMalayalam,
    /// <summary>u0D80 - u0DFF</summary>
    IsSinhala,
    /// <summary>u0E00 - u0E7F</summary>
    IsThai,
    /// <summary>u0E80 - u0EFF</summary>
    IsLao,
    /// <summary>u0F00 - u0FFF</summary>
    IsTibetan,
    /// <summary>u1000 - u109F</summary>
    IsMyanmar,
    /// <summary>u10A0 - u10FF</summary>
    IsGeorgian,
    /// <summary>u1100 - u11FF</summary>
    IsHangulJamo,
    /// <summary>u1200 - u137F</summary>
    IsEthiopic,
    /// <summary>u13A0 - u13FF</summary>
    IsCherokee,
    /// <summary>u1400 - u167F</summary>
    IsUnifiedCanadianAboriginalSyllabics,
    /// <summary>u1680 - u169F</summary>
    IsOgham,
    /// <summary>u16A0 - u16FF</summary>
    IsRunic,
    /// <summary>u1700 - u171F</summary>
    IsTagalog,
    /// <summary>u1720 - u173F</summary>
    IsHanunoo,
    /// <summary>u1740 - u175F</summary>
    IsBuhid,
    /// <summary>u1760 - u177F</summary>
    IsTagbanwa,
    /// <summary>u1780 - u17FF</summary>
    IsKhmer,
    /// <summary>u1800 - u18AF</summary>
    IsMongolian,
    /// <summary>u1900 - u194F</summary>
    IsLimbu,
    /// <summary>u1950 - u197F</summary>
    IsTaiLe,
    /// <summary>u19E0 - u19FF</summary>
    IsKhmerSymbols,
    /// <summary>u1D00 - u1D7F</summary>
    IsPhoneticExtensions,
    /// <summary>u1E00 - u1EFF</summary>
    IsLatinExtendedAdditional,
    /// <summary>u1F00 - u1FFF</summary>
    IsGreekExtended,
    /// <summary>u2000 - u206F</summary>
    IsGeneralPunctuation,
    /// <summary>u2070 - u209F</summary>
    IsSuperscriptsandSubscripts,
    /// <summary>u20A0 - u20CF</summary>
    IsCurrencySymbols,
    /// <summary>u20D0 - u20FF</summary>
    IsCombiningMarksforSymbols,
    /// <summary>u2100 - u214F</summary>
    IsLetterlikeSymbols,
    /// <summary>u2150 - u218F</summary>
    IsNumberForms,
    /// <summary>u2190 - u21FF</summary>
    IsArrows,
    /// <summary>u2200 - u22FF</summary>
    IsMathematicalOperators,
    /// <summary>u2300 - u23FF</summary>
    IsMiscellaneousTechnical,
    /// <summary>u2400 - u243F</summary>
    IsControlPictures,
    /// <summary>u2440 - u245F</summary>
    IsOpticalCharacterRecognition,
    /// <summary>u2460 - u24FF</summary>
    IsEnclosedAlphanumerics,
    /// <summary>u2500 - u257F</summary>
    IsBoxDrawing,
    /// <summary>u2580 - u259F</summary>
    IsBlockElements,
    /// <summary>u25A0 - u25FF</summary>
    IsGeometricShapes,
    /// <summary>u2600 - u26FF</summary>
    IsMiscellaneousSymbols,
    /// <summary>u2700 - u27BF</summary>
    IsDingbats,
    /// <summary>u27C0 - u27EF</summary>
    IsMiscellaneousMathematicalSymbols_A,
    /// <summary>u27F0 - u27FF</summary>
    IsSupplementalArrows_A,
    /// <summary>u2800 - u28FF</summary>
    IsBraillePatterns,
    /// <summary>u2900 - u297F</summary>
    IsSupplementalArrows_B,
    /// <summary>u2980 - u29FF</summary>
    IsMiscellaneousMathematicalSymbols_B,
    /// <summary>u2A00 - u2AFF</summary>
    IsSupplementalMathematicalOperators,
    /// <summary>u2B00 - u2BFF</summary>
    IsMiscellaneousSymbolsandArrows,
    /// <summary>u2E80 - u2EFF</summary>
    IsCJKRadicalsSupplement,
    /// <summary>u2F00 - u2FDF</summary>
    IsKangxiRadicals,
    /// <summary>u2FF0 - u2FFF</summary>
    IsIdeographicDescriptionCharacters,
    /// <summary>u3000 - u303F</summary>
    IsCJKSymbolsandPunctuation,
    /// <summary>u3040 - u309F</summary>
    IsHiragana,
    /// <summary>u30A0 - u30FF</summary>
    IsKatakana,
    /// <summary>u3100 - u312F</summary>
    IsBopomofo,
    /// <summary>u3130 - u318F</summary>
    IsHangulCompatibilityJamo,
    /// <summary>u3190 - u319F</summary>
    IsKanbun,
    /// <summary>u31A0 - u31BF</summary>
    IsBopomofoExtended,
    /// <summary>u31F0 - u31FF</summary>
    IsKatakanaPhoneticExtensions,
    /// <summary>u3200 - u32FF</summary>
    IsEnclosedCJKLettersandMonths,
    /// <summary>u3300 - u33FF</summary>
    IsCJKCompatibility,
    /// <summary>u3400 - u4DBF</summary>
    IsCJKUnifiedIdeographsExtensionA,
    /// <summary>u4DC0 - u4DFF</summary>
    IsYijingHexagramSymbols,
    /// <summary>u4E00 - u9FFF</summary>
    IsCJKUnifiedIdeographs,
    /// <summary>uA000 - uA48F</summary>
    IsYiSyllables,
    /// <summary>uA490 - uA4CF</summary>
    IsYiRadicals,
    /// <summary>uAC00 - uD7AF</summary>
    IsHangulSyllables,
    /// <summary>uD800 - uDB7F</summary>
    IsHighSurrogates,
    /// <summary>uDB80 - uDBFF</summary>
    IsHighPrivateUseSurrogates,
    /// <summary>uDC00 - uDFFF</summary>
    IsLowSurrogates,
    /// <summary>uE000 - uF8FF</summary>
    IsPrivateUseArea,
    /// <summary>uF900 - uFAFF</summary>
    IsCJKCompatibilityIdeographs,
    /// <summary>uFB00 - uFB4F</summary>
    IsAlphabeticPresentationForms,
    /// <summary>uFB50 - uFDFF</summary>
    IsArabicPresentationForms_A,
    /// <summary>uFE00 - uFE0F</summary>
    IsVariationSelectors,
    /// <summary>uFE20 - uFE2F</summary>
    IsCombiningHalfMarks,
    /// <summary>uFE30 - uFE4F</summary>
    IsCJKCompatibilityForms,
    /// <summary>uFE50 - uFE6F</summary>
    IsSmallFormVariants,
    /// <summary>uFE70 - uFEFF</summary>
    IsArabicPresentationForms_B,
    /// <summary>uFF00 - uFFEF</summary>
    IsHalfwidthandFullwidthForms,
    /// <summary>uFFF0 - uFFFF</summary>
    IsSpecials,
  }
  /************************************************************************************************/
}