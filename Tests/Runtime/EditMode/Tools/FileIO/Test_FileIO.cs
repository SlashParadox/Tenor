using CodeParadox.Tenor.Tools;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System.IO;

namespace CodeParadox.Tenor.Tests.Runtime.EditMode
{
  public class Test_FileIO
  {
    // <summary>
    /// A test for <see cref="FileIO.IsValidFilePath(string, bool)"/>.
    /// </summary>
    [Test(TestOf = typeof(FileIO))]
    public void CreateFile_SimpleFile_ReturnsSuccess()
    {
      string filepath = @"MyFile.txt";
      FileIO.SanitizeFilePath(filepath, out filepath);

      FileInfo info = new FileInfo(filepath);

      if (info.Exists)
      {
        File.Delete(filepath);
        info.Refresh();
      }
        

      Assert.IsTrue(FileIO.CreateFile(info, false, false));
      Assert.IsFalse(FileIO.CreateFile(info, false, false));
      Assert.IsTrue(FileIO.CreateFile(info.FullName, out _, false, true));
      File.Delete(filepath);
    }
  }
}
