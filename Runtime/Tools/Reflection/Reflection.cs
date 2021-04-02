using System.Collections;
using System.Reflection;
using System.Text;
using System.Linq;
using Tenor.Tools.Collection;
using System.Collections.Generic;

namespace Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// A series of useful functions for using the C# Reflection System.
  /// </summary>
  public static partial class Reflection
  {
    /// <summary> The default flags that are used for getting Fields and Properties. These are 'Public, NonPublic, Instance, Static, and FlattenHierarchy'. </summary>
    public static readonly BindingFlags DefaultFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy;
    /// <summary> The character typically used to separate pieces of a full member path.</summary>
    private static readonly char PathSeparator = '.';
    /// <summary> The keyword declaring that a path is accessing an <see cref="IList"/>.</summary>
    private static readonly string ArrayKeyword = "Array";
    /// <summary> The keyword before the index of a <see cref="IList"/> element path.</summary>
    private static readonly string DataKeyword = "data[";

    public static string[] BreakPath(string path, char separator = '.')
    {
      return path.Split(separator);
    }

    /// <summary>
    /// A helper function to turn a group of individual paths into one formatted like a Unity Property Path.
    /// </summary>
    /// <param name="paths">The paths to concatenate. These are put together using the '.' character.</param>
    /// <returns>Returns the full string of the paths concatenated.</returns>
    public static string ConcatenatePaths(params string[] paths)
    {
      // Make sure the paths exist.
      if (!paths.IsEmptyOrNull())
      {
        StringBuilder fullPath = new StringBuilder(); // Make a new StringBuilder.

        // Append each path, along with the path separator.
        foreach (string p in paths)
          fullPath.Append(p).Append(PathSeparator);

        fullPath.Remove(fullPath.Length - 1, 1); // Remove the final separator.
        return fullPath.ToString(); // Return the full path.
      }

      return string.Empty; // Return an empty string.
    }

    /// <summary>
    /// A helper function to create a path to an IList index. This is formatted as 'Array.data[<paramref name="index"/>]'.
    /// </summary>
    /// <param name="index">The index to stick into the path.</param>
    /// <returns>Returns the IList path. Just concatenate it to the full path.</returns>
    public static string CreateIListIndexPath(int index)
    {
      // Append the keywords, with the index in the middle.
      return new StringBuilder(ArrayKeyword).Append(PathSeparator).Append(DataKeyword).Append(index).Append(']').ToString();
    }

    /// <summary>
    /// A function to get a value from a member variable. Use this if you do not know if the variable is a Field or a Property.
    /// This version defaults to the DefaultFlags set in the Reflection Kit.
    /// </summary>
    /// <typeparam name="T">The type of the variable being looked for. If you don't know, use 'object'.</typeparam>
    /// <param name="obj">The starting object to get the value off from.</param>
    /// <param name="path">The series of paths to get to the value. Unity concatonates all of these with a '.'.</param>
    /// <returns>Returns the value found at the path. If something goes wrong, it returns T's default value.</returns>
    public static T GetMemberVariableValue<T>(object obj, params string[] path)
    {
      return GetMemberVariableValue<T>(obj, DefaultFlags, path); // Get the value of the FieldInfo.
    }

    /// <summary>
    /// A function to get a value from a member variable. Use this if you do not know if the variable is a Field or a Property.
    /// </summary>
    /// <typeparam name="T">The type of the variable being looked for. If you don't know, use 'object'.</typeparam>
    /// <param name="obj">The starting object to get the value off from.</param>
    /// <param name="flags">The flags used to get the value. Use this to limit or expand what is available to access.</param>
    /// <param name="path">The series of paths to get to the value. Unity concatonates all of these with a '.'.</param>
    /// <returns>Returns the value found at the path. If something goes wrong, it returns T's default value.</returns>
    public static T GetMemberVariableValue<T>(object obj, BindingFlags flags, params string[] path)
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
    /// A function to get a value from a Field after a series of paths. A Field is a variable without a {get;set;}.
    /// This version defaults to the DefaultFlags set in the Reflection Kit.
    /// </summary>
    /// <typeparam name="T">The type of the variable being looked for. If you don't know, use 'object'.</typeparam>
    /// <param name="obj">The starting object to get the value off from.</param>
    /// <param name="path">The series of paths to get to the value. Unity concatonates all of these with a '.'.</param>
    /// <returns>Returns the value found at the path. If something goes wrong, it returns T's default value.</returns>
    public static T GetFieldValue<T>(object obj, params string[] path)
    {
      return GetFieldValue<T>(obj, DefaultFlags, path); // Get the value of the FieldInfo.
    }

    /// <summary>
    /// A function to get a value from a Field after a series of paths. A Field is a variable without a {get;set;}.
    /// </summary>
    /// <typeparam name="T">The type of the variable being looked for. If you don't know, use 'object'.</typeparam>
    /// <param name="obj">The starting object to get the value off from.</param>
    /// <param name="flags">The flags used to get the value. Use this to limit or expand what is available to access.</param>
    /// <param name="path">The series of paths to get to the value. Unity concatonates all of these with a '.'.</param>
    /// <returns>Returns the value found at the path. If something goes wrong, it returns T's default value.</returns>
    public static T GetFieldValue<T>(object obj, BindingFlags flags, params string[] path)
    {
      object current = obj; // The current object. This is a constantly changing reference.

      // Get the FieldInfo at the end of the series of paths. Getting a FieldValue does not require the previous object.
      FieldInfo field = GetFieldInfo(ref current, out object previous, flags, path);

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
    /// A function to set a value from a Field after a series of paths. A Field is a variable without a {get;set;}.
    /// Make sure the the Field is serialized for custom Editors.
    /// This version defaults to the DefaultFlags set in the Reflection Kit.
    /// </summary>
    /// <typeparam name="T">The type of the variable being looked for. If you don't know, use 'object'.</typeparam>
    /// <param name="obj">The starting object to get the value off from.</param>
    /// <param name="value">The value to set into the object.</param>
    /// <param name="path">The series of paths to get to the value. Unity concatonates all of these with a '.'.</param>
    /// <returns>Returns the value found at the path. If something goes wrong, it returns T's default value.</returns>
    public static bool SetFieldValue<T>(object obj, T value, params string[] path)
    {
      return SetFieldValue<T>(obj, value, DefaultFlags, path); // Set the value to the FieldInfo.
    }

    /// <summary>
    /// A function to set a value from a Field after a series of paths. A Field is a variable without a {get;set;}.
    /// Make sure the the Field is serialized for custom Editors.
    /// </summary>
    /// <typeparam name="T">The type of the variable being looked for. If you don't know, use 'object'.</typeparam>
    /// <param name="obj">The starting object to get the value off from.</param>
    /// <param name="value">The value to set into the object.</param>
    /// <param name="flags">The flags used to get the value. Use this to limit or expand what is available to access.</param>
    /// <param name="path">The series of paths to get to the value. Unity concatonates all of these with a '.'.</param>
    /// <returns>Returns if the value was properly set or not.</returns>
    public static bool SetFieldValue<T>(object obj, T value, BindingFlags flags, params string[] path)
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
          // If the prevous object is an IList (IList<T> is an IList), and the last path points to an array element.
          case IList ilist when path.LastElement().Contains(DataKeyword):
            int index = ParseArrayPathIndex(path.LastElement()); // Parse the index out from the last path.

            // Check that the collection is not null, and the requested index is valid.
            if (ilist != null && ilist.IsValidIndexNG(index))
            {
              ilist[index] = value; // Set the value at the requested index.
              return true; // The value was successfully set.
            }

            return false; // The value was not set.
          case IList<T> ilist when path.LastElement().Contains(DataKeyword):
            index = ParseArrayPathIndex(path.LastElement()); // Parse the index out from the last path.

            // Check that the collection is not null, and the requested index is valid.
            if (ilist != null && ilist.IsValidIndex(index))
            {
              ilist[index] = value; // Set the value at the requested index.
              return true; // The value was successfully set.
            }

            return false; // The value was not set.
          default:
            field.SetValue(previous, value);
            return true;// field.GetValue(previous) == (object)value; // Return if the value was correctly set.
        }
      }

      return false; // The value was not set.
    }

    /// <summary>
    /// A function to acquire an object's FieldInfo, given a series of paths.
    /// </summary>
    /// <param name="obj">The starting object to get the FieldInfo off from.</param>
    /// <param name="flags">The BindingFlags used to determine what values are allowed to be accessed.</param>
    /// <param name="path">The array of path pieces to the wanted value. In Unity, this would be the property path, split by the '.' character.</param>
    /// <returns>Returns the FieldInfo at the end of the paths. Returns null in the event of an error.</returns>
    public static FieldInfo GetFieldInfo(object obj, BindingFlags flags, params string[] path)
    {
      object current = obj; // Make a separate reference to the object.
      return GetFieldInfo(ref current, out object previous, flags, path);
    }

    /// <summary>
    /// An internal function to acquire an object's FieldInfo, given a series of paths.
    /// </summary>
    /// <param name="current">The current object being looked into. This will end as the final value in the FieldInfo.</param>
    /// <param name="previous">The previous object looked into.</param>
    /// <param name="flags">The BindingFlags used to determine what values are allowed to be accessed.</param>
    /// <param name="path">The array of path pieces to the wanted value. In Unity, this would be the property path, split by the '.' character.</param>
    /// <returns>Returns the FieldInfo at the end of the paths. Returns null in the event of an error.</returns>
    public static FieldInfo GetFieldInfo(ref object current, out object previous, BindingFlags flags, params string[] path)
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
            // If the current object is an IList
            case IList ilist:
              // Check if the current path matches the ArrayKeyword.
              if (path[i] == ArrayKeyword)
              {
                i++; // The next path is guaranteed to be the element path.
                int index = ParseArrayPathIndex(path[i]); // Parse out the index.
                previous = current; // Set the previous object.
                current = ilist.IsValidIndexNG(index) ? ilist[index] : null; // If the index is valid, return that object. Else, return null;
              }
              break;
            default:
              field = current.GetType().GetField(path[i], flags); // Get the field of the current object's type, using the given flags.
              previous = current; // Update the previous object reference.
              current = field.GetValue(current); // Update the current object reference with the current value.
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
    /// A function to get a value from a Property after a series of paths. A Property is a variable with a {get;set;}.
    /// This version defaults to the DefaultFlags set in the Reflection Kit.
    /// </summary>
    /// <typeparam name="T">The type of the variable being looked for. If you don't know, use 'object'.</typeparam>
    /// <param name="obj">The starting object to get the value off from.</param>
    /// <param name="path">The series of paths to get to the value. Unity concatonates all of these with a '.'.</param>
    /// <returns>Returns the value found at the path. If something goes wrong, it returns T's default value.</returns>
    public static T GetPropertyValue<T>(object obj, params string[] path)
    {
      return GetPropertyValue<T>(obj, DefaultFlags, path); // Get the value of the FieldInfo.
    }

    /// <summary>
    /// A function to get a value from a PropertyField after a series of paths. A Property is a variable with a {get;set;}.
    /// </summary>
    /// <typeparam name="T">The type of the variable being looked for. If you don't know, use 'object'.</typeparam>
    /// <param name="obj">The starting object to get the value off from.</param>
    /// <param name="flags">The flags used to get the value. Use this to limit or expand what is available to access.</param>
    /// <param name="path">The series of paths to get to the value. Unity concatonates all of these with a '.'.</param>
    /// <returns>Returns the value found at the path. If something goes wrong, it returns T's default value.</returns>
    public static T GetPropertyValue<T>(object obj, BindingFlags flags, params string[] path)
    {
      object current = obj; // The current object. This is a constantly changing reference.

      // Get the PropertyInfo at the end of the series of paths. Getting a property value does not require the previous object.
      GetPropertyInfo(ref current, out object previous, flags, path);

      // If the value is not null, and is assignable from T, return the value casted to T.
      if (current != null && current is T value)
        return value;

      return default; // In the case of an error, return the default value.
    }

    /// <summary>
    /// A function to set a value from a Property after a series of paths. A Property is a variable with a {get;set;}.
    /// Make sure the the Property is serialized for custom Editors.
    /// This version defaults to the DefaultFlags set in the Reflection Kit.
    /// </summary>
    /// <typeparam name="T">The type of the variable being looked for. If you don't know, use 'object'.</typeparam>
    /// <param name="obj">The starting object to get the value off from.</param>
    /// <param name="value">The value to set into the object.</param>
    /// <param name="path">The series of paths to get to the value. Unity concatonates all of these with a '.'.</param>
    /// <returns>Returns the value found at the path. If something goes wrong, it returns T's default value.</returns>
    public static bool SetPropertyValue<T>(object obj, T value, params string[] path)
    {
      return SetPropertyValue<T>(obj, value, DefaultFlags, path); // Set the value to the FieldInfo.
    }

    /// <summary>
    /// A function to set a value from a Property after a series of paths. A Property is a variable with a {get;set;}.
    /// Make sure the the Property is serialized for custom Editors.
    /// </summary>
    /// <typeparam name="T">The type of the variable being looked for. If you don't know, use 'object'.</typeparam>
    /// <param name="obj">The starting object to get the value off from.</param>
    /// <param name="value">The value to set into the object.</param>
    /// <param name="flags">The flags used to get the value. Use this to limit or expand what is available to access.</param>
    /// <param name="path">The series of paths to get to the value. Unity concatonates all of these with a '.'.</param>
    /// <returns>Returns if the value was properly set or not.</returns>
    public static bool SetPropertyValue<T>(object obj, T value, BindingFlags flags, params string[] path)
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
            int index = ParseArrayPathIndex(path.LastElement()); // Parse the index out from the last path.

            // Check that the collection is not null, and the requested index is valid.
            if (ilist != null && ilist.IsValidIndexNG(index))
            {
              ilist[index] = value; // Set the value at the requested index.
              return true; // The value was successfully set.
            }

            return false; // The value was not set.
          case IList<T> ilist when path.LastElement().Contains(DataKeyword):
            index = ParseArrayPathIndex(path.LastElement()); // Parse the index out from the last path.

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
              return property.GetValue(previous) == (object)value; // Return if the value was correctly set.
            }
            catch
            {
              return false; // In the case of an error, return false.
            }
        }
      }

      return false; // The value was not set.
    }

    /// <summary>
    /// A function to acquire an object's PropertyInfo, given a series of paths.
    /// </summary>
    /// <param name="obj">The starting object to get the FieldInfo off from.</param>
    /// <param name="flags">The BindingFlags used to determine what values are allowed to be accessed.</param>
    /// <param name="path">The array of path pieces to the wanted value. In Unity, this would be the property path, split by the '.' character.</param>
    /// <returns>Returns the PropertyInfo at the end of the paths. Returns null in the event of an error.</returns>
    public static PropertyInfo GetPropertyInfo(object obj, BindingFlags flags, params string[] path)
    {
      object current = obj; // Make a separate reference to the object.
      return GetPropertyInfo(ref current, out object previous, flags, path);
    }

    /// <summary>
    /// An internal function to acquire an object's PropertyInfo, given a series of paths.
    /// </summary>
    /// <param name="current">The current object being looked into. This will end as the final value in the PropertyInfo.</param>
    /// <param name="previous">The previous object looked into.</param>
    /// <param name="flags">The BindingFlags used to determine what values are allowed to be accessed.</param>
    /// <param name="path">The array of path pieces to the wanted value. In Unity, this would be the property path, split by the '.' character.</param>
    /// <returns>Returns the PropertyInfo at the end of the paths. Returns null in the event of an error.</returns>
    public static PropertyInfo GetPropertyInfo(ref object current, out object previous, BindingFlags flags, params string[] path)
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
            // If the current object is an IList (IList<T> is an IList)
            case IList ilist:
              // Check if the current path matches the ArrayKeyword.
              if (path[i] == ArrayKeyword)
              {
                i++; // The next path is guaranteed to be the element path.
                int index = ParseArrayPathIndex(path[i]); // Parse out the index.
                previous = current; // Set the previous object.
                current = ilist.IsValidIndexNG(index) ? ilist[index] : null; // If the index is valid, return that object. Else, return null;
              }
              break;
            default:
              property = current.GetType().GetProperty(path[i], flags); // Get the field of the current object's type, using the given flags.
              previous = current; // Update the previous object reference.
              current = property.GetValue(current); // Update the current object reference with the current value.
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
    /// An internal function to parse out an array element index. This is used when accessing an array element via reflection.
    /// </summary>
    /// <param name="dataPath">The path of the element index. This is taken apart to get to the actual value.</param>
    /// <returns>Returns the index stored within the data path.</returns>
    private static int ParseArrayPathIndex(string dataPath)
    {
      StringBuilder indexPath = new StringBuilder(dataPath); // Create a StringBuilder for the data path.
      return int.Parse(indexPath.Remove(0, DataKeyword.Length).Remove(indexPath.Length - 1, 1).ToString()); // Parse out the keyword and remaining ']' character.
    }
  }
  /************************************************************************************************/
}