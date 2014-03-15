using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowRename.Common
{
    public class ShowNotFoundException : Exception
    {
        public ShowNotFoundException() { }

        public ShowNotFoundException(string message)
            : base(message) { }

        public ShowNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}
