using Core.Common.Contracts;
using Core.Common.Core;
using System.ComponentModel.Composition;

namespace TVShowRename.Client
{
    public abstract class Controller
    {
        [Import]
        protected IServiceFactory _serviceFactory;

        /// <summary>
        /// Constructor imports are parts marked with the import attribute.
        /// </summary>
        public Controller()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }
    }
}
