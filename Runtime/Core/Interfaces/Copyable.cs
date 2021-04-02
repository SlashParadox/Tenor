/**************************************************************************************************/
/*!
\file   Copyable.cs
\author Craig Williams
\par    Last Updated
        2021-04-01
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for an interface that allows for copying values at runtime, without the copy constructor.

\par Bug List

\par References
*/
/**************************************************************************************************/

namespace Tenor
{
  /************************************************************************************************/
  /// <summary>
  /// A small interface which allows for copying values from one instance of
  /// <typeparamref name="TType"/> to another. This should be paired with a copy constructor.
  /// </summary>
  /// <typeparam name="TType">The type that can be copied from.</typeparam>
  public interface ICopyable<TType>
  {
    /// <summary>
    /// A function for copying the values from one instance of <typeparamref name="TType"/> to
    /// this instance.
    /// </summary>
    /// <param name="original">The original <typeparamref name="TType"/> to copy.</param>
    public abstract void CopyFrom(TType original);

    /// <summary>
    /// A function for copying the values of this instance of <typeparamref name="TType"/> to
    /// another instance. Use this for any special implementation of the reverse operation.
    /// Otherwise, merely call <see cref="CopyFrom(TType)"/> on the <paramref name="copy"/>.
    /// </summary>
    /// <param name="copy">The copy <typeparamref name="TType"/> to give values to.</param>
    public abstract void CopyTo(TType copy);
  }
  /************************************************************************************************/
}