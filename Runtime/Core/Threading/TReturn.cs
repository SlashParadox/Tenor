/**************************************************************************************************/
/*!
\file   TReturn.cs
\author Craig Williams
\par    Last Updated
        2021-06-21
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for classes that are useful for returning from an asynchronous task.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System.Threading.Tasks;

namespace CodeParadox.Tenor.Threading
{
  /************************************************************************************************/
  /// <summary>
  /// A useful class for returning a from a <see cref="Task"/>, while giving a way of checking on
  /// if the <see cref="Task"/> was successful.
  /// </summary>
  public abstract partial class TReturn
  {
    /// <summary>A check on if the <see cref="Task"/>returned successfully.</summary>
    public bool isValid = false;
  }
  /************************************************************************************************/
  /************************************************************************************************/
  /// <summary>
  /// A useful class for returning a <see cref="value"/> from a <see cref="Task"/>, while giving
  /// a way of checking on if the <see cref="Task"/> was successful.
  /// </summary>
  /// <typeparam name="T0">The type that is being returned.</typeparam>
  public sealed partial class TReturn<T0> : TReturn
  {
    /// <summary>The final value. Check the <see cref="Task"/> with <see cref="isValid"/>.</summary>
    public T0 value = default;
  }
  /************************************************************************************************/
  /************************************************************************************************/
  /// <summary>
  /// A useful class for returning a <see cref="value"/> from a <see cref="Task"/>, while giving
  /// a way of checking on if the <see cref="Task"/> was successful.
  /// </summary>
  /// <typeparam name="T1">The first type that is being returned.</typeparam>
  /// <typeparam name="T2">The second type that is being returned.</typeparam>
  public sealed partial class TReturn<T1, T2> : TReturn
  {
    /// <summary>The first value. Check the <see cref="Task"/> with <see cref="isValid"/>.</summary>
    public T1 value1 = default;
    /// <summary>The second value. Check the <see cref="Task"/> with <see cref="isValid"/>.</summary>
    public T2 value2 = default;
  }
  /************************************************************************************************/
}