using ProjectTPA.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace ProjectTPA.ViewModel.Module
{
    public static class ViewType
    {
        public static string ShowMethod(Type type)
        {
            string str = "";
            foreach (MethodInfo method in type.GetMethods())
            {
                ParameterInfo[] parameters = method.GetParameters();
                string parameterDescriptions = string.Join
                    (", ", method.GetParameters()
                                 .Select(x => x.ParameterType + " " + x.Name)
                                 .ToArray());

                str +="\t" + method.ReturnType +"  "+method.Name+" " +parameterDescriptions+"\r\n";
            }

            return str;
        }

        public static string ShowConstructor(Type type)
        {
            string str = "";
            foreach (ConstructorInfo n in type.GetConstructors())
            {
                string parameterDescriptions = string.Join
                    (", ", n.GetParameters()
                                 .Select(x => x.ParameterType + " " + x.Name)
                                 .ToArray());

                str +="\t" + n.ReflectedType.Name + " " + parameterDescriptions+"\r\n";
            }

            return str;
        }
    
        public static List<TreeViewItem> GetChildren(TypeMetadata type, TreeViewItem ime, List<string> parentType)
        {
            List<TreeViewItem> list = new List<TreeViewItem>();

            if (type.Fields == null)
            {
                return list;
            }

            foreach (ParameterMetadata fieldInfo in type.Fields)
            {
                if (!parentType.Contains(fieldInfo.Name))
                {
                    string name = fieldInfo.Name;

                    TreeViewItem imo = new TreeViewItem()
                    {
                        Name = fieldInfo.Type.Name + name
                    };

                    list.Add(imo);
                    parentType.Add(fieldInfo.Name);

                    if (fieldInfo.Type != null)
                    {
                        imo.Children = new ObservableCollection<TreeViewItem>(GetChildren(fieldInfo.GetType(), imo, parentType));
                    }
                }
            }

            return list;
        }

        public static List<TreeViewItem> GetChildren(Type type, TreeViewItem ime, List<string> parentType)
        {
            List<TreeViewItem> list = new List<TreeViewItem>();

            foreach (PropertyInfo fieldInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
            {
                if (!parentType.Contains(fieldInfo.Name))
                {
                    TreeViewItem imo = new TreeViewItem()
                    {
                        Name = fieldInfo.PropertyType.Name+"  "+ fieldInfo.Name
                    };

                    list.Add(imo);
                    parentType.Add(fieldInfo.Name);

                    if (fieldInfo.PropertyType.IsClass)
                    {
                        imo.Children = new ObservableCollection<TreeViewItem>(GetChildren(fieldInfo.GetType(), imo, parentType));
                    }
                }
            }

            return list;
        }
    }
}
