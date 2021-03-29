/**************************************************************************************************/
/*!
\file   GenericDelegate.cs
\author Craig Williams
\par    Last Updated
        2021-03-07
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for generic delegate implementation that can be used anywhere for multiple situations.

\par Bug List

\par References
*/
/**************************************************************************************************/

namespace Tenor
{
  /// <summary>
  /// A very basic delegate, taking in no value and outputting no value.
  /// </summary>
  public delegate void GenericDelegate();

  /// <summary>
  /// A very generic, single input delegate with no output.
  /// </summary>
  /// <typeparam name="T0">The type of <paramref name="t0"/></typeparam>
  /// <param name="t0">The first input for this delegate.</param>
  public delegate void GenericDelegate<T0>(T0 t0);

  /// <summary>
  /// A very generic, dual input delegate with no output.
  /// </summary>
  /// <typeparam name="T0">The type of <paramref name="t0"/></typeparam>
  /// <typeparam name="T1">The type of <paramref name="t1"/></typeparam>
  /// <param name="t0">The first input for this delegate.</param>
  /// <param name="t1">The second input for this delegate.</param>
  public delegate void GenericDelegate<T0, T1>(T0 t0, T1 t1);
}