using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTPA.Model
{
    [DataContract(IsReference = true)]
    public class MethodMetadata
    {
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// List of Generic arguments
        /// </summary>
        [DataMember]
        public List<TypeMetadata> GenericArguments { get; set; }

        /// <summary>
        /// Tuple of modifiers for method ( Access level, Abstract, Static, Virtual)
        /// </summary>
        [DataMember]
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }

        /// <summary>
        /// The type that method returns
        /// </summary>
        [DataMember]
        public TypeMetadata ReturnType { get; set; }

        /// <summary>
        /// True if method is extension method 
        /// </summary>
        [DataMember]
        public bool Extension { get; set; }

        /// <summary>
        /// Parameters of the method
        /// </summary>
        [DataMember]
        public List<ParameterMetadata> Parameters { get; set; }

        /// <summary>
        /// Constructor with MethodBase parameter
        /// </summary>
        /// <param name="method"></param>
        public MethodMetadata(MethodBase method)
        {
            Name = method.Name;
            GenericArguments = !method.IsGenericMethodDefinition ? null : EmitGenericArguments(method);
            ReturnType = EmitReturnType(method);
            Parameters = EmitParameters(method);
            Modifiers = EmitModifiers(method);
            Extension = EmitExtension(method);
        }

        public MethodMetadata(ProjectTPA.DatabaseHelper.MethodBase methodBase)
        {
            Name = methodBase.Name;
            GenericArguments = methodBase.GenericArguments?.Select(TypeMetadata.GetOrAdd).ToList();
            ReturnType = TypeMetadata.GetOrAdd(methodBase.ReturnType);
            Parameters = methodBase.Parameters?.Select(x => new ParameterMetadata(x)).ToList();
            Extension = methodBase.Extension;
        }


        private List<TypeMetadata> EmitGenericArguments(MethodBase method)
        {
            return method.GetGenericArguments().Select(TypeMetadata.EmitReference).ToList();
        }

        /// <summary>
        /// Emits MethodModels collection from MetodBase collection
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<MethodMetadata> EmitMethods(Type type)
        {
            return type.GetMethods(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                                   BindingFlags.Static | BindingFlags.Instance).Select(t => new MethodMetadata(t)).ToList();
        }

        /// <summary>
        /// Emits ParametersModels collection from ParameterInfo collection
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private static List<ParameterMetadata> EmitParameters(MethodBase method)
        {
            return method.GetParameters().Select(t => new ParameterMetadata(t.Name,TypeMetadata.EmitReference(t.ParameterType))).ToList( );
        }

        /// <summary>
        /// Emits TypeModel to return from MethodBase 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private static TypeMetadata EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;
            return TypeMetadata.EmitReference(methodInfo.ReturnType);
        }

        /// <summary>
        /// Emits if Method is extension method or not from MethodBase
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private static bool EmitExtension(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }

        /// <summary>
        /// Emits Modifiers from MethodBase
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private static Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> EmitModifiers(MethodBase method)
        {
            AccessLevel access = method.IsPublic ? AccessLevel.Public :
                method.IsFamily ? AccessLevel.Protected :
                method.IsAssembly ? AccessLevel.Internal : AccessLevel.Private;

            AbstractEnum _abstract = method.IsAbstract ? AbstractEnum.Abstract : AbstractEnum.NotAbstract;

            StaticEnum _static = method.IsStatic ? StaticEnum.Static : StaticEnum.NotStatic;

            VirtualEnum _virtual = method.IsVirtual ? VirtualEnum.Virtual : VirtualEnum.NotVirtual;

            return new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>(access, _abstract, _static, _virtual);
        }

        public static List<MethodMetadata> EmitConstructors(Type type)
        {
            return type.GetConstructors().Select(t => new MethodMetadata(t)).ToList();
        }


    }
}
