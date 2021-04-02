/**************************************************************************************************/
/*!
\file   ValueRange.cs
\author Craig Williams
\par    Last Updated
        2021-04-01
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for the implementation of an interface for values that are set between two extremes.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System;
using Tenor.Tools.Math;

namespace Tenor.Numerals
{
  /************************************************************************************************/
  /// <summary>
  /// The base class for ranged value types. <see cref="Tenor"/> uses this as a way of enforcing
  /// how these should be implemented. Ranged values have a <see cref="Min"/> and
  /// <see cref="Max"/>, with a <see cref="Value"/> wrapping between the two based on it's
  /// given <see cref="WrappingMode"/>. <see cref="Min"/> and <see cref="Max"/> are inclusive.
  /// </summary>
  /// <typeparam name="TBase">The base numeric type being used.</typeparam>
  public interface IValueRange<TBase> where TBase : IComparable<TBase>, IEquatable<TBase>
  {
    /// <summary>The real numeric value.</summary>
    public TBase Value { get; set; }
    /// <summary>The inclusive minimum that <see cref="Value"/> can be.</summary>
    public TBase Min { get; set; }
    /// <summary>The inclusive maximum that <see cref="Value"/> can be.</summary>
    public TBase Max { get; set; }
    /// <summary>The mode for wrapping <see cref="Value"/> between the extremes.</summary>
    public WrapMode WrappingMode { get; set; }

    /// <summary>
    /// A helper function for setting the range's current <see cref="Value"/>.
    /// </summary>
    /// <param name="newValue">The new value of the range.</param>
    /// <remarks>When implementing, this should be called in <see cref="Value"/>'s setter. It should
    /// set <see cref="Value"/> to a wrapped value. <see cref="Tools.Math.Math.WrapII"/> works best
    /// for this function.</remarks>
    public void SetValue(TBase newValue);

    /// <summary>
    /// A helper function for setting the range's current <see cref="Min"/>.
    /// </summary>
    /// <param name="newMin">The new minimum of the range.</param>
    /// <remarks>When implementing, this should be called in <see cref="Min"/>'s setter. It should
    /// set the min to the minimum of <see cref="Max"/> and <paramref name="newMin"/>, before
    /// calling <see cref="SetValue(TBase)"/>, passing in the current <see cref="Value"/>.</remarks>
    public void SetMin(TBase newMin);

    /// <summary>
    /// A helper function for setting the range's current <see cref="Max"/>.
    /// </summary>
    /// <param name="newMax">The new maximum of the range.</param>
    /// <remarks>When implementing, this should be called in <see cref="Max"/>'s setter. It should
    /// set the min to the maximum of <see cref="Min"/> and <paramref name="newMax"/>, before
    /// calling <see cref="SetValue(TBase)"/>, passing in the current <see cref="Value"/>.</remarks>
    public void SetMax(TBase newMax);

    /// <summary>
    /// A helper function for setting the range's current <see cref="WrappingMode"/>.
    /// </summary>
    /// <param name="newMode">The new <see cref="WrapMode"/> of the range.</param>
    /// <remarks>When implementing, this should be called in <see cref="WrappingMode"/>'s setter.
    /// It should set the new mode, before calling <see cref="SetValue(TBase)"/>, passing in the
    /// current <see cref="Value"/>.</remarks>
    public void SetWrapMode(WrapMode newMode);
  }
  /************************************************************************************************/
}
