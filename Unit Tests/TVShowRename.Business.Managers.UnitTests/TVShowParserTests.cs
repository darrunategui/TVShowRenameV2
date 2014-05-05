using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TVShowRename.Business.Managers;

namespace TVShowRename.Business.Managers.UnitTests
{
   [TestClass]
   public class TVShowParserTests
   {
      TVShowParser _parser;


      [TestInitialize]
      public void Initialize()
      {
         _parser = new TVShowParser();
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void CanParse_Null_fileName()
      {
         string filename = null;
         string expectedParamName = "filename";
         string expectedErrorMessage = string.Format("Parameter cannot be null or empty.{0}Parameter name: {1}", Environment.NewLine, expectedParamName);

         try
         {
            if (_parser.CanParse(filename))
            { }
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
      public void CanParse_valid1_fileName()
      {
         string filename = @"Test.S01E01.HDTV.mkv";
         bool canParse = _parser.CanParse(filename);
         Assert.IsTrue(canParse);
      }

      /// <summary>
      /// filename format Test.S1E01.mkv
      /// </summary>
      [TestMethod]
      public void CanParse_valid2_fileName()
      {
         string filename = @"Test.S1E01.HDTV.mkv";
         bool canParse = _parser.CanParse(filename);
         Assert.IsTrue(canParse);
      }

      /// <summary>
      /// filename format Test.S01E1.mkv
      /// </summary>
      [TestMethod]
      public void CanParse_valid3_fileName()
      {
         string filename = @"Test.S01E1.HDTV.mkv";
         bool canParse = _parser.CanParse(filename);
         Assert.IsTrue(canParse);
      }

      /// <summary>
      /// filename format Test.S101E101.mkv
      /// </summary>
      [TestMethod]
      public void CanParse_valid4_fileName()
      {
         string filename = @"Test.S101E101.HDTV.mkv";
         bool canParse = _parser.CanParse(filename);
         Assert.IsTrue(canParse);
      }
   }
}
