/**************************************************************************************************/
/*!
\file   IntRange.cs
\author Craig Williams
\par    Last Updated
        2021-04-01
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for the implementation of an IntRange, which keeps a value between or wrapped between two
  extremes.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System.Text;
using Tenor.Tools.Math;

namespace Tenor.Numerals
{
  /************************************************************************************************/
  /// <summary>
  /// A numeric struct for holding an <see cref="int"/> between a minimum and maximum.
  /// </summary>
  [System.Serializable]
  public struct IntRange : IValueRange<int>
  {
    public int Value { get { return value; } set { SetValue(value); } }
    public int Min { get { return min; } set { SetMin(value); } }
    public int Max { get { return max; } set { SetMax(value); } }
    public WrapMode WrappingMode { get { return wrappingMode; } set { SetWrapMode(value); } }

#if UNITY_EDITOR
    /// <summary>The real value of <see cref="Value"/>.</summary>
    [UnityEngine.SerializeField] private int value;
    /// <summary>The real value of <see cref="Min"/>.</summary>
    [UnityEngine.SerializeField] private int min;
    /// <summary>The real value of <see cref="Max"/>.</summary>
    [UnityEngine.SerializeField] private int max;
    /// <summary>The real value of <see cref="WrappingMode"/>.</summary>
    [UnityEngine.SerializeField] private WrapMode wrappingMode;
#else
    /// <summary>The real value of <see cref="Value"/>.</summary>
    private int value;
    /// <summary>The real value of <see cref="Min"/>.</summary>
    private int min;
    /// <summary>The real value of <see cref="Max"/>.</summary>
    private int max;
    /// <summary>The real value of <see cref="WrappingMode"/>.</summary>
    private WrapMode wrappingMode;
#endif

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder(value.ToString());
      return sb.Append("[").Append(min.ToString()).Append(", ").Append(max.ToString()).Append("]").ToString();
    }

    public void SetValue(int newValue)
    {
      value = Math.WrapII(newValue, min, max, wrappingMode);
    }

    public void SetMin(int newMin)
    {
      min = System.Math.Min(newMin, max);
      SetValue(value);
    }

    public void SetMax(int newMax)
    {
      max = System.Math.Max(newMax, min);
      SetValue(value);
    }

    public void SetWrapMode(WrapMode newMode)
    {
      wrappingMode = newMode;
      SetValue(value);
    }
  }
  /************************************************************************************************/
}