using ProjectTPA.Interfaces;
using ProjectTPA.Model;
using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.Serialization;

namespace ProjectTPA.XmlSerializer
{
    [Export(typeof(ISerializer))]
    public class XmlSerializer : ISerializer
    {
        public void Serialize(AssemblyMetadata _object, string path)
        {
            DataContractSerializer dataContractSerializer =
                new DataContractSerializer(typeof(AssemblyMetadata));

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                dataContractSerializer.WriteObject(fileStream, _object);
            }
        }

        public AssemblyMetadata Deserialize(string path)
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(AssemblyMetadata));
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                AssemblyMetadata as1 = (AssemblyMetadata)dataContractSerializer.ReadObject(fileStream);
                return as1;
            }
        }
    }
}
