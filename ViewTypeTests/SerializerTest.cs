using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectTPA.Database.Serialize;
using ProjectTPA.Model;
using ProjectTPA.XmlSerializer;

namespace ViewTypeTests
{
    [TestClass]
    public class SerializerTest
    {
        private string path = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "ViewTypeTests\\bin\\Debug\\Test.xml");
        private string pathToDLL = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Model\\bin\\Debug\\Model.dll");

        [TestMethod]
        public void XmlSerialization()
        {
            Reflector reflector = new Reflector(pathToDLL);
            XmlSerializer xmlSerialization = new XmlSerializer();
            xmlSerialization.Serialize(reflector.AssemblyModel, path);
            AssemblyMetadata model = xmlSerialization.Deserialize(path);
            Assert.AreEqual(1, model.NamespaceModels.Count);
        }

        [TestMethod]
        public void XmlDeserialization()
        {
            Reflector reflector = new Reflector(pathToDLL);
            XmlSerializer xmlSerialization = new XmlSerializer();
            xmlSerialization.Serialize(reflector.AssemblyModel, path);
            AssemblyMetadata model = xmlSerialization.Deserialize(path);

            List<TypeMetadata> testLibraryTypes = model.NamespaceModels.Find(t => t.Name == "ProjectTPA.Model").Types;
            Assert.AreEqual(16, testLibraryTypes.Count);
        }

        [TestMethod]
        public void DatabaseSerialization()
        {
            Reflector reflector = new Reflector(pathToDLL);
            DatabaseSerializer xmlSerialization = new DatabaseSerializer();
            xmlSerialization.Serialize(reflector.AssemblyModel, path);
            AssemblyMetadata model = xmlSerialization.Deserialize(path);
            Assert.AreEqual(1, model.NamespaceModels.Count);
        }

        [TestMethod]
        public void DatabaseDeserialization()
        {
            Reflector reflector = new Reflector(pathToDLL);
            DatabaseSerializer xmlSerialization = new DatabaseSerializer();
            xmlSerialization.Serialize(reflector.AssemblyModel, path);
            AssemblyMetadata model = xmlSerialization.Deserialize(path);

            List<TypeMetadata> testLibraryTypes = model.NamespaceModels.Find(t => t.Name == "ProjectTPA.Model").Types;
            Assert.AreEqual(16, testLibraryTypes.Count);
        }
    }
}
