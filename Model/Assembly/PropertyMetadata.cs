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
    [DataContract(IsReference = true)]
    public class PropertyMetadata
    {
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// TypeModel of the Property
        /// </summary>
        [DataMember]
        public TypeMetadata Type { get; set; }

        /// <summary>
        /// Constructor with name and TypeModel as params
        /// </summary>
        /// <param name="name"></param>
        /// <param name="propertyType"></param>
        public PropertyMetadata(string name, TypeMetadata propertyType)
        {
            Name = name;
            Type = propertyType;
        }

        /// <summary>
        /// Emits PropertyModel collection from PropertyInfo collection
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<PropertyMetadata> EmitProperties(Type type)
        {
            List<PropertyInfo> props = type
                .GetProperties(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                               BindingFlags.Static | BindingFlags.Instance).ToList();

            return props.Where(t => t.GetGetMethod().GetVisible() || t.GetSetMethod().GetVisible())
                .Select(t => new PropertyMetadata(t.Name, TypeMetadata.EmitReference(t.PropertyType))).ToList(); 
        }

        public PropertyMetadata(PropertyBase propertyBase)
        {
            Name = propertyBase.Name;
            Type = TypeMetadata.GetOrAdd(propertyBase.TypeMetadata);
        }
    }
}
