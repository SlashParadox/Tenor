/**************************************************************************************************/
/*!
\file   Program.cs
\author Craig Williams
\par    Last Updated
        2021-06-18
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for a class that contains data pertaining to the current program and how it's running.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SlashParadox.Tenor.Data
{
  /************************************************************************************************/
  /// <summary>
  /// A class of data on how the current application is running. Information such as the operating
  /// system can be found here.
  /// </summary>
  public static partial class Program
  {
    /// <summary>The current <see cref="OSPlatform"/> the program is running on.</summary>
    public static OSPlatform CurrentOSPlatform { get; private set; }
    /// <summary>The general type of operating system the program is running on.</summary>
    public static OSType CurrentOSType { get; private set; }

    /// <summary>
    /// A static constructor for general <see cref="Program"/> data.
    /// </summary>
    static Program()
    {
      DetermineCurrentOSPlatform(); // Get the current operating system.
    }

    /// <summary>
    /// A helper function for determining the current operating system.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void DetermineCurrentOSPlatform()
    {
      // Always create a new OSPlatform based on the current description.
      CurrentOSPlatform = OSPlatform.Create(RuntimeInformation.OSDescription);

      // Check the primary three operating systems first, before creating a unique one otherwise.
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        CurrentOSType = OSType.Windows;
      else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        CurrentOSType = OSType.Linux;
      else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        CurrentOSType = OSType.OSX;
      else
        CurrentOSType = OSType.NonStandard;
    }
  }
  /************************************************************************************************/
}