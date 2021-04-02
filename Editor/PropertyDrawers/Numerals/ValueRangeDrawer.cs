/**************************************************************************************************/
/*!
\file   ValueRangeDrawer.cs
\author Craig Williams
\par    Last Updated
        2021-04-01
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for the implementation of all drawers for Tenor's built-in ValueRange types. This file
  should not be included outside of Unity projects.

\par Bug List

\par References
*/
/**************************************************************************************************/

#if UNITY_EDITOR
using System;
using System.Reflection;
using Tenor.Numerals;
using Tenor.Tools;
using UnityEngine;
using UnityEditor;

namespace Tenor.UnityEditor.PropertyDrawers
{
  /// <summary>
  /// The base <see cref="PropertyDrawer"/> for <see cref="IValueRange{TBase}"/>s.
  /// </summary>
  public abstract class ValueRangeDrawer : PropertyDrawer
  {
    /// <summary></summary>
    /// <summary>The base path to <see cref="IValueRange{TBase}.Value"/>.</summary>
    private static readonly string ValuePath = "Value";
    /// <summary>The base path to <see cref="IValueRange{TBase}.Min"/>.</summary>
    private static readonly string MinPath = "Min";
    /// <summary>The base path to <see cref="IValueRange{TBase}.Max"/>.</summary>
    private static readonly string MaxPath = "Max";
    /// <summary>The base path to <see cref="IValueRange{TBase}.WrappingMode"/>.</summary>
    private static readonly string WrapPath = "WrappingMode";

    /// <summary>The <see cref="IValueRange{TBase}"/> this drawer represents.</summary>
    protected object rangeObject;
    /// <summary>The reflected info of <see cref="IValueRange{TBase}.Value"/>.</summary>
    protected PropertyInfo valueInfo;
    /// <summary>The reflected info of <see cref="IValueRange{TBase}.Min"/>.</summary>
    protected PropertyInfo minInfo;
    /// <summary>The reflected info of <see cref="IValueRange{TBase}.Max"/>.</summary>
    protected PropertyInfo maxInfo;
    /// <summary>The reflected info of <see cref="IValueRange{TBase}.WrappingMode"/>.</summary>
    protected PropertyInfo wrapInfo;

    /// <summary>A check for if the property values have already been initialized.</summary>
    private bool isInitialized = false;
    /// <summary>The broken-up property path, ready for <see cref="Reflection"/>.</summary>
    private string[] propertyPath;
    /// <summary>The amount of space to maintain between horizontal fields.</summary>
    private float horizontalSpacing;
    /// <summary>The width of the valued fields.</summary>
    private float valueWidth;
    /// <summary>The width of the <see cref="WrapMode"/> field.</summary>
    private float wrapWidth;

    /// <summary>
    /// An initialization function to set up the property's values.
    /// </summary>
    /// <param name="property">The <see cref="SerializedProperty"/> being drawn.</param>
    protected void InitializeDrawer(SerializedProperty property)
    {
      // Only perform this function if necessary.
      if (!isInitialized)
      {
        // Get the object and path we are currently checking.
        propertyPath = Reflection.BreakPath(property.propertyPath);

        // Obtain the range object, and its properties.
        rangeObject = Reflection.GetFieldValue<object>(property.serializedObject.targetObject, propertyPath);
        valueInfo = Reflection.GetPropertyInfo(rangeObject, Reflection.DefaultFlags, ValuePath);
        minInfo = Reflection.GetPropertyInfo(rangeObject, Reflection.DefaultFlags, MinPath);
        maxInfo = Reflection.GetPropertyInfo(rangeObject, Reflection.DefaultFlags, MaxPath);
        wrapInfo = Reflection.GetPropertyInfo(rangeObject, Reflection.DefaultFlags, WrapPath);

        isInitialized = true;
      }
    }

    /// <summary>
    /// An initialization function for the initial <see cref="Rect"/>. This assumes that the
    /// <see cref="EditorGUI.LabelField(Rect, GUIContent)"/> has already been called.
    /// </summary>
    /// <param name="position">The initial <see cref="Rect"/>.</param>
    /// <returns>Returns the edited <paramref name="position"/>.</returns>
    protected Rect InitializeLabelAndSpacings(Rect position)
    {
      // Calculate the spacings for the top row.
      position.height = EditorGUIUtility.singleLineHeight; // Set the height to one line.
      position.width -= EditorGUIUtility.labelWidth; // Remove the amount of label width.
      position.x += EditorGUIUtility.labelWidth; // Move over by the label width.

      // Set the spacing and width values to use.
      horizontalSpacing = position.width * 0.01f;
      valueWidth = position.width * 0.25f;
      wrapWidth = position.width * 0.48f;

      return position;
    }

    /// <summary>
    /// A function for drawing <see cref="IValueRange{TBase}.Min"/>.
    /// </summary>
    /// <param name="startRect">The initial <see cref="Rect"/> position.</param>
    /// <returns>Returns the finalized <see cref="Rect"/> position.</returns>
    protected Rect DrawMinArea(Rect startRect)
    {
      object min = minInfo.GetValue(rangeObject); // Get the current value.
      startRect.width = valueWidth; // Set to the correct width.

      HandleMinArea(startRect, min); // Handle the mimum field drawing.

      startRect.x += valueWidth + horizontalSpacing; // Update the rect.
      return startRect;
    }

    /// <summary>
    /// A function for drawing <see cref="IValueRange{TBase}.WrappingMode"/>.
    /// </summary>
    /// <param name="startRect">The initial <see cref="Rect"/> position.</param>
    /// <returns>Returns the finalized <see cref="Rect"/> position.</returns>
    protected virtual Rect DrawWrapArea(Rect startRect)
    {
      // Get the current value.
      Tools.Math.WrapMode mode = (Tools.Math.WrapMode)wrapInfo.GetValue(rangeObject);
      startRect.width = wrapWidth; // Set to the correct width.

      // Check for a change on the enum. If changed, set the value to the PropertyInfo.
      EditorGUI.BeginChangeCheck();
      mode = (Tools.Math.WrapMode)EditorGUI.EnumPopup(startRect, mode);
      if (EditorGUI.EndChangeCheck())
        wrapInfo.SetValue(rangeObject, mode);
      
      startRect.x += wrapWidth + horizontalSpacing; // Update the rect.
      return startRect;
    }

    /// <summary>
    /// A function for drawing <see cref="IValueRange{TBase}.Max"/>.
    /// </summary>
    /// <param name="startRect">The initial <see cref="Rect"/> position.</param>
    /// <returns>Returns the finalized <see cref="Rect"/> position.</returns>
    protected Rect DrawMaxArea(Rect startRect)
    {
      object max = maxInfo.GetValue(rangeObject); // Get the current value.
      startRect.width = valueWidth; // Set to the correct width.

      HandleMaxArea(startRect, max); // Handle the maximum field drawing.

      startRect.x += valueWidth + horizontalSpacing; // Update the rect.
      return startRect;
    }

    /// <summary>
    /// A function for drawing <see cref="IValueRange{TBase}.Value"/>.
    /// </summary>
    /// <param name="startRect">The initial <see cref="Rect"/> position.</param>
    /// <returns>Returns the finalized <see cref="Rect"/> position.</returns>
    protected Rect DrawValueArea(Rect startRect)
    {
      object value = valueInfo.GetValue(rangeObject); // Get the current value.

      HandleValueArea(startRect, value); // Handle the value field drawing.

      return startRect;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      
      InitializeDrawer(property);
      EditorGUI.LabelField(position, label);
      Rect topRect = InitializeLabelAndSpacings(position);

      topRect = DrawMinArea(topRect);
      topRect = DrawWrapArea(topRect);
      topRect = DrawMaxArea(topRect);

      // Move to the second row.
      topRect = position;
      topRect.height = EditorGUIUtility.singleLineHeight;
      topRect.width -= EditorGUIUtility.labelWidth;
      topRect.x += EditorGUIUtility.labelWidth;
      topRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

      topRect = DrawValueArea(topRect);

      Reflection.SetFieldValue(property.serializedObject.targetObject, rangeObject, propertyPath);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
      return base.GetPropertyHeight(property, label) * 2.0f + EditorGUIUtility.standardVerticalSpacing; // Two rows.
    }

    /// <summary>
    /// A function for handling the drawing of the <see cref="IValueRange{TBase}.Min"/>.
    /// This must be overloaded to properly cast and set the inner value.
    /// </summary>
    /// <param name="position">The <see cref="Rect"/> of the field.</param>
    /// <param name="min">The <see cref="object"/> from the <see cref="PropertyInfo"/>.</param>
    protected abstract void HandleMinArea(Rect position, object min);

    /// <summary>
    /// A function for handling the drawing of the <see cref="IValueRange{TBase}.Max"/>.
    /// This must be overloaded to properly cast and set the inner value.
    /// </summary>
    /// <param name="position">The <see cref="Rect"/> of the field.</param>
    /// <param name="max">The <see cref="object"/> from the <see cref="PropertyInfo"/>.</param>
    protected abstract void HandleMaxArea(Rect position, object max);

    /// <summary>
    /// A function for handling the drawing of the <see cref="IValueRange{TBase}.Value"/>.
    /// This must be overloaded to properly cast and set the inner value.
    /// </summary>
    /// <param name="position">The <see cref="Rect"/> of the field.</param>
    /// <param name="value">The <see cref="object"/> from the <see cref="PropertyInfo"/>.</param>
    protected abstract void HandleValueArea(Rect position, object value);
  }

  /// <summary>
  /// The <see cref="PropertyDrawer"/> for an <see cref="IntRange"/>.
  /// </summary>
  [CustomPropertyDrawer(typeof(IntRange))]
  public sealed class IntRangeDrawer : ValueRangeDrawer
  {
    protected override void HandleMinArea(Rect position, object min)
    {
      // Check for a change on the min. If changed, set the value to the PropertyInfo.
      EditorGUI.BeginChangeCheck();
      min = EditorGUI.IntField(position, Convert.ToInt32(min));
      if (EditorGUI.EndChangeCheck())
        minInfo.SetValue(rangeObject, min);
    }

    protected override void HandleMaxArea(Rect position, object max)
    {
      // Check for a change on the max. If changed, set the value to the PropertyInfo.
      EditorGUI.BeginChangeCheck();
      max = EditorGUI.IntField(position, Convert.ToInt32(max));
      if (EditorGUI.EndChangeCheck())
        maxInfo.SetValue(rangeObject, max);
    }

    protected override void HandleValueArea(Rect position, object value)
    {
      // Get the minimum and maximum values for the slider.
      int min = (int)minInfo.GetValue(rangeObject);
      int max = (int)maxInfo.GetValue(rangeObject);

      // Check for a change on the value. If changed, set the value to the PropertyInfo.
      EditorGUI.BeginChangeCheck();
      value = EditorGUI.IntSlider(position, Convert.ToInt32(value), min, max);
      if (EditorGUI.EndChangeCheck())
        valueInfo.SetValue(rangeObject, value);
    }
  }
}
#endif