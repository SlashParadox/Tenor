/**************************************************************************************************/
/*!
\file   PathSanitizer.cs
\author Craig Williams
\par    Last Updated
        2021-06-18
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file containing a class used for sanitizing filepaths.

\par Bug List

\par References
*/
/**************************************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeParadox.Tenor.Tools
{
  /************************************************************************************************/
  /// <summary>
  /// A helpful tool class for sanitizing a filepath. It works perfectly fine with directories and
  /// filenames as well. Use this in conjunction with <see cref="FileIO"/>'s functions.
  /// </summary>
  /// <remarks>Many settings can be changed for a <see cref="PathSanitizer"/>. Because of this,
  /// using irregular settings may make your filepath unusable on most operating systems.
  /// <see cref="FileIO"/> has default <see cref="PathSanitizer"/>s that are guaranteed to work on
  /// most operating systems, which can be copied as a baseline. Use <see cref="FileIO"/>'s
  /// <see cref="FileIO.IsValidFilename(string)"/> functions as well for validation.Path lengths
  /// are not sanitized.</remarks>
  public sealed partial class PathSanitizer
  {
    /**********************************************************************************************/
    /// <summary>
    /// An <see langword="enum"/> for determining how to replace invalid parts of the filepath.
    /// </summary>
    public enum ReplacementMode
    {
      /// <summary>Only use <see cref="quickReplacements"/>.</summary>
      QuickOnly,
      /// <summary>Only use <see cref="exactReplacements"/>.</summary>
      ExactOnly,
      /// <summary>Replace quickly, then exactly.</summary>
      QuickThenExact,
      /// <summary>Replace exactly, then quickly.</summary>
      ExactThenQuick,
    }
    /**********************************************************************************************/
    /**********************************************************************************************/
    /// <summary>
    /// An <see langword="enum"/> for determining how to handle the path having a root.
    /// </summary>
    public enum RootMode
    {
      /// <summary>Allow both lettered roots and <see cref="fixedSeparator"/> roots.</summary>
      AllowAllRoots,
      /// <summary>Allow only <see cref="fixedSeparator"/> roots, removing lettered roots.</summary>
      SeparatorOnly,
      /// <summary>Remove all roots altogether.</summary>
      RemoveAllRoots,
    }
    /**********************************************************************************************/

    /// <summary>The symbol used by a <see cref="Regex"/> as an OR gate.</summary>
    private const string RegexPipe = "|";
    /// <summary>An ending <see cref="char"/> that is usually trimmed by file systems.</summary>
    private const char TrimmableChar = '.';

    /// <summary>A <see cref="Regex"/> check for validating lettered directory roots.</summary>
    private static readonly string LetteredRootRegex = @"^[a-z|A-Z]:";

    /// <summary>The separator to replace all <see cref="possibleSeparators"/> with.</summary>
    public string FixedSeparator { get { return fixedSeparator; } set { SetFixedSeparator(value); } }

    /// <summary>A <see cref="List{T}"/> of directory separators, which are universalized.</summary>
    public readonly List<string> possibleSeparators = new List<string>();
    /// <summary>A <see cref="Dictionary{TKey, TValue}"/> of replacements to make when an invalid
    /// section of the path is found. Using exact replacements is a slow process.</summary>
    public readonly Dictionary<string, string> exactReplacements = new Dictionary<string, string>();
    /// <summary>The root handling mode. <see cref="forceRootSeparator"/> overides this.</summary>
    public RootMode rootMode = RootMode.AllowAllRoots;
    /// <summary>The <see cref="ReplacementMode"/> for replacing invalid path sections.</summary>
    public ReplacementMode replacementMode = ReplacementMode.QuickOnly;
    /// <summary>A toggle for removing redundant separators, which separate empty space.</summary>
    public bool removeRedundantSeparators = true;
    /// <summary>A toggle to add a <see cref="fixedSeparator"/>, if a root doesn't exist.</summary>
    public bool forceRootSeparator = false;
    /// <summary>A toggle for fully qualifying the path upon sanitization, using the current
    /// directory as the qualification.</summary>
    public bool fullyQualify = false;
    /// <summary>A toggle for automatically fixing the built <see cref="Regex"/> from the
    /// <see cref="quickReplacements"/> upon an addition or removal.</summary>
    public bool autoRebuildQuickReplacements = false;

    /// <summary>A list of invalid strings that can be quickly replaced with
    /// <see cref="string.Empty"/>. These are built into a <see cref="Regex"/>, and as such
    /// can be formatted for a Regex group.</summary>
    private readonly List<string> quickReplacements = new List<string>();
    /// <summary>The <see cref="Regex"/> built from the <see cref="quickReplacements"/>.</summary>
    private string quickReplacementRegex = string.Empty;
    /// <summary>See: <see cref="FixedSeparator"/></summary>
    private string fixedSeparator;
    
    /// <summary>
    /// The constructor for a <see cref="PathSanitizer"/>, taking in initialization data.
    /// </summary>
    /// <param name="fixedSeparator">See: <see cref="FixedSeparator"/></param>
    public PathSanitizer(string fixedSeparator)
    {
      FixedSeparator = fixedSeparator;
    }

    /// <summary>
    /// The copy constructor for a <see cref="PathSanitizer"/>.
    /// </summary>
    /// <param name="original">The original <see cref="PathSanitizer"/> to copy.</param>
    public PathSanitizer(PathSanitizer original)
    {
      // Copy all the standard data.
      rootMode = original.rootMode;
      replacementMode = original.replacementMode;
      removeRedundantSeparators = original.removeRedundantSeparators;
      fullyQualify = original.fullyQualify;
      autoRebuildQuickReplacements = original.autoRebuildQuickReplacements;
      quickReplacementRegex = original.quickReplacementRegex;
      fixedSeparator = original.fixedSeparator;

      // Copy the possible separators.
      int count = original.possibleSeparators.Count;
      for (int i = 0; i < count; i++)
        possibleSeparators.Add(original.possibleSeparators[i]);

      // Copy the quick replacements.
      count = original.quickReplacements.Count;
      for (int i = 0; i < count; i++)
        quickReplacements.Add(original.quickReplacements[i]);

      // Copy the exact replacements.
      foreach (KeyValuePair<string, string> pair in original.exactReplacements)
        exactReplacements.Add(pair.Key, pair.Value);
    }

    /// <summary>
    /// A function for sanitizing a given filepath, directory, or filename.
    /// </summary>
    /// <param name="filepath">The path to sanitize.</param>
    /// <param name="sanitizedPath">The saniztized <paramref name="filepath"/>.</param>
    /// <returns>Returns if the sanitization was successful.</returns>
    /// <remarks>Path length is not sanitized.</remarks>
    public bool SanitizePath(string filepath, out string sanitizedPath)
    {
      sanitizedPath = string.Empty; // Initialize the sanitized return.

      // Return false immediately if there is no filepath given.
      if (string.IsNullOrEmpty(filepath))
        return false;

      // Replace all possible separators with the universal separator.
      int separatorCount = possibleSeparators.Count;
      for (int i = 0; i < separatorCount; i++)
        filepath = filepath.Replace(possibleSeparators[i], fixedSeparator);

      string[] splitPaths = SplitFilePath(ref filepath); // Split the filepath.

      // Emptiness only occurs if redundancy is removed, and only separators remain.
      if (splitPaths.IsEmpty())
        return true;

      // Check if there is a root.
      bool hasRoot = CheckRoot(splitPaths, ref filepath, out string root, out int startIndex);

      SanitizeAllPaths(splitPaths, startIndex); // Sanitize each individual path.

      StringBuilder finalPath = new StringBuilder(); // Begin a StringBuilder for the path.

      // If a letter root exists, append it.
      if (hasRoot)
        finalPath.Append(root);

      // Append all the paths together, and create one final path.
      sanitizedPath = AppendAllPaths(splitPaths, startIndex, finalPath);

      // If fully qualifying, make sure the path is a valid filepath or directory path.
      if (fullyQualify &&
         (FileIO.IsValidFilePath(sanitizedPath) || FileIO.IsValidDirectory(sanitizedPath)))
      {
        sanitizedPath = Path.GetFullPath(sanitizedPath); // Qualify the path.
      }

      return true; // The path is sanitized as best as it can be.
    }

    /// <summary>
    /// A function for adding replacable <see cref="string"/>s to the
    /// <see cref="quickReplacements"/>. <see cref="quickReplacements"/> are replaced with
    /// <see cref="string.Empty"/>.
    /// </summary>
    /// <param name="replacements">The <see cref="string"/>s that can be replaced.</param>
    /// <remarks>The replacements should be formatted for a <see cref="Regex"/>. For example,
    /// passing in the '/' character should be passed as '@"\/"'.</remarks>
    public void AddQuickReplacements(params string[] replacements)
    {
      // If the array is not null or empty, begin adding its items.
      if (replacements.IsNotEmptyOrNull())
      {
        int length = replacements.Length;

        // For each item, if it is not null and is unique, add it to the list.
        for (int i = 0; i < length; i++)
        {
          if (replacements[i] != null)
            quickReplacements.AddUnique(replacements[i]);
        }

        // Auto rebuild the regex if applicable.
        if (autoRebuildQuickReplacements)
          BuildQuickReplacementRegex();
      }
    }

    /// <summary>
    /// A function for removing replacable <see cref="string"/>s from the
    /// <see cref="quickReplacements"/>. <see cref="quickReplacements"/> are replaced with
    /// <see cref="string.Empty"/>.
    /// </summary>
    /// <param name="replacements">The <see cref="string"/>s to remove.</param>
    /// <remarks>The replacements should be formatted for a <see cref="Regex"/>. For example,
    /// passing in the '/' character should be passed as '@"\/"'.</remarks>
    public void RemoveQuickReplacements(params string[] replacements)
    {
      // If the replacement is not null, and is unique, add it to the list.
      if (replacements.IsNotEmptyOrNull())
      {
        int length = replacements.Length;

        for (int i = 0; i < length; i++)
        {
          // Get the index of the replacement, if it is in the list.
          int index = quickReplacements.IndexOf(replacements[i]);

          // If the replacement exists, remove it from the list.
          if (index >= 0)
            quickReplacements.RemoveAt(index);
        }

        // Auto rebuild the regex if applicable.
        if (autoRebuildQuickReplacements)
          BuildQuickReplacementRegex();
      }
    }

    /// <summary>
    /// A function for rebuilding the <see cref="quickReplacementRegex"/>. Make sure to call this
    /// if <see cref="autoRebuildQuickReplacements"/> is <see langword="false"/>.
    /// </summary>
    public void BuildQuickReplacementRegex()
    {
      // If there are no quick replacements, build an empty Regex.
      if (quickReplacements.IsEmpty())
      {
        quickReplacementRegex = string.Empty;
        return;
      }

      // Initialize a StringBuilder, and get the number of replacements.
      StringBuilder regex = new StringBuilder();
      int count = quickReplacements.Count;

      // Add all but the last replacement, along with a Regex pipe symbol.
      for (int i = 0; i < count - 1; i++)
        regex.Append(quickReplacements[i]).Append(RegexPipe);

      // Append the last replacement, without the pipe.
      regex.Append(quickReplacements.LastElement());

      quickReplacementRegex = regex.ToString();
    }

    /// <summary>
    /// A helper function for splitting a filepath based on the <see cref="fixedSeparator"/>.
    /// </summary>
    /// <param name="filepath">The filepath to split.</param>
    /// <returns>Returns a <see cref="string"/> array of split paths.</returns>
    private string[] SplitFilePath(ref string filepath)
    {
      // If not removing redundant separators, then leave in empty paths.
      if (!removeRedundantSeparators)
      {
        string[] paths = filepath.Split(new string[] { fixedSeparator }, StringSplitOptions.None);

        // Put back in all the separators for now, so that their positions aren't removed.
        int length = paths.Length;
        for (int i = 0; i < length; i++)
        {
          if (paths[i] == string.Empty)
            paths[i] = fixedSeparator;
        }

        return paths;
      }
        
      // Otherwise, remove empty paths.
      return filepath.Split(new string[] { fixedSeparator }, StringSplitOptions.RemoveEmptyEntries);
    }

    /// <summary>
    /// A helper function for checking if there is a lettered drive root in the filepath.
    /// </summary>
    /// <param name="paths">The array of paths that make up the final path.</param>
    /// <param name="filepath">The original filepath.</param>
    /// <param name="root">The lettered root, if it exists. Returns as
    /// <see cref="string.Empty"/> if a root is not found.</param>
    /// <param name="startIndex">The first valid index to add paths from.</param>
    /// <returns>Returns if a root was found.</returns>
    private bool CheckRoot(string[] paths, ref string filepath, out string root, out int startIndex)
    {
      bool result; // Hold onto the result from the root method.

      // Switch based on the root mode.
      switch (rootMode)
      {
        case RootMode.SeparatorOnly:
          result = SeparatorRootOnly(paths, ref filepath, out root, out startIndex);
          break;
        case RootMode.RemoveAllRoots:
          result = RemoveAllRoots(paths, out root, out startIndex);
          break;
        default:
          result = AllowAllRoots(paths, ref filepath, out root);
          startIndex = 0;
          break;
      }

      // If there is no root, but one is required, force a separator root to exist.
      if (!result && forceRootSeparator)
      {
        root = fixedSeparator;
        result = true;
      }

      return result;
    }

    /// <summary>
    /// A helper function for removing all starting roots from a filepath.
    /// </summary>
    /// <param name="paths">The array of paths that make up the final path.</param>
    /// <param name="root">The lettered root, if it exists. Returns as
    /// <see cref="string.Empty"/> if a root is not found.</param>
    /// <param name="startIndex">The first valid index to add paths from.</param>
    /// <returns>Always returns <see langword="false"/>.</returns>
    private bool RemoveAllRoots(string[] paths, out string root, out int startIndex)
    {
      int length = paths.Length; // Get the path length.
      root = string.Empty; // Initialize the empty root.

      // Iterate through the array until a non-root path is found.
      for (startIndex = 0; startIndex < length; startIndex++)
      {
        string path = paths[startIndex]; // Get the current path.

        // Check if it is not a separator root.
        if (path != fixedSeparator)
        {
          // Attempt to get a letter root off of the path.
          string possibleRoot = path.Length >= FileIO.LetterRootLength ?
            path.Substring(0, FileIO.LetterRootLength) : string.Empty;

          // Check if the possible root is indeed a leettered root.
          if (Regex.IsMatch(possibleRoot, LetteredRootRegex, RegexOptions.CultureInvariant))
          {
            // Remove the root from the current path.
            path = path.Substring(FileIO.LetterRootLength, path.Length - FileIO.LetterRootLength);

            // If there is still more to the path, this is no longer a root, and thus is valid.
            if (path.Length > 0)
            {
              startIndex++;
              break;
            }
          }
          else
            break;
        }
      }

      return false; // Always return false for the purposes of the parent function.
    }

    /// <summary>
    /// A helper function for enforcing only <see cref="fixedSeparator"/>s in a path.
    /// </summary>
    /// <param name="paths">The array of paths that make up the final path.</param>
    /// <param name="filepath">The original filepath.</param>
    /// <param name="root">The lettered root, if it exists. Returns as
    /// <see cref="string.Empty"/> if a root is not found.</param>
    /// <param name="startIndex">The first valid index to add paths from.</param>
    /// <returns>Returns if a root was found.</returns>
    private bool SeparatorRootOnly(string[] paths, ref string filepath, out string root,
                                   out int startIndex)
    {
      startIndex = 0; // Initialize the first valid index.
      string firstPath = paths[startIndex]; // Get the first path, which would have the root.

      // If the path starts with the separator, or there is a redundant separator, make it the root.
      if (filepath.StartsWith(fixedSeparator) || firstPath == fixedSeparator)
      {
        // The root is only required if redundancy is removed. Regardless, act like it exists.
        root = removeRedundantSeparators ? fixedSeparator : string.Empty;
        return true;
      }

      // Substring the root, if the path is even long enough to contain it.
      root = firstPath.Length >= FileIO.LetterRootLength ?
             firstPath.Substring(0, FileIO.LetterRootLength) : string.Empty;

      // If the Lettered Root exists, chop it off from the filepath for now.
      if (Regex.IsMatch(root, LetteredRootRegex, RegexOptions.CultureInvariant))
      {
        startIndex++; // Increment the starting index, as the first path is not valid.

        // If the first path was only the root, there was a separator root after it that is usable.
        if (firstPath.Length <= FileIO.LetterRootLength && paths.Length > 1)
        {
          root = fixedSeparator;
          return true;
        }
      }

      return false; // A root was not found.
    }

    /// <summary>
    /// A helper function for checking for any possible root.
    /// </summary>
    /// <param name="paths">The array of paths that make up the final path.</param>
    /// <param name="filepath">The original filepath.</param>
    /// <param name="root">The lettered root, if it exists. Returns as
    /// <see cref="string.Empty"/> if a root is not found.</param>
    /// <returns>Returns if a root was found.</returns>
    private bool AllowAllRoots(string[] paths, ref string filepath, out string root)
    {
      // If the path starts with the separator, or there is a redundant separator, make it the root.
      if (filepath.StartsWith(fixedSeparator))
      {
        root = removeRedundantSeparators ? fixedSeparator : string.Empty;
        return true;
      }

      string firstPath = paths[0]; // Get the first path, which would have the root.

      // Substring the root, if the path is even long enough to contain it.
      root = firstPath.Length >= FileIO.LetterRootLength ?
             firstPath.Substring(0, FileIO.LetterRootLength) : string.Empty;

      // If the Lettered Root exists, chop it off from the filepath for now.
      if (Regex.IsMatch(root, LetteredRootRegex, RegexOptions.CultureInvariant))
      {
        // Reset the first path and return that a root was found.
        paths[0] = firstPath.Substring(FileIO.LetterRootLength, firstPath.Length - FileIO.LetterRootLength);
        root += fixedSeparator;
        return true;
      }

      return false; // A root was not found.
    }

    /// <summary>
    /// A helper function for sanitizing each individual path that makes up a filepath, based on
    /// the <see cref="replacementMode"/>.
    /// </summary>
    /// <param name="paths">The array of paths that make up the final path.</param>
    /// <param name="startIndex">The index to start sanitizing from.</param>
    private void SanitizeAllPaths(string[] paths, int startIndex)
    {
      // Sanitize based on the given order. By default, only do a quick sanitization.
      switch (replacementMode)
      {
        case ReplacementMode.ExactOnly:
          ExactSanitizePaths(paths, startIndex);
          break;
        case ReplacementMode.QuickThenExact:
          QuickSanitizePaths(paths, startIndex);
          ExactSanitizePaths(paths, startIndex);
          break;
        case ReplacementMode.ExactThenQuick:
          ExactSanitizePaths(paths, startIndex);
          QuickSanitizePaths(paths, startIndex);
          break;
        default:
          QuickSanitizePaths(paths, startIndex);
          break;
      }
    }

    /// <summary>
    /// A helper function for sanitizing each individual path that makes up a filepath, using the
    /// <see cref="quickReplacementRegex"/>.
    /// </summary>
    /// <param name="paths">The array of paths that make up the final path.</param>
    /// <param name="startIndex">The index to start sanitizing from.</param>
    private void QuickSanitizePaths(string[] paths, int startIndex)
    {
      int length = paths.Length; // Get the number of paths.

      // Extra precautions are necessary if redundancy is not removed.
      if (removeRedundantSeparators)
      {
        // Sanitize each path.
        for (int i = startIndex; i < length; i++)
        {
          // First, replace using the Regex created earlier.
          string path = paths[i];
          path = Regex.Replace(path, quickReplacementRegex, string.Empty, RegexOptions.CultureInvariant);
          path = path.Trim(); // Trim leading and trailing whitespace.
          path = path.TrimEnd(TrimmableChar); // Trim a character disallowed by file systems.
          paths[i] = path; // Put the path back into the array.
        }
      }
      else
      {
        // Sanitize each path.
        for (int i = startIndex; i < length; i++)
        {
          // First, replace using the Regex created earlier.
          string path = paths[i];

          // Only replace if the path isn't the fixed separator.
          if (path != fixedSeparator)
          {
            path = Regex.Replace(path, quickReplacementRegex, string.Empty, RegexOptions.CultureInvariant);
            path = path.Trim(); // Trim leading and trailing whitespace.
            path = path.TrimEnd(TrimmableChar); // Trim a character disallowed by file systems.
            paths[i] = path; // Put the path back into the array.
          }
        }
      }
    }

    /// <summary>
    /// A helper function for sanitizing each individual path that makes up a filepath, using the
    /// <see cref="exactReplacements"/>.
    /// </summary>
    /// <param name="paths">The array of paths that make up the final path.</param>
    /// <param name="startIndex">The index to start sanitizing from.</param>
    private void ExactSanitizePaths(string[] paths, int startIndex)
    {
      int length = paths.Length; // Get the number of paths.

      // Extra precautions are necessary if redundancy is not removed.
      if (removeRedundantSeparators)
      {
        // Iterate through every given replacement.
        foreach (KeyValuePair<string, string> pair in exactReplacements)
        {
          // Sanitize each path.
          for (int i = startIndex; i < length; i++)
          {
            // First, replace using the current key and value.
            string path = paths[i];
            path = path.Replace(pair.Key, pair.Value);
            path.Trim(); // Trim leading and trailing whitespace.
            path.TrimEnd(TrimmableChar); // Trim a leftover character disallowed by file systems.
            paths[i] = path; // Put the path back into the array.
          }
        }
      }
      else
      {
        // Iterate through every given replacement.
        foreach (KeyValuePair<string, string> pair in exactReplacements)
        {
          // Sanitize each path.
          for (int i = startIndex; i < length; i++)
          {
            // First, replace using the current key and value.
            string path = paths[i];

            // Only replace if the path isn't the fixed separator.
            if (path != fixedSeparator)
            {
              path = path.Replace(pair.Key, pair.Value);
              path.Trim(); // Trim leading and trailing whitespace.
              path.TrimEnd(TrimmableChar); // Trim a leftover character disallowed by file systems.
              paths[i] = path; // Put the path back into the array.
            }
          }
        }
      }
    }

    /// <summary>
    /// A helper function for joining all the individual paths together into one filepath.
    /// </summary>
    /// <param name="paths">The array of paths that make up the final path.</param>
    /// <param name="startIndex">The index to start appending from.</param>
    /// <param name="builder">The starting <see cref="StringBuilder"/>, which has the root.</param>
    /// <returns>Returns the final string.</returns>
    private string AppendAllPaths(string[] paths, int startIndex, StringBuilder builder)
    {
      int length = paths.Length; // Get the number of paths.

      // Extra precautions are necessary if redundancy is not removed.
      if (removeRedundantSeparators)
      {
        // Iterate through all but the last path, which does not get a separator.
        for (int i = startIndex; i < length - 1; i++)
        {
          // If the path is not null or empty whitespace, append it and a separator.
          if (!string.IsNullOrWhiteSpace(paths[i]))
            builder.Append(paths[i]).Append(fixedSeparator);
        }
      }
      else
      {
        // Iterate through all but the last path, which does not get a separator.
        for (int i = startIndex; i < length - 1; i++)
        {
          string path = paths[i]; // Get the current path.

          // Make sure the path is not null or empty whitespace.
          if (!string.IsNullOrWhiteSpace(path))
          {
            // Append the path, and only another separator if it was not already a separator.
            builder.Append(path);
            if (path != fixedSeparator)
              builder.Append(fixedSeparator);
          }
        }
      }

      // Append the last path under the same rules.
      if (!string.IsNullOrWhiteSpace(paths.LastElement()))
        builder.Append(paths.LastElement());

      return builder.ToString(); // Finalize the path.
    }

    /// <summary>
    /// A helper function for setting the <see cref="fixedSeparator"/>.
    /// </summary>
    /// <param name="separator">The new <see cref="fixedSeparator"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SetFixedSeparator(string separator)
    {
      fixedSeparator = separator ?? string.Empty;
    }
  }
  /************************************************************************************************/
}