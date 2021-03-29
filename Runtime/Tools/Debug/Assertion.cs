using System.Text;
using Tenor;
using Tenor.Tools.Collection;

namespace Tenor.Tools.Debug
{
  public class AssertionException : System.Exception
  {
    public AssertionException(string expectationMessage) : base(expectationMessage) { }
  }

  public static partial class Assertion
  {
    private static string BuildFailMessage(string message, params object[] args)
    {
      if (message == null)
        message = string.Empty;
      else if (!args.IsEmptyOrNull())
        message = string.Format(message, args);

      return message;
    }

    /// <summary>
    /// An assertion function to check if any error is thrown by the passed-in function.
    /// </summary>
    /// <param name="call">The delegate to invoke, testing if it throws any error.</param>
    /// <returns>Returns the thrown exception, if one was successfully thrown.</returns>
    public static System.Exception ThrowsAny(GenericDelegate call)
    {
      return ThrowsAny(call, string.Empty, null);
    }

    /// <summary>
    /// An assertion function to check if any error is thrown by the passed-in function.
    /// </summary>
    /// <param name="call">The delegate to invoke, testing if it throws any error.</param>
    /// <param name="message">The message to print out if the assertion fails.</param>
    /// <param name="args">Arguments to format the <paramref name="message"/> with.</param>
    /// <returns>Returns the thrown exception, if one was successfully thrown.</returns>
    public static System.Exception ThrowsAny(GenericDelegate call, string message, params object[] args)
    {
      try
      {
        call.Invoke(); // Attempt to throw an exception. If the call works, then we have to fail this assertion.
      }
      catch (System.Exception e)
      {
        return e; // Return any exception properly thrown.
      }

      StringBuilder failMessage = new StringBuilder(BuildFailMessage(message, args));
      failMessage.Append("\n  ").Append("Expected: Any Exception To Be Thrown");
      failMessage.Append("\n  ").Append("But Was: No Exception Thrown");
      throw new System.Exception(failMessage.ToString());
    }
  }
}
