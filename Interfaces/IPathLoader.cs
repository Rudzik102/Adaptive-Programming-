using System.Reflection;

namespace ProjectTPA.Interfaces
{
    public interface IPathLoader
    {
        Assembly Load(string path);
    }
}
