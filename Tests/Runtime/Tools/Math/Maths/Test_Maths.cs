/**************************************************************************************************/
/*!
\file   Test_Maths.cs
\author Craig Williams
\par    Last Updated
        2021-05-24
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A unit test file for the Math tools.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System;

namespace SlashParadox.Tenor.Tests.Runtime.EditMode
{
  /************************************************************************************************/
  /// <summary>
  /// A test class for <see cref="Tenor.Tools.Maths"/>.
  /// </summary>
  public abstract class Test_Maths
  {
    /**********************************************************************************************/
    /// <summary>
    /// A helper class for testing the <see cref="IComparable"/> variants of various function types.
    /// </summary>
    protected class ComparableTest : IComparable<ComparableTest>, IComparable
    {
      /// <summary>The comparable value.</summary>
      public int Value { get; private set; }

      /// <summary>
      /// The constructor for a <see cref="ComparableTest"/>.
      /// </summary>
      /// <param name="value">See: <see cref="Value"/></param>
      public ComparableTest(int value)
      {
        this.Value = value;
      }

      public int CompareTo(ComparableTest other)
      {
        return Value.CompareTo(other.Value);
      }

      public int CompareTo(object obj)
      {
        if (obj is ComparableTest)
          return Value.CompareTo((obj as ComparableTest).Value);

        return -1;
      }
    }
    /**********************************************************************************************/
  }
  /************************************************************************************************/
}