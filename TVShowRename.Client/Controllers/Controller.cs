using Core.Common.Contracts;
using Core.Common.Core;
using System.ComponentModel.Composition;

namespace TVShowRename.Client
{
    public abstract class Controller
    {
        [Import]
        protected IServiceFactory _serviceFactory;

        public Controller()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }
    }
}
