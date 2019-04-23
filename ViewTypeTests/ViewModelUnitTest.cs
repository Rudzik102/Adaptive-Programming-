using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectTPA.Utility;
using ProjectTPA.ViewModel;
using ProjectTPA.ViewModel.Module;

namespace ViewTypeTests
{
    [TestClass]
    public class ViewModelUnitTest
    {
        private string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Model\\bin\\Debug\\Model.dll");

        [TestMethod]
        public void ViewModelGetChildren()
        {
            XamlFileChooser load = new XamlFileChooser();
            Assembly asse = load.Load(path);
            TreeViewItem nw = new TreeViewItem();
            List<string> parentType = new List<string>();
            List<TreeViewItem> cos = ViewType.GetChildren(asse.GetType("ProjectTPA.Model.PropertyMetadata"), nw, parentType);
            Assert.AreNotEqual(cos.Count, 0);
        }

        [TestMethod]
        public void ViewModelShowMethod()
        {
            string suma = "";
            XamlFileChooser load = new XamlFileChooser();
            Assembly asse = load.Load(path);
            List<string> parentType = new List<string>();
            suma = ViewType.ShowMethod(asse.GetType("ProjectTPA.Model.AccessLevel"));
            Assert.IsTrue(suma.Contains("Equals"));
        }

        [TestMethod]
        public void ViewModelShowConstructor()
        {
            string suma = "";
            XamlFileChooser load = new XamlFileChooser();
            Assembly asse = load.Load(path);
            List<string> parentType = new List<string>();
            suma = ViewType.ShowConstructor(asse.GetType("ProjectTPA.Model.ParameterMetadata"));
            Assert.AreNotEqual(suma, "");
        }
    }
}
