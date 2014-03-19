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
                return ObjectBase.Container.GetExportedValue<T>();
            }
            catch
            {
                throw;
            }
        }


        IEnumerable<T> IServiceFactory.GetServiceManagers<T>()
        {
           try
           {
              return ObjectBase.Container.GetExportedValues<T>();
           }
           catch
           {
              throw;
           }
        }
    }
}
