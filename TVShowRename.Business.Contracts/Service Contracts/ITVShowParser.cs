using Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShowRename.Business.Entities;

namespace TVShowRename.Business.Contracts
{
   public interface ITVShowParser : IService
   {
      TVShowFile Parse(string file);

      bool CanParse(string file);
   }
}
