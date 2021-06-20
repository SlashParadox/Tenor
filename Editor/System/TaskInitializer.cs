/**************************************************************************************************/
/*!
\file   TaskInitializer.cs
\author Craig Williams
\par    Last Updated
        2021-06-19
\par    Copyright
        Copyright © 2021 Craig Joseph Williams, All Rights Reserved.

\brief
  A file containing implementation of a class specifically for handling Tasks in Unity Engine.

\par Bug List

\par References
*/
/**************************************************************************************************/

#if UNITY_2018_1_OR_NEWER && TENOR_INITIALIZE_TASKS_FOR_UNITY

using CodeParadox.Tenor.Threading;
using UnityEditor;

namespace CodeParadox.Tenor.UnityEditor
{
  /************************************************************************************************/
  /// <summary>
  /// A helper class specifically for Unity projects. If 'TENOR_INITIALIZE_TASKS_FOR_UNITY' is
  /// defined in the <see cref="Tenor"/>.<see cref="UnityEditor"/> assembly, this class will
  /// activate. It will bind the <see cref="TaskTokenSource.GlobalCancel"/> function to Unity's
  /// <see cref="EditorApplication.playModeStateChanged"/> event. This prevents asynchronous
  /// <see cref="System.Threading.Tasks.Task"/>s from continuing on without being asked to
  /// cancel when switching between player modes. If you do not want to use this method, simply
  /// remove the definition.
  /// </summary>
  public static class TaskInitializer
  {
    /// <summary>
    /// A helper function for binding <see cref="TaskTokenSource.GlobalCancel"/> to Unity's
    /// <see cref="EditorApplication.playModeStateChanged"/> event, preventing
    /// <see cref="System.Threading.Tasks.Task"/>s from continuing without being asked to
    /// cancel when switching between player modes.
    /// </summary>
    [InitializeOnLoadMethod]
    private static void InitializeGlobalTaskTokenSourceForUnity()
    {
      EditorApplication.playModeStateChanged += (_) => TaskTokenSource.GlobalCancel();
    }
  }
  /************************************************************************************************/
}
#endif