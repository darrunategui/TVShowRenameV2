using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowRename.Common.Exceptions
{
   public class EpisodeNotFoundException : Exception
   {
      public EpisodeNotFoundException() { }

      public EpisodeNotFoundException(string message)
         : base(message) { }

      public EpisodeNotFoundException(string message, Exception inner)
         : base(message, inner) { }
   }
}
