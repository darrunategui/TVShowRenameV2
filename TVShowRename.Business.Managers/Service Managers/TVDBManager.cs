using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShowRename.Business.Contracts;
using TVShowRename.Business.Entities;

namespace TVShowRename.Business.Managers
{
    public class TVDBManager : ITVDBService
    {
        public IEnumerable<Show> GetShowsByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Episode> GetEpisodesByShowId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
