using ProjectTPA.Interfaces;
using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;

namespace ProjectTPA.Utility
{
    [Export(typeof(IPathLoader))]
    public class XamlFileChooser : IPathLoader
    {
        public  Assembly Load(string path)
        {
            string fullPath = Path.GetFullPath(path);
            FileInfo fileInfo = new FileInfo(fullPath);

            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException("File doesn't exist!");
            }

            return Assembly.LoadFrom(fileInfo.FullName);
        }
    }
}
