/**************************************************************************************************/
/*!
\file   TaskTokenSource.cs
\author Craig Williams
\par    Last Updated
        2021-06-19
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file containing implementation of a class that can be used to link and track Task
  CancellationTokens.

\par Bug List

\par References
*/
/**************************************************************************************************/

using SlashParadox.Tenor.Tools;
using System;
using System.Threading;

namespace SlashParadox.Tenor.Threading
{
  /************************************************************************************************/
  /// <summary>
  /// A wrapper around a <see cref="CancellationTokenSource"/>. This allows for managing all
  /// <see cref="CancellationToken"/>s under one global <see cref="CancellationTokenSource"/>.
  /// </summary>
  public partial class TaskTokenSource : IDisposable
  {
    /// <summary>The <see cref="CancellationToken"/> associated with the global
    /// <see cref="CancellationTokenSource"/>.</summary>
    public static CancellationToken GlobalToken { get { return globalSource.Token; } }

    /// <summary>The global <see cref="CancellationTokenSource"/>. Cancelling this cancels all
    /// <see cref="TaskTokenSource"/>s at once. A new one is made each cancellation.</summary>
    private static CancellationTokenSource globalSource;

    /// <summary>The <see cref="CancellationToken"/> associated with the inner
    /// <see cref="CancellationTokenSource"/>.</summary>
    public CancellationToken Token { get { return selfSource.Token; } }

    /// <summary>The internal <see cref="CancellationTokenSource"/>.</summary>
    private readonly CancellationTokenSource selfSource;

    /// <summary>
    /// The static constructor for <see cref="TaskTokenSource"/>s.
    /// </summary>
    static TaskTokenSource()
    {
      ResetSource(); // Register the global source.
    }

    /// <summary>
    /// A constructor for a <see cref="TaskTokenSource"/>.
    /// </summary>
    public TaskTokenSource()
    {
      // Create a new CancellationTokenSource, linked to the global token.
      selfSource = CancellationTokenSource.CreateLinkedTokenSource(globalSource.Token);
    }

    /// <summary>
    /// A constructor for a <see cref="TaskTokenSource"/>, which cancels itself after a
    /// <paramref name="delay"/>.
    /// </summary>
    /// <param name="delay">The <see cref="TimeSpan"/> to wait before canceling the internal
    /// <see cref="CancellationTokenSource"/>.</param>
    /// <exception cref="ObjectDisposedException">The exception thrown when the internal
    /// <see cref="CancellationTokenSource"/> has been disposed.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The exception thrown when
    /// <paramref name="delay"/> is less than -1 or greater than
    /// <see cref="int.MaxValue"/>.</exception>
    public TaskTokenSource(TimeSpan delay) : this()
    {
      selfSource.CancelAfter(delay);
    }

    /// <summary>
    /// A constructor for a <see cref="TaskTokenSource"/>, which cancels itself after a
    /// <paramref name="millisecondsDelay"/>.
    /// </summary>
    /// <param name="millisecondsDelay">The time to wait before canceling the internal
    /// <see cref="CancellationTokenSource"/>.</param>
    /// <exception cref="ObjectDisposedException">The <see cref="Exception"/> thrown when the
    /// internal <see cref="CancellationTokenSource"/> has been disposed.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The <see cref="Exception"/> thrown when
    /// <paramref name="millisecondsDelay"/> is less than -1.</exception>
    public TaskTokenSource(int millisecondsDelay) : this()
    {
      selfSource.CancelAfter(millisecondsDelay);
    }

    /// <summary>
    /// A constructor for a <see cref="TaskTokenSource"/>.
    /// </summary>
    /// <param name="linkedTokens">Extra <see cref="CancellationToken"/>s to link
    /// to this source.</param>
    public TaskTokenSource(params CancellationToken[] linkedTokens)
    {
      // If there are no tokens, create a source that's just linked to the global source.
      if (linkedTokens.IsEmptyOrNull())
        selfSource = CancellationTokenSource.CreateLinkedTokenSource(globalSource.Token);
      else
      {
        // Get the number of tokens, and create a new array 1 greater than that.
        int length = linkedTokens.Length;
        CancellationToken[] tokens = new CancellationToken[length + 1];

        // Copy over all of the tokens.
        for (int i = 0; i < length; i++)
          tokens[i] = linkedTokens[i];

        tokens[length] = globalSource.Token; // Add the global token.
        // Create the final source.
        selfSource = CancellationTokenSource.CreateLinkedTokenSource(tokens);
      }
    }

    /// <summary>
    /// A constructor for a <see cref="TaskTokenSource"/>, which cancels itself after a
    /// <paramref name="delay"/>.
    /// </summary>
    /// <param name="delay">The <see cref="TimeSpan"/> to wait before canceling the internal
    /// <see cref="CancellationTokenSource"/>.</param>
    /// <param name="linkedTokens">Extra <see cref="CancellationToken"/>s to link
    /// to this source.</param>
    /// <exception cref="ObjectDisposedException">The exception thrown when the internal
    /// <see cref="CancellationTokenSource"/> has been disposed.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The exception thrown when
    /// <paramref name="delay"/> is less than -1 or greater than
    /// <see cref="int.MaxValue"/>.</exception>
    public TaskTokenSource(TimeSpan delay, params CancellationToken[] linkedTokens)
      : this(linkedTokens)
    {
      selfSource.CancelAfter(delay);
    }

    /// <summary>
    /// A constructor for a <see cref="TaskTokenSource"/>, which cancels itself after a
    /// <paramref name="millisecondsDelay"/>.
    /// </summary>
    /// <param name="millisecondsDelay">The time to wait before canceling the internal
    /// <see cref="CancellationTokenSource"/>.</param>
    /// <param name="linkedTokens">Extra <see cref="CancellationToken"/>s to link
    /// to this source.</param>
    /// <exception cref="ObjectDisposedException">The <see cref="Exception"/> thrown when the
    /// internal <see cref="CancellationTokenSource"/> has been disposed.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The <see cref="Exception"/> thrown when
    /// <paramref name="millisecondsDelay"/> is less than -1.</exception>
    public TaskTokenSource(int millisecondsDelay, params CancellationToken[] linkedTokens)
      : this(linkedTokens)
    {
      selfSource.CancelAfter(millisecondsDelay);
    }

    /// <summary>
    /// A function to communicate a request for cancellation of the global
    /// <see cref="CancellationTokenSource"/>.
    /// </summary>
    /// <exception cref="ObjectDisposedException">The <see cref="CancellationTokenSource"/>
    /// has been disposed.</exception>
    /// <exception cref="AggregateException">An aggregate <see cref="Exception"/> containing all
    /// the <see cref="Exception"/>s thrown by the registered callbacks on the associated
    /// <see cref="CancellationToken"/>.</exception>
    public static void GlobalCancel()
    {
      globalSource.Cancel();
    }

    /// <summary>
    /// A function to communicate a request for cancellation of the global
    /// <see cref="CancellationTokenSource"/>, and specifies whether remaining callbacks and
    /// cancelable operations should be processed if an <see cref="Exception"/> occurs.
    /// </summary>
    /// <param name="throwOnFirstException"> Pass <see langword="true"/> if exceptions should
    /// immediately propagate. Otherwise, pass <see langword="false"/>.</param>
    /// <exception cref="ObjectDisposedException">The <see cref="CancellationTokenSource"/>
    /// has been disposed.</exception>
    /// <exception cref="AggregateException">An aggregate <see cref="Exception"/> containing all
    /// the <see cref="Exception"/>s thrown by the registered callbacks on the associated
    /// <see cref="CancellationToken"/>.</exception>
    public static void GlobalCancel(bool throwOnFirstException)
    {
      globalSource.Cancel(throwOnFirstException);
    }

    /// <summary>
    /// A function to schedule a cancel operation on the global
    /// <see cref="CancellationTokenSource"/> after the specified number of milliseconds.
    /// </summary>
    /// <param name="millisecondsDelay">The time to wait before canceling the global
    /// <see cref="CancellationTokenSource"/>.</param>
    /// global <exception cref="ObjectDisposedException">The <see cref="Exception"/> thrown when the
    /// <see cref="CancellationTokenSource"/> has been disposed.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The <see cref="Exception"/> thrown when
    /// <paramref name="millisecondsDelay"/> is less than -1.</exception>
    public static void GlobalCancelAfter(int millisecondsDelay)
    {
      globalSource.CancelAfter(millisecondsDelay);
    }

    /// <summary>
    /// A function to schedule a cancel operation on the global
    /// <see cref="CancellationTokenSource"/> after the specified <see cref="TimeSpan"/>.
    /// </summary>
    /// <param name="delay">The <see cref="TimeSpan"/> to wait before canceling the global
    /// <see cref="CancellationTokenSource"/>.</param>
    /// <exception cref="ObjectDisposedException">The exception thrown when the global
    /// <see cref="CancellationTokenSource"/> has been disposed.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The exception thrown when
    /// <paramref name="delay"/> is less than -1 or greater than
    /// <see cref="int.MaxValue"/>.</exception>
    public static void GlobalCancelAfter(TimeSpan delay)
    {
      globalSource.CancelAfter(delay);
    }

    /// <summary>
    /// A function called when the global <see cref="CancellationTokenSource"/> is cancelled.
    /// A new source is created and initialized.
    /// </summary>
    private static void ResetSource()
    {
      // Dispose of the old source.
      if (globalSource != null)
        globalSource.Dispose();

      globalSource = new CancellationTokenSource(); // Create a new source.
      globalSource.Token.Register(ResetSource); // Register the reset function.
    }

    /// <summary>
    /// A function to communicate a request for cancellation.
    /// </summary>
    /// <exception cref="ObjectDisposedException">The <see cref="CancellationTokenSource"/>
    /// has been disposed.</exception>
    /// <exception cref="AggregateException">An aggregate <see cref="Exception"/> containing all
    /// the <see cref="Exception"/>s thrown by the registered callbacks on the associated
    /// <see cref="CancellationToken"/>.</exception>
    public void Cancel()
    {
      selfSource.Cancel();
    }

    /// <summary>
    /// A function to communicate a request for cancellation, and specifies whether
    /// remaining callbacks and cancelable operations should be processed if an
    /// <see cref="Exception"/> occurs.
    /// </summary>
    /// <param name="throwOnFirstException"> Pass <see langword="true"/> if exceptions should
    /// immediately propagate. Otherwise, pass <see langword="false"/>.</param>
    /// <exception cref="ObjectDisposedException">The <see cref="CancellationTokenSource"/>
    /// has been disposed.</exception>
    /// <exception cref="AggregateException">An aggregate <see cref="Exception"/> containing all
    /// the <see cref="Exception"/>s thrown by the registered callbacks on the associated
    /// <see cref="CancellationToken"/>.</exception>
    public void Cancel(bool throwOnFirstException)
    {
      selfSource.Cancel(throwOnFirstException);
    }

    /// <summary>
    /// A function to schedule a cancel operation on the internal
    /// <see cref="CancellationTokenSource"/> after the specified number of milliseconds.
    /// </summary>
    /// <param name="millisecondsDelay">The time to wait before canceling the internal
    /// <see cref="CancellationTokenSource"/>.</param>
    /// <exception cref="ObjectDisposedException">The <see cref="Exception"/> thrown when the
    /// internal <see cref="CancellationTokenSource"/> has been disposed.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The <see cref="Exception"/> thrown when
    /// <paramref name="millisecondsDelay"/> is less than -1.</exception>
    public void CancelAfter(int millisecondsDelay)
    {
      selfSource.CancelAfter(millisecondsDelay);
    }

    /// <summary>
    /// A function to schedule a cancel operation on the internal
    /// <see cref="CancellationTokenSource"/> after the specified <see cref="TimeSpan"/>.
    /// </summary>
    /// <param name="delay">The <see cref="TimeSpan"/> to wait before canceling the internal
    /// <see cref="CancellationTokenSource"/>.</param>
    /// <exception cref="ObjectDisposedException">The exception thrown when the internal
    /// <see cref="CancellationTokenSource"/> has been disposed.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The exception thrown when
    /// <paramref name="delay"/> is less than -1 or greater than
    /// <see cref="int.MaxValue"/>.</exception>
    public void CancelAfter(TimeSpan delay)
    {
      selfSource.CancelAfter(delay);
    }

    public void Dispose()
    {
      selfSource.Dispose(); // Dispose the inner source.
    }
  }
  /************************************************************************************************/
}