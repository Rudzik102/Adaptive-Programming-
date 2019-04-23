using ProjectTPA.Model;

namespace ProjectTPA.Interfaces
{
    public interface ISerializer
    {
        void Serialize(AssemblyMetadata _object, string path);
        AssemblyMetadata Deserialize(string path);
    }
}
