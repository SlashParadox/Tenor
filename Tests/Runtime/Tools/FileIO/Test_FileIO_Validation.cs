using SlashParadox.Tenor.Tools;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System.IO;

namespace SlashParadox.Tenor.Tests.Runtime.EditMode
{
  public class Test_FileIO_Validation
  {
    /// <summary>
    /// A test for <see cref="FileIO.IsValidFilename(string)"/>.
    /// </summary>
    [Test(TestOf = typeof(FileIO))]
    public void ValidFilename_Universal_ReturnsSuccess()
    {
      Assert.IsTrue(FileIO.IsValidFilename(@"myFile.txt"));
      Assert.IsTrue(FileIO.IsValidFilename(@".gitignore"));
      Assert.IsTrue(FileIO.IsValidFilename(@"myFile"));
      Assert.IsTrue(FileIO.IsValidFilename(@"Hello.myFile.txt"));
      Assert.IsTrue(FileIO.IsValidFilename(@"Hello.myF ile.txt"));
      Assert.IsTrue(FileIO.IsValidFilename(@"my     File.txt"));
      Assert.IsTrue(FileIO.IsValidFilename(@".Hello.myFile.txt"));
      Assert.IsTrue(FileIO.IsValidFilename(@"#e^ll&o.my%File.t$xt"));
      Assert.IsTrue(FileIO.IsValidFilename(@"myFile.t!xt"));
      Assert.IsTrue(FileIO.IsValidFilename(@"..k"));

      Assert.IsFalse(FileIO.IsValidFilename(@".."));
      Assert.IsFalse(FileIO.IsValidFilename(@"Hello.myFile.txt  "));
      Assert.IsFalse(FileIO.IsValidFilename(@"<.myFile.txt"));
      Assert.IsFalse(FileIO.IsValidFilename(@"myFi>le.txt"));
      Assert.IsFalse(FileIO.IsValidFilename(@"myFi?le.txt"));
      Assert.IsFalse(FileIO.IsValidFilename(@"myFile*.txt"));
      Assert.IsFalse(FileIO.IsValidFilename(@"myFi|le.txt"));
      Assert.IsFalse(FileIO.IsValidFilename("myFi\0le.txt"));
      Assert.IsFalse(FileIO.IsValidFilename(@"myFi/le.txt"));
      Assert.IsFalse(FileIO.IsValidFilename(@"myFi\le.txt"));
    }

    /// <summary>
    /// A test for <see cref="FileIO.IsValidFilename(string, Data.OSType)"/>.
    /// </summary>
    [Test(TestOf = typeof(FileIO))]
    public void ValidFilename_UNIX_ReturnsSuccess()
    {
      Assert.IsTrue(FileIO.IsValidFilename(@"myFile.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilename(@".gitignore", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilename(@"myFile", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilename(@"Hello.myFile.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilename(@"Hello.myF ile.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilename(@"my     File.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilename(@".Hello.myFile.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilename(@"#e^ll&o.my%File.t$xt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilename(@"myFile.t!xt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilename(@"..k", Data.OSType.Linux));
      
      Assert.IsTrue(FileIO.IsValidFilename(@"<.myFile.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilename(@"myFi>le.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilename(@"myFi?le.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilename(@"myFile*.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilename(@"myFi|le.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilename(@"myFi\le.txt", Data.OSType.Linux));

      Assert.IsFalse(FileIO.IsValidFilename(@"..", Data.OSType.Linux));
      Assert.IsFalse(FileIO.IsValidFilename(@"Hello.myFile.txt  ", Data.OSType.Linux));
      Assert.IsFalse(FileIO.IsValidFilename("myFi\0le.txt", Data.OSType.Linux));
      Assert.IsFalse(FileIO.IsValidFilename(@"myFi/le.txt", Data.OSType.Linux));
    }

    /// <summary>
    /// A test for <see cref="FileIO.IsValidDirectory(string, bool)"/>.
    /// </summary>
    [Test(TestOf = typeof(FileIO))]
    public void ValidDirectory_Universal_ReturnsSuccess()
    {
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:/MyPrograms/"));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:/MyPrograms/MySubDirectory"));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:MyPrograms/"));
      Assert.IsTrue(FileIO.IsValidDirectory(@"     MyPrograms"));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:MyPrograms/MySubDirectory"));
      Assert.IsTrue(FileIO.IsValidDirectory(@"MyPrograms/MySubDirectory"));
      Assert.IsTrue(FileIO.IsValidDirectory(@"/MyPrograms/MySubDirectory"));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:////MyPro//grams////MySub///Dire//ctory"));

      Assert.IsTrue(FileIO.IsValidDirectory(@"C:/MyPrograms/", true));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:/MyPrograms/MySubDirectory", true));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:MyPrograms/", true));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:MyPrograms/MySubDirectory", true));
      Assert.IsTrue(FileIO.IsValidDirectory(@"/MyPrograms/MySubDirectory", true));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:/M  y..P.rogr.ams/", true));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:////MyPro//grams////MySub///Dire//ctory", true));

      Assert.IsFalse(FileIO.IsValidDirectory(@"MyPrograms/MySubDirectory", true));
      Assert.IsFalse(FileIO.IsValidDirectory(@"C::/MyPrograms/"));
      Assert.IsFalse(FileIO.IsValidDirectory(@"C:/MyProgr<ams/"));
      Assert.IsFalse(FileIO.IsValidDirectory(@"C:/MyPr>ograms/"));
      Assert.IsFalse(FileIO.IsValidDirectory(@"C:/?MyPrograms/"));
      Assert.IsFalse(FileIO.IsValidDirectory(@"C:/MyPrograms/   .tx|"));
      Assert.IsFalse(FileIO.IsValidDirectory(@"\MyPrograms\MySubDirectory"));
      Assert.IsFalse(FileIO.IsValidDirectory(@"///C:///MyP//rograms/MySu///bDirectory"));
    }

    /// <summary>
    /// A test for <see cref="FileIO.IsValidDirectory(string, Data.OSType, bool)"/>, specifically
    /// with a <see cref="Data.OSType.Windows"/> system.
    /// </summary>
    [Test(TestOf = typeof(FileIO))]
    public void ValidDirectory_Windows_ReturnsSuccess()
    {
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:/MyPrograms/", Data.OSType.Windows));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:/MyPrograms/MySubDirectory", Data.OSType.Windows));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:MyPrograms/", Data.OSType.Windows));
      Assert.IsTrue(FileIO.IsValidDirectory(@"     MyPrograms", Data.OSType.Windows));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:MyPrograms/MySubDirectory", Data.OSType.Windows));
      Assert.IsTrue(FileIO.IsValidDirectory(@"MyPrograms/MySubDirectory", Data.OSType.Windows));
      Assert.IsTrue(FileIO.IsValidDirectory(@"/MyPrograms/MySubDirectory", Data.OSType.Windows));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:////MyPro//grams////MySub///Dire//ctory", Data.OSType.Windows));
      Assert.IsTrue(FileIO.IsValidDirectory(@"\MyPrograms\MySubDirectory", Data.OSType.Windows));

      Assert.IsTrue(FileIO.IsValidDirectory(@"C:/MyPrograms/", Data.OSType.Windows, true));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:/MyPrograms/MySubDirectory", Data.OSType.Windows, true));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:MyPrograms/", Data.OSType.Windows, true));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:MyPrograms/MySubDirectory", Data.OSType.Windows, true));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:/M  y..P.rogr.ams/", Data.OSType.Windows, true));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:////MyPro//grams////MySub///Dire//ctory", Data.OSType.Windows, true));

      Assert.IsFalse(FileIO.IsValidDirectory(@"/MyPrograms/MySubDirectory", Data.OSType.Windows, true));
      Assert.IsFalse(FileIO.IsValidDirectory(@"MyPrograms/MySubDirectory", Data.OSType.Windows, true));
      Assert.IsFalse(FileIO.IsValidDirectory(@"C::/MyPrograms/", Data.OSType.Windows));
      Assert.IsFalse(FileIO.IsValidDirectory(@"C:/MyProgr<ams/", Data.OSType.Windows));
      Assert.IsFalse(FileIO.IsValidDirectory(@"C:/MyPr>ograms/", Data.OSType.Windows));
      Assert.IsFalse(FileIO.IsValidDirectory(@"C:/?MyPrograms/", Data.OSType.Windows));
      Assert.IsFalse(FileIO.IsValidDirectory(@"C:/MyPrograms/   .tx|", Data.OSType.Windows));
      Assert.IsFalse(FileIO.IsValidDirectory(@"///C:///MyP//rograms/MySu///bDirectory", Data.OSType.Windows));
    }

    /// <summary>
    /// A test for <see cref="FileIO.IsValidDirectory(string, Data.OSType, bool)"/>.
    /// </summary>
    [Test(TestOf = typeof(FileIO))]
    public void ValidDirectory_UNIX_ReturnsSuccess()
    {
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:/MyPrograms/", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:/MyPrograms/MySubDirectory", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:MyPrograms/", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidDirectory(@"     MyPrograms", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:MyPrograms/MySubDirectory", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidDirectory(@"MyPrograms/MySubDirectory", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidDirectory(@"/MyPrograms/MySubDirectory", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:////MyPro//grams////MySub///Dire//ctory", Data.OSType.Linux));
      

      Assert.IsTrue(FileIO.IsValidDirectory(@"/C:/MyPrograms/", Data.OSType.Linux, true));
      Assert.IsTrue(FileIO.IsValidDirectory(@"/C:/MyPrograms/MySubDirectory", Data.OSType.Linux, true));
      Assert.IsTrue(FileIO.IsValidDirectory(@"/C:MyPrograms/", Data.OSType.Linux, true));
      Assert.IsTrue(FileIO.IsValidDirectory(@"/C:MyPrograms/MySubDirectory", Data.OSType.Linux, true));
      Assert.IsTrue(FileIO.IsValidDirectory(@"/C:/M  y..P.rogr.ams/", Data.OSType.Linux, true));
      Assert.IsTrue(FileIO.IsValidDirectory(@"/C:////MyPro//grams////MySub///Dire//ctory", Data.OSType.Linux, true));

      Assert.IsTrue(FileIO.IsValidDirectory(@"/MyPrograms/MySubDirectory", Data.OSType.Linux, true));
      Assert.IsTrue(FileIO.IsValidDirectory(@"MyPrograms/MySubDirectory", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C::/MyPrograms/", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:/MyProgr<ams/", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:/MyPr>ograms/", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:/?MyPrograms/", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidDirectory(@"C:/MyPrograms/   .tx|", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidDirectory(@"///C:///MyP//rograms/MySu///bDirectory", Data.OSType.Linux));

      Assert.IsFalse(FileIO.IsValidDirectory(@"\MyPrograms\MySubDirectory", Data.OSType.Linux));
      Assert.IsFalse(FileIO.IsValidDirectory(@"MyPrograms/MySubDirectory", Data.OSType.Linux, true));
      Assert.IsFalse(FileIO.IsValidDirectory("\\MyProgr\0ams\\MySubDirectory", Data.OSType.Linux));
    }

    /// <summary>
    /// A test for <see cref="FileIO.IsValidFilePath(string, bool)"/>.
    /// </summary>
    [Test(TestOf = typeof(FileIO))]
    public void ValidFilePath_Universal_ReturnsSuccess()
    {
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:/MyPrograms/myFile.txt"));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:/MyPrograms/MySubDirectory/myFile.txt"));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:MyPrograms/myFile.txt"));
      Assert.IsTrue(FileIO.IsValidFilePath(@"     MyPrograms/myFile.txt"));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:MyPrograms/MySubDirectory/myFile.txt"));
      Assert.IsTrue(FileIO.IsValidFilePath(@"MyPrograms/MySubDirectory/myFile.txt"));
      Assert.IsTrue(FileIO.IsValidFilePath(@"/MyPrograms/MySubDirectory/myFile.txt"));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:////MyPro//grams////MySub///Dire//ctory/myFile.txt"));
      Assert.IsTrue(FileIO.IsValidFilePath(@"myFile.txt"));
      Assert.IsTrue(FileIO.IsValidFilePath(@"/myFile.txt"));

      Assert.IsTrue(FileIO.IsValidFilePath(@"C:/MyPrograms/myFile.txt", true));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:/MyPrograms/MySubDirectory/myFile.txt", true));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:MyPrograms//myFile.txt", true));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:MyPrograms/MySubDirectory/myFile.txt", true));
      Assert.IsTrue(FileIO.IsValidFilePath(@"/MyPrograms/MySubDirectory/myFile.txt", true));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:/M  y..P.rogr.ams//myFile.txt", true));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:////MyPro//grams////MySub///Dire//ctory/myFile.txt", true));

      Assert.IsFalse(FileIO.IsValidFilePath(@"MyPrograms/MySubDirectory/myFile.txt", true));
      Assert.IsFalse(FileIO.IsValidFilePath(@"C::/MyPrograms//myFile.txt"));
      Assert.IsFalse(FileIO.IsValidFilePath(@"C:/MyProgr<ams//myFile.txt"));
      Assert.IsFalse(FileIO.IsValidFilePath(@"C:/MyPr>ograms//myFile.txt"));
      Assert.IsFalse(FileIO.IsValidFilePath(@"C:/?MyPrograms//myFile.txt"));
      Assert.IsFalse(FileIO.IsValidFilePath(@"C:/MyPrograms/   .tx|/myFile.txt"));
      Assert.IsFalse(FileIO.IsValidFilePath(@"\MyPrograms\MySubDirectory/myFile.txt"));
      Assert.IsFalse(FileIO.IsValidFilePath(@"///C:///MyP//rograms/MySu///bDirectory/myFile.txt"));
      Assert.IsFalse(FileIO.IsValidFilePath(@"C:/MyPrograms/MySubDirectory/myFi?le.txt"));
      Assert.IsFalse(FileIO.IsValidFilePath(@"C:/MyPrograms/MySubDirectory/myFi<le.txt"));
      Assert.IsFalse(FileIO.IsValidFilePath(@"C:/MyPrograms/MySubDirectory/myFi|le.txt"));
    }

    /// <summary>
    /// A test for <see cref="FileIO.IsValidFilePath(string, Data.OSType, bool)"/>, specifically
    /// with a <see cref="Data.OSType.Windows"/> system.
    /// </summary>
    [Test(TestOf = typeof(FileIO))]
    public void ValidFilePath_Windows_ReturnsSuccess()
    {
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:/MyPrograms/myFile.txt", Data.OSType.Windows));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:/MyPrograms/MySubDirectory/myFile.txt", Data.OSType.Windows));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:MyPrograms/myFile.txt", Data.OSType.Windows));
      Assert.IsTrue(FileIO.IsValidFilePath(@"     MyPrograms/myFile.txt", Data.OSType.Windows));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:MyPrograms/MySubDirectory/myFile.txt", Data.OSType.Windows));
      Assert.IsTrue(FileIO.IsValidFilePath(@"MyPrograms/MySubDirectory/myFile.txt", Data.OSType.Windows));
      Assert.IsTrue(FileIO.IsValidFilePath(@"/MyPrograms/MySubDirectory/myFile.txt", Data.OSType.Windows));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:////MyPro//grams////MySub///Dire//ctory/myFile.txt", Data.OSType.Windows));
      Assert.IsTrue(FileIO.IsValidFilePath(@"myFile.txt", Data.OSType.Windows));
      Assert.IsTrue(FileIO.IsValidFilePath(@"/myFile.txt", Data.OSType.Windows));
      Assert.IsTrue(FileIO.IsValidFilePath(@"\MyPrograms\MySubDirectory/myFile.txt", Data.OSType.Windows));

      Assert.IsTrue(FileIO.IsValidFilePath(@"C:/MyPrograms/myFile.txt", Data.OSType.Windows, true));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:/MyPrograms/MySubDirectory/myFile.txt", Data.OSType.Windows, true));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:MyPrograms//myFile.txt", Data.OSType.Windows, true));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:MyPrograms/MySubDirectory/myFile.txt", Data.OSType.Windows, true));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:/M  y..P.rogr.ams//myFile.txt", Data.OSType.Windows, true));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:////MyPro//grams////MySub///Dire//ctory/myFile.txt", Data.OSType.Windows, true));

      Assert.IsFalse(FileIO.IsValidFilePath(@"/MyPrograms/MySubDirectory/myFile.txt", Data.OSType.Windows, true));
      Assert.IsFalse(FileIO.IsValidFilePath(@"MyPrograms/MySubDirectory/myFile.txt", Data.OSType.Windows, true));
      Assert.IsFalse(FileIO.IsValidFilePath(@"C::/MyPrograms//myFile.txt", Data.OSType.Windows));
      Assert.IsFalse(FileIO.IsValidFilePath(@"C:/MyProgr<ams//myFile.txt", Data.OSType.Windows));
      Assert.IsFalse(FileIO.IsValidFilePath(@"C:/MyPr>ograms//myFile.txt", Data.OSType.Windows));
      Assert.IsFalse(FileIO.IsValidFilePath(@"C:/?MyPrograms//myFile.txt", Data.OSType.Windows));
      Assert.IsFalse(FileIO.IsValidFilePath(@"C:/MyPrograms/   .tx|/myFile.txt", Data.OSType.Windows));
      Assert.IsFalse(FileIO.IsValidFilePath(@"///C:///MyP//rograms/MySu///bDirectory/myFile.txt", Data.OSType.Windows));
      Assert.IsFalse(FileIO.IsValidFilePath(@"C:/MyPrograms/MySubDirectory/myFi?le.txt", Data.OSType.Windows));
      Assert.IsFalse(FileIO.IsValidFilePath(@"C:/MyPrograms/MySubDirectory/myFi<le.txt", Data.OSType.Windows));
      Assert.IsFalse(FileIO.IsValidFilePath(@"C:/MyPrograms/MySubDirectory/myFi|le.txt", Data.OSType.Windows));
    }

    /// <summary>
    /// A test for <see cref="FileIO.IsValidFilePath(string, Data.OSType, bool)"/>.
    /// </summary>
    [Test(TestOf = typeof(FileIO))]
    public void ValidFilePath_Unix_ReturnsSuccess()
    {
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:/MyPrograms/myFile.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:/MyPrograms/MySubDirectory/myFile.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:MyPrograms/myFile.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilePath(@"     MyPrograms/myFile.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:MyPrograms/MySubDirectory/myFile.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilePath(@"MyPrograms/MySubDirectory/myFile.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilePath(@"/MyPrograms/MySubDirectory/myFile.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:////MyPro//grams////MySub///Dire//ctory/myFile.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilePath(@"myFile.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilePath(@"/myFile.txt", Data.OSType.Linux));

      Assert.IsFalse(FileIO.IsValidFilePath(@"\MyPrograms\MySubDirectory/myFile.txt", Data.OSType.Linux));
      Assert.IsFalse(FileIO.IsValidFilePath(@"C:/MyPrograms/myFile.txt", Data.OSType.Linux, true));
      Assert.IsFalse(FileIO.IsValidFilePath(@"C:/MyPrograms/MySubDirectory/myFile.txt", Data.OSType.Linux, true));
      Assert.IsFalse(FileIO.IsValidFilePath(@"C:MyPrograms//myFile.txt", Data.OSType.Linux, true));
      Assert.IsFalse(FileIO.IsValidFilePath(@"C:MyPrograms/MySubDirectory/myFile.txt", Data.OSType.Linux, true));
      Assert.IsFalse(FileIO.IsValidFilePath(@"C:/M  y..P.rogr.ams//myFile.txt", Data.OSType.Linux, true));
      Assert.IsFalse(FileIO.IsValidFilePath(@"C:////MyPro//grams////MySub///Dire//ctory/myFile.txt", Data.OSType.Linux, true));
      Assert.IsFalse(FileIO.IsValidFilePath(@"MyPrograms/MySubDirectory/myFile.txt", Data.OSType.Linux, true));

      Assert.IsTrue(FileIO.IsValidFilePath(@"C::/MyPrograms//myFile.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:/MyProgr<ams//myFile.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:/MyPr>ograms//myFile.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:/?MyPrograms//myFile.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:/MyPrograms/   .tx|/myFile.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilePath(@"///C:///MyP//rograms/MySu///bDirectory/myFile.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:/MyPrograms/MySubDirectory/myFi?le.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:/MyPrograms/MySubDirectory/myFi<le.txt", Data.OSType.Linux));
      Assert.IsTrue(FileIO.IsValidFilePath(@"C:/MyPrograms/MySubDirectory/myFi|le.txt", Data.OSType.Linux));
    }

    /// <summary>
    /// A test for <see cref="FileIO.IsValidFilePath(string, bool)"/>.
    /// </summary>
    [Test(TestOf = typeof(FileIO))]
    public void SanitizeFilePath_Universal_ReturnsSuccess()
    {
      Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"C:/MyProg:rams//myFile.txt")));


      Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"C:/MyPrograms/myFile.txt")));
      Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"C:/MyPrograms/MySubDirectory/myFile.txt")));
      Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"C:MyPrograms/myFile.txt")));
      Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"     MyPrograms/myFile.txt")));
      Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"C:MyPrograms/MySubDirectory/myFile.txt")));
      Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"MyPrograms/MySubDirectory/myFile.txt")));
      Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"/MyPrograms/MySubDirectory/myFile.txt")));
      Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"C:////MyPro//grams////MySub///Dire//ctory/myFile.txt")));
      Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"myFile.txt")));
      Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"/myFile.txt")));

      //Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"C:/MyPrograms/myFile.txt"), true));
      //Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"C:/MyPrograms/MySubDirectory/myFile.txt"), true));
      //Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"C:MyPrograms//myFile.txt"), true));
      //Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"C:MyPrograms/MySubDirectory/myFile.txt"), true));
      //Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"/MyPrograms/MySubDirectory/myFile.txt"), true));
      //Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"C:/M  y..P.rogr.ams//myFile.txt"), true));
      //Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"C:////MyPro//grams////MySub///Dire//ctory/myFile.txt"), true));

      Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"C::/MyPrograms//myFile.txt")));
      Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"C:/MyProgr<ams//myFile.txt")));
      Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"C:/MyPr>ograms//myFile.txt")));
      Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"C:/?MyPrograms//myFile.txt")));
      Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"C:/MyPrograms/   .tx|/myFile.txt")));
      Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"\MyPrograms\MySubDirectory/myFile.txt")));
      Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"///C:///MyP//rograms/MySu///bDirectory/myFile.txt")));
      Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"C:/MyPrograms/MySubDirectory/myFi?le.txt")));
      Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"C:/MyPrograms/MySubDirectory/myFi<le.txt")));
      Assert.IsTrue(FileIO.IsValidFilePath(FileIO.SanitizeFilePath(@"C:/MyPrograms/MySubDirectory/myFi|le.txt")));
    }


    /// <summary>
    /// A test for <see cref="FileIO.IsValidFilename(string, Data.OSType)"/>.
    /// </summary>
    [Test(TestOf = typeof(FileIO))]
    public void PathTest()
    {
      Directory.CreateDirectory(@"Sub0/Sub1/Sub2");
      File.Create(@"Sub0/myFile.txt");
      Directory.Delete(@"Sub0/Sub1/Sub2", true);
      Assert.IsFalse(Directory.Exists(@"Sub0"));
    }


  }
}
