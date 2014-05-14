using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TVShowRename.Business.Entities;
using System.Threading.Tasks;
using System.Net;
using TVShowRename.Common;

namespace TVShowRename.Business.Managers.UnitTests
{
   [TestClass]
   public class TVDBManagerTests
   {
      TVDBManager _tvdbManager;

      [TestInitialize]
      public void Initialize()
      {
         _tvdbManager = new TVDBManager();
      }

      [TestMethod]
      [ExpectedException(typeof(ShowNotFoundException))]
      public async Task GetShowsByTitle_UnknownTitle_throws()
      {
         string title = "hjbijbkj";
         string expectedErrorMessage = string.Format("The show could not be found.{0}Show name: {1}", Environment.NewLine, title);
         try
         {
            List<Show> shows = await _tvdbManager.GetShowsByTitle(title);
         }
         catch (ShowNotFoundException ex)
         {
            Assert.AreEqual(title, ex.ShowName);
            Assert.AreEqual(expectedErrorMessage, ex.Message);
            throw;
         }
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public async Task GetShowsByTitle_NullTitle_throws()
      {
         string title = null;
         string expectedParamName = "title";
         string expectedErrorMessage = string.Format("Parameter cannot be null or empty.{0}Parameter name: {1}", Environment.NewLine, expectedParamName);
          
         try
         {
            await _tvdbManager.GetShowsByTitle(title);
         }
         catch (ArgumentException ex)
         {
            Assert.AreEqual(expectedErrorMessage, ex.Message);
            Assert.AreEqual(expectedParamName, ex.ParamName);
            throw;
         }
      }


   }
}
