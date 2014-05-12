using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TVShowRename.Business.Managers;
using TVShowRename.Business.Entities;

namespace TVShowRename.Business.Managers.UnitTests
{
   [TestClass]
   public class TVShowParserTests
   {
      TVShowParser _parser;
      string _showName = "Test";

      [TestInitialize]
      public void Initialize()
      {
         _parser = new TVShowParser();
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void CanParse_Null_fileName_Throws()
      {
         string filename = null;
         string expectedParamName = "filename";
         string expectedErrorMessage = string.Format("Parameter cannot be null or empty.{0}Parameter name: {1}", Environment.NewLine, expectedParamName);

         try
         {
            _parser.CanParse(filename);
         }
         catch (ArgumentException ex)
         {
            Assert.AreEqual(expectedErrorMessage, ex.Message);
            Assert.AreEqual(expectedParamName, ex.ParamName);
            throw;
         }
      }

      /// <summary>
      /// filename format Test.S01E01.mkv
      /// </summary>
      [TestMethod]
      public void CanParse_valid1_fileName_CanParse()
      {
         string filename = @"Test.S01E01.HDTV.mkv";
         bool canParse = _parser.CanParse(filename);
         Assert.IsTrue(canParse);
      }

      /// <summary>
      /// filename format Test.S1E01.mkv
      /// </summary>
      [TestMethod]
      public void CanParse_valid2_fileName_CanParse()
      {
         string filename = @"Test.S1E01.HDTV.mkv";
         bool canParse = _parser.CanParse(filename);
         Assert.IsTrue(canParse);
      }

      /// <summary>
      /// filename format Test.S01E1.mkv
      /// </summary>
      [TestMethod]
      public void CanParse_valid3_fileName_CanParse()
      {
         string filename = @"Test.S01E1.HDTV.mkv";
         bool canParse = _parser.CanParse(filename);
         Assert.IsTrue(canParse);
      }

      /// <summary>
      /// filename format Test.S101E101.mkv
      /// </summary>
      [TestMethod]
      public void CanParse_valid4_fileName_CanParse()
      {
         string filename = @"Test.S101E101.HDTV.mkv";
         bool canParse = _parser.CanParse(filename);
         Assert.IsTrue(canParse);
      }

      /// <summary>
      /// No title - .S01E01.mkv
      /// </summary>
      [TestMethod]
      public void CanParse_Invalid_fileName_CantParse()
      {
         string filename = @".S01E01.HDTV.mkv";
         bool canParse = _parser.CanParse(filename);
         Assert.IsFalse(canParse);
      }

      /// <summary>
      /// Parses the filename Test.S01E01.HDTV.mkv and ensures the parsed values are accurate.
      /// </summary>
      [TestMethod]
      public void Parse_valid1_Parses()
      {
         int episode = 1;
         int season = 1;
         string episodeString = episode.ToString("00");
         string seasonString = season.ToString("00");
         string filename = string.Format("{0}.S{1}E{2}.HDTV.mkv", _showName, seasonString, episodeString);
         TVShowFile file = _parser.Parse(filename);

         Assert.AreEqual(episode, file.Episode);
         Assert.AreEqual(season, file.Season);
         Assert.AreEqual(_showName, file.ShowName);
         Assert.AreEqual(filename, file.Filename);
      }

      [TestMethod]
      [ExpectedException(typeof(InvalidOperationException))]
      public void Parse_Invalid_fileName_Throws()
      {
         try
         {
            _parser.Parse("invalid filename.");
         }
         catch
         {
            throw;
         }
      }
   }
}
