using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShowRename.Business.Entities;

namespace TVShowRename.Client.Views.Interfaces
{
   public interface IShowsView : IView
   {
      void AddShows(IEnumerable<Show> shows);
   }
}
