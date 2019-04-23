using ProjectTPA.DatabaseHelper;
using System.Collections.Generic;
using System.Linq;

namespace ProjectTPA.Database
{
    public static class DatabaseMapper
    {
        private static Dictionary<string, TypeBase> typeDictionary = new Dictionary<string, TypeBase>();

        public static AssemblyBase Convert(DbAssembly db)
        {
            typeDictionary.Clear();
            return new AssemblyBase()
            {
                Name = db.Name,
                Namespaces = db.Namespaces?.Select(Convert)
            };
        }

        private static NamespaceBase Convert(DbNamespace db)
        {
            return new NamespaceBase()
            {
                Name = db.Name,
                Types = db.Types?.Select(Convert)
            };
        }

        private static TypeBase Convert(DbType db)
        {
            if (db == null)
            {
                return null;
            }

            if (typeDictionary.ContainsKey(db.Name))
            {
                return typeDictionary[db.Name];
            }

            TypeBase typeBase = new TypeBase()
            {
                TypeName = db.Name,
                NamespaceName = db.Namespace,
                TypeKind = db.Type,
                BaseType = Convert(db.BaseType),
                DeclaringType = Convert(db.DeclaringType),
                Constructors = db.Constructors?.Select(Convert),
                Fields = db.Fields?.Select(Convert),
                GenericArguments = db.GenericArguments?.Select(Convert),
                ImplementedInterfaces = db.ImplementedInterfaces?.Select(Convert),
                Methods = db.Methods?.Select(Convert),
                NestedTypes = db.NestedTypes?.Select(Convert),
                Properties = db.Properties?.Select(Convert)
            };

            typeDictionary.Add(typeBase.TypeName, typeBase);

            return typeBase;
        }

        private static PropertyBase Convert(DbProperty db)
        {
            return new PropertyBase()
            {
                Name = db.Name,
                TypeMetadata = Convert(db.Type)
            };
        }

        private static ParameterBase Convert(DbParameter db)
        {
            return new ParameterBase()
            {
                Name = db.Name,
                TypeMetadata = Convert(db.Type)
            };
        }

        private static MethodBase Convert(DbMethod db)
        {
            return new MethodBase()
            {
                Name = db.Name,
                ReturnType = Convert(db.ReturnType),
                Parameters = db.Parameters?.Select(Convert),
                GenericArguments = db.GenericArguments?.Select(Convert),
                Extension = db.Extension
            };
        }
    }
}
