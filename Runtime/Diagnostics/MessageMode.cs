/**************************************************************************************************/
/*!
\file   MessageMode.cs
\author Craig Williams
\par    Last Updated
        2021-06-18
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for a class representing a Log's form of handling messages.

\par Bug List

\par References
*/
/**************************************************************************************************/

namespace CodeParadox.Tenor.Diagnostics
{
  /************************************************************************************************/
  public abstract partial class Log
  {
    /**********************************************************************************************/
    /// <summary>
    /// An <see langword="enum"/> for how the <see cref="Log"/> handles messages.
    /// </summary>
    public enum MessageMode
    {
      /// <summary>No messages are logged.</summary>
      Off,
      /// <summary>Messages are only logged to the <see cref="System.Console"/>.</summary>
      ConsoleOnly,
      /// <summary>Messages are only logged to a file.</summary>
      FileOnly,
      /// <summary>Messages are logged to the <see cref="System.Console"/> and a file.</summary>
      ConsoleAndFile,
    }
    /**********************************************************************************************/
  }
  /************************************************************************************************/
}