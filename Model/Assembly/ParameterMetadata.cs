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
    public class ParameterMetadata
    {
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// TypeModel of the parameter
        /// </summary>
        [DataMember]
        public TypeMetadata Type { get; set; }

        /// <summary>
        /// Constructor with name and TypeModel as params
        /// </summary>
        /// <param name="name"></param>
        /// <param name="typeModel"></param>
        public ParameterMetadata(string name, TypeMetadata typeModel)
        {
            Name = name;
            Type = typeModel;
        }
        public ParameterMetadata(ParameterBase parameterBase)
        {
            Name = parameterBase.Name;
            Type = TypeMetadata.GetOrAdd(parameterBase.TypeMetadata);
        }
    }
}
