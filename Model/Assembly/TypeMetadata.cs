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
    public class TypeMetadata
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string AssemblyName { get; set; }
        [DataMember]
        public bool IsExternal { get; set; } = true;
        [DataMember]
        public bool IsGeneric { get; set; }
        [DataMember]
        public TypeMetadata BaseType { get; set; }
        [DataMember]
        public List<TypeMetadata> GenericArguments { get; set; }
        [DataMember]
        public Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum> Modifiers { get; set; }
        [DataMember]
        public TypeEnum Type { get; set; }
        [DataMember]
        public List<TypeMetadata> ImplementedInterfaces { get; set; }
        [DataMember]
        public List<TypeMetadata> NestedTypes { get; set; }
        [DataMember]
        public List<PropertyMetadata> Properties { get; set; }
        [DataMember]
        public TypeMetadata DeclaringType { get; set; }
        [DataMember]
        public List<MethodMetadata> Methods { get; set; }
        [DataMember]
        public List<MethodMetadata> Constructors { get; set; }
        [DataMember]
        public List<ParameterMetadata> Fields { get; set; }

        private TypeMetadata(Type type)
        {
            Name = type.Name;
            IsGeneric = type.IsGenericParameter;
            AssemblyName = type.AssemblyQualifiedName;
        }

        private void Analyze(Type type)
        {
            Type = GetTypeEnum(type);
            BaseType = EmitExtends(type.BaseType);
            Modifiers = EmitModifiers(type);
            
            DeclaringType = EmitDeclaringType(type.DeclaringType);
            Constructors = MethodMetadata.EmitConstructors(type);
            Methods = MethodMetadata.EmitMethods(type);
            NestedTypes = EmitNestedTypes(type);
            ImplementedInterfaces = EmitImplements(type.GetInterfaces()).ToList();
            GenericArguments = !type.IsGenericTypeDefinition ? null : EmitGenericArguments(type);
            Properties = PropertyMetadata.EmitProperties(type);
            Fields = EmitFields(type);
            IsExternal = false;
            _isAnalyzed = true;
        }

        private TypeMetadata(TypeBase typeBase)
        {
            if (!DictionaryTypeSingleton.Instance.ContainsKey(typeBase.TypeName))
                DictionaryTypeSingleton.Instance.Add(typeBase.TypeName, this);

            Name = typeBase.TypeName;
            BaseType = GetOrAdd(typeBase.BaseType);
            DeclaringType = GetOrAdd(typeBase.DeclaringType);
            Constructors = typeBase.Constructors?.Select(x => new MethodMetadata(x)).ToList();
            Fields = typeBase.Fields?.Select(x => new ParameterMetadata(x)).ToList();
            GenericArguments = typeBase.GenericArguments?.Select(GetOrAdd).ToList();
            ImplementedInterfaces = typeBase.ImplementedInterfaces?.Select(GetOrAdd).ToList();
            Methods = typeBase.Methods?.Select(x => new MethodMetadata(x)).ToList();
            NestedTypes = typeBase.NestedTypes?.Select(GetOrAdd).ToList();
            Properties = typeBase.Properties?.Select(x => new PropertyMetadata(x)).ToList();
        }


        public static TypeMetadata EmitType(Type type)
        {
            if (!DictionaryTypeSingleton.Instance.ContainsKey(type.Name))
            {
                DictionaryTypeSingleton.Instance.Add(type.Name, new TypeMetadata(type));
            }

            if (!DictionaryTypeSingleton.Instance.Get(type.Name)._isAnalyzed)
            {
                DictionaryTypeSingleton.Instance.Get(type.Name).Analyze(type);
            }

            return DictionaryTypeSingleton.Instance.Get(type.Name);
        }

        public static TypeMetadata EmitReference(Type type)
        {
            if (!DictionaryTypeSingleton.Instance.ContainsKey(type.Name))
            {
                DictionaryTypeSingleton.Instance.Add(type.Name, new TypeMetadata(type));

            }
            return DictionaryTypeSingleton.Instance.Get(type.Name);
        }

        public static List<TypeMetadata> EmitGenericArguments(Type type)
        {
            List<Type> arguments = type.GetGenericArguments().ToList();

            return arguments.Select(EmitReference).ToList();
        }

        public static TypeMetadata GetOrAdd(TypeBase typeBase)
        {
            if (typeBase != null)
            {
                if (DictionaryTypeSingleton.Instance.ContainsKey(typeBase.TypeName))
                    return DictionaryTypeSingleton.Instance.Get(typeBase.TypeName);
                else
                    return new TypeMetadata(typeBase);
            }
            else
            {
                return null;
            }
        }


        #region Private Emits

        private static List<ParameterMetadata> EmitFields(Type type)
        {
            List<FieldInfo> fieldInfo = type.GetFields(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                                           BindingFlags.Static | BindingFlags.Instance).ToList();

            List<ParameterMetadata> parameters = new List<ParameterMetadata>();
            foreach (FieldInfo field in fieldInfo)
            {
                parameters.Add(new ParameterMetadata(field.Name, EmitReference(field.FieldType)));
            }
            return parameters;
        }

        private TypeMetadata EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            return EmitReference(declaringType);
        }
        private List<TypeMetadata> EmitNestedTypes(Type type)
        {
            List<Type> nestedTypes = type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic).ToList();

            return nestedTypes.Select(EmitType).ToList();
        }
        private IEnumerable<TypeMetadata> EmitImplements(IEnumerable<Type> interfaces)
        {
            return from currentInterface in interfaces
                   select EmitReference(currentInterface);
        }
        private static TypeEnum GetTypeEnum(Type type)
        {
            return type.IsEnum ? TypeEnum.Enum :
                   type.IsValueType ? TypeEnum.Struct :
                   type.IsInterface ? TypeEnum.Interface :
                   TypeEnum.Class;
        }

        private static Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum> EmitModifiers(Type type)
        {
            AccessLevel _access = type.IsPublic || type.IsNestedPublic ? AccessLevel.Public :
                type.IsNestedFamily ? AccessLevel.Protected :
                type.IsNestedFamANDAssem ? AccessLevel.Internal :
                AccessLevel.Private;
            StaticEnum _static = type.IsSealed && type.IsAbstract ? StaticEnum.Static : StaticEnum.NotStatic;
            SealedEnum _sealed = SealedEnum.NotSealed;
            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            if (_static == StaticEnum.NotStatic)
            {
                _sealed = type.IsSealed ? SealedEnum.Sealed : SealedEnum.NotSealed;
                _abstract = type.IsAbstract ? AbstractEnum.Abstract : AbstractEnum.NotAbstract;
            }



            return new Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum>(_access, _sealed, _abstract, _static);
        }

        private static TypeMetadata EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;
            return EmitReference(baseType);
        }
        private bool _isAnalyzed = false;

        #endregion


    }
}
