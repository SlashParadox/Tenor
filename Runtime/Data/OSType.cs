/**************************************************************************************************/
/*!
\file   OSType.cs
\author Craig Williams
\par    Last Updated
        2021-06-09
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for an enum about general operating systems.

\par Bug List

\par References
*/
/**************************************************************************************************/

namespace SlashParadox.Tenor.Data
{
  /************************************************************************************************/
  /// <summary>
  /// An <see langword="enum"/> for the general type of operating system the program is running on.
  /// See <see cref="Program"/> for how to get the current general operating system.
  /// </summary>
  public enum OSType
  {
    /// <summary>The operating system is not a known standard.</summary>
    NonStandard,
    /// <summary>The operating system is some form of Windows.</summary>
    Windows,
    /// <summary>The operating system is some form of Linux.</summary>
    Linux,
    /// <summary>The operating system is some form of Mac OSX.</summary>
    OSX,
  }
  /************************************************************************************************/
}