using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShowRename.Business.Entities;

namespace TVShowRename.Client.Controllers
{
    public class RenameController
    {
        private TVShowFile _file;

        public RenameController(TVShowFile tvShowFile)
        {
            _file = tvShowFile;
        }

    }
}
