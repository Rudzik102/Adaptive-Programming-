using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTPA.DatabaseHelper
{
    public class TypeBase
    {
        public string TypeName;
        public string NamespaceName;
        public TypeBase BaseType;
        public IEnumerable<TypeBase> GenericArguments;
        //public Tuple<AccessLevelEnum, SealedEnum, AbstractEnum> Modifiers;
        public TypeKindEnum TypeKind;
        public IEnumerable<Attribute> Attributes;
        public IEnumerable<TypeBase> ImplementedInterfaces;
        public IEnumerable<TypeBase> NestedTypes;
        public IEnumerable<PropertyBase> Properties;
        public TypeBase DeclaringType;
        public IEnumerable<MethodBase> Methods;
        public IEnumerable<MethodBase> Constructors;
        public IEnumerable<ParameterBase> Fields;
    }
}
