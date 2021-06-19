using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security;
using System.Text;
using UnityEngine;

namespace CodeParadox.Tenor.Tools
{
  public static partial class AppStack
  {
    public static string[] GetParsedFrames(StackTrace trace, StackFrameParser parser)
    {
      if (trace != null && parser != null && trace.FrameCount > 0)
        return GetParsedFramesInternal(trace, parser);

      return null;
    }

    private static string[] GetParsedFramesInternal(StackTrace trace, StackFrameParser parser)
    {
      int count = trace.FrameCount;
      string[] frames = new string[count];
      StringBuilder builder = new StringBuilder(70);

      for (int i = 0; i < count; i++)
      {
        builder.Clear();
        parser.ParseFrame(trace.GetFrame(i), builder);
        frames[i] = builder.ToString();
      }

      return frames;
    }
  }
}
