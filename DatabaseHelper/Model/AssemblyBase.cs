using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTPA.DatabaseHelper
{
    public class AssemblyBase
    {
        public string Name;
        public IEnumerable<NamespaceBase> Namespaces;
    }
}
