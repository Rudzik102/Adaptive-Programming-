using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ProjectTPA.ConsoleApp
{ 
    public class ViewType
    {

        public string GetChildren(Type type, int indent, List<string> parentType, string sum)
        {

            sum += ShowMethod(type, indent+1);
            sum += ShowConstructor(type, indent+1);
            foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
            {
                if (!parentType.Contains(fieldInfo.Name))
                {
                    sum +=fieldInfo.FieldType+ " " + fieldInfo.Name+"\r\n";
                    parentType.Add(fieldInfo.Name);
                    if (fieldInfo.FieldType.IsClass)
                    {
                        sum += new String('\t', indent) + fieldInfo.Attributes + fieldInfo.FieldType.Name + fieldInfo.Name + "\r\n" + GetChildren(fieldInfo.GetType(), indent, parentType, sum);
                        //DoRecursive(fieldInfo.FieldType, indent + 1, parentType, sum);
                    }
                }
            }
            return sum;
        }
        

        public string ShowMethod(Type type, int indent)
        {
            string str = "";
            foreach (MethodInfo method in type.GetMethods())
            {
                ParameterInfo[] parameters = method.GetParameters();
                string parameterDescriptions = string.Join
                    (", ", method.GetParameters()
                                 .Select(x => x.ParameterType + " " + x.Name)
                                 .ToArray());

                str += new String('\t', indent) + method.ReturnType +" " + method.Name + " " + parameterDescriptions+"\r\n";
            }
            return str;
        }

        public string ShowConstructor(Type type, int indent)
        {
            string str = "";
            foreach (ConstructorInfo n in type.GetConstructors())
            {
                string parameterDescriptions = string.Join
                    (", ", n.GetParameters()
                                 .Select(x => x.ParameterType + " " + x.Name)
                                 .ToArray());

                str += new String('\t', indent)+""+ n.ReflectedType.Name+""+parameterDescriptions+"\r\n";
            }
            return str;
        }
    }
}