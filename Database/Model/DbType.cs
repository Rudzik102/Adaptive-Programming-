using ProjectTPA.DatabaseHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTPA.Database
{
    public class DbType
    {
        public static Dictionary<string, DbType> storedTypes = new Dictionary<string, DbType>();
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }
        public DbType BaseType { get; set; }
        public TypeKindEnum Type { get; set; }
        public DbType DeclaringType { get; set; }
        public AccessLevelEnum AccessLevel { get; set; }
        public SealedEnum Sealed { get; set; }
        public AbstractEnum Abstract { get; set; }
        public ICollection<DbMethod> Constructors { get; set; }
        public ICollection<DbParameter> Fields { get; set; }
        public ICollection<DbType> GenericArguments { get; set; }
        public ICollection<DbType> ImplementedInterfaces { get; set; }
        public ICollection<DbMethod> Methods { get; set; }
        public ICollection<DbType> NestedTypes { get; set; }
        public ICollection<DbProperty> Properties { get; set; }

        public virtual ICollection<DbType> TypeBaseTypes { get; set; }
        public virtual ICollection<DbType> TypeDeclaringTypes { get; set; }
        [InverseProperty("GenericArguments")]
        public virtual ICollection<DbMethod> MethodGenericArguments { get; set; }
        [InverseProperty("GenericArguments")]
        public virtual ICollection<DbType> TypeGenericArguments { get; set; }
        [InverseProperty("ImplementedInterfaces")]
        public virtual ICollection<DbType> TypeImplementedInterfaces { get; set; }
        [InverseProperty("NestedTypes")]
        public virtual ICollection<DbType> TypeNestedTypes { get; set; }

        public DbType()
        {
            MethodGenericArguments = new HashSet<DbMethod>();
            TypeGenericArguments = new HashSet<DbType>();
            TypeImplementedInterfaces = new HashSet<DbType>();
            TypeNestedTypes = new HashSet<DbType>();
        }

        public DbType(TypeBase typeBase)
        {
            if (!storedTypes.ContainsKey(typeBase.TypeName))
                storedTypes.Add(typeBase.TypeName, this);

            Name = typeBase.TypeName;
            Namespace = typeBase.NamespaceName;
            BaseType = GetOrAdd(typeBase.BaseType);
            Type = typeBase.TypeKind;
            DeclaringType = GetOrAdd(typeBase.DeclaringType);
            //AccessLevel = typeBase.Modifiers.Item1;
            //Sealed = typeBase.Modifiers.Item2;
            //Abstract = typeBase.Modifiers.Item3;
            Constructors = typeBase.Constructors?.Select(x => new DbMethod(x)).ToList();
            Fields = typeBase.Fields?.Select(x => new DbParameter(x)).ToList();
            GenericArguments = typeBase.GenericArguments?.Select(x => GetOrAdd(x)).ToList();
            ImplementedInterfaces = typeBase.ImplementedInterfaces?.Select(x => GetOrAdd(x)).ToList();
            Methods = typeBase.Methods?.Select(x => new DbMethod(x)).ToList();
            NestedTypes = typeBase.NestedTypes?.Select(x => GetOrAdd(x)).ToList();
            Properties = typeBase.Properties?.Select(x => new DbProperty(x)).ToList();
        }

        public static DbType GetOrAdd(TypeBase typeBase)
        {
            if (typeBase != null)
            {
                if (storedTypes.ContainsKey(typeBase.TypeName))
                    return storedTypes[typeBase.TypeName];
                else
                    return new DbType(typeBase);
            }
            else
            {
                return null;
            }
        }
    }
}
