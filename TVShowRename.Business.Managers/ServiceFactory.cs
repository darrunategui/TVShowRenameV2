using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowRename.Business.Managers
{
    [Export(typeof(IServiceFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ServiceFactory : IServiceFactory
    {
        T IServiceFactory.GetServiceManager<T>()
        {
            try
            {
                return ObjectBase.Container.GetExport<T>().Value;
            }
            catch
            {
                throw;
            }
        }
    }
}
