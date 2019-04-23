using ProjectTPA.DatabaseHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTPA.Model
{
    [DataContract(IsReference = true)]
    public class NamespaceMetadata
    {
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// The List of types in the namespace
        /// </summary>
        [DataMember]
        public List<TypeMetadata> Types { get; set; }

        /// <summary>
        /// Constructor with name of the namespace and types as params 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="types"></param>
        public NamespaceMetadata(string name, List<Type> types)
        {
            Name = name;
            Types = types.OrderBy(t => t.Name).Select(TypeMetadata.EmitType).ToList();
        }

        public NamespaceMetadata(NamespaceBase namespaceBase)
        {
            Name = namespaceBase.Name;
            Types = namespaceBase.Types?.Select(x => TypeMetadata.GetOrAdd(x)).ToList();
        }

    }
}
