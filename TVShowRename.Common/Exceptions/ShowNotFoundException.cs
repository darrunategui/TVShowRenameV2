using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowRename.Common
{
   public class ShowNotFoundException : Exception
   {
      /// <summary>
      /// Gets the error message and the show name, or only the error message if no show name is set.
      /// </summary>
      public override string Message
      {
         get
         {
            if (String.IsNullOrEmpty(ShowName))
            {
               return base.Message;
            }
            else
            {
               return String.Format("{0}{1}Show name: {2}", base.Message, Environment.NewLine, ShowName);
            }
         }
      }

      /// <summary>
      /// Gets the name of the show that causes this exception.
      /// </summary>
      public string ShowName { get; private set; }

      public ShowNotFoundException() { }

      public ShowNotFoundException(string message, string show)
         : base(message)
      {
         ShowName = show;
      }

      public ShowNotFoundException(string message, string show, Exception inner)
         : base(message, inner)
      {
         ShowName = show;
      }
   }
}