using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShowRename.Business.Entities;

namespace TVShowRename.Client
{
    public interface IMainView : IView
    {
       string Status { set; }

       void VisualizeProgress(bool showprogess);
    }
}
