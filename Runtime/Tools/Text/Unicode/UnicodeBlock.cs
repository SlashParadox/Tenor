/**************************************************************************************************/
/*!
\file   UnicodeBlock.cs
\author Craig Williams
\par    Last Updated
        2021-05-21
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A special struct for a block of Unicode characters. Typically, these should only be used for
  actual blocks that are supported in C#.

\par Bug List

\par References
*/
/**************************************************************************************************/

namespace SlashParadox.Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// A block of <see cref="Unicode"/> characters. Typically, new blocks are not needed. The
  /// <see cref="Unicode"/> class contains all blocks that are supported by .NET and C#.
  /// </summary>
  public readonly struct UnicodeBlock
  {
    /// <summary>The name of the <see cref="Unicode"/> block.</summary>
    public readonly string name;
    /// <summary>The character that starts this block's <see cref="Unicode"/> range.</summary>
    public readonly char rangeStart;
    /// <summary>The character that ends this block's <see cref="Unicode"/> range.</summary>
    public readonly char rangeEnd;

    /// <summary>
    /// A constructor for a Block of <see cref="Unicode"/> characters.
    /// </summary>
    /// <param name="name">See: <see cref="name"/>.</param>
    /// <param name="rangeStart">See: <see cref="rangeStart"/>.</param>
    /// <param name="rangeEnd">See: <see cref="rangeEnd"/>.</param>
    public UnicodeBlock(string name, char rangeStart, char rangeEnd)
    {
      this.name = name;
      this.rangeStart = rangeStart;
      this.rangeEnd = rangeEnd;
    }
  }
  /************************************************************************************************/
}