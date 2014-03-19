using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Contracts
{
   public interface IServiceFactory
   {
      T GetServiceManager<T>() where T : IService;

      IEnumerable<T> GetServiceManagers<T>() where T : IService;
   }
}
