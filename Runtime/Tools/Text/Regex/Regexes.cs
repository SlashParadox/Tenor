/**************************************************************************************************/
/*!
\file   Regexes.cs
\author Craig Williams
\par    Last Updated
        2021-03-07
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for tool functions related to Regular Expressions.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Tenor.Tools.Collection;

namespace Tenor.Tools.Text
{
  /************************************************************************************************/
  /// <summary>
  /// A series of useful functions for creating custom regular expressions (Regex). Use these
  /// alongside <see cref="Unicode"/> and types to create expressions for any language or
  /// category supported by .NET.
  /// </summary>
  public static partial class Regexes
  {
    /// <summary>A Regex group to only allow for Letters, regardless of language.</summary>
    private const string RegexOnlyLetters = @"[\p{N}\p{M}\p{P}\p{S}\p{Z}\p{C}]";

    /// <summary>A Regex group to only allow for Numbers, regardless of language.</summary>
    private const string RegexOnlyNumbers = @"[\p{L}\p{M}\p{P}\p{S}\p{Z}\p{C}]";

    /// <summary>A Regex group to only allow for Marks, regardless of language.</summary>
    private const string RegexOnlyMarks = @"[\p{N}\p{L}\p{P}\p{S}\p{Z}\p{C}]";

    /// <summary>A Regex group to only allow for Punctuation, regardless of language.</summary>
    private const string RegexOnlyPunctuation = @"[\p{N}\p{M}\p{L}\p{S}\p{Z}\p{C}]";

    /// <summary>A Regex group to only allow for Symbols, regardless of language.</summary>
    private const string RegexOnlySymbols = @"[\p{N}\p{M}\p{P}\p{L}\p{Z}\p{C}]";

    /// <summary>A Regex group to only allow for letters, regardless of language.</summary>
    private const string RegexOnlySeparators = @"[\p{N}\p{M}\p{P}\p{S}\p{L}\p{C}]";

    /// <summary>A Regex group to only allow for Other Characters, regardless of language.</summary>
    private const string RegexOnlyOther = @"[\p{N}\p{M}\p{P}\p{S}\p{Z}\p{L}]";

    /// <summary>A Regex matching string for numeric strings. This does NOT allow currency,
    /// group separators, or whitespace.</summary>
    public static string RegexNumeric
    {
      get
      {
        NumberFormatInfo info = NumberFormatInfo.CurrentInfo;
        StringBuilder sb = new StringBuilder(@"^");
        sb.Append(Regex.Escape(info.NegativeSign)).Append(@"?(\d+(");
        sb.Append(Regex.Escape(info.NumberDecimalSeparator)).Append(@"\d+)?)$");
        return sb.ToString();
      }
    }

    /// <summary>A Regex matching string for numeric strings, with group separators.
    /// This does NOT allow currency, or whitespace.</summary>
    public static string RegexNumericGroup
    {
      get
      {
        NumberFormatInfo info = NumberFormatInfo.CurrentInfo;
        StringBuilder sb = new StringBuilder(@"^");
        sb.Append(Regex.Escape(info.NegativeSign)).Append(@"?((\d{1,3}((");
        sb.Append(Regex.Escape(info.NumberGroupSeparator)).Append(@"\d{3})*))(");
        sb.Append(Regex.Escape(info.NumberDecimalSeparator)).Append(@"\d+)?)$");
        return sb.ToString();
      }
    }

    /// <summary>A Regex matching string for standard phone numbers,
    /// based on the International Numbering Plan.</summary>
    public static readonly string RegexPhoneNumber = @"^[+]?(\d{1,3})?[\s.-]?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$";

    /// <summary>
    /// A function which creates a perfect match Regex. This is used to make sure a string matches
    /// the wanted expression from start to finish.
    /// </summary>
    /// <param name="expression">The expression to enclose into the final Regex.</param>
    /// <returns>Returns the perfect match Regex, in the form of: ^expression+$</returns>
    public static string CreatePerfectMatch(string expression)
    {
      return new StringBuilder(@"^").Append(expression).Append(@"+$").ToString();
    }

    /// <summary>
    /// A function to create a snippet of a Regex for a user-chosen alphabet. This only creates a
    /// snippet in the format of: [<paramref name="namedBlock"/>].
    /// </summary>
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet. If unsure, simply pass
    /// <see cref="UnicodeNamedBlockType.IsBasicLatin"/>.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlock"/>].</returns>
    public static string CreateAlphabet(UnicodeNamedBlockType namedBlock)
    {
      // Create a string representing an enclosure of the wanted block, using the 'p' escape.
      return new StringBuilder(@"[\p{").Append(namedBlock.NamedBlockToString()).Append(@"}").Append(@"]").ToString();
    }

    /// <summary>
    /// A function to create a snippet of a Regex for a user-chosen alphabet. This only creates a
    /// snippet in the format of: [<paramref name="category"/>].
    /// </summary>
    /// <param name="category">The General Categories that will be removed from the alphabet.
    /// Use this to only allow certain characters. For example, pass
    /// <see cref="UnicodeCategoryType.L"/> to only allow letters.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="category"/>].</returns>
    public static string CreateAlphabet(UnicodeCategoryType category)
    {
      // Create a string representing an enclosure of the wanted category, using the 'p' escape.
      return new StringBuilder(@"[\p{").Append(category.ToString()).Append(@"}").Append(@"]").ToString();
    }

    /// <summary>
    /// A function to create a snippet of a Regex for a user-chosen alphabet. This only creates a
    /// snippet in the format of: [<paramref name="block_or_category"/>]. This does no error
    /// checking on what is valid! Please refer to the .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a> and
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-unicode-general-categories">
    /// general categories</a>.
    /// </summary>
    /// <param name="block_or_category">Either the Named Block or General Category for the Regex.
    /// The Named Block is the language that is available in the alphabet. If unsure, simply pass
    /// 'IsBasicLatin' for Latin-based letters [English], or pass 'L' for all Letters,
    /// no matter the language.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="block_or_category"/>].</returns>
    public static string CreateAlphabet(string block_or_category)
    {
      // Create a string representing an enclosure of the wanted block/category, using the 'p' escape.
      return new StringBuilder(@"[\p{").Append(block_or_category).Append(@"}").Append(@"]").ToString();
    }

    /// <summary>
    /// A function to create a snippet of a Regex for a user-chosen alphabet. This only creates a
    /// snippet in the format of: [<paramref name="namedBlock"/> -
    /// [<paramref name="removedCategory"/>]].
    /// </summary>
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet. If unsure, simply pass
    /// <see cref="UnicodeNamedBlockType.IsBasicLatin"/>.</param>
    /// <param name="removedCategory">The General Category that will be removed from the alphabet.
    /// Use this to remove certain characters. For example, pass
    /// <see cref="UnicodeCategoryType.L"/> to remove all letters.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlock"/> - [<paramref name="removedCategory"/>]].</returns>
    public static string CreateAlphabet(UnicodeNamedBlockType namedBlock, UnicodeCategoryType removedCategory)
    {
      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.

      // Append the named block using the 'p' escape.
      regex.Append(@"\p{").Append(namedBlock.NamedBlockToString()).Append(@"}");
      // Append the subtraction of the removed category using the 'p' escape.
      regex.Append(@"-[").Append(@"\p{").Append(removedCategory.ToString()).Append(@"}]]");

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// A function to create a snippet of a Regex for a user-chosen alphabet. This only creates a
    /// snippet in the format of: [<paramref name="namedBlock"/> -
    /// [<paramref name="removedCategory"/>]]. This does no error checking on what is valid!
    /// Please refer to the .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a> and
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-unicode-general-categories">
    /// general categories</a>.
    /// </summary>
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet. If unsure, simply pass 'IsBasicLatin'.</param>
    /// <param name="removedCategory">The General Category that will be removed from the alphabet.
    /// Use this to remove certain characters. For example, pass 'L' to remove all letters.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlock"/> - [<paramref name="removedCategory"/>]].</returns>
    public static string CreateAlphabet(string namedBlock, string removedCategory)
    {
      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.

      // Append the named block using the 'p' escape.
      regex.Append(@"\p{").Append(namedBlock).Append(@"}");
      // Append the subtraction of the removed category using the 'p' escape.
      regex.Append(@"-[").Append(@"\p{").Append(removedCategory).Append(@"}]]");

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// A function to create a snippet of a Regex for a user-chosen alphabet. This only creates a
    /// snippet in the format of: [<paramref name="namedBlock"/> -
    /// [<paramref name="removedCategory"/>]]. This does no error checking on valid
    /// General Categories! Please refer to the .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-unicode-general-categories">
    /// general categories</a>.
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet. If unsure, simply pass
    /// <see cref="UnicodeNamedBlockType.IsBasicLatin"/>.</param>
    /// <param name="removedCategory">The General Category that will be removed from the alphabet.
    /// Use this to remove certain characters. For example, pass 'L' to remove all letters.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlock"/> - [<paramref name="removedCategory"/>]].</returns>
    public static string CreateAlphabet(UnicodeNamedBlockType namedBlock, string removedCategory)
    {
      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.

      // Append the named block using the 'p' escape.
      regex.Append(@"\p{").Append(namedBlock.NamedBlockToString()).Append(@"}");
      // Append the subtraction of the removed category using the 'p' escape.
      regex.Append(@"-[").Append(@"\p{").Append(removedCategory).Append(@"}]]");

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// A function to create a snippet of a Regex for a user-chosen alphabet. This only creates a
    /// snippet in the format of: [<paramref name="namedBlock"/> -
    /// [<paramref name="removedCategory"/>]]. This does no error checking on valid
    /// named blocks! Please refer to the .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet.
    /// If unsure, simply pass 'IsBasicLatin'.</param>
    /// <param name="removedCategory">The General Category that will be removed from the alphabet.
    /// Use this to remove certain characters. For example, pass
    /// <see cref="UnicodeCategoryType.L"/> to remove all letters.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlock"/> - [<paramref name="removedCategory"/>]].</returns>
    public static string CreateAlphabet(string namedBlock, UnicodeCategoryType removedCategory)
    {
      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.

      // Append the named block using the 'p' escape.
      regex.Append(@"\p{").Append(namedBlock).Append(@"}");
      // Append the subtraction of the removed category using the 'p' escape.
      regex.Append(@"-[").Append(@"\p{").Append(removedCategory.ToString()).Append(@"}]]");

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// A function to create a snippet of a Regex for a user-chosen alphabet. This only creates a
    /// snippet in the format of: [<paramref name="namedBlocks"/>].
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an IList containing
    /// <see cref="UnicodeNamedBlockType.IsBasicLatin"/>.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/>].</returns>
    public static string CreateAlphabet(IList<UnicodeNamedBlockType> namedBlocks)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i].NamedBlockToString()).Append(@"}");

      regex.Append(@"]"); // Append the last bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// A function to create a snippet of a Regex for a user-chosen alphabet. This only creates a
    /// snippet in the format of: [<paramref name="categories"/>].
    /// </summary>
    /// <param name="categories">The General Categories that will be removed from the alphabet.
    /// Use these to only allow certain characters. For examnple, pass an IList containing
    /// <see cref="UnicodeCategoryType.L"/> to only allow letters.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="categories"/>].</returns>
    public static string CreateAlphabet(IList<UnicodeCategoryType> categories)
    {
      // If there are no categories, return an empty string.
      if (categories.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = categories.Count;

      // For all categories, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(categories[i].ToString()).Append(@"}");

      regex.Append(@"]"); // Append the last bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// A function to create a snippet of a Regex for a user-chosen alphabet. This only creates a
    /// snippet in the format of: [<paramref name="blocks_or_categories"/>]. This does no error
    /// checking on what is valid!  Please refer to the .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a> and
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-unicode-general-categories">
    /// general categories</a>.
    /// </summary>
    /// <param name="blocks_or_categories">The General Categories that will be removed from the
    /// alphabet. Use these to only allow certain characters. For examnple, pass an
    /// <see cref="IList{T}"/> containing <see cref="UnicodeCategoryType.L"/>
    /// to only allow letters.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="blocks_or_categories"/>].</returns>
    public static string CreateAlphabet(IList<string> blocks_or_categories)
    {
      // If there are no blocks/categories, return an empty string.
      if (blocks_or_categories.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = blocks_or_categories.Count; // Get the count.

      // For all blocks/categories, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(blocks_or_categories[i]).Append(@"}");

      regex.Append(@"]"); // Append the last bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// A function to create a snippet of a Regex for a user-chosen alphabet. This only creates a
    /// snippet in the format of: [<paramref name="namedBlock"/> -
    /// [<paramref name="removedCategories"/>]].
    /// </summary>
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet. If unsure, simply pass
    /// <see cref="UnicodeNamedBlockType.IsBasicLatin"/>.</param>
    /// <param name="removedCategories">The General Categories that will be removed from the
    /// alphabet. Use these to remove certain characters. For example, pass an IList containing
    /// <see cref="UnicodeCategoryType.L"/> to remove all letters.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlock"/> - [<paramref name="removedCategories"/>]].</returns>
    public static string CreateAlphabet(UnicodeNamedBlockType namedBlock, IList<UnicodeCategoryType> removedCategories)
    {
      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.

      // Append the named block using the 'p' escape.
      regex.Append(@"\p{").Append(namedBlock.NamedBlockToString()).Append(@"}");

      // If there are any removed categories, we have to append to the Regex string.
      if (!removedCategories.IsEmptyOrNull())
      {
        regex.Append(@"-["); // Append a subtraction.
        int count = removedCategories.Count; // Get the number of categories.

        // For all categories, append using the 'p' escape.
        for (int i = 0; i < count; i++)
          regex.Append(@"\p{").Append(removedCategories[i].ToString()).Append(@"}");

        regex.Append(@"]"); // Append an ending bracket.
      }

      regex.Append(@"]"); // Append the last bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// A function to create a snippet of a Regex for a user-chosen alphabet. This only creates a
    /// snippet in the format of: [<paramref name="namedBlock"/> -
    /// [<paramref name="removedCategories"/>]]. This does no error checking on what is valid!
    /// Please refer to the .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a> and
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-unicode-general-categories">
    /// general categories</a>.
    /// </summary>
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet. If unsure, simply pass 'IsBasicLatin'.</param>
    /// <param name="removedCategories">The General Categories that will be removed from the
    /// alphabet. Use these to remove certain characters. For example, pass an IList containing 'L'
    /// if you only want letters.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlock"/> - [<paramref name="removedCategories"/>]].</returns>
    public static string CreateAlphabet(string namedBlock, IList<string> removedCategories)
    {
      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.

      // Append the named block using the 'p' escape.
      regex.Append(@"\p{").Append(namedBlock).Append(@"}");

      // If there are any removed categories, we have to append to the Regex string.
      if (!removedCategories.IsEmptyOrNull())
      {
        regex.Append(@"-["); // Append a subtraction.
        int count = removedCategories.Count; // Get the number of categories.

        // For all categories, append using the 'p' escape.
        for (int i = 0; i < count; i++)
          regex.Append(@"\p{").Append(removedCategories[i]).Append(@"}");

        regex.Append(@"]"); // Append an ending bracket.
      }

      regex.Append(@"]"); // Append the last bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// A function to create a snippet of a Regex for a user-chosen alphabet. This only creates a
    /// snippet in the format of: [<paramref name="namedBlock"/> -
    /// [<paramref name="removedCategories"/>]]. This does no error checking on valid
    /// General Categories! Please refer to the .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-unicode-general-categories">
    /// general categories</a>.
    /// </summary>
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet. If unsure, simply pass
    /// <see cref="UnicodeNamedBlockType.IsBasicLatin"/>.</param>
    /// <param name="removedCategories">The General Categories that will be removed from the
    /// alphabet. Use these to remove certain characters. For example, pass an
    /// <see cref="IList{T}"/> containing 'L' to remove all letters.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlock"/> - [<paramref name="removedCategories"/>]].</returns>
    public static string CreateAlphabet(UnicodeNamedBlockType namedBlock, IList<string> removedCategories)
    {
      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.

      // Append the named block using the 'p' escape.
      regex.Append(@"\p{").Append(namedBlock.NamedBlockToString()).Append(@"}");

      // If there are any removed categories, we have to append to the Regex string.
      if (!removedCategories.IsEmptyOrNull())
      {
        regex.Append(@"-["); // Append a subtraction.
        int count = removedCategories.Count; // Get the number of categories.

        // For all categories, append using the 'p' escape.
        for (int i = 0; i < count; i++)
          regex.Append(@"\p{").Append(removedCategories[i]).Append(@"}");
        
        regex.Append(@"]"); // Append an ending bracket.
      }

      regex.Append(@"]"); // Append the last bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// A function to create a snippet of a Regex for a user-chosen alphabet. This only creates a
    /// snippet in the format of: [<paramref name="namedBlock"/> -
    /// [<paramref name="removedCategories"/>]]. This does no error checking on valid
    /// named blocks! Please refer to the .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet. If unsure, simply pass 'IsBasicLatin'.</param>
    /// <param name="removedCategories">The General Categories that will be removed from the
    /// alphabet. Use these to remove certain characters. For example, pass an
    /// <see cref="IList{T}"/> containing <see cref="UnicodeCategoryType.L"/>
    /// to remove all letters.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlock"/> - [<paramref name="removedCategories"/>]].</returns>
    public static string CreateAlphabet(string namedBlock, IList<UnicodeCategoryType> removedCategories)
    {
      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.

      // Append the named block using the 'p' escape.
      regex.Append(@"\p{").Append(namedBlock).Append(@"}");

      // If there are any removed categories, we have to append to the Regex string.
      if (!removedCategories.IsEmptyOrNull())
      {
        regex.Append(@"-["); // Append a subtraction.
        int count = removedCategories.Count; // Get the number of categories.

        // For all categories, append using the 'p' escape.
        for (int i = 0; i < count; i++)
          regex.Append(@"\p{").Append(removedCategories[i].ToString()).Append(@"}");

        regex.Append(@"]"); // Append an ending bracket.
      }

      regex.Append(@"]"); // Append the last bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// A function to create a snippet of a Regex for a user-chosen alphabet. This only creates a
    /// snippet in the format of: [<paramref name="namedBlocks"/> -
    /// [<paramref name="removedCategory"/>]].
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an <see cref="IList{T}"/> containing
    /// <see cref="UnicodeNamedBlockType.IsBasicLatin"/>.</param>
    /// <param name="removedCategory">The General Category that will be removed from the alphabet.
    /// Use this to remove certain characters. For example, pass <see cref="UnicodeCategoryType.L"/>
    /// to remove all letters.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/> - [<paramref name="removedCategory"/>]].</returns>
    public static string CreateAlphabet(IList<UnicodeNamedBlockType> namedBlocks, UnicodeCategoryType removedCategory)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i].NamedBlockToString()).Append(@"}");

      // Append the subtraction of the removed category using the 'p' escape.
      regex.Append(@"-[").Append(@"\p{").Append(removedCategory.ToString()).Append(@"}]]");

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// A function to create a snippet of a Regex for a user-chosen alphabet. This only creates a
    /// snippet in the format of: [<paramref name="namedBlocks"/> -
    /// [<paramref name="removedCategory"/>]]. This does no error checking on what is valid!
    /// Please refer to the .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a> and
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-unicode-general-categories">
    /// general categories</a>.
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an <see cref="IList{T}"/>
    /// containing 'IsBasicLatin'.</param>
    /// <param name="removedCategory">The General Category that will be removed from the alphabet.
    /// Use this to remove certain characters. For example, pass 'L' to remove all letters.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/> - [<paramref name="removedCategory"/>]].</returns>
    public static string CreateAlphabet(IList<string> namedBlocks, string removedCategory)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i]).Append(@"}");

      // Append the subtraction of the removed category using the 'p' escape.
      regex.Append(@"-[").Append(@"\p{").Append(removedCategory).Append(@"}]]");

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// A function to create a snippet of a Regex for a user-chosen alphabet. This only creates a
    /// snippet in the format of: [<paramref name="namedBlocks"/> -
    /// [<paramref name="removedCategory"/>]]. This does no error checking on valid General
    /// Categories! Please refer to the .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-unicode-general-categories">
    /// general categories</a>.
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an <see cref="IList{T}"/> containing
    /// <see cref="UnicodeNamedBlockType.IsBasicLatin"/>.</param>
    /// <param name="removedCategory">The General Category that will be removed from the alphabet.
    /// Use this to remove certain characters. For example, pass 'L' to remove all letters.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/> - [<paramref name="removedCategory"/>]].</returns>
    public static string CreateAlphabet(IList<UnicodeNamedBlockType> namedBlocks, string removedCategory)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i].NamedBlockToString()).Append(@"}");

      // Append the subtraction of the removed category using the 'p' escape.
      regex.Append(@"-[").Append(@"\p{").Append(removedCategory).Append(@"}]]");

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// A function to create a snippet of a Regex for a user-chosen alphabet. This only creates a
    /// snippet in the format of: [<paramref name="namedBlocks"/> -
    /// [<paramref name="removedCategory"/>]]. This does no error checking on valid named blocks!
    /// Please refer to the .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an <see cref="IList{T}"/>
    /// containing 'IsBasicLatin'.</param>
    /// <param name="removedCategory">The General Category that will be removed from the alphabet.
    /// Use this to remove certain characters. For example, pass <see cref="UnicodeCategoryType.L"/>
    /// to remove all letters.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/> - [<paramref name="removedCategory"/>]].</returns>
    public static string CreateAlphabet(IList<string> namedBlocks, UnicodeCategoryType removedCategory)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i]).Append(@"}");

      // Append the subtraction of the removed category using the 'p' escape.
      regex.Append(@"-[").Append(@"\p{").Append(removedCategory.ToString()).Append(@"}]]");

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// A function to create a snippet of a Regex for a user-chosen alphabet. This only creates a
    /// snippet in the format of: [<paramref name="namedBlocks"/> -
    /// [<paramref name="removedCategories"/>]].
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an <see cref="IList{T}"/> containing
    /// <see cref="UnicodeNamedBlockType.IsBasicLatin"/>.</param>
    /// <param name="removedCategories">The General Categories that will be removed from the
    /// alphabet. Use these to remove certain characters. For examnple, pass an
    /// <see cref="IList{T}"/> containing <see cref="UnicodeCategoryType.L"/>
    /// to remove all letters.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/> - [<paramref name="removedCategories"/>]].</returns>
    public static string CreateAlphabet(IList<UnicodeNamedBlockType> namedBlocks, IList<UnicodeCategoryType> removedCategories)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i].NamedBlockToString()).Append(@"}");
      
      // If there are any removed categories, we have to append to the Regex string.
      if (!removedCategories.IsEmptyOrNull())
      {
        regex.Append(@"-["); // Append a subtraction.
        count = removedCategories.Count; // Get the number of categories.

        // For all categories, append using the 'p' escape.
        for (int i = 0; i < count; i++)
          regex.Append(@"\p{").Append(removedCategories[i].ToString()).Append(@"}");

        regex.Append(@"]"); // Append an ending bracket.
      }

      regex.Append(@"]"); // Append the last bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// A function to create a snippet of a Regex for a user-chosen alphabet. This only creates a
    /// snippet in the format of: [<paramref name="namedBlocks"/> -
    /// [<paramref name="removedCategories"/>]]. This does no error checking on what is valid!
    /// Please refer to the .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a> and 
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-unicode-general-categories">
    /// general categories</a>.
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an <see cref="IList{T}"/>
    /// containing "IsBasicLatin".</param>
    /// <param name="removedCategories">The General Categories that will be removed from the
    /// alphabet. Use these to remove certain characters. For example, pass an
    /// <see cref="IList{T}"/> containing 'L' to remove all letters.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/> - [<paramref name="removedCategories"/>]].</returns>
    public static string CreateAlphabet(IList<string> namedBlocks, IList<string> removedCategories)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i]).Append(@"}");

      // If there are any removed categories, we have to append to the Regex string.
      if (!removedCategories.IsEmptyOrNull())
      {
        regex.Append(@"-["); // Append a subtraction.
        count = removedCategories.Count; // Get the number of categories.

        // For all categories, append using the 'p' escape.
        for (int i = 0; i < count; i++)
          regex.Append(@"\p{").Append(removedCategories[i]).Append(@"}");

        regex.Append(@"]"); // Append an ending bracket.
      }

      regex.Append(@"]"); // Append the last bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// A function to create a snippet of a Regex for a user-chosen alphabet. This only creates a
    /// snippet in the format of: [<paramref name="namedBlocks"/> -
    /// [<paramref name="removedCategories"/>]]. This does no error checking on valid
    /// General Categories! Please refer to the .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-unicode-general-categories">
    /// general categories</a>.
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an <see cref="IList{T}"/>
    /// containing <see cref="UnicodeNamedBlockType.IsBasicLatin"/>.</param>
    /// <param name="removedCategories">The General Categories that will be removed from the
    /// alphabet. Use these to remove certain characters. For examnple, pass an
    /// <see cref="IList{T}"/> containing 'L' to remove all letters.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/> - [<paramref name="removedCategories"/>]].</returns>
    public static string CreateAlphabet(IList<UnicodeNamedBlockType> namedBlocks, IList<string> removedCategories)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i].NamedBlockToString()).Append(@"}");

      // If there are any removed categories, we have to append to the Regex string.
      if (!removedCategories.IsEmptyOrNull())
      {
        regex.Append(@"-["); // Append a subtraction.
        count = removedCategories.Count; // Get the number of categories.

        // For all categories, append using the 'p' escape.
        for (int i = 0; i < count; i++)
          regex.Append(@"\p{").Append(removedCategories[i]).Append(@"}");

        regex.Append(@"]"); // Append an ending bracket.
      }

      regex.Append(@"]"); // Append the last bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// A function to create a snippet of a Regex for a user-chosen alphabet. This only creates a
    /// snippet in the format of: [<paramref name="namedBlocks"/> -
    /// [<paramref name="removedCategories"/>]]. This does no error checking on valid
    /// named blocks! Please refer to the .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an <see cref="IList{T}"/>
    /// containing 'IsBasicLatin'.</param>
    /// <param name="removedCategories">The General Categories that will be removed from the
    /// alphabet. Use these to remove certain characters. For example, pass an
    /// <see cref="IList{T}"/> containing <see cref="UnicodeCategoryType.L"/>
    /// to remove all letters.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/> - [<paramref name="removedCategories"/>]].</returns>
    public static string CreateAlphabet(IList<string> namedBlocks, IList<UnicodeCategoryType> removedCategories)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i]).Append(@"}");

      // If there are any removed categories, we have to append to the Regex string.
      if (!removedCategories.IsEmptyOrNull())
      {
        regex.Append(@"-["); // Append a subtraction.
        count = removedCategories.Count; // Get the number of categories.

        // For all categories, append using the 'p' escape.
        for (int i = 0; i < count; i++)
          regex.Append(@"\p{").Append(removedCategories[i].ToString()).Append(@"}");

        regex.Append(@"]"); // Append an ending bracket.
      }

      regex.Append(@"]"); // Append the last bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Letters in a Unicode Named Block.
    /// </summary>
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet.
    /// If unsure, simply pass <see cref="UnicodeNamedBlockType.IsBasicLatin"/>.</param>
    /// <returns>Returns the Regex snippet representing the wanted
    /// alphabet with only Letters allowed.</returns>
    public static string CreateLetterAlphabet(UnicodeNamedBlockType namedBlock)
    {
      // Create a string representing an enclosure of the wanted block and just Letters, using the 'p' escape.
      return new StringBuilder(@"[\p{").Append(namedBlock.NamedBlockToString()).Append(@"}-").Append(RegexOnlyLetters).Append(@"]").ToString();
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Letters in a Unicode Named Block.
    /// This does no error checking on valid  named blocks! Please refer to the
    /// .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet. If unsure, simply pass 'IsBasicLatin'.</param>
    /// <returns>Returns the Regex snippet representing the wanted
    /// alphabet with only Letters allowed.</returns>
    public static string CreateLetterAlphabet(string namedBlock)
    {
      // Create a string representing an enclosure of the wanted block and just Letters, using the 'p' escape.
      return new StringBuilder(@"[\p{").Append(namedBlock).Append(@"}-").Append(RegexOnlyLetters).Append(@"]").ToString();
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Letters in a series of Unicode Named Blocks.
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an <see cref="IList{T}"/>
    /// containing <see cref="UnicodeNamedBlockType.IsBasicLatin"/>.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/>].</returns>
    public static string CreateLetterAlphabet(IList<UnicodeNamedBlockType> namedBlocks)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i].NamedBlockToString()).Append(@"}");

      regex.Append(@"-").Append(RegexOnlyLetters).Append(@"]"); // Append the codes for the subtraction, and the final bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Letters in a series of Unicode Named Blocks.
    /// This does no error checking on valid named blocks! Please refer to the
    /// .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an <see cref="IList{T}"/>
    /// containing 'IsBasicLatin'.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/>].</returns>
    public static string CreateLetterAlphabet(IList<string> namedBlocks)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i]).Append(@"}");

      regex.Append(@"-").Append(RegexOnlyLetters).Append(@"]"); // Append the codes for the subtraction, and the final bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Numbers in a Unicode Named Block.
    /// </summary>
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet. If unsure, simply pass
    /// <see cref="UnicodeNamedBlockType.IsBasicLatin"/>.</param>
    /// <returns>Returns the Regex snippet representing the wanted
    /// alphabet with only Numbers allowed.</returns>
    public static string CreateNumberAlphabet(UnicodeNamedBlockType namedBlock)
    {
      // Create a string representing an enclosure of the wanted block and just Numbers, using the 'p' escape.
      return new StringBuilder(@"[\p{").Append(namedBlock.NamedBlockToString()).Append(@"}-").Append(RegexOnlyNumbers).Append(@"]").ToString();
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Numbers in a Unicode Named Block.
    /// This does no error checking on valid named blocks! Please refer to the
    /// .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet. If unsure, simply pass 'IsBasicLatin'.</param>
    /// <returns>Returns the Regex snippet representing the wanted
    /// alphabet with only Numbers allowed.</returns>
    public static string CreateNumberAlphabet(string namedBlock)
    {
      // Create a string representing an enclosure of the wanted block and just Numbers, using the 'p' escape.
      return new StringBuilder(@"[\p{").Append(namedBlock).Append(@"}-").Append(RegexOnlyNumbers).Append(@"]").ToString();
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Numbers in a series of Unicode Named Blocks.
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an <see cref="IList{T}"/> containing
    /// <see cref="UnicodeNamedBlockType.IsBasicLatin"/>.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/>].</returns>
    public static string CreateNumberAlphabet(IList<UnicodeNamedBlockType> namedBlocks)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i].NamedBlockToString()).Append(@"}");

      regex.Append(@"-").Append(RegexOnlyNumbers).Append(@"]"); // Append the codes for the subtraction, and the final bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Numbers in a series of Unicode Named Blocks.
    /// This does no error checking on valid named blocks! Please refer to the
    /// .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an <see cref="IList{T}"/>
    /// containing 'IsBasicLatin'.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/>].</returns>
    public static string CreateNumberAlphabet(IList<string> namedBlocks)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i]).Append(@"}");

      regex.Append(@"-").Append(RegexOnlyNumbers).Append(@"]"); // Append the codes for the subtraction, and the final bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Marks in a Unicode Named Block.
    /// This does no error checking on valid named blocks! Please refer to the
    /// .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet. If unsure, simply pass 'IsBasicLatin'.</param>
    /// <returns>Returns the Regex snippet representing the wanted
    /// alphabet with only Marks allowed.</returns>
    public static string CreateMarkAlphabet(UnicodeNamedBlockType namedBlock)
    {
      // Create a string representing an enclosure of the wanted block and just Marks, using the 'p' escape.
      return new StringBuilder(@"[\p{").Append(namedBlock.NamedBlockToString()).Append(@"}-").Append(RegexOnlyMarks).Append(@"]").ToString();
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Marks in a Unicode Named Block.
    /// This does no error checking on valid named blocks! Please refer to the
    /// .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet. If unsure, simply pass 'IsBasicLatin'.</param>
    /// <returns>Returns the Regex snippet representing the wanted
    /// alphabet with only Marks allowed.</returns>
    public static string CreateMarkAlphabet(string namedBlock)
    {
      // Create a string representing an enclosure of the wanted block and just Marks, using the 'p' escape.
      return new StringBuilder(@"[\p{").Append(namedBlock).Append(@"}-").Append(RegexOnlyMarks).Append(@"]").ToString();
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Marks in a series of Unicode Named Blocks.
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an <see cref="IList{T}"/> containing
    /// <see cref="UnicodeNamedBlockType.IsBasicLatin"/>.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/>].</returns>
    public static string CreateMarkAlphabet(IList<UnicodeNamedBlockType> namedBlocks)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i].NamedBlockToString()).Append(@"}");

      regex.Append(@"-").Append(RegexOnlyMarks).Append(@"]"); // Append the codes for the subtraction, and the final bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Marks in a series of Unicode Named Blocks.
    /// This does no error checking on valid named blocks! Please refer to the
    /// .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an <see cref="IList{T}"/>
    /// containing 'IsBasicLatin'.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/>].</returns>
    public static string CreateMarkAlphabet(IList<string> namedBlocks)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i]).Append(@"}");

      regex.Append(@"-").Append(RegexOnlyMarks).Append(@"]"); // Append the codes for the subtraction, and the final bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Punctuation in a Unicode Named Block.
    /// This does no error checking on valid named blocks! Please refer to the
    /// .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet. If unsure, simply pass 'IsBasicLatin'.</param>
    /// <returns>Returns the Regex snippet representing the wanted
    /// alphabet with only Punctuation allowed.</returns>
    public static string CreatePunctuationAlphabet(UnicodeNamedBlockType namedBlock)
    {
      // Create a string representing an enclosure of the wanted block and just Punctuation, using the 'p' escape.
      return new StringBuilder(@"[\p{").Append(namedBlock.NamedBlockToString()).Append(@"}-").Append(RegexOnlyPunctuation).Append(@"]").ToString();
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Punctuation in a Unicode Named Block.
    /// This does no error checking on valid named blocks! Please refer to the
    /// .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet. If unsure, simply pass 'IsBasicLatin'.</param>
    /// <returns>Returns the Regex snippet representing the wanted
    /// alphabet with only Punctuation allowed.</returns>
    public static string CreatePunctuationAlphabet(string namedBlock)
    {
      // Create a string representing an enclosure of the wanted block and just Punctuation, using the 'p' escape.
      return new StringBuilder(@"[\p{").Append(namedBlock).Append(@"}-").Append(RegexOnlyPunctuation).Append(@"]").ToString();
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Punctuation in a series of Unicode Named Blocks.
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an <see cref="IList{T}"/> containing
    /// <see cref="UnicodeNamedBlockType.IsBasicLatin"/>.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/>].</returns>
    public static string CreatePunctuationAlphabet(IList<UnicodeNamedBlockType> namedBlocks)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i].NamedBlockToString()).Append(@"}");

      regex.Append(@"-").Append(RegexOnlyPunctuation).Append(@"]"); // Append the codes for the subtraction, and the final bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Punctuation in a series of Unicode Named Blocks.
    /// This does no error checking on valid named blocks! Please refer to the
    /// .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an <see cref="IList{T}"/>
    /// containing 'IsBasicLatin'.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/>].</returns>
    public static string CreatePunctuationAlphabet(IList<string> namedBlocks)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i]).Append(@"}");

      regex.Append(@"-").Append(RegexOnlyPunctuation).Append(@"]"); // Append the codes for the subtraction, and the final bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Symbols in a Unicode Named Block.
    /// This does no error checking on valid named blocks! Please refer to the
    /// .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet. If unsure, simply pass 'IsBasicLatin'.</param>
    /// <returns>Returns the Regex snippet representing the wanted
    /// alphabet with only Symbols allowed.</returns>
    public static string CreateSymbolAlphabet(UnicodeNamedBlockType namedBlock)
    {
      // Create a string representing an enclosure of the wanted block and just Symbols, using the 'p' escape.
      return new StringBuilder(@"[\p{").Append(namedBlock.NamedBlockToString()).Append(@"}-").Append(RegexOnlySymbols).Append(@"]").ToString();
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Symbols in a Unicode Named Block.
    /// This does no error checking on valid named blocks! Please refer to the
    /// .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet. If unsure, simply pass 'IsBasicLatin'.</param>
    /// <returns>Returns the Regex snippet representing the wanted
    /// alphabet with only Symbols allowed.</returns>
    public static string CreateSymbolAlphabet(string namedBlock)
    {
      // Create a string representing an enclosure of the wanted block and just Symbols, using the 'p' escape.
      return new StringBuilder(@"[\p{").Append(namedBlock).Append(@"}-").Append(RegexOnlySymbols).Append(@"]").ToString();
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Symbols in a series of Unicode Named Blocks.
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an <see cref="IList{T}"/> containing
    /// <see cref="UnicodeNamedBlockType.IsBasicLatin"/>.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/>].</returns>
    public static string CreateSymbolAlphabet(IList<UnicodeNamedBlockType> namedBlocks)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i].NamedBlockToString()).Append(@"}");

      regex.Append(@"-").Append(RegexOnlySymbols).Append(@"]"); // Append the codes for the subtraction, and the final bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Symbols in a series of Unicode Named Blocks.
    /// This does no error checking on valid named blocks! Please refer to the
    /// .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an <see cref="IList{T}"/>
    /// containing 'IsBasicLatin'.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/>].</returns>
    public static string CreateSymbolAlphabet(IList<string> namedBlocks)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i]).Append(@"}");

      regex.Append(@"-").Append(RegexOnlySymbols).Append(@"]"); // Append the codes for the subtraction, and the final bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Separators in a Unicode Named Block.
    /// This does no error checking on valid named blocks! Please refer to the
    /// .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet. If unsure, simply pass 'IsBasicLatin'.</param>
    /// <returns>Returns the Regex snippet representing the wanted
    /// alphabet with only Separators allowed.</returns>
    public static string CreateSeparatorAlphabet(UnicodeNamedBlockType namedBlock)
    {
      // Create a string representing an enclosure of the wanted block and just Separators, using the 'p' escape.
      return new StringBuilder(@"[\p{").Append(namedBlock.NamedBlockToString()).Append(@"}-").Append(RegexOnlySeparators).Append(@"]").ToString();
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Separators in a Unicode Named Block.
    /// This does no error checking on valid named blocks! Please refer to the
    /// .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet. If unsure, simply pass 'IsBasicLatin'.</param>
    /// <returns>Returns the Regex snippet representing the wanted
    /// alphabet with only Separators allowed.</returns>
    public static string CreateSeparatorAlphabet(string namedBlock)
    {
      // Create a string representing an enclosure of the wanted block and just Separators, using the 'p' escape.
      return new StringBuilder(@"[\p{").Append(namedBlock).Append(@"}-").Append(RegexOnlySeparators).Append(@"]").ToString();
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Separators in a series of Unicode Named Blocks.
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an <see cref="IList{T}"/> containing
    /// <see cref="UnicodeNamedBlockType.IsBasicLatin"/>.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/>].</returns>
    public static string CreateSeparatorAlphabet(IList<UnicodeNamedBlockType> namedBlocks)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i].NamedBlockToString()).Append(@"}");

      regex.Append(@"-").Append(RegexOnlySeparators).Append(@"]"); // Append the codes for the subtraction, and the final bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Separators in a series of Unicode Named Blocks.
    /// This does no error checking on valid named blocks! Please refer to the
    /// .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an <see cref="IList{T}"/>
    /// containing 'IsBasicLatin'.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/>].</returns>
    public static string CreateSeparatorAlphabet(IList<string> namedBlocks)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i]).Append(@"}");

      regex.Append(@"-").Append(RegexOnlySeparators).Append(@"]"); // Append the codes for the subtraction, and the final bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Other Characters in a Unicode Named Block.
    /// This does no error checking on valid named blocks! Please refer to the
    /// .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet. If unsure, simply pass 'IsBasicLatin'.</param>
    /// <returns>Returns the Regex snippet representing the wanted
    /// alphabet with only Other Characters allowed.</returns>
    public static string CreateOtherAlphabet(UnicodeNamedBlockType namedBlock)
    {
      // Create a string representing an enclosure of the wanted block and just Other, using the 'p' escape.
      return new StringBuilder(@"[\p{").Append(namedBlock.NamedBlockToString()).Append(@"}-").Append(RegexOnlyOther).Append(@"]").ToString();
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Other Characters in a Unicode Named Block.
    /// This does no error checking on valid named blocks! Please refer to the
    /// .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlock">The Named Block for the Regex. This is the language that is
    /// available in the alphabet. If unsure, simply pass 'IsBasicLatin'.</param>
    /// <returns>Returns the Regex snippet representing the wanted
    /// alphabet with only Other Characters allowed.</returns>
    public static string CreateOtherAlphabet(string namedBlock)
    {
      // Create a string representing an enclosure of the wanted block and just Other, using the 'p' escape.
      return new StringBuilder(@"[\p{").Append(namedBlock).Append(@"}-").Append(RegexOnlyOther).Append(@"]").ToString();
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Other Characters in a series of Unicode
    /// Named Blocks.
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an <see cref="IList{T}"/> containing
    /// <see cref="UnicodeNamedBlockType.IsBasicLatin"/>.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/>].</returns>
    public static string CreateOtherAlphabet(IList<UnicodeNamedBlockType> namedBlocks)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i].NamedBlockToString()).Append(@"}");

      regex.Append(@"-").Append(RegexOnlyOther).Append(@"]"); // Append the codes for the subtraction, and the final bracket.

      return regex.ToString(); // Return the string.
    }

    /// <summary>
    /// An easy way to create an alphabet of only the Other Characters in a series of Unicode
    /// Named Blocks. This does no error checking on valid named blocks! Please refer to the
    /// .NET standard for what are valid
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks">
    /// named blocks</a>.
    /// </summary>
    /// <param name="namedBlocks">The Named Blocks for the Regex. These are the languages that are
    /// available in the alphabet. If unsure, simply make an <see cref="IList{T}"/>
    /// containing 'IsBasicLatin'.</param>
    /// <returns>Returns a string representation of the alphabet snippet, in the format of:
    /// [<paramref name="namedBlocks"/>].</returns>
    public static string CreateOtherAlphabet(IList<string> namedBlocks)
    {
      // If there are no named blocks, return an empty string.
      if (namedBlocks.IsEmptyOrNull())
        return string.Empty;

      StringBuilder regex = new StringBuilder(@"["); // The Regex string. Start it off with a bracket.
      int count = namedBlocks.Count; // Get the number of named blocks.

      // For all named blocks, append using the 'p' escape.
      for (int i = 0; i < count; i++)
        regex.Append(@"\p{").Append(namedBlocks[i]).Append(@"}");

      regex.Append(@"-").Append(RegexOnlyOther).Append(@"]"); // Append the codes for the subtraction, and the final bracket.

      return regex.ToString(); // Return the string.
    }
  }
  /************************************************************************************************/
}