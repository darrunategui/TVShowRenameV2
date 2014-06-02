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
       /// <summary>
       /// Indicates if this element has a child element with the specified System.Xml.Linq.XName.
       /// </summary>
       /// <param name="xContainer"></param>
       /// <param name="name">The XName to match.</param>
       /// <returns>true if a child element with the specified <paramref name="name"/> exists; false, otherwise.</returns>
        public static bool HasElement(this XContainer xContainer, XName name)
        {
            return xContainer.Element(name) != null;
        }
    }
}
