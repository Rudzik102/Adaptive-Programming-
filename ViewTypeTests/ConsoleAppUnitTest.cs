using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectTPA.ConsoleApp;
using ProjectTPA.Utility;

namespace ViewTypeTests
{
    [TestClass]
    public class ConsoleAppUnitTest
    {
        [TestMethod]
        public void LoadAssemblyTest()
        {
            string path = "../../../Model/bin/Debug/Model.dll";
            LoadAssembly load = new LoadAssembly();
            Assembly asse = load.Load(path);
            Assert.AreNotEqual(asse, null, "Błąd ładowania biblioteki dll");
        }
        [TestMethod]
        public void ConsoleAppGetChildren()
        {
            string suma = "";
            string path = "../../../Model/bin/Debug/Model.dll";
            LoadAssembly load = new LoadAssembly();
            Assembly asse = load.Load(path);
            ViewType view = new ViewType();
            List<string> parentType = new List<string>();
            suma = view.GetChildren(asse.GetType("ProjectTPA.Model.AssemblyMetadata"), 0, parentType, suma);
            Assert.IsTrue(suma.Contains("NamespaceModels"));
        }
        [TestMethod]
        public void ConsoleAppShowMethod()
        {
            string suma = "";
            string path = "../../../Model/bin/Debug/Model.dll";
            LoadAssembly load = new LoadAssembly();
            Assembly asse = load.Load(path);
            ViewType view = new ViewType();
            List<string> parentType = new List<string>();
            suma = view.ShowMethod(asse.GetType("ProjectTPA.Model.AccessLevel"), 0);
            Assert.IsTrue(suma.Contains("Equals"));
        }

        [TestMethod]
        public void ConsoleAppShowConstructor()
        {
            string suma = "";
            string path = "../../../Model/bin/Debug/Model.dll";
            LoadAssembly load = new LoadAssembly();
            Assembly asse = load.Load(path);
            ViewType view = new ViewType();
            List<string> parentType = new List<string>();
            suma = view.ShowConstructor(asse.GetType("ProjectTPA.Model.ParameterMetadata"), 0);
            Assert.AreNotEqual(suma, "");
        }
    }
}
