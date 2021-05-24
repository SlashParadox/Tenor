/**************************************************************************************************/
/*!
\file   UnicodeCategory.cs
\author Craig Williams
\par    Last Updated
        2021-05-21
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file containing an enum for every Unicode General Category supported by .NET.

\par Bug List

\par References
*/
/**************************************************************************************************/

namespace CodeParadox.Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// A representation for every Unicode General Category supported by .NET.
  /// This list can be found in the
  /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-unicode-general-categories">
  /// .NET Documentation</a>. These are named exactly as they would be placed into a
  /// <see cref="System.Text.RegularExpressions.Regex"/>.
  /// </summary>
  public enum UnicodeCategory
  {
    /// <summary>All Letter Characters</summary>
    L,
    /// <summary>Letters, Uppercase</summary>
    Lu,
    /// <summary>Letters, Lowercase</summary>
    Ll,
    /// <summary>Letters, Titlecase</summary>
    Lt,
    /// <summary>Letters, Modifier</summary>
    Lm,
    /// <summary>Letters, Other</summary>
    Lo,
    /// <summary>All Marks</summary>
    M,
    /// <summary>Marks, Nonspacing</summary>
    Mn,
    /// <summary>Marks, Spacing Combining</summary>
    Mc,
    /// <summary>Marks, Enclosing</summary>
    Me,
    /// <summary>All Number Characters</summary>
    N,
    /// <summary>Numbers, Decimal Digit</summary>
    Nd,
    /// <summary>Numbers, Letter</summary>
    Nl,
    /// <summary>Numbers, Other</summary>
    No,
    /// <summary>All Punctuation Characters</summary>
    P,
    /// <summary>Punctuation, Connector</summary>
    Pc,
    /// <summary>Punctuation, Dash</summary>
    Pd,
    /// <summary>Punctuation, Open</summary>
    Ps,
    /// <summary>Punctuation, Close</summary>
    Pe,
    /// <summary>Punctuation, Initial Quote [May Behave Like Ps or Pe Depending on Usage]</summary>
    Pi,
    /// <summary>Punctuation, Final Quote [May Behave Like Ps or Pe Depending on Usage]</summary>
    Pf,
    /// <summary>Punctuation, Other</summary>
    Po,
    /// <summary>All Symbol Characters</summary>
    S,
    /// <summary>Symbols, Math</summary>
    Sm,
    /// <summary>Symbols, Currency</summary>
    Sc,
    /// <summary>Symbols, Modifier</summary>
    Sk,
    /// <summary>Symbols, Other</summary>
    So,
    /// <summary>All Separator Characters</summary>
    Z,
    /// <summary>Separators, Space</summary>
    Zs,
    /// <summary>Separators, Line</summary>
    Zl,
    /// <summary>Separators, Paragraph</summary>
    Zp,
    /// <summary>All Control/Other Characters</summary>
    C,
    /// <summary>Othere, Control</summary>
    Cc,
    /// <summary>Other, Format</summary>
    Cf,
    /// <summary>Other, Surrogate</summary>
    Cs,
    /// <summary>Other, Private Use</summary>
    Co,
    /// <summary>Other, Not Assigned [No Characters Have This Property]</summary>
    Cn,
  }
  /************************************************************************************************/
}