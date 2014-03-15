using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShowRename.Business.Managers;

namespace TVShowRename.Business.Bootstrapper
{
    /// <summary>
    /// Loads the composition container with the appropriate parts from the required assemblies.
    /// </summary>
    public static class MEFLoader
    {
        /// <summary>
        /// Discovers all the parts available that have been exported and initializes a new composition container with them.
        /// </summary>
        /// <returns>A new composition container.</returns>
        public static CompositionContainer Initialize()
        {
            AggregateCatalog catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(TVDBManager).Assembly));

            CompositionContainer container = new CompositionContainer(catalog);
            return container;
        }
    }
}
