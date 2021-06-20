/**************************************************************************************************/
/*!
\file   ErrorLogMode.cs
\author Craig Williams
\par    Last Updated
        2021-06-19
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for an enum of how an ErrorLog handles logging Exceptions.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System;

namespace CodeParadox.Tenor.Diagnostics
{
  /************************************************************************************************/
  public abstract partial class Log
  {
    /**********************************************************************************************/
    /// <summary>
    /// An <see langword="enum"/> for how the <see cref="errorLog"/> tracks<see cref="Exception"/>s.
    /// </summary>
    public enum ErrorLogMode
    {
      /// <summary>No <see cref="Exception"/>s are logged.</summary>
      Off,
      /// <summary>Only <see cref="AppDomain.UnhandledException"/>s are logged.</summary>
      Unhandled,
      /// <summary>All <see cref="AppDomain.FirstChanceException"/>s are logged.</summary>
      All,
    }
    /**********************************************************************************************/
  }
  /************************************************************************************************/
}