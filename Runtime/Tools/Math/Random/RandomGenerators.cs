/**************************************************************************************************/
/*!
\file   Randomization.cs
\author Craig Williams
\par    Last Updated
        2021-05-24
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for an enum used for standard random number generators used by Tenor globally.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System.Security.Cryptography;

namespace SlashParadox.Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// An enum for selecting a global random generator. This is primarily used in the
  /// <see cref="Randomization"/> tool.
  /// </summary>
  public enum RandomGenerators
  {
    /// <summary>The standard <see cref="System.Random"/> class.</summary>
    NETStandard,
    /// <summary>The <see cref="Math.RejectionRandom"/> class, based off the standard
    /// <see cref="System.Random"/> class.</summary>
    RejectionRandom,
    /// <summary>The standard <see cref="RNGCryptoServiceProvider"/> class.</summary>
    CryptoServiceProvider,
  }
  /************************************************************************************************/
}