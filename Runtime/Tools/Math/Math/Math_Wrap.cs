

namespace Tenor.Tools.Math
{
  /************************************************************************************************/
  /// <summary>
  /// An enum for dictating how a value should wrap around two extremes.
  /// </summary>
  public enum WrapMode
  {
    /// <summary>No wrapping is done. This is effectively a clamp in most scenarios.</summary>
    None,
    /// <summary>A value out of range is wrapped to the opposite extreme.</summary>
    StrictWrap,
    /// <summary>A value out of range continuously wraps until it fits the range.</summary>
    FullWrap,
  }
  /************************************************************************************************/
  /************************************************************************************************/
  public static partial class Math
  {
    public static int WrapII(int value, int min, int max, WrapMode mode)
    {
      return mode switch
      {
        WrapMode.StrictWrap => value < min ? max : (value > max ? min : value),
        WrapMode.FullWrap => value < min ? max - (min - value) % (max - min) : (value > max ? min + (value - min) % (max - min) : value),
        _ => ClampII(value, min, max),
      };
    }
  }
  /************************************************************************************************/
}
