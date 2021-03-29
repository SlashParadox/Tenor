using System.Security.Cryptography;

namespace Tenor.Tools.Math
{
  /// <summary>
  /// An enum for selecting a global random generator. This is primarily used in the
  /// <see cref="Randomization"/> tool.
  /// </summary>
  public enum StandardRandomGenerators
  {
    /// <summary>The standard <see cref="System.Random"/> class.</summary>
    NETStandard,
    /// <summary>The <see cref="Tenor.Tools.Math.RejectionRandom"/> class, based off the standard
    /// <see cref="System.Random"/> class.</summary>
    RejectionRandom,
    /// <summary>The standard <see cref="RNGCryptoServiceProvider"/> class.</summary>
    CryptoServiceProvider,
  }
}