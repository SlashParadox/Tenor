/**************************************************************************************************/
/*!
\file   Reflection.cs
\author Craig Williams
\par    Last Updated
        2021-05-24
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file for helper functions for the C# Reflection system. You can use these to get various
  hidden values at runtime, or in an editor.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CodeParadox.Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// A series of useful extension and helper functions for dealing with the C# Reflection system.
  /// </summary>
  public static partial class Reflection
  {
    /// <summary> The character typically used to separate pieces of a full member path.</summary>
    private static readonly char PathSeparator = '.';
    /// <summary> The keyword declaring that a path is accessing an <see cref="IList"/>.</summary>
    private static readonly string ArrayKeyword = "Array";
    /// <summary> The keyword before the index of a <see cref="IList"/> element path.</summary>
    private static readonly string DataKeyword = "data[";
    /// <summary> The default <see cref="BindingFlags"/> that are used for getting members.
    /// These are 'Public, NonPublic, Instance, Static, and FlattenHierarchy'. </summary>
    public static readonly BindingFlags DefaultFlags = BindingFlags.Public | BindingFlags.NonPublic
                           | BindingFlags.Instance | BindingFlags.Static |
                           BindingFlags.FlattenHierarchy;

    /// <summary>
    /// A function for concatenating a group of paths together into one <see cref="string"/>, with a
    /// <see cref="PathSeparator"/> in between all of them. Useful for certain engines.
    /// </summary>
    /// <param name="paths">The separate paths to concatenate.</param>
    /// <returns>Returns the full concatenated <see cref="string"/>.</returns>
    public static string ConcatenatePaths(params string[] paths)
    {
      return ConcatenatePaths(PathSeparator, paths);
    }

    /// <summary>
    /// A function for concatenating a group of paths together into one <see cref="string"/>, with a
    /// separator in between all of them. Useful for certain engines.
    /// </summary>
    /// <param name="separator">The character to place between each path.</param>
    /// <param name="paths">The separate paths to concatenate.</param>
    /// <returns>Returns the full concatenated <see cref="string"/>.</returns>
    public static string ConcatenatePaths(char separator, params string[] paths)
    {
      // Make sure there are paths to concatenate.
      if (paths.IsNotEmptyOrNull())
      {
        // Create a string builder, and get the number of pacts.
        StringBuilder fullPath = new StringBuilder();
        int count = paths.Length - 1;

        // Add all paths, except the last one, with a separator.
        for (int i = 0; i < count; i++)
          fullPath.Append(paths[i]).Append(separator);

        // Add the final path without a separator, and return the string.
        fullPath.Append(paths[count]);
        return fullPath.ToString();
      }

      return string.Empty; // Return an empty string if there are no paths.
    }

    /// <summary>
    /// A helper function to create a path to an <see cref="IList"/> index. This is formatted as
    /// 'Array.data[<paramref name="index"/>]'.
    /// </summary>
    /// <param name="index">The index to stick into the path.</param>
    /// <returns>Returns the <see cref="IList"/> path.</returns>
    public static string CreateIListIndexPath(int index)
    {
      // Append the keywords, with the index in the middle.
      StringBuilder sb = new StringBuilder(ArrayKeyword).Append(PathSeparator).Append(DataKeyword);
      sb.Append(index).Append(']');
      return sb.ToString();
    }

    /// <summary>
    /// A helper function to parse out an array element index. This is used when accessing an
    /// array element via reflection.
    /// </summary>
    /// <param name="dataPath">The path of the element index. This is formatted as
    /// 'Array.data[INDEX]'.</param>
    /// <returns>Returns the index stored within the data path.</returns>
    public static int ParseArrayPathIndex(string dataPath)
    {
      StringBuilder indexPath = new StringBuilder(dataPath); // Create a StringBuilder for the data path.

      // Parse out the keyword and remaining ']' character.
      indexPath.Remove(0, DataKeyword.Length).Remove(indexPath.Length - 1, 1);
      return int.Parse(indexPath.ToString());
    }

    /// <summary>
    /// A function to get an object's <see cref="FieldInfo"/>, given a <paramref name="path"/>.
    /// </summary>
    /// <param name="obj">The starting object to get the <see cref="FieldInfo"/> from.</param>
    /// <param name="flags">The <see cref="BindingFlags"/> to determine what is accessible.</param>
    /// <param name="path">The array of path pieces to the wanted value.</param>
    /// <returns>Returns the <see cref="FieldInfo"/> at the end of the paths.
    /// Returns null in the event of an error.</returns>
    public static FieldInfo GetFieldInfo(object obj, BindingFlags flags, params string[] path)
    {
      object current = obj; // Make a separate reference to the object.
      return GetFieldInfo(ref current, out object _, flags, path);
    }

    /// <summary>
    /// A function to get an object's FieldInfo, given a <paramref name="path"/>.
    /// </summary>
    /// <param name="current">The current object being looked into. This will end as the final
    /// value in the <see cref="FieldInfo"/>.</param>
    /// <param name="previous">The previous object looked into.</param>
    /// <param name="flags">The <see cref="BindingFlags"/> to determine what is accessible.</param>
    /// <param name="path">The array of path pieces to the wanted value.</param>
    /// <returns>Returns the <see cref="FieldInfo"/> at the end of the paths.
    /// Returns null in the event of an error.</returns>
    public static FieldInfo GetFieldInfo(ref object current, out object previous,
                                         BindingFlags flags, params string[] path)
    {
      FieldInfo field = null; // The FieldInfo of the field.
      previous = current; // Set the previous object to null to start.
      try
      {
        // Go through all the individual paths.
        for (int i = 0; i < path.Length; i++)
        {
          // Switch on current's type. This also allows for extra types later if required.
          switch (current)
          {
            // If the current object is an IList.
            case IList ilist:
              // Check if the current path matches the ArrayKeyword.
              if (path[i] == ArrayKeyword)
              {
                i++; // The next path is guaranteed to be the element path.
                int index = ParseArrayPathIndex(path[i]); // Parse out the index.
                previous = current; // Set the previous object.

                // If the index is valid, return that object. Else, return null.
                current = ilist.IsValidIndexNG(index) ? ilist[index] : null;
              }
              break;
            default:
              // Get the field of the current object's type, using the given flags.
              field = current.GetType().GetField(path[i], flags); 
              previous = current; // Update the previous object reference.
              current = field.GetValue(current); // Update the current object reference.
              break;
          }
        }
      }
      catch
      {
        // In the event of an error, return null for everything.
        current = null;
        previous = null;
        return null;
      }

      return field; // Return the found FieldInfo.
    }

    /// <summary>
    /// A function to get an object's <see cref="PropertyInfo"/>, given a <paramref name="path"/>.
    /// </summary>
    /// <param name="obj">The starting object to get the <see cref="PropertyInfo"/> from.</param>
    /// <param name="flags">The <see cref="BindingFlags"/> to determine what is accessible.</param>
    /// <param name="path">The array of path pieces to the wanted value.</param>
    /// <returns>Returns the <see cref="PropertyInfo"/> at the end of the paths.
    /// Returns null in the event of an error.</returns>
    public static PropertyInfo GetPropertyInfo(object obj, BindingFlags flags, params string[] path)
    {
      object current = obj; // Make a separate reference to the object.
      return GetPropertyInfo(ref current, out object previous, flags, path);
    }

    /// <summary>
    /// A function to get an object's <see cref="PropertyInfo"/>, given a <paramref name="path"/>.
    /// </summary>
    /// <param name="current">The current object being looked into. This will end as the final
    /// value in the <see cref="PropertyInfo"/>.</param>
    /// <param name="previous">The previous object looked into.</param>
    /// <param name="flags">The <see cref="BindingFlags"/> to determine what is accessible.</param>
    /// <param name="path">The array of path pieces to the wanted value.</param>
    /// <returns>Returns the <see cref="PropertyInfo"/> at the end of the paths.
    /// Returns null in the event of an error.</returns>
    public static PropertyInfo GetPropertyInfo(ref object current, out object previous,
                                               BindingFlags flags, params string[] path)
    {
      PropertyInfo property = null; // The FieldInfo of the field.
      previous = current; // Set the previous object to null to start.
      try
      {
        // Go through all the individual paths.
        for (int i = 0; i < path.Length; i++)
        {
          // Switch on current's type. This also allows for extra types later if required.
          switch (current)
          {
            // If the current object is an IList.
            case IList ilist:
              // Check if the current path matches the ArrayKeyword.
              if (path[i] == ArrayKeyword)
              {
                i++; // The next path is guaranteed to be the element path.
                int index = ParseArrayPathIndex(path[i]); // Parse out the index.
                previous = current; // Set the previous object.

                // If the index is valid, return that object. Else, return null.
                current = ilist.IsValidIndexNG(index) ? ilist[index] : null;
              }
              break;
            default:
              // Get the field of the current object's type, using the given flags.
              property = current.GetType().GetProperty(path[i], flags);
              previous = current; // Update the previous object reference.
              current = property.GetValue(current); // Update the current object reference.
              break;
          }
        }
      }
      catch
      {
        // In the event of an error, return null for everything.
        current = null;
        previous = null;
        return null;
      }

      return property; // Return the found FieldInfo.
    }

    /// <summary>
    /// A function to get a value from a member variable. Use this if you do not know if the
    /// variable is a Field or a Property. The <see cref="DefaultFlags"/> are used.
    /// </summary>
    /// <typeparam name="T">The type of the variable. If you don't know, use 'object'.</typeparam>
    /// <param name="obj">The starting object to get the value off from.</param>
    /// <param name="path">The series of paths to get to the value.</param>
    /// <returns>Returns the value found at the path. If something goes wrong,
    /// it returns <typeparamref name="T"/>'s default value.</returns>
    public static T GetMemberValue<T>(object obj, params string[] path)
    {
      return GetMemberValue<T>(obj, DefaultFlags, path); // Get the value of the FieldInfo.
    }

    /// <summary>
    /// A function to get a value from a member variable. Use this if you do not know if the
    /// variable is a Field or a Property.
    /// </summary>
    /// <typeparam name="T">The type of the variable. If you don't know, use 'object'.</typeparam>
    /// <param name="obj">The starting object to get the value off from.</param>
    /// <param name="flags">The flags used to get the value. Use this to limit or expand what is
    /// available to access.</param>
    /// <param name="path">The series of paths to get to the value.</param>
    /// <returns>Returns the value found at the path. If something goes wrong,
    /// it returns <typeparamref name="T"/>'s default value.</returns>
    public static T GetMemberValue<T>(object obj, BindingFlags flags, params string[] path)
    {
      object current = obj;
      FieldInfo field = GetFieldInfo(ref current, out _, flags, path);
      if (field != null)
      {
        // If the value is not null, and is assignable from T, return the value casted to T.
        if (current != null && current is T value)
          return value;
      }
      else
      {
        current = obj;
        PropertyInfo property = GetPropertyInfo(ref current, out _, flags, path);

        if (property != null)
        {
          if (current != null && current is T value)
            return value;
        }
      }

      return default;
    }

    /// <summary>
    /// A function to get a value from a Field after a series of paths.
    /// A Field is a variable without a {get;set;}. The <see cref="DefaultFlags"/> are used.
    /// </summary>
    /// <typeparam name="T">The type of the variable. If you don't know, use 'object'.</typeparam>
    /// <param name="obj">The starting object to get the value off from.</param>
    /// <param name="path">The series of paths to get to the value.</param>
    /// <returns>Returns the value found at the path. If something goes wrong,
    /// it returns <typeparamref name="T"/>'s default value.</returns>
    public static T GetFieldValue<T>(object obj, params string[] path)
    {
      return GetFieldValue<T>(obj, DefaultFlags, path); // Get the value of the Field.
    }

    /// <summary>
    /// A function to get a value from a Field after a series of paths.
    /// A Field is a variable without a {get;set;}.
    /// </summary>
    /// <typeparam name="T">The type of the variable. If you don't know, use 'object'.</typeparam>
    /// <param name="obj">The starting object to get the value off from.</param>
    /// <param name="flags">The flags used to get the value. Use this to limit or expand what is
    /// available to access.</param>
    /// <param name="path">The series of paths to get to the value.</param>
    /// <returns>Returns the value found at the path. If something goes wrong,
    /// it returns <typeparamref name="T"/>'s default value.</returns>
    public static T GetFieldValue<T>(object obj, BindingFlags flags, params string[] path)
    {
      object current = obj; // The current object. This is a constantly changing reference.

      // Get the FieldInfo at the end of the series of paths.
      FieldInfo field = GetFieldInfo(ref current, out object _, flags, path);

      // If the field is not null, attempt to return its value.
      if (field != null)
      {
        // If the value is not null, and is assignable from T, return the value casted to T.
        if (current != null && current is T value)
          return value;
      }

      return default; // In the case of an error, return the default value.
    }

    /// <summary>
    /// A function to set a value from a Field after a series of paths.
    /// A Field is a variable without a {get;set;}. The <see cref="DefaultFlags"/> are used.
    /// </summary>
    /// <typeparam name="T">The type of the variable. If you don't know, use 'object'.</typeparam>
    /// <param name="obj">The starting object to get the value off from.</param>
    /// <param name="value">The value to set into the object.</param>
    /// <param name="path">The series of paths to get to the value.</param>
    /// <returns>Returns if the value was properly set or not.</returns>
    public static bool SetFieldValue<T>(object obj, T value, params string[] path)
    {
      return SetFieldValue<T>(obj, value, DefaultFlags, path); // Set the value to the Field.
    }

    /// <summary>
    /// A function to set a value from a Field after a series of paths.
    /// A Field is a variable without a {get;set;}.
    /// </summary>
    /// <typeparam name="T">The type of the variable. If you don't know, use 'object'.</typeparam>
    /// <param name="obj">The starting object to get the value off from.</param>
    /// <param name="value">The value to set into the object.</param>
    /// <param name="flags">The flags used to get the value. Use this to limit or expand what is
    /// available to access.</param>
    /// <param name="path">The series of paths to get to the value.</param>
    /// <returns>Returns if the value was properly set or not.</returns>
    public static bool SetFieldValue<T>(object obj, T value, BindingFlags flags,
                                        params string[] path)
    {
      object current = obj; // The current object. This is a constantly changing reference.

      // Get the FieldInfo at the end of the series of paths. We will need the previous object.
      FieldInfo field = GetFieldInfo(ref current, out object previous, flags, path);

      // Make sure the field is not null.
      if (field != null)
      {
        // Switch on previous' type. This also allows for extra types later if required.
        switch (previous)
        {
          // If the prevous object is an IList, and the last path points to an array element.
          case IList ilist when path.LastElement().Contains(DataKeyword):
            int index = ParseArrayPathIndex(path.LastElement()); // Parse the index out..

            // Check that the collection is not null, and the requested index is valid.
            if (ilist != null && ilist.IsValidIndexNG(index))
            {
              ilist[index] = value; // Set the value at the requested index.
              return true; // The value was successfully set.
            }

            return false; // The value was not set.
          case IList<T> ilist when path.LastElement().Contains(DataKeyword):
            index = ParseArrayPathIndex(path.LastElement()); // Parse the index out.

            // Check that the collection is not null, and the requested index is valid.
            if (ilist != null && ilist.IsValidIndex(index))
            {
              ilist[index] = value; // Set the value at the requested index.
              return true; // The value was successfully set.
            }

            return false; // The value was not set.
          default:
            field.SetValue(previous, value);
            return true; // Return if the value was correctly set.
        }
      }

      return false; // The value was not set.
    }

    /// <summary>
    /// A function to get a value from a Property after a series of paths.
    /// A Property is a variable with a {get;set;}. The <see cref="DefaultFlags"/> are used.
    /// </summary>
    /// <typeparam name="T">The type of the variable. If you don't know, use 'object'.</typeparam>
    /// <param name="obj">The starting object to get the value off from.</param>
    /// <param name="path">The series of paths to get to the value.</param>
    /// <returns>Returns the value found at the path. If something goes wrong,
    /// it returns <typeparamref name="T"/>'s default value.</returns>
    public static T GetPropertyValue<T>(object obj, params string[] path)
    {
      return GetPropertyValue<T>(obj, DefaultFlags, path); // Get the value of the Property.
    }

    /// <summary>
    /// A function to get a value from a PropertyField after a series of paths.
    /// A Property is a variable with a {get;set;}.
    /// </summary>
    /// <typeparam name="T">The type of the variable. If you don't know, use 'object'.</typeparam>
    /// <param name="obj">The starting object to get the value off from.</param>
    /// <param name="flags">The flags used to get the value. Use this to limit or expand what is
    /// available to access.</param>
    /// <param name="path">The series of paths to get to the value.</param>
    /// <returns>Returns the value found at the path. If something goes wrong,
    /// it returns <typeparamref name="T"/>'s default value.</returns>
    public static T GetPropertyValue<T>(object obj, BindingFlags flags, params string[] path)
    {
      object current = obj; // The current object. This is a constantly changing reference.

      // Get the PropertyInfo at the end of the series of paths.
      PropertyInfo property = GetPropertyInfo(ref current, out object _, flags, path);

      // If the value is not null, and is assignable from T, return the value casted to T.
      if (property != null && current != null && current is T value)
        return value;

      return default; // In the case of an error, return the default value.
    }

    /// <summary>
    /// A function to set a value from a Property after a series of paths.
    /// A Property is a variable with a {get;set;}. The <see cref="DefaultFlags"/> are used.
    /// </summary>
    /// <typeparam name="T">The type of the variable. If you don't know, use 'object'.</typeparam>
    /// <param name="obj">The starting object to get the value off from.</param>
    /// <param name="value">The value to set into the object.</param>
    /// <param name="path">The series of paths to get to the value.</param>
    /// <returns>Returns if the value was properly set or not.</returns>
    public static bool SetPropertyValue<T>(object obj, T value, params string[] path)
    {
      return SetPropertyValue(obj, value, DefaultFlags, path); // Set the value to the Property.
    }

    /// <summary>
    /// A function to set a value from a Property after a series of paths.
    /// A Property is a variable with a {get;set;}.
    /// </summary>
    /// <typeparam name="T">The type of the variable. If you don't know, use 'object'.</typeparam>
    /// <param name="obj">The starting object to get the value off from.</param>
    /// <param name="value">The value to set into the object.</param>
    /// <param name="flags">The flags used to get the value. Use this to limit or expand what is
    /// available to access.</param>
    /// <param name="path">The series of paths to get to the value.</param>
    /// <returns>Returns if the value was properly set or not.</returns>
    public static bool SetPropertyValue<T>(object obj, T value, BindingFlags flags,
                                           params string[] path)
    {
      object current = obj; // The current object. This is a constantly changing reference.

      // Get the FieldInfo at the end of the series of paths. We will need the previous object.
      PropertyInfo property = GetPropertyInfo(ref current, out object previous, flags, path);

      // Make sure the field is not null.
      if (property != null)
      {
        // Switch on previous' type. This also allows for extra types later if required.
        switch (previous)
        {
          // If the prevous object is an IList, and the last path points to an array element.
          case IList ilist when path.LastElement().Contains(DataKeyword):
            int index = ParseArrayPathIndex(path.LastElement()); // Parse the index out.

            // Check that the collection is not null, and the requested index is valid.
            if (ilist != null && ilist.IsValidIndexNG(index))
            {
              ilist[index] = value; // Set the value at the requested index.
              return true; // The value was successfully set.
            }

            return false; // The value was not set.
          case IList<T> ilist when path.LastElement().Contains(DataKeyword):
            index = ParseArrayPathIndex(path.LastElement()); // Parse the index out.

            // Check that the collection is not null, and the requested index is valid.
            if (ilist != null && ilist.IsValidIndex(index))
            {
              ilist[index] = value; // Set the value at the requested index.
              return true; // The value was successfully set.
            }

            return false; // The value was not set.
          default:
            try
            {
              property.SetValue(previous, value);
              return property.GetValue(previous) == (object)value; // Return if the value was set.
            }
            catch
            {
              return false; // In the case of an error, return false.
            }
        }
      }

      return false; // The value was not set.
    }
  }
  /************************************************************************************************/
}