using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowRename.Client
{
    public class MainController : Controller
    {
        private IMainView _view;

        public MainController(IMainView view)
        {
            _view = view;
            view.SetController(this);
        }

    }
}
