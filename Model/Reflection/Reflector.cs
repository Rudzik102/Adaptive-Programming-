using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTPA.Model
{
    public class Reflector
    {
        public AssemblyMetadata AssemblyModel { get; set; }
        public Reflector()
        {

        }

        public Reflector(Assembly assembly)
        {
            AssemblyModel = new AssemblyMetadata(assembly);
        }
        public Reflector(AssemblyMetadata assemblyModel)
        {
            AssemblyModel = assemblyModel;
        }

        public Reflector(string assemblyPath)
        {
            if (string.IsNullOrEmpty(assemblyPath))
                throw new System.ArgumentNullException();
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            AssemblyModel = new AssemblyMetadata(assembly);
        }
    }
}
