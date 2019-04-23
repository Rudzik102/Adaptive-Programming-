using ProjectTPA.DatabaseHelper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectTPA.Model
{
    public static class Mapper
    {
        private static Dictionary<string, TypeBase> typeDictionary = new Dictionary<string, TypeBase>();

        public static ProjectTPA.DatabaseHelper.AbstractEnum Convert(AbstractEnum e)
        {
            switch (e)
            {
                case AbstractEnum.Abstract:
                    return ProjectTPA.DatabaseHelper.AbstractEnum.Abstract;
                case AbstractEnum.NotAbstract:
                    return ProjectTPA.DatabaseHelper.AbstractEnum.NotAbstract;
                default:
                    throw new Exception();
            }
        }

        public static ProjectTPA.DatabaseHelper.AccessLevelEnum Convert(AccessLevel e)
        {
            switch (e)
            {
                case AccessLevel.Private:
                    return ProjectTPA.DatabaseHelper.AccessLevelEnum.IsPrivate;
                case AccessLevel.Protected:
                    return ProjectTPA.DatabaseHelper.AccessLevelEnum.IsProtected;
                case AccessLevel.Internal:
                    return ProjectTPA.DatabaseHelper.AccessLevelEnum.IsProtectedInternal;
                case AccessLevel.Public:
                    return ProjectTPA.DatabaseHelper.AccessLevelEnum.IsPublic;
                default:
                    throw new Exception();
            }
        }

        public static ProjectTPA.DatabaseHelper.SealedEnum Convert(SealedEnum e)
        {
            switch (e)
            {
                case SealedEnum.NotSealed:
                    return ProjectTPA.DatabaseHelper.SealedEnum.NotSealed;
                case SealedEnum.Sealed:
                    return ProjectTPA.DatabaseHelper.SealedEnum.Sealed;
                default:
                    throw new Exception();
            }
        }

        public static ProjectTPA.DatabaseHelper.StaticEnum Convert(StaticEnum e)
        {
            switch (e)
            {
                case StaticEnum.NotStatic:
                    return ProjectTPA.DatabaseHelper.StaticEnum.NotStatic;
                case StaticEnum.Static:
                    return ProjectTPA.DatabaseHelper.StaticEnum.Static;
                default:
                    throw new Exception();
            }
        }

        public static ProjectTPA.DatabaseHelper.VirtualEnum Convert(VirtualEnum e)
        {
            switch (e)
            {
                case VirtualEnum.NotVirtual:
                    return ProjectTPA.DatabaseHelper.VirtualEnum.NotVirtual;
                case VirtualEnum.Virtual:
                    return ProjectTPA.DatabaseHelper.VirtualEnum.Virtual;
                default:
                    throw new Exception();
            }
        }

        public static AssemblyBase Convert(AssemblyMetadata md)
        {
            typeDictionary.Clear();
            return new AssemblyBase()
            {
                Name = md.Name,
                Namespaces = md.NamespaceModels?.Select(Convert)
            };
        }

        private static NamespaceBase Convert(NamespaceMetadata md)
        {
            return new NamespaceBase()
            {
                Name = md.Name,
                Types = md.Types?.Select(Convert)
            };
        }

        private static TypeBase Convert(TypeMetadata md)
        {
            if (md == null)
                return null;

            if (typeDictionary.ContainsKey(md.Name))
                return typeDictionary[md.Name];

            TypeBase typeBase = new TypeBase()
            {
                TypeName = md.Name,
                BaseType = Convert(md.BaseType),
                DeclaringType = Convert(md.DeclaringType),
                //Modifiers = new Tuple<ProjectTPA.DatabaseHelper.AccessLevelEnum, ProjectTPA.DatabaseHelper.SealedEnum, ProjectTPA.DatabaseHelper.AbstractEnum>(Convert(md.Modifiers.Item1), Convert(md.Modifiers.Item2), Convert(md.Modifiers.Item3)),
                Constructors = md.Constructors?.Select(Convert),
                Fields = md.Fields?.Select(Convert),
                GenericArguments = md.GenericArguments?.Select(Convert),
                ImplementedInterfaces = md.ImplementedInterfaces?.Select(Convert),
                Methods = md.Methods?.Select(Convert),
                NestedTypes = md.NestedTypes?.Select(Convert),
                Properties = md.Properties?.Select(Convert)
            };

            typeDictionary.Add(typeBase.TypeName, typeBase);
            return typeBase;
        }

        private static PropertyBase Convert(PropertyMetadata md)
        {
            return new PropertyBase()
            {
                Name = md.Name,
                TypeMetadata = Convert(md.Type)
            };
        }

        private static ParameterBase Convert(ParameterMetadata md)
        {
            return new ParameterBase()
            {
                Name = md.Name,
                TypeMetadata = Convert(md.Type)
            };
        }

        private static MethodBase Convert(MethodMetadata md)
        {
            return new MethodBase()
            {
                Name = md.Name,
                ReturnType = Convert(md.ReturnType),
                Parameters = md.Parameters?.Select(Convert),
                GenericArguments = md.GenericArguments?.Select(Convert),
                Modifiers = new Tuple<ProjectTPA.DatabaseHelper.AccessLevelEnum, ProjectTPA.DatabaseHelper.AbstractEnum, ProjectTPA.DatabaseHelper.StaticEnum, ProjectTPA.DatabaseHelper.VirtualEnum>(Convert(md.Modifiers.Item1), Convert(md.Modifiers.Item2), Convert(md.Modifiers.Item3), Convert(md.Modifiers.Item4)),
                Extension = md.Extension
            };
        }
    }
}
