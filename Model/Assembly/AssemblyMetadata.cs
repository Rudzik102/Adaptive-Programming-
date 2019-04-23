using ProjectTPA.DatabaseHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTPA.Model
{
    [DataContract]
    public class AssemblyMetadata
    {
        /// <summary>
        /// The List of Namespaces in the Assembly
        /// </summary>
        [DataMember]
        public List<NamespaceMetadata> NamespaceModels { get; set; }

        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Constructor with assembly as a parameter
        /// </summary>
        /// <param name="assembly"></param>
        public AssemblyMetadata(Assembly assembly)
        {
            Name = assembly.ManifestModule.Name;
            Type[] types = assembly.GetTypes();
            NamespaceModels = types.Where(t => t.IsVisible).GroupBy(t => t.Namespace).OrderBy(t => t.Key)
                .Select(t => new NamespaceMetadata(t.Key, t.ToList())).ToList();
        }

        public AssemblyMetadata(AssemblyBase assemblyBase)
        {
            Name = assemblyBase.Name;
            NamespaceModels = assemblyBase.Namespaces?.Select(x => new NamespaceMetadata(x)).ToList();
        }


    }
}
