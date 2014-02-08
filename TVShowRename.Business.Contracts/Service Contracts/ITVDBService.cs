using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShowRename.Business.Entities;

namespace TVShowRename.Business.Contracts
{
    public interface ITVDBService
    {
        IEnumerable<Show> GetShowsByTitle(string title);

        IEnumerable<Episode> GetEpisodesByShowId(int id);

    }
}
