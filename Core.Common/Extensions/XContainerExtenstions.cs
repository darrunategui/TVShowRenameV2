using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Common.Extensions
{
    public static class XContainerExtenstions
    {
        public static bool HasElement(this XContainer xContainer, XName name)
        {
            return xContainer.Element(name) != null;
        }
    }
}
