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
         try
         {
            List<Show> shows = await _tvdbManager.GetShowsByTitle("hjbijbkj");
         }
         catch (Exception)
         {
            throw;
         }
      }
   }
}
