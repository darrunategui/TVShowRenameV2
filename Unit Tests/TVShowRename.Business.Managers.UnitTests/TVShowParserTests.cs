using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TVShowRename.Business.Managers.UnitTests
{
   [TestClass]
   public class TVShowParserTests
   {
      [TestMethod]
      [ExpectedException(typeof(ArgumentNullException))]
      public void CanParse_Null_fileName()
      {
      }
   }
}
